/*
 * Development code experiment page. Delete before release itno production.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestMate.Common;
using System.IO;
using TestMate.Resources;

namespace TestMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevTestPage : ContentPage
    {
        public DevTestPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // URLContent.Text = await AppFunctions.devGetPage();
            
            List<string> testfiles = new List<string>();
            try
            {
                testfiles = await AppFunctions.devGetFiles();
                if (testfiles != null)
                {
                    TestList.ItemsSource = testfiles;
                }
                else
                {
                    await this.DisplayAlert("Test Mate", "No files found!", "OK");
                }
                
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Test Mate", "Something went wrong, but we've logged it in: " + ex.ToString(), "OK");
            }
            
        }

        private async void TestList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // CHECK https://stackoverflow.com/questions/45711428/download-file-with-webclient-or-httpclient
            string testFile = e.SelectedItem as string;
            Console.WriteLine(">>> SELECTED FILE: " + testFile);
            // string testFileURL = "http://testmate.rgprogramming.com/" + testFile;

            try
            {
                AppFunctions.devDownloadFileAsync2(testFile);
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