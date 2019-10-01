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
    public partial class TestPage : ContentPage {
        // Empty list of test questions; populated based on settings
        private readonly List<TestQuestion> testQuestion;
        // Initialize and set a question index to 0 each time a test is started
        private int questionIndex = 0;
        // Holds the question, the user's selection, and if the selection was correct
        private readonly List<Result> resultList = new List<Result>();
        // Use this string (instead of creating a new instance of Test just to get the test title) for PoplateControls
        private readonly string testTitle = "";

        public TestPage(TestFile testFile) {
            InitializeComponent();
            // Create the actual test and store the questions in a list
            Test test = new Test();
            testQuestion = test.GetTest(Path.Combine(Constants.AppDataPath, testFile.FileName), App.questionOrder, App.termDisplay);
            // Initialize the results list instead of using Add. This makes changng the answers to questions easier
            foreach (TestQuestion tq in testQuestion) {
                resultList.Add(new Result(tq.Question, null, false));
            }
            // Display the test title and the first question
            testTitle = test.TestTitle;
            PopulateControls();
        }

        private async void ReviewButton_Clicked(object sender, EventArgs e) {
            // await this.DisplayAlert("Test Mate", "Review button clicked.", "OK");
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
            if (answer) {
                EndTest();
            }
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e) {
            string selectedItem = (string)ListView1.SelectedItem;
            if (selectedItem == null) {
                await this.DisplayAlert("Test Mate", "Nothing selected!", "OK");
            }
            else {
                resultList[questionIndex].SelectedItem = selectedItem;
                resultList[questionIndex].CorrectFlag = (resultList[questionIndex].SelectedItem == testQuestion[questionIndex].Choices[testQuestion[questionIndex].CorrectAnswerIndex]) ? true : false;
                if (App.provideFeedback == Constants.ProvideFeedback.Yes) {
                    await this.DisplayAlert("Test Mate", resultList[questionIndex].CorrectFlag ?
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
                    EndTest();
                }
            }
        }

        private void PopulateControls() {
            Title = String.Format("{0} ({1}/{2})", testTitle, questionIndex + 1, resultList.Count);
            QuestionLabel.Text = testQuestion[questionIndex].Question;
            ObservableCollection<string> itemList = new ObservableCollection<string>();
            foreach (string c in testQuestion[questionIndex].Choices) {
                itemList.Add(c);
            }
            ListView1.ItemsSource = itemList;
            // Reset selected item. For some reason, if two similar answers are submitted in a row (e.g., true and true), the answer is not highlighted
            ListView1.SelectedItem = null;
            ListView1.SelectedItem = resultList[questionIndex].SelectedItem;
            ReviewButton.IsEnabled = (questionIndex > 0) ? true : false;
            SubmitButton.IsEnabled = (questionIndex <= (testQuestion.Count)) ? true : false;
        }

        private async void EndTest() {
            // Reset the question index
            questionIndex = 0;
            // Get a count of correct answers
            int correctCount = 0;
            foreach (Result r in resultList) {
                correctCount += r.CorrectFlag == true ? 1 : 0;
            }
            // Display current score and ask the user if they want to see a detailed results list
            bool answer = await DisplayAlert("Test Mate",
                String.Format("Your score was {0:0.00}% ({1} out of {2}).\nWould you like to see your results in more detail?", ((float)correctCount / (float)resultList.Count) * 100, correctCount, resultList.Count),
                "Yes", "No");
            if (answer) {
                await Navigation.PushAsync(new ResultsPage(resultList));
                Navigation.RemovePage(this);
            }
            else {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
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