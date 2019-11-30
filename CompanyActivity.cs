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
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using Android.Webkit;
using ExpoApp.Models;

namespace ExpoApp.Droid
{
    [Activity(Label = "Wystawca", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    class CompanyActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Company);
            ParseAndReturnAsync();
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View popup = layoutInflaterAndroid.Inflate(Resource.Layout.HistoryWindowU, null);
            Android.Support.V7.App.AlertDialog.Builder alertDialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertDialogbuilder.SetView(popup);

            var userContent = popup.FindViewById<EditText>(Resource.Id.History_description);
            alertDialogbuilder.SetCancelable(false)
            .SetPositiveButton("Dodaj", delegate
            {
                Toast.MakeText(this, "Wysłano", ToastLength.Long).Show();
            }
            ).SetNegativeButton("Zamknij", delegate
            {
                alertDialogbuilder.Dispose();
            });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertDialogbuilder.Create();
            if (this.Intent.GetBooleanExtra("Show", true))
            {
                alertDialogAndroid.Show();
            }
        }
        
        private async Task ParseAndReturnAsync()
        {


            // Extract the array of name/value results for the field name "weatherObservation". 
            Models.Company c = await DbConnection.FetchCompanyAsync(this.Intent.GetStringExtra("Email"));
            // ID=Long.ParseLong(jsonR["ID"]),
            TextView CompanyName = FindViewById<TextView>(Resource.Id.company_name);
            CompanyName.Text = c.CompanyName;
            TextView CompanyFullName = FindViewById<TextView>(Resource.Id.company_fullname);
            CompanyFullName.Text = c.CompanyFullName;
            TextView CompanyAbout = FindViewById<TextView>(Resource.Id.company_about);
            CompanyAbout.Text = c.CompanyAbout;
            TextView ProductsAbout = FindViewById<TextView>(Resource.Id.company_product);
            ProductsAbout.Text = c.ProductsAbout;
            TextView ContactEmail = FindViewById<TextView>(Resource.Id.company_email);
            ContactEmail.Text = c.ContactEmail;
            ContactEmail.Click += (s, e) =>
            {
                var uri = Android.Net.Uri.Parse("mailto:" + c.ContactEmail);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };
            TextView Phone = FindViewById<TextView>(Resource.Id.company_phone);
            Phone.Text = c.Phone.ToString();
            TextView Adress = FindViewById<TextView>(Resource.Id.company_adres);
            Adress.Text = c.Adress;
            TextView WWW = FindViewById<TextView>(Resource.Id.company_www);
            WWW.Text =c.www;
            WWW.Click += (s, e) => {
                var uri = Android.Net.Uri.Parse(c.www);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };
            WebView Youtube = FindViewById<WebView>(Resource.Id.company_youtube);
            WebSettings set = Youtube.Settings;
            set.JavaScriptEnabled = true;
            Youtube.SetWebChromeClient(new WebChromeClient());
            Youtube.LoadUrl("https://www.youtube.com/embed/"+ c.Youtube);
            ImageButton fb = FindViewById<ImageButton>(Resource.Id.company_facebook);
            fb.Click += (s, e) => {
                String uri = "fb://facewebmodal/f?href="+c.Facebook;
                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
                StartActivity(intent);
            };
            if (c.Facebook == null)
            {
                fb.Visibility = ViewStates.Invisible;
                fb.SetMaxHeight(0);
            }
            ImageButton insta = FindViewById<ImageButton>(Resource.Id.company_instagram);
            insta.Click += (s, e) => {
                String[] tab=c.Instagram.ToString().Split('/');
                String uri = "instagram://user?username=" + tab[3];
                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
                StartActivity(intent);
            };
            if (c.Instagram == null)
            {
                insta.Visibility = ViewStates.Invisible;
                insta.SetMaxHeight(0);
            }
            ImageView CompanyLogo = FindViewById<ImageView>(Resource.Id.company_logo);
            CompanyLogo.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.CompanyLogo));
            ImageView StandPhoto = FindViewById<ImageView>(Resource.Id.company_standphoto);
            StandPhoto.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.StandPhoto));
            ImageView Photo1 = FindViewById<ImageView>(Resource.Id.company_photo1);
            Photo1.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.Photo1));
            ImageView Photo2 = FindViewById<ImageView>(Resource.Id.company_photo2);
            Photo2.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.Photo2));
            ImageView Photo3 = FindViewById<ImageView>(Resource.Id.company_photo3);
            Photo3.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.Photo3));
            ImageView Photo4 = FindViewById<ImageView>(Resource.Id.company_photo4);
            Photo4.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.Photo4));
            ImageView Photo5 = FindViewById<ImageView>(Resource.Id.company_photo5);
            Photo5.SetImageBitmap(DbConnection.GetImageBitmapFromUrl(this, "http://expotest.somee.com/Images/Company/" + c.Photo5));
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
                
                if (LDbConnection.getUserType() == "Uczestnik")
                {
                    h.Description = userContent.Text;
                    h.User = LDbConnection.GetUser().ID;
                    h.Expo = Java.Lang.Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
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
                }
                else if (LDbConnection.getUserType() == "Wystawca")
                {
                    h.Description = userContent.Text;
                    h.User = LDbConnection.GetCompany().Id;
                    h.Expo = Java.Lang.Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
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
                    h.Expo = Java.Lang.Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
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
                }
                else if (LDbConnection.getUserType() == "Wystawca")
                {
                    h.Description = "Skanowanie Nr: " + LDbConnection.GetHistoryUser().Count;
                    h.User = LDbConnection.GetCompany().Id;
                    h.Expo = Java.Lang.Long.ParseLong(this.Intent.GetIntExtra("expo_id", 1).ToString());
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
    public class MyWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}