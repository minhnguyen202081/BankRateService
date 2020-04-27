using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using DataAccessLayer;
using System.Net.Mail;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

    namespace Library
    {


















        public static class Helper
        {
            /// <summary>
            /// Converts a DataTable to a list with generic objects
            /// </summary>
            /// <typeparam name="T">Generic object</typeparam>
            /// <param name="table">DataTable</param>
            /// <returns>List with generic objects</returns>
            public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
            {
                try
                {
                    List<T> list = new List<T>();

                    foreach (var row in table.AsEnumerable())
                    {
                        T obj = new T();

                        foreach (var prop in obj.GetType().GetProperties())
                        {
                            try
                            {
                                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        list.Add(obj);
                    }

                    return list;
                }
                catch
                {
                    return null;
                }
            }
        }



        class LibraryFuncs
        {

            public static string FindCommand(string sInput, string sFind)
            {
                string sResult = "";

                int iPos;

                iPos = sInput.IndexOf(sFind, 0);

                if (iPos > 0)
                    sResult = sInput.Substring(iPos + sFind.Length, sInput.Length - iPos - sFind.Length);

                return sResult;
            }


            public static void SendEmail(string subject, string body, string[] listAttachments, string[] listImages, MailPriority priority = MailPriority.Normal)
            {
                string strToAddress, strCCAddress;
                //send email
                // create mail message object
                MailMessage msg = new MailMessage();
                strToAddress = ConfigurationManager.AppSettings["MailToAddresses"].ToString();
                strCCAddress = ConfigurationManager.AppSettings["MailCCAddresses"].ToString();
                if (!strToAddress.Equals(string.Empty)) msg.To.Add(strToAddress);
                if (!strCCAddress.Equals(string.Empty)) msg.To.Add(strCCAddress);

                msg.From = new MailAddress("sgvfeod.automonitor@sgvf.com.vn");
                msg.Priority = priority;
                msg.SubjectEncoding = System.Text.Encoding.ASCII;
                msg.Subject = subject + " " + String.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);


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

                msg.BodyEncoding = System.Text.Encoding.ASCII;
                //msg.Body = body;
                msg.IsBodyHtml = true;

                if (listImages != null)
                {
                    foreach (string file in listImages)
                    {
                        if (File.Exists(file))
                        {
                            body = body + string.Format(@"<img src=""cid:{0}"" />", Path.GetFileNameWithoutExtension(file)) + "<BR>";
                        }
                    }

                    AlternateView view = AlternateView.CreateAlternateViewFromString(body, new System.Net.Mime.ContentType("text/html"));

                    foreach (string file in listImages)
                    {
                        if (File.Exists(file))
                        {
                            LinkedResource objLinkedRes = new LinkedResource(file);
                            objLinkedRes.ContentId = Path.GetFileNameWithoutExtension(file);
                            view.LinkedResources.Add(objLinkedRes);
                        }
                    }

                    msg.AlternateViews.Add(view);
                }
                else
                    msg.Body = body;              

                //Add the Creddentials
                SmtpClient client = new SmtpClient("mail.sgvf.com.vn", 2525);
                client.Credentials = new System.Net.NetworkCredential("sgvfeod.automonitor", "@ut0M0n1t0r", "sgvf");
                client.EnableSsl = false;
                client.Send(msg);
                msg.Dispose();

            }

            public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
            {
                if (!Directory.Exists(path)) return null;

                string[] searchPatterns = searchPattern.Split('|');
                List<string> files = new List<string>();
                foreach (string sp in searchPatterns)
                    files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
                files.Sort();
                return files.ToArray();
            }

            public static void LogFile(String str)
            {            
                // Create a writer and open the file:
                StreamWriter log;
                if (!File.Exists("logfile.txt"))
                {
                    log = new StreamWriter("logfile.txt");
                }
                else
                {
                    log = File.AppendText("logfile.txt");
                }
                // Write to the file:
                log.WriteLine(DateTime.Now);
                log.WriteLine(str);
                log.WriteLine();
                // Close the stream:
                log.Close();
                //end of write             
            }

            public static SqlConnection Connection()
            {
                String datasource = ConfigurationManager.AppSettings["SqlDatasource"].ToString();
                String username = ConfigurationManager.AppSettings["SqlUsername"].ToString();
                String userid = ConfigurationManager.AppSettings["SqlUserid"].ToString();
                String password = ConfigurationManager.AppSettings["SqlPassword"].ToString();
                if (!password.Equals(string.Empty))
                    password = Library.LibraryFuncs.DecryptString(password, "encryptpasswordSGVF");

                SqlConnection conn = new SqlConnection();
                string str = "Data Source=" + datasource + "; Initial Catalog=" + username + ";" + "Persist Security Info=True;User ID= " + userid + ";Password=" + password;
                conn.ConnectionString = str;
                conn.Open();
                return conn;
            }

            public static void InsertRateHistory(string GroupCode, string BankCode, string TenorCode, string InsRate)
            {
                SqlConnection conn = new SqlConnection();
                   conn= Connection();
              

                SqlCommand cmd = new SqlCommand("sp_RateHistory_ins", conn);
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GroupCode", SqlDbType.NVarChar, 50).Value = GroupCode;
                    cmd.Parameters.Add("@BankCode", SqlDbType.NVarChar, 50).Value = BankCode;
                    cmd.Parameters.Add("@TenorCode", SqlDbType.NVarChar, 50).Value = TenorCode;
                    cmd.Parameters.Add("@InsRate", SqlDbType.NVarChar, 50).Value = InsRate;

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                }
            }

            public static DataTable ExecuteSQLReturnDataTable(string strSQL)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.Text;
                SqlConnection sqlconn = Connection();
                DataTable dtb = new DataTable();

                try
                {
                    cmd.Connection = sqlconn;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtb);
                    cmd.Parameters.Clear();
                    return dtb;
                }
                catch
                {
                    return (dtb = null);
                }
                finally
                {
                    cmd.Dispose();
                    sqlconn.Close();
                    sqlconn.Dispose();
                }
            }

            public static string FontBold(string str)
            {
                return "<b>" + str + "</b>";
            }
            public static string FontRed(string str)
            {
                return "<font color=red>" + str + "</font>";
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
