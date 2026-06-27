using System.Windows.Input;
using BullsAndCowsWPF.Infrastructure.Command;
using BullsAndCowsWPF.ViewModels.Base;

namespace BullsAndCowsWPF.ViewModels
{
    public class MainWindowsViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (Set(ref _currentViewModel, value))
                {
                    OnPropertyChanged(nameof(IsGameViewActive));
                    OnPropertyChanged(nameof(IsSettingsViewActive));
                }
            }
        }

        public bool IsGameViewActive => CurrentViewModel is GameViewModel;
        public bool IsSettingsViewActive => CurrentViewModel is SettingsViewModel;

        public ICommand ShowGameViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public MainWindowsViewModel(GameViewModel gameViewModel, SettingsViewModel settingsViewModel)
        {
            ShowGameViewCommand = new LambdaCommand(_ => CurrentViewModel = gameViewModel);
            ShowSettingsViewCommand = new LambdaCommand(_ => CurrentViewModel = settingsViewModel);
            CurrentViewModel = gameViewModel;
        }
    }
}
