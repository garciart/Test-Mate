# Creating Test Mate Test Files
FILENAMES CANNOT HAVE SPACES OR ANY OF THE FOLLOWING CHARACTERS: /\?%*:|\"<>.
FILENAMES MUST END IN TMF
Good example: my-test.tmf

<h2>File Format:</h2>
Test Title
Type of Question (K = Key Terms, M = Multiple Choice, T = True or False Questions)
- Key Term
- Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)
- Media Filename (Optional)
- Definition
Type of Question (K = Key Terms, M = Multiple Choice, T = True or False Questions)
- Multiple-Choice Question
- Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)
- Media Filename (Optional)
- Zero-Based Number of Answers
- Correct Answer
- Wrong Answer 1 (Optional)
- Wrong Answer 2 (Optional)
- Wrong Answer 3 (Optional)
- Explanation (Blank line if using default)
Type of Question (K = Key Terms, M = Multiple Choice, T = True or False Questions)
- True/False Question
- Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)
- Media Filename (Optional)
- Correct Answer
- Explanation (Blank line if using default)

Notes
Enter -1 even if there are no items
- Try to capitalize the first letter of each sentence.
- Use a period after each sentence.
- Add an extra return after the last item at the end of the file.
