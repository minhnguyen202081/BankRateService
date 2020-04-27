using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public interface CommandProcessor
    {
        string process(string input, Command command, string bank);
    }

}
