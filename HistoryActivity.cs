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
using Android.Support.V7.App;
using ExpoApp.Models;

namespace ExpoApp.Droid
{
    [Activity(Label = "History", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class HistoryActivity : AppCompatActivity
    {
        List<HistoryU> lista;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.History);
            ListView l = (ListView)FindViewById(Resource.Id.HistoryList);
            lista = LDbConnection.GetHistoryUser();
            HistoryAdapter adapter = new HistoryAdapter(lista, this);
            l.SetAdapter(adapter);
            l.ItemClick += List_ItemClick;
        }
        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (lista[e.Position].Wyszukiwanie.Split(':')[0] == "Uczestnik")
            {
                var NxtAct = new Android.Content.Intent(this, typeof(UserActivity));
                NxtAct.PutExtra("Show", false);
                NxtAct.PutExtra("Email", lista[e.Position].Wyszukiwanie.Split(':')[1]);
                NxtAct.PutExtra("expo_id", lista[e.Position].Expo);
                NxtAct.PutExtra("Search", lista[e.Position].Wyszukiwanie);
                StartActivity(NxtAct);
            }else if(lista[e.Position].Wyszukiwanie.Split(':')[0] == "Wystawca")
            {
                var NxtAct1 = new Android.Content.Intent(this, typeof(CompanyActivity));
                NxtAct1.PutExtra("Show", false);
                NxtAct1.PutExtra("Email", lista[e.Position].Wyszukiwanie.Split(':')[1]);
                NxtAct1.PutExtra("expo_id", lista[e.Position].Expo);
                NxtAct1.PutExtra("Search", lista[e.Position].Wyszukiwanie);
                StartActivity(NxtAct1);
            }
        }
    }
}