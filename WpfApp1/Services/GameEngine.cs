using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCowsWPF.Models;
using BullsAndCowsWPF.Services.Interfaces;

namespace BullsAndCowsWPF.Services
{
    public class GameEngine : IGameEngine
    {
        private readonly INumberGenerator _numberGenerator;
        private readonly List<GameResult> _history = new List<GameResult>();

        public GameEngine(INumberGenerator numberGenerator)
        {
            ArgumentNullException.ThrowIfNull(numberGenerator);
            _numberGenerator = numberGenerator;
        }

        public string SecretNumber { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }
        public int AttemptCount => _history.Count;
        public GameSettings Settings { get; private set; } = new GameSettings();
        public IReadOnlyList<GameResult> History => _history.AsReadOnly();

        public event Action<GameSettings> GameStarted;

        public void StartNewGame(GameSettings settings = null)
        {
            Settings = settings ?? Settings ?? new GameSettings();
            SecretNumber = _numberGenerator.GenerateNumber(
                Settings.NumberLength,
                Settings.MinDigit,
                Settings.MaxDigit,
                Settings.AllowDuplicates);
            IsGameOver = false;
            IsWin = false;
            _history.Clear();
            GameStarted?.Invoke(Settings);
        }

        public bool ValidateGuess(string guess, out string errorMessage)
        {
            errorMessage = null;
            if (string.IsNullOrWhiteSpace(guess))
            {
                errorMessage = "Введите число.";
                return false;
            }
            if (guess.Length != Settings.NumberLength)
            {
                errorMessage = $"Число должно содержать ровно {Settings.NumberLength} цифр.";
                return false;
            }
            if (!guess.All(char.IsDigit))
            {
                errorMessage = "Число должно содержать только цифры.";
                return false;
            }
            if (!Settings.AllowDuplicates && guess.Distinct().Count() != guess.Length)
            {
                errorMessage = "Цифры не должны повторяться.";
                return false;
            }
            foreach (char c in guess)
            {
                int digit = c - '0';
                if (digit < Settings.MinDigit || digit > Settings.MaxDigit)
                {
                    errorMessage = $"Цифры должны быть в диапазоне {Settings.MinDigit}-{Settings.MaxDigit}.";
                    return false;
                }
            }
            return true;
        }

        public GameResult MakeGuess(string guess)
        {
            if (IsGameOver)
                throw new InvalidOperationException("Игра уже завершена.");
            if (!ValidateGuess(guess, out string error))
                throw new ArgumentException(error);

            int bulls = 0;
            var secretRemaining = new List<char>();
            var guessRemaining = new List<char>();

            for (int i = 0; i < Settings.NumberLength; i++)
            {
                if (guess[i] == SecretNumber[i])
                    bulls++;
                else
                {
                    secretRemaining.Add(SecretNumber[i]);
                    guessRemaining.Add(guess[i]);
                }
            }

            int cows = 0;
            var secretCounts = secretRemaining.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            foreach (char c in guessRemaining)
            {
                if (secretCounts.TryGetValue(c, out int count) && count > 0)
                {
                    cows++;
                    secretCounts[c] = count - 1;
                }
            }

            var result = new GameResult
            {
                Guess = guess,
                Bulls = bulls,
                Cows = cows,
                AttemptNumber = _history.Count + 1
            };
            _history.Add(result);

            if (bulls == Settings.NumberLength)
            {
                IsWin = true;
                IsGameOver = true;
            }
            else if (Settings.MaxAttempts > 0 && _history.Count >= Settings.MaxAttempts)
            {
                IsGameOver = true;
            }

            return result;
        }
    }
}
