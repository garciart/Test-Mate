/*
 * The MIT License
 *
 * Copyright 2019 Rob Garcia at rgarcia@rgprogramming.com.
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
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        private static string[] settings = { App.questionOrder.ToString(), App.termDisplay.ToString(), App.provideFeedback.ToString() };

        /// <summary>
        /// Attempt to read and set stored settings. If the settings file is not found, it creates one with default values.
        /// </summary>
        /// <returns>Appropriate error message.</returns>
        public static string ReadSettingsFromFile() {
            try {
                if (File.Exists(Constants.SettingsFile)) {
                    // File.Delete(Constants.SettingsFile);
                    settings = File.ReadAllLines(Constants.SettingsFile, Encoding.UTF8);
                    Enum.TryParse(settings[0], out App.questionOrder);
                    Enum.TryParse(settings[1], out App.termDisplay);
                    Enum.TryParse(settings[2], out App.provideFeedback);
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
        public static string SaveSettingsToFile(Constants.QuestionOrder questionOrder, Constants.TermDisplay termDisplay, Constants.ProvideFeedback provideFeedback) {
            try {
                settings[0] = questionOrder.ToString();
                settings[1] = termDisplay.ToString();
                settings[2] = provideFeedback.ToString();
                File.WriteAllLines(Constants.SettingsFile, settings, Encoding.UTF8);
                return string.Format(AppResources.SettingsSaveSuccessMessage);
            }
            catch (Exception e) {
                return string.Format(AppResources.SettingsSaveErrorMessage, e.Message);
            }
        }

        /// <summary>
        /// Thanks to Daxton47 at https://forums.xamarin.com/discussion/138266/how-to-hit-an-url-to-download-a-file
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static async Task<byte[]> DownloadFileAsync(string fileUrl) {
            var _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };

            try {
                using (var httpResponse = await _httpClient.GetAsync(fileUrl)) {
                    if (httpResponse.StatusCode == HttpStatusCode.OK) {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else {
                        // Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception) {
                //Handle Exception
                return null;
            }
        }
    }
}
