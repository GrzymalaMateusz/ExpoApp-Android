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
using Android.Webkit;

namespace ExpoApp.Droid
{
    [Activity(Label = "Start", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class VideoActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Video);
        }

    }
}

