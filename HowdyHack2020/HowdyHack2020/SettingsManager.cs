using HowdyHack2020.Core;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Threading.Tasks;

namespace HowdyHack2020
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class SettingsManager
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string DeviceIdKey = "settings_key";
        private static readonly string DeviceIdDefault = string.Empty;

        #endregion


        public static string DeviceId
        {
            get
            {
                return AppSettings.GetValueOrDefault(DeviceIdKey, DeviceIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(DeviceIdKey, value);
            }
        }
        public static async Task<string> EnsureAndGetDeviceId()
        {
            string deviceId = DeviceId;
            if (deviceId == DeviceIdDefault)
            {
                deviceId = Guid.NewGuid().ToString();
                switch (await Api.CreateUser(deviceId))
                {
                    case System.Net.HttpStatusCode.Conflict:
                        return await EnsureAndGetDeviceId();
                }
                DeviceId = deviceId;
            }
            return deviceId;
        }
    }
}