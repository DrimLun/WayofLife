using WayofLife.ViewModel;
using WayofLifev2.Database_File;
using WayofLifev2.Models;

namespace WayofLifev2.Pages;

public partial class SingleJournal : ContentPage
{
    private readonly JournalDatabase jdatabase = new ();

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
                lblCategory.Text = selectedJournal!.Category;
                lblContent.Text = selectedJournal!.WrittenContent;
                lblDateTime.Text = selectedJournal!.DateTime.ToString();
            }
        }
        catch(Exception ex)
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
