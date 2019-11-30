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
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Java.Lang;
using ExpoApp.Models;
using Android.Views.InputMethods;

namespace ExpoApp.Droid
{
    [Activity(Label = "Uczestnik", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class UserActivity :Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.User);
            LoadDAtaAsync();



        }
        protected async void LoadDAtaAsync()
        {
            User u = await DbConnection.FetchUserAsync(this.Intent.GetStringExtra("Email"));
            TextView ForName = FindViewById<TextView>(Resource.Id.user_forname);
            ForName.Text = u.ForName;
            TextView SurName = FindViewById<TextView>(Resource.Id.user_surname);
            SurName.Text = u.SurName;
            TextView Email = FindViewById<TextView>(Resource.Id.user_email);
            Email.Text = u.Email;
            TextView Phone = FindViewById<TextView>(Resource.Id.user_phone);
            Phone.Text = u.Phone.ToString();
            TextView Nationality = FindViewById<TextView>(Resource.Id.user_Nationality);
            Nationality.Text = u.Nationality;
            
            ImageView photo = FindViewById<ImageView>(Resource.Id.user_photo);
            photo.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/User/" + u.Photo));
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View popup = layoutInflaterAndroid.Inflate(Resource.Layout.HistoryWindowU, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogbuilder.SetView(popup);

            var userContent = popup.FindViewById<EditText>(Resource.Id.History_description);
            alertDialogbuilder.SetCancelable(false)
            .SetPositiveButton("Dodaj", async delegate
            {
                HistoryU h = new HistoryU();
                //h.ID = LDbConnection.GetHistoryUser(this).Count;
               
                if(LDbConnection.getUserType() == "Uczestnik")
                {
                    h.Description = userContent.Text;
                    h.User = LDbConnection.GetUser().ID;
                    h.Expo = Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
                    h.Wyszukiwanie = this.Intent.GetStringExtra("Search");
                    LDbConnection.InsertHistoryU(h);
                    if (await DbConnection.FetchUserHistoryAsync(h) == true)
                    {
                        Android.Widget.Toast.MakeText(this, "Zapisano w bazie", Android.Widget.ToastLength.Short).Show();
                    }
                    else
                    {
                        Android.Widget.Toast.MakeText(this, "SendError", Android.Widget.ToastLength.Short).Show();
                    }
                }else if (LDbConnection.getUserType() == "Wystawca")
                {
                    h.Description = userContent.Text;
                    h.User = LDbConnection.GetCompany().Id;
                    h.Expo = Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
                    h.Wyszukiwanie = this.Intent.GetStringExtra("Search");
                    LDbConnection.InsertHistoryU(h);
                    if (await DbConnection.FetchCompanyHistoryAsync(h) == true)
                    {
                        Android.Widget.Toast.MakeText(this, "Zapisano w bazie", Android.Widget.ToastLength.Short).Show();
                    }
                    else
                    {
                        Android.Widget.Toast.MakeText(this, "SendError", Android.Widget.ToastLength.Short).Show();
                    }
                }
                Toast.MakeText(this, "Wysłano", ToastLength.Long).Show();
            }
            ).SetNegativeButton("Zamknij", async delegate
            {
                HistoryU h = new HistoryU();
                //h.ID = LDbConnection.GetHistoryUser(this).Count;
                
                if (LDbConnection.getUserType() == "Uczestnik")
                {
                    h.Description = "Skanowanie Nr: " + LDbConnection.GetHistoryUser().Count;
                    h.User = LDbConnection.GetUser().ID;
                    h.Expo = Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
                    h.Wyszukiwanie = this.Intent.GetStringExtra("Search");
                    LDbConnection.InsertHistoryU(h);
                    if (await DbConnection.FetchUserHistoryAsync(h) == true)
                    {
                        Android.Widget.Toast.MakeText(this, "Zapisano w bazie", Android.Widget.ToastLength.Short).Show();
                    }
                    else
                    {
                        Android.Widget.Toast.MakeText(this, "SendError", Android.Widget.ToastLength.Short).Show();
                    }
                }else if (LDbConnection.getUserType() == "Wystawca")
                {
                    h.Description = "Skanowanie Nr: " + LDbConnection.GetHistoryUser().Count;
                    h.User = LDbConnection.GetCompany().Id;
                    h.Expo = Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
                    h.Wyszukiwanie = this.Intent.GetStringExtra("Search");
                    LDbConnection.InsertHistoryU(h);
                    if (await DbConnection.FetchCompanyHistoryAsync(h) == true)
                    {
                        Android.Widget.Toast.MakeText(this, "Zapisano w bazie", Android.Widget.ToastLength.Short).Show();
                    }
                    else
                    {
                        Android.Widget.Toast.MakeText(this, "SendError", Android.Widget.ToastLength.Short).Show();
                    }
                }
                    alertDialogbuilder.Dispose();
            });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogbuilder.Create();
            if (this.Intent.GetBooleanExtra("Show", true))
            {
                alertDialogAndroid.Show();
            }
        }

    }
}  