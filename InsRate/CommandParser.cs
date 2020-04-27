using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
   public class CommandParser
    {

       public static Command parse(string commandText)
       {
           Command cmd = new Command();

           try
           {

              

               string[] elements = commandText.Split(',');

               cmd.keywork = elements[0];
               List<string> str = new List<string>();
               for (int i = 1; i < elements.Length; i++)
               {
                   str.Add(elements[i]);
               }
               cmd.parameters = str;
              
           }
           catch (Exception ex)
           {
               ErrorUtil.logError(ex, commandText);
           }
           return cmd;
       }



    }

}
