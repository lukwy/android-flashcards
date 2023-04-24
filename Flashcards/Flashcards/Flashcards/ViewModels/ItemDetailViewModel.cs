using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Flashcards.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string infinitive;
        private string preteritum;
        private string perfektum;
        private string meaning;

        public int Id { get; set; }

        public ItemDetailViewModel()
        {
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged += (_, __) => DeleteCommand.ChangeCanExecute();
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public string Infinitive { get => infinitive; set => SetProperty(ref infinitive, value); }
        public string Preteritum { get => preteritum; set => SetProperty(ref preteritum, value); }
        public string Perfektum { get => perfektum; set => SetProperty(ref perfektum, value); }
        public string Meaning { get => meaning; set => SetProperty(ref meaning, value); }
        
        public Command DeleteCommand { get; }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Infinitive = item.Infinitive;
                Preteritum = item.Preteritum;
                Perfektum = item.Perfektum;
                Meaning = item.Meaning;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnDelete()
        {
            await DataStore.DeleteItemAsync(Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
