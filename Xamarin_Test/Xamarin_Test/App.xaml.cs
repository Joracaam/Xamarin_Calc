using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Test.Data;
using System.IO;

namespace Xamarin_Test
{
    public partial class App : Application
    {
        static OperationDatabase database;
        public static string FolderPath { get; private set; }

        public static OperationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new OperationDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Opw.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage( new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
