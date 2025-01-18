using SQLite;

namespace WayofLife.Models
{
    public partial class Quote
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string? QuoteText { get; set; }
        public string? Category { get; set; }

        public Quote(string inQuoteText, string inCategory)
        {
            if (inQuoteText == null || inCategory == null)
            {
                throw new ArgumentNullException(nameof(inQuoteText));
            }
            else
            {
                this.QuoteText  = inQuoteText;
                this.Category   = inCategory;
            }
        }

        public Quote() { }
    }
}
