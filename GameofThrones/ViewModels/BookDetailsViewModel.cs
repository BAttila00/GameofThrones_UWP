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
	class BookDetailsViewModel : ViewModelBase {
		
		private Book _book;

		public ObservableCollection<Character> CharactersInBook { get; set; } = new ObservableCollection<Character>();
		public ObservableCollection<string> Authors { get; set; } = new ObservableCollection<string>();
		public Book Book {
			get { return _book; }
			set { Set(ref _book, value); }
		}


		/*
		private string _name;
		public string Name {
			get { return _name; }
			set { Set(ref _name, value); }
		}

		private string _isbn;
		public string Isbn {
			get { return _isbn; }
			set { Set(ref _isbn, value); }
		}
		*/
		public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {	//lefut amikor erre az oldalra navigálunk
			var bookUrl = (string)parameter;
			var service = new GOTService();
			Book = await service.GetBookAsync(bookUrl);

			foreach (var author in Book.authors) {
				Authors.Add(author);
			}

			//List < List<string> > characterUrisLists = service.splitList(Book.characters, 5);
			//List < List<Character> > charactersLists = new List<List<Character>>();
			//foreach (List<string> element in characterUrisLists) {
			//	charactersLists.Add(await service.GetCharactersInBook(element.ToArray()));
			//}
			//foreach (var charactersInBook in charactersLists) {
			//	foreach (var character in charactersInBook) {
			//		CharactersInBook.Add(character);
			//	}
			//}

			var charactersInBook = await service.GetCharactersInBook(Book.characters);
			foreach (var character in charactersInBook) {
				CharactersInBook.Add(character);
			}


			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		public void NavigateToCharacterDetails(Character selectedItem) {
			NavigationService.Navigate(typeof(CharacterDetails), selectedItem.url);
		}

		public void NavigateToMainDetails() {
			NavigationService.Navigate(typeof(MainPage));
		}

	}
}
