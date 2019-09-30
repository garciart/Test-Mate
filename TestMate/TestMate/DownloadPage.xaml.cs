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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TestMate.Common;
using TestMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage {
        private static List<TestQuestion> testQuestion;
        private static int questionIndex = 0;
        private static int correctAnswerCount = 0;
        public DownloadPage() {
            InitializeComponent();
            Test test = new Test();
            testQuestion = test.GetTest(Path.Combine(Constants.AppDataPath, "small-test.tmf"), AppFunctions.questionOrder, AppFunctions.termDisplay);
            PopulateControls();
        }

        private async void PreviousButton_Clicked(object sender, EventArgs e) {
            await this.DisplayAlert("Test Mate", "Previous button clicked.", "OK");
            if (questionIndex > 0) {
                questionIndex--;
                PopulateControls();
            }
            else {
                await this.DisplayAlert("Test Mate", "This is the beginning of the test.", "OK");
            }
        }

        private async void QuitButton_Clicked(object sender, EventArgs e) {
            bool answer = await DisplayAlert("Test Mate", "Are you sure you want to quit this test?", "Yes", "No");
            if(answer) {
                questionIndex = 0;
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async void NextButton_Clicked(object sender, EventArgs e) {
            if(AppFunctions.provideFeedback == Constants.ProvideFeedback.Yes) {
                await this.DisplayAlert("Test Mate",
                    ((string)ListView1.SelectedItem == testQuestion[questionIndex].Choices[testQuestion[questionIndex].CorrectAnswerIndex]) ?
                    "Correct.\n" + testQuestion[questionIndex].Explanation :
                    "Incorrect.\n" + testQuestion[questionIndex].Explanation,
                    "OK");
            }
            if (questionIndex < (testQuestion.Count - 1)) {
                questionIndex++;
                PopulateControls();
            }
            else {
                await this.DisplayAlert("Test Mate", "You've reached the end of the test.", "OK");
            }
        }

        private void PopulateControls() {
            QuestionLabel.Text = testQuestion[questionIndex].Question;
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            foreach (string c in testQuestion[questionIndex].Choices) {
                itemList.Add(c);
            }
            ListView1.ItemsSource = itemList;
            PreviousButton.IsEnabled = (questionIndex > 0) ? true : false;
            NextButton.IsEnabled = (questionIndex < (testQuestion.Count - 1)) ? true : false;
        }

        private void WriteTestToStorage() {
            /*
            string myTest = "";
            foreach (TestQuestion t in testQuestion) {
                myTest += t.Question + "\n";
            }
            Application.Current.MainPage.DisplayAlert("Test Mate", myTest, "OK");
            */

            /*
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "TestMate.small-test.tmf";
            string smallTestContents;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)) {
                    smallTestContents = reader.ReadToEnd();
                }
            }
            // Application.Current.MainPage.DisplayAlert("Test Mate", smallTestContents.ToString(), "OK");
            File.WriteAllText(Path.Combine(Constants.AppDataPath, "small-test.tmf"), smallTestContents, Encoding.UTF8);
            */
        }
    }
}