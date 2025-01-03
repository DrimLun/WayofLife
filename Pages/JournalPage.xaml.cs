using System.Collections.ObjectModel;
using WayofLife.ViewModel;
using WayofLifev2.Database_File;
using WayofLifev2.Models;
using WayofLifev2.Pages;
using WayofLifev2.Repositories;

namespace WayofLife.Pages;

public partial class JournalPage : ContentPage
{

    JournalDatabase jdatabase = new JournalDatabase();
    public JournalPage()
	{
        InitializeComponent();

        //this.RefreshData();
        ////Has to be observable collection
    }

    public ObservableCollection<Journal> jCollection = new ObservableCollection<Journal>();

    private void RefreshData()
    {

        try
        {
            //Has to be observable collection...
            //public ObservableCollection<Journal> jCollection = new ObservableCollection<Journal>();

            journalListView.ItemsSource = jCollection;
        }
        catch (Exception ex)
        {
            //catch exception then pass it
            handleException(ex);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Fetch the data
        var jCollection = await jdatabase.GetJournalsAsync();

        // Bind data to ListView
        journalListView.ItemsSource = jCollection;
    }

    private async void handleException(Exception ex)
    {

        string msg = ex.Message.ToString();
        string caption = "Error";

        try
        {
            //https://stackoverflow.com/questions/21307789/how-to-save-exception-in-txt-file
            //new MessageWriteToFile(ex).WriteToFile();
        }
        catch (Exception exInEx)
        {
            await DisplayAlert("Error", "Error Occured! See Details Below:\n\n" + exInEx.Message.ToString(), "Ok");
        }
        finally
        {
            await DisplayAlert(caption, "Error Occured! See Details Below:\n\n" + msg, "Ok");
        }

    }

    //https://devblogs.microsoft.com/dotnet/announcing-dotnet-maui-preview-11/

    //https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/button?view=net-maui-9.0
    private async void BtnNewJournal(object sender, EventArgs e)
    {
        try
        {
            var journalViewModel = new JournalViewModel(); // Create or reuse an instance

            await Navigation.PushAsync(page: new AddJournal(journalViewModel));
        }
        catch (Exception ex)
        {
            handleException(ex);
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
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
            handleException(ex);
        }
    }
}