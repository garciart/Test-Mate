/*
 * TestMate model class to capture results in a data object.
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

namespace TestMate.Models
{
    /// <summary>
    /// TestMate model class to capture results in a data object.
    /// </summary>
    public class Result
    {
        private string question = "";

        public string SelectedItem { get; set; } = null;

        public string Question {
            get {
                return question;
            }
            set {
                question = value ?? throw new ArgumentNullException("Question cannot be null or empty.");
            }
        }

        public bool CorrectFlag { get; set; } = false;

        public string Correct { get; set; } = "Not Correct.";

        public Result(string question, string selectedItem, bool correctFlag)
        {
            Question = question;
            SelectedItem = selectedItem;
            CorrectFlag = correctFlag;
        }
    }
}
