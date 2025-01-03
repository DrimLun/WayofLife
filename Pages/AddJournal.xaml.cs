using WayofLife.ViewModel;

namespace WayofLife.Pages;

public partial class AddJournal : ContentPage
{
	public AddJournal(JournalViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
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
        JournalDatabase journalDatabase = new JournalDatabase();

        string inTitle = enTitle.Text;
        string inWrittenContent = enContent.Text;

        Journal newJournal = new Journal(inTitle, inWrittenContent);

        await journalDatabase.SaveItemAsync(newJournal);
        await Shell.Current.GoToAsync("..");

        //jCollection.Add();
    }
}