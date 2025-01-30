// ViewModels/CalculatorViewModel.cs
using System.Windows.Input;
using calc.interfaces;
using calc.models;

public class CalculatorViewModel : ViewModelBase
{
    private readonly CalculatorInterface _calculator;

    public CalculatorViewModel()
    {
        _calculator = new CalculatorModel();
    }

    private double _number1;
    public double Number1
    {
        get { return _number1; }
        set
        {
            _number1 = value;
            OnPropertyChanged();
        }
    }

    private double _number2;
    public double Number2
    {
        get { return _number2; }
        set
        {
            _number2 = value;
            OnPropertyChanged();
        }
    }

    private double _result;
    public double Result
    {
        get { return _result; }
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddCommand => new ActionCommand(Add);
    public ICommand SubtractCommand => new ActionCommand(Subtract);
    public ICommand MultiplyCommand => new ActionCommand(Multiply);
    public ICommand DivideCommand => new ActionCommand(Divide);

    private void Add(object parameter)
    {
        Result = _calculator.Add(Number1, Number2);
    }

    private void Subtract(object parameter)
    {
        Result = _calculator.Subtract(Number1, Number2);
    }

    private void Multiply(object parameter)
    {
        Result = _calculator.Multiply(Number1, Number2);
    }

    private void Divide(object parameter)
    {
        Result = _calculator.Divide(Number1, Number2);
    }
}
