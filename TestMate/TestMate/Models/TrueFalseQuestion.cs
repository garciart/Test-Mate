/*
 * TestMate model class for true/false data objects.
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
using TestMate.Common;

namespace TestMate.Models
{
    /// <summary>
    /// TestMate model class for true/false data objects.
    /// </summary>
    public class TrueFalseQuestion : Question
    {
        private string question = "";

        /// <summary>
        /// The true or false question; cannot be null.
        /// </summary>
        public string Question {
            get {
                return question;
            }
            set {
                question = value ?? throw new ArgumentNullException("True or false questions cannot be null or empty.");
            }
        }

        /// <summary>
        /// The true or false answer; default value is true;
        /// </summary>
        public bool Answer { get; set; } = true;

        public TrueFalseQuestion()
        {
            QuestionType = Constants.QuestionType.T;
        }
    }
}
