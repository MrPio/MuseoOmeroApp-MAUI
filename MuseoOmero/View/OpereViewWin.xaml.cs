using Microsoft.Maui.Controls;

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
			ListaMostre.TranslateTo(1800 * (inverse ? 1 : 0), 0, 500, Easing.CubicInOut);
			ListaOpere.TranslateTo(-1800 * (inverse ? 0 : 1), 0, 500, Easing.CubicInOut);
		}

		private void CardViewClose_Clicked(object sender, EventArgs e)
		{
			CardViewTransition(false);
		}
		private async void CardViewTransition(bool show)
		{
			if (show)
			{
				CardView.TranslationX = 0;
				Overlay.TranslationX = 0;
				CardViewScroll.ScrollToAsync(0, 0, false);
			}
			CardView.FadeTo((show ? 1 : 0), 450, Easing.CubicOut);
			CardView.TranslateTo(0, 50 * (show ? 0 : 1), 450, Easing.CubicOut);
			await Overlay.FadeTo((show ? 0.6 : 0), 450, Easing.CubicOut);
			if (!show)
			{
				CardView.TranslationX = 9999;
				Overlay.TranslationX = 9999;
			}
		}

		private void OperaElement_Clicked(object sender, EventArgs e)
		{
			_viewModel.SelectedOpera = ((Button)sender).BindingContext as Opera;
			CardViewTransition(true);
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