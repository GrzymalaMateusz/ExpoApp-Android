

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using ExpoApp.Models;
using System.IO;
using System.Reflection.Emit;
using static Android.Content.Res.Resources;
using static Android.Graphics.Bitmap;
using Android.Widget;
using System;

namespace ExpoApp.Droid
{
    [Activity(Label = "Start", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    class StartActivity :AppCompatActivity
    {
        Android.Support.V4.Widget.DrawerLayout drawerLayout;
        Android.Support.Design.Widget.NavigationView navigationView;
        long doublePressInterval_ms = 300;
        DateTime lastPressTime = DateTime.Now;
        DateTime date = DateTime.Now;
        System.Collections.Generic.List<Expo> lista;
        int select=0;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Start);
            lista = LDbConnection.GetActualUserExpo();
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            drawerLayout = FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<Android.Support.Design.Widget.NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += HomeNavigationView_NavigationItemSelected;
            var qrcode = FindViewById<Android.Widget.ImageView>(Resource.Id.Start_qrcode);
            var writer = new ZXing.QrCode.QRCodeWriter();
            String s="";
            if (LDbConnection.getUserType() == "Uczestnik")
            {
                s = "Uczestnik:" + LDbConnection.GetUser().Email;
            }
            else if(LDbConnection.getUserType() == "Wystawca"){
                s = "Wystawca:" + LDbConnection.GetCompany().Email;
            }
            ZXing.Common.BitMatrix bm = writer.encode(s, ZXing.BarcodeFormat.QR_CODE, 500, 500);
            Android.Graphics.Bitmap ImageBitmap = Android.Graphics.Bitmap.CreateBitmap(500, 500, Config.Argb8888);

            for (int i = 0; i < 500; i++)
            {//width
                for (int j = 0; j < 500; j++)
                {//height
                    ImageBitmap.SetPixel(i, j, bm[i, j] ? Color.Black : Color.White);
                }
            }

            if (ImageBitmap != null)
            {
                qrcode.SetImageBitmap(ImageBitmap);
            }
            var expo_list = FindViewById<Android.Widget.Spinner>(Resource.Id.Start_targi);
            expo_list.ItemSelected += new System.EventHandler<Android.Widget.AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = new MyExpoSingleListViewAdapter(lista, this);
            expo_list.Adapter = adapter;
            var btn1 = (Button)FindViewById(Resource.Id.Start_join);
            btn1.Visibility = Android.Views.ViewStates.Invisible;
            btn1.Click += delegate
            {
                var NxtAct = new Android.Content.Intent(this, typeof(UserActivity));
                StartActivity(NxtAct);
            };
            var button = FindViewById<Android.Widget.Button>(Resource.Id.Start_scan);
            button.Click += async delegate
            {
                ZXing.Mobile.MobileBarcodeScanner scanner;
                ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
                scanner = new ZXing.Mobile.MobileBarcodeScanner();
                scanner.UseCustomOverlay = false;

                //We can customize the top and bottom text of the default overlay
                scanner.BottomText = "Poczekaj, aż kod kreskowy będzie automatycznie zeskanowany!";

                //Start scanning
                var result = await scanner.Scan();

                scanner.Cancel();
                if (result == null)
                {
                    scanner.Cancel();

                }
                else {
                    scanner.Cancel();
                    string[] scan = result.Text.Split(':');
                if (scan[0] == "Wystawca")
                {
                    var NxtAct = new Android.Content.Intent(this, typeof(CompanyActivity));
                    NxtAct.PutExtra("Email", scan[1]);
                        NxtAct.PutExtra("expo_id", lista[select].Id);
                        NxtAct.PutExtra("Search", result.Text);
                        NxtAct.PutExtra("Show", true);

                        StartActivity(NxtAct);
                }
                else if (scan[0].Contains("Uczestnik"))
                {
                    System.Console.WriteLine("Uczestnik");
                    var NxtAct = new Android.Content.Intent(this, typeof(UserActivity));
                    NxtAct.PutExtra("Email", scan[1]);
                        NxtAct.PutExtra("expo_id", lista[select].Id);
                        NxtAct.PutExtra("Search", result.Text);
                        NxtAct.PutExtra("Show", true);

                        StartActivity(NxtAct);
                }
                }
            };
            if (lista == null)
            {
                button.Visibility = Android.Views.ViewStates.Invisible;
                btn1.Visibility = Android.Views.ViewStates.Visible;
            }
        }
        public override void OnBackPressed()
        {
            DateTime pressTime = DateTime.Now;
            if ((pressTime - lastPressTime).TotalMilliseconds <= doublePressInterval_ms)
            {
                FinishActivity(0);
                Java.Lang.JavaSystem.Exit(0);
            
            } else {
                    Toast.MakeText(this, "Press once again to exit!",
                            ToastLength.Short).Show();
            }
            lastPressTime = pressTime;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            select = (int)spinner.GetItemIdAtPosition(e.Position);
            string toast = string.Format("Selected car is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
          {
              switch (item.ItemId)
              {
                  case Android.Resource.Id.Home:
                      drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                      return true;
              }
              return base.OnOptionsItemSelected(item);
          }
          public void HomeNavigationView_NavigationItemSelected(object sender, Android.Support.Design.Widget.NavigationView.NavigationItemSelectedEventArgs e)
          {
              var menuItem = e.MenuItem; menuItem.SetChecked(!menuItem.IsChecked);
              Android.Content.Intent intent;
              switch (menuItem.ItemId)
              {
                  case Resource.Id.nav_my_expos:
                    intent = new Android.Content.Intent(this, typeof(MyExposActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_help:
                    intent = new Android.Content.Intent(this, typeof(VideoActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_expos:
                    intent = new Android.Content.Intent(this, typeof(ExposActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_prof:
                    intent = new Android.Content.Intent(this, typeof(MyProfileActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_history:
                    intent = new Android.Content.Intent(this, typeof(HistoryActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.nav_logout:
                      using (var conn = new SQLite.SQLiteConnection(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal).ToString(), "database.sqlite")))
                      {
                        conn.DeleteAll<Login>();
                        if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'User'") != 0)
                        {

                            if (LDbConnection.GetCompany() != null)
                            {
                                Company c = conn.Table<Company>().First();
                                conn.DropTable<Company>();
                                conn.DropTable<Login>();
                                conn.DropTable<Expo>();
                                conn.DropTable<HistoryU>();
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.Photo1));
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.Photo2));
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.Photo3));
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.Photo4));
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.Photo5));
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.StandPhoto));
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + c.CompanyLogo));


                            }
                            if (LDbConnection.GetUser() != null)
                            {
                                User u = conn.Table<User>().First();
                                conn.DropTable<User>();
                                conn.DropTable<Login>();
                                conn.DropTable<Expo>();
                                conn.DropTable<HistoryU>();
                                File.Delete(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + u.Photo));
                            }
                            if (LDbConnection.GetUserExpo() != null)
                                conn.DropTable<Expo>();
                            if (LDbConnection.GetHistoryUser() != null)
                                if (LDbConnection.GetHistoryUser().Count != 0)
                                    conn.DropTable<HistoryU>();
                        }
                        conn.Commit();
                        conn.Close();
                      }
                      intent = new Android.Content.Intent(this, typeof(MainActivity));
                      StartActivity(intent);
                      Finish();
                      break;
              }
          }
      }
  }
 