using System.Collections.ObjectModel;
using WayofLifev2.Database;
using WayofLifev2.Models;

namespace WayofLife.Pages;

public partial class ExpiryPage : ContentPage
{

    ExpiryDatabase eDatabase = new ();
    public ExpiryPage()
	{
        InitializeComponent();
    }

    public ObservableCollection<Journal> eCollection = new(); //Has to be observable collection

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            // Fetch the data
            var eCollection = await eDatabase.GetExpiriesAsync();

            // Bind data to ListView
            expiryListView.ItemsSource = eCollection;
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private void HandleException(Exception ex)
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
            _ = DisplayAlert("Error", "Error Occured! See Details Below:\n\n" + exInEx.ToString(), "Ok");
        }
        finally
        {
            _ = DisplayAlert(caption, "Error Occured! See Details Below:\n\n" + msg, "Ok");
        }

    }

    private async void BtnNewExpiry(object sender, EventArgs e)
    {
        try
        {
            Expiry NewExpiry = new(enName.Text, pickDate.Date);
            //NewExpiry.Category = pickCategory.SelectedItem.ToString();

            await eDatabase.SaveExpiryAsync(NewExpiry);
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
            var button = (Button)sender; // Get the clicked button
            int id = (int)button.CommandParameter; // Get the ID of the item
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this?", "Yes", "No");
            if (confirm)
            {
                // Delete from database
                var item = await eDatabase.GetExpiryAsync(id);
                await eDatabase.DeleteExpiryAsync(item);
                // Refresh ListView
                expiryListView.ItemsSource = await eDatabase.GetExpiriesAsync();
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private void expiryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Expiry selectedExpiry = (Expiry)e.SelectedItem;

            enName.Text                 = selectedExpiry.Name;
            pickDate.Date               = selectedExpiry.ExpiryDateTime;
            pickCategory.SelectedItem   = selectedExpiry.Category;
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
}