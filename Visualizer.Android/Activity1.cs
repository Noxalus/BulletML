using System.Xml;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Visualizer.Android
{
    [Activity(Label = "Visualizer.Android"
         , MainLauncher = true
         , Icon = "@drawable/icon"
         , Theme = "@style/Theme.Splash"
         , AlwaysRetainTaskState = true
         , LaunchMode = LaunchMode.SingleInstance
         , ScreenOrientation = ScreenOrientation.SensorLandscape
         ,
         ConfigurationChanges =
             ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden |
             ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new Game1(this);
            SetContentView((View) g.Services.GetService(typeof(View)));
            g.Run();
        }

        public string[] ListFile(string folder)
        {
            return Assets.List(folder);
        }

        public XmlReader OpenXmlResourceParser(string file)
        {
            return Assets.OpenXmlResourceParser(file);
        }
    }
}