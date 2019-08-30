using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Krungsri.AppUser.ViewModels
{
    public class PinViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PinViewModel()
        {
            EnterNumber = new Command(EnterNumberFunc);
            DeleteNumber = new Command(DeleteNumberFunc);
            FingerScan = new Command(async => FingerScanAsync());
        }
        public ICommand EnterNumber { get; set; }
        public ICommand DeleteNumber { get; set; }
        public ICommand FingerScan { get; set; }
        public async System.Threading.Tasks.Task FingerScanAsync()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers!");
            if (result.Authenticated)
            {
                await Application.Current.MainPage.DisplayAlert("Result", "Success", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Result", "NotSuccess", "OK");
                //Process.GetCurrentProcess().CloseMainWindow();
            }
        }
        public void EnterNumberFunc()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
            }
            else if(CountPin > 6)
            {
                CountPin = 6;
            }
        }
        public void DeleteNumberFunc()
        {
            CountPin--;
            if (CountPin == 0)
            {
                CircleBg1 = "#FFFFFF";
            }
            else if (CountPin == 1)
            {
                CircleBg2 = "#FFFFFF";
            }
            else if (CountPin == 2)
            {
                CircleBg3 = "#FFFFFF";
            }
            else if (CountPin == 3)
            {
                CircleBg4 = "#FFFFFF";
            }
            else if (CountPin == 4)
            {
                CircleBg5 = "#FFFFFF";
            }
            else if (CountPin == 5)
            {
                CircleBg6 = "#FFFFFF";
            }
            else if (CountPin < 0)
            {
                CountPin = 0;
            }
        }
        private int countPin = 0;

        public int CountPin
        {
            get { return countPin; }
            set { countPin = value; }
        }

        private string circleBg1 = "#FFFFFF";
        public string CircleBg1
        {
            get { return circleBg1; }
            set
            {
                circleBg1 = value;
                OnPropertyChanged(nameof(CircleBg1));
            }
        }
        private string circleBg2 = "#FFFFFF";
        public string CircleBg2
        {
            get { return circleBg2; }
            set
            {
                circleBg2 = value;
                OnPropertyChanged(nameof(CircleBg2));
            }
        }
        private string circleBg3 = "#FFFFFF";
        public string CircleBg3
        {
            get { return circleBg3; }
            set
            {
                circleBg3 = value;
                OnPropertyChanged(nameof(CircleBg3));
            }
        }
        private string circleBg4 = "#FFFFFF";
        public string CircleBg4
        {
            get { return circleBg4; }
            set
            {
                circleBg4 = value;
                OnPropertyChanged(nameof(CircleBg4));
            }
        }
        private string circleBg5 = "#FFFFFF";
        public string CircleBg5
        {
            get { return circleBg5; }
            set
            {
                circleBg5 = value;
                OnPropertyChanged(nameof(CircleBg5));
            }
        }
        private string circleBg6 = "#FFFFFF";
        public string CircleBg6
        {
            get { return circleBg6; }
            set
            {
                circleBg6 = value;
                OnPropertyChanged(nameof(CircleBg6));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
