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
using TestMate.Common;
using TestMate.Models;
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestMate {

    public partial class App : Application {
        // Initialize global settings
        public static Constants.QuestionOrder questionOrder = new Constants.QuestionOrder();
        public static Constants.TermDisplay termDisplay = new Constants.TermDisplay();
        public static Constants.ProvideFeedback provideFeedback = new Constants.ProvideFeedback();
        // All UI items (e.g., buttons, etc.) are enabled by default.
        public static bool enableAppFlag = true;
        public static TestFile testFile = new TestFile();

        /// <summary>
        /// 
        /// </summary>
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
            string errorMessage = AppFunctions.ReadSettingsFromFile();
            if (!String.IsNullOrEmpty(errorMessage)) {
                // Display error
                Application.Current.MainPage.DisplayAlert("Test Mate", errorMessage, "OK");
                // Disable application only if the error is NOT a missing settings file (e.g., IOException, etc.)
                App.enableAppFlag = (errorMessage != AppResources.SettingsMissingErrorMessage) ? false : true;
            }
        }

        /// <summary>
        /// Handle when your app sleeps
        /// </summary>
        protected override void OnSleep() {

        }

        /// <summary>
        /// Handle when your app resumes
        /// </summary>
        protected override void OnResume() {

        }
    }
}
