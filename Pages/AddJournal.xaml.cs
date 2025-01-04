using System.Collections.ObjectModel;
using WayofLife.ViewModel;
using WayofLifev2.Database_File;
using WayofLifev2.Models;
using WayofLifev2.Repositories;

namespace WayofLife.Pages;

public partial class AddJournal : ContentPage
{

    public AddJournal(JournalViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
    }

    private JournalDatabase journalDatabase = new();
    public ObservableCollection<Category> cCollection = new ObservableCollection<Category>();
    public List<string> cNameList = new List<string>();

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var cCollection = await journalDatabase.GetCategoriesAsync();

        foreach (Category category in cCollection)
        {
            cNameList.Add(category.Name);
        }

        enCategory.ItemsSource = cNameList;

    }

    private async void BtnAddImages_Clicked(object sender, EventArgs e)
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
            handleException(ex);
        }
    }

    private async void BtnAddJournal_Clicked(object sender, EventArgs e)
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

            Journal newJournal = new Journal(inTitle, inCategory, inWrittenContent, inImageContentPath);

            await journalDatabase.SaveJournalAsync(newJournal);
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void handleException(Exception ex)
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
            await DisplayAlert("Error", "Error Occured! See Details Below:\n\n" + exInEx.Message.ToString(), "Ok");
        }
        finally
        {
            await DisplayAlert(caption, "Error Occured! See Details Below:\n\n" + msg, "Ok");
        }

    }
}