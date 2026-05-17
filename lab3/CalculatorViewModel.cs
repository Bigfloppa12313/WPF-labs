using lab3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab3
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string displayText = "0";

        private double firstNumber;
        private string operation;

        public string DisplayText
        {
            get => displayText;
            set
            {
                displayText = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public ICommand NumberCommand { get; }
        public ICommand OperationCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand ClearCommand { get; }

        public CalculatorViewModel()
        {
            NumberCommand = new RelayCommand(AddNumber);
            OperationCommand = new RelayCommand(SetOperation);
            EqualCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Clear);
        }

        private void AddNumber(object parameter)
        {
            string number = parameter.ToString();

            if (DisplayText == "0")
                DisplayText = number;
            else
                DisplayText += number;
        }

        private void SetOperation(object parameter)
        {
            firstNumber = double.Parse(DisplayText);
            operation = parameter.ToString();
            DisplayText = "0";
        }

        private void Calculate(object parameter)
        {
            if (!double.TryParse(DisplayText, out double secondNumber))
            {
                DisplayText = "Error";
                return;
            }

            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;

                case "-":
                    result = firstNumber - secondNumber;
                    break;

                case "*":
                    result = firstNumber * secondNumber;
                    break;

                case "/":
                    if (secondNumber == 0)
                    {
                        DisplayText = "Error";
                        return;
                    }

                    result = firstNumber / secondNumber;
                    break;
            }

            DisplayText = result.ToString();
        }

        private void Clear(object parameter)
        {
            DisplayText = "0";
            firstNumber = 0;
            operation = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}