/*
 * TestMate model abstract class for test data objects.
 *
 * .NET Standard version used: 2.0
 * C# version used: 7.3
 *
 * Styling guide: .NET Core Engineering guidelines
 *     (https://github.com/dotnet/aspnetcore/wiki/Engineering-guidelines#coding-guidelines) and
 *     C# Programming Guide
 *     (https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
 *
 * @category  Testmate
 * @package   TestMate
 * @author    Rob Garcia <rgarcia@rgprogramming.com>
 * @license   https://opensource.org/licenses/MIT The MIT License
 * @link      https://github.com/garciart/TestMate
 * @copyright 1993-2020 Rob Garcia
 */

using System;
using System.Text.RegularExpressions;
using TestMate.Common;

namespace TestMate.Models
{
    /// <summary>
    /// TestMate model abstract class for test data objects.
    /// </summary>
    public abstract class Question
    {
        /// <summary>
        /// The question type; K for Key Term, M for Multiple Choice, T for True or False.
        /// </summary>
        public Constants.QuestionType QuestionType { get; set; }

        /// <summary>
        /// The media type; N for none, I for images, A for audio files, and V for video files.
        /// </summary>
        public Constants.MediaType MediaType { get; set; }

        /// <summary>
        /// The media file name; must be null or a name.
        /// </summary>
        public string MediaFileName { get; set; } = null;

        /// <summary>
        /// The explanation for feedback. Can be temporarily null; multiple choice questions populate this field during construction, while key term and true/false questions populate this field when the test is built.
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Method to validate the media can be read by the application.
        /// </summary>
        /// <param name="mediaType">The media type; N for none, I for images, A for audio files, and V for video files.</param>
        /// <param name="mediaFileName">The media file name.</param>
        public void ValidateAndSetMedia(Constants.MediaType mediaType, string mediaFileName)
        {
            if (mediaType == Constants.MediaType.N)
            {
                if (!mediaFileName.ToLowerInvariant().Equals("null"))
                {
                    throw new ArgumentException("Filename should be NULL.");
                }
            }
            else
            {
                if (mediaFileName.ToLowerInvariant().Equals("null") || String.IsNullOrEmpty(mediaFileName))
                {
                    throw new ArgumentException("Missing media file name.");
                }
                switch (mediaType)
                {
                    case Constants.MediaType.A:
                        {
                            Regex regex = new Regex(@"^[\w\- ]+(.mp3)$");
                            if (!regex.Match(mediaFileName).Success)
                            {
                                throw new ArgumentException("Media format not supported for that media type.");
                            }
                            break;
                        }
                    case Constants.MediaType.I:
                        {
                            Regex regex = new Regex(@"^[\w\- ]+(.jpg|.png)$");
                            if (!regex.Match(mediaFileName).Success)
                            {
                                throw new ArgumentException("Media format not supported for that media type.");
                            }
                            break;
                        }
                    case Constants.MediaType.V:
                        {
                            Regex regex = new Regex(@"^[\w\- ]+(.mpg|.mpeg|.mp4)$");
                            if (!regex.Match(mediaFileName).Success)
                            {
                                throw new ArgumentException("Media format not supported for that media type.");
                            }
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Unsupported media type.");
                        }
                }
                MediaType = mediaType;
                MediaFileName = mediaFileName;
            }
        }
    }
}
