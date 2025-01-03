namespace WayofLife
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;

        public MainPage()
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine("Local Database File Path: " + Constant.DatabasePath);

            List<string> quotes =
            [
                "\"All you have to do...is look within\" -- John Greetings (Interface)",
                "\"It is a shame for a man to grow old without knowing the beauty and strength of which their body is capable.\" -- Socrates",
                "\"What are we waiting for? Just for the stars to align?!\" -- David Goggins",
                "\"How do you run the world if you can't fix your room?\" -- Book of the Later Han (后汉书)",
                "\"There Is A Difference Between Knowing The Path And Walking The Path.\" - - Morpheus"
            ];

            //Create new instance of Random
            Random random = new Random();

            string selectedQuote = quotes[random.Next(0, quotes.Count - 1)];
            //string selectedQuote = quotes[0];

            //Console.WriteLine(selectedQuote);
            //System.Diagnostics.Debug.WriteLine(selectedQuote);

            QuoteLabel.Text = selectedQuote;
        }
    }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

}
