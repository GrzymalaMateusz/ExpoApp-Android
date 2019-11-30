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
using Android.Graphics;
using static Android.Graphics.Bitmap;

namespace ExpoApp.Droid
{
    [Activity(Label = "Expo", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class MyExposExpoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MyExposExpo);
            
            TextView ExpoName = FindViewById<TextView>(Resource.Id.myexposexpo_name);
            ExpoName.Text = this.Intent.GetStringExtra("name");
            TextView ProductsAbout = FindViewById<TextView>(Resource.Id.myexposexpo_adres);
            ProductsAbout.Text = this.Intent.GetStringExtra("adres");
            TextView ExpoDescription = FindViewById<TextView>(Resource.Id.myexposexpo_description);
            ExpoDescription.Text = this.Intent.GetStringExtra("opis");
            TextView ExpoStart = FindViewById<TextView>(Resource.Id.myexposexpo_data_start);
            ExpoStart.Text = this.Intent.GetStringExtra("DS");
            TextView ExpoEnd = FindViewById<TextView>(Resource.Id.myexposexpo_data_end);
            ExpoEnd.Text = this.Intent.GetStringExtra("DE");
            ImageView ExpoPhoto = FindViewById<ImageView>(Resource.Id.myexposexpo_image);
            ExpoPhoto.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Expo/" + this.Intent.GetStringExtra("photo")));

           
        }
    }
}