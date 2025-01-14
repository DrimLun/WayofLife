﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayofLife
{
    public static class Constant
    {
        public const string DatabaseFilename = "JournalSQLite.db3";

        public const string DatabaseFilenameCategory = "CategorySQLite.db3";

        public const string DatabaseFilenameExpiry = "ExpirySQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
                // open the database in read/write mode
                SQLite.SQLiteOpenFlags.ReadWrite |
                // create the database if it doesn't exist
                SQLite.SQLiteOpenFlags.Create |
                // enable multi-threaded database access
                SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public static string DatabasePathCateogry =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilenameCategory);
        public static string DatabasePathExpiry =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilenameCategory);
    }
}
