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
using System.IO;
using System.Linq;
using System.Text;
using TestMate.Common;

namespace TestMate.Models {
    /// <summary>
    /// TestMate model class for test objects.
    /// </summary>
    public class Test {
        /// <summary>
        /// Initiate Random at the beginning, and then only once, or it will regenerate the same set of numbers
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// The test title.
        /// </summary>
        public string TestTitle { get; set; }

        /// <summary>
        /// Creates an array list of test questions.
        /// </summary>
        /// <param name="testFileName">The name of the test data file</param>
        /// <param name="questionOrder">Question order setting: Default to display questions as read from the file, Random to randomize the order.</param>
        /// <param name="termDisplay">Term display settings: TermAsQuestion to display terms as question (Default); DefinitionAsQuestion to display definitions as question; Mixed to mix it up.</param>
        /// <returns>A formatted list of test questions created from test data objects.</returns>
        public List<TestQuestion> GetTest(string testFileName, Constants.QuestionOrder questionOrder, Constants.TermDisplay termDisplay) {
            List<TestQuestion> testQuestions = new List<TestQuestion>();
            List<Question> testData = ReadFile(testFileName);
            List<int> ktIndex = new List<int>();
            for (int x = 0; x < testData.Count; x++) {
                if (testData[x].QuestionType == Constants.QuestionType.K) {
                    ktIndex.Add(x);
                }
            }
            int ktCount = 0;
            for (int x = 0; x < testData.Count; x++) {
                Constants.QuestionType qt = testData[x].QuestionType;
                RandomNumbers rn;
                switch (qt) {
                    case Constants.QuestionType.K:
                        KeyTermQuestion kt = (KeyTermQuestion)testData[x];
                        int ktNumberOfChoices = ((ktIndex.Count - 1) < 3 ? (ktIndex.Count - 1) : 3);
                        List<string> ktTempChoices = new List<string>();
                        rn = new RandomNumbers((ktIndex.Count - 1), ktCount, ktNumberOfChoices);
                        bool displayTermAsQuestion = true;
                        switch (termDisplay) {
                            case Constants.TermDisplay.DefinitionAsQuestion:
                                displayTermAsQuestion = false;
                                break;
                            case Constants.TermDisplay.Mixed:
                                displayTermAsQuestion = (Random.Next(2) == 0 ? false : true);
                                break;
                            case Constants.TermDisplay.TermAsQuestion:
                            default:
                                break;
                        }
                        if (displayTermAsQuestion) {
                            for (int y = 0; y <= ktNumberOfChoices; y++) {
                                ktTempChoices.Add(((KeyTermQuestion)testData[ktIndex[rn.UniqueArray[y]]]).Definition);
                            }
                            testQuestions.Add(new TestQuestion(qt, kt.KeyTerm, kt.MediaType, kt.MediaFileName, ktNumberOfChoices, ktTempChoices, rn.IndexLocation, kt.Explanation));
                        }
                        else {
                            for (int y = 0; y <= ktNumberOfChoices; y++) {
                                ktTempChoices.Add(((KeyTermQuestion)testData[ktIndex[rn.UniqueArray[y]]]).KeyTerm);
                            }
                            testQuestions.Add(new TestQuestion(qt, kt.Definition, kt.MediaType, kt.MediaFileName, ktNumberOfChoices, ktTempChoices, rn.IndexLocation, kt.Explanation));
                        }
                        ktCount++;
                        break;
                    case Constants.QuestionType.M:
                        MultipleChoiceQuestion mc = (MultipleChoiceQuestion)testData[x];
                        List<string> mcTempChoices = new List<string>();
                        rn = new RandomNumbers(mc.NumberOfChoices, 0, mc.NumberOfChoices);
                        for (int i = 0; i <= mc.NumberOfChoices; i++) {
                            mcTempChoices.Add(mc.Choices[rn.UniqueArray[i]]);
                        }
                        testQuestions.Add(new TestQuestion(qt, mc.Question, mc.MediaType, mc.MediaFileName, mc.NumberOfChoices, mcTempChoices, rn.IndexLocation, mc.Explanation));
                        break;
                    case Constants.QuestionType.T:
                        TrueFalseQuestion tf = (TrueFalseQuestion)testData[x];
                        List<string> tfTempChoices = new List<string> {
                            "true",
                            "false"
                        };
                        testQuestions.Add(new TestQuestion(qt, tf.Question, tf.MediaType, tf.MediaFileName, 1, tfTempChoices, (tf.Answer ? 0 : 1), tf.Explanation));
                        break;
                    default:
                        throw new ArgumentException("Corrupt data. Check structure and values.");
                }
            }
            if (questionOrder == Constants.QuestionOrder.Random) {
                RandomNumbers qoArray = new RandomNumbers(testQuestions.Count);
                for (int x = 0; x < testQuestions.Count; x++) {
                    TestQuestion temp = testQuestions[x];
                    testQuestions[x] = testQuestions[qoArray.UniqueArray[x]];
                    testQuestions[qoArray.UniqueArray[x]] = temp;
                }
            }
            return testQuestions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testFileName"></param>
        /// <returns></returns>
        private List<Question> ReadFile(string testFileName) {
            List<Question> testQuestion = new List<Question>();
            try {
                using (StreamReader sr = new StreamReader(testFileName, Encoding.UTF8)) {
                    TestTitle = sr.ReadLine();
                    string questionTypeFromFile;
                    while ((questionTypeFromFile = sr.ReadLine()) != null) {
                        questionTypeFromFile = questionTypeFromFile.ToUpperInvariant();
                        if (questionTypeFromFile.Equals(Constants.QuestionType.K.ToString())) {
                            KeyTermQuestion k = new KeyTermQuestion();
                            k.KeyTerm = sr.ReadLine();
                            k.ValidateAndSetMedia((Constants.MediaType)Enum.Parse(typeof(Constants.MediaType), sr.ReadLine()), sr.ReadLine());
                            k.Definition = sr.ReadLine();
                            k.Explanation = k.KeyTerm + ": " + k.Definition;
                            testQuestion.Add(k);
                        }
                        else if (questionTypeFromFile.Equals(Constants.QuestionType.M.ToString())) {
                            MultipleChoiceQuestion m = new MultipleChoiceQuestion();
                            m.Question = sr.ReadLine();
                            m.ValidateAndSetMedia((Constants.MediaType)Enum.Parse(typeof(Constants.MediaType), sr.ReadLine()), sr.ReadLine());
                            m.NumberOfChoices = Int32.Parse(sr.ReadLine());
                            for (int x = 0; x <= m.NumberOfChoices; x++) {
                                m.Choices.Add(sr.ReadLine());
                            }
                            string tempExplanation = sr.ReadLine();
                            if (tempExplanation.ToLowerInvariant().Equals("null") || String.IsNullOrEmpty(tempExplanation)) {
                                m.Explanation = "The answer is: " + m.Choices[0];
                            }
                            else {
                                m.Explanation = tempExplanation;
                            }
                            testQuestion.Add(m);
                        }
                        else if (questionTypeFromFile.Equals(Constants.QuestionType.T.ToString())) {
                            TrueFalseQuestion t = new TrueFalseQuestion();
                            t.Question = sr.ReadLine();
                            t.ValidateAndSetMedia((Constants.MediaType)Enum.Parse(typeof(Constants.MediaType), sr.ReadLine()), sr.ReadLine());
                            t.Answer = Boolean.Parse(sr.ReadLine());
                            string tempExplanation = sr.ReadLine();
                            if (tempExplanation.ToLowerInvariant().Equals("null") || String.IsNullOrEmpty(tempExplanation)) {
                                t.Explanation = "The answer is: " + t.Answer;
                            }
                            else {
                                t.Explanation = tempExplanation;
                            }
                            testQuestion.Add(t);
                        }
                        else {
                            throw new ArgumentException("Corrupt data file. Check structure and values.");
                        }
                    }
                }
            }
            catch (FileNotFoundException ex) {
                throw new FileNotFoundException("Cannot find test file: " + ex.ToString());
            }
            catch (IOException ex) {
                throw new IOException("Cannot open test file: " + ex.ToString());
            }
            return testQuestion;
        }
    }
}
