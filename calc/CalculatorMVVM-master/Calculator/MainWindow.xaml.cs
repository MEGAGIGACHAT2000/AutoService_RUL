using Calculator.ViewModels;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel(new WpfApp2.Models.Calculator());
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property != DataContextProperty)
            {
                return;
            }
            if (e.OldValue is CalculatorViewModel oldViewModel)
            {
                oldViewModel.DividedByZero -= ShowDividedByZeroMessage;
            }
            if (e.NewValue is CalculatorViewModel newViewModel)
            {
                newViewModel.DividedByZero += ShowDividedByZeroMessage;
            }
        }
        private void ShowDividedByZeroMessage()
        {
            MessageBox.Show("Делить на 0 нельзя.");
        }
    }
}
