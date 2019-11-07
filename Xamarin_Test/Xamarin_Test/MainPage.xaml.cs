using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Test.Models;

namespace Xamarin_Test
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Operations.txt");
        int currrentState = 1;
        string operators;
        double firstNumber, secondNumber;
        public MainPage()
        {
            InitializeComponent();
            OnClear(new object(), new EventArgs());

            if (File.Exists(_fileName))
            {
                resultText.Text = File.ReadAllText(_fileName);
            }
        }

        void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            currrentState = 1;
            this.resultText.Text = "0";
        }

        void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            if (this.resultText.Text == "0" || currrentState < 0)
            {
                this.resultText.Text = "";
                if (currrentState < 0)
                {
                    currrentState *= -1;
                }
            }

                this.resultText.Text += pressed;
                double number;

                if (double.TryParse(this.resultText.Text, out number))
                {
                    this.resultText.Text = number.ToString("N0");
                    if (currrentState == 1)
                    {
                        firstNumber = number;
                    }
                    else
                    {
                        secondNumber = number;
                    }
                }   
        }

        void OnSelectOperator(object sender, EventArgs e)
        {
            currrentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            operators = pressed;
        }

        async void onCalculate(object sender, EventArgs e)
        {
            File.WriteAllText(_fileName, resultText.Text);
            var Calculation = (calculation)BindingContext;

            if (currrentState == 2)
            {
                double result = 0;

                if (operators == "+")
                {
                    result = firstNumber + secondNumber;
                }

                if (operators == "-")
                {
                    result = firstNumber - secondNumber;
                }

                if (operators == "÷")
                {
                    result = firstNumber / secondNumber;
                }

                if (operators == "×")
                {
                    result = firstNumber * secondNumber;
                }

                this.resultText.Text = result.ToString("N0");

                firstNumber = secondNumber = 0;
                currrentState = 1;

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var calculations = new List<calculation>();
            var OperationIDs = Directory.EnumerateFiles(App.FolderPath, "*.Operations.txt");

            foreach (var OperationID in OperationIDs)
            {
                calculations.Add(new calculation
                {
                    OperationID = Convert.ToInt32(OperationID),
                    OperationResult = File.ReadAllText(OperationID),
                    Date = File.GetCreationTime(OperationID)
                });
            }
        }

        async void onHistoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage( new HistoryPage())
            {
                BindingContext = new calculation()
            });
        }
    }
}
