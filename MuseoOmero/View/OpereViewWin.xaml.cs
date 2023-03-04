using MuseoOmero.Managers;

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
			CardViewTransition(false, CardView);

		}
		private void CardViewAggiungiClose_Clicked(object sender, EventArgs e)
		{
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

			//RESET dei componenti
			MaterialiCollectionView.UpdateSelectedItems(new List<object>());
			TecnicheCollectionView.UpdateSelectedItems(new List<object>());
			PhotoImage.Source = ImagesOnline.NoImage;
			NuovaOperaLarghezza.Text = string.Empty;
			NuovaOperaAltezza.Text = string.Empty;
			NuovaOperaProfondita.Text = string.Empty;

			CardViewTransition(true, CardViewAggiungi);

		}

		private void MaterialiCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var current = e.CurrentSelection;
			_viewModel.NuovaOpera.Materiali = current.Cast<string>().ToList();
		}
		private void TecnicheCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var current = e.CurrentSelection;
			_viewModel.NuovaOpera.Tecnica = current.Cast<string>().ToList();
		}

		private void NuovaOperaPhoto_Pressed(object sender, EventArgs e)
		{
			PhotoFrame.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => PhotoFrame.BackgroundColor = c, 350, Easing.CubicOut);
			PhotoIcon.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => PhotoIcon.TextColor = c, 350, Easing.CubicOut);
			PhotoIcon.FadeTo(1, 350, Easing.CubicOut);
			PhotoImage.FadeTo(0, 350, Easing.CubicOut);
		}
		private void NuovaOperaPhoto_Released(object sender, EventArgs e)
		{
			PhotoFrame.CancelAnimation();
			PhotoIcon.CancelAnimation();
			PhotoFrame.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => PhotoFrame.BackgroundColor = c, 350, Easing.CubicOut);
			PhotoIcon.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => PhotoIcon.TextColor = c, 350, Easing.CubicOut);
			PhotoIcon.FadeTo(0, 350, Easing.CubicOut);
			PhotoImage.FadeTo(1, 350, Easing.CubicOut);
		}
		private async void NuovaOperaPhoto_Clicked(object sender, EventArgs e)
		{
			_viewModel.IsBusy = true;
			var fileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Scegli una nuova foto di profilo" });
			if (fileResult is { })
			{
				var sm = StorageManager.Instance;
				var um = UtiliesManager.Instance;
				var randUid = um.RandomString(18);
				var res = $"opereImages/{randUid}/foto/";
				var stream = um.ImageToStream(fileResult.FullPath, false);

				await sm.Upload(res, stream);
				var url = await sm.GetLink(res);
				_viewModel.NuovaOpera.Foto = url;
				PhotoImage.Source = url;
			}
			_viewModel.IsBusy = false;
		}

		private async void SalvaNuovaOpera_Clicked(object sender, EventArgs e)
		{
			_viewModel.IsBusy = true;
			var opera = _viewModel.NuovaOpera;

			try
			{
				opera.Dimensioni[0] = float.Parse(NuovaOperaLarghezza.Text);
				opera.Dimensioni[1] = float.Parse(NuovaOperaAltezza.Text);
				opera.Dimensioni[2] = float.Parse(NuovaOperaProfondita.Text);
			}
			catch (Exception ex)
			{
				DisplayAlert("Dimensioni errate", "Per favore, inserisci dei valori validi per le 3 dimensioni dell'opera.", "Ok");
				_viewModel.IsBusy = false;
				return;
			}
			if (opera.Nome.Length < 1)
			{
				DisplayAlert("Titolo necessario", "Per favore, inserisci un titolo per poter salvare la nuova opera.", "Ok");
			}
			else
			{
				if (_viewModel.OpereOrdinate.Where(o => o.Nome.ToLower().Trim() == opera.Nome.ToLower().Trim()).Any())
				{
					DisplayAlert("Titolo già in uso", "Per favore, un titolo diverso.", "Ok");
					_viewModel.IsBusy = false;
					return;
				}
				await DatabaseManager.Instance.Post("opere", opera);
				await _viewModel.HomeViewModel.Initialize();
				_viewModel.OrdinaOpere();
				DisplayAlert("Successo", "Opera memorizzata con successo!", "Ok");
				CardViewTransition(false, CardViewAggiungi);
			}
			_viewModel.IsBusy = false;
		}
		private async void EliminaOpera_Clicked(object sender, EventArgs e)
		{
			var opera = _viewModel.SelectedOpera;
			var answer = await DisplayAlert("Eliminazione definitiva", $"Attenzione, stai per eliminare definitivamente l'opera [{opera.Nome}]. Sei sicuro di voler procedere?", "Si", "Annulla");
			if (answer)
			{
				_viewModel.IsBusy = true;
				await DatabaseManager.Instance.Delete($"opere/{opera.Id}");
				await _viewModel.HomeViewModel.Initialize();
				_viewModel.OrdinaOpere();
				DisplayAlert("Successo", "Opera eliminata con successo!", "Ok");
				CardViewTransition(false, CardView);
				_viewModel.IsBusy = false;
			}
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