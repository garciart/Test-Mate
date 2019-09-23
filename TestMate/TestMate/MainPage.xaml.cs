/*
 * The MIT License
 *
 * Copyright 2018 Rob Garcia at rgarcia@rgprogramming.com.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using TestMate.Common;
using Xamarin.Forms;

namespace TestMate {
    public partial class MainPage : ContentPage {
        /// <summary>
        /// Initialize component and display logo, but disable buttons if unable to read or initialize settings file.
        /// </summary>
        public MainPage() {
            InitializeComponent();
            // For uniformity, make sure image is 160 pixels per inch
            headerImage.Source = ImageSource.FromResource("TestMate.Assets.headerImage2.png");
            // Disable all buttons except the About button if App.xaml.cs was not able to read or initialize the settings file.
            if (Constants.enableAppFlag == false) {
                startButton.IsEnabled = false;
                settingsButton.IsEnabled = false;
                downloadButton.IsEnabled = false;
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        }

        private async void StartButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new StartPage());
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void AboutButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new AboutPage());
        }

        private async void DownloadButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new DownloadPage());
            // await this.DisplayAlert("Test Mate", "You clicked Download Test!", "OK");
        }
    }
}
