using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    public class DeleteCommandProcessor : CommandProcessor
    {

        public const string BEFORE = "Before";
        public const string AFTER = "After";

        public string process(String input, Command command, string bank )
        {
          //  input = Utilities.RemoveWhitespace(input);
            string result = "";
            try
            {
                switch (command.parameters[0].ToUpper())
                {
                    case "BEFORE":
                        result = input.Remove(0, input.IndexOf(Regex.Unescape(command.parameters[1])) +  Regex.Unescape(command.parameters[1]).Length);

                        break;
                    case "AFTER":
                        result = input.Remove(input.IndexOf(Regex.Unescape(command.parameters[1])));
                        break;
                }
            }
            catch(Exception ex)
            {
                ErrorUtil.logError(ex, command.parameters[0].ToUpper() +" "+ input);
            }

            return result;

        }


    }
}
