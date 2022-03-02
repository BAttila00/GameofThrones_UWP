//https://anapioficeandfire.com/

using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using GameofThrones.Services;
using GameofThrones.Views;
using Windows.Media.Playback;
using Windows.Media.Core;
using System.Diagnostics;

namespace GameofThrones.ViewModels {

	public class MainPageViewModel : ViewModelBase {

		public ObservableCollection<Book> ThroneBooks { get; set; } = new ObservableCollection<Book>();
		public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
		public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();


		public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {
			//var service = new GOTService();
			//var books = await service.GetSelectedList<Book>("Books");
			//foreach (var item in books) {
			//	ThroneBooks.Add(item);
			//}

			/*Book bookType = new Book();
			Type t1 = bookType.GetType();
			Debug.WriteLine(t1.FullName);

			Type type = Type.GetType("Book");
			object instance = Activator.CreateInstance(type);
			Type t2 = instance.GetType();
			Debug.WriteLine(t2.FullName);*/

			Categories.Add("Books");
			Categories.Add("Characters");
			Categories.Add("Houses");
			await base.OnNavigatedToAsync(parameter, mode, state);      //tehát amikor await-et hívunk akkor a metódus amiben van, az visszatér, hogy az await futása alatt (az abban lévo utasítás/metódus) ne blokkolja a hívó szálat. 04 Xaml 2 -> 21.old
		}

		public void NavigateToDetails(object selectedItem, string content) {
			switch (content) {
				case "Books":
					var selectedBook = (Book)selectedItem;
					NavigationService.Navigate(typeof(BookDetails), selectedBook.url);      //BookDetails.xaml-re navigál és paraméterként átadja a bookUrl-t
																							//mivel BookDetails.xaml-ben az adatkötés BookDetailsViewModel-re mutat(lasd lent) ezért az ott lévo OnNavigatedToAsync fv fog lefutni
																							//< Page.DataContext >
																							//< vm:BookDetailsViewModel />
																							//</ Page.DataContext >
					break;
				case "Characters":
					var selectedCharacter = (Character)selectedItem;
					NavigationService.Navigate(typeof(CharacterDetails), selectedCharacter.url);
					break;
				case "Houses":
					var selectedHouse = (House)selectedItem;
					NavigationService.Navigate(typeof(HouseDetails), selectedHouse.url);
					break;
				default:
					break;
			}
		}

		public void NavigateToBookDetails(string bookUrl) {
			NavigationService.Navigate(typeof(BookDetails), bookUrl);       //BookDetails.xaml-re navigál és paraméterként átadja a bookUrl-t
																			//mivel BookDetails.xaml-ben az adatkötés BookDetailsViewModel-re mutat(lasd lent) ezért az ott lévo OnNavigatedToAsync fv fog lefutni
		}
																			//< Page.DataContext >
																			//< vm:BookDetailsViewModel />
																			//</ Page.DataContext >

		internal void NavigateToCharacterDetails(string url) {
			throw new NotImplementedException();
		}

		internal void NavigateToHouseDetails(string url) {
			throw new NotImplementedException();
		}



		public async void ComboBoxChanged(string content) {
			var service = new GOTService();
			switch (content) {
				case "Books":
					if (!ThroneBooks.Any()) {
						var books = await service.GetSelectedList<Book>(content.ToLower());
						foreach (var item in books) {
							ThroneBooks.Add(item);
						}
					}
					break;
				case "Characters":
					if (!Characters.Any()) {
						var characters = await service.GetSelectedList<Character>(content.ToLower());
						foreach (var item in characters) {
							Characters.Add(item);
						}
					}
					break;
				case "Houses":
					if (!Houses.Any()) {
						var houses = await service.GetSelectedList<House>(content.ToLower());
						foreach (var item in houses) {
							Houses.Add(item);
						}
					}
					break;
				default:
					break;
			}
		}

		public ObservableCollection<Book> FillSearchResultsBooks(string searcText) {
			ObservableCollection<Book> resultList = new ObservableCollection<Book>();
			foreach (var item in ThroneBooks) {
				if (item.name.ToLower().Contains(searcText))
					resultList.Add(item);
			}
			return resultList;
		}
		public ObservableCollection<Character> FillSearchResultsCharacters(string searcText) {
			ObservableCollection<Character> resultList = new ObservableCollection<Character>();
			foreach (var item in Characters) {
				if (item.name.ToLower().Contains(searcText))
					resultList.Add(item);
			}
			return resultList;
		}
		public ObservableCollection<House> FillSearchResultsHouses(string searcText) {
			ObservableCollection<House> resultList = new ObservableCollection<House>();
			foreach (var item in Houses) {
				if (item.name.ToLower().Contains(searcText))
					resultList.Add(item);
			}
			return resultList;
		}


	}
}