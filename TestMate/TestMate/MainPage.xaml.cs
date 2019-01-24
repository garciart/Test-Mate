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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using TestMate.Resources;
using Xamarin.Forms;

namespace TestMate {
    public partial class MainPage : ContentPage {
        public MainPage() {

        }

        public void PopulateControls() {
            InitializeComponent();
            // For uniformity, make sure image is 160 pixels per inch
            headerImage.Source = ImageSource.FromResource("TestMate.Assets.headerImage2.png");
        }

        private async void StartNewTestButton_Clicked(object sender, EventArgs e) {
            FileData fileName = await CrossFilePicker.Current.PickFile();
            await this.DisplayAlert("Test Mate", (fileName == null ? AppResources.openFileError : (String.Format(AppResources.clickTestText, fileName))), "OK");
        }

        private async void ChangeSettingsButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new SettingsPage());            
        }

        private async void AboutTestMateButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new AboutPage());
            // await this.DisplayAlert("Test Mate", "You clicked About Test Mate!", "OK");
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            PopulateControls();
        }

        /*
        private double width;
        private double height;
        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height) {
                this.width = width;
                this.height = height;
                if (width > height) {
                    Grid.SetColumnSpan(headerImage, 2);
                    Grid.SetColumn(startButton, 0);
                    Grid.SetColumn(settingsButton, 0);
                    Grid.SetColumn(aboutButton, 1);
                    Grid.SetRow(aboutButton, 1);
                    Grid.SetColumn(exitButton, 1);
                    Grid.SetRow(exitButton, 2);
                }
                else {
                    Grid.SetColumnSpan(headerImage, 1);
                    Grid.SetColumn(startButton, 0);
                    Grid.SetColumn(settingsButton, 0);
                    Grid.SetColumn(aboutButton, 0);
                    Grid.SetRow(aboutButton, 3);
                    Grid.SetColumn(exitButton, 0);
                    Grid.SetRow(exitButton, 4);
                }
            }
        }
        */
    }
}
