using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public  class CommandProcessorFactory
    {
        public static CommandProcessor getCommandProcessor(Command command)
        {
            CommandProcessor obj = null;
            try
            {
                Type t = Type.GetType("Library." + command.keywork + "CommandProcessor");

                 obj = (CommandProcessor)Activator.CreateInstance(t);


                
            }
            catch(Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
            return obj;

        }
    }
}
