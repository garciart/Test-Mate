/*
 * Code, functions, and methods common to all classes.
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

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestMate.Models;
using TestMate.Resources;

namespace TestMate.Common
{
    /// <summary>
    /// Non-data access functions used throughout the application.
    /// </summary>
    public static class AppFunctions
    {
        /// <summary>
        /// Initiate Random at the beginning, and then only once, or it will regenerate the same set of numbers
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// The test title.
        /// </summary>
        public static string TestTitle { get; set; }

        /// <summary>
        /// Default configuration settings
        /// 0 (Use the default question order),
        /// 0 (Use the key term for the question and a set of possible definitions for the choices),
        /// 0 (Provide feedback immediately).
        /// </summary>
        private static string[] settings = { App.questionOrder.ToString(), App.termDisplay.ToString(), App.provideFeedback.ToString() };

        /// <summary>
        /// Attempt to read and set stored settings. If the settings file is not found, it creates one with default values.
        /// </summary>
        /// <returns>Appropriate error message.</returns>
        public static string ReadSettingsFromFile()
        {
            try
            {
                if (File.Exists(Constants.SettingsFile))
                {
                    // File.Delete(Constants.SettingsFile);
                    settings = File.ReadAllLines(Constants.SettingsFile, Encoding.UTF8);
                    Enum.TryParse(settings[0], out App.questionOrder);
                    Enum.TryParse(settings[1], out App.termDisplay);
                    Enum.TryParse(settings[2], out App.provideFeedback);
                    return null;
                }
                else
                {
                    File.WriteAllLines(Constants.SettingsFile, settings, Encoding.UTF8);
                    return AppResources.SettingsMissingErrorMessage;
                }
            }
            catch (Exception e)
            {
                return string.Format(AppResources.SettingsReadErrorMessage, e.Message);
            }
        }

        /// <summary>
        /// Attempt to save settings to file.
        /// </summary>
        /// <returns>Appropriate error message.</returns>
        public static string SaveSettingsToFile(Constants.QuestionOrder questionOrder, Constants.TermDisplay termDisplay, Constants.ProvideFeedback provideFeedback)
        {
            try
            {
                settings[0] = questionOrder.ToString();
                settings[1] = termDisplay.ToString();
                settings[2] = provideFeedback.ToString();
                File.WriteAllLines(Constants.SettingsFile, settings, Encoding.UTF8);
                return string.Format(AppResources.SettingsSaveSuccessMessage);
            }
            catch (Exception e)
            {
                return string.Format(AppResources.SettingsSaveErrorMessage, e.Message);
            }
        }

        /// <summary>
        /// Thanks to Daxton47 at https://forums.xamarin.com/discussion/138266/how-to-hit-an-url-to-download-a-file
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static async Task<byte[]> DownloadFileAsync(string fileUrl)
        {
            var _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            Console.WriteLine(">>> HERE!");
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(fileUrl))
                {
                    Console.WriteLine(">>> RESPONSE: " + httpResponse);
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        Console.WriteLine(">>> CHECK THE URL: " + fileUrl);
                        // Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> HAD AN EXCEPTION: " + ex.ToString());
                //Handle Exception
                return null;
            }
        }

        /// <summary>
        /// Thanks to Alex at https://stackoverflow.com/questions/46302570/how-to-get-list-of-files-from-a-specific-github-repolink-in-c-sharp
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetTestList()
        {
            var repoOwner = "garciart";
            var repoName = "TestMate";
            var path = "Tests";
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com"),
                DefaultRequestHeaders = {
                    {"User-Agent", "Github-API-Test"}
                }
            };
            HttpResponseMessage json = await client.GetAsync($"repos/{repoOwner}/{repoName}/contents/{path}");
            string bodyString = await json.Content.ReadAsStringAsync();
            JToken bodyJson = JToken.Parse(bodyString);
            return bodyJson.SelectTokens("$.[*].name")
                .Where(token => token.Value<string>().EndsWith("tmf"))
                .Select(token => token.Value<string>());
        }

        /// <summary>
        /// Creates an array list of test questions.
        /// </summary>
        /// <param name="testFileName">The name of the test data file</param>
        /// <param name="questionOrder">Question order setting: Default to display questions as read from the file, Random to randomize the order.</param>
        /// <param name="termDisplay">Term display settings: TermAsQuestion to display terms as question (Default); DefinitionAsQuestion to display definitions as question; Mixed to mix it up.</param>
        /// <returns>A formatted list of test questions created from test data objects.</returns>
        public static List<TestQuestion> GetTest(string testFileName, Constants.QuestionOrder questionOrder, Constants.TermDisplay termDisplay)
        {
            List<TestQuestion> testQuestions = new List<TestQuestion>();
            List<Question> testData = ReadFile(testFileName);
            List<int> ktIndex = new List<int>();
            for (int x = 0; x < testData.Count; x++)
            {
                if (testData[x].QuestionType == Constants.QuestionType.K)
                {
                    ktIndex.Add(x);
                }
            }
            int ktCount = 0;
            for (int x = 0; x < testData.Count; x++)
            {
                Constants.QuestionType qt = testData[x].QuestionType;
                RandomNumbers rn;
                switch (qt)
                {
                    case Constants.QuestionType.K:
                        KeyTermQuestion kt = (KeyTermQuestion)testData[x];
                        int ktNumberOfChoices = ((ktIndex.Count - 1) < 3 ? (ktIndex.Count - 1) : 3);
                        List<string> ktTempChoices = new List<string>();
                        rn = new RandomNumbers((ktIndex.Count - 1), ktCount, ktNumberOfChoices);
                        bool displayTermAsQuestion = true;
                        switch (termDisplay)
                        {
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
                        if (displayTermAsQuestion)
                        {
                            for (int y = 0; y <= ktNumberOfChoices; y++)
                            {
                                ktTempChoices.Add(((KeyTermQuestion)testData[ktIndex[rn.UniqueArray[y]]]).Definition);
                            }
                            testQuestions.Add(new TestQuestion(qt, kt.KeyTerm, kt.MediaType, kt.MediaFileName, ktNumberOfChoices, ktTempChoices, rn.IndexLocation, kt.Explanation));
                        }
                        else
                        {
                            for (int y = 0; y <= ktNumberOfChoices; y++)
                            {
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
                        for (int i = 0; i <= mc.NumberOfChoices; i++)
                        {
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
            if (questionOrder == Constants.QuestionOrder.Random)
            {
                RandomNumbers qoArray = new RandomNumbers(testQuestions.Count);
                for (int x = 0; x < testQuestions.Count; x++)
                {
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
        private static List<Question> ReadFile(string testFileName)
        {
            List<Question> testQuestion = new List<Question>();
            try
            {
                using (StreamReader sr = new StreamReader(testFileName, Encoding.UTF8))
                {
                    TestTitle = sr.ReadLine();
                    string questionTypeFromFile;
                    while ((questionTypeFromFile = sr.ReadLine()) != null)
                    {
                        questionTypeFromFile = questionTypeFromFile.ToUpperInvariant();
                        if (questionTypeFromFile.Equals(Constants.QuestionType.K.ToString()))
                        {
                            KeyTermQuestion k = new KeyTermQuestion();
                            k.KeyTerm = sr.ReadLine();
                            k.ValidateAndSetMedia((Constants.MediaType)Enum.Parse(typeof(Constants.MediaType), sr.ReadLine()), sr.ReadLine());
                            k.Definition = sr.ReadLine();
                            k.Explanation = k.KeyTerm + ": " + k.Definition;
                            testQuestion.Add(k);
                        }
                        else if (questionTypeFromFile.Equals(Constants.QuestionType.M.ToString()))
                        {
                            MultipleChoiceQuestion m = new MultipleChoiceQuestion();
                            m.Question = sr.ReadLine();
                            m.ValidateAndSetMedia((Constants.MediaType)Enum.Parse(typeof(Constants.MediaType), sr.ReadLine()), sr.ReadLine());
                            m.NumberOfChoices = Int32.Parse(sr.ReadLine());
                            for (int x = 0; x <= m.NumberOfChoices; x++)
                            {
                                m.Choices.Add(sr.ReadLine());
                            }
                            string tempExplanation = sr.ReadLine();
                            if (tempExplanation.ToLowerInvariant().Equals("null") || String.IsNullOrEmpty(tempExplanation))
                            {
                                m.Explanation = "The answer is: " + m.Choices[0];
                            }
                            else
                            {
                                m.Explanation = tempExplanation;
                            }
                            testQuestion.Add(m);
                        }
                        else if (questionTypeFromFile.Equals(Constants.QuestionType.T.ToString()))
                        {
                            TrueFalseQuestion t = new TrueFalseQuestion();
                            t.Question = sr.ReadLine();
                            t.ValidateAndSetMedia((Constants.MediaType)Enum.Parse(typeof(Constants.MediaType), sr.ReadLine()), sr.ReadLine());
                            t.Answer = Boolean.Parse(sr.ReadLine());
                            string tempExplanation = sr.ReadLine();
                            if (tempExplanation.ToLowerInvariant().Equals("null") || String.IsNullOrEmpty(tempExplanation))
                            {
                                t.Explanation = "The answer is: " + t.Answer;
                            }
                            else
                            {
                                t.Explanation = tempExplanation;
                            }
                            testQuestion.Add(t);
                        }
                        else
                        {
                            throw new ArgumentException("Corrupt data file. Check structure and values.");
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("Cannot find test file: " + ex.ToString());
            }
            catch (IOException ex)
            {
                throw new IOException("Cannot open test file: " + ex.ToString());
            }
            return testQuestion;
        }

        public static async Task<List<string>> devGetFiles()
        {
            List<string> fileList = new List<string>();
            try
            {
                HttpClient client = new HttpClient();
                // Get page contents. Should be a simple directory listing
                string responseBody = await client.GetStringAsync("http://testmate.rgprogramming.com");
                // Collect only files that end in .tmf and return null if none found
                Regex regex = new Regex("<A HREF=\".*?tmf\">(?<name>.*?tmf)</A>");
                MatchCollection matches = regex.Matches(responseBody);
                if (matches.Count == 0)
                {
                    return null;
                }
                else
                {
                    foreach (Match match in matches)
                    {
                        if (!match.Success) { continue; }
                        fileList.Add(match.Groups["name"].ToString());
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Cannot get directory listing: " + ex.ToString());
            }
            return fileList;
        }

        public static async Task<string> devGetPage()
        {
            string responseBody = null;
            try
            {
                HttpClient client = new HttpClient();
                responseBody = await client.GetStringAsync("http://testmate.rgprogramming.com");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }

        public static async void devDownloadFileAsync(string testFile)
        {
            try
            {
                Console.WriteLine(">>> TEST FILE: " + testFile);
                // string testFileURL = "https://raw.githubusercontent.com/garciart/TestMate/master/Tests/" + testFile;
                string testFileURL = "http://testmate.rgprogramming.com/" + testFile;
                Console.WriteLine(">>> TEST FILE URL: " + testFileURL);
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
                var response = await client.GetAsync(testFileURL);
                Console.WriteLine(">>> RESPONSE: " + response.ToString());
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var fileInfo = new FileInfo(String.Format("{0}/{1}", Constants.AppDataPath, testFile));
                    using (var fileStream = fileInfo.OpenWrite())
                    {
                        Console.WriteLine(">>> " + fileStream);
                        await stream.CopyToAsync(fileStream);
                    }
                }
             }
            catch (Exception ex)
            {
                Console.WriteLine(">>> HAD AN EXCEPTION: " + ex.ToString());
            }
        }
    }
}
