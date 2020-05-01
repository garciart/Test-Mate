/*
 * Initializes the application and presents MainPage.
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
using TestMate.Models;
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestMate
{

    public partial class App : Application
    {
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
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Attempt to read and set stored settings upon application load
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
            // ReadSettingsFromFile() returns null if successful
            string errorMessage = AppFunctions.ReadSettingsFromFile();
            if (!String.IsNullOrEmpty(errorMessage))
            {
                // Display error
                Application.Current.MainPage.DisplayAlert("Test Mate", errorMessage, "OK");
                // Disable application only if the error is NOT a missing settings file (e.g., IOException, etc.)
                App.enableAppFlag = (errorMessage != AppResources.SettingsMissingErrorMessage) ? false : true;
            }
        }

        /// <summary>
        /// Handle when your app sleeps
        /// </summary>
        protected override void OnSleep()
        {

        }

        /// <summary>
        /// Handle when your app resumes
        /// </summary>
        protected override void OnResume()
        {

        }
    }
}
