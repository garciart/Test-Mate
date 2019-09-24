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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestMate.Common;
using TestMate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage {
        public StartPage() {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            List<TestFile> testfiles = new List<TestFile>();
            IEnumerable<string> files = Directory.EnumerateFiles(Constants.AppDataPath, "*.tm4");
            foreach (string fileName in files) {
                testfiles.Add(new TestFile {
                    FileName = Path.GetFileName(fileName),
                    TestName = File.ReadLines(fileName).First(),
                    DateCreated = File.GetCreationTime(fileName)
                });
            }
            fileList.ItemsSource = testfiles
                .OrderBy(n => n.TestName)
                .ToList();
        }
    }
}