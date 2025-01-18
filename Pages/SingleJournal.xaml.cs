using WayofLife.Databases;
using WayofLife.Models;
using WayofLife.ViewModel;

namespace WayofLife.Pages;

public partial class SingleJournal : ContentPage
{
    private readonly JournalDatabase journalDatabase = new();

    private Journal? selectedJournal;
    private static int selectedID = 0;

    //Category List to loop through, String list to contain the names...
    private static List<Category> cCollection = [];
    public List<string> cNameList = [];

    public SingleJournal(JournalViewModel vm, int id)
    {
        InitializeComponent();
        BindingContext = vm;

        selectedID = id;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            selectedJournal = await this.journalDatabase.GetJournalAsync(selectedID);

            if (selectedJournal == null)
            {
                await DisplayAlert("Null", "It's null bro", "Ok...");
            }
            else
            {
                lblTitle.Text = selectedJournal!.Title;
                lblCategory.Text = selectedJournal!.Category;
                lblContent.Text = selectedJournal!.WrittenContent;
                lblDateTime.Text = selectedJournal!.DateTime.ToString();
            }

            _ = FillCategoryPickerAsync();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    private async Task FillCategoryPickerAsync()
    {
        enCategory.ItemsSource = null;
        cNameList.Clear();
        cCollection.Clear();
        JournalDatabase journalDatabase = new();

        cCollection = await journalDatabase.GetCategoriesAsync();

        foreach (Category category in cCollection)
        {
            if (category.Name != null)
                cNameList.Add(category.Name);
            else
                continue;
            //cNameList.Add("No Category");
        }

        enCategory.ItemsSource = cNameList;
    }

    private void Btn_editJournal_Clicked(object sender, EventArgs e)
    {
        lblTitle.IsVisible = false;
        lblCategory.IsVisible = false;
        lblContent.IsVisible = false;
        lblDateTime.IsVisible = false;

        enCategory.IsVisible = true;
        enTitle.IsVisible = true;
        enContent.IsVisible = true;
        enDateTime.IsVisible = true;

        enTitle.Text = selectedJournal!.Title;
        enCategory.SelectedItem = selectedJournal!.Category;
        enContent.Text = selectedJournal!.WrittenContent;
        enDateTime.Text = selectedJournal!.DateTime.ToString();

        btn_addCategory.IsVisible = true;
        btn_editJournal.IsVisible = false;
        btn_saveJournal.IsVisible = true;
    }

    private void Btn_saveJournal_Clicked(object sender, EventArgs e)
    {
        _ = SaveJournalAsync();
    }

    protected async Task SaveJournalAsync()
    {
        if (enContent.Text == null)
        {
            await DisplayAlert("Error", "Please write something", "Ok");
            return;
        }
        else
        {
            selectedJournal!.Title = enTitle.Text;
            selectedJournal!.WrittenContent = enContent.Text;
            selectedJournal!.Category = enCategory.SelectedItem.ToString()!;
            selectedJournal!.ImageContentPath = "";

            await journalDatabase.SaveJournalAsync(selectedJournal);
            await Shell.Current.GoToAsync("..");
        }
    }

    private void Btn_addCategory_Clicked(object sender, EventArgs e)
    {
        _ = AddCategoryAsync();
        _ = FillCategoryPickerAsync();
    }

    protected async Task AddCategoryAsync()
    {
        try
        {
            string inCatName = await DisplayPromptAsync("Add Category", "Enter Category Name", "Ok", "Cancel", "Category Name");
            if (inCatName != null)
            {
                Category newCategory = new(inCatName, "");
                await journalDatabase.SaveCategoryAsync(newCategory);
                await FillCategoryPickerAsync();
            }
            else
            {
                await DisplayAlert("", "Please enter a category name", "Ok");
            }

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
}