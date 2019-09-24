using System;
using System.Collections.Generic;
using TestMate.Common;

namespace TestMate.Models {
    /// <summary>
    /// TestMate model class for test question objects.
    /// </summary>
    public class TestQuestion {
        /// <summary>
        /// The question type: KeyTerm, MultipleChoice, TrueFalse.
        /// </summary>
        public Constants.QuestionType QuestionType { get; set; }

        /// <summary>
        /// The final test question. Determined by code and settings for key terms/definitions.
        /// </summary>
        public string Question {
            get {
                return Question;
            }
            set {
                Question = value ?? throw new ArgumentNullException("Multiple choice questions cannot be null or empty.");
            }
        }

        /// <summary>
        /// The media type: None, Audio, Image, Video.
        /// </summary>
        public Constants.MediaType MediaType { get; set; }

        /// <summary>
        /// The media file name or null if no media is associated with the question.
        /// </summary>
        public string MediaFileName { get; set; } = null;

        /// <summary>
        /// The final number of possible answers for the question. Must be less than six.
        /// </summary>
        public int NumberOfChoices {
            get {
                return NumberOfChoices;
            }
            set {
                if (value <= 0) {
                    throw new ArgumentException("The number of multiple choice answers cannot be null or zero.");
                }
                else if (value > 6) {
                    throw new ArgumentException("The number of multiple choice answers cannot be greater than six.");
                }
                else {
                    NumberOfChoices = value;
                }
            }
        }

        /// <summary>
        /// An array of possible answers to the test question.
        /// </summary>
        public List<string> Choices {
            get {
                return Choices;
            }
            set {
                if (value == null) {
                    throw new ArgumentNullException("Questions must have at least one choice.");
                }
                else {
                    Choices = value;
                }
            }
        }

        /// <summary>
        /// The location of the correct answer in the choices array.
        /// </summary>
        public int CorrectAnswerIndex {
            get {
                return CorrectAnswerIndex;
            }
            set {
                if (value <= 0) {
                    throw new ArgumentException("The number of choices cannot be null or zero.");
                }
                else if (value > 6) {
                    throw new ArgumentException("The number of choices cannot be greater than six.");
                }
                else {
                    CorrectAnswerIndex = value;
                }
            }
        }

        /// <summary>
        /// The explanation for the correct answer of the test question.
        /// </summary>
        public string Explanation {
            get {
                return Explanation;
            }
            set {
                Explanation = value ?? throw new ArgumentNullException("Correct answers must have an explantion.");
            }
        }

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
        public TestQuestion(Constants.QuestionType questionType, string question, Constants.MediaType mediaType, string mediaFileName, int numberOfChoices, List<string> choices, int correctAnswerIndex, string explanation) {
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
