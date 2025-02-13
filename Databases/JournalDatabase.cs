﻿using SQLite;
using WayofLife.Models;

namespace WayofLife.Databases
{
    internal class JournalDatabase
    {
        private SQLiteAsyncConnection Database;

        public JournalDatabase()
        {
            Database = new SQLiteAsyncConnection(WayofLife.Constants.DatabasePath);
        }

        public async Task InitAsync()
        {
            //if (Database != null)
            //{
                var journalTableInfo = await Database.GetTableInfoAsync("Journal");
                if (journalTableInfo.Count == 0) // Create the table only if it doesn't exist
                {
                    await Database.CreateTableAsync<Journal>();
                }
                else
            {
                System.Diagnostics.Debug.WriteLine("Table Journal already exists");
            }

                var categoryTableInfo = await Database.GetTableInfoAsync("Category");
                if (categoryTableInfo.Count == 0)
                {
                    await Database.CreateTableAsync<Category>();
                    await Database.InsertAsync(new Category("Gratitude", "Green"));
                    await Database.InsertAsync(new Category("Reflection", "Cadet Blue"));
                    await Database.InsertAsync(new Category("Knowledge", "Orange"));
                    await Database.InsertAsync(new Category("Health", "Cyan"));
                    await Database.InsertAsync(new Category("Other", "White"));
                }
                else
            {
                System.Diagnostics.Debug.WriteLine("Table Category already exists");
            }
            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine("Database is not initialized."); // Debug
            //}
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
            if (item.Id != 0) //Journal's id is 0 when created, later assigned by SQLite
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
