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
    public class Admob : BaseCommand
    {
        private string trackingId;
        private string userId;
        private bool debugMode = false;


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
            config.trackingId = "UA-XXXX-Y";
            config.appName = "Test App";
            config.appVersion = "1.0.0.0";
            GoogleAnalytics.EasyTracker.current.config = config;
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
            this.debugMode = true;
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
            GoogleAnalytics.EasyTracker.getTracker().sendView(view);
        }
    }
}