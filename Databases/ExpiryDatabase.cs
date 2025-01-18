using SQLite;
using WayofLife.Models;

namespace WayofLife.Databases
{
    internal class ExpiryDatabase
    {
        private SQLiteAsyncConnection Database;

        public ExpiryDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePathExpiry);
        }

        public async Task InitAsync()
        {
            var expiryTableInfo = await Database.GetTableInfoAsync("Expiry");
            var expiryCategoryTableInfo = await Database.GetTableInfoAsync("Category");

            if (expiryTableInfo.Count == 0)
            {
                await Database.CreateTableAsync<Expiry>();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Table Expiry already exists");
            }

            if (expiryCategoryTableInfo.Count == 0)
            {
                await Database.CreateTableAsync<Category>();
                await Database.InsertAsync(new Category("Pass/Coupon", "Blue"));
                await Database.InsertAsync(new Category("Vegetable", "Green"));
                await Database.InsertAsync(new Category("Fruit", "Crimson Red"));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Table Category already exists");
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

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await InitAsync();
            return await Database.Table<Category>().ToListAsync();
        }

        public async Task<int> SaveCategoryAsync(Category item)
        {
            await InitAsync();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteCategoryAsync(Category item)
        {
            await InitAsync();
            return await Database.DeleteAsync(item);
        }
    }
}
