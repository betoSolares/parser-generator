using Regex;
using System;
using System.Collections.Generic;

namespace scanner_generator.Regex
{
    class Helpers
    {
        /// <summary>Tokenize the regular expression</summary>
        /// <param name="regex">The regular expression</param>
        /// <returns>A list with all the tokens</returns>
        public List<string> TokenizeExpression(string regex)
        {
            List<string> tokens = new List<string>();
            for (int i = 0; i < regex.Length; i++)
            {
                char character = regex[i];
                if (character.Equals('\\'))
                {
                    try
                    {
                        tokens.Add(character.ToString() + regex[i + 1].ToString());
                    }
                    catch (Exception)
                    {
                        throw new BadExpressionException(@"the character \ must be followed by another character");
                    }
                    if (i != regex.Length - 1)
                        i++;
                }
                else if (character.Equals('['))
                {
                    try
                    {
                        string token = regex.Substring(i, 5);
                        if (token[4].Equals(']'))
                        {
                            if (DigitInterval(token) || UpperInterval(token) || LowerInterval(token))
                            {
                                tokens.Add(token);
                                i += 4;
                            }
                            else
                            {
                                throw new BadExpressionException("Bad interval");
                            }
                        }
                        else
                        {
                            throw new BadExpressionException("The character [ must have a closing character");
                        }
                    }
                    catch (Exception)
                    {
                        throw new BadExpressionException("The character [ must have a closing character");
                    }
                }
                else if (character.Equals(']'))
                {
                    throw new BadExpressionException("Invalid interval");
                }
                else
                {
                    tokens.Add(character.ToString());
                }
            }
            return tokens;
        }

        /// <summary>Check if the interval is two numbers</summary>
        /// <param name="token">The token to check</param>
        /// <returns>True if are numbers</returns>
        private bool DigitInterval(string token)
        {
            return char.IsDigit(token[1]) && char.IsDigit(token[3]);
        }

        /// <summary>Check if the interval is two upper case letters</summary>
        /// <param name="token">The token to check</param>
        /// <returns>True if are two upper case letters</returns>
        private bool UpperInterval(string token)
        {
            return char.IsUpper(token[1]) && char.IsUpper(token[3]);
        }

        /// <summary>Check if the interval is two lower case letters</summary>
        /// <param name="token">The token to check</param>
        /// <returns>True if are two lower case letters</returns>
        private bool LowerInterval(string token)
        {
            return char.IsLower(token[1]) && char.IsLower(token[3]);
        }
    }
}