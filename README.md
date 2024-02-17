# Suggestor
C# based username suggestor based off current input.

## Example usage

Default settings.

```c
    string input = ".:Test:.";

    List<string> suggestionList = SuggestorService.GetSuggestions(".:alex:.");

    // [".:alexVigorous:.", ".:alexSnoopy:.", ".:alexThrifty:."]
```

Customisable, you can load the wordlist yourself or you can give it a word list file path and it will load the loads (list of words separated by new line characters).

```c
    List<string> suggestionList = SuggestorService.GetSuggestions(".:alex:.", new SuggestorSettings
    {
        Format = "{}{original}",
        MaximumWordLength = 12,
        SuggestionCount = 3,
        MaximumAttempts = 10,
        WordList = null,
        WordListFile = null
    });

    // [".:alexRich:.", ".:alexSnoopy:.", ".:alexThrifty:."]
```