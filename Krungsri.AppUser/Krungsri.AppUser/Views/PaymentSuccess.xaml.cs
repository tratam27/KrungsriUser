﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krungsri.AppUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSuccess : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PaymentSuccess()
        {
            InitializeComponent();
        }
    }
}