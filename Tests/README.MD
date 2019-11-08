# Creating Test Mate Test Files
<p>FILENAMES CANNOT HAVE SPACES OR ANY OF THE FOLLOWING CHARACTERS: /\?%*:|\"<>.</p>
<p>FILENAMES MUST END IN TMF (Good example: my-test.tmf)</p>
<p>Save files in UTF-8 format with CR/LF in Notepad or Notepad++.</p>
<hr>
<h2>File Format:</h2>
<p>Test Title</p>
<p><b>K (Type of Question (K = Key Terms))</b></p>
<p>- [The Key Term]</p>
<p>- [The Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)]</p>
<p>- [Media Filename or "null"]</p>
<p>- [The Definition]</p>
<p><b>M (Type of Question (M = Multiple Choice))</b></p>
<p>- [The Multiple-Choice Question]</p>
<p>- [The Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)]</p>
<p>- [Media Filename or "null"]</p>
<p>- [Zero-Based Number of Answers]</p>
<p>- [Make sure the Correct Answer is first]</p>
<p>- [Wrong Answer 1 (Optional)]</p>
<p>- ...</p>
<p>- [Wrong Answer n (Optional)]</p>
<p>- [Explanation (A blank line will add the correct answer as the explanation)]</p>
<p><b>T (Type of Question (T = True or False Questions))</b></p>
<p>- [The True or False Question]</p>
<p>- [The Media Flag (0 = None, 1 = Image, 2 = Audio, 3 = Video)]</p>
<p>- [Media Filename or "null"]</p>
<p>- [The Correct Answer]</p>
<p>- [Explanation (A blank line will add the correct answer and the question as the explanation)]</p>
<h2>Example:</h2>
<p>This is the Smallest Test</p>
<p>K</p>
<p>A German Shepherd</p>
<p>N</p>
<p>null</p>
<p>It has four legs and barks.</p>
<p>M</p>
<p>_____ is the symbol for 'the sum of'.</p>
<p>N</p>
<p>null</p>
<p>3</p>
<p>Σ</p>
<p>=</p>
<p>Δ</p>
<p>∞</p>
<p>The symbol for summation is Σ.</p>
<p>T</p>
<p>The picture is of a church in Puerto Rico.</p>
<p>I</p>
<p>Architecture.jpg</p>
<p>true</p>
<p>The picture shows La Iglesia de San Antonio de Padua in Guayama, Puerto Rico.</p>
<hr>
<h2>Notes:</h2>
<p>- Try to capitalize the first letter of each sentence.</p>
<p>- Use a period after each sentence.</p>
<p>- Do not add an extra return after the last item at the end of the file.</p>
<p>- Use UTF-8 if using Notepad++ or "Unicode (UTF-8 with signature) – Codepage 65001" if using Visual Studio to save the test.</p>