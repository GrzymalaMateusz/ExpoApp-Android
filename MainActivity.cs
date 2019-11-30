using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using ExpoApp.Models;
using Android.Database.Sqlite;
using SQLite;

namespace ExpoApp.Droid
{
	[Activity (Label = "ExpoApp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal).ToString(), "database.sqlite")))
            {
                if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Login'")==0)
                {
                    conn.CreateTable<Login>();
                    conn.Commit();
                }
                else
                {
                    if (conn.ExecuteScalar<int>("Select Count(*) From Login") != 0)
                    {
                        var NxtAct = new Intent(this, typeof(StartActivity));
                        StartActivity(NxtAct);
                    }
                }
                conn.Close();
            }
            Button button = FindViewById<Button> (Resource.Id.Login);
            button.Click +=delegate {
                var NxtAct = new Intent(this, typeof(LoginActivity));
                StartActivity(NxtAct);
            };
            Button button1 = FindViewById<Button>(Resource.Id.Register);
            button1.Click += delegate {
                var NxtAct = new Intent(this, typeof(RegisterActivity));
                StartActivity(NxtAct);
            };
        }
}
}


