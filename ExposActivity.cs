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
    class ExposActivity : Activity
    {
        ListView list;
        List<Expo> expos;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Expos);
            list = FindViewById<ListView>(Resource.Id.ExposlistView);
            parseandsetAsync();

        }
        private async Task parseandsetAsync()
        {
            expos = await DbConnection.FetchExpoAsync();
            ExpoListViewAdapter adapter = new ExpoListViewAdapter(expos, this);
            list.SetAdapter(adapter);
            list.ItemClick += List_ItemClick;
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var NxtAct = new Intent(this, typeof(ExpoActivity));
            NxtAct.PutExtra("ename", expos[e.Position].Name_Expo);
            NxtAct.PutExtra("eid", expos[e.Position].Id);
            NxtAct.PutExtra("eadres", expos[e.Position].Adres);
            NxtAct.PutExtra("eopis", expos[e.Position].Description);
            NxtAct.PutExtra("ephoto", expos[e.Position].Photo);
            NxtAct.PutExtra("emapphoto", expos[e.Position].MapPhoto);
            NxtAct.PutExtra("eDS", expos[e.Position].DataTargowStart.ToString());
            NxtAct.PutExtra("eDE", expos[e.Position].DataTargowEnd.ToString());
            StartActivity(NxtAct);
        }

       
    }
}