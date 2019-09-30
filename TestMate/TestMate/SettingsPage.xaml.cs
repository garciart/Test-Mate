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
using Xamarin.Forms.Xaml;

namespace TestMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage {
        /// <summary>
        /// Initialize component and set pickers to indexes based on settings
        /// </summary>
        public SettingsPage() {
            InitializeComponent();
            // QuestionOrderPicker.SelectedIndex = int.Parse(AppFunctions.settings[0]);
            QuestionOrderPicker.SelectedIndex = (int)App.questionOrder;
            TermDisplayPicker.SelectedIndex = (int)App.termDisplay;
            ProvideFeedbackPicker.SelectedIndex = (int)App.provideFeedback;
        }

        /// <summary>
        /// Get and save settings to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveSettingsButton_Clicked(object sender, EventArgs e) {
            App.questionOrder = (Constants.QuestionOrder)QuestionOrderPicker.SelectedIndex;
            App.termDisplay = (Constants.TermDisplay)TermDisplayPicker.SelectedIndex;
            App.provideFeedback = (Constants.ProvideFeedback)ProvideFeedbackPicker.SelectedIndex;
            // Returns success or error message
            await this.DisplayAlert("Test Mate", AppFunctions.SaveSettingsToFile(App.questionOrder, App.termDisplay, App.provideFeedback), "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Return to main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CancelSaveButton_Clicked(object sender, EventArgs e) {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}