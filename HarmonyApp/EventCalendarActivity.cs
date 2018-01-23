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
            public System.Object GoToDate { get;internal set; }

            public MyAdapter(System.Object[][] items, Context context)
            {
                this.items = items;
                this.myContext = context;
            }

            public override int Count => items.Length;


            public override Java.Lang.Object GetItem(int position)//unused
            {
                return "THIS METHOD SHOULD NOT BE USED";
            }

            public override long GetItemId(int position)//unused
            {
                //DateTime actualDateTime;
                //try
                //{
                //    actualDateTime = (DateTime)items[position][1];
                //    GoToDate = actualDateTime.Ticks;//needs to be tested
                //}
                //catch
                //{
                //    Log.Warn("HarmonyApp", "Selected event has no DateTime");
                //}
                return position;
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
                    Log.Warn("HarmonyApp", "An event at position "+position.ToString()+" does not have a valid DateTime");

                }
                return myView;
            }
        }
        
        public System.Object[][] myList;
        //private long currentDateTicks;
        private CalendarView eventCalendar;
        private LinearLayout titleBar;
        private ListView eventList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EventCalendar);
            InitialiseControls();
        }

        private void InitialiseControls()
        {
            titleBar = FindViewById<LinearLayout>(Resource.Id.linearLayoutH_TitleBar);
            titleBar.Click += TitleBar_Click;
            eventList = FindViewById<ListView>(Resource.Id.EventList1);
            
            eventCalendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            LoadEvents(eventCalendar);
            //currentDateTicks = eventCalendar.Date;

        }

        private void LoadEvents(CalendarView calendar)
        {

            //string[] items = { "Grace and Glory Conference", new DateTime(2018, 3, 9,17,0,0).ToString(), "Night Services Begin", new DateTime(2018, 2, 11,18,0,0).ToString(),"Wild, Strong, and Free Women's Conference","Date to be Confirmed" };//temporary solution while i await the proper list
            System.Object[][] items = {
                new System.Object[] { "Grace and Glory Conference", new DateTime(2018, 3, 9, 17, 0, 0) },
                new System.Object[] { "Night Services Begin", new DateTime(2018, 2, 11, 18, 0, 0) },
                new System.Object[]{ "Wild, Strong, and Free Women's Conference", "Date to be Confirmed" } };
            myList = items;
            var adapter = new MyAdapter(items,this);
            eventList.Adapter = adapter;
            //eventList.ItemClick += EventList_ItemClick;
        }

        //private void EventList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    var mySender = (ListView)sender;
        //    var something = mySender.Adapter.GetView(e.Position, new TextView(this), mySender);
        //    DateTime actualDateTime;
        //    try
        //    {
        //        actualDateTime = (DateTime)myList[e.Position][1];
        //        Log.Info("HarmonyApp", "Going to date: "+actualDateTime.ToString(@"dd/MM/yyyy hh:mm:ss tt"));

        //        eventCalendar.Date = actualDateTime.Ticks- (long)6.365606e+17;///500000;//????? why is the division necessary????00000000000000000
        //        //GoToDate = actualDateTime.Ticks;//needs to be tested
        //    }
        //    catch
        //    {
        //        Log.Warn("HarmonyApp", "Selected event has no DateTime");
        //    }
            
        //}

        private void LoadEvents(CalendarView calendar, Uri eventListPage)
        {
            throw new NotImplementedException();//will eventually load events from a list online
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