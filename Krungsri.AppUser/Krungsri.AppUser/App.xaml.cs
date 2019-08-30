using Krungsri.AppUser.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krungsri.AppUser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new EnterPin());
            //MainPage = new NavigationPage(new UserLogin());
            //MainPage = new NavigationPage(new MotherTabbed());
            MainPage = new NavigationPage(new UserProfile());
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
