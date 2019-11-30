using Android.Content;
using ExpoApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExpoApp
{
    public class LDbConnection
    {
        public static LDbConnection db = null;
        public LDbConnection Instance()
        {
            if (db != null)
            {
                return db;
            }
            else
            {
                db = new LDbConnection();
                return db;
            }


        }
        public static String getUserType()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'User'") != 0)
                {
           
                    return "Uczestnik";
                }
                if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Company'") != 0)
                {
                    return "Wystawca";
                }
            }
            return null;
        }
        public static void InsertUser(Context c,User u)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<User>();
                conn.DeleteAll<User>();
                DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/User/" + u.Photo), u.Photo);
                conn.Insert(u);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertUser(User u)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<User>();
                conn.DeleteAll<User>();
                conn.Insert(u);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertCompany(Context c, Company com)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<Company>();
                conn.DeleteAll<Company>();
                if (com.CompanyLogo != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.CompanyLogo), com.CompanyLogo);
                }
                if (com.Photo1 != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.Photo1), com.Photo1);
                }
                if (com.Photo2 != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.Photo2), com.Photo2);
                }
                if (com.Photo3 != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.Photo3), com.Photo3);
                }
                if (com.Photo4 != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.Photo4), com.Photo4);
                }
                if (com.Photo5 != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.Photo5), com.Photo5);
                }
                if (com.StandPhoto != null)
                {
                    DbConnection.GetImageBitmapFromUrlAndSave(c, ("http://expotest.somee.com/Images/Company/" + com.StandPhoto), com.StandPhoto);
                }
                conn.Insert(com);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertCompany(Company c)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<Company>();
                conn.DeleteAll<Company>();
                conn.Insert(c);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertLogin(Login l)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<Login>();
                conn.Insert(l);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertUserExpos(List<Expo> l)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<Expo>();
                for (int i = 0; i < l.Count; i++)
                {
                    conn.Insert(l[i]);
                }
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertHistoryU(HistoryU h)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<HistoryU>();
                conn.Insert(h);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertUExpo(Expo e)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<Expo>();
                conn.Insert(e);
                conn.Commit();
                conn.Close();
            }
        }
        public static void InsertSynchronization(Models.Synchronization e)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<Models.Synchronization>();
                e.Data_mod = DateTime.Now;
                conn.Insert(e);
                conn.Commit();
                conn.Close();
            }
        }
        public static User GetUser()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'User'") != 0)
                {
                    return conn.Table<User>().First();
                }
            }
            return null;
        }
        public static Company GetCompany()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Company'") != 0)
                {
                    return conn.Table<Company>().First();
                }
            }
            return null;
        }
        public static MobileLogin GetLogin()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                if (conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Company'") != 0)
                {
                    return conn.Table<MobileLogin>().First();
                }
            }
            return null;
        }
        public static List<Expo> GetUserExpo()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                var count = conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Expo'");
                if (count != 0)
                {
                    List<Expo> lista = new List<Expo>();
                    for (int i = 0; i < conn.Table<Expo>().Count(); i++)
                        lista.Add(conn.Table<Expo>().ElementAt(i));
                    return lista;
                }
            }
            return new List<Expo>();
        }
        public static Expo GetUserExpo(int index)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                var count = conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Expo'");
                if (count != 0)
                {
                     return conn.Table<Expo>().Where(s=>s.Id==index).First();
                }
            }
            return new Expo();
        }
        public static List<Expo> GetActualUserExpo()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                var count = conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Expo'");
                if (count != 0)
                {
                    List<Expo> lista = new List<Expo>();
                    for (int i = 0; i < conn.Table<Expo>().Count(); i++)
                    {
                        if (DateTime.Compare(conn.Table<Expo>().ElementAt(i).DataTargowStart, DateTime.Now) < 0 && DateTime.Compare(conn.Table<Expo>().ElementAt(i).DataTargowEnd, DateTime.Now) > 0)
                        {

                            lista.Add(conn.Table<Expo>().ElementAt(i));
                        }
                    }
                    return lista;
                }
            }
            return new List<Expo>();
        }
        public static List<HistoryU> GetHistoryUser()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                var count = conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'HistoryU'");
                if (count != 0)
                {
                    if (conn.Table<HistoryU>().Count() != 0)
                    {
                        List<HistoryU> lista = new List<HistoryU>();
                        for (int i = 0; i < conn.Table<HistoryU>().Count(); i++)
                        {
                            HistoryU h = conn.Table<HistoryU>().ElementAt(i);
                            lista.Add(h);
                        }
                        return lista;
                    }
                }
            }
            return new List<HistoryU>();
        }
        public static HistoryU GetHistoryUser(long index)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                var count = conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'HistoryU'");
                if (count != 0)
                {
                    if (conn.Table<HistoryU>().Count() != 0)
                    {
                            return conn.Table<HistoryU>().Where(s=>s.ID==index).First();
                    }
                }
            }
            return new HistoryU();
        }
        public static void InsertHistory(List<HistoryU> l)
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                conn.CreateTable<HistoryU>();
                for (int i = 0; i < l.Count; i++)
                {
                    conn.Insert(l[i]);
                }
                conn.Commit();
                conn.Close();
            }
        }

        public static List<Synchronization> GetSynchronization()
        {
            using (var conn = new SQLite.SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite")))
            {
                var count = conn.ExecuteScalar<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'Synchronization'");
                if (count != 0)
                {
                    if (conn.Table<Synchronization>().Count() != 0)
                    {
                        List<Synchronization> lista = new List<Synchronization>();
                        for (int i = 0; i < conn.Table<Synchronization>().Count(); i++)
                        {
                            Synchronization h = conn.Table<Synchronization>().ElementAt(i);
                            lista.Add(h);
                        }
                        return lista;
                    }
                }
            }
            return new List<Synchronization>();
        }
    }
}
