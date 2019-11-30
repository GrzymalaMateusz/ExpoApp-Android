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
    class HistoryAdapter : BaseAdapter<HistoryU>
    {
        public List<HistoryU> mItems;
        public Context mContext;

        public HistoryAdapter(List<HistoryU> mItems, Context mContext)
        {
            this.mItems = mItems;
            this.mContext = mContext;
        }

        public override HistoryU this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.HistoryRow, null, false);
            }
            TextView txtName = row.FindViewById<TextView>(Resource.Id.History_Row_Description);
            txtName.Text = mItems[position].Description;

            return row;
        }
    }
}