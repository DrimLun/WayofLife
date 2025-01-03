using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace WayofLife.ViewModel
{
    public partial class MainViewModel : ObservableObject, INotifyPropertyChanged
    {

        [ObservableProperty]
        string? text1;

        //public event PropertyChangedEventHandler? PropertyChanged;
        /////NOT who subscribe to to notify .net of updates
        ////BUT PropertyChanged( event ) is subcribed by .NET MAUI to know when UI update is needed

        //protected new void OnPropertyChanged (string name) => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));

        //[RelayCommand]
        //void Add()
        //{
        //    if (string.IsNullOrEmpty(text1))
        //    {
        //        return;
        //    }

        //    //Items.Add(text);

        //    text1 = string.Empty;
        //}

        //public string IP
        //{
        //    get { return text1; }
        //    set
        //    {
        //        text1 = value;
        //        OnPropertyChanged(nameof(text1));}
        //}
        //Enter then automically update

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
