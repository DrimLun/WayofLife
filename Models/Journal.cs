using SQLite;

namespace WayofLife.Models
{
    public class Journal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }

        public string? WrittenContent { get; set; }
        public string? ImageContentPath { get; set; }
        private DateTime dateTime;
        //public bool Done { get; set; }

        public Journal(string inTitle, string inCategory, string inWrittenContent, string inImageContentPath)
        {
            Title = inTitle;
            Category = inCategory;
            WrittenContent = inWrittenContent;
            ImageContentPath = inImageContentPath;
            DateTime = DateTime.Now;
        }
        public Journal() 
        {
            Title = "Default Title";
            WrittenContent = "";
            ImageContentPath = "";
            DateTime = DateTime.Now;
        }

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
