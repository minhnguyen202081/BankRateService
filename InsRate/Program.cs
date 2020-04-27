using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.Data.SqlClient;
using System.Net;
using System.IO;    
using System.Net.Mime;
using Library;
using System.Text.RegularExpressions;


namespace InsRate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]




        static void Main()
        {
            AutoExtractInterest.mainProcess();





        }

    }
}
