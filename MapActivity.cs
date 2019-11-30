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

namespace ExpoApp.Droid
{
    [Activity(Label = "Expo", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class MapActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Map);
            ImageView iv = FindViewById<ImageView>(Resource.Id.Map);
                            iv.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Expo/" + this.Intent.GetStringExtra("mapphoto")));

        }
    }
}