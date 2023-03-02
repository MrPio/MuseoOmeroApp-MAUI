using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using MuseoOmero.Resources.Material;
using System.Runtime.ExceptionServices;

namespace MuseoOmero.ViewWin
{
	public partial class OpereViewWin : ContentPage
	{
		private readonly OpereViewModelWin _viewModel;
		public OpereViewWin(OpereViewModelWin viewModel)
		{
			_viewModel = viewModel;
			BindingContext = _viewModel;
			InitializeComponent();

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (_viewModel.ShowOpera)
			{
				_viewModel.ShowOpera = false;
				CardViewTransition(true, CardView);
			}
		}

		private void FiltroPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			_viewModel.OpereSortAcending = true;
			_viewModel.OrdinaOpere();
		}
		private void FiltroMostrePicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			_viewModel.MostreSortAcending = true;
			_viewModel.OrdinaMostre();
		}

		private void HighlightView_Clicked(object sender, EventArgs e)
		{
			_viewModel.OpereSortAcending = !_viewModel.OpereSortAcending;
			_viewModel.OrdinaOpere();
		}
		private void MostreHighlightView_Clicked(object sender, EventArgs e)
		{
			_viewModel.MostreSortAcending = !_viewModel.MostreSortAcending;
			_viewModel.OrdinaMostre();
		}

		private void OpereOn_Clicked(object sender, EventArgs e)
		{
			if (this.AnimationIsRunning("translation"))
				return;
			_viewModel.OpereOn = true;
			_viewModel.MostreOn = false;
			Translation(true);
		}

		private void MostreOn_Clicked(object sender, EventArgs e)
		{
			if (this.AnimationIsRunning("translation"))
				return;
			_viewModel.OpereOn = false;
			_viewModel.MostreOn = true;
			Translation(false);
		}

		private void Translation(bool inverse = false)
		{
			ListaMostre.TranslateTo(1600 * (inverse ? 1 : 0), 0, 500, Easing.CubicInOut);
			ListaOpere.TranslateTo(-1600 * (inverse ? 0 : 1), 0, 500, Easing.CubicInOut);
		}

		private void CardViewClose_Clicked(object sender, EventArgs e)
		{
			if (sender == ExitButton)
				CardViewTransition(false, CardView);
			else
				CardViewTransition(false, CardViewAggiungi);
		}
		public async void CardViewTransition(bool show, Grid cardView)
		{
			if (show)
			{
				cardView.TranslationX = 0;
				Overlay.TranslationX = 0;

				CardViewScroll.ScrollToAsync(0, 0, false);
				MostreCardViewScroll.ScrollToAsync(0, 0, false);

				CardViewAggiungiScroll.ScrollToAsync(0, 0, false);
				MostreCardViewAggiungiScroll.ScrollToAsync(0, 0, false);
			}
			cardView.FadeTo((show ? 1 : 0), 450, Easing.CubicOut);
			cardView.TranslateTo(0, 50 * (show ? 0 : 1), 450, Easing.CubicOut);
			await Overlay.FadeTo((show ? 0.6 : 0), 450, Easing.CubicOut);

			if (!show)
			{
				cardView.TranslationX = 9999;
				Overlay.TranslationX = 9999;
			}
		}

		private void OperaElement_Clicked(object sender, EventArgs e)
		{
			_viewModel.SelectedOpera = ((Button)sender).Parent.Parent.BindingContext as Opera;
			CardViewTransition(true, CardView);
		}
		private void MostraElement_Clicked(object sender, EventArgs e)
		{
			_viewModel.SelectedMostra = ((Button)sender).Parent.Parent.BindingContext as Mostra;
			CardViewTransition(true, CardView);
		}

		private async void AggiungiOpera_Clicked(object sender, EventArgs e)
		{
			//await DisplayAlert("Funzionalità non ancora implementata", "Per favore, per poter usufruire di questa funzionalità, attendi i prossimi aggiornamenti.","Ok");
			_viewModel.NuovaOpera = new(
				sala: Sala.Values[0],
				nome: "",
				autore: "",
				dataAggiunta: DateTime.Now,
				dimensioni: new[] { 0f, 0f, 0f },
				tecnica: new(),
				materiali: new() { Materiale.Values[1] },
				foto: ImagesOnline.NoImage,
				descrizione: "",
				visualizzazioni: 0
				);
			CardViewTransition(true, CardViewAggiungi);

		}
	}
}

namespace MuseoOmero.View.TemplatesWin
{
	public class HeaderElement : ContentView
	{
		public static readonly BindableProperty TitoloProperty = BindableProperty.Create(nameof(Titolo), typeof(string), typeof(HeaderElement), string.Empty);

		public string Titolo
		{
			get => (string)GetValue(TitoloProperty);
			set => SetValue(TitoloProperty, value);
		}
	}
}