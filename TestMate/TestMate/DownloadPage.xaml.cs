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
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestMate.Common;
using TestMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage {
        public DownloadPage() {
            InitializeComponent();
        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            List<string> testfiles = new List<string>();
            IEnumerable<string> tests = await AppFunctions.GetTestList();
            foreach (string test in tests) {
                testfiles.Add(test);
            }
            TestList.ItemsSource = testfiles;
        }

        private async void TestList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            string testFile = e.SelectedItem as string;
            // await this.DisplayAlert("Test Mate", String.Format("You selected {0}!", testFile), "OK");
            // await this.DisplayAlert("Test Mate", String.Format("It will be stored at {0}!", Constants.AppDataPath + "/" + testFile), "OK");
            string testURL = "https://raw.githubusercontent.com/garciart/TestMate/master/Tests/" + testFile;
            // await this.DisplayAlert("Test Mate", String.Format("We'll download from {0}!", testURL), "OK");
            try {
                byte[] returnedBytes = await AppFunctions.DownloadFileAsync(testURL);
                File.WriteAllBytes(String.Format("{0}/{1}", Constants.AppDataPath, testFile), returnedBytes);
            }
            catch (Exception ex) {
                await this.DisplayAlert("Test Mate", ex.Message, "OK");
            }
        }
    }
}