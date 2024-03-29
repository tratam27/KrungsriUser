﻿using Android.Preferences;
using Krungsri.AppUser.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krungsri.AppUser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new EnterPin());
            MainPage = new NavigationPage(new UserLogin());
            //MainPage = new NavigationPage(new MotherTabbed());
            //MainPage = new NavigationPage(new UserProfile());
            //MainPage = new NavigationPage(new Register());
        }

        protected override void OnStart()
        {
            Preferences.Clear();
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
