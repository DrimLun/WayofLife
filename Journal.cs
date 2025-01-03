using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayofLife
{
    public class Journal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Title { get; set; }
        private string? writtenContent;
        private string? imageContentPath;
        private DateTime dateTime;
        public bool Done { get; set; }

        public Journal(string? inTitle, string? inWrittenContent)
        {
            this.Title = inTitle;
            this.WrittenContent = inWrittenContent;
            this.DateTime = DateTime.Now;
        }

        public Journal(string? inTitle, string? inWrittenContent, string? inImageContentPath)
        {
            this.Title = inTitle;
            this.WrittenContent = inWrittenContent;
            this.ImageContentPath = inImageContentPath;
            this.DateTime = DateTime.Now;

        }

        public Journal() { }

        public string? WrittenContent { get => writtenContent; set => writtenContent = value; }
        public string? ImageContentPath { get => imageContentPath; set => imageContentPath = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
