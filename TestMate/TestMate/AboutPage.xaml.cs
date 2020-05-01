/*
 * About TestMate page.
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
using TestMate.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            CopyrightLabel.Text = String.Format(AppResources.CopyrightLabel, DateTime.Now.Year.ToString());
        }

        private void UserManualButton_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new System.Uri("https://rgprogramming.com"));
        }
    }
}