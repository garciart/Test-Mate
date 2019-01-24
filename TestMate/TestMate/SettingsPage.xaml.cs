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
			InitializeComponent ();
		}

        private async void SwitchLanguageButton_Clicked(object sender, EventArgs e) {
            String cultureName = Thread.CurrentThread.CurrentCulture.Name;
            // String cultureName = CultureInfo.CurrentCulture.Name;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo((cultureName == "en-US" ? "es-ES" : "en-US"));
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo((cultureName == "en-US" ? "es-ES" : "en-US"));
            cultureName = Thread.CurrentThread.CurrentCulture.Name;
            await Navigation.PopModalAsync();
        }
    }
}