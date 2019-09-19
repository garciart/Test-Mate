using System;
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage {
        public SettingsPage() {
            InitializeComponent();
            questionOrderPicker.SelectedIndex = int.Parse(Common.configContents[0]);
            keyTermDisplayPicker.SelectedIndex = int.Parse(Common.configContents[1]);
            provideFeedbackPicker.SelectedIndex = int.Parse(Common.configContents[2]);
        }

        private async void SaveSettingsButton_Clicked(object sender, EventArgs e) {
            Common.configContents[0] = questionOrderPicker.SelectedIndex.ToString();
            Common.configContents[1] = keyTermDisplayPicker.SelectedIndex.ToString();
            Common.configContents[2] = provideFeedbackPicker.SelectedIndex.ToString();
            await this.DisplayAlert("Test Mate", Common.SaveSettingsToFile(), "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void CancelSaveButton_Clicked(object sender, EventArgs e) {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}