
namespace WpfApp1.Interfaces
{
    public interface ICalculator
    {
        int Result { get; }
        void Add(int a, int b);
        void Subtract(int a, int b);
        void Multiply(int a, int b);
        void Divide(int a, int b);
    }
}
