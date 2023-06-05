using Flashcards.Models;
using System;
using Xamarin.Forms;

namespace Flashcards.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string word;
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
            return !string.IsNullOrWhiteSpace(word)
                && !string.IsNullOrWhiteSpace(meaning);
        }

        public string Word
        {
            get => word;
            set => SetProperty(ref word, value);
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
            Item newItem = new Item()
            {
                Guid = Guid.NewGuid().ToString(),
                Word = Word,
                Meaning = Meaning
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
