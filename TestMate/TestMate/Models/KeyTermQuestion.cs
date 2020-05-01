/*
 * TestMate model class for key term data objects.
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
using TestMate.Common;

namespace TestMate.Models {
    /// <summary>
    /// TestMate model class for key term data objects.
    /// </summary>
    public class KeyTermQuestion : Question {
        private string keyTerm = "";
        private string definition = "";

        /// <summary>
        /// The key term; cannot be null.
        /// </summary>
        public string KeyTerm {
            get {
                return keyTerm;
            }
            set {
                keyTerm = value ?? throw new ArgumentNullException("Key terms cannot be null or empty.");
            }
        }
        
        /// <summary>
        /// The key term definition; cannot be null.
        /// </summary>
        public string Definition {
            get {
                return definition;
            }
            set {
                definition = value ?? throw new ArgumentNullException("Key term definitions cannot be null or empty.");
            }
        }

        public KeyTermQuestion() {
            QuestionType = Constants.QuestionType.K;
        }
    }
}
