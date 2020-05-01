/*
 * TestMate model class for test file data objects.
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

namespace TestMate.Models
{
    /// <summary>
    /// TestMate model class for test file data objects.
    /// </summary>
    public class TestFile
    {
        /// <summary>
        /// Path, filename, and extension of test file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Descriptive name of the test.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// The date the test was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
