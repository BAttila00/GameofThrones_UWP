using GameofThrones.Services;
using GameofThrones.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace GameofThrones.ViewModels {
	class CharacterDetailsViewModel : ViewModelBase {

		public ObservableCollection<House> Allegiances { get; set; } = new ObservableCollection<House>();
		public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
		public ObservableCollection<Book> PovBooks { get; set; } = new ObservableCollection<Book>();
		public CharacterDetails CharacterDetailsProp { get; set; }

		private Character _character;
		public Character Character {
			get { return _character; }
			set { Set(ref _character, value); }
		}

		private Character _father;

		public Character Father {
			get { return _father; }
			set { Set(ref _father, value); }
		}

		private Character _mother;

		public Character Mother {
			get { return _mother; }
			set { Set(ref _mother, value); }
		}

		private Character _spouse;

		public Character Spouse {
			get { return _spouse; }
			set { Set(ref _spouse, value); }
		}


		public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {   //lefut amikor erre az oldalra navigálunk
			var url = (string)parameter;
			var service = new GOTService();
			Character = await service.GetCharacterAsync(url);
			if (Character.father != "")
				Father = await service.GetAsync<Character>(new Uri(Character.father));
			if (Character.mother != "")
				Mother = await service.GetAsync<Character>(new Uri(Character.mother));
			if (Character.spouse != "")
				Spouse = await service.GetAsync<Character>(new Uri(Character.spouse));

			CharacterDetailsProp.SetButtonsDisable();

			var allegiances = await service.GetItemsList<House>(Character.allegiances);
			foreach (var item in allegiances) {
				Allegiances.Add(item);
			}

			var books = await service.GetItemsList<Book>(Character.books);
			foreach (var item in books) {
				Books.Add(item);
			}

			var povBooks = await service.GetItemsList<Book>(Character.povBooks);
			foreach (var item in povBooks) {
				PovBooks.Add(item);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		public void NavigateToCharacterDetails(Character selectedItem) {
			NavigationService.Navigate(typeof(CharacterDetails), selectedItem.url);
		}
		public void NavigateToHouseDetails(House selectedItem) {
			NavigationService.Navigate(typeof(HouseDetails), selectedItem.url);
		}
		public void NavigateToBookDetails(Book selectedItem) {
			NavigationService.Navigate(typeof(BookDetails), selectedItem.url);
		}
		public void NavigateToMainDetails() {
			NavigationService.Navigate(typeof(MainPage));
		}


	}
}
