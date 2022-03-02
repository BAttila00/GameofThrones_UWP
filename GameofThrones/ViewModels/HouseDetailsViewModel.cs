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
	class HouseDetailsViewModel : ViewModelBase {

		private House _house;
		public ObservableCollection<string> Titles { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> Seats { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> AncestralWeapons { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<House> CadetBranches { get; set; } = new ObservableCollection<House>();
		public ObservableCollection<Character> SwornMembers { get; set; } = new ObservableCollection<Character>();

		public HouseDetails HouseDetailsProp { get; set; }
		public House House {
			get { return _house; }
			set { Set(ref _house, value); }
		}

		private Character _currentLord;
		public Character CurrentLord {
			get { return _currentLord; }
			set { Set(ref _currentLord, value); }
		}

		private Character _heir;
		public Character Heir {
			get { return _heir; }
			set { Set(ref _heir, value); }
		}

		private House _overlord;
		public House Overlord {
			get { return _overlord; }
			set { Set(ref _overlord, value); }
		}

		private Character _founder;
		public Character Founder {
			get { return _founder; }
			set { Set(ref _founder, value); }
		}


		public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {   //lefut amikor erre az oldalra navigálunk
			var houseUrl = (string)parameter;
			var service = new GOTService();
			House = await service.GetHouseAsync(houseUrl);
			if(House.currentLord != "")
				CurrentLord = await service.GetAsync<Character>(new Uri(House.currentLord));
			if (House.heir != "")
				Heir = await service.GetAsync<Character>(new Uri(House.heir));
			if (House.overlord != "")
				Overlord = await service.GetAsync<House>(new Uri(House.overlord));
			if (House.founder != "")
				Founder = await service.GetAsync<Character>(new Uri(House.founder));

			HouseDetailsProp.SetButtonsDisable();

			foreach (var title in House.titles) {
				Titles.Add(title);
			}
			foreach (var seat in House.seats) {
				Seats.Add(seat);
			}
			foreach (var ancestralWeapon in House.ancestralWeapons) {
				AncestralWeapons.Add(ancestralWeapon);
			}

			var swornMembers = await service.GetItemsList<Character>(House.swornMembers);
			foreach (var character in swornMembers) {
				SwornMembers.Add(character);
			}

			var cadetBranches = await service.GetItemsList<House>(House.cadetBranches);
			foreach (var house in cadetBranches) {
				CadetBranches.Add(house);
			}

			await base.OnNavigatedToAsync(parameter, mode, state);
		}

		public void NavigateToCharacterDetails(Character selectedItem) {
			NavigationService.Navigate(typeof(CharacterDetails), selectedItem.url);
		}
		public void NavigateToHouseDetails(House selectedItem) {
			NavigationService.Navigate(typeof(HouseDetails), selectedItem.url);
		}
		public void NavigateToBookDetails(Character selectedItem) {
			NavigationService.Navigate(typeof(BookDetails), selectedItem.url);
		}
		public void NavigateToMainDetails() {
			NavigationService.Navigate(typeof(MainPage));
		}
	}
}