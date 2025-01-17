using WayofLifev2.Database;
using WayofLifev2.Models;

namespace WayofLifev2.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

	private readonly ExpiryDatabase expiryDatabase = new();
	private static List<Category> eCategoryList = [];
    private static readonly List<string> eCNameList = [];

	public async Task RefreshECListAsync()
	{
        eCategoryList = await expiryDatabase.GetCategoriesAsync();

        foreach (var category in eCategoryList)
		{
            if (category.Name == null)
            {
                continue;
            }
            else
            {
                eCNameList.Add(category.Name);
            }
        }
    }

}