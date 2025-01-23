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

    //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = (ImageButton)sender;
            int id = (int)button.CommandParameter;
            var category = (Category)button.BindingContext;

            if (category.Name == null)
            {
                await DisplayAlert("Error", "Category name is null", "OK");
                return;
            }
            else
            {
                bool confirm = await DisplayAlert("Delete", $"Are you sure you want to delete category {category.Name}?", "Yes", "No");

                if (confirm)
                {
                    var itemToDelete = await expiryDatabase.GetCategoryAsync(id);
                    await expiryDatabase.DeleteCategoryAsync(itemToDelete);
                }

            }
            await RefreshECListAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void btn_NewExpiryCategory_Clicked(object sender, EventArgs e)
    {
        _ = NewExpiryCategory();
    }

    private async Task NewExpiryCategory()
    {
        string newCategory = await DisplayPromptAsync("New Category", "Enter a name for the new category:", "OK", "Cancel", "Category Name");

        await expiryDatabase.SaveCategoryAsync(new Category(newCategory, "No color yet womp womp"));

        await RefreshECListAsync();
    }
}