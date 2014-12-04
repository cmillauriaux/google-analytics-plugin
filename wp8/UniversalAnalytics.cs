using System;
using System.Windows;
using System.Runtime.Serialization;
using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;
using System.Diagnostics; //Debug.WriteLine
//
using GoogleAnalytics.Core;
using Windows.ApplicationModel;
using GoogleAnalytics;

namespace Cordova.Extension.Commands
{
    public class UniversalAnalytics : BaseCommand
    {
        private string trackingId;
        private string userId;
        private bool debug = false;
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
            var trackerConfig = new GoogleAnalytics.EasyTrackerConfig();
            trackerConfig.TrackingId = this.trackingId;
            GoogleAnalytics.EasyTracker.Current.Config = trackerConfig;
            this.tracker = GoogleAnalytics.EasyTracker.GetTracker();
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