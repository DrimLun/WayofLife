using Microsoft.VisualBasic;
using SQLite;

namespace WayofLife
{
    class JournalDatabase
    {
        
        private SQLiteAsyncConnection Database;

        public JournalDatabase()
        {
            Database = new SQLiteAsyncConnection(Constant.DatabasePath);
        }

        public async Task Init()
        {
            if (Database == null)
            {
                Database = new SQLiteAsyncConnection(Constant.DatabasePath);
                //await Database.CreateTableAsync<Journal>();

                // Create the table only if it doesn't exist
                var tableInfo = await Database.GetTableInfoAsync("Journal");
                if (tableInfo.Count == 0)
                {
                    await Database.CreateTableAsync<Journal>();
                }
            }
            else
            {
                Console.WriteLine("Database already initialized."); // Debug
            }
        }

        public async Task<List<Journal>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Journal>().ToListAsync();
        }

        public async Task<List<Journal>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<Journal>().Where(t => t.Done).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<Journal>("SELECT * FROM [Journal] WHERE [Done] = 0");
        }

        public async Task<Journal> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Journal>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Journal item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Journal item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
