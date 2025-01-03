using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using Microsoft.VisualStudio.PlatformUI;

namespace WayofLife.ViewModel
{
    //https://www.syncfusion.com/blogs/post/sqlite-data-to-net-maui-listview
    public partial class JournalViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<Journal> contactsInfo;
        private Journal selectedContact;

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        //#region Properties
        //public Journal SelectedItem
        //{
        //    get
        //    {
        //        return selectedContact;
        //    }
        //    set
        //    {
        //        selectedContact = value;
        //        OnPropertyChanged("SelectedItem");
        //    }
        //}
        //public ObservableCollection<Journal> ContactsInfo
        //{
        //    get
        //    {
        //        return contactsInfo;
        //    }
        //    set
        //    {
        //        contactsInfo = value;
        //        OnPropertyChanged("ContactsInfo");
        //    }
        //}

        //#endregion

        //#region Constructor
        //public JournalViewModel()
        //{
        //    GenerateContacts();
        //}
        //#endregion

        //#region Methods

        //private void GenerateContacts()
        //{
        //    ContactsInfo = new ObservableCollection<Journal>();
        //    ContactsInfo = new ContactsInfoRepository().GetContactDetails(20);
        //    PopulateDB();
        //}

        //private async void PopulateDB()
        //{
        //    foreach (Journal contact in ContactsInfo)
        //    {
        //        var item = await WayofLife.JournalDatabase.GetItemsAsync();
        //        if (item == null)
        //            await WayofLife.JournalDatabase.SaveItemAsync(contact);
        //    }
        //}
        //private async void OnAddNewItem()
        //{
        //    await WayofLife.JournalDatabase.SaveItemAsync(SelectedItem);
        //    ContactsInfo.Add(SelectedItem);
        //    await App.Current.MainPage.Navigation.PopAsync();
        //}

        //#endregion

        //#region Interface Member

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged(string name)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

        //#endregion
    }
}