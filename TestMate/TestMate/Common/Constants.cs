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
            KeyTerm,
            MultipleChoice,
            TrueFalse
        }

        /// <summary>
        /// Media type constants: None, Audio, Image, Video.
        /// </summary>
        public enum MediaType {
            None,
            Audio,
            Image,
            Video
        }
    }
}
