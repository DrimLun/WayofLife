﻿using Plugin.LocalNotification;
using System.Text;
using WayofLife.Databases;
using WayofLife.Models;

namespace WayofLife
{
    public partial class MainPage : ContentPage
    {
        public static List<string> selectedQuotes = [];
        private readonly static List<string> motivationModes = ["Daily", "Rebuilding", "Philosophical", "Hardcore"];
        private readonly static string quoteSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "quoteSettings.ini");
        private string selectedMode = "";
        private readonly Random random = new();
        public MainPage()
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine("Local Database File Path: " + Constants.DatabasePath);

            picker_motivationModes.ItemsSource = motivationModes;

            if (File.Exists(quoteSettingsPath))
            {
                selectedMode = File.ReadAllText(quoteSettingsPath, Encoding.UTF8);
            }
            else
            {
                if (File.Exists(quoteSettingsPath))
                {
                    selectedMode = File.ReadAllText(quoteSettingsPath, Encoding.UTF8);
                }
                else
                {
                    //string writablePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "quoteSettings.txt");
                    AddText(File.Create(quoteSettingsPath), "Daily");
                }
                //AddText(File.Create(quoteSettingsPath), "Daily");
            }
            
            picker_motivationModes.SelectedItem = selectedMode;
            switch (selectedMode)
            {
                case "Daily":
                    selectedQuotes.Clear();
                    selectedQuotes.AddRange(dailyQuotes);
                    break;
                case "Rebuilding":
                    selectedQuotes.Clear();
                    selectedQuotes.AddRange(rebuildingQuotes);
                    break;
                case "Philosophical":
                    selectedQuotes.Clear();
                    selectedQuotes.AddRange(philisophicalQuotes);
                    break;
                case "Hardcore":
                    selectedQuotes.Clear();
                    selectedQuotes.AddRange(hardcoreQuotes);
                    break;
            }

            GetRandomQuote();

            //PrintSelectedQuotes("MainPage()"); //Debugging
        }

        private void SaveQuoteMode()
        {
            try
            { 
                if (File.Exists(quoteSettingsPath))
                {
                    //selectedMode = File.ReadAllText(quoteSettingsPath, Encoding.UTF8);
                    using FileStream fs = File.Create(quoteSettingsPath);
                    AddText(fs, selectedMode);
                }
                MainPage.PrintSelectedQuotes("RefreshQuote()");
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
                string selectedQuote = selectedQuotes[random.Next(0, selectedQuotes.Count - 1)];
                //string selectedQuote = quotes[0];

                //Console.WriteLine(selectedQuote);
                System.Diagnostics.Debug.WriteLine(selectedQuote);

                QuoteLabel.Text = selectedQuote;

                MainPage.PrintSelectedQuotes("GetRandomQuote()");
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
            "The world ain't all sunshine and rainbows. -- Rocky"
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
            "Y'all don't what it is to do the hard work! -- David Goggins"
            ];

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            GetRandomQuote();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (picker_motivationModes.SelectedIndex != -1)
            {
                this.selectedMode = motivationModes[picker_motivationModes.SelectedIndex];

                switch(selectedMode)
                {
                    case "Daily":
                        selectedQuotes.Clear();
                        selectedQuotes.AddRange(dailyQuotes);
                        break;
                    case "Rebuilding":
                        selectedQuotes.Clear();
                        selectedQuotes.AddRange(rebuildingQuotes);
                        break;
                    case "Philosophical":
                        selectedQuotes.Clear();
                        selectedQuotes.AddRange(philisophicalQuotes);
                        break;
                    case "Hardcore":
                        selectedQuotes.Clear();
                        selectedQuotes.AddRange(hardcoreQuotes);
                        break;
                }

                SaveQuoteMode();
                this.GetRandomQuote();

                PrintSelectedQuotes("Picker()");
            }
            else
            {
                DisplayAlert("Error", "There is something wrong with the selection...", "Ok");
            }
        }

        private static void PrintSelectedQuotes(string methodRunning)
        {
            System.Diagnostics.Debug.WriteLine("\n" + methodRunning + ":" + "\nSelected Quotes:\n");
            foreach (string quote in selectedQuotes)
            {
                System.Diagnostics.Debug.WriteLine(quote);
            }
        }

        private readonly static ExpiryDatabase eDatabase = new();
        private static List<Expiry> eCollection = [];
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                await RFExpiry();

                if (CheckExpiry() != "")
                {
                    //https://www.youtube.com/watch?v=c_nbI0-FeOo
                    var request = new NotificationRequest
                    {
                        NotificationId = 1000,
                        Title = "Expired Item",
                        Description = "The following item(s) has expired: " + CheckExpiry(),
                        BadgeNumber = 42,
                        Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(0.3), NotifyRepeatInterval = TimeSpan.FromDays(1), }
                    };
                    LocalNotificationCenter.Current.Show(request);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async static Task RFExpiry()
        {
            eCollection = await eDatabase.GetExpiriesAsync();
        }

        public static string CheckExpiry()
        {
            string expiredItemList = "";
            foreach (var item in eCollection)
            {
                if (item.ExpiryDateTime < DateTime.Now)
                {
                    expiredItemList += "\n· " + item.Name; //interpunct
                }
            }

            return expiredItemList;
        }
    }

}
