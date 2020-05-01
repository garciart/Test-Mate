/*
 * TestMate model class for multiple choice data objects.
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
using System.Collections.Generic;
using TestMate.Common;

namespace TestMate.Models
{
    /// <summary>
    /// TestMate model class for multiple choice data objects.
    /// </summary>
    public class MultipleChoiceQuestion : Question
    {
        private string question = "";
        private int numberOfChoices = 0;
        private List<string> choices = new List<string>();

        /// <summary>
        /// The multiple choice question; cannot be null.
        /// </summary>
        public string Question {
            get {
                return question;
            }
            set {
                question = value ?? throw new ArgumentNullException("Multiple choice questions cannot be null or empty.");
            }
        }

        /// <summary>
        /// The number of multiple choices; must be greater than 0, but less than 6.
        /// </summary>
        public int NumberOfChoices {
            get {
                return numberOfChoices;
            }
            set {
                if (value <= 0)
                {
                    throw new ArgumentException("The number of multiple choice answers cannot be null or zero.");
                }
                else if (value > 6)
                {
                    throw new ArgumentException("The number of multiple choice answers cannot be greater than six.");
                }
                else
                {
                    numberOfChoices = value;
                }
            }
        }

        /// <summary>
        /// The possible answers to the multiple choice question; each question must have at least one choice (e.g., for flashcards, etc.).
        /// </summary>
        public List<string> Choices {
            get {
                return choices;
            }
            set {
                choices = value ?? throw new ArgumentNullException("Multiple choice questions must have at least one choice.");
            }
        }

        public MultipleChoiceQuestion()
        {
            // this.Choices = new List<string>();
            QuestionType = Constants.QuestionType.M;
        }
    }
}