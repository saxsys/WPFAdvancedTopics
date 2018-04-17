using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Janus.Model;

namespace Janus.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand initializationCommand;
        private bool isLoading = true;
        private string output;
        private ICommand selectInputCommand;
        
        public MainWindowViewModel()
        {
            Output = "0";
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand InitializationCommand
        {
            get
            {
                if (initializationCommand == null)
                {
                    initializationCommand = new RelayCommand(ExecuteInitializeCommand);
                }

                return initializationCommand;
            }
        }

        public string Output
        {
            get => output;

            set
            {
                output = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectInputCommand
        {
            get
            {
                if (selectInputCommand == null)
                {
                    selectInputCommand = new RelayCommand(ExecuteSelectInputCommand);
                }

                return selectInputCommand;
            }
        }

        private void ExecuteInitializeCommand(object obj)
        {
            IsLoading = true;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                Dispatcher.CurrentDispatcher.Invoke(() => IsLoading = false);
            });
        }

        private void ExecuteSelectInputCommand(object parameter)
        {
            var input = parameter as string;
            if (input == null)
            {
                return;
            }

            if (input.Equals("C"))
            {
                Output = "0";
                return;
            }

            double number;
            if (double.TryParse(input, out number))
            {
                if (Output.Equals("0"))
                {
                    Output = number.ToString();
                }
                else
                {
                    Output += number.ToString();
                }

                return;
            }

            switch (input)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                case "=":
                    var temp = Output;
                    MathHelper.CalculateIfSecondOperator(input, ref temp);
                    Output = temp;
                    break;
                case ",":
                    AddOneComma();
                    break;
            }
        }

        private void AddOneComma()
        {
            if (Output.Contains(","))
            {
                return;
            }

            Output += ",";
        }
    }
}