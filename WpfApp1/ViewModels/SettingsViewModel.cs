using System.Windows.Input;
using BullsAndCowsWPF.Infrastructure.Command;
using BullsAndCowsWPF.Models;
using BullsAndCowsWPF.Services.Interfaces;
using BullsAndCowsWPF.ViewModels.Base;

namespace BullsAndCowsWPF.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IGameEngine _gameEngine;

        private int _numberLength = 4;
        public int NumberLength
        {
            get => _numberLength;
            set => Set(ref _numberLength, value);
        }

        private int _minDigit = 0;
        public int MinDigit
        {
            get => _minDigit;
            set => Set(ref _minDigit, value);
        }

        private int _maxDigit = 9;
        public int MaxDigit
        {
            get => _maxDigit;
            set => Set(ref _maxDigit, value);
        }

        private bool _allowDuplicates = false;
        public bool AllowDuplicates
        {
            get => _allowDuplicates;
            set => Set(ref _allowDuplicates, value);
        }

        private int _maxAttempts = 0;
        public int MaxAttempts
        {
            get => _maxAttempts;
            set => Set(ref _maxAttempts, value);
        }

        public ICommand ApplySettingsCommand { get; }

        public SettingsViewModel(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            ApplySettingsCommand = new LambdaCommand(_ => _gameEngine.StartNewGame(ToSettings()));
        }

        public GameSettings ToSettings() => new GameSettings
        {
            NumberLength = NumberLength,
            MinDigit = MinDigit,
            MaxDigit = MaxDigit,
            AllowDuplicates = AllowDuplicates,
            MaxAttempts = MaxAttempts
        };
    }
}
