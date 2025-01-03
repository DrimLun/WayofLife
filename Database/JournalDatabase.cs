using Microsoft.VisualBasic;
using SQLite;
using WayofLife;
using WayofLifev2.Models;

namespace WayofLifev2.Database_File
{
    class JournalDatabase
    {

        private SQLiteAsyncConnection Database;

        public JournalDatabase()
        {
            Database = new SQLiteAsyncConnection(Constant.DatabasePath);
        }

        public async Task InitAsync()
        {
            if (Database != null)
            {
                // Create the table only if it doesn't exist
                var journalTableInfo = await Database.GetTableInfoAsync("Journal");
                if (journalTableInfo.Count == 0)
                {
                    await Database.CreateTableAsync<Journal>();
                }

                var categoryTableInfo = await Database.GetTableInfoAsync("Category");
                if (categoryTableInfo.Count == 0)
                {
                    await Database.CreateTableAsync<Category>();
                    await Database.InsertAsync(new Category("Gratitude"));
                    await Database.InsertAsync(new Category("Reflection"));
                    await Database.InsertAsync(new Category("Knowledge"));
                    await Database.InsertAsync(new Category("Health"));
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Database is not initialized."); // Debug
            }
        }
            
        public async Task<List<Journal>> GetJournalsAsync()
        {
            await InitAsync();
            return await Database.Table<Journal>().ToListAsync();
        }

        public async Task<Journal> GetJournalAsync(int id)
        {
            await InitAsync();
            return await Database.Table<Journal>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveJournalAsync(Journal item)
        {
            await InitAsync();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteJournalAsync(Journal item)
        {
            await InitAsync();
            return await Database.DeleteAsync(item);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await InitAsync();
            return await Database.Table<Category>().ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            await InitAsync();
            return await Database.Table<Category>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveCategoryAsync(Journal item)
        {
            await InitAsync();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteCategoryAsync(Journal item)
        {
            await InitAsync();
            return await Database.DeleteAsync(item);
        }
    }
}
