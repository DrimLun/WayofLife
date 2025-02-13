using WayofLife.Databases;
using WayofLife.ViewModel;
using WayofLife.Models;

namespace WayofLife.Pages;

public partial class AddJournalPage : ContentPage
{
    public AddJournalPage(JournalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private JournalDatabase journalDatabase = new();
    private List <Category> cCollection = [];
    private readonly List<string> cNameList = [];

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _ = RefreshDataAsync();
    }

    private async Task RefreshDataAsync()
    {
        try
        {
            enCategory.ItemsSource = null;
            cNameList.Clear();
            cCollection.Clear();

            cCollection = await journalDatabase.GetCategoriesAsync();
            foreach (Category category in cCollection)
            {
                if (category.Name != null)
                    cNameList.Add(category.Name);
                else
                    continue;
            }
            enCategory.ItemsSource = cNameList;
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
        }
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

            //imagePreview = stream.Im //Practice code
            //imagePreview.Source = ImageSource.FromStream(() => stream);

            //var customFileType
            lblimagePreview.Text = "Image Preview:";
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
        }
    }

    private async void BtnAddJournal_Clicked(object sender, EventArgs e)
    {
        try
        {
            string inTitle;
            string inWrittenContent;
            string inCategory;
            string inImageContentPath;
            if (enContent.Text == null)
                    {
                        await DisplayAlert("Error", "Please write something", "Ok");
                        return;
                    }
                    else
                    {
                        if (enCategory.SelectedItem == null)
                {
                    inTitle = enTitle.Text;
                    inWrittenContent = enContent.Text;
                    inImageContentPath = "";
                    inCategory = "";
                    Journal newJournal = new(inTitle, inCategory, inWrittenContent, inImageContentPath);
                    await journalDatabase.SaveJournalAsync(newJournal);
                    await Shell.Current.GoToAsync("..");
                }
                        else if (enCategory.SelectedItem.ToString() != null)
                {
                        inTitle = enTitle.Text;
                        inWrittenContent = enContent.Text;
                        inCategory = enCategory.SelectedItem.ToString()!;
                        inImageContentPath = "";
                    Journal newJournal = new(inTitle, inCategory, inWrittenContent, inImageContentPath);

                    await journalDatabase.SaveJournalAsync(newJournal);
                    await Shell.Current.GoToAsync("..");
                }

                    }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
        }
    }

    private async Task HandleExceptionAsync(Exception ex)
    {
        string msg = ex.Message.ToString();
        string caption = "Error";

        try
        {
            
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