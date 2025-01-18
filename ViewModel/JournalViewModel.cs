using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using WayofLife.Databases;
using WayofLife.Models;

namespace WayofLife.ViewModel
{
    public partial class JournalViewModel
    {
        //https://www.syncfusion.com/blogs/post/sqlite-data-to-net-maui-listview
            public AsyncCommand AddImage { get; set; }
            public AsyncCommand AddJournal { get; set; }

            private JournalDatabase journalDatabase = new();
            public ObservableCollection<Category> cCollection = [];
            public List<string> cNameList = [];
    }
}
