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

In addition, you can add a callback to return true / false if this entry already exists in your own data source so it will keep retrying.

```c
    SuggestorService.GetSuggestions(".:alex:.", IsUsernameTaken)

    // or...

    SuggestorService.GetSuggestions(".:alex:.", IsUsernameTaken)
```

Example callback method.

```c
    private static bool IsUsernameTaken(string input)
    {
        return _db.Users.Any(x => x.Username == input);
    }
```