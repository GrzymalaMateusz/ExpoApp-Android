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

namespace ExpoApp.Droid.Modules
{
    class Parsers
    {
        public static Models.MobileChangeAccount Change(System.Json.JsonValue json)
        {


            // Extract the array of name/value results for the field name "weatherObservation". 
            System.Json.JsonValue jsonR = json;
            var u = new Models.MobileChangeAccount();

            u.AccountChanged = bool.Parse(jsonR["AccountChanged"].ToString());
            u.UnValid = jsonR["UnValid"];
            return u;
        }
        public static Models.MobileRegister Register(System.Json.JsonValue json)
        {


            // Extract the array of name/value results for the field name "weatherObservation". 
            System.Json.JsonValue jsonR = json;
            var u = new Models.MobileRegister();

            u.AccountCreated = bool.Parse(jsonR["AccountCreated"].ToString());
            u.UnValid = jsonR["UnValid"];
            return u;
        }
        public static Models.MobileLogin Login(System.Json.JsonValue json)
        {


            // Extract the array of name/value results for the field name "weatherObservation". 
            System.Json.JsonValue jsonR = json;
            var u = new Models.MobileLogin();

            u.UserAndPasswordCorrect = bool.Parse(jsonR["UserAndPasswordCorrect"].ToString());
            u.UserType = jsonR["UserType"];
            return u;
        }
        public static Models.User User(System.Json.JsonValue json)
        {
            System.Json.JsonValue jsonR = json;
            Console.WriteLine("jsonResult: " + jsonR["Email"]);
            var u = new Models.User();
            u.ID = Java.Lang.Long.ParseLong(json["ID"].ToString());
            u.Email = jsonR["Email"];
            u.ForName = jsonR["ForName"];
            u.SurName = jsonR["SurName"];
            u.Photo = jsonR["Photo"];
            u.Phone = Java.Lang.Long.ParseLong(json["Phone"].ToString());
            u.Nationality = jsonR["Nationality"];
            return u;

        }
        public static Models.Company Company(System.Json.JsonValue json)
        {
            System.Json.JsonValue jsonR = json;
            Console.WriteLine("jsonResult: " + jsonR["Email"]);
            var u = new Models.Company();
            u.Id = Java.Lang.Long.ParseLong(json["Id"].ToString());
            u.Email = jsonR["Email"];
            u.Adress = jsonR["Adress"];
            u.CompanyAbout = jsonR["CompanyAbout"];
            u.CompanyFullName = jsonR["CompanyFullName"];
            u.Phone = Java.Lang.Long.ParseLong(json["Phone"].ToString());
            u.CompanyLogo = jsonR["CompanyLogo"];
            u.CompanyName = jsonR["CompanyName"];
            u.ContactEmail = jsonR["ContactEmail"];
            u.Facebook = jsonR["Facebook"];
            u.Instagram = jsonR["Instagram"];
            u.ForName_And_SurName = jsonR["ForName_And_SurName"];
            u.NIP = jsonR["NIP"];
            u.ProductsAbout = jsonR["ProductsAbout"];
            u.Snapchat = jsonR["Snapchat"];
            u.StandPhoto = jsonR["StandPhoto"];
            u.Photo1 = jsonR["Photo1"];
            u.Photo2 = jsonR["Photo2"];
            u.Photo3 = jsonR["Photo3"];
            u.Photo4 = jsonR["Photo4"];
            u.Photo5 = jsonR["Photo5"];
            u.UserPhone = Java.Lang.Long.ParseLong(json["Phone"].ToString());
            u.www = jsonR["www"];
            u.Youtube = jsonR["Youtube"];
            return u;

        }
    }
}