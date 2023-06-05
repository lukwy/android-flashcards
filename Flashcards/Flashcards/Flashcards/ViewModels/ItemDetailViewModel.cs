using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Flashcards.ViewModels
{
    [QueryProperty(nameof(ItemGuid), nameof(ItemGuid))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemGuid;
        private string word;
        private string meaning;

        public string Guid { get; set; }

        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged += (_, __) => DeleteCommand.ChangeCanExecute();
        }

        public string ItemGuid
        {
            get
            {
                return itemGuid;
            }
            set
            {
                itemGuid = value;
                LoadItemId(value);
            }
        }

        public string Word { get => word; set => SetProperty(ref word, value); }
        public string Meaning { get => meaning; set => SetProperty(ref meaning, value); }
        
        public Command DeleteCommand { get; }

        public async void LoadItemId(string itemGuid)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemGuid);
                Guid = item.Guid;
                Word = item.Word;
                Meaning = item.Meaning;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnDelete()
        {
            await DataStore.DeleteItemAsync(Guid);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
