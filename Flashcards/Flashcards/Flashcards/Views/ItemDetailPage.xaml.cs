using Flashcards.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Flashcards.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}