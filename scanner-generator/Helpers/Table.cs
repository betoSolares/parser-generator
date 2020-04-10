using System.Collections.Generic;

namespace Helpers
{
    public class Table
    {
        /// <summary>Create a string with the elements on the list</summary>
        /// <param name="list">The list with the elements</param>
        /// <returns>A string with each element separated by a comma</returns>
        public string GetList(List<int> list)
        {
            string text = string.Empty;
            foreach(int number in list)
            {
                text += number + ", ";
            }
            
            if (text.Length == 0)
            {
                return " --- ";
            }
            else
            {
                return text.Remove(text.Length - 2, 2);
            }
        }
    }
}
