using System.Text;

namespace WayofLife
{
    public partial class MainPage : ContentPage
    {
        private static List<string> selectedQuotes = [];
        private readonly static List<string> motivationModes = ["Daily", "Rebuilding", "Philosophical", "Hardcore"];
        private readonly static string quoteSettingsPath = @"..\quoteSettings.txt";
        public MainPage()
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine("Local Database File Path: " + Constants.DatabasePath);

            picker_motivationModes.ItemsSource = motivationModes;
            RefreshQuote();
            GetRandomQuote();
        }

        private void RefreshQuote()
        {
            try
            {
                if (File.Exists(quoteSettingsPath))
                {
                    string quoteSettings = File.ReadAllText(quoteSettingsPath, Encoding.UTF8);
                    if (quoteSettings.Length > 0)
                    {
                        if (quoteSettings == "Daily")
                        {
                            selectedQuotes = dailyQuotes;
                        }
                        else if (quoteSettings == "Rebuilding")
                        {
                            selectedQuotes = rebuildingQuotes;
                        }
                        else if (quoteSettings == "Philosophical")
                        {
                            selectedQuotes = philisophicalQuotes;
                        }
                        else if (quoteSettings == "Hardcore")
                        {
                            selectedQuotes = hardcoreQuotes;
                        }
                        else
                        {
                            selectedQuotes = dailyQuotes;
                            QuoteLabel.Text += "Error: Invalid quote setting. Defaulting to Daily.";
                        }
                    }
                }
                else //Default to Daily
                {
                    using FileStream fs = File.Create(quoteSettingsPath);
                    AddText(fs, "Daily");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message + "\n\n" + ex);
            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void GetRandomQuote()
        {
            try
            {
                Random random = new();
                if (selectedQuotes.Count == 0)
                {
                    RefreshQuote();
                }

                string selectedQuote = selectedQuotes[random.Next(0, selectedQuotes.Count - 1)];
                //string selectedQuote = quotes[0];

                //Console.WriteLine(selectedQuote);
                System.Diagnostics.Debug.WriteLine(selectedQuote);

                QuoteLabel.Text = selectedQuote;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message + "\n\n" + ex);
            }
        }

        private static readonly List<string> rebuildingQuotes = [
            "Find the solution to your problems -- David Goggins",
            "\"How do you run the world if you can't fix your room?\" -- Book of the Later Han (后汉书)",
            "The only person you should try to be better than is the person you were yesterday. - Anonymous",
            "Success is not final, failure is not fatal: It is the courage to continue that counts. - Winston Churchill",
            "The world breaks everyone, and afterward, some are strong at the broken places. - Ernest Hemingway",
        ];

        private static readonly List<string> dailyQuotes =
        [
            "The best way to predict the future is to create it. - Abraham Lincoln",
            "The only way to do great work is to love what you do. - Steve Jobs",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt",
            "The only way to do great work is to love what you do. - Steve Jobs",
                "\"It is a shame for a man to grow old without knowing the beauty and strength of which their body is capable.\" -- Socrates",
            "The only easy day was yesterday. - Navy Seals"
        ];

        private static readonly List<string> philisophicalQuotes =
            [
                "\"All you have to do...is look within\" -- John Greetings (Interface)",
                "\"What are we waiting for? Just for the stars to align?!\" -- David Goggins",
                "\"There Is A Difference Between Knowing The Path And Walking The Path.\" - - Morpheus",
            "The only way to get rid of a temptation is to yield to it. - Napoleon Bonaparte",
            ];

        private static readonly List<string> hardcoreQuotes = [
            "You should know, not fear, that someday you are going to die. Until you know that and embrace that, you are useless. - Tyler Durden",
            "The more you sweat in training, the less you bleed in combat. - Navy Seals",
            "Without pain, without sacrifice, we would have nothing! - Tyler Durden",
            "You gain knowledge through suffering. -- David Goggins",
            "No pain, no gain. -- Arnold Schwarzenegger",
            "There's a lot of motherfuckers out there, who wants what you have! -- David Goggins",
            ];

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            RefreshQuote();
            GetRandomQuote();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker_motivationModes.SelectedIndex != -1)
            {
                string selectedMode = motivationModes[picker_motivationModes.SelectedIndex];
                using FileStream fs = File.Create(quoteSettingsPath);
                AddText(fs, selectedMode);
                RefreshQuote();
                GetRandomQuote();
            }
        }
    }

}
