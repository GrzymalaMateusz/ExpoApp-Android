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
using Android.Text;
using System.IO;
using ExpoApp.Models;
using System.Threading.Tasks;
using System.Net;
using System.Json;
using Java.Lang;

namespace ExpoApp.Droid
{
    [Activity(Label = "Logowanie", MainLauncher = false, Icon = "@drawable/icon",Theme = "@style/Theme.DesignDemo")]
    class LoginActivity :Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);
            
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += async delegate { OnClickAsync(); };
        }
        protected async void OnClickAsync()
        {
            TextView email = FindViewById<TextView>(Resource.Id.Email);
            TextView password = FindViewById<TextView>(Resource.Id.Password);
            MobileLogin ml = await DbConnection.FetchLoginAsync(email.Text, password.Text);
            if (ml.UserAndPasswordCorrect == true)
            {
                Login l = new Login
                {
                    Name = email.Text,
                    Password = password.Text,
                    Typ = ml.UserType
                };
                LDbConnection.InsertLogin(l);
                if (ml.UserType == "User")
                {
                    LDbConnection.InsertUser(this, await DbConnection.FetchUserAsync(email.Text));
                    LDbConnection.InsertUserExpos(await DbConnection.FetchUserExposAsync(LDbConnection.GetUser()));
                    //LDbConnection.InsertHistory(await DbConnection.FetchHistory(LDbConnection.GetUser()));

                }
                if (ml.UserType == "Company")
                {
                    LDbConnection.InsertCompany(this, await DbConnection.FetchCompanyAsync(email.Text));
                    LDbConnection.InsertUserExpos(await DbConnection.FetchUserExposAsync(LDbConnection.GetCompany()));
                    //LDbConnection.InsertHistory(await DbConnection.FetchHistory(LDbConnection.GetCompany()));
                }
                var NxtAct = new Intent(this, typeof(StartActivity));
                StartActivity(NxtAct);
                Finish();
            }
            else
            {
                if (TextUtils.IsEmpty(email.Text) || TextUtils.IsEmpty(password.Text))
                {
                    if (TextUtils.IsEmpty(email.Text))
                    {
                        email.RequestFocus();
                        email.SetError("Pole nie może być puste!", null);
                    }
                    if (TextUtils.IsEmpty(password.Text))
                    {
                        password.RequestFocus();
                        password.SetError("Pole nie może być puste!", null);
                    }
                }
                else if (ml.UserAndPasswordCorrect==false)
                {
                        email.RequestFocus();
                        email.SetError("Zły Email lub Hasło!", null);
                        password.RequestFocus();
                        password.SetError("Zły Email lub Hasło!", null);
                }
                return;
            }
        }
       
        
    }
}