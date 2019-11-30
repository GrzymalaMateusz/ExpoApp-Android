using Android.Content;
using System;
using System.Diagnostics;
using System.IO;

namespace ExpoApp
{
	public class DbConnection
	{
        public static string url = "http://www.grzymek.cba.pl/MyFiles/Json/";
        public static string url1 = "http://expotest.somee.com/json/";
        public static DbConnection db=null;
		public DbConnection Instance()
		{
            if (db != null)
            {
                return db;
            }
            else
            {
                db = new DbConnection();
                return db;
            }


		}
        /**
         * pobranie i zapisanie obrazka z sieci
         * 
         * Parametry: Context i adres www 
         */
        public static Android.Graphics.Bitmap GetImageBitmapFromUrl(Context c,string url)
        {
            Android.Graphics.Bitmap imageBitmap = null;

            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

                    }

                }
                catch (System.Net.WebException ex)
                {
                    Android.Widget.Toast.MakeText(c, "Brak połączenia", Android.Widget.ToastLength.Long);
                }
            }
            return imageBitmap;
        }
        /**
         * pobranie i zapisanie obrazka z sieci
         * 
         * Parametry: Context, adres www i nazwa pliku na urządzeniu
         */
        public static void GetImageBitmapFromUrlAndSave(Context c,string url, string name)
        {
            Android.Graphics.Bitmap imageBitmap = null;

            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

                        System.IO.FileStream fs = new System.IO.FileStream(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) +"/"+ name), FileMode.OpenOrCreate, FileAccess.Write);
                        imageBitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, fs);
                    }

                }
                catch (System.Net.WebException ex)
                {
                    Android.Widget.Toast.MakeText(c, "Brak połączenia", Android.Widget.ToastLength.Long);
                }
            }
        }
        public static async System.Threading.Tasks.Task UploadImageUser(Context c,Android.Net.Uri imageUri, String Email)
        {
            //variable

            try
            {
                //read file into upfilebytes array
                var upfilebytes = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c,imageUri));

                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.MultipartFormDataContent content = new System.Net.Http.MultipartFormDataContent();
                System.Net.Http.ByteArrayContent baContent = new System.Net.Http.ByteArrayContent(upfilebytes);
                System.Net.Http.StringContent studentIdContent = new System.Net.Http.StringContent(Email);
                content.Add(baContent, "photod", "avatar.jpg");
                content.Add(studentIdContent, "Email");


                //upload MultipartFormDataContent content async and store response in response var
                var response =
                    await client.PostAsync(url1+"UploadPhotoUser", content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;

                //debug
                Android.Widget.Toast.MakeText(c, responsestr, Android.Widget.ToastLength.Long).Show();

            }
            catch (Exception e)
            {
                //debug
                Debug.WriteLine("Exception Caught: " + e.ToString());

            }
        }
        public static async System.Threading.Tasks.Task UploadImageCompany(Context c, Android.Net.Uri logo, Android.Net.Uri stand, Android.Net.Uri photo1, Android.Net.Uri photo2, Android.Net.Uri photo3, Android.Net.Uri photo4, Android.Net.Uri photo5, String Email)
        {
            //variable

            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.MultipartFormDataContent content = new System.Net.Http.MultipartFormDataContent();

                //read file into upfilebytes array
                if (logo != null)
                {
                    var ilogo = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, logo));
                    System.Net.Http.ByteArrayContent clogo = new System.Net.Http.ByteArrayContent(ilogo);
                    content.Add(clogo, "logo", "logo.jpg");
                }
                if (stand != null)
                {
                    var istand = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, stand));
                    System.Net.Http.ByteArrayContent cstand = new System.Net.Http.ByteArrayContent(istand);
                    content.Add(cstand, "stand", "stand.jpg");
                }
                if (photo1 != null)
                {
                    var iphoto1 = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, photo1));
                    System.Net.Http.ByteArrayContent cphoto1 = new System.Net.Http.ByteArrayContent(iphoto1);
                    content.Add(cphoto1, "photo1", "p1.jpg");
                }
                if (photo2 != null)
                {
                    var iphoto2 = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, photo2));
                    System.Net.Http.ByteArrayContent cphoto2 = new System.Net.Http.ByteArrayContent(iphoto2);
                    content.Add(cphoto2, "photo2", "p2.jpg");
                }
                if (photo3 != null)
                {
                    var iphoto3 = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, photo3));
                    System.Net.Http.ByteArrayContent cphoto3 = new System.Net.Http.ByteArrayContent(iphoto3);
                    content.Add(cphoto3, "photo3", "p3.jpg");
                }
                if (photo4 != null)
                {
                    var iphoto4 = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, photo4));
                    System.Net.Http.ByteArrayContent cphoto4 = new System.Net.Http.ByteArrayContent(iphoto4);
                    content.Add(cphoto4, "photo4", "p4.jpg");
                }
                if (photo5 != null)
                {
                    var iphoto5 = File.ReadAllBytes(Droid.Modules.ImagesHelp.GetPathToImage(c, photo5));
                    System.Net.Http.ByteArrayContent cphoto5 = new System.Net.Http.ByteArrayContent(iphoto5);
                    content.Add(cphoto5, "photo5", "p5.jpg");
                }
                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId


                System.Net.Http.StringContent studentIdContent = new System.Net.Http.StringContent(Email);
                
                content.Add(studentIdContent, "Email");


                //upload MultipartFormDataContent content async and store response in response var
                var response =
                    await client.PostAsync(url+"UploadPhotoCompany", content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;

                //debug
                Android.Widget.Toast.MakeText(c, responsestr, Android.Widget.ToastLength.Long).Show();

            }
            catch (Exception e)
            {
                //debug
                Debug.WriteLine("Exception Caught: " + e.ToString());

            }
        }
        public static async System.Threading.Tasks.Task<Models.User> FetchUserAsync(string email)
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1+"/user?Email="+email));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            try
            {
                using (System.Net.WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                        // Return the JSON document:
                        return Droid.Modules.Parsers.User(jsonDoc);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                
            }
            return null;
        }
        public static async System.Threading.Tasks.Task<Models.Company> FetchCompanyAsync(string email)
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1 + "/company?Email=" + email));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            try
            {
                using (System.Net.WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                        // Return the JSON document:
                        return Droid.Modules.Parsers.Company(jsonDoc);
                    }
                }
            }
            catch (System.Net.WebException ex)
            {

            }
            return null;
        }
        public static async System.Threading.Tasks.Task<System.Collections.Generic.List<Models.Event>> FetchEventsAsync()
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1 + "events"));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    System.Collections.Generic.List<Models.Event> lista = new System.Collections.Generic.List<Models.Event>();
                    for (int i = 0; i != jsonDoc.Count; i++)
                    {
                        Models.Event e = new Models.Event
                        {
                            Name = jsonDoc[i]["Name"],
                            Id = Int32.Parse(jsonDoc[i]["Id"].ToString()),
                            Description = jsonDoc[i]["Description"],
                            Place = jsonDoc[i]["Place"]
                        };

                        var start = double.Parse(jsonDoc[i]["StartDate"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time = TimeSpan.FromMilliseconds(start);
                        DateTime startdate = new DateTime(1970, 1, 1) + time;

                        e.StartDate = startdate;
                        var end = double.Parse(jsonDoc[i]["EndDate"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time1 = TimeSpan.FromMilliseconds(start);
                        DateTime enddate = new DateTime(1970, 1, 1) + time;

                        e.EndDate = enddate;
                        lista.Add(e);

                    }
                    return lista;
                }
            }

        }
        public static async System.Threading.Tasks.Task<System.Collections.Generic.List<Models.Expo>> FetchExpoAsync()
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1+"expos"));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    System.Collections.Generic.List<Models.Expo> lista = new System.Collections.Generic.List<Models.Expo>();
                    for (int i = 0; i != jsonDoc.Count; i++)
                    {
                        Models.Expo e = new Models.Expo
                        {
                            Name_Expo = jsonDoc[i]["Name_Expo"],
                            Id = Int32.Parse(jsonDoc[i]["Id"].ToString()),
                            Photo = jsonDoc[i]["Photo"],
                            MapPhoto = jsonDoc[i]["MapPhoto"],
                            Adres = jsonDoc[i]["Adres"],
                            Description = jsonDoc[i]["Description"]
                        };
                        var start = double.Parse(jsonDoc[i]["ExpoStartData"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time = TimeSpan.FromMilliseconds(start);
                        DateTime startdate = new DateTime(1970, 1, 1) + time;

                        e.DataTargowStart = startdate;
                        var end = double.Parse(jsonDoc[i]["ExpoEndData"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time1 = TimeSpan.FromMilliseconds(start);
                        DateTime enddate = new DateTime(1970, 1, 1) + time;

                        e.DataTargowEnd = enddate;
                        lista.Add(e);

                    }
                    return lista;
                }
            }

        }
        public static async System.Threading.Tasks.Task<System.Collections.Generic.List<Models.Expo>> FetchUserExposAsync(Models.Company u)
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1 + "CompanyExpos?email=" + u.Email));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    System.Collections.Generic.List<Models.Expo> lista = new System.Collections.Generic.List<Models.Expo>();
                    for (int i = 0; i != jsonDoc.Count; i++)
                    {
                        Models.Expo e = new Models.Expo
                        {
                            Name_Expo = jsonDoc[i]["Name_Expo"],
                            Id = Int32.Parse(jsonDoc[i]["Id"].ToString()),
                            Photo = jsonDoc[i]["Photo"],
                            MapPhoto = jsonDoc[i]["MapPhoto"],
                            Adres = jsonDoc[i]["Adres"],
                            Description = jsonDoc[i]["Description"]
                        };
                        var start = double.Parse(jsonDoc[i]["ExpoStartData"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time = TimeSpan.FromMilliseconds(start);
                        DateTime startdate = new DateTime(1970, 1, 1) + time;

                        e.DataTargowStart = startdate;
                        var end = double.Parse(jsonDoc[i]["ExpoEndData"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time1 = TimeSpan.FromMilliseconds(end);
                        DateTime enddate = new DateTime(1970, 1, 1) + time1;

                        e.DataTargowEnd = enddate;
                        lista.Add(e);

                    }
                    return lista;
                }
            }

        }
        public static async System.Threading.Tasks.Task<System.Collections.Generic.List<Models.Expo>> FetchUserExposAsync(Models.User u)
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1+"UserExpos?email="+u.Email));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    System.Collections.Generic.List<Models.Expo> lista = new System.Collections.Generic.List<Models.Expo>();
                    for (int i = 0; i != jsonDoc.Count; i++)
                    {
                        Models.Expo e = new Models.Expo
                        {
                            Name_Expo = jsonDoc[i]["Name_Expo"],
                            Id = Int32.Parse(jsonDoc[i]["Id"].ToString()),
                            Photo = jsonDoc[i]["Photo"],
                            MapPhoto = jsonDoc[i]["MapPhoto"],
                            Adres = jsonDoc[i]["Adres"],
                            Description = jsonDoc[i]["Description"]
                        };
                        var start = double.Parse(jsonDoc[i]["ExpoStartData"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time = TimeSpan.FromMilliseconds(start);
                        DateTime startdate = new DateTime(1970, 1, 1) + time;

                        e.DataTargowStart = startdate;
                        var end = double.Parse(jsonDoc[i]["ExpoEndData"].ToString().Replace('/', ' ').Replace('"', ' ').Replace(')', ' ').Replace("Date(", "").Trim());
                        TimeSpan time1 = TimeSpan.FromMilliseconds(end);
                        DateTime enddate = new DateTime(1970, 1, 1) + time1;

                        e.DataTargowEnd = enddate;
                        lista.Add(e);

                    }
                    return lista;
                }
            }

        }
        public static async System.Threading.Tasks.Task<Boolean> FetchUserHistoryAsync(Models.HistoryU obj)
        {
            Uri u = new Uri(url1+"insertHistoryU?Search=" + obj.Wyszukiwanie + "&Description=" + obj.Description + "&Userid=" + obj.User + "&Expoid=" + obj.Expo);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    if (Boolean.Parse(jsonDoc["saved"].ToString()) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static async System.Threading.Tasks.Task<Boolean> FetchCompanyHistoryAsync(Models.HistoryU obj)
        {
            Uri u = new Uri(url1 + "insertHistoryC?Search=" + obj.Wyszukiwanie + "&Description=" + obj.Description + "&Companyid=" + obj.User + "&Expoid=" + obj.Expo);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    if (Boolean.Parse(jsonDoc["saved"].ToString()) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static async System.Threading.Tasks.Task<System.Collections.Generic.List<Models.HistoryU>> FetchHistory(Models.User u)
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1 + "History?email=" + u.Email));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    System.Collections.Generic.List<Models.HistoryU> lista = new System.Collections.Generic.List<Models.HistoryU>();
                    for (int i = 0; i != jsonDoc.Count; i++)
                    {
                        Models.HistoryU h = new Models.HistoryU
                        {
                            ID = Int32.Parse(jsonDoc[i]["ID"].ToString()),
                            Description = jsonDoc[i]["Description"],
                            Wyszukiwanie = jsonDoc[i]["Wyszukiwanie"],
                            User = Int32.Parse(jsonDoc[i]["User"].ToString()),
                            Expo = Int32.Parse(jsonDoc[i]["Expo"].ToString())
                        };
                        lista.Add(h);

                    }
                    return lista;
                }
            }

        }
        public static async System.Threading.Tasks.Task<System.Collections.Generic.List<Models.HistoryU>> FetchHistory(Models.Company u)
        {
            // Create an HTTP web request using the URL:
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(new Uri(url1 + "History?email=" + u.Email));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    System.Collections.Generic.List<Models.HistoryU> lista = new System.Collections.Generic.List<Models.HistoryU>();
                    for (int i = 0; i != jsonDoc.Count; i++)
                    {
                        Models.HistoryU h = new Models.HistoryU
                        {
                            ID = Int32.Parse(jsonDoc[i]["ID"].ToString()),
                            Description = jsonDoc[i]["Description"],
                            Wyszukiwanie = jsonDoc[i]["Wyszukiwanie"],
                            User = Int32.Parse(jsonDoc[i]["User"].ToString()),
                            Expo = Int32.Parse(jsonDoc[i]["Expo"].ToString())
                        };
                        lista.Add(h);

                    }
                    return lista;
                }
            }

        }

        public static async System.Threading.Tasks.Task<Boolean> FetchUserJoinExpoAsync(Models.User us,Models.Expo e)
        {
            Uri u = new Uri(url1+"insertUExpo?Expoid=" + e.Id + "&Userid=" + us.ID);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    if (Boolean.Parse(jsonDoc["JoinedU"].ToString()) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static async System.Threading.Tasks.Task<Boolean> FetchCompanyJoinExpoAsync(Models.Company us, Models.Expo e)
        {
            Uri u = new Uri(url1 + "insertCExpo?Expoid=" + e.Id + "&Companyid=" + us.Id);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    if (Boolean.Parse(jsonDoc["JoinedC"].ToString()) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static async System.Threading.Tasks.Task<Boolean> FetchUserEditAsync(Models.User obj)
        {
            Uri u = new Uri(url1+"EditProfileUser?Email=" + obj.Email + "&ForName=" +obj.ForName + "&SurName=" + obj.SurName + "&Phone=" + obj.Phone + "&Nationality=" + obj.Nationality);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    if (Boolean.Parse(jsonDoc["AccountChanged"].ToString()) == true)
                    {
                        return true;
                    }
                    else if (Boolean.Parse(jsonDoc["Unactual"].ToString()) == true)
                    {
                        LDbConnection.InsertUser(await DbConnection.FetchUserAsync(obj.Email));
                        return true;
                    }
                }
            }
            return false;
        }
        public static async System.Threading.Tasks.Task<Boolean> FetchCompanyEditAsync(Models.Company obj)
        {
            Uri u = new Uri(url1+"EditProfileCompany?Email=" + obj.Email + "&CompanyName=" + obj.CompanyName +
             "&CompanyFullName=" + obj.CompanyFullName + "&CompanyAbout=" + obj.CompanyAbout + "&ProductsAbout=" + obj.ProductsAbout + "&Facebook=" + obj.Facebook
             + "&Instagram=" + obj.Instagram + "&Snapchat=" + obj.Snapchat + "&Youtube=" + obj.Youtube + "&Phone=" + obj.Phone + "&UserPhone=" + obj.UserPhone
             + "&ContactEmail=" + obj.ContactEmail + "&www=" + obj.www + "&Adress=" + obj.www + "&NIP=" + obj.NIP + "&Forname_And_Surname=" + obj.ForName_And_SurName);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    if (Boolean.Parse(jsonDoc["AccountChanged"].ToString()) == true)
                    {
                        return true;
                    }
                    else if (Boolean.Parse(jsonDoc["Unactual"].ToString()) == true)
                    {
                        LDbConnection.InsertCompany(await DbConnection.FetchCompanyAsync(obj.Email));
                        return true;
                    }
                }
            }
            return false;
        }
        public static async System.Threading.Tasks.Task<Models.MobileRegister> FetchRegisterAsync(String email,String password,String userType)
        {
            Uri u = new Uri(url1+"Register?Email=" + email + "&Password=" + password + "&UserType=" + userType);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    return Droid.Modules.Parsers.Register(jsonDoc);
                }
            }
            return null;
        }
        public static async System.Threading.Tasks.Task<Models.MobileLogin> FetchLoginAsync(String email, String password)
        {
            Uri u = new Uri(url1+"Login?Email=" + email + "&Password=" + password);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(u);
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (System.Net.WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    System.Json.JsonValue jsonDoc = await System.Threading.Tasks.Task.Run(() => System.Json.JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    return Droid.Modules.Parsers.Login(jsonDoc);
                }
            }
            return null;
        }

    }
}

