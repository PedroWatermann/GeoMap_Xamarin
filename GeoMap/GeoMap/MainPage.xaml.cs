using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeoMap
{
    public partial class MainPage : ContentPage
    {
        public async Task mostrarMapa()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            var locationInfo = new Location(location.Latitude, location.Longitude);
            var options = new MapLaunchOptions { Name = "Meu local" };
            await Map.OpenAsync(locationInfo, options);
        }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void btnLocalizacao_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location != null) 
                {
                    lblLatitude.Text = $"Latitude: {location.Latitude}";
                    lblLongitude.Text = $"Longitude: {location.Longitude}";
                }
            }
            catch (FeatureNotSupportedException fns)
            {
                // Trata execessões onde não há suporte para GPS
                await DisplayAlert("Suporte", fns.Message, "OK");
            }
            catch (PermissionException pms)
            {
                // Trata excessões quando o usuário não permitir a utilização do GPS
                await DisplayAlert("Permissão", pms.Message, "OK");
            }
            catch (Exception exc)
            {
                await DisplayAlert("Falha Grave", exc.Message, "OK");
            }
        }

        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            await mostrarMapa();
        }
    }
}
