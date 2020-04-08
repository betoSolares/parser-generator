using System.Collections.Generic;

namespace RegularExpression
{
    public class Utils
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
    }
}