using System.Collections.ObjectModel;
using WayofLife.Databases;
using WayofLife.Models;
using WayofLife.ViewModel;

namespace WayofLife.Pages;

public partial class JournalPage : ContentPage
{
	public JournalPage()
	{
		InitializeComponent();
        _ = RefreshJournalsAsync();
    }

    readonly JournalDatabase jdatabase = new();

    public ObservableCollection<Journal> jCollection = [];

    private async Task RefreshJournalsAsync()
    {

        try
        {
            //Has to be observable collection...
            //public ObservableCollection<Journal> jCollection = new ObservableCollection<Journal>();
            var jCollection = await jdatabase.GetJournalsAsync();

            // Bind data to ListView
            journalListView.ItemsSource = jCollection;
        }
        catch (Exception ex)
        {
            //catch exception then pass it
            HandleException(ex);
        }
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            _ = RefreshJournalsAsync();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private void HandleException(Exception ex)
    {
        string msg = ex.Message.ToString();

        _ = DisplayAlert("Expiry Page Error", "Error Occured! See Details Below:\n\n" + ex, "Ok");

    }

    //https://devblogs.microsoft.com/dotnet/announcing-dotnet-maui-preview-11/

    //https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/button?view=net-maui-9.0
    private void BtnNewJournal(object sender, EventArgs e)
    {
        _ = NewJournalAsync();
    }

    private async Task NewJournalAsync()
    {
        try
        {
            var journalViewModel = new JournalViewModel(); // Create or reuse an instance

            await Navigation.PushAsync(page: new AddJournalPage(journalViewModel));
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = (ImageButton)sender; // Get the clicked button
            int id = (int)button.CommandParameter; // Get the ID of the item

            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this?", "Yes", "No");
            if (confirm)
            {
                // Delete from database
                var item = await jdatabase.GetJournalAsync(id);
                await jdatabase.DeleteJournalAsync(item);

                // Refresh ListView
                journalListView.ItemsSource = await jdatabase.GetJournalsAsync();
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private async void journalListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var journalViewModel = new JournalViewModel(); // Create or reuse an instance

            Journal selectedJournal = (Journal)e.SelectedItem;

            if (selectedJournal != null)
            {
                await Navigation.PushAsync(new SingleJournal(journalViewModel, (int)selectedJournal.Id));
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
}