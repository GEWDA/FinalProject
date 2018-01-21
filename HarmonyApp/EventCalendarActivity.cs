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
using Android.Provider;

using Android.Database;
using Java.Util;

namespace HarmonyApp
{
    [Activity(Label = "Event Calendar")]
    public class EventCalendarActivity : Activity
    {
        private CalendarView eventCalendar;
        private LinearLayout titleBar;
        private ListView eventList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Context.LayoutInflaterService;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventCalendar);
            InitialiseControls();
        }

        private void InitialiseControls()
        {
            titleBar = FindViewById<LinearLayout>(Resource.Id.linearLayoutH_TitleBar);
            titleBar.Click += TitleBar_Click;
            eventList = FindViewById<ListView>(Resource.Id.EventList1);
            eventList.UpdateViewLayout(FindViewById(Android.Resource.Layout.SimpleListItem2), null);

            eventCalendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            LoadEvents(eventCalendar);
            ListEvents(eventCalendar);
        }

        private void LoadEvents(CalendarView calendar)
        {
            var startDate = new DateTime(2018, 3, 9).Ticks;
            var endDate = new DateTime(2018, 3, 10).Ticks;
            ContentValues events = new ContentValues();
            events.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calendar.Id);
            events.Put(CalendarContract.Events.InterfaceConsts.Title, "Grace and Glory Conference");
            events.Put(CalendarContract.Events.InterfaceConsts.Dtstart,startDate);//gets the date in ms
            events.Put(CalendarContract.Events.InterfaceConsts.Dtend, endDate);
            
        }

        private void LoadEvents(CalendarView calendar, Uri eventListPage)
        {
            throw new NotImplementedException();//will eventually load events from a list online
        }

        private void ListEvents(CalendarView eventCalendar)
        {
            var myUri = CalendarContract.Events.ContentUri;
            string[] items = {
                CalendarContract.Events.InterfaceConsts.Id,
                CalendarContract.Events.InterfaceConsts.Title,
                CalendarContract.Events.InterfaceConsts.Dtstart };
            var loader = new CursorLoader(this, myUri, items, //load the values in items from events...
                "calendar_id="+eventCalendar.Id.ToString(), null, "dtstart ASC");//...from eventCalendar, with no arguments, and order by date
            var cursor = (ICursor)loader.LoadInBackground();

            string[] viewItems = {CalendarContract.Events.InterfaceConsts.Title,
                CalendarContract.Events.InterfaceConsts.Dtstart };

            //int[] viewData = { Resource.Id.//eve};
        }

        private void ListEvents(CalendarView eventCalendar, Uri eventListPage)
        {
            throw new NotImplementedException();//will eventually load events from a list online
        }

        private void TitleBar_Click(object sender, EventArgs e)
        {
            LaunchWebBrowser();
        }

        private void LaunchWebBrowser()
        {
            var uri = Android.Net.Uri.Parse("http://harmonychurch.nz/");
            var intent = new Intent(Intent.ActionView, uri);
            Log.Info("HarmonyApp", "Launching Web Browser");
            StartActivity(intent);

        }
    }
}