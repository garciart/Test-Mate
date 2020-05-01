/*
 * TestMate model class for test question objects.
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
    /// TestMate model class for test question objects.
    /// </summary>
    public class TestQuestion : Question
    {
        private string question = "";
        private int numberOfChoices = 0;
        private List<string> choices = new List<string>();

        /// <summary>
        /// The final test question. Determined by code and settings for key terms/definitions.
        /// </summary>
        public string Question {
            get {
                return question;
            }
            set {
                question = value ?? throw new ArgumentNullException("Questions cannot be null or empty.");
            }
        }

        /// <summary>
        /// The final number of possible answers for the question. Must be less than six.
        /// </summary>
        public int NumberOfChoices {
            get {
                return numberOfChoices;
            }
            set {
                if (value < 0)
                {
                    throw new ArgumentException("The number of answers cannot be null or less than zero.");
                }
                else if (value > 6)
                {
                    throw new ArgumentException("The number of answers cannot be greater than six.");
                }
                else
                {
                    numberOfChoices = value;
                }
            }
        }

        /// <summary>
        /// An array of possible answers to the test question.
        /// </summary>
        public List<string> Choices {
            get {
                return choices;
            }
            set {
                choices = value ?? throw new ArgumentNullException("Questions must have at least one choice.");
            }
        }

        /// <summary>
        /// The location of the correct answer in the choices array.
        /// </summary>
        public int CorrectAnswerIndex { get; set; }

        /// <summary>
        /// Constructor for adding multiple choice and true/false questions.
        /// </summary>
        /// <param name="questionType">The question type: KeyTerm, MultipleChoice, TrueFalse.</param>
        /// <param name="question">The final test question. Determined by code and settings for key terms/definitions.</param>
        /// <param name="mediaType">The media type: None, Audio, Image, Video.</param>
        /// <param name="mediaFileName">The media file name or null if no media is associated with the question.</param>
        /// <param name="numberOfChoices">The final number of possible answers for the question. Must be less than six.</param>
        /// <param name="choices">An array of possible answers to the test question.</param>
        /// <param name="correctAnswerIndex">The location of the correct answer in the choices array.</param>
        /// <param name="explanation">The explanation for the correct answer of the test question.</param>
        public TestQuestion(Constants.QuestionType questionType, string question, Constants.MediaType mediaType, string mediaFileName, int numberOfChoices, List<string> choices, int correctAnswerIndex, string explanation)
        {
            QuestionType = questionType;
            Question = question;
            MediaType = mediaType;
            MediaFileName = mediaFileName;
            NumberOfChoices = numberOfChoices;
            Choices = choices;
            CorrectAnswerIndex = correctAnswerIndex;
            Explanation = explanation;
        }
    }
}
