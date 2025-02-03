using MvvmHelpers.Commands;
using Plugin.LocalNotification;
using WayofLife.Databases;
using WayofLife.Models;

namespace WayofLife.Pages;

public partial class ExpiryPage : ContentPage
{
    private readonly static ExpiryDatabase eDatabase = new();
    public required AsyncCommand<int> NewExpiryCommand { get; init; }
    private static readonly List<string> cList = [];
    public static List<Expiry> eCollection = [];
    public static List<Category> cCollection = [];
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


    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            _ = RefreshDataAsync();

            if (CheckExpiry() != "")
            {
            //https://www.youtube.com/watch?v=c_nbI0-FeOo
                var request = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = "Expired Item",
                    Description = "The following item(s) has expired: " + CheckExpiry(),
                    BadgeNumber = 42,
                    Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(0.3), NotifyRepeatInterval = TimeSpan.FromDays(1), }
                };
                LocalNotificationCenter.Current.Show(request);
            }
            
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

            await WarnExpiredAsync(CheckExpiry());
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
                expiredItemList += "\n· " + item.Name; //interpunct
            }
        }

        return expiredItemList;
    }
    private async Task WarnExpiredAsync(string expiredItemList)
    {

        if (expiredItemList == "")
            return;
        else
            await DisplayAlert("Warning", "Item " + expiredItemList + " has expired!", "Ok");
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

            lblId.Text = selectedExpiry.Id.ToString();
            enName.Text = selectedExpiry.Name;
            pickDate.Date = selectedExpiry.ExpiryDateTime;
            pickCategory.SelectedItem = selectedExpiry.Category;

            btnDeleteExpiry.IsEnabled = true;
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