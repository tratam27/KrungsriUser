using Krungsri.AppUser.Models;
using Krungsri.AppUser.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Krungsri.AppUser.ViewModels
{
    public class MainAppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainAppViewModel()
        {
            scanQr = new Command(ScanQrCodeAsync);
            MerchantName = Preferences.Get("MerchantName", "");
            MerchantBookBank = Preferences.Get("MerchantBookBank", "");
            TopUpSuccessNext = new Command(TopUpSuccessNextt);
            BackHome = new Command(BackHomeFunc);
            ScanQrTopUp = new Command(ScanQrCodeTopUpAsync);
            Balance = Preferences.Get("Balance","");
            PushToTopUpSuccess = new Command(ConfirmTopUp);
            AdminName = Preferences.Get("AdminName", "");
            AdminBookBank = Preferences.Get("AdminBookBank", "");
            TopUpMoney = Preferences.Get("TopUpAmount", "");
            FirstName = Preferences.Get("FirstName", "");
            BookBank = Preferences.Get("BookBank", "");
            Reference = Preferences.Get("Ref", "");
            //UserId = int.Parse( Preferences.Get("UserId", ""));
        }
        public ICommand scanQr { get; set; }//scan merchant
        public ICommand ScanQrTopUp { get; set; }
        public ICommand TopUpSuccessNext { get; set; }
        public ICommand BackHome { get; set; }
        public ICommand PushToTopUpSuccess { get; set; }
        private void ConfirmTopUp()
        {
            UpdateUserBalance();
            Application.Current.MainPage.Navigation.PopPopupAsync();
            Application.Current.MainPage.Navigation.PushPopupAsync(new TopUpSuccess());
        }
        private void BackHomeFunc()
        {
            Application.Current.MainPage.Navigation.PopPopupAsync();
            Application.Current.MainPage.Navigation.PushAsync(new MotherTabbed());
        }
        private void TopUpSuccessNextt()
        {
            Application.Current.MainPage.Navigation.PopPopupAsync();
            Application.Current.MainPage.Navigation.PushAsync(new TopUpSlip());
        }
        private async void ScanQrCodeAsync()// Scan merchant
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                QrcodeModel jsonqrcode = JsonConvert.DeserializeObject<QrcodeModel>(result);
                if (result != null)
                {
                    //return result;
                    Preferences.Set("MerchantName", jsonqrcode.Name);
                    Preferences.Set("MerchantBookBank", jsonqrcode.BookBank);
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ScanPopup());// merchant popup
                }
                else
                {
                   // return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async void ScanQrCodeTopUpAsync()//scan top up
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                TopUpModel jsonqrcode = JsonConvert.DeserializeObject<TopUpModel>(result);
                if (result != null)
                {
                    //return result;
                    Preferences.Set("Reference",jsonqrcode.Ref);
                    Preferences.Set("AdminName", jsonqrcode.Name);
                    Preferences.Set("TopUpAmount", jsonqrcode.MoneyAmount.ToString());
                    Preferences.Set("ExpiryDate", jsonqrcode.ExpiryDate);
                    Preferences.Set("AdminBookBank", jsonqrcode.BookBank);
                    Preferences.Set("AdminId", jsonqrcode.AdminId);                    
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new TopUpPopup());
                    
                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        async Task UpdateUserBalance()
        {
            try
            {
                var objToApi = new
                {
                    UserId = Preferences.Get("UserId", ""),
                    TopUpMoney = Preferences.Get("TopUpAmount", ""),
                    Ref = Preferences.Get("Reference", "")
                };

                StringContent request = new StringContent($"{JsonConvert.SerializeObject(objToApi)}", Encoding.UTF8, "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://192.168.1.34:5000/api/admin/updateuseridandbalance", request);
                //response.EnsureSuccessStatusCode();                    
                string responseBody = await response.Content.ReadAsStringAsync();
                UpdateUserIdAndBalance balance = JsonConvert.DeserializeObject<UpdateUserIdAndBalance>(responseBody);
                if (balance != null)
                {
                    Preferences.Set("Balance", balance.Balance.ToString());
                    //await Application.Current.MainPage.Navigation.PushAsync(new GenQrCode());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        private int paymentMoney;

        public int PaymentMoney
        {
            get { return paymentMoney; }
            set { paymentMoney = value; }
        }
        public string FirstName { get; set; }
        public string BookBank { get; set; }
        //public decimal Balance { get; set; }
        public string MerchantName { get; set; }
        public string MerchantBookBank { get; set; }
        public string AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminBookBank { get; set; }
        public string ExpiryDate { get; set; }
        public string Reference { get; set; }//top up ref
        public string TopUpMoney { get; set; }
        public int UserId { get; set; }
        private string balance;

        public string Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                //OnPropertyChanged(nameof(Balance));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
