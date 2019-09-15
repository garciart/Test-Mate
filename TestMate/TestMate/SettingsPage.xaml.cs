using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
            PopulateControls();
		}

        private void PopulateControls() {
            InitializeComponent();
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