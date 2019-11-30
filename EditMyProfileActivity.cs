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
    class EditMyProfileActivity : Activity
    {
        private static int CAMERA_REQUEST = 1888;
        private Android.Net.Uri imageUri;
        private Android.Net.Uri imageUril;
        private Android.Net.Uri imageUris;
        private Android.Net.Uri imageUri1;
        private Android.Net.Uri imageUri2;
        private Android.Net.Uri imageUri3;
        private Android.Net.Uri imageUri4;
        private Android.Net.Uri imageUri5;

        ImageView imageView;
        ImageView i1;
        ImageView i2;
        ImageView i3;
        ImageView i4;
        ImageView i5;
        ImageView iss;
        ImageView il;
        Android.Graphics.Bitmap b;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (this.Intent.GetStringExtra("Typ") == "uczestnik")
            {
                SetContentView(Resource.Layout.EditMyProfileU);
                imageView = (ImageView)FindViewById(Resource.Id.User_Edit_Profile_Photo);
                EditText Email = (EditText)FindViewById(Resource.Id.User_Edit_Profile_Email);
                Email.Text = this.Intent.GetStringExtra("Email");

                EditText ForName = (EditText)FindViewById(Resource.Id.User_Edit_Profile_ForName);
                EditText SurName = (EditText)FindViewById(Resource.Id.User_Edit_Profile_SurName);
                EditText Phone = (EditText)FindViewById(Resource.Id.User_Edit_Profile_Phone);
                EditText Nationality = (EditText)FindViewById(Resource.Id.User_Edit_Profile_Nationality);

                Button ChangePhoto = (Button)FindViewById(Resource.Id.User_Edit_Profile_Button_Photo);
                Button TakePhoto = (Button)FindViewById(Resource.Id.User_Edit_Profile_Button_Take_Photo);
                Button Save = (Button)FindViewById(Resource.Id.User_Edit_Profile_Save);
                Save.Click+=delegate {
                    SaveUserChangesAsync(Email.Text,ForName.Text,SurName.Text, Java.Lang.Long.ParseLong(Phone.Text), Nationality.Text);
                    
                };
                ChangePhoto.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(
                        Intent.CreateChooser(imageIntent, "Select photo"), 88);
                };
                TakePhoto.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, CAMERA_REQUEST);
                };
            }
            else if (this.Intent.GetStringExtra("Typ") == "wystawca")
            {
                SetContentView(Resource.Layout.EditMyProfileC);
                EditText CompanyName = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_ComapnyName);
                EditText CompanyFullName = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_CompanyFullName);
                EditText CompanyAbout = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_CompanyAbout);
                EditText ProductsAbout = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_ProductsAbout);
                EditText Phone = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_Phone);
                EditText ContactPhone = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_UserPhone);
                EditText Email = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_Email);
                EditText ContactEmail = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_ContactEmail);
                EditText WWW = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_WWW);
                EditText Youtube = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_Youtube);
                EditText Facebook = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_Facebook);
                EditText Instagram = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_Instagram);
                EditText NIP = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_NIP);
                EditText Adress = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_Adress);
                EditText FornameAndSurname = (EditText)FindViewById(Resource.Id.Company_Edit_Profile_FornameAndSurname);

                i1 = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_Photo1);
                i2 = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_Photo2);
                i3 = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_Photo3);
                i4 = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_Photo4);
                i5 = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_Photo5);
                iss = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_SPhoto);
                il = (ImageView)FindViewById(Resource.Id.Company_Edit_Profile_Logo);


                Button logo = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Logo);
                Button logoc = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_Logo);
                Button p1 = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Photo1);
                Button p1c = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_Logo);
                Button p2 = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Photo2);
                Button p2c = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_Photo2);
                Button p3 = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Photo3);
                Button p3c = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_Photo3);
                Button p4 = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Photo4);
                Button p4c = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_Photo4);
                Button p5 = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Photo5);
                Button p5c = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_Photo5);
                Button spc = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_Take_SPhoto);
                Button sp = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Button_SPhoto);


                p1.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 188);
                };
                p1c.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 189);
                };
                p2.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 288);
                };
                p2c.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 289);
                };
                p3.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 388);
                };
                p3c.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 389);
                };
                p4.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 488);
                };
                p4c.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 489);
                };
                p5.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 588);
                };
                p5c.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 589);
                };
                sp.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 688);
                };
                spc.Click += delegate
                {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 689);
                };
                logo.Click += delegate {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 788);
                };
                logoc.Click += delegate {
                    Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCaptureSecure);
                    StartActivityForResult(intent, 789);
                };





                Button Save = (Button)FindViewById(Resource.Id.Company_Edit_Profile_Save);
                Save.Click += delegate {
                    SaveCompanyChangesAsync(Email.Text,ContactEmail.Text,long.Parse(ContactPhone.Text),long.Parse(Phone.Text),CompanyName.Text,CompanyFullName.Text,
                        CompanyAbout.Text,ProductsAbout.Text,Facebook.Text,Instagram.Text,Youtube.Text,WWW.Text,Adress.Text,FornameAndSurname.Text,NIP.Text);

                };

            }

        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 88 && resultCode == Result.Ok)
            {
                imageUri = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this,imageUri));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri);
                imageView.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == CAMERA_REQUEST && resultCode == Result.Ok)
            {
                imageUri = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this,imageUri));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri);
                imageView.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b,degree));
            }
            if (requestCode == 188 && resultCode == Result.Ok)
            {
                imageUri1 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri1));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri1);
                i1.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 189 && resultCode == Result.Ok)
            {
                imageUri1 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri1));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri1);
                i1.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 288 && resultCode == Result.Ok)
            {
                imageUri2 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri2));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri2);
                i2.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 289 && resultCode == Result.Ok)
            {
                imageUri2 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri2));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri2);
                i2.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 388 && resultCode == Result.Ok)
            {
                imageUri3 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri3));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri3);
                i3.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 389 && resultCode == Result.Ok)
            {
                imageUri3 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri3));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri3);
                i3.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 488 && resultCode == Result.Ok)
            {
                imageUri4 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri4));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri4);
                i4.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 489 && resultCode == Result.Ok)
            {
                imageUri4 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri4));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri4);
                i4.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 588 && resultCode == Result.Ok)
            {
                imageUri5 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri5));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri5);
                i5.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 589 && resultCode == Result.Ok)
            {
                imageUri5 = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUri5));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri5);
                i5.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 688 && resultCode == Result.Ok)
            {
                imageUris = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUris));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUris);
                iss.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 689 && resultCode == Result.Ok)
            {
                imageUris = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUris));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUris);
                iss.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 788 && resultCode == Result.Ok)
            {
                imageUril = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUril));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUril);
                il.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
            if (requestCode == 789 && resultCode == Result.Ok)
            {
                imageUril = data.Data;
                Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(Modules.ImagesHelp.GetPathToImage(this, imageUril));
                int degree = Java.Lang.Integer.ParseInt(exifInterface.GetAttribute(Android.Media.ExifInterface.TagOrientation));
                b = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUril);
                il.SetImageBitmap(Modules.ImagesHelp.rotateBitmap(b, degree));
            }
        }
        private async System.Threading.Tasks.Task SaveUserChangesAsync(String Email, String ForName,String SurName, long Phone, String Nationality)
        {
            User u=null;
            if (this.Intent.GetStringExtra("Action") == "Register")
            {
                u = new User();
            }
            else if (this.Intent.GetStringExtra("Action") == "Edit")
            {
                u = LDbConnection.GetUser();
            }
            u.ForName = ForName;
            u.SurName = SurName;
            u.Phone = Phone;
            u.Email = Email;
            u.Nationality = Nationality;
            if (await DbConnection.FetchUserEditAsync(u))
            {
                if (imageUri != null)
                {
                    await DbConnection.UploadImageUser
                    (this, imageUri, Email);
                    LDbConnection.InsertUser(this, await DbConnection.FetchUserAsync(Email));
                }
                else
                {
                    LDbConnection.InsertUser(await DbConnection.FetchUserAsync(Email));
                }
                var NxtAct = new Intent(this, typeof(StartActivity));
                StartActivity(NxtAct);
                Finish();
            }
        }
        private async System.Threading.Tasks.Task SaveCompanyChangesAsync(String Email, String ContactEmail, long UserPhone, long Phone, String CompanyName, String CompanyFullName, String CompanyAbout, String ProductsAbout, String Facebook, String Instagram, String Youtube, String WWW, String Adres, String ForName_And_SurName,String NIP)
        {
            Company c = null;
            if (this.Intent.GetStringExtra("Action") == "Register")
            {
                c = new Company();
            }
            else if (this.Intent.GetStringExtra("Action") == "Edit")
            {
                c = LDbConnection.GetCompany();
            }
            c.CompanyName = CompanyName;
            c.CompanyFullName = CompanyFullName;
            c.CompanyAbout = CompanyAbout;
            c.ProductsAbout = ProductsAbout;
            c.Email = Email;
            c.ContactEmail = ContactEmail;
            c.UserPhone = UserPhone;
            c.Facebook = Facebook;
            c.Instagram = Instagram;
            c.Youtube = Youtube;
            c.www = WWW;
            c.Phone = Phone;
            c.Adress = Adres;
            c.ForName_And_SurName = ForName_And_SurName;
            c.NIP = NIP;


            if (await DbConnection.FetchCompanyEditAsync(c))
            {
                if (imageUri1 != null || imageUri2!=null || imageUri3!=null || imageUri4!=null || imageUri5!=null || imageUril!=null || imageUris!=null)
                {
                    await DbConnection.UploadImageCompany(this, imageUril, imageUris, imageUri1, imageUri2, imageUri3, imageUri4, imageUri5, Email);
                }
                LDbConnection.InsertCompany(this, await DbConnection.FetchCompanyAsync(Email));

                var NxtAct = new Intent(this, typeof(StartActivity));
                StartActivity(NxtAct);
                Finish();
            }
        }


    }
}