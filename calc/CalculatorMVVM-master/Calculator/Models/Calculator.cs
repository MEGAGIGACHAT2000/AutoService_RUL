using WpfApp1.Interfaces;

namespace WpfApp2.Models
{
    public class Calculator : ICalculator
    {
        public int Result { get; private set; }
        //Сложить
        public void Add(int a, int b)
        {
            Result = a + b;
        }
        //Вычесть
        public void Subtract(int a, int b)
        {
            Result = a - b;
        }
        //Умножить
        public void Multiply(int a, int b)
        {
            Result = a * b;
        }
        //Разделить
        public void Divide(int a, int b)
        {
            Result = a / b;
        }
    }
}
