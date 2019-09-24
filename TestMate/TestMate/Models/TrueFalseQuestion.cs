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
using TestMate.Common;

namespace TestMate.Models {
    /**
     * TestMate model class for true/false data objects
     *
     * @author Rob Garcia at rgarcia@rgprogramming.com
     */
    public class TrueFalseQuestion : Question {
        /// <summary>
        /// The true or false question; cannot be null.
        /// </summary>
        public string Question {
            get {
                return Question;
            }
            set {
                Question = value ?? throw new ArgumentNullException("True or false questions cannot be null or empty.");
            }
        }

        /// <summary>
        /// The true or false answer; default value is true;
        /// </summary>
        public bool Answer { get; set; } = true;

        public TrueFalseQuestion() {
            QuestionType = Constants.QuestionType.T;
        }
    }
}
