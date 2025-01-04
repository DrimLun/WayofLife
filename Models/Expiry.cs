using SQLite;

namespace WayofLifev2.Models
{
    public class Expiry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        //public DateOnly ExpiryDate { get; set; }
        public DateTime ExpiryDateTime { get; set; }

        public Expiry(string name, DateOnly expiryDate)
        {
            Name = name;
            //ExpiryDate = expiryDate;
        }
        public Expiry(string name, DateTime expiryDateTime)
        {
            Name = name;
            ExpiryDateTime = expiryDateTime;
        }
        public Expiry()
        {
            //ExpiryDate = new DateOnly();
            ExpiryDateTime = new DateTime();
        }
    }
}
