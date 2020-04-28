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
            URLContent.Text = AppFunctions.testGetFiles().ToString();
        }
    }
}