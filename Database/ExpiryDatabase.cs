using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayofLife;
using WayofLifev2.Models;

namespace WayofLifev2.Database
{
    internal class ExpiryDatabase
    {
        private SQLiteAsyncConnection Database;

        public ExpiryDatabase()
        {
            Database = new SQLiteAsyncConnection(Constant.DatabasePathExpiry);
        }

        public async Task InitAsync()
        {
            var expiryTableInfo = await Database.GetTableInfoAsync("Expiry");

            if (expiryTableInfo.Count == 0)
            {
                await Database.CreateTableAsync<Expiry>();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Table Expiry already exists");
            }
        }

        public async Task<List<Expiry>> GetExpiriesAsync()
        {
            await InitAsync();
            return await Database.Table<Expiry>().ToListAsync();
        }

        public async Task<Expiry> GetExpiryAsync(int id)
        {
            await InitAsync();
            return await Database.Table<Expiry>().Where(i => i.Id == id).FirstOrDefaultAsync(); //return 1st element or null
        }

        public async Task<int> SaveExpiryAsync(Expiry item)
        {
            await InitAsync();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteExpiryAsync(Expiry item)
        {
            await InitAsync();
            return await Database.DeleteAsync(item);
        }
    }
}
