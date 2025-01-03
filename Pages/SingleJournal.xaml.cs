using WayofLife.ViewModel;
using WayofLifev2.Database_File;
using WayofLifev2.Models;
using WayofLifev2.Repositories;

namespace WayofLifev2.Pages;

public partial class SingleJournal : ContentPage
{
    private JournalDatabase jdatabase = new ();

    static int selectedID = 0;
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
            Journal selectedJournal = await this.jdatabase.GetJournalAsync(selectedID);

            if (selectedJournal == null)
            {
                await DisplayAlert("Null", "It's null bro", "Ok...");
            }
            else
            {
                lblTitle.Text = selectedJournal!.Title;
                lblContent.Text = selectedJournal!.WrittenContent;
                lblDateTime.Text = selectedJournal!.DateTime.ToString();
            }
        }
        catch(Exception ex)
        {
            handleException(ex);
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
