# Creating Test Mate Test Files
Here is the format to use when creating your own tests.

Remember:

- Filenames cannot have spaces or any of the following characters: /\?%*:|\"<>.
- Filenames must end in .tmf (Good example: my-test.tmf)
- Save files in UTF-8 format with CR/LF in Notepad or Notepad++.

---

## File Format:

**Test Title**
**K (Type of Question (K = Key Terms))**
- [The Key Term]
- [The Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)]
- [Media Filename or "null"]
- [The Definition]

**M (Type of Question (M = Multiple Choice))**
- [The Multiple-Choice Question]
- [The Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)]
- [Media Filename or "null"]
- [Zero-Based Number of Answers]
- [Make sure the Correct Answer is first]
- [Wrong Answer 1 (Optional)]
- . . .
- [Wrong Answer n (Optional)]
- [Explanation (A blank line will add the correct answer as the explanation)]

**T (Type of Question (T = True or False Questions))**
- [The True or False Question]
- [The Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)]
- [Media Filename or "null"]
- [The Correct Answer]
- [Explanation (A blank line will add the correct answer and the question as the explanation)]

## Example Test:

    This is the Smallest Test
    K
    A German Shepherd
    N
    null
    It has four legs and barks.
    M
    _____ is the symbol for 'the sum of'.
    N
    null
    3
    Σ
    =
    Δ
    ∞
    The symbol for summation is Σ.
    T
    The picture is of a church in Puerto Rico.
    I
    Architecture.jpg
    true
    The picture shows La Iglesia de San Antonio de Padua in Guayama, Puerto Rico.

---

## Notes:

- Try to capitalize the first letter of each sentence.
- Use a period after each sentence.
- Do not add an extra return after the last item at the end of the file.
- Use UTF-8 if using Notepad++ or "Unicode (UTF-8 with signature) – Codepage 65001" if using Visual Studio to save the test.