using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class CPFCheckInDetails
	{

        private string cpfDetailID;
        private string cpfID;
        
        private string contractNo;
        private string doc;
        private string status;
        private string mistake;

        private string mistakeType;
        private string description;

        private string docID;

        private string category;

        public string CpfDetailID
        {
            get { return cpfDetailID; }
            set { cpfDetailID = value; }
        }

        public string CpfID
        {
            get { return cpfID; }
            set { cpfID = value; }
        }
                

        public string ContractNo
        {
            get { return contractNo; }
            set { contractNo = value; }
        }


        


        public string Doc
        {
            get { return doc; }
            set { doc = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Mistake
        {
            get { return mistake; }
            set { mistake = value; }
        }


        
		public string MistakeType
		{
			get {return mistakeType;}
			set {mistakeType = value;}
		}

		
		public string Description
		{
			get {return description ;}
			set {description = value;}
		}

        public string DocID
        {
            get { return docID; }
            set { docID = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

      
		public CPFCheckInDetails(string cpfDetailID, string cpfID, string contractNo, string doc, string status,
            string mistake, string mistakeType, string description, string docID, string category)
		{
            this.cpfDetailID  = cpfDetailID;
            this.cpfID = cpfID;
            this.contractNo = contractNo;
            this.contractNo = contractNo;
			this.doc = doc;

       
            this.status = status;
            this.mistake = mistake;   
         
            //modified by Minh 11-june-13

            this.mistakeType = mistakeType;
            this.description = description;

            this.docID = docID;
            this.category = category;


		}

      


        
	}

}
