using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin_Test.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            OnAppearing();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var calculations = new List<calculation>();
            var Operations = Directory.EnumerateFiles(App.FolderPath, "*.Operations.txt");

            foreach (var Operation in Operations)
            {
                calculations.Add(new calculation
                {
                    OperationID = Convert.ToInt32(Operation),
                    OperationResult = File.ReadAllText(Operation),
                    Date = File.GetCreationTime(Operation)
                });
            }

            listView.ItemsSource = calculations
                .OrderBy(d => d.Date)
                .ToList();
        }
    }
}