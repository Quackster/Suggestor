using Suggestor;

namespace SuggestorTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = ".:Test:.";

            List<string> suggestionList = SuggestorService.GetSuggestions(".:alex:.", new SuggestorSettings
            {
                Format = "{original}{}",
                MaximumWordLength = 12,
                SuggestionCount = 3,
                MaximumAttempts = 10,
                WordListFile = null,
                WordList = null
            });

            // [".:alexVigorous:.", ".:alexSnoopy:.", ".:alexThrifty:."]


            Console.WriteLine("Hello, World!");
        }
    }
}
