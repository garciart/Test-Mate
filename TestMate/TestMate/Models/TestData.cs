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
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using TestMate.Common;
using System.Text.RegularExpressions;

namespace TestMate.Models {
    /**
     * TestMate model abstract class for test data objects
     *
     * @author Rob Garcia at rgarcia@rgprogramming.com
     */
    public abstract class TestData {
        public Constants.QuestionType questionType { get; set; }
        public Constants.MediaType mediaType { get; set; }
        public string mediaFileName { get; set; }
        public string explanation { get; set; }

        /// <summary>
        /// Method to validate the media can be read by the application
        /// </summary>
        /// <param name="mediaType">The media type; N for none, I for images, A for audio files, and V for video files</param>
        /// <param name="mediaFileName">The media file name</param>
        protected void validateAndSetMedia(Constants.MediaType mediaType, string mediaFileName) {
            if (mediaType == Constants.MediaType.N) {
                if (!mediaFileName.ToLowerInvariant().Equals("null")) {
                    throw new ArgumentException("Filename should be NULL.");
                }
            }
            else {
                if (mediaFileName.ToLowerInvariant().Equals("null") || String.IsNullOrEmpty(mediaFileName)) {
                    throw new ArgumentException("Missing media file name.");
                }
                switch (mediaType) {
                    case Constants.MediaType.I: {
                            Regex regex = new Regex(@"^[\\w\\- ]+(.jpg|.png)$");
                            if (!regex.Match(mediaFileName).Success) {
                                throw new ArgumentException("Media format not supported for that media type.");
                            }
                            break;
                        }
                    case Constants.MediaType.A: {
                            Regex regex = new Regex(@"^[\\w\\- ]+(.mp3)$");
                            if (!regex.Match(mediaFileName).Success) {
                                throw new ArgumentException("Media format not supported for that media type.");
                            }
                            break;
                        }
                    case Constants.MediaType.V: {
                            Regex regex = new Regex(@"^[\\w\\- ]+(.mpg|.mpeg|.mp4)$");
                            if (!regex.Match(mediaFileName).Success) {
                                throw new ArgumentException("Media format not supported for that media type.");
                            }
                            break;
                        }
                    default: {
                            throw new ArgumentException("Unsupported media type.");
                        }
                }
                this.mediaType = mediaType;
                this.mediaFileName = mediaFileName;
            }
        }
    }
}
