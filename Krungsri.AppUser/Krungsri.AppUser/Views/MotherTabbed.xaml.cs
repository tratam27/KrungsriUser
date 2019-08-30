using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Krungsri.AppUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MotherTabbed : Xamarin.Forms.TabbedPage
    {
        public MotherTabbed()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}