using System;
using System.Collections.Generic;
using BullsAndCowsWPF.Models;

namespace BullsAndCowsWPF.Services.Interfaces
{
    public interface IGameEngine
    {
        string SecretNumber { get; }
        bool IsGameOver { get; }
        bool IsWin { get; }
        int AttemptCount { get; }
        GameSettings Settings { get; }
        IReadOnlyList<GameResult> History { get; }
        event Action<GameSettings> GameStarted;

        void StartNewGame(GameSettings settings = null);
        GameResult MakeGuess(string guess);
        bool ValidateGuess(string guess, out string errorMessage);
    }
}
