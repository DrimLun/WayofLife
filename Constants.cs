﻿namespace WayofLife
{
    internal class Constants
    {
        public const string DatabaseFilename = "JournalSQLite.db3";

        public const string DatabaseFilenameCategory = "CategorySQLite.db3";

        public const string DatabaseFilenameExpiry = "ExpirySQLite.db3";

        public const string DatabaseFilenameQuote = "QuoteSQLite.db3";

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
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilenameExpiry);

        public static string DatabasePathQuote => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilenameQuote);
    }
}
