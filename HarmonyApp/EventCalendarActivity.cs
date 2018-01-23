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
using Java.Lang;

namespace HarmonyApp
{
    [Activity(Label = "Event Calendar")]
    public class EventCalendarActivity : Activity
    {

        private class MyAdapter : BaseAdapter
        {
            private System.Object[][] items;
            private Context myContext;

            public MyAdapter(System.Object[][] items, Context context)
            {
                this.items = items;
                this.myContext = context;
            }

            public override int Count => items.Length;


            public override Java.Lang.Object GetItem(int position)//unused
            {
                throw new NotImplementedException();
            }

            public override long GetItemId(int position)//unused
            {
                throw new NotImplementedException();
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var myInflator = (LayoutInflater)myContext.GetSystemService(LayoutInflaterService);
                
                var myView=myInflator.Inflate(Resource.Layout.EventListView, parent, false);
                var eventTitle=myView.FindViewById<TextView>(Resource.Id.eventTitle);
                var eventStartDate = myView.FindViewById<TextView>(Resource.Id.eventStartDate);
                eventTitle.Text = items[position][0].ToString();
                try
                {
                    eventStartDate.Text = Convert.ToDateTime(items[position][1]).ToString(@"dd/MM/yyyy hh:mm:ss tt");//try to put the date to the correct format
                }
                catch
                {
                    eventStartDate.Text = items[position][1].ToString();//if it is not a date, just show it
                    Log.Warn("HarmonyApp", "An event does not have a valid DateTime");

                }

                return myView;
            }
        }
        
        public System.Object[][] myList;

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
            //eventList.Adapter = new MyAdapter();

            eventCalendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            LoadEvents(eventCalendar);
            ListEvents(eventCalendar);
            var test = titleBar;

        }

        private void LoadEvents(CalendarView calendar)
        {

            //string[] items = { "Grace and Glory Conference", new DateTime(2018, 3, 9,17,0,0).ToString(), "Night Services Begin", new DateTime(2018, 2, 11,18,0,0).ToString(),"Wild, Strong, and Free Women's Conference","Date to be Confirmed" };//temporary solution while i await the proper list
            System.Object[][] items = {
                new System.Object[] { "Grace and Glory Conference", new DateTime(2018, 3, 9, 17, 0, 0) },
                new System.Object[] { "Night Services Begin", new DateTime(2018, 2, 11, 18, 0, 0) },
                new System.Object[]{ "Wild, Strong, and Free Women's Conference", "Date to be Confirmed" } };
            myList = items;
            //var startDate = new DateTime(2018, 3, 9).Ticks;
            //var endDate = new DateTime(2018, 3, 10).Ticks;

            //ContentValues events = new ContentValues();
            //events.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calendar.Id);
            //events.Put(CalendarContract.Events.InterfaceConsts.Title, "Grace and Glory Conference");
            //events.Put(CalendarContract.Events.InterfaceConsts.Dtstart,startDate);//gets the date in ms
            //events.Put(CalendarContract.Events.InterfaceConsts.Dtend, endDate);
            //string[][] items = { new string[]{ "Grace and Glory Conference", new DateTime(2018, 3, 9).ToString() },new string[] { "Night Services Begin", new DateTime(2018, 2, 11).ToString() } };


            //var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, events);
            //ArrayAdapter<string[]> adapter = new ArrayAdapter<string[]>(this,Resource.Layout.EventListView,myList);
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.EventListView, myList);
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myList);
            var adapter = new MyAdapter(items,this);
            eventList.Adapter = adapter;            
        }

        private void LoadEvents(CalendarView calendar, Uri eventListPage)
        {
            throw new NotImplementedException();//will eventually load events from a list online
        }

        private void ListEvents(CalendarView eventCalendar)
        {
            //var myUri = CalendarContract.Events.ContentUri;
            //string[] items = {
            //    CalendarContract.Events.InterfaceConsts.Id,
            //    CalendarContract.Events.InterfaceConsts.Title,
            //    CalendarContract.Events.InterfaceConsts.Dtstart };
            //var loader = new CursorLoader(this, myUri, myList, //load the values in myList from events...
            //    null, null, null);//...from eventCalendar, with no arguments, and order by date//String.Format("calendar_id={0}",eventCalendar.Id)//"dtstart ASC"
            ////var cursor = (ICursor)loader.LoadInBackground();//FIX

            //string[] viewItems = {CalendarContract.Events.InterfaceConsts.Title,
            //    CalendarContract.Events.InterfaceConsts.Dtstart };
            //int[] viewData = { Resource.Id.eventTitle,Resource.Id.eventStartDate};
            ////var adapter = new SimpleCursorAdapter(this, Resource.Layout.EventListView, cursor, viewItems, viewData);

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