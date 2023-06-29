using Flashcards.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Flashcards.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string CorrectAnswer = "Poprawna odpowiedź!";
        private const string WrongAnswer = "Niepoprawna odpowiedź!";

        private string checkStatus;
        private string answer;
        private int itemsCount;
        private Item item;
        private string itemMeaning;
        private Color statusColor = Color.Transparent;

        public MainViewModel()
        {
            Title = "Fiszki";
            CheckCommand = new Command(OnCheck);
            DrawCommand = new Command(OnDraw);
            InitializeAsync().Wait();
        }

        public Color StatusColor
        { 
            get => statusColor;
            set
            { 
                statusColor = value;
                OnPropertyChanged(nameof(StatusColor));
            }
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

        public Command DrawCommand { get; }

        private void OnCheck()
        {
            if (item.Word.Equals(answer, StringComparison.OrdinalIgnoreCase))
            {
                CheckStatus = $"{CorrectAnswer}{Environment.NewLine}{item.Word}";
                StatusColor = Color.Green;
            }
            else
            {
                CheckStatus = $"{WrongAnswer}{Environment.NewLine}{item.Word}";
                StatusColor = Color.Red;
            }
        }

        private async void OnDraw() 
        {
            await GetRandItemAsync();
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
            CheckStatus = String.Empty;
            StatusColor = Color.Transparent;
            Answer = String.Empty;
        }
    }
}