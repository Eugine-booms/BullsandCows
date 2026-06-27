using System.ComponentModel;
using System.Windows.Controls;

namespace BullsAndCowsWPF.View
{
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged oldVm)
                oldVm.PropertyChanged -= OnViewModelPropertyChanged;
            if (e.NewValue is INotifyPropertyChanged newVm)
                newVm.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModels.GameViewModel.UserGuess))
            {
                var vm = DataContext as ViewModels.GameViewModel;
                if (vm != null && string.IsNullOrEmpty(vm.UserGuess))
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Action(() =>
                    {
                        GuessTextBox.Focus();
                    }));
                }
            }
        }
    }
}
