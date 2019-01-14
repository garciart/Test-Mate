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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestMate {
    public partial class MainPage : ContentPage {
        private double width;
        private double height;

        public MainPage() {
            InitializeComponent();
            headerImage.Source = ImageSource.FromResource("TestMate.Assets.tmbanner360.png");
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void StartNewTestBtn_Clicked(object sender, EventArgs e) {
            await this.DisplayAlert("Test Mate", "You clicked Start a New Test!", "OK");
        }

        private async void ChangeSettingsBtn_Clicked(object sender, EventArgs e) {
            await this.DisplayAlert("Test Mate", "You clicked Change Settings!", "OK");
        }

        private async void AboutTestMateBtn_Clicked(object sender, EventArgs e) {
            await Navigation.PushModalAsync(new AboutPage());
            // await this.DisplayAlert("Test Mate", "You clicked About Test Mate!", "OK");
        }

        private async void ExitTestMateBtn_Clicked(object sender, EventArgs e) {
            if(await this.DisplayAlert("Test Mate", "Are you sure you want to quit?", "Yes", "No")) {
                await this.DisplayAlert("Test Mate", "You clicked Exit Test Mate!", "OK");
            }
        }

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
    }
}
