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
	public sealed partial class HouseDetails : Page {
		public HouseDetails() {
			this.InitializeComponent();
			ViewModel.HouseDetailsProp = this;
		}

		private void NavigateToCurrentLord(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails(ViewModel.CurrentLord);
		}

		private void NavigateToHeir(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails(ViewModel.Heir);
		}

		private void NavigateToOverlord(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToHouseDetails(ViewModel.Overlord);
		}

		private void NavigateToFounder(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails(ViewModel.Founder);
		}

		private void SwornMembersDoubleTapped(object sender, DoubleTappedRoutedEventArgs e) {
			ViewModel.NavigateToCharacterDetails((Character)SwornMembersList.SelectedItem);
		}

		private void CadetBranchesDoubleTapped(object sender, DoubleTappedRoutedEventArgs e) {
			ViewModel.NavigateToHouseDetails((House)CadetBranchesList.SelectedItem);
		}

		public void SetButtonsDisable() {
			if (ViewModel.CurrentLord == null)
				Currentlord.IsEnabled = false;
			if (ViewModel.Overlord == null)
				Overlord.IsEnabled = false;
			if (ViewModel.Heir == null)
				Heir.IsEnabled = false;
			if (ViewModel.Founder == null)
				Founder.IsEnabled = false;
		}

		private void GotLogoTapped(object sender, TappedRoutedEventArgs e) {
			ViewModel.NavigateToMainDetails();
		}
	}
}
