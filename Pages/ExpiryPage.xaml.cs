using MvvmHelpers.Commands;
using WayofLifev2.Database;
using WayofLifev2.Models;

namespace WayofLife.Pages;

public partial class ExpiryPage : ContentPage
{
    private ExpiryDatabase eDatabase = new ();
    public required AsyncCommand<int> NewExpiryCommand { get; init; }
    public ExpiryPage()
	{
        try
        {
            InitializeComponent();
            NewExpiryCommand = new AsyncCommand<int>(async(id) => await New_ExpiryAsync());
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private readonly List<string> cList = [];

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
                    continue;
            }
            pickCategory.ItemsSource = cList;
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    private async Task New_ExpiryAsync()
    {
        try
        {
            Expiry NewExpiry = new(enName.Text, pickDate.Date);

            System.Diagnostics.Debug.WriteLine(pickCategory.SelectedItem);

            if (pickCategory.SelectedItem == null)
            {
                NewExpiry.Category = "No Category";
            }
            else
            {
                NewExpiry.Category = pickCategory.SelectedItem.ToString();
            }

            await eDatabase.SaveExpiryAsync(NewExpiry);

            _ = RefreshDataAsync();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    private void NewButton_Clicked(object sender, EventArgs e)
    {
        _ = New_ExpiryAsync();
    }
    private async Task Delete_ExpiryAsync(int id)
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

    private void ExpiryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Expiry selectedExpiry = (Expiry)e.SelectedItem;

            lblId.Text                  = selectedExpiry.Id.ToString();
            enName.Text                 = selectedExpiry.Name;
            pickDate.Date               = selectedExpiry.ExpiryDateTime;
            pickCategory.SelectedItem   = selectedExpiry.Category;

            btnDeleteExpiry.IsEnabled   = true;
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

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        _ = Delete_ExpiryAsync(Convert.ToInt32(lblId.Text));
    }


}