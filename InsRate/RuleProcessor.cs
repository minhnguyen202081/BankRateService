using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Library
{
    public class RuleProcessor
    {
      public static string process(String input, String rule, string bank ) {
                                string result = input;
                                try {
                                                char delimiter = Convert.ToChar(ConfigurationManager.AppSettings["delimiter"]);
                                                string[] commandTextList =  rule.Split(delimiter);
                                                foreach( string commandText in commandTextList)
                                                {
                                                                Command command = CommandParser.parse(commandText);

                                                                CommandProcessor commandProcessor = CommandProcessorFactory.getCommandProcessor(command);
                                                                result = commandProcessor.process(result, command,bank).Trim();
                                                }
                                           
                                }
                                    
                                catch (Exception e) {
                                    ErrorUtil.logError(e,rule + " " + bank);
                                }
                                return result;
                }

    
    }
}
