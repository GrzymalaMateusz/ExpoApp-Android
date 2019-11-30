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
    class ImagesHelp
    {
        public static string GetPathToImage(Context c,Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = c.ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = c.ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
        public static Android.Graphics.Bitmap rotateBitmap(Android.Graphics.Bitmap bitmap, int orientation)
        {

            Android.Graphics.Matrix matrix = new Android.Graphics.Matrix();
            switch (orientation)
            {
                case (int)Android.Media.Orientation.Normal:
                    return bitmap;
                case (int)Android.Media.Orientation.FlipHorizontal:
                    matrix.SetScale(-1, 1);
                    break;
                case (int)Android.Media.Orientation.Rotate180:
                    matrix.SetRotate(180);
                    break;
                case (int)Android.Media.Orientation.FlipVertical:
                    matrix.SetRotate(180);
                    matrix.PostScale(-1, 1);
                    break;
                case (int)Android.Media.Orientation.Transpose:
                    matrix.SetRotate(90);
                    matrix.PostScale(-1, 1);
                    break;
                case (int)Android.Media.Orientation.Rotate90:
                    matrix.SetRotate(90);
                    break;
                case (int)Android.Media.Orientation.Transverse:
                    matrix.SetRotate(-90);
                    matrix.PostScale(-1, 1);
                    break;
                case (int)Android.Media.Orientation.Rotate270:
                    matrix.SetRotate(-90);
                    break;
                default:
                    return bitmap;
            }
            try
            {
                Android.Graphics.Bitmap bmRotated = Android.Graphics.Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
                bitmap.Recycle();
                return bmRotated;
            }
            catch (Java.Lang.OutOfMemoryError e)
            {
                e.PrintStackTrace();
                return null;
            }
        }
    }
}