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
using System.IO;
using ExpoApp.Models;

namespace ExpoApp.Droid
{
    [Activity(Label = "ExpoApp", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    class MyProfileActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (LDbConnection.GetUser() != null)
            {
                SetContentView(Resource.Layout.MyProfileU);
                User u = LDbConnection.GetUser();
                TextView ForName=FindViewById<TextView>(Resource.Id.myprofu_forname);
                ForName.Text =u.ForName;
                TextView SurName = FindViewById<TextView>(Resource.Id.myprofu_surname);
                SurName.Text = u.SurName;
                TextView Email = FindViewById<TextView>(Resource.Id.myprofu_email);
                Email.Text = u.Email;
                TextView Phone = FindViewById<TextView>(Resource.Id.myprofu_phone);
                Phone.Text = u.Phone.ToString();
                TextView Nationality = FindViewById<TextView>(Resource.Id.myprofu_country);
                Nationality.Text = u.Nationality;
                ImageView photo = FindViewById<ImageView>(Resource.Id.myprofu_photo);
                
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + u.Photo));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                Android.Graphics.Bitmap b = Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + u.Photo));

                photo.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
                Button btn = (Button)FindViewById(Resource.Id.myprofu_edit);
                btn.Click += delegate
                {
                    var NxtAct = new Intent(this, typeof(EditMyProfileActivity)); 
                        NxtAct.PutExtra("Typ", "uczestnik");
                        NxtAct.PutExtra("Email", Email.Text);
                        NxtAct.PutExtra("Action", "Edit");
                    StartActivity(NxtAct);
                };
            }
            if (LDbConnection.GetCompany()!=null)
            {
                SetContentView(Resource.Layout.MyProfileC);
                Company c = LDbConnection.GetCompany();
            
                TextView CompanyName = FindViewById<TextView>(Resource.Id.myprofc_name);
                CompanyName.Text = c.CompanyName;
                TextView CompanyFullName = FindViewById<TextView>(Resource.Id.myprofc_fullname);
                CompanyFullName.Text = c.CompanyFullName;
                TextView CompanyAbout = FindViewById<TextView>(Resource.Id.myprofc_about);
                CompanyAbout.Text = c.CompanyAbout;
                TextView ProductsAbout = FindViewById<TextView>(Resource.Id.myprofc_product);
                ProductsAbout.Text = c.ProductsAbout;
                TextView ContactEmail = FindViewById<TextView>(Resource.Id.myprofc_contactemail);
                ContactEmail.Text = c.ContactEmail;
                ContactEmail.Click += (s, e) =>
                {
                    var uri = Android.Net.Uri.Parse("mailto:" + c.ContactEmail);
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                };
                TextView Phone = FindViewById<TextView>(Resource.Id.myprofc_phone);
                Phone.Text = c.Phone.ToString();
                TextView Adress = FindViewById<TextView>(Resource.Id.myprofc_adres);
                Adress.Text = c.Adress;
                TextView WWW = FindViewById<TextView>(Resource.Id.myprofc_www);
                WWW.Text = c.www;
                WWW.Click += (s, e) => {
                    var uri = Android.Net.Uri.Parse(c.www);
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                };
                TextView privateEmail = FindViewById<TextView>(Resource.Id.myprofc_email);
                privateEmail.Text = c.Email;
                TextView privatePhone = FindViewById<TextView>(Resource.Id.myprofc_userphone);
                privatePhone.Text = c.UserPhone.ToString();
                Android.Webkit.WebView Youtube = FindViewById<Android.Webkit.WebView>(Resource.Id.myprofc_youtube);
                Android.Webkit.WebSettings set = Youtube.Settings;
                set.JavaScriptEnabled = true;
                Youtube.SetWebChromeClient(new Android.Webkit.WebChromeClient());
                Youtube.LoadUrl("https://www.youtube.com/embed/" + c.Youtube);
                ImageButton fb = FindViewById<ImageButton>(Resource.Id.myprofc_facebook);
                fb.Click += (s, e) => {
                    String uri = "fb://facewebmodal/f?href=" + c.Facebook;
                    Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
                    StartActivity(intent);
                };
                if (c.Facebook == null)
                {
                    fb.Visibility = ViewStates.Invisible;
                    fb.SetMaxHeight(0);
                }
                ImageButton insta = FindViewById<ImageButton>(Resource.Id.myprofc_instagram);
                insta.Click += (s, e) => {
                    String[] tab = c.Instagram.ToString().Split('/');
                    String uri = "instagram://user?username=" + tab[3];
                    Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
                    StartActivity(intent);
                };
                if (c.Instagram == null)
                {
                    insta.Visibility = ViewStates.Invisible;
                    insta.SetMaxHeight(0);
                }
                ImageView CompanyLogo = FindViewById<ImageView>(Resource.Id.myprofc_logo);
                String u = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.CompanyLogo);
                CompanyLogo.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.CompanyLogo)));
                ImageView StandPhoto = FindViewById<ImageView>(Resource.Id.myprofc_standphoto);
                StandPhoto.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.StandPhoto)));
                ImageView Photo1 = FindViewById<ImageView>(Resource.Id.myprofc_photo1);
                Photo1.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.Photo1)));
                ImageView Photo2 = FindViewById<ImageView>(Resource.Id.myprofc_photo2);
                Photo2.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.Photo2)));
                ImageView Photo3 = FindViewById<ImageView>(Resource.Id.myprofc_photo3);
                Photo3.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.Photo3)));
                ImageView Photo4 = FindViewById<ImageView>(Resource.Id.myprofc_photo4);
                Photo4.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.Photo4)));
                ImageView Photo5 = FindViewById<ImageView>(Resource.Id.myprofc_photo5);
                Photo5.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeFile(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + c.Photo5)));
                Button btn = (Button)FindViewById(Resource.Id.myprofc_edit);
                btn.Click += delegate
                {
                    var NxtAct = new Intent(this, typeof(EditMyProfileActivity));
                    NxtAct.PutExtra("Typ", "wystawca");
                    NxtAct.PutExtra("Email", privateEmail.Text);
                    NxtAct.PutExtra("Action", "Edit");
                    StartActivity(NxtAct);
                };
            }
        }     
    }
    public class MyWebViewClient1 : Android.Webkit.WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}