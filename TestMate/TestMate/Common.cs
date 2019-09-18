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
        public static string[] configContents = { "1", "1", "1" };

        public static string readSettingsFromFile() {
            try {
                if (File.Exists(settingsFile)) {
                    // File.Delete(settingsFile);
                    configContents = File.ReadAllLines(settingsFile, Encoding.UTF8);
                    return null;
                }
                else {
                    File.WriteAllLines(settingsFile, configContents, Encoding.UTF8);
                    return AppResources.settingsReadError;
                }
            } catch (Exception e) {
                return String.Format(AppResources.settingsSaveFail, e.Message);
            }
        }

        public static string saveSettingsToFile() {
            try {
                File.WriteAllLines(settingsFile, configContents, Encoding.UTF8);
                return String.Format(AppResources.settingsSavePass);
            }
            catch (Exception e) {
                return String.Format(AppResources.settingsSaveFail, e.Message);
            }
        }
    }
}
