using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using WayofLifev2.Database_File;
using WayofLifev2.Models;
using WayofLifev2.ViewModel;

namespace WayofLife.Pages;

public partial class AddJournalPage : ContentPage
{
    //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers
    public AsyncCommand AddImage { get; set; }
    public AsyncCommand AddJournal { get; set; }
    public AddJournalPage(JournalViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        AsyncCommand<int> AddImage = new(async (id) => await BtnAddImages_ClickedAsync());
        AsyncCommand<int> AddJournal = new(async (id) => await BtnAddJournal_ClickedAsync());
    }

    private JournalDatabase journalDatabase = new();
    public ObservableCollection<Category> cCollection = [];
    public List<string> cNameList = [];

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _ = RefreshDataAsync();
    }

    private async Task RefreshDataAsync()
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

    private async Task BtnAddImages_ClickedAsync()
    {
        try
        {
            var results = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Add a Picture",
                FileTypes = FilePickerFileType.Images
            });

            if (results == null)
            {
                await DisplayAlert("Error", "No file selected", "Ok");
            }

            var stream = await results!.OpenReadAsync();

            //imagePreview = stream.Im
            imagePreview.Source = ImageSource.FromStream(() => stream);

            //var customFileType
            lblimagePreview.Text = "Image Preview:";
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
        }
    }

    private async Task BtnAddJournal_ClickedAsync()
    {
        if (enContent.Text == null)
        {
            await DisplayAlert("Error", "Please write something", "Ok");
            return;
        }
        else
        {
            string inTitle = enTitle.Text;
            string inWrittenContent = enContent.Text;
            string inCategory = enCategory.SelectedItem.ToString()!;
            string inImageContentPath = "";

            Journal newJournal = new(inTitle, inCategory, inWrittenContent, inImageContentPath);

            await journalDatabase.SaveJournalAsync(newJournal);
            await Shell.Current.GoToAsync("..");
        }
    }

    private async Task HandleExceptionAsync(Exception ex)
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
            await DisplayAlert("Error", "Error Occured! See Details Below:\n\n" + exInEx.ToString(), "Ok");
        }
        finally
        {
            await DisplayAlert(caption, "Error Occured! See Details Below:\n\n" + msg, "Ok");
        }

    }
}