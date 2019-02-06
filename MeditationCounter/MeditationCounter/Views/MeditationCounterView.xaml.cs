using MeditationCounter.ViewModels;
using MeditationCounter.ViewModels.Base;
using Xamarin.Forms;

namespace MeditationCounter.Views
{
    public partial class MeditationCounterView : ContentPage
	{
		public MeditationCounterView ()
		{
			InitializeComponent ();

            BindingContext = new MeditationCounterViewModel();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }
    }
}