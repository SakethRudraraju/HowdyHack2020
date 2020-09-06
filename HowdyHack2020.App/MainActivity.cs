using Android.App;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Map = Esri.ArcGISRuntime.Mapping.Map;
using HowdyHack2020.Core;

namespace HowdyHack2020.App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;
        MapView mainMapView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);
            mainMapView = FindViewById<MapView>(Resource.Id.mainMapView);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            InitHome();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            ResetHome();

            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    InitHome();
                    return true;
                case Resource.Id.navigation_dashboard:
                    textMessage.SetText(Resource.String.title_dashboard);
                    return true;
                //case Resource.Id.navigation_notifications:
                //    textMessage.SetText(Resource.String.title_notifications);
                //    return true;
            }
            return false;
        }

        #region Home
        Graphic UserPoint;
        System.Threading.Timer HomeTimer;

        private async void InitHome()
        {
            HomeTimer = new System.Threading.Timer(UpdateUserLocation, null, 0, 1000);

            AppDataManager.ResetDeviceId();
            string deviceId = await AppDataManager.EnsureAndGetDeviceId();

            var loc = await GetLocation();
            Status dist = await Api.CheckNearby(loc.Latitude, loc.Longitude, deviceId);
            textMessage.SetText($"You're {10:0.#} miles away", TextView.BufferType.Normal);
            LoadMap(loc.Latitude, loc.Longitude);
            //textMessage.SetText(Resource.String.title_home);
        }

        private void ResetHome()
        {
            HomeTimer.Dispose();
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
            mainMapView.GraphicsOverlays = new GraphicsOverlayCollection()
            {
                MapGraphics
            };
            mainMapView.Map = new Map(
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
            mainMapView.Map.OperationalLayers.Add(buildingsALayer);
            var buildingsBUri = new Uri("https://gis.tamu.edu/arcgis/rest/services/FCOR/TAMU_BaseMap/MapServer/3");
            var buildingsBLayer = new FeatureLayer(new ServiceFeatureTable(buildingsBUri));
            mainMapView.Map.OperationalLayers.Add(buildingsBLayer);
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
            //mainMapView.GraphicsOverlays[0].Graphics.Remove(UserPoint);

            UserPoint = DrawPoint(
                loc.Latitude, loc.Longitude,
                System.Drawing.Color.Red
            );

            //MapGraphics.Graphics.Add(stopPoint);
        }
		#endregion
	}
}

