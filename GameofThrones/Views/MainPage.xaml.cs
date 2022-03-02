using GameofThrones.Models;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GameofThrones.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

		private void Books_ItemClick(object sender, ItemClickEventArgs e) {
			//var bookUrl = (Book)e.ClickedItem;
			//ViewModel.NavigateToDetails(bookUrl.url);       //xaml-ben el van helyezve: <vm:MainPageViewModel x:Name="ViewModel" /> így most a MainPageViewModel-ben lévo NavigateToDetails fv fog lefutni
		}

		//https://social.msdn.microsoft.com/Forums/en-US/e32c3474-879e-4fac-af62-f6f87022cc90/double-tap-on-a-listview-item?forum=winappswithcsharp				//doubleTap
		private void LsView_DoubleTapped_01(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e) {           //most doubleTap-re váltunk
			//System.Activator.CreateInstance(Type.GetType("Class1"));
			//Type book = Type.GetType("Class1");
			//var bookUrl = Convert.ChangeType(LsView.SelectedItem, Type.GetType("Book"));
			//Debug.WriteLine(selectedItem.url);

			string content = (string)CategoriesCombobBox.SelectedItem;
			ViewModel.NavigateToDetails(LsView.SelectedItem, content);   //xaml-ben el van helyezve: <vm:MainPageViewModel x:Name="ViewModel" /> így most a MainPageViewModel-ben lévo NavigateToDetails fv fog lefutni
			/*switch (content) {
				case "Books":
					var selectedBook = (Book)LsView.SelectedItem;
					ViewModel.NavigateToBookDetails(selectedBook.url);   //xaml-ben el van helyezve: <vm:MainPageViewModel x:Name="ViewModel" /> így most a MainPageViewModel-ben lévo NavigateToBookDetails fv fog lefutni
					break;
				case "Characters":
					var selectedCharacter = (Character)LsView.SelectedItem;
					ViewModel.NavigateToCharacterDetails(selectedCharacter.url);
					break;
				case "Houses":
					var selectedHouse = (House)LsView.SelectedItem;
					ViewModel.NavigateToHouseDetails(selectedHouse.url);
					break;
				default:
					break;
			}*/
		}


		private void ComboBox_01_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			var _sender = sender as ComboBox;
			string content = (string)_sender.SelectedItem;
			ViewModel.ComboBoxChanged(content);

			switch (content) {
				case "Books":
					LsView.ItemsSource = ViewModel.ThroneBooks;
					break;
				case "Characters":
					LsView.ItemsSource = ViewModel.Characters;
					break;
				case "Houses":
					LsView.ItemsSource = ViewModel.Houses;
					break;
				default:
					break;
			}
		}

		private void SearchButtonTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
			var comboBox = CategoriesCombobBox as ComboBox;
			string searchText = SearchBox.Text;
			string content = (string)comboBox.SelectedItem;
			switch (content) {
				case "Books":
					SearchResultList.ItemsSource = ViewModel.FillSearchResultsBooks(searchText);
					break;
				case "Characters":
					SearchResultList.ItemsSource = ViewModel.FillSearchResultsCharacters(searchText);
					break;
				case "Houses":
					SearchResultList.ItemsSource = ViewModel.FillSearchResultsHouses(searchText);
					break;
				default:
					break;
			}
		}

		private void SearchResultListDoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e) {
			string content = (string)CategoriesCombobBox.SelectedItem;
			ViewModel.NavigateToDetails(SearchResultList.SelectedItem, content);
		}
	}
}
