using SQLite;
using WayofLife.Models;

namespace WayofLife.Databases
{
    internal class QuoteDatabase
    {
        //private SqliteConnection
        private readonly SQLiteAsyncConnection Database;
        public QuoteDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePathExpiry);
        }

        public async Task Init()
        {
            //var quoteDatabaseInfo = await Database.CreateTableAsync<Quote>();

            //if (quoteDatabaseInfo == 0)
            //{
            //    await
            //}

            if (Database != null)
            {
                var quoteDatabaseInfo = await Database.CreateTableAsync<Quote>();

                if (quoteDatabaseInfo == 0)
                {
                    await Database.InsertAllAsync(rebuildingQuotes.Select(q => new Quote { QuoteText = q , Category = "Rebuilding" }));
                    await Database.InsertAllAsync(dailyQuotes.Select(q => new Quote { QuoteText = q, Category = "Daily" }));
                    await Database.InsertAllAsync(philisophicalQuotes.Select(q => new Quote { QuoteText = q, Category = "Philosophical" }));
                    await Database.InsertAllAsync(hardcoreQuotes.Select(q => new Quote { QuoteText = q , Category = "Hardcore" }));
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Database is not initialized."); // Debug
            }
        }

        public static List<string> rebuildingQuotes = [
            "Find the solution to your problems -- David Goggins",
            "\"How do you run the world if you can't fix your room?\" -- Book of the Later Han (后汉书)",
            "The only person you should try to be better than is the person you were yesterday. - Anonymous",
            "Success is not final, failure is not fatal: It is the courage to continue that counts. - Winston Churchill",
            "The world breaks everyone, and afterward, some are strong at the broken places. - Ernest Hemingway",
            ];

        public static List<string> dailyQuotes =
        [
            "The best way to predict the future is to create it. - Abraham Lincoln",
            "The only way to do great work is to love what you do. - Steve Jobs",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt",
            "The only way to do great work is to love what you do. - Steve Jobs",
                "\"It is a shame for a man to grow old without knowing the beauty and strength of which their body is capable.\" -- Socrates"
        ];

        public static List<string> philisophicalQuotes =
            [
                "\"All you have to do...is look within\" -- John Greetings (Interface)",
                "\"What are we waiting for? Just for the stars to align?!\" -- David Goggins",
                "\"There Is A Difference Between Knowing The Path And Walking The Path.\" - - Morpheus",
            "The only way to get rid of a temptation is to yield to it. - Napoleon Bonaparte",
            ];

        public static List<string> hardcoreQuotes = [
            "You should know, not fear, that someday you are going to die. Until you know that and embrace that, you are useless. - Tyler Durden",
            "The only easy day was yesterday. - Navy Seals",
            "The more you sweat in training, the less you bleed in combat. - Navy Seals",
            "Without pain, without sacrifice, we would have nothing! - Tyler Durden",
            "You gain knowledge through suffering. -- David Goggins",
            "No pain, no gain. -- Arnold Schwarzenegger",
            "There's a lot of motherfuckers out there, who wants what you have! -- David Goggins",
            ];
    }
}
