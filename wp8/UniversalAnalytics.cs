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
    public class UniversalAnalytics : BaseCommand
    {
        private string trackingId;
        private string userId;
        private bool debug = false;


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
            var config = new GoogleAnalytics.EasyTrackerConfig();
            config.TrackingId = trackingId;
            GoogleAnalytics.EasyTracker.Current.Config = config;
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
            GoogleAnalytics.EasyTracker.GetTracker().SendView(view);
        }

        public void trackEvent(string args)
        {
            string category = JsonHelper.Deserialize<string[]>(args)[0];
            string action = JsonHelper.Deserialize<string[]>(args)[1];
            string label = JsonHelper.Deserialize<string[]>(args)[2];
            string value = JsonHelper.Deserialize<string[]>(args)[3];
            long valueLong = long.Parse(value);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _trackEvent(category, action, label, valueLong);
            });
        }

        private void _trackEvent(string category, string action, string label, long value)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent(category, action, label, value);
        }
    }
}