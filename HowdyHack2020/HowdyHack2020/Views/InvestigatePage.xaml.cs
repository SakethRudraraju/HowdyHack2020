using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Map = Esri.ArcGISRuntime.Mapping.Map;
using HowdyHack2020.Core;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace HowdyHack2020.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InvestigatePage : ContentPage
	{
        Graphic UserPoint;
        System.Threading.Timer HomeTimer;

        public InvestigatePage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

            var loc = await GetLocation();
            LoadMap(loc.Latitude, loc.Longitude);

            HomeTimer = new System.Threading.Timer(UpdateUserLocation, null, 0, 5000);
        }

        protected override void OnDisappearing()
		{
			base.OnDisappearing();
            
            //HomeTimer?.Dispose();
		}

		public async Task<Location> GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    return location;
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                return null;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                return null;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                return null;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                return null;
            }
        }

        public void LoadMap(double lat, double lon)
        {
            var MapGraphics = new GraphicsOverlay();
            MainMapView.GraphicsOverlays = new GraphicsOverlayCollection()
            {
                MapGraphics
            };
            MainMapView.Map = new Map(
                BasemapType.Imagery,
                lat, lon,
                18
            );

            // Draw a point where the user is
            UserPoint = DrawPoint(lat, lon, System.Drawing.Color.Red);
            MapGraphics.Graphics.Add(UserPoint);

            // Display all buildings
            var buildingsAUri = new Uri("https://gis.tamu.edu/arcgis/rest/services/FCOR/TAMU_BaseMap/MapServer/2");
            var buildingsALayer = new FeatureLayer(new ServiceFeatureTable(buildingsAUri));
            MainMapView.Map.OperationalLayers.Add(buildingsALayer);
            var buildingsBUri = new Uri("https://gis.tamu.edu/arcgis/rest/services/FCOR/TAMU_BaseMap/MapServer/3");
            var buildingsBLayer = new FeatureLayer(new ServiceFeatureTable(buildingsBUri));
            MainMapView.Map.OperationalLayers.Add(buildingsBLayer);
        }

        private Graphic DrawPoint(double lat, double lon, System.Drawing.Color fill)
        {
            var mapPoint = new MapPoint(
                Convert.ToDouble(lon), Convert.ToDouble(lat), SpatialReferences.Wgs84
            );
            var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, fill, 10);
            pointSymbol.Outline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.White, 2);
            return new Graphic(mapPoint, pointSymbol);
        }

        private Graphic DrawRadius(double lat, double lon, double radius, System.Drawing.Color fill)
        {
            var mapPoint = new MapPoint(
                Convert.ToDouble(lon), Convert.ToDouble(lat), SpatialReferences.Wgs84
            );
            var circleSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, fill, radius);
            return new Graphic(mapPoint, circleSymbol);
        }

        private async void UpdateUserLocation(object state)
        {
            var loc = await GetLocation();
            UserPoint = DrawPoint(
                loc.Latitude, loc.Longitude,
                System.Drawing.Color.Red
            );
        }
    }
}