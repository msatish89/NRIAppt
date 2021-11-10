using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace NRIApp.Helpers
{
    public class Commonsettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string TechjobsAPI = "https://techjobs.sulekha.com/mobileapp/techjobs";
        public static string USHomeAPI = "https://techjobs.sulekha.com/mobileapp/ushome";
        public static string Localservices = "https://techjobs.sulekha.com/MobileApp/LS";
        public static string RoommatesAPI = "https://techjobs.sulekha.com/mobileapp/roommates";
        public static string DaycareAPI = "https://techjobs.sulekha.com/mobileapp/daycare";
        public static string LocaljobsAPI = "https://techjobs.sulekha.com/mobileapp/localjobs";
        public static string EventsAPI = "https://techjobs.sulekha.com/mobileapp/events";
        public static string isloadedlocation = "0";
        public static string isadpage = "0";


        #region GoogleAPIkey
        public static string GoogleAPIkey = "AIzaSyCHfoI6em7Q7jAiwX2PaAwzGOA6EABpsZ4";
        #endregion

        public static int CAdTypeID;
        public static string CRMCategory;
        public static int CRTAdTypeID;
        public static string CRTCategory;
        public static string CategoryType;

        #region Setting Constants
        const string UserPidKey = "UserPid";
        const string UserIdKey = "UserId";
        const string UserNameKey = "UserName";
        const string UserEmailKey = "UserEmail";
        const string UserMobilenoKey = "UserMobileno";
        const string UserLatKey = "UserLat";
        const string UserLongKey = "UserLong";
        const string UsercityKey = "Usercity";
        const string UserzipcodeKey = "Userzipcode";
        const string UsercountrycodeKey = "Usercountry";
        const string UserstatecodeKey = "Userstatecode";
        const string UserstateKey = "Userstate";
        const string UserMobileOSKey = "UserMobileOS";
        const string UsercityurlKey = "Usercityurl";
        const string UsermetrourlKey = "Usermetrourl";
        const string UsermetroKey = "Usermetro";
        const string UserDeviceIdKey = "UserDeviceId";
        const string UserDeviceNameKey = "UserDeviceName";
        const string UserDeviceModelKey = "UserDeviceModel";
        const string UserDeviceOSVersionKey = "UserDeviceOSVersion";
        const string AzureBlobKey = "AzureBlobStorgekey";
        const string UserDeviceHeightKey = "UserDeviceHeight";
        const string UserDeviceWidthKey = "UserDeviceWidth";
        #endregion
        const string UserappversionKey = "Userappversion";

        public static string Userappversion
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserappversionKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserappversionKey, value);
            }
        }
        public static string UserDeviceOSVersion
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserDeviceOSVersionKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserDeviceOSVersionKey, value);
            }
        }
        public static string UserDeviceModel
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserDeviceModelKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserDeviceModelKey, value);
            }
        }
        public static string UserDeviceId
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserDeviceNameKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserDeviceNameKey, value);
            }
        }
        public static string UserDeviceName
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserDeviceIdKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserDeviceIdKey, value);
            }
        }

        public static string Usermetro
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UsermetroKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsermetroKey, value);
            }
        }
        public static string Usermetrourl
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UsermetrourlKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsermetrourlKey, value);
            }
        }

        public static string Usercityurl
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UsercityurlKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsercityurlKey, value);
            }
        }

        public static string UserMobileOS
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserMobileOSKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserMobileOSKey, value);
            }
        }

        public static string Userstatecode
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserstatecodeKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserstatecodeKey, value);
            }
        }
        public static string Userstate
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserstateKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserstateKey, value);
            }
        }
        public static string Userzipcode
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserzipcodeKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserzipcodeKey, value);
            }
        }

        public static string Usercity
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UsercityKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsercityKey, value);
            }
        }

        public static string Usercountry
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UsercountrycodeKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsercountrycodeKey, value);
            }
        }
        public static double UserLat
        {
            get
            {
                return (double)AppSettings.GetValueOrDefault(UserLatKey, default(double));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserLatKey, value);
            }
        }
        public static double UserLong
        {
            get
            {
                return (double)AppSettings.GetValueOrDefault(UserLongKey, default(double));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserLongKey, value);
            }
        }


        public static string UserPid
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserPidKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserPidKey, value);
            }
        }
        public static string UserId
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserIdKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdKey, value);
            }
        }
        public static string UserName
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserNameKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }
        public static string UserEmail
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserEmailKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserEmailKey, value);
            }
        }

        public static string UserMobileno
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserMobilenoKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserMobilenoKey, value);
            }
        }
        public static string AzureBlobStorgekey
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(AzureBlobKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(AzureBlobKey, value);
            }
        }
        public static string UserDeviceHeight
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserDeviceHeightKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserDeviceHeightKey, value);
            }
        }
        public static string UserDeviceWidth
        {
            get
            {
                return (string)AppSettings.GetValueOrDefault(UserDeviceWidthKey, default(string));
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserDeviceWidthKey, value);
            }
        }

    }
}
