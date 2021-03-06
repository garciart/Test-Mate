﻿/*
 * Question and answer page.
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TestMate.Common;
using TestMate.Models;
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        // Empty list of test questions; populated based on settings
        private readonly List<TestQuestion> testQuestion;
        // Initialize and set a question index to 0 each time a test is started
        private int questionIndex = 0;
        // Holds the question, the user's selection, and if the selection was correct
        private readonly List<Result> resultList = new List<Result>();
        // Use this string (instead of creating a new instance of Test just to get the test title) for PoplateControls
        private readonly string testTitle = "";

        public TestPage(TestFile testFile)
        {
            InitializeComponent();
            // Create the actual test and store the questions in a list
            // Test test = new Test();
            // testQuestion = test.GetTest(Path.Combine(Constants.AppDataPath, testFile.FileName), App.questionOrder, App.termDisplay);
            testQuestion = AppFunctions.GetTest(Path.Combine(Constants.AppDataPath, testFile.FileName), App.questionOrder, App.termDisplay);
            // Initialize the results list instead of using Add. This makes changng the answers to questions easier
            foreach (TestQuestion tq in testQuestion)
            {
                resultList.Add(new Result(tq.Question, null, false));
            }
            // Display the test title and the first question
            testTitle = AppFunctions.TestTitle;
            PopulateControls();
        }

        private async void ReviewButton_Clicked(object sender, EventArgs e)
        {
            if (questionIndex > 0)
            {
                questionIndex--;
                PopulateControls();
            }
            else
            {
                await this.DisplayAlert("Test Mate", AppResources.TestBeginningMessage, "OK");
            }
        }

        private async void QuitButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Test Mate", AppResources.TestQuitMessage, AppResources.ButtonYes, AppResources.ButtonNo);
            if (answer)
            {
                EndTest();
            }
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string selectedItem = ((Choice)ListView1.SelectedItem).ChoiceText;
                resultList[questionIndex].SelectedItem = selectedItem;
                resultList[questionIndex].CorrectFlag = (resultList[questionIndex].SelectedItem == testQuestion[questionIndex].Choices[testQuestion[questionIndex].CorrectAnswerIndex]) ? true : false;
                resultList[questionIndex].Correct = (resultList[questionIndex].SelectedItem == testQuestion[questionIndex].Choices[testQuestion[questionIndex].CorrectAnswerIndex]) ? AppResources.ButtonCorrect : AppResources.ButtonWrong;
                if (App.provideFeedback == Constants.ProvideFeedback.Yes)
                {
                    await this.DisplayAlert("Test Mate", resultList[questionIndex].CorrectFlag ?
                        AppResources.ButtonCorrect + "\n" + testQuestion[questionIndex].Explanation :
                        AppResources.ButtonWrong + "\n" + testQuestion[questionIndex].Explanation,
                        "OK");
                }
                if (questionIndex < (testQuestion.Count - 1))
                {
                    questionIndex++;
                    PopulateControls();
                }
                else
                {
                    await this.DisplayAlert("Test Mate", AppResources.TestEndMessage, "OK");
                    EndTest();
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Test Mate", AppResources.TestBadSelectionMessage, "OK");
            }
        }

        private void PopulateControls()
        {
            Title = testTitle;
            QuestionLabel.Text = String.Format("{0} ({1}/{2})", testQuestion[questionIndex].Question, questionIndex + 1, resultList.Count);
            ObservableCollection<Choice> itemList = new ObservableCollection<Choice>();
            foreach (string c in testQuestion[questionIndex].Choices)
            {
                itemList.Add(new Choice { ChoiceText = c });
            }
            ListView1.ItemsSource = itemList;
            // Reset selected item. For some reason, if two similar answers are submitted in a row (e.g., true and true), the answer is not highlighted
            ListView1.SelectedItem = null;
            ListView1.SelectedItem = resultList[questionIndex].SelectedItem;
            ReviewButton.IsEnabled = (questionIndex > 0) ? true : false;
            SubmitButton.IsEnabled = (questionIndex <= (testQuestion.Count)) ? true : false;
        }

        private async void EndTest()
        {
            // Reset the question index
            questionIndex = 0;
            // Get a count of correct answers
            int correctCount = 0;
            foreach (Result r in resultList)
            {
                correctCount += r.CorrectFlag == true ? 1 : 0;
            }
            // Display current score and ask the user if they want to see a detailed results list
            bool answer = await DisplayAlert("Test Mate",
                String.Format(AppResources.TestScoreMessage, ((float)correctCount / (float)resultList.Count) * 100, correctCount, resultList.Count),
                "Yes", "No");
            if (answer)
            {
                await Navigation.PushAsync(new ResultsPage(resultList));
                Navigation.RemovePage(this);
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void WriteTestToStorage()
        {
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

    public class Choice
    {
        public string ChoiceText { get; set; }
    }
}