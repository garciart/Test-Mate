using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestMate.Resources;
using System.IO;

namespace TestMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage () {
            InitializeComponent();
            questionOrderPicker.SelectedIndex = int.Parse(App.configContents[0]);
            keyTermDisplayPicker.SelectedIndex = int.Parse(App.configContents[1]);
            provideFeedbackPicker.SelectedIndex = int.Parse(App.configContents[2]);
        }

        private async void SaveSettingsButton_Clicked(object sender, EventArgs e) {
            App.configContents[0] = questionOrderPicker.SelectedIndex.ToString();
            App.configContents[1] = keyTermDisplayPicker.SelectedIndex.ToString();
            App.configContents[2] = provideFeedbackPicker.SelectedIndex.ToString();
            File.WriteAllLines(App._configFile, App.configContents);
            await this.DisplayAlert("Test Mate", AppResources.saveSettingsPass, "OK");
            // await this.DisplayAlert("Test Mate", (fileName == null ? AppResources.openFileErrorMessage : (String.Format(AppResources.clickTestMessage, fileName))), "OK");
        }
        /*
         * <Button x:Name="switchLanguageButton" Text="{x:Static local:Resources.AppResources.swtichLanguageButton}" StyleClass="button" Clicked="SwitchLanguageButton_Clicked" />
         * private async void SwitchLanguageButton_Clicked(object sender, EventArgs e) {
            String cultureName = TestMate.Resources.AppResources.Culture.Name;
            TestMate.Resources.AppResources.Culture = new System.Globalization.CultureInfo((cultureName == "en-US" ? "es-ES" : "en-US"));
            await Navigation.PopModalAsync();
        }
        */
    }
}