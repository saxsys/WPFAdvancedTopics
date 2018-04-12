namespace Janus.ViewModel
{
    using System.Windows.Input;

    using Janus.Model;

    public class MainWindowViewModel : ViewModelBase
    {
        private string output;

        private ICommand selectInputCommand;

        public MainWindowViewModel()
        {
            this.Output = "0";
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

        public ICommand SelectInputCommand
        {
            get
            {
                if (this.selectInputCommand == null)
                {
                    this.selectInputCommand = new RelayCommand(this.ExecuteSelectInputCommand);
                }

                return this.selectInputCommand;
            }
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
                this.Output = "0";
                return;
            }

            double number;
            if (double.TryParse(input, out number))
            {
                if (this.Output.Equals("0"))
                {
                    this.Output = number.ToString();
                }
                else
                {
                    this.Output += number.ToString();
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
                    var temp = this.Output;
                    MathHelper.CalculateIfSecondOperator(input, ref temp);
                    this.Output = temp;
                    break;
                case ",":
                    this.AddOneComma();
                    break;
            }
        }

        private void AddOneComma()
        {
            if (this.Output.Contains(","))
            {
                return;
            }

            this.Output += ",";
        }
    }
}