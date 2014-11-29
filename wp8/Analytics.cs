using System;
using System.Windows;
using System.Runtime.Serialization;
using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;
using System.Diagnostics; //Debug.WriteLine
//
using GoogleAnalytics.Core;

namespace Cordova.Extension.Commands
{
    public class Analytics : BaseCommand
    {
        private string trackingId;
        private string userId;
        private bool debug = false;
        private TrackerManager trackerManager;
        private Tracker tracker;


        public void startTrackerWithId(string args)
        {
            string trackingId = JsonHelper.Deserialize<string[]>(args)[0];

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _startTrackerWithId(trackingId);

                PluginResult pr = new PluginResult(PluginResult.Status.OK);
                DispatchCommandResult(pr);
            });
        }

        private void _startTrackerWithId(string trackingId)
        {
            this.trackingId = trackingId;
            this.tracker = trackerManager.GetTracker(this.trackingId); // saves as default
            this.tracker.AppName = "My app";
            this.tracker.AppVersion = "1.0.0.0";
        }

        public void setUserId(string args)
        {
            string userId = JsonHelper.Deserialize<string[]>(args)[0];

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _setUserId(userId);

                PluginResult pr = new PluginResult(PluginResult.Status.OK);
                DispatchCommandResult(pr);
            });
        }

        private void _setUserId(string userId)
        {
            this.userId = userId;
            this.trackerManager = new TrackerManager(new PlatformInfoProvider()
            {
                AnonymousClientId = this.userId,
                ScreenResolution = new Dimensions(1920, 1080),
                UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko",
                UserLanguage = "en-us",
                ViewPortResolution = new Dimensions(1920, 1080)
            });
        }

        public void debugMode(string args)
        {

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _debugMode();

                PluginResult pr = new PluginResult(PluginResult.Status.OK);
                DispatchCommandResult(pr);
            });
        }

        private void _debugMode()
        {
            this.debug = true;
        }

        public void trackView(string args)
        {
            string view = JsonHelper.Deserialize<string[]>(args)[0];

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _trackView(view);
            });
        }

        private void _trackView(string view)
        {
            this.tracker.SendView(view);
        }
    }
}