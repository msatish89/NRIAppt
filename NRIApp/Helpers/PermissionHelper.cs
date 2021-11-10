﻿using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace NRIApp.Helpers
{
    public static class PermissionHelper
    {
        public static async Task<bool> CheckPermission(Permission permission)
        {
            try
            {
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
               
                //bool request = false;
                //if (permissionStatus == PermissionStatus.Denied)
                //{
                //    if (Device.RuntimePlatform == Device.iOS)
                //    {
                //        var title = $"{permission} Permission";
                //        var question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                //        var positive = "Settings";
                //        var negative = "Maybe Later";
                //        var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                //        if (task == null)
                //            return false;

                //        var result = await task;
                //        if (result)
                //        {
                //            CrossPermissions.Current.OpenAppSettings();
                //        }

                //        return false;
                //    }

                //    request = true;

                //}

                //if (request || permissionStatus != PermissionStatus.Granted)
                //{
                //    var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                //    if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                //    {
                var title = $"{permission} Permission";
                        var question = $"To use the plugin the {permission} permission is required.";
                        var positive = "Settings";
                        var negative = "Maybe Later";
                        var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                        if (task == null)
                            return false;

                        var result = await task;
                        if (result)
                        {
                    DependencyService.Get<IKeyboardHelper>().Gotosettings();
                        }
                        return false;
                //    }
                //}
                //return true;
            }
            catch (Exception e)
            {
                string Error = e.Message + e.StackTrace;
                return false;
            }
        }
    }
}
