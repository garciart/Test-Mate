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
            URLContent.Text = await AppFunctions.devGetPage();
            List<string> testfiles = new List<string>();
            testfiles = await AppFunctions.devGetFiles();
            TestList.ItemsSource = testfiles;
        }
    }
}