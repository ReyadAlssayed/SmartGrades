using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace SmartGrades
{
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        ConfigurationChanges =
            ConfigChanges.ScreenSize |
            ConfigChanges.Orientation |
            ConfigChanges.UiMode |
            ConfigChanges.ScreenLayout |
            ConfigChanges.SmallestScreenSize |
            ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // جعل شريط الحالة أبيض
            Window.SetStatusBarColor(Android.Graphics.Color.White);

            // جعل الأيقونات سوداء
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Window.DecorView.SystemUiVisibility =
                    (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
        }
    }
}
