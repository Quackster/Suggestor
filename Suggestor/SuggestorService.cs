using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Suggestor.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace Suggestor
{
    public class SuggestorService
    {
        private static Random _random = new Random();

        public static SuggestorSettings DefaultSettings => GetDefaultSettings();

        public static List<string> GetSuggestions(string input, Func<string, bool> existsCallback = null)
        {
            return GetSuggestions(input, DefaultSettings, existsCallback);
        }

        public static List<string> GetSuggestions(string input, SuggestorSettings selectorSettings, Func<string, bool> existsCallback = null)
        {
            var suggestions = new List<string>();

            if (selectorSettings.WordList == null || !selectorSettings.WordList.Any())
            {
                if (!string.IsNullOrEmpty(selectorSettings.WordListFile))
                {
                    selectorSettings.WordList = File.ReadAllText(selectorSettings.WordListFile)
                        .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                }
                else
                {
                    selectorSettings.WordList = Resources.WordList
                        .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                }
            }

            int attempts = 0;
            while (suggestions.Count < selectorSettings.SuggestionCount && attempts < selectorSettings.MaximumAttempts)
            {
                string newSuggestion = CreateSuggestion(input, selectorSettings);

                // Check if the suggestion already exists
                if (suggestions.Contains(newSuggestion) ||
                    (existsCallback != null && existsCallback(newSuggestion)) ||
                    (selectorSettings.MaximumWordLength != null && newSuggestion.Length > selectorSettings.MaximumWordLength))
                {
                    attempts++; // Increment the attempts counter
                    continue; // Skip adding the duplicate suggestion
                }

                suggestions.Add(newSuggestion);
                attempts++; // Increment the attempts counter
            }


            return suggestions;
        }

        private static string CreateSuggestion(string input, SuggestorSettings selectorSettings)
        {
            // selectorSettings.Format = "{original}{}{}"
            string nonAlphanumericEnding = input.ExtractNonAlphanumeric();
            string newInput = input.Substring(0, input.Length - nonAlphanumericEnding.Length);
            string formatText = selectorSettings.Format.Replace("{original}", newInput);

            while (formatText.Contains("{}"))
            {
                string word = selectorSettings
                    .WordList[_random.Next(0, selectorSettings.WordList.Count)];

                word = word.FirstCharToUpper();

                formatText = formatText.ReplaceFirst("{}", word + nonAlphanumericEnding);
            }

            return formatText;
        }

        private static SuggestorSettings GetDefaultSettings()
        {
            var selectorSettings = new SuggestorSettings
            {
                Format = "{original}{}",
                MaximumWordLength = null,
                WordList = new List<string>(),
                SuggestionCount = 3,
                MaximumAttempts = 10
            };

            if (selectorSettings.WordList == null || !selectorSettings.WordList.Any())
            {
                selectorSettings.WordList = Resources.WordList
                    .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }

            return selectorSettings;
        }
    }
}
