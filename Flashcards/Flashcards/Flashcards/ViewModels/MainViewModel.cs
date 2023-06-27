using Flashcards.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Flashcards.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string checkStatus;
        private string answer;
        private int itemsCount;
        private Item item;
        private string itemMeaning;

        public MainViewModel()
        {
            Title = "Fiszki";
            CheckCommand = new Command(OnCheck);
            InitializeAsync().Wait();
        }

        public string CheckStatus
        {
            get => checkStatus;
            set 
            { 
                checkStatus = value;
                OnPropertyChanged(nameof(CheckStatus));
            }
        }

        public string Answer
        {
            get => answer;
            set => SetProperty(ref answer, value);
        }

        public string ItemMeaning
        {
            get => itemMeaning;
            set
            {
                itemMeaning = value;
                OnPropertyChanged(nameof(ItemMeaning));
            }
        }

        public Command CheckCommand { get; }

        private void OnCheck()
        {
            if (item.Word.Equals(answer, StringComparison.OrdinalIgnoreCase))
            {
                CheckStatus = "Poprawna odpowiedź!";
            }
            else
            {
                CheckStatus = "Niepoprawna odpowiedź!";
            }
        }

        private async Task LoadRandItemAsync()
        {
            itemsCount = await DataStore.GetItemsCountAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadRandItemAsync();
            await GetRandItemAsync();
        }

        private async Task GetRandItemAsync()
        { 
            var rnd = new Random();

            item = await DataStore.GetRandItemAsync(rnd.Next(itemsCount));

            ItemMeaning = item.Meaning;
        }
    }
}