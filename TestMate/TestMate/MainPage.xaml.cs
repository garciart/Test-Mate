/*
 * Landing page.
 *
 * .NET Standard version used: 2.0
 * C# version used: 7.3
 *
 * Styling guide: .NET Core Engineering guidelines
 *     (https://github.com/dotnet/aspnetcore/wiki/Engineering-guidelines#coding-guidelines) and
 *     C# Programming Guide
 *     (https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
 *
 * @category  Testmate
 * @package   TestMate
 * @author    Rob Garcia <rgarcia@rgprogramming.com>
 * @license   https://opensource.org/licenses/MIT The MIT License
 * @link      https://github.com/garciart/TestMate
 * @copyright 1993-2020 Rob Garcia
 */

using System;
using TestMate.Common;
using Xamarin.Forms;

namespace TestMate
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Initialize component and display logo, but disable buttons if unable to read or initialize settings file.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            AppFunctions.testGetFiles();
            // For uniformity, make sure image is 160 pixels per inch
            HeaderImage.Source = ImageSource.FromResource("TestMate.Assets.headerImage2.png");
            // Disable all buttons except the About button if App.xaml.cs was not able to read or initialize the settings file.
            if (App.enableAppFlag == false)
            {
                StartButton.IsEnabled = false;
                SettingsButton.IsEnabled = false;
                DownloadButton.IsEnabled = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void DownloadButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DownloadPage());
        }

        private async void DevTestButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DevTestPage());
        }
    }
}
