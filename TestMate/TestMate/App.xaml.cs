/*
 * The MIT License
 *
 * Copyright 2019 Rob Garcia at rgarcia@rgprogramming.com.
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
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestMate {
    /**
     * Application controller page
     *
     * @author Rob Garcia at rgarcia@rgprogramming.com
     */
    public partial class App : Application {
        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Attempt to read and set stored settings upon application load
        /// </summary>
        protected override void OnStart() {
            // Handle when your app starts
            // ReadSettingsFromFile() returns null if successful
            string errorMessage = Common.ReadSettingsFromFile();
            if (!String.IsNullOrEmpty(errorMessage)) {
                // Display error
                Application.Current.MainPage.DisplayAlert("Test Mate", errorMessage, "OK");
                // Disable application only if the error is NOT a missing settings file (e.g., IOException, etc.)
                Common.enableAppFlag = (errorMessage != AppResources.SettingsMissingErrorMessage) ? false : true;
            }
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

       protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
