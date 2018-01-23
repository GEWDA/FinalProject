using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Graphics;
using Android.Content;
using Android.Util;

namespace HarmonyApp
{
    [Activity(Label = "Harmony App", MainLauncher = true,Theme = "@style/Theme.Custom",Icon ="@drawable/temporary_logo")]
    public class MainActivity : Activity
    {
        private Button btnLaunchResonate;
        private Button btnLaunchEventCalendar;
        private LinearLayout titleBar;
        public Typeface ParagraphFont;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Log.Info("HarmonyApp", "Loading Main View");
            SetContentView(Resource.Layout.Main);
            Log.Info("HarmonyApp", "Initializing controls");
            InitializeControls();
        }

        private void InitializeControls()
        {
            
            btnLaunchResonate = FindViewById<Button>(Resource.Id.buttonLaunchResonate);
            btnLaunchResonate.Click += BtnLaunchResonate_Click;
            btnLaunchEventCalendar = FindViewById<Button>(Resource.Id.buttonLaunchEventCalendar);
            btnLaunchEventCalendar.Click += BtnLaunchEventCalendar_Click;
            titleBar = FindViewById<LinearLayout>(Resource.Id.linearLayoutH_TitleBar);
            titleBar.Click += TitleBar_Click;
            ActionBar.Title = null;
            ActionBar.Hide();
            //ParagraphFont = Typeface.CreateFromAsset(Assets, "fonts/paragraph.ttf");//missing the file currently
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

        private void BtnLaunchResonate_Click(object sender, EventArgs e)
        {
            StartResonate();
        }

        public void StartResonate()
        {
            Intent startResonate = new Intent(this, typeof(ResonateActivity));
            StartActivity(startResonate);
            Log.Info("HarmonyApp", "Beginning Resonate Activity");
            //potentially launch Resonate service here
        }

        private void BtnLaunchEventCalendar_Click(object sender, EventArgs e)
        {
            StartCalendar();
        }

        public void StartCalendar()
        {
            Intent startCalendar = new Intent(this, typeof(EventCalendarActivity));
            StartActivity(startCalendar);
            Log.Info("HarmonyApp", "Beginning Event Calendar Activity");
            //potentially launch Resonate service here
        }
    }
}

