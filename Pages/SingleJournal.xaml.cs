using System.Collections.ObjectModel;
using WayofLife.ViewModel;
using WayofLifev2.Database_File;
using WayofLifev2.Models;

namespace WayofLifev2.Pages;

public partial class SingleJournal : ContentPage
{
    private readonly JournalDatabase journalDatabase = new ();

    private Journal? selectedJournal;
    private static int selectedID = 0;

    protected ObservableCollection<Category> cCollection = [];
    public List<string> cNameList = [];

    public SingleJournal(JournalViewModel vm, int id)
	{
		InitializeComponent();
        BindingContext = vm;

        selectedID = id;
        _ = FillCategoryPickerAsync();
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
                lblTitle.Text       = selectedJournal!.Title;
                lblCategory.Text    = selectedJournal!.Category;
                lblContent.Text     = selectedJournal!.WrittenContent;
                lblDateTime.Text    = selectedJournal!.DateTime.ToString();
            }

            _= FillCategoryPickerAsync();
        }
        catch(Exception ex)
        {
            HandleException(ex);
        }
    }
    private async Task FillCategoryPickerAsync()
    {
        var cCollection = await journalDatabase.GetCategoriesAsync();

        foreach (Category category in cCollection)
        {
            if (category.Name != null)
                cNameList.Add(category.Name);
            else
                cNameList.Add("No Category");
        }
        enCategory.ItemsSource = cNameList;
    }
    private void HandleException(Exception ex)
    {
        string msg = ex.Message.ToString();

        _ = DisplayAlert("Expiry Page Error", "Error Occured! See Details Below:\n\n" + ex, "Ok");

    }

    private void btn_editJournal_Clicked(object sender, EventArgs e)
    {
        lblTitle.IsVisible      = false;
        lblCategory.IsVisible   = false;
        lblContent.IsVisible    = false;
        lblDateTime.IsVisible   = false;

        enCategory.IsVisible    = true;
        enTitle.IsVisible       = true;
        enContent.IsVisible     = true;
        enDateTime.IsVisible    = true;

        enTitle.Text            = selectedJournal!.Title;
        enCategory.SelectedItem = selectedJournal!.Category;
        enContent.Text          = selectedJournal!.WrittenContent;
        enDateTime.Text         = selectedJournal!.DateTime.ToString();

        btn_editJournal.IsVisible = false;
        btn_saveJournal.IsVisible = true;
    }

    private void btn_saveJournal_Clicked(object sender, EventArgs e)
    {
        _ = SaveJournalAsync();
    }

    public async Task SaveJournalAsync()
    {
        if (enContent.Text == null)
        {
            await DisplayAlert("Error", "Please write something", "Ok");
            return;
        }
        else
        {
            selectedJournal!.Title              = enTitle.Text;
            selectedJournal!.WrittenContent     = enContent.Text;
            selectedJournal!.Category           = enCategory.SelectedItem.ToString()!;
            selectedJournal!.ImageContentPath   = "";

            //Journal editedJournal = new(inTitle, inCategory, inWrittenContent, inImageContentPath);
            btn_saveJournal.IsVisible = false;
            btn_editJournal.IsVisible = true;

            await journalDatabase.SaveJournalAsync(selectedJournal);
            await Shell.Current.GoToAsync("..");
        }
    }
}
