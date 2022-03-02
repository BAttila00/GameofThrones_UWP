using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameofThrones.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CharacterDetails : Page {
		public CharacterDetails() {
			this.InitializeComponent();
			ViewModel.CharacterDetailsProp = this;
		}

		private void GotLogoTapped(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToMainDetails();
		}

		private void SpouseButtonTapped(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails(ViewModel.Spouse);
		}

		private void MotherButtonTapped(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails(ViewModel.Mother);
		}

		private void FatherButtonTapped(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails(ViewModel.Father);
		}

		public void SetButtonsDisable() {
			if (ViewModel.Father == null)
				Father.IsEnabled = false;
			if (ViewModel.Mother == null)
				Mother.IsEnabled = false;
			if (ViewModel.Spouse == null)
				Spouse.IsEnabled = false;
		}

		private void AllegiancesDoubleTapped(object sender, DoubleTappedRoutedEventArgs e) {
			ViewModel.NavigateToHouseDetails((House)AllegiancesList.SelectedItem);
		}

		private void BooksDoubleTapped(object sender, DoubleTappedRoutedEventArgs e) {
			var v = sender as ListView;				//így is jó
			ViewModel.NavigateToBookDetails((Book)v.SelectedItem);
		}

		private void PovBooksDoubleTapped(object sender, DoubleTappedRoutedEventArgs e) {
			ViewModel.NavigateToBookDetails((Book)PovBooksList.SelectedItem);
		}
	}
}
