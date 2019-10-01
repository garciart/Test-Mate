using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestMate.Models;

namespace TestMate {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage {
        public ResultsPage(List<Result> resultList) {
            InitializeComponent();
            ResultsList.ItemsSource = resultList;
        }
    }
}