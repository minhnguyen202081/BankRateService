using System;
using System.Collections.Generic;
using System.Linq;
//using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Library
{
    public class LibraryFuncs
    {
        public static void SendEmail(string subject, string body, string[] listAttachments, MailPriority priority = MailPriority.Normal)
        {
            //send email
            // create mail message object
            MailMessage msg = new MailMessage();
            string strToAddress = System.Configuration.ConfigurationManager.AppSettings["MailToAddresses"].ToString();
            string strCCAddress = System.Configuration.ConfigurationManager.AppSettings["MailCCAddresses"].ToString();
            if (!strToAddress.Equals(string.Empty)) msg.To.Add(strToAddress);
            if (!strCCAddress.Equals(string.Empty)) msg.To.Add(strCCAddress);

            msg.From = new MailAddress("sgvfeod.automonitor@sgvf.com.vn");
            msg.Priority = priority;
            msg.SubjectEncoding = System.Text.Encoding.ASCII;
            msg.Subject = subject + " " + String.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);
            msg.BodyEncoding = System.Text.Encoding.ASCII;
            msg.IsBodyHtml = true;
            msg.Body = body;

            if (listAttachments != null)
            {
                foreach (string file in listAttachments)
                {
                    if (File.Exists(file))
                    {
                        msg.Attachments.Add(new Attachment(file));
                    }
                }
            }


            //Add the Creddentials
            SmtpClient client = new SmtpClient("mail.sgvf.com.vn", 2525);
            client.Credentials = new System.Net.NetworkCredential("sgvfeod.automonitor", "@ut0M0n1t0r", "sgvf");
            client.EnableSsl = false;
            client.Send(msg);
            msg.Dispose();

        }


        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
            files.Sort();
            return files.ToArray();
        }

        public static void LogFile(String filePath, string content)
        {            
            // Create a writer and open the file:
            StreamWriter log;
            string filename = "logfile_" + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".log";
            if (!File.Exists(filePath + filename))
            {
                log = new StreamWriter(filePath + filename);
            }
            else
            {
                log = File.AppendText(filePath + filename);
            }
            // Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(content);
            log.WriteLine();
            // Close the stream:
            log.Close();
            //end of write             
        }

        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            catch
            {
                return string.Empty;            
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }

        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }
    }
}
