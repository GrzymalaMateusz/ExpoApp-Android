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
using ExpoApp.Models;

namespace ExpoApp.Droid
{
    class EventListViewAdapter : BaseAdapter<Event>
    {
        public List<Event> mItems;
        public Context mContext;

        public EventListViewAdapter(List<Event> mItems, Context mContext)
        {
            this.mItems = mItems;
            this.mContext = mContext;
        }

        public override Event this[int position]
        {
            get { return mItems[position]; }
        }


        public override int Count
        {
            get { return mItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.EventRow, null, false);
            }
            TextView txtName = row.FindViewById<TextView>(Resource.Id.ExpoRowName);
            txtName.Text = mItems[position].StartDate.ToShortDateString();

            TextView txtDS = row.FindViewById<TextView>(Resource.Id.ExpoRowDateS);
            txtDS.Text = mItems[position].Name;

            TextView txtDE = row.FindViewById<TextView>(Resource.Id.ExpoRowDateE);
            txtDE.Text = mItems[position].Place;
            

            return row;
        }
    }
}