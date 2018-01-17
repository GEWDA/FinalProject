using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Content;
using Android.Util;
using Android.Runtime;
using Android.Views;


namespace HarmonyApp
{
    [Activity(Label = "Event Calendar")]
    public class EventCalendarActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventCalendar);
            // Create your application here
        }
    }
}