using System;
using System.IO;
using System.Text;
using TestMate.Resources;

namespace TestMate.Common {
    /// <summary>
    /// Non-data access functions used throughout the application.
    /// </summary>
    public static class AppFunctions {
        /// <summary>
        /// Default configuration settings
        /// 0 (Use the default question order),
        /// 0 (Use the key term for the question and a set of possible definitions for the choices),
        /// 0 (Provide feedback immediately).
        /// </summary>
        public static string[] settings = { "0", "0", "0" };

        /// <summary>
        /// All UI items (e.g., buttons, etc.) are enabled by default.
        /// </summary>
        public static bool enableAppFlag = true;

        /// <summary>
        /// Attempt to read and set stored settings. If the settings file is not found, it creates one with default values.
        /// </summary>
        /// <returns>Appropriate error message.</returns>
        public static string ReadSettingsFromFile() {
            try {
                if (File.Exists(Constants.SettingsFile)) {
                    // File.Delete(Constants.SettingsFile);
                    settings = File.ReadAllLines(Constants.SettingsFile, Encoding.UTF8);
                    return null;
                }
                else {
                    File.WriteAllLines(Constants.SettingsFile, settings, Encoding.UTF8);
                    return AppResources.SettingsMissingErrorMessage;
                }
            }
            catch (Exception e) {
                return string.Format(AppResources.SettingsReadErrorMessage, e.Message);
            }
        }

        /// <summary>
        /// Attempt to save settings to file.
        /// </summary>
        /// <returns>Appropriate error message.</returns>
        public static string SaveSettingsToFile() {
            try {
                File.WriteAllLines(Constants.SettingsFile, settings, Encoding.UTF8);
                return string.Format(AppResources.SettingsSaveSuccessMessage);
            }
            catch (Exception e) {
                return string.Format(AppResources.SettingsSaveErrorMessage, e.Message);
            }
        }
    }
}
