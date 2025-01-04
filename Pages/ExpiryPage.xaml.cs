using System.Collections.ObjectModel;
using WayofLife.ViewModel;
using WayofLifev2.Database;
using WayofLifev2.Database_File;
using WayofLifev2.Models;
using WayofLifev2.Pages;

namespace WayofLife.Pages;

public partial class ExpiryPage : ContentPage
{

    ExpiryDatabase jdatabase = new ();
    public ExpiryPage()
	{
        InitializeComponent();

        //this.RefreshData();
    }

    public ObservableCollection<Journal> eCollection = new(); //Has to be observable collection

    private void RefreshData()
    {

        try
        {
            //Has to be observable collection...
            //public ObservableCollection<Journal> eCollection = new ObservableCollection<Journal>();

            expiryListView.ItemsSource = eCollection;
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
        var eCollection = await jdatabase.GetExpiriesAsync();

        // Bind data to ListView
        expiryListView.ItemsSource = eCollection;
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
    private async void BtnNewExpiry(object sender, EventArgs e)
    {
        try
        {
            var expiryViewModel = new JournalViewModel(); // Create or reuse an instance

            await Navigation.PushAsync(page: new AddJournal(expiryViewModel));
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
            var item = await jdatabase.GetExpiryAsync(id);
            await jdatabase.DeleteExpiryAsync(item);

            // Refresh ListView
            expiryListView.ItemsSource = await jdatabase.GetExpiriesAsync();
        }
    }

    private async void expiryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Expiry selectedExpiry = (Expiry)e.SelectedItem;

            enName.Text = selectedExpiry.Name;
            pickDate.Date = selectedExpiry.ExpiryDateTime;
        }
        catch (Exception ex)
        {
            handleException(ex);
        }
    }
}