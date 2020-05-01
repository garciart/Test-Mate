/*
 * Testmate Settings page.
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
using Xamarin.Forms.Xaml;

namespace TestMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        /// <summary>
        /// Initialize component and set pickers to indexes based on settings
        /// </summary>
        public SettingsPage()
        {
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
        private async void SaveSettingsButton_Clicked(object sender, EventArgs e)
        {
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
        private async void CancelSaveButton_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}