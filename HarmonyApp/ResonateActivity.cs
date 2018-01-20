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
    [Activity(Label ="Resonate")]
    public class ResonateActivity : Activity
    {
        private Button btnOpenExternal;
        private LinearLayout titleBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Resonate);
            InitialiseControls();
            titleBar = FindViewById<LinearLayout>(Resource.Id.linearLayoutH_TitleBar);
            titleBar.Click += TitleBar_Click;
        }

        private void InitialiseControls()
        {
            btnOpenExternal = FindViewById<Button>(Resource.Id.buttonExternalSermonAccess);
            btnOpenExternal.Click += BtnOpenExternal_Click;
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
        private void BtnOpenExternal_Click(object sender, EventArgs e)
        {
            LaunchResonate();
        }

        public void LaunchResonate()
        {
            var uri = Android.Net.Uri.Parse("https://itunes.apple.com/nz/podcast/harmony-church/id676872916?mt=2");
            var intent = new Intent(Intent.ActionView, uri);
            Log.Info("HarmonyApp", "Launching Web Browser to Itunes Page");
            StartActivity(intent);
        }
    }
}