using System;
using System.Collections.Generic;

namespace Helpers
{
    public class TextManipulation
    {
        /// <summary>Get all the elements under the ACTIONS section</summary>
        /// <param name="text">The text to parse</param>
        /// <returns>A dictionary with all the actions</returns>
        public Dictionary<string, string> GetActions(string text)
        {
            int indexFrom = text.IndexOf("ACTIONS") + "ACTIONS".Length;
            int indexTo = text.LastIndexOf("ERROR");
            string subtext = text.Substring(indexFrom, indexTo - indexFrom);
            indexFrom = subtext.IndexOf("()") + "()".Length;
            indexTo = subtext.LastIndexOf("}");
            string result = subtext.Substring(indexFrom, indexTo - indexFrom);
            string[] actions = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return MakeDictionary(actions);
        }

        /// <summary>Get all the elements under the SETS section</summary>
        /// <param name="text">The text to parse</param>
        /// <returns>A dictionary with all the SETS</returns>
        public Dictionary<string, string> GetSets(string text)
        {
            if (text.Contains("SETS"))
            {
                int indexFrom = text.IndexOf("SETS") + "SETS".Length;
                int indexTo = text.LastIndexOf("TOKENS");
                string result = text.Substring(indexFrom, indexTo - indexFrom);
                string[] sets = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                return MakeDictionary(sets);
            }
            return null;
        }

        /// <summary>Get all the elements under the TOKENS section</summary>
        /// <param name="text">The text to parse</param>
        /// <returns>A dictionary with all the TOKENS</returns>
        public Dictionary<string, string> GetTokens(string text)
        {
            int indexFrom = text.IndexOf("TOKENS") + "TOKENS".Length;
            int indexTo = text.LastIndexOf("ACTIONS");
            string result = text.Substring(indexFrom, indexTo - indexFrom);
            string[] tokens = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return NormalizeDictionary(MakeDictionary(tokens));
        }

        /// <summary>Transform an array into a dictionary</summary>
        /// <param name="elements">The array to transform</param>
        /// <returns>A new dictionary</returns>
        private Dictionary<string, string> MakeDictionary(string[] elements)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string element in elements)
            {
                if (!element.Equals(string.Empty) && !string.IsNullOrWhiteSpace(element))
                {
                    string[] parts = element.Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        result.Add(parts[0].Trim(new[] { '\t', ' ' }), parts[1].Trim(new[] { '\t', ' ' }));
                    }
                }
            }
            return result;
        }

        /// <summary>Delete the call functions from the TOKENS</summary>
        /// <param name="dictionary">The original dictionary to normalize</param>
        /// <returns>A new dictionary</returns>
        private Dictionary<string, string> NormalizeDictionary(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> newDictionary = new Dictionary<string, string>(dictionary);
            foreach (KeyValuePair<string, string> element in dictionary)
            {
                int indexFrom = element.Value.IndexOf("{");
                int indexTo = element.Value.LastIndexOf("}");
                if (indexFrom != -1 && indexTo != -1)
                {
                    string newValue = element.Value.Remove(indexFrom, indexTo - indexFrom + 1);
                    newDictionary[element.Key] = newValue.Trim(new[] { '\t', ' ' });
                }
            }
            return newDictionary;
        }

    }
}
