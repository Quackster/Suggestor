using System;
using System.Collections.Generic;

namespace Suggestor
{
    public struct SuggestorSettings
    {
        public int MaximumAttempts { get; set; }

        public int? MaximumWordLength { get; set; }
        public string WordListFile { get; set; }
        public List<string> WordList { get; set; }
        public string Format { get; set; }
        public int SuggestionCount { get; set; }
    }
}
