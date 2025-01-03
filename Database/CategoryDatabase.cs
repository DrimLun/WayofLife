using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayofLife;

namespace WayofLifev2.Database
{
    internal class CategoryDatabase
    {
        private SQLiteAsyncConnection Database;

        public CategoryDatabase()
        {
            Database = new SQLiteAsyncConnection(Constant.DatabasePathCateogry);
        }

    }
}
