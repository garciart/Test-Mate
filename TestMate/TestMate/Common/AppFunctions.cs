using System;
using System.IO;
using System.Text;
using TestMate.Resources;

namespace TestMate.Common {
    public static class AppFunctions {
        /// <summary>
        /// Attempt to read and set stored settings. If the settings file is not found, it creates one with default values
        /// </summary>
        /// <returns>Appropriate error message</returns>
        public static string ReadSettingsFromFile() {
            try {
                if (File.Exists(Constants.settingsFile)) {
                    // File.Delete(settingsFile);
                    Constants.settings = File.ReadAllLines(Constants.settingsFile, Encoding.UTF8);
                    return null;
                }
                else {
                    File.WriteAllLines(Constants.settingsFile, Constants.settings, Encoding.UTF8);
                    return AppResources.SettingsMissingErrorMessage;
                }
            }
            catch (Exception e) {
                return String.Format(AppResources.SettingsReadErrorMessage, e.Message);
            }
        }

        /// <summary>
        /// Attempt to save settings to file
        /// </summary>
        /// <returns>Appropriate error message</returns>
        public static string SaveSettingsToFile() {
            try {
                File.WriteAllLines(Constants.settingsFile, Constants.settings, Encoding.UTF8);
                return String.Format(AppResources.SettingsSaveSuccessMessage);
            }
            catch (Exception e) {
                return String.Format(AppResources.SettingsSaveErrorMessage, e.Message);
            }
        }
    }
}
