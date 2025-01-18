using WayofLife.Databases;
using WayofLife.Models;

namespace WayofLife.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        _ = RefreshECListAsync();
    }

    private readonly ExpiryDatabase expiryDatabase = new();
    protected List<Category> eCategoryList = [];

    public async Task RefreshECListAsync()
    {
        expiryCatListView.ItemsSource = null;
        eCategoryList.Clear();

        eCategoryList = await expiryDatabase.GetCategoriesAsync();

        expiryCatListView.ItemsSource = eCategoryList;
    }
    
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        int id = (int)button.CommandParameter;

        bool confirm = await DisplayAlert("Delete Expiry Category", "Are you sure you want to delete this category?", "Yes", "No");

        if (confirm)
        {
            var itemToDelete = await expiryDatabase.GetCategoryAsync(id);
            await expiryDatabase.DeleteCategoryAsync(itemToDelete);
            }
        
        await RefreshECListAsync();
    }
}