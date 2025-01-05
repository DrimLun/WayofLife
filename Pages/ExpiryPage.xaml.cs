using MvvmHelpers.Commands;
using WayofLifev2.Database;
using WayofLifev2.Models;

namespace WayofLife.Pages;

public partial class ExpiryPage : ContentPage
{

    ExpiryDatabase eDatabase = new ();
    public required AsyncCommand<int> DeleteCommand { get; init; }
    public required AsyncCommand<int> NewExpiryCommand { get; init; }
    public ExpiryPage()
	{
        try
        {
            InitializeComponent();
            DeleteCommand = new AsyncCommand<int>(async (id) => await DeleteButton_ClickedAsync(id));
            NewExpiryCommand = new AsyncCommand<int>(async(id) => await BtnNewExpiryAsync());
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private List<string> cList = [];

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            _ = RefreshDataAsync();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private async Task RefreshDataAsync()
    {
        try
        {
            base.OnAppearing();

            // Fetch the data
            var eCollection = await eDatabase.GetExpiriesAsync();
            var cCollection = await eDatabase.GetCategoriesAsync();

            // Bind data to ListView
            expiryListView.ItemsSource = eCollection;


            foreach (var category in cCollection)
            {
                if (category.Name != null)
                    cList.Add(category.Name);
                else
                    cList.Add("No Category");
            }
            pickCategory.ItemsSource = cList;
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

    private async Task BtnNewExpiryAsync()
    {
        try
        {
            Expiry NewExpiry = new(enName.Text, pickDate.Date);

            if (pickCategory.SelectedItem == null)
            {
                NewExpiry.Category = "No Category";
            }
            else
            {
                NewExpiry.Category = pickCategory.SelectedItem.ToString();
            }

            await eDatabase.SaveExpiryAsync(NewExpiry);
            await Shell.Current.GoToAsync(".");
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }


    private async Task DeleteButton_ClickedAsync(int id)
    {
        try
        {
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this?", "Yes", "No");
            if (confirm)
            {
                // Delete from database
                var item = await eDatabase.GetExpiryAsync(id);
                if (item != null)
                {
                    await eDatabase.DeleteExpiryAsync(item);
                }
                else
                {
                    await DisplayAlert("Error", "Item not found", "Ok");
                }
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