using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BullsAndCowsWPF.Infrastructure.Command;
using BullsAndCowsWPF.Models;
using BullsAndCowsWPF.Services.Interfaces;
using BullsAndCowsWPF.ViewModels.Base;

namespace BullsAndCowsWPF.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private readonly IGameEngine _gameEngine;

        private string _userGuess;
        public string UserGuess
        {
            get => _userGuess;
            set
            {
                if (Set(ref _userGuess, value))
                    CommandManager.InvalidateRequerySuggested();
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => Set(ref _statusMessage, value);
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set
            {
                if (Set(ref _isGameOver, value))
                    CommandManager.InvalidateRequerySuggested();
            }
        }

        public GameSettings Settings => _gameEngine.Settings;
        public ObservableCollection<GameResult> Results { get; } = new ObservableCollection<GameResult>();

        public ICommand MakeGuessCommand { get; }
        public ICommand NewGameCommand { get; }

        public GameViewModel(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            _gameEngine.GameStarted += OnGameStarted;
            MakeGuessCommand = new LambdaCommand(MakeGuessExecute, MakeGuessCanExecute);
            NewGameCommand = new LambdaCommand(_ => StartNewGame());
            StartNewGame();
        }

        private void OnGameStarted(GameSettings settings)
        {
            Results.Clear();
            UserGuess = string.Empty;
            StatusMessage = $"Угадайте {settings.NumberLength}-значное число!";
            IsGameOver = false;
            OnPropertyChanged(nameof(Settings));
            CommandManager.InvalidateRequerySuggested();
        }

        public void StartNewGame()
        {
            _gameEngine.StartNewGame();
        }

        private bool MakeGuessCanExecute(object p) => !IsGameOver && !string.IsNullOrWhiteSpace(UserGuess);

        private void MakeGuessExecute(object p)
        {
            if (!_gameEngine.ValidateGuess(UserGuess, out string error))
            {
                StatusMessage = error;
                return;
            }

            var result = _gameEngine.MakeGuess(UserGuess);
            Results.Insert(0, result);
            UserGuess = string.Empty;

            if (_gameEngine.IsWin)
            {
                StatusMessage = $"Победа! Загаданное число: {_gameEngine.SecretNumber}. Попыток: {_gameEngine.AttemptCount}";
                IsGameOver = true;
            }
            else if (_gameEngine.IsGameOver)
            {
                StatusMessage = $"Проигрыш! Загаданное число: {_gameEngine.SecretNumber}.";
                IsGameOver = true;
            }
            else
            {
                StatusMessage = $"Быков: {result.Bulls}, Коров: {result.Cows}. Попыток: {_gameEngine.AttemptCount}";
            }
        }
    }
}
