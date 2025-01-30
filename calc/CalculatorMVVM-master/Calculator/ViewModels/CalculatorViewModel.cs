using System;
using System.Diagnostics;
using System.Windows.Input;
using WpfApp1.Interfaces;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : ViewModelBase
    {

        public ICommand AddCommand { get; private set; }
        public ICommand SubtractCommand { get; private set; }
        public ICommand MultiplyCommand { get; private set; }
        public ICommand DivideCommand { get; private set; }

        public int FirstOperand
        {
            get
            {
                return firstOperand;
            }
            set
            {
                firstOperand = value;
                NotifyPropertyChanged(nameof(FirstOperand));
            }
        }
        private int firstOperand;
        public int SecondOperand
        {
            get
            {
                return secondOperand;
            }
            set
            {
                secondOperand = value;
                NotifyPropertyChanged(nameof(SecondOperand));
            }
        }
        private int secondOperand;
        public int Result
        {
            get
            {
                return calculator.Result;
            }
        }
        private ICalculator calculator;
        public event Action DividedByZero;
        public CalculatorViewModel(ICalculator model)
        {
            AddCommand = new ActionCommand(Add);
            SubtractCommand = new ActionCommand(Subtract);
            MultiplyCommand = new ActionCommand(Multiply);
            DivideCommand = new ActionCommand(Divide);
            calculator = model;
        }
        private void Add()
        {
            Debug.WriteLine("Команда сложения выполнена");
            calculator.Add(FirstOperand, SecondOperand);
            NotifyResultChanged();
        }
        private void Subtract()
        {
            calculator.Subtract(FirstOperand, SecondOperand);
            NotifyResultChanged();
        }
        private void Multiply()
        {
            calculator.Multiply(FirstOperand, SecondOperand);
            NotifyResultChanged();
        }
        private void Divide()
        {
            try
            {
                calculator.Divide(FirstOperand, SecondOperand);
                NotifyResultChanged();
            }
            catch (DivideByZeroException)
            {
                NotifyDividedByZero();
            }
        }
        private void NotifyDividedByZero()
        {
            if (DividedByZero != null)
            {
                DividedByZero?.Invoke();
            }
        }
        private void NotifyResultChanged()
        {
            NotifyPropertyChanged(nameof(Result));
        }
    }
}
