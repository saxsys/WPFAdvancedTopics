using System.Threading;

namespace Janus.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string output;

        public MainWindowViewModel()
        {
            Output = "0";
        }

        public string Output
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