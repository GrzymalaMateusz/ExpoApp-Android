using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Java.Util;

namespace ExpoApp.Droid
{
    [Activity(Label = "Expo", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class ExpoActivity :Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Expo);
            ParseAndReturnAsync();
        }

        private async Task ParseAndReturnAsync()
        {
            Models.Expo e = new Models.Expo();
            e.Id = this.Intent.GetIntExtra("eid", 0);
            e.Name_Expo = this.Intent.GetStringExtra("ename");
            e.Photo = this.Intent.GetStringExtra("ephoto");
            e.MapPhoto = this.Intent.GetStringExtra("emapphoto");
            e.Description = this.Intent.GetStringExtra("eopis");
            e.DataTargowStart = DateTime.Parse(this.Intent.GetStringExtra("eDS"));
            e.DataTargowEnd = DateTime.Parse(this.Intent.GetStringExtra("eDE"));
            e.Adres = this.Intent.GetStringExtra("eadres");
            TextView ExpoName = FindViewById<TextView>(Resource.Id.expo_name);
            ExpoName.Text = e.Name_Expo;
            TextView ProductsAbout = FindViewById<TextView>(Resource.Id.expo_adres);
            ProductsAbout.Text = e.Adres;
            TextView ExpoDescription = FindViewById<TextView>(Resource.Id.expo_description);
            ExpoDescription.Text = e.Description;
            TextView ExpoStart = FindViewById<TextView>(Resource.Id.expo_data_start);
            ExpoStart.Text = e.DataTargowStart.ToString();
            TextView ExpoEnd = FindViewById<TextView>(Resource.Id.expo_data_end);
            ExpoEnd.Text = e.DataTargowEnd.ToString();
            ImageView ExpoPhoto = FindViewById<ImageView>(Resource.Id.expo_image);
            ExpoPhoto.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Expo/" + e.Photo));
            Button btn = FindViewById<Button>(Resource.Id.expo_join);
            btn.Click += async delegate
            {
                LDbConnection.InsertUExpo(e);
                if (LDbConnection.getUserType() == "Uczestnik")
                {
                    if (await DbConnection.FetchUserJoinExpoAsync(LDbConnection.GetUser(), e))
                    {
                        Toast.MakeText(this, "Dołączono", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Brak połączenia", ToastLength.Short).Show();
                    }
                }
                else if (LDbConnection.getUserType() == "Wystawca")
                {
                    if (await DbConnection.FetchCompanyJoinExpoAsync(LDbConnection.GetCompany(), e))
                    {
                        Toast.MakeText(this, "Dołączono", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Brak połączenia", ToastLength.Short).Show();
                    }
                }


            };
            Button btn1 = FindViewById<Button>(Resource.Id.ExpoMap);
            btn1.Click += async delegate
            {
                var NxtAct = new Intent(this, typeof(MapActivity));
                NxtAct.PutExtra("mapphoto", e.MapPhoto);

                StartActivity(NxtAct);
            };
        }
    }
}