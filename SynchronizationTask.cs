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
using Java.Lang;

namespace ExpoApp.Droid
{
    class SynchronizationTask : AsyncTask
    {
        protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
        {
            List<Models.Synchronization> list = LDbConnection.GetSynchronization();
            for(int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).Expo_Id != -1 && list.ElementAt(i).User_Id!=-1)
                {
                    DbConnection.FetchUserJoinExpoAsync(LDbConnection.GetUser(), LDbConnection.GetUserExpo(list.ElementAt(i).Expo_Id));
                }else if (list.ElementAt(i).Expo_Id != -1 && list.ElementAt(i).Company_Id != -1)
                {
                    //DbConnection.FetchCompanyJoinExpoAsync(LDbConnection.GetCompany(), LDbConnection.GetUserExpo(list.ElementAt(i).Expo_Id));
                }
                else if (list.ElementAt(i).History_Id != -1)
                {
                    DbConnection.FetchUserHistoryAsync(LDbConnection.GetHistoryUser(list.ElementAt(i).History_Id));
                }else if(list.ElementAt(i).User_Id!=-1){
                    //DbConnection.FetchUserSync(LDbConnection.GetUser());
                }
                else if (list.ElementAt(i).Company_Id != -1)
                {
                  //  DbConnection.FetchCompanySync(LDbConnection.GetCompany());
                }
            }
            return "Zsycnchronizowano";
        }
    }
}