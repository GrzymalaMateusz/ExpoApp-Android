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
using System.Net;
using System.IO;
using ExpoApp.Models;
using Java.Util;

namespace ExpoApp.Droid
{
    [Activity(Label = "ExpoApp", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    class MyExposActivity : Activity
    {
        ListView list;
        List<Expo> expos;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MyExpos);
            list = FindViewById<ListView>(Resource.Id.listView1);
            parseandsetAsync();
            list.ItemClick += List_ItemClick;
        }
        private async Task parseandsetAsync()
        {
            expos = LDbConnection.GetUserExpo();
            MyExpoListViewAdapter adapter = new MyExpoListViewAdapter(expos, this);
            list.SetAdapter(adapter);
            
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var NxtAct = new Android.Content.Intent(this, typeof(MyExposExpoActivity));
            NxtAct.PutExtra("name", expos[e.Position].Name_Expo);
            NxtAct.PutExtra("id", expos[e.Position].Id);
            NxtAct.PutExtra("adres", expos[e.Position].Adres);
            NxtAct.PutExtra("opis", expos[e.Position].Description);
            NxtAct.PutExtra("photo", expos[e.Position].Photo);
            NxtAct.PutExtra("DS", expos[e.Position].DataTargowStart.ToString());
            NxtAct.PutExtra("DE", expos[e.Position].DataTargowEnd.ToString());
            StartActivity(NxtAct);
        }


    }
}