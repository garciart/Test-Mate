/*
 * Test download page from GitHub.
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
using System.IO;
using TestMate.Common;
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage
    {
        public DownloadPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<string> testfiles = new List<string>();
            IEnumerable<string> tests = await AppFunctions.GetTestList();
            foreach (string test in tests)
            {
                testfiles.Add(test);
            }
            TestList.ItemsSource = testfiles;
        }

        private async void TestList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string testFile = e.SelectedItem as string;
            string testURL = "https://raw.githubusercontent.com/garciart/TestMate/master/Tests/" + testFile;
            try
            {
                byte[] returnedBytes = await AppFunctions.DownloadFileAsync(testURL);
                File.WriteAllBytes(String.Format("{0}/{1}", Constants.AppDataPath, testFile), returnedBytes);
                await this.DisplayAlert("Test Mate", AppResources.DownloadSuccessMessage, "OK");
                OnAppearing();
                // await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Test Mate", String.Format(AppResources.DownloadErrorMessage, ex.Message), "OK");
            }
        }
    }
}