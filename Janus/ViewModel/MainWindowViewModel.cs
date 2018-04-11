using System.Threading;

namespace Janus.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private double output;

        public MainWindowViewModel()
        {
            Output = 0;
        }

        public double Output
        {
            get
            {
                return this.output;
            }

            set
            {
                this.output = value;
                this.OnPropertyChanged();
            }
        }
    }
}