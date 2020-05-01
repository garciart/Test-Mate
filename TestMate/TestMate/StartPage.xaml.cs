/*
 * Test selection page.
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
using System.Linq;
using TestMate.Common;
using TestMate.Models;
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<TestFile> testfiles = new List<TestFile>();
            IEnumerable<string> files = Directory.EnumerateFiles(Constants.AppDataPath, "*.tmf");
            foreach (string fileName in files)
            {
                testfiles.Add(new TestFile
                {
                    FileName = Path.GetFileName(fileName),
                    TestName = File.ReadLines(fileName).First(),
                    DateCreated = File.GetCreationTime(fileName)
                });
            }
            FileList.ItemsSource = testfiles
                .OrderBy(n => n.TestName)
                .ToList();
        }

        private async void FileList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TestFile fileName = e.SelectedItem as TestFile;
            await Navigation.PushAsync(new TestPage(fileName));
            Navigation.RemovePage(this);
        }

        private async void DeleteButton_Clicked(object sender, System.EventArgs e)
        {
            Button button = sender as Button;
            string fileName = button.BindingContext.ToString();
            bool answer = await DisplayAlert("Test Mate", String.Format(AppResources.StartDeleteConfirm, fileName), AppResources.ButtonYes, AppResources.ButtonNo);
            if (answer)
            {
                try
                {
                    string f = String.Format("{0}/{1}", Constants.AppDataPath, fileName);
                    if (File.Exists(f))
                    {
                        File.Delete(f);
                        await this.DisplayAlert("Test Mate", String.Format(AppResources.StartDeleteSuccessMessage, fileName), "OK");
                        OnAppearing();
                    }
                    else
                    {
                        await this.DisplayAlert("Test Mate", AppResources.StartDeleteNotFoundMessage, "OK");
                    }
                }
                catch (Exception ex)
                {
                    await this.DisplayAlert("Test Mate", String.Format(AppResources.StartDeleteErrorMessage, ex.Message), "OK");
                }
                // await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}