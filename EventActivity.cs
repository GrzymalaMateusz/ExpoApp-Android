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
    class EventActivity :Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Event);
            ParseAndReturnAsync();
        }

        private async Task ParseAndReturnAsync()
        {
            Models.Event e = new Models.Event();
            e.Id = this.Intent.GetIntExtra("eid", 0);
            e.Name = this.Intent.GetStringExtra("ename");
            e.Description = this.Intent.GetStringExtra("eopis");
            e.Place = this.Intent.GetStringExtra("eplace");
            e.StartDate = DateTime.Parse(this.Intent.GetStringExtra("eDS"));
            e.EndDate = DateTime.Parse(this.Intent.GetStringExtra("eDE"));
            TextView ExpoName = FindViewById<TextView>(Resource.Id.event_name);
            ExpoName.Text = e.Name;
            TextView ProductsAbout = FindViewById<TextView>(Resource.Id.event_place);
            ProductsAbout.Text = e.Place;
            TextView ExpoDescription = FindViewById<TextView>(Resource.Id.event_description);
            ExpoDescription.Text = e.Description;
            TextView ExpoStart = FindViewById<TextView>(Resource.Id.event_data_start);
            ExpoStart.Text = e.StartDate.ToString();
            TextView ExpoEnd = FindViewById<TextView>(Resource.Id.event_data_end);
            ExpoEnd.Text = e.EndDate.ToString();
            
        }
    }
}