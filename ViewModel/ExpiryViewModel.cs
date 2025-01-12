//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using MvvmHelpers.Commands;
//using WayofLifev2.Database;
//using WayofLifev2.Models;

//namespace WayofLifev2.ViewModel;

//public class ExpiryViewModel : INotifyPropertyChanged
//{
//    private readonly ExpiryDatabase eDatabase;
//    public ObservableCollection<Expiry> ExpiryItems { get; set; } = new();
//    public AsyncCommand<int> DeleteCommand { get; }

//    public event PropertyChangedEventHandler? PropertyChanged;

//    public ExpiryViewModel(ExpiryDatabase database)
//    {
//        eDatabase = database;
//        DeleteCommand = new AsyncCommand<int>(DeleteExpiryAsync);
//    }

//    public async Task RefreshDataAsync()
//    {
//        try
//        {
//            var eCollection = await eDatabase.GetExpiriesAsync();
//            ExpiryItems.Clear();

//            foreach (var expiry in eCollection)
//            {
//                ExpiryItems.Add(expiry);
//            }
//        }
//        catch (Exception ex)
//        {
//            System.Diagnostics.Debug.WriteLine($"Error refreshing data: {ex.Message}");
//        }
//    }

//    private async Task DeleteExpiryAsync(int id)
//    {
//        try
//        {
//            var expiryToRemove = ExpiryItems.FirstOrDefault(e => e.Id == id);

//            if (expiryToRemove != null)
//            {
//                await eDatabase.DeleteExpiryAsync(expiryToRemove);
//                ExpiryItems.Remove(expiryToRemove);
//            }
//        }
//        catch (Exception ex)
//        {
//            System.Diagnostics.Debug.WriteLine($"Error deleting expiry: {ex.Message}");
//        }
//    }

//    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}
