//Model: Expiry
using SQLite;

namespace WayofLife.Models
{
    public class Expiry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public DateTime ExpiryDateTime { get; set; }

        public Expiry(string name, DateTime expiryDateTime)
        {
            Name = name;
            ExpiryDateTime = expiryDateTime;
        }
        public Expiry()
        {
            ExpiryDateTime = new DateTime();
        }
    }
}
