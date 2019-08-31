using Krungsri.AppUser.Models;
using Krungsri.AppUser.Views;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Krungsri.AppUser.ViewModels
{
    public class PinViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string[] PinArr = new string[6];
        public PinViewModel()
        {
            EnterNo0 = new Command(EnterNumberZero);
            EnterNo1 = new Command(EnterNumberOne);
            EnterNo2 = new Command(EnterNumberTwo);
            EnterNo3 = new Command(EnterNumberThree);
            EnterNo4 = new Command(EnterNumberFour);
            EnterNo5 = new Command(EnterNumberFive);
            EnterNo6 = new Command(EnterNumberSix);
            EnterNo7 = new Command(EnterNumberSeven);
            EnterNo8 = new Command(EnterNumberEight);
            EnterNo9 = new Command(EnterNumberNine);
            DeleteNumber = new Command(DeleteNumberFunc);
            FingerScan = new Command(async => FingerScanAsync());
            SendOtpCommand = new Command(async => SendOtpEmail());
            SignIn = new Command(async => SignInFunc());
            SetGenderM = new Command(SetGenderMen);
            SetGenderW = new Command(SetGenderWomen);
            RegisterNextCommand = new Command(RegisterNextButton);
        }
        public ICommand RegisterNextCommand { get; set; }
        public ICommand EnterNo0 { get; set; }
        public ICommand EnterNo1 { get; set; }
        public ICommand EnterNo2 { get; set; }
        public ICommand EnterNo3 { get; set; }
        public ICommand EnterNo4 { get; set; }
        public ICommand EnterNo5 { get; set; }
        public ICommand EnterNo6 { get; set; }
        public ICommand EnterNo7 { get; set; }
        public ICommand EnterNo8 { get; set; }
        public ICommand EnterNo9 { get; set; }
        public ICommand DeleteNumber { get; set; }
        public ICommand FingerScan { get; set; }
        public ICommand SendOtpCommand { get; set; }
        public ICommand SignIn { get; set; }
        public ICommand SetGenderM { get; set; }
        public ICommand SetGenderW { get; set; }
        //async Task Login()
        //{
        //    try
        //    {
        //        //var obj = new { Email = Email, Password = Password };
        //        //var obj1 = new { Email , Password };
        //
        //        string json = "{ Email: '" + Email + "', Password: '" + Password + "'}";
        //        var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
        //        var client = new HttpClient();
        //        HttpResponseMessage response = await client.PostAsync("http://10.0.2.2:5000/api/auth/login", requestBody);
        //        //response.EnsureSuccessStatusCode();
        //        string responseBody = response.Content.ReadAsStringAsync().Result;
        //        LoginModel login = JsonConvert.DeserializeObject<LoginModel>(responseBody);
        //        if (login.Token != null)
        //        {
        //            Application.Current.MainPage = new HomeTabbedPage();//กด back ออกแอป
        //        }
        //        else
        //        {
        //            InvalidString();
        //        }
        //    }
        //    catch (Exception g)
        //    {
        //        InvalidString();
        //    }
        //}
        public async void SignInFunc()
        {
            Preferences.Set("PinPush", "1");
            await SendOtpEmail();
            await Application.Current.MainPage.Navigation.PushAsync(new InputOtp(this));
        }
        public async void PinPush()
        {
            var FormerPage = Preferences.Get("PinPush", "1");
            if(FormerPage == "1")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Register());
            }
            else if(FormerPage == "2")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ConfirmCreatePassword());
            }
            else if(FormerPage == "3")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterSuccess());
            }
        }
        async Task SendOtpEmail()
        {
            try
            {
                string json = "{ Email: '" + Email + "'}";
                var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://192.168.1.34:5000/api/auth/sendotp", requestBody);
                //response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();                
                SendOtpModel otpModel = JsonConvert.DeserializeObject<SendOtpModel>(responseBody);
                await SecureStorage.SetAsync("otp", otpModel.Otp);
                await SecureStorage.SetAsync("ref", otpModel.Ref);
                Preferences.Set("otpemail", otpModel.Email);
                Reference = await SecureStorage.GetAsync("ref");
            }
            catch (Exception ex)
            {

            }
        }
        async Task SignUp()
        {
            try
            {
                var validemail = IsValidEmail(Email);
                var validpassword = PasswordSame(Password, RepeatPassword);
                if (validemail == true && validpassword == true)
                {
                    string json = "{ Email: '" + Email + "', Password: '" + Password + "'}";
                    var requestBody = new StringContent(json, Encoding.UTF8, "application/json");
                    var client = new HttpClient();
                    HttpResponseMessage response = client.PostAsync("http://10.0.2.2:5000/api/user/registation", requestBody).Result;
                    //response.EnsureSuccessStatusCode();                    
                    var responseBody = response.StatusCode.ToString();
                    //SendEmailModel sendEmail = JsonConvert.DeserializeObject<SendEmailModel>(responseBody);
                    if (responseBody == "OK")
                    {
                        
                    }
                }
                else
                {
                    
                }
            }
            catch
            {
                
            }
        }
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
        public async void EnterNumberOne()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "1";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "1";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "1";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "1";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "1";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "1";
                
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);
                    PinPush();
                    Preferences.Set("PinPush", "3");
                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if(RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }                
            }
        }
        public async void EnterNumberTwo()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "2";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "2";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "2";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "2";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "2";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "2";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);                    
                    PinPush();
                    Preferences.Set("PinPush", "3");
                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberThree()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "3";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "3";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "3";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "3";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "3";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "3";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);
                    PinPush();
                    Preferences.Set("PinPush", "3");
                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberFour()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "4";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "4";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "4";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "4";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "4";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "4";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);
                    PinPush();
                    Preferences.Set("PinPush", "3");

                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberFive()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "5";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "5";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "5";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "5";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "5";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "5";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);

                    PinPush();
                    Preferences.Set("PinPush", "3");

                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberSix()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "6";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "6";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "6";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "6";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "6";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "6";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);

                    PinPush();
                    Preferences.Set("PinPush", "3");

                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberSeven()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "7";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "7";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "7";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "7";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "7";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "7";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);

                    PinPush();
                    Preferences.Set("PinPush", "3");

                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberEight()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "8";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "8";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "8";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "8";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "8";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "8";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);
                    PinPush();
                    Preferences.Set("PinPush", "3");

                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberNine()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "9";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "9";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "9";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "9";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "9";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "9";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);

                    PinPush();
                    Preferences.Set("PinPush", "3");

                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();

                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public async void EnterNumberZero()
        {
            CountPin++;
            if (CountPin == 1)
            {
                CircleBg1 = "#B8B8B8";
                PinArr[0] = "0";
            }
            else if (CountPin == 2)
            {
                CircleBg2 = "#B8B8B8";
                PinArr[1] = "0";
            }
            else if (CountPin == 3)
            {
                CircleBg3 = "#B8B8B8";
                PinArr[2] = "0";
            }
            else if (CountPin == 4)
            {
                CircleBg4 = "#B8B8B8";
                PinArr[3] = "0";
            }
            else if (CountPin == 5)
            {
                CircleBg5 = "#B8B8B8";
                PinArr[4] = "0";
            }
            else if (CountPin == 6)
            {
                CircleBg6 = "#B8B8B8";
                PinArr[5] = "0";
                var FormerPage = Preferences.Get("PinPush", "1");
                if (FormerPage == "1")
                {
                    var otp = await SecureStorage.GetAsync("otp");
                    if (otp == ChangeArrToString())
                    {                        
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
                else if (FormerPage == "2")
                {
                    Password = ChangeArrToString();
                    Preferences.Set("Password", Password);
                    PinPush();
                    Preferences.Set("PinPush", "3");
                }
                else if (FormerPage == "3")
                {
                    RepeatPassword = ChangeArrToString();
                    if (RepeatPassword == Preferences.Get("Password", ""))
                    {
                        PinPush();
                    }
                    else
                    {
                        Vibration.Vibrate();
                        ChangeInvalidColor();
                    }
                }
            }
        }
        public void DeleteNumberFunc()
        {
            CountPin--;
            if (CountPin == 0)
            {
                CircleBg1 = "#FFFFFF";
                PinArr[0] = "";
            }
            else if (CountPin == 1)
            {
                CircleBg2 = "#FFFFFF";
                PinArr[1] = "";
            }
            else if (CountPin == 2)
            {
                CircleBg3 = "#FFFFFF";
                PinArr[2] = "";
            }
            else if (CountPin == 3)
            {
                CircleBg4 = "#FFFFFF";
                PinArr[3] = "";
            }
            else if (CountPin == 4)
            {
                CircleBg5 = "#FFFFFF";
                PinArr[4] = "";
            }
            else if (CountPin == 5)
            {
                CircleBg6 = "#FFFFFF";
                PinArr[5] = "";
            }
            else if (CountPin < 0)
            {
                CountPin = 0;
            }
        }
        public void ChangeInvalidColor()
        {
            InvalidMessageColor = "Red";
        }
        private string ChangeArrToString()
        {
            return string.Join("", PinArr);
        }
        public bool PasswordSame(string password, string repeatPassword)
        {
            if (password == repeatPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void SetGenderMen()
        {
            Gender = "Men";
        }
        private void SetGenderWomen()
        {
            Gender = "Women";
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private string invalidMessageColor = "#FFFFFF";

        public string InvalidMessageColor
        {
            get { return invalidMessageColor; }
            set
            {
                invalidMessageColor = value;
                OnPropertyChanged(nameof(InvalidMessageColor));
            }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string repeatPassword;

        public string RepeatPassword
        {
            get { return repeatPassword; }
            set { repeatPassword = value; }
        }
        private string reference;

        public string Reference
        {
            get { return reference; }
            set
            {
                reference = value;
                OnPropertyChanged(nameof(Reference));
            }
        }
        public string EmailInPhone { get; set; } = Preferences.Get("otpemail", "");
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }

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
        private void RegisterNextButton()
        {
            Preferences.Set("FirstName", FirstName);
            Preferences.Set("LastName", LastName);
            Preferences.Set("BirthDate", BirthDate);
            Preferences.Set("PhoneNo", PhoneNo);
            Preferences.Set("Gender", Gender);
            Preferences.Set("PinPush", "2");
            Application.Current.MainPage.Navigation.PushAsync(new CreatePassword());
        }

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
