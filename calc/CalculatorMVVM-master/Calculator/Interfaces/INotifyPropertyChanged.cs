using System.ComponentModel;

namespace Calculator.ViewModels
{
    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
