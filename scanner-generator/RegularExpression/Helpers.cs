using System;
using System.Collections.Generic;

namespace RegularExpression
{
    class Helpers
    {
        /// <summary>Check the precedence of the token</summary>
        /// <param name="token">The token to check the preference</param>
        /// <param name="lastOperator">The operator to compare</param>
        /// <param name="operators">The list of operators</param>
        /// <returns>True if the token precedence is less or equals than the operator precedence</returns>
        public bool CheckPrecedence(string token, string lastOperator, List<string> operators)
        {
            int operatorIndex = operators.FindIndex(x => x.Equals(lastOperator));
            int tokenIndex = operators.FindIndex(x => x.Equals(token));
            return tokenIndex >= operatorIndex;
        }

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

        /// <summary>Tokenize the text to evaluate</summary>
        /// <param name="text">The text to evaluate</param>
        /// <returns>A queue with all of the elements of the text</returns>
        public Queue<Element> TokenizeText(string text)
        {
            Queue<Element> elements = new Queue<Element>();
            text = text.Replace(Environment.NewLine, "\n");
            int row = 1;
            int column = 1;
            foreach(char character in text)
            {
                elements.Enqueue(new Element(character, column, row));
                if (character.Equals('\n'))
                {
                    row++;
                    column = 1;
                }
                else
                {
                    column++;
                }
            }
            return elements;
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