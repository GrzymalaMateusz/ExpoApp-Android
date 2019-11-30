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
using ExpoApp.Models;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;

namespace ExpoApp.Droid
{
    [Activity(Label = "ExpoApp", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    class EventsActivity : Activity
    {
        ListView list;
        List<Event> events;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Expos);
            list = FindViewById<ListView>(Resource.Id.EventlistView);
            ParseandsetAsync();

        }
        private async Task ParseandsetAsync()
        {
            events = await DbConnection.FetchEventsAsync();
            EventListViewAdapter adapter = new EventListViewAdapter(events, this);
            list.SetAdapter(adapter);
            list.ItemClick += List_ItemClick;
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var NxtAct = new Intent(this, typeof(EventActivity));
            NxtAct.PutExtra("ename", events[e.Position].Name);
            NxtAct.PutExtra("eid", events[e.Position].Id);
            NxtAct.PutExtra("eopis", events[e.Position].Description);
            NxtAct.PutExtra("eplace", events[e.Position].Place);
            NxtAct.PutExtra("eDS", events[e.Position].StartDate.ToString());
            NxtAct.PutExtra("eDE", events[e.Position].EndDate.ToString());
            StartActivity(NxtAct);
        }

       
    }
}