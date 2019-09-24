using System;
using System.Collections.Generic;
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
        private string testTitle { get; set; }

        /// <summary>
        /// Creates an array list of test questions.
        /// </summary>
        /// <param name="testFileName">The name of the test data file</param>
        /// <param name="questionOrder">Question order setting: Default to display questions as read from the file, Random to randomize the order.</param>
        /// <param name="termDisplay">Term display settings: TermAsQuestion to display terms as question (Default); DefinitionAsQuestion to display definitions as question; Mixed to mix it up.</param>
        /// <returns>A formatted list of test questions created from test data objects.</returns>
        public List<TestQuestion> getTest(string testFileName, Constants.QuestionOrder questionOrder, Constants.TermDisplay termDisplay) {
            List<TestQuestion> testQuestions = new List<TestQuestion>();
            List<Question> testData = readFile(testFileName);
            List<int> ktIndex = new List<int>();
            for (int x = 0; x < testData.Count; x++) {
                if (testData[x].QuestionType == Constants.QuestionType.KeyTerm) {
                    ktIndex.Add(x);
                }
            }
            int ktCount = 0;
            for (int x = 0; x < testData.Count; x++) {
                Constants.QuestionType qt = testData[x].QuestionType;
                RandomNumbers rn;
                switch (qt) {
                    case Constants.QuestionType.KeyTerm:
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
                    case Constants.QuestionType.MultipleChoice:
                        MultipleChoiceQuestion mc = (MultipleChoiceQuestion)testData[x];
                        List<string> mcTempChoices = new List<string>();
                        rn = new RandomNumbers(mc.NumberOfChoices, 0, mc.NumberOfChoices);
                        for (int i = 0; i <= mc.NumberOfChoices; i++) {
                            mcTempChoices.Add(mc.Choices[rn.UniqueArray[i]]);
                        }
                        testQuestions.Add(new TestQuestion(qt, mc.Question, mc.MediaType, mc.MediaFileName, mc.NumberOfChoices, mcTempChoices, rn.IndexLocation, mc.Explanation));
                        break;
                    case Constants.QuestionType.TrueFalse:
                        TrueFalseQuestion tf = (TrueFalseQuestion)testData[x];
                        List<string> tfTempChoices = new List<string>();
                        tfTempChoices.Add("true");
                        tfTempChoices.Add("false");
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
        private ArrayList<TestData> readFile(String testFileName) {
        // Due to MultipleChoice's fluctuating size, we will use getters and setters instead of a constructor for all question types
        // Due to MultipleChoice's fluctuating size, we will use getters and setters instead of a constructor for all question types
        ArrayList<TestData> testData = new ArrayList<>();
InputStream inputStream = new FileInputStream(testFileName);
Reader isr = new InputStreamReader(inputStream, StandardCharsets.UTF_8);
        try (BufferedReader bufferedReader = new BufferedReader(isr)) {
            setTestTitle(bufferedReader.readLine());
String qTypeFromFile;
            while (!Utilities.isNullOrEmpty(qTypeFromFile = bufferedReader.readLine())) {
                qTypeFromFile = qTypeFromFile.toUpperCase(Locale.ENGLISH);
                if (qTypeFromFile.equals(Constants.QuestionType.K.toString())) {
                    KeyTerm k = new KeyTerm();
k.setKeyTerm(Utilities.fixEscapeCharacters(bufferedReader.readLine()));
                    k.validateAndSetMedia(Constants.MediaType.valueOf(bufferedReader.readLine().toUpperCase(Locale.ENGLISH)), bufferedReader.readLine());
                    k.setKTDefinition(Utilities.fixEscapeCharacters(bufferedReader.readLine()));
                    k.setExplanation(k.getKeyTerm() + ": " + k.getKTDefinition());
                    testData.add(k);
                } else if (qTypeFromFile.equals(Constants.QuestionType.M.toString())) {
                    MultipleChoice m = new MultipleChoice();
m.setMCQuestion(Utilities.fixEscapeCharacters(bufferedReader.readLine()));
                    m.validateAndSetMedia(Constants.MediaType.valueOf(bufferedReader.readLine().toUpperCase(Locale.ENGLISH)), bufferedReader.readLine());
                    m.setMCNumberOfChoices(Integer.parseInt(bufferedReader.readLine()));
                    for (int x = 0; x <= m.getMCNumberOfChoices(); x++) {
                        m.getMCChoices().add(Utilities.fixEscapeCharacters(bufferedReader.readLine()));
                    }
                    String tempExplanation = Utilities.fixEscapeCharacters(bufferedReader.readLine());
                    if (tempExplanation.toLowerCase(Locale.ENGLISH).equals("null") || Utilities.isNullOrEmpty(tempExplanation)) {
                        m.setExplanation("The answer is: " + m.getMCChoices().get(0));
                    } else {
                        m.setExplanation(tempExplanation);
                    }
                    testData.add(m);
                } else if (qTypeFromFile.equals(Constants.QuestionType.T.toString())) {
                    TrueFalse t = new TrueFalse();
t.setTFQuestion(Utilities.fixEscapeCharacters(bufferedReader.readLine()));
                    t.validateAndSetMedia(Constants.MediaType.valueOf(bufferedReader.readLine().toUpperCase(Locale.ENGLISH)), bufferedReader.readLine());
                    t.setTFAnswer(Boolean.valueOf(bufferedReader.readLine()));
                    String tempExplanation = Utilities.fixEscapeCharacters(bufferedReader.readLine());
                    if (tempExplanation.toLowerCase(Locale.ENGLISH).equals("null") || Utilities.isNullOrEmpty(tempExplanation)) {
                        t.setExplanation("The answer is: " + t.getTFAnswer());
                    } else {
                        t.setExplanation(tempExplanation);
                    }
                    testData.add(t);
                } else {
                    throw new IllegalArgumentException("Corrupt data file. Check structure and values.");
                }
            }
        } catch (FileNotFoundException ex) {
            throw new FileNotFoundException("Cannot find test file: " + ex.toString());
        } catch (IOException ex) {
            throw new IOException("Cannot open test file: " + ex.toString());
        }
        
        return testData;
    }
    }
}
