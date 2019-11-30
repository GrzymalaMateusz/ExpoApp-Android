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
using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ExpoApp.Droid
{
    [Activity(Label = "Rejestracja", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    public class RegisterActivity : Activity
    {
        EditText email;
        EditText password;
        EditText passwordconfirm;
        RadioButton wystawca;
        RadioButton uczestnik;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Register);
            email = FindViewById<EditText>(Resource.Id.Register_Email);
            password = FindViewById<EditText>(Resource.Id.Register_Password);
            passwordconfirm = FindViewById<EditText>(Resource.Id.Register_Confirm_Password);
            Button button = FindViewById<Button>(Resource.Id.Register1);
            wystawca = FindViewById<RadioButton>(Resource.Id.radioButton2);
            uczestnik = FindViewById<RadioButton>(Resource.Id.radioButton1);
            button.Click += delegate { OnClickAsync(); };
        }
        protected async Task OnClickAsync()
        {
            if (TextUtils.IsEmpty(email.Text) || TextUtils.IsEmpty(password.Text))
            {
                if (TextUtils.IsEmpty(email.Text))
                {
                    email.RequestFocus();
                    email.SetError("Pole nie może być puste!", null);
                    return;
                }
                if (TextUtils.IsEmpty(password.Text))
                {
                    password.RequestFocus();
                    password.SetError("Pole nie może być puste!", null);
                    return;
                }
            }
            else if (!TextUtils.IsEmpty(email.Text) || !TextUtils.IsEmpty(password.Text))
            {
                if (password.Text != passwordconfirm.Text)
                {
                    passwordconfirm.RequestFocus();
                    passwordconfirm.SetError("Hasła są niezgodne!", null);
                    password.RequestFocus();
                    password.SetError("Hasła są niezgodne!", null);
                    return;
                }
                else
                {
                    String userType = null;
                    if (uczestnik.Checked)
                    {
                        userType = "User";
                    }
                    if (wystawca.Checked)
                    {
                        userType = "Company";
                    }
                    Models.MobileRegister mr = await DbConnection.FetchRegisterAsync(email.Text, password.Text, userType);
                    if (mr.AccountCreated == true)
                    {
                        var NxtAct = new Intent(this, typeof(EditMyProfileActivity));
                        if (uczestnik.Checked)
                        {
                            NxtAct.PutExtra("Typ", "uczestnik");
                            NxtAct.PutExtra("Email", email.Text);
                            NxtAct.PutExtra("Action", "Register");

                        }
                        else if (wystawca.Checked)
                        {
                            NxtAct.PutExtra("Typ", "wystawca");
                            NxtAct.PutExtra("Email", email.Text);
                            NxtAct.PutExtra("Action", "Register");

                        }
                        StartActivity(NxtAct);
                    }
                    else
                    {
                        if (mr.UnValid == "Email")
                        {
                            Toast.MakeText(this, "Konto o takim Emailu istnieje", ToastLength.Long).Show();
                        }
                    }
                }
            }
            
        }
        
    }
}