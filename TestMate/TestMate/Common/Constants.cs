using System;
using System.IO;

namespace TestMate.Common {
    public static class Constants {
        // The location of the settings file and test files
        public static string AppDataPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // The settings file is in the local application data folder
        public static string settingsFile = Path.Combine(AppDataPath, "TestMate.ini");

        /**
         * Used to convert the user's choice to an index
         */
        public static readonly char[] LETTERS = { 'A', 'B', 'C', 'D' };

        /**
         * Question order settings
         */
        public enum QuestionOrder {
            /**
             * DEFAULT to display questions as read from the file
             */
            DEFAULT,
            /**
             * RANDOM to randomize the order
             */
            RANDOM
        }

        /**
         * Term display settings
         */
        public enum TermDisplay {
            /**
             * TERMISQUESTION to display terms as question (Default)
             */
            TERMISQUESTION,
            /**
             * DEFISQUESTION to display definitions as question
             */
            DEFISQUESTION,
            /**
             * MIXEDQUESTION to mix it up
             */
            MIXEDQUESTION
        }

        /**
         * Provide feedback settings
         */
        public enum ProvideFeedback {
            /**
             * YES to to provide feedback after each answer (Default)
             */
            YES,
            /**
             * NO to wait until the end of the test to provide feedback
             */
            NO
        }

        /**
         * Question type constants
         */
        public enum QuestionType {
            /**
             * K for Key Term
             */
            K,
            /**
             * M for Multiple Choice
             */
            M,
            /**
             * T for True or False
             */
            T
        }

        /**
         * Media flag constants
         */
        public enum MediaType {
            /**
             * N for none
             */
            N,
            /**
             * I for images
             */
            I,
            /**
             * A for audio files
             */
            A,
            /**
             * V for video files
             */
            V
        }


        /* 
         * Default configuration settings
         * 0 - Use the default question order.
         * 0 - Use the key term for the question and a set of possible definitions for the choices.
         * 0 - Provide feedback immediately.
         */
        public static string[] settings = { "0", "0", "0" };

        // All UI items (e.g., buttons, etc.) are enabled by default
        public static bool enableAppFlag = true;
    }
}
