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
				CardViewTransition(true, CardView, true);
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

		public async void CardViewTransition(bool show, Grid cardView, bool alsoArrows = false)
		{
			if (show)
			{
				cardView.TranslationX = 0;
				Overlay.TranslationX = 0;
				if (alsoArrows)
					LeftRightArrows.TranslationX = 0;

				CardViewScroll.ScrollToAsync(0, 0, false);
				MostreCardViewScroll.ScrollToAsync(0, 0, false);

				CardViewAggiungiScroll.ScrollToAsync(0, 0, false);
				MostreCardViewAggiungiScroll.ScrollToAsync(0, 0, false);
			}
			cardView.FadeTo((show ? 1 : 0), 450, Easing.CubicOut);
			cardView.TranslateTo(0, 50 * (show ? 0 : 1), 450, Easing.CubicOut);
			if (alsoArrows)
				LeftRightArrows.FadeTo((show ? 1 : 0), 650, Easing.CubicOut);
			await Overlay.FadeTo((show ? 0.6 : 0), 450, Easing.CubicOut);

			if (!show)
			{
				cardView.TranslationX = 9999;
				Overlay.TranslationX = 9999;
				LeftRightArrows.TranslationX = 9999;
			}
		}
		private void CardViewClose_Clicked(object sender, EventArgs e)
		{
			CardViewTransition(false, CardView);

		}
		private void CardViewAggiungiClose_Clicked(object sender, EventArgs e)
		{
			CardViewTransition(false, CardViewAggiungi);

		}

		private void OperaElement_Clicked(object sender, EventArgs e)
		{
			_viewModel.SelectedOpera = ((Button)sender).Parent.Parent.BindingContext as Opera;

			var index = _viewModel.OpereOrdinate.IndexOf(_viewModel.SelectedOpera);
			if (index == 0)
				LeftArrow.TextColor = DeviceManager.Instance.Colors[5];
			else
				LeftArrow.TextColor = DeviceManager.Instance.Colors[4];
			if (index == _viewModel.OpereOrdinate.Count - 1)
				RightArrow.TextColor = DeviceManager.Instance.Colors[5];
			else
				RightArrow.TextColor = DeviceManager.Instance.Colors[4];

			CardViewTransition(true, CardView, true);
		}
		private void MostraElement_Clicked(object sender, EventArgs e)
		{
			_viewModel.SelectedMostra = ((Button)sender).Parent.Parent.BindingContext as Mostra;
			MostraPhotoImage2.Source = _viewModel.SelectedMostra.Foto;

			var index = _viewModel.MostreOrdinate.IndexOf(_viewModel.SelectedMostra);
			if (index == 0)
				LeftArrow.TextColor = DeviceManager.Instance.Colors[5];
			else
				LeftArrow.TextColor = DeviceManager.Instance.Colors[4];
			if (index == _viewModel.MostreOrdinate.Count - 1)
				RightArrow.TextColor = DeviceManager.Instance.Colors[5];
			else
				RightArrow.TextColor = DeviceManager.Instance.Colors[4];

			CardViewTransition(true, CardView, true);
		}

		private async void AggiungiOperaMostra_Clicked(object sender, EventArgs e)
		{
			_viewModel.IsBusy = true;
			if (((Button)sender).Text.Contains("Opera"))
			{
				_viewModel.AggiungiMostreOn = false;
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
			}
			else
			{
				_viewModel.AggiungiMostreOn = true;
				_viewModel.NuovaMostra = new(
					dataAggiunta: DateTime.Now,
					dataFine: DateTime.Today.AddMonths(1),
					dataInizio: DateTime.Today,
					titolo: "",
					foto: ImagesOnline.NoImage,
					descrizione: "",
					opere: new()
				);

				//RESET dei componenti
				MostraPhotoImage.Source = ImagesOnline.NoImage;
				OpereInMostraCollectionView.UpdateSelectedItems(new List<object>());
			}
			CardViewTransition(true, CardViewAggiungi);
			_viewModel.IsBusy = false;
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
		private void OpereInMostraCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var current = e.CurrentSelection;
			_viewModel.NuovaMostra.Opere = current.Cast<Opera>().Select(o => o.Id).ToList();
		}

		private void NuovaOperaMostraPhoto_PointerEntered(object sender, EventArgs e)
		{
			var photoFrame = Metadata.GetTag(sender as BindableObject) switch
			{
				"MostraPhoto" => MostraPhotoFrame,
				"MostraPhoto2" => MostraPhotoFrame2,
				"Photo" => PhotoFrame
			};
			var photoIcon = Metadata.GetTag(sender as BindableObject) switch
			{
				"MostraPhoto" => MostraPhotoIcon,
				"MostraPhoto2" => MostraPhotoIcon2,
				"Photo" => PhotoIcon
			};
			var photoImage = Metadata.GetTag(sender as BindableObject) switch
			{
				"MostraPhoto" => MostraPhotoImage,
				"MostraPhoto2" => MostraPhotoImage2,
				"Photo" => PhotoImage
			};

			photoFrame.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[4], c => photoFrame.BackgroundColor = c, 350, Easing.CubicOut);
			photoIcon.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => PhotoIcon.TextColor = c, 350, Easing.CubicOut);
			photoIcon.FadeTo(1, 350, Easing.CubicOut);
			photoImage.FadeTo(0, 350, Easing.CubicOut);
		}
		private void NuovaOperaMostraPhoto_PointerExited(object sender, EventArgs e)
		{
			var photoFrame = Metadata.GetTag(sender as BindableObject) switch
			{
				"MostraPhoto" => MostraPhotoFrame,
				"MostraPhoto2" => MostraPhotoFrame2,
				"Photo" => PhotoFrame
			};
			var photoIcon = Metadata.GetTag(sender as BindableObject) switch
			{
				"MostraPhoto" => MostraPhotoIcon,
				"MostraPhoto2" => MostraPhotoIcon2,
				"Photo" => PhotoIcon
			};
			var photoImage = Metadata.GetTag(sender as BindableObject) switch
			{
				"MostraPhoto" => MostraPhotoImage,
				"MostraPhoto2" => MostraPhotoImage2,
				"Photo" => PhotoImage
			};

			photoFrame.CancelAnimation();
			photoIcon.CancelAnimation();
			photoFrame.ColorTo(DeviceManager.Instance.Colors[4], DeviceManager.Instance.Colors[3], c => photoFrame.BackgroundColor = c, 350, Easing.CubicOut);
			photoIcon.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => PhotoIcon.TextColor = c, 350, Easing.CubicOut);
			photoIcon.FadeTo(0, 350, Easing.CubicOut);
			photoImage.FadeTo(1, 350, Easing.CubicOut);
		}
		private async void NuovaOperaMostraPhoto_Clicked(object sender, EventArgs e)
		{
			var senderTag = Metadata.GetTag(sender as BindableObject);
			var photoImage = senderTag switch
			{
				"MostraPhoto" => MostraPhotoImage,
				"MostraPhoto2" => MostraPhotoImage2,
				"Photo" => PhotoImage
			};

			_viewModel.IsBusy = true;
			var fileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Scegli una nuova foto di profilo" });
			if (fileResult is { })
			{
				var sm = StorageManager.Instance;
				var db = DatabaseManager.Instance;
				var um = UtiliesManager.Instance;
				var randUid = um.RandomString(18);
				var res = $"opereImages/{randUid}/foto/";
				var stream = um.ImageToStream(fileResult.FullPath, false);

				await sm.Upload(res, stream);
				var url = await sm.GetLink(res);
				if (_viewModel.MostreOn)
					if (senderTag == "MostraPhoto")
						_viewModel.NuovaMostra.Foto = url;
					else
					{
						_viewModel.SelectedMostra.Foto = url;
						await db.Put($"mostre/{_viewModel.SelectedMostra.Id}/foto", url);
					}
				else
					_viewModel.NuovaOpera.Foto = url;
				photoImage.Source = url;
			}
			_viewModel.IsBusy = false;
		}

		private async void SalvaNuovaOperaMostra_Clicked(object sender, EventArgs e)
		{
			_viewModel.IsBusy = true;
			if (_viewModel.AggiungiMostreOn)
			{
				var mostra = _viewModel.NuovaMostra;
				mostra.Titolo = mostra.Titolo.TrimStart().TrimEnd();
				if (mostra.Titolo.Length < 1)
				{
					DisplayAlert("Titolo necessario", "Per favore, inserisci un titolo per poter salvare la nuova mostra.", "Ok");
				}
				if (mostra.DataFine <= mostra.DataInizio)
				{
					DisplayAlert("Data fine errata", "Per favore, assicurati che la data di fine della mostra sia successiva alla data di inizio.", "Ok");
				}
				else
				{
					if (_viewModel.MostreOrdinate.Where(m => m.Titolo.ToLower().Trim() == mostra.Titolo.ToLower().Trim()).Any())
					{
						DisplayAlert("Titolo già in uso", "Per favore, un titolo diverso.", "Ok");
						_viewModel.IsBusy = false;
						return;
					}
					await DatabaseManager.Instance.Post("mostre", mostra);
					await _viewModel.HomeViewModel.Initialize();
					_viewModel.OrdinaMostre();
					DisplayAlert("Successo", "Mostra memorizzata con successo!", "Ok");
					CardViewTransition(false, CardViewAggiungi);
				}
			}
			else
			{
				var opera = _viewModel.NuovaOpera;
				opera.Nome = opera.Nome.TrimStart().TrimEnd();
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
			}
			_viewModel.IsBusy = false;
		}
		private async void EliminaOperaMostra_Clicked(object sender, EventArgs e)
		{
			if (_viewModel.MostreOn)
			{
				var mostra = _viewModel.SelectedMostra;
				var answer = await DisplayAlert("Eliminazione definitiva", $"Attenzione, stai per eliminare definitivamente la mostra [{mostra.Titolo}]. Sei sicuro di voler procedere?", "Si", "Annulla");
				if (answer)
				{
					_viewModel.IsBusy = true;
					await DatabaseManager.Instance.Delete($"mostre/{mostra.Id}");
					await _viewModel.HomeViewModel.Initialize();
					_viewModel.OrdinaMostre();
					DisplayAlert("Successo", "Mostra eliminata con successo!", "Ok");
					CardViewTransition(false, CardView);
					_viewModel.IsBusy = false;
				}

			}
			else
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

		private void LeftRightArrow_Tapped(object sender, TappedEventArgs e)
		{
			if (_viewModel.OpereOn)
			{
				var opere = _viewModel.OpereOrdinate;
				var index = opere.IndexOf(_viewModel.SelectedOpera);
				if (Metadata.GetTag(sender as BindableObject) == "left")
				{
					if (index > 0)
						_viewModel.SelectedOpera = opere[--index];
					else
						return;
				}
				else
				{
					if (index < opere.Count - 1)
						_viewModel.SelectedOpera = opere[++index];
					else
						return;
				}

				if (index == 0)
					LeftArrow.TextColor = DeviceManager.Instance.Colors[5];
				else
					LeftArrow.TextColor = DeviceManager.Instance.Colors[4];
				if (index == opere.Count - 1)
					RightArrow.TextColor = DeviceManager.Instance.Colors[5];
				else
					RightArrow.TextColor = DeviceManager.Instance.Colors[4];
			}
			else
			{
				var mostre = _viewModel.MostreOrdinate;
				var index = mostre.IndexOf(_viewModel.SelectedMostra);
				if (Metadata.GetTag(sender as BindableObject) == "left")
				{
					if (index > 0)
						_viewModel.SelectedMostra = mostre[--index];
					else
						return;
				}
				else
				{
					if (index < mostre.Count - 1)
						_viewModel.SelectedMostra = mostre[++index];
					else
						return;
				}
				MostraPhotoImage2.Source = _viewModel.SelectedMostra.Foto;

				if (index == 0)
					LeftArrow.TextColor = DeviceManager.Instance.Colors[5];
				else
					LeftArrow.TextColor = DeviceManager.Instance.Colors[4];
				if (index == mostre.Count - 1)
					RightArrow.TextColor = DeviceManager.Instance.Colors[5];
				else
					RightArrow.TextColor = DeviceManager.Instance.Colors[4];
			}
			_viewModel.IsBusy = true;
			_ = Task.Delay(650)
				.ContinueWith(t => _viewModel.IsBusy = false);
		}

		private async void OperaInMostra_Clicked(object sender, EventArgs e)
		{
			var opera = ((Button)sender).Parent.Parent.BindingContext as Opera;
			var mostra = _viewModel.SelectedMostra;
			if (await DisplayAlert("Rimozione opera da mostra", $"Sei sicuro di voler rimuovere l'opera [{opera.Nome}] dalla mostra [{_viewModel.SelectedMostra.Titolo}]?", "Si", "Annulla"))
			{
				_viewModel.IsBusy = true;
				mostra.Opere.Remove(opera.Id);
				_viewModel.SelectedMostra = null;
				_viewModel.SelectedMostra = mostra;
				_ = Task.Delay(450)
			.ContinueWith(t => _viewModel.IsBusy = false);
				await DatabaseManager.Instance.Delete($"mostre/{mostra.Id}/opere/{mostra.Opere.IndexOf(opera.Id)}");
			}
		}
		private async void AggiungiOperaAMostra_Clicked(object sender, EventArgs e)
		{
			var mostra = _viewModel.SelectedMostra;

			var titolo = await DisplayActionSheet("Seleziona l'opera da aggiungere", null, null, _viewModel.OpereOrdinate.Select(o => o.Nome).ToArray());
			if (!string.IsNullOrEmpty(titolo))
			{
				_viewModel.IsBusy = true;
				var opera = _viewModel.OpereOrdinate.ToList().Find(o => o.Nome == titolo);
				mostra.Opere.Add(opera.Id);
				_viewModel.SelectedMostra = null;
				_viewModel.SelectedMostra = mostra;
				_ = Task.Delay(450)
			.ContinueWith(t => _viewModel.IsBusy = false);
				await DatabaseManager.Instance.Put($"mostre/{mostra.Id}/opere/{mostra.Opere.Count-1}/", opera.Id);
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