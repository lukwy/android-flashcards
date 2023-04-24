using Flashcards.Models;
using Xamarin.Forms;

namespace Flashcards.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string infinitive;
        private string preteritum;
        private string perfektum;
        private string meaning;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(infinitive)
                && !string.IsNullOrWhiteSpace(preteritum)
                && !string.IsNullOrWhiteSpace(perfektum)
                && !string.IsNullOrWhiteSpace(meaning);
        }

        public string Infinitive
        {
            get => infinitive;
            set => SetProperty(ref infinitive, value);
        }

        public string Preteritum
        {
            get => preteritum;
            set => SetProperty(ref preteritum, value);
        }

        public string Perfektum
        {
            get => perfektum;
            set => SetProperty(ref perfektum, value);
        }

        public string Meaning
        {
            get => meaning;
            set => SetProperty(ref meaning, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var lastId = await DataStore.GetLastItemIdAsync();

            Item newItem = new Item()
            {
                Id = ++lastId,
                Infinitive = Infinitive,
                Preteritum = Preteritum,
                Perfektum = Perfektum,
                Meaning = Meaning
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
