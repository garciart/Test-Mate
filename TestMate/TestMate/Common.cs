using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TestMate.Resources;

namespace TestMate {
    class Common {

        // The settings file is in the local application data folder
        private static string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestMate.ini");
        /* Default configuration settings
         * 1 - Use the default question order.
         * 1 - Use the key term for the question and a set of possible definitions for the choices.
         * 1 - Provide feedback immediately.
         */
        public static string[] configContents = { "0", "0", "0" };
        public static bool enableAppFlag = true;

        public static string ReadSettingsFromFile() {
            try {
                if (File.Exists(settingsFile)) {
                    // File.Delete(settingsFile);
                    configContents = File.ReadAllLines(settingsFile, Encoding.UTF8);
                    return null;
                }
                else {
                    File.WriteAllLines(settingsFile, configContents, Encoding.UTF8);
                    return AppResources.SettingsMissingErrorMessage;
                }
            } catch (Exception e) {
                return String.Format(AppResources.SettingsReadErrorMessage, e.Message);
            }
        }

        public static string SaveSettingsToFile() {
            try {
                File.WriteAllLines(settingsFile, configContents, Encoding.UTF8);
                return String.Format(AppResources.SettingsSaveSuccessMessage);
            }
            catch (Exception e) {
                return String.Format(AppResources.SettingsSaveErrorMessage, e.Message);
            }
        }
    }
}
