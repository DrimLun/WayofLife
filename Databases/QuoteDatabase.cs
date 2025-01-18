using SQLite;

namespace WayofLife.Databases
{
    internal class QuoteDatabase
    {
        //private SqliteConnection
        private SQLiteAsyncConnection Database;
        public QuoteDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePathExpiry);
        }

        public static List<string> Quotes = new()
        {
            "The best way to predict the future is to create it. - Abraham Lincoln",
            "The only way to do great work is to love what you do. - Steve Jobs",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt",
            "Success is not final, failure is not fatal: It is the courage to continue that counts. - Winston Churchill",
            "The only person you should try to be better than is the person you were yesterday. - Anonymous",
            "The only way to do great work is to love what you do. - Steve Jobs",
        };
    }
}
