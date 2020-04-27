using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class REPORTLOGDetails
	{
        private string reportLogID;
        private string reportCode;
        private string reportName;
        
        private string extractedDate;
        private string userName;
        private string fileName;        

        private string status;
		private string fileLocation;
        private string duration;
        public string ReportLogID
        {
            get { return reportLogID; }
            set { reportLogID = value; }
        }

        public string ReportCode
        {
            get { return reportCode; }
            set { reportCode = value; }
        }

        public string ReportName
        {
            get { return reportName; }
            set { reportName = value; }
        }

                

        public string ExtractedDate
        {
            get { return extractedDate; }
            set { extractedDate = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }


        
        public string Status
		{
			get {return status;}
			set {status = value;}
		}

		public string FileLocation
		{
			get {return fileLocation;}
			set {fileLocation = value;}
		}
        public string Duration
        {
            get { return duration ; }
            set { duration = value; }
        }



        public REPORTLOGDetails(string reportLogID, string reportCode, string reportName, string extractedDate, string userName, string fileName,
            string status, string fileLocation,string duration)
		{
            this.reportLogID = reportLogID;
            this.reportCode = reportCode;
            this.reportName = reportName;
            this.extractedDate = extractedDate;
            this.userName = userName;
            this.fileName = fileName;
            this.status = status;
            this.fileLocation = fileLocation;
            this.duration = duration;
		}

        public REPORTLOGDetails() { }


        
	}

}
