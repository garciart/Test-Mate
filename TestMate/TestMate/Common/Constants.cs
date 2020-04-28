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

namespace TestMate.Common {
    /// <summary>
    /// Constant values used throughout the application.
    /// </summary>
    public static class Constants {
        /// <summary>
        /// The path of the settings file and test files.
        /// </summary>
        public static string AppDataPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // public static string AppDataPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// The settings file is in the local application data folder.
        /// </summary>
        public static string SettingsFile = Path.Combine(AppDataPath, "TestMate.ini");

        /// <summary>
        /// Used to convert the user's choice to an index.
        /// </summary>
        public static readonly char[] Letters = { 'A', 'B', 'C', 'D' };

        /// <summary>
        /// Question order settings:
        /// Default to display questions as read from the file,
        /// Random to randomize the order.
        /// </summary>
        public enum QuestionOrder {
            Default,
            Random
        }

        /// <summary>
        /// Term display settings:
        /// TermAsQuestion to display terms as question (Default),
        /// DefinitionAsQuestion to display definitions as question,
        /// Mixed to mix it up.
        /// </summary>
        public enum TermDisplay {
            TermAsQuestion,
            DefinitionAsQuestion,
            Mixed
        }

        /// <summary>
        /// Provide feedback settings:
        /// Yes to to provide feedback after each answer (Default),
        /// No to wait until the end of the test to provide feedback.
        /// </summary>
        public enum ProvideFeedback {
            Yes,
            No
        }

        /// <summary>
        /// Question type constants: KeyTerm, MultipleChoice, TrueFalse.
        /// </summary>
        public enum QuestionType {
            K,
            M,
            T
        }

        /// <summary>
        /// Media type constants: None, Audio, Image, Video.
        /// </summary>
        public enum MediaType {
            N,
            A,
            I,
            V
        }
    }
}
