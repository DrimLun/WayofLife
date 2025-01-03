using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayofLifev2.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Color { get; set; }

        public string? Icon { get; set; }

        public Category(string inCatName)
        {
            Name = inCatName;
        }

        public Category()
        {
            Name = "Default Name";
        }
    }
}
