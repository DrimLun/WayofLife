using MvvmHelpers.Commands;
using WayofLife.Databases;
using WayofLife.Models;

namespace WayofLife.Pages;

public partial class ExpiryPage : ContentPage
{
    private readonly static ExpiryDatabase eDatabase = new();
    public required AsyncCommand<int> NewExpiryCommand { get; init; }
    private static readonly List<string> cList = [];
    private static List<Expiry> eCollection = [];
    private static List<Category> cCollection = [];
    public ExpiryPage()
    {
        try
        {
            InitializeComponent();
            NewExpiryCommand = new AsyncCommand<int>(async (id) => await New_ExpiryAsync());
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }


    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            await RefreshDataAsync();

            await WarnExpiredAsync(CheckExpiry());
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public async Task RefreshDataAsync()
    {
        try
        {

            // Fetch the data
            eCollection = await eDatabase.GetExpiriesAsync();
            cCollection = await eDatabase.GetCategoriesAsync();

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

            if (eCollection.Count != 0)
                lblPlaceholder.IsVisible = false;

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public static string CheckExpiry()
    {
        string expiredItemList = "";
        foreach (var item in eCollection)
        {
            if (item.ExpiryDateTime < DateTime.Now)
            {
                expiredItemList += "\n� " + item.Name; //interpunct
            }
        }

        return expiredItemList;
    }
    private async Task WarnExpiredAsync(string expiredItemList)
    {

        if (expiredItemList == "")
            return;
        else
            await DisplayAlert("Warning", "The following item(s) has expired: " + expiredItemList, "Ok");
    }

    private async Task New_ExpiryAsync()
    {
        try
        {
            if (enName.Text == "")
            {
                await DisplayAlert("Error", "Name cannot be empty", "Ok");
                return;
            }
            else if (pickDate.Date < DateTime.Now)
            {
                await DisplayAlert("Error", "Expiry date cannot be in the past or today", "Ok");
                return;
            }
            else
            {
                Expiry NewExpiry = new(enName.Text, pickDate.Date);

                //System.Diagnostics.Debug.WriteLine(pickCategory.SelectedItem); //For debug use

                if (pickCategory.SelectedItem == null)
                {
                    NewExpiry.Category = "No Category";
                }
                else
                {
                    NewExpiry.Category = pickCategory.SelectedItem.ToString();
                }

                await eDatabase.SaveExpiryAsync(NewExpiry);
            }

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

    private async Task EditExpiryAsync()
    {
        try
        {
            if (enName.Text == "")
            {
                await DisplayAlert("Error", "Name cannot be empty", "Ok");
                return;
            }
            else if (pickDate.Date < DateTime.Now)
            {
                await DisplayAlert("Error", "Expiry date cannot be in the past or today", "Ok");
                return;
            }
            else
            {
                Expiry EditedExpiry = await eDatabase.GetExpiryAsync(Convert.ToInt32(lblId.Text));
                EditedExpiry.Name = enName.Text;
                EditedExpiry.ExpiryDateTime = pickDate.Date;
                if (pickCategory.SelectedItem == null)
                {
                    EditedExpiry.Category = "No Category";
                }
                else
                {
                    EditedExpiry.Category = pickCategory.SelectedItem.ToString();
                }
                await eDatabase.SaveExpiryAsync(EditedExpiry);

                ClearEntries();
            }
            _ = RefreshDataAsync();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    private void EditButton_Clicked(object sender, EventArgs e)
    {
        _= EditExpiryAsync();
    }

    private async Task Delete_ExpiryAsync(int id)
    {
        try
        {
            bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this item?", "Yes", "No");
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
    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        _ = Delete_ExpiryAsync(Convert.ToInt32(lblId.Text));
    }

    private void ExpiryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Expiry selectedExpiry = (Expiry)e.SelectedItem;

            lblId.Text = selectedExpiry.Id.ToString();
            enName.Text = selectedExpiry.Name;
            pickDate.Date = selectedExpiry.ExpiryDateTime;
            pickCategory.SelectedItem = selectedExpiry.Category;

            btnAddExpiry.IsVisible = false;
            btnEditExpiry.IsVisible = true;
            btnDeleteExpiry.IsEnabled = true;
            btnClearExpiry.IsEnabled = true;
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
    private void ClearEntries()
    {
        expiryListView.SelectedItem = new Expiry(); //Haha I figured it out
        lblId.Text = "";
        enName.Text = "";
        pickDate.Date = DateTime.Now;
        pickCategory.SelectedIndex = -1;

        btnAddExpiry.IsVisible = true;
        btnEditExpiry.IsVisible = false;
        btnDeleteExpiry.IsEnabled = false;
        btnClearExpiry.IsEnabled = false;
    }
    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        ClearEntries();
    }
}