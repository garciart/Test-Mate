/*
 * The MIT License
 *
 * Copyright 2018 Rob Garcia at rgarcia@rgprogramming.com.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using System.IO;
using System.Text;
using TestMate.Resources;

namespace TestMate {
    class Common {
        // The location of the settings file and test files
        public static string AppDataPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // The settings file is in the local application data folder
        private static readonly string settingsFile = Path.Combine(AppDataPath, "TestMate.ini");

        /* 
         * Default configuration settings
         * 0 - Use the default question order.
         * 0 - Use the key term for the question and a set of possible definitions for the choices.
         * 0 - Provide feedback immediately.
         */
        public static string[] settings = { "0", "0", "0" };

        // All UI items (e.g., buttons, etc.) are enabled by default
        public static bool enableAppFlag = true;

        /// <summary>
        /// Attempt to read and set stored settings. If the settings file is not found, it creates one with default values
        /// </summary>
        /// <returns>Appropriate error message</returns>
        public static string ReadSettingsFromFile() {
            try {
                if (File.Exists(settingsFile)) {
                    // File.Delete(settingsFile);
                    settings = File.ReadAllLines(settingsFile, Encoding.UTF8);
                    return null;
                }
                else {
                    File.WriteAllLines(settingsFile, settings, Encoding.UTF8);
                    return AppResources.SettingsMissingErrorMessage;
                }
            } catch (Exception e) {
                return String.Format(AppResources.SettingsReadErrorMessage, e.Message);
            }
        }

        /// <summary>
        /// Attempt to save settings to file
        /// </summary>
        /// <returns>Appropriate error message</returns>
        public static string SaveSettingsToFile() {
            try {
                File.WriteAllLines(settingsFile, settings, Encoding.UTF8);
                return String.Format(AppResources.SettingsSaveSuccessMessage);
            }
            catch (Exception e) {
                return String.Format(AppResources.SettingsSaveErrorMessage, e.Message);
            }
        }
    }
}
