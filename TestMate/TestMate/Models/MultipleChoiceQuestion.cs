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
using TestMate.Common;

namespace TestMate.Models {
    /// <summary>
    /// TestMate model class for multiple choice data objects.
    /// </summary>
    public class MultipleChoiceQuestion : Question {
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
                if (value <= 0) {
                    throw new ArgumentException("The number of multiple choice answers cannot be null or zero.");
                }
                else if (value > 6) {
                    throw new ArgumentException("The number of multiple choice answers cannot be greater than six.");
                }
                else {
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

        public MultipleChoiceQuestion() {
            // this.Choices = new List<string>();
            QuestionType = Constants.QuestionType.M;
        }
    }
}