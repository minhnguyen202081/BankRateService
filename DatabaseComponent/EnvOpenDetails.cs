using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class EnvOpenDetails
	{

        private string envelopeDeID;
        private string envelopeID;

        private string docTypeID;
        private string docTypeName;

        private string contractNo;
        private string mrcQuantity;      

        private string receptionDate;
        private string nextUser;
        private string previousUser;

		private string aWB;		
		private string sipCode;
        private string comment;
        private string deletable;
        
        public string EnvelopeDeID
        {
            get { return envelopeDeID; }
            set { envelopeDeID = value; }
        }

        public string EnvelopeID
        {
            get { return envelopeID; }
            set { envelopeID = value; }
        }

        public string DocTypeID
        {
            get { return docTypeID; }
            set { docTypeID = value; }
        }

        public string DocTypeName
        {
            get { return docTypeName; }
            set { docTypeName = value; }
        }

        public string ContractNo
        {
            get { return contractNo; }
            set { contractNo = value; }
        }

        public string MrcQuantity
        {
            get { return mrcQuantity; }
            set { mrcQuantity = value; }
        }


        public string ReceptionDate
        {
            get { return receptionDate; }
            set { receptionDate = value; }
        }

        public string NextUser
        {
            get { return nextUser; }
            set { nextUser = value; }
        }

        public string PreviousUser
        {
            get { return previousUser; }
            set { previousUser = value; }
        }


        
		public string AWB
		{
			get {return aWB;}
			set {aWB = value;}
		}

		
		public string SipCode
		{
			get {return sipCode;}
			set {sipCode = value;}
		}

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public string Deletable
        {
            get { return deletable; }
            set { deletable = value; }
        }

        
        

		public EnvOpenDetails(string envelopeDeID, string envelopeID, string docTypeID, string docTypeName, string contractNo, string mrcQuantity,
            string receptionDate, string aWB, string sipCode, string nextUser, string previousUser, string comment, string deletable)
		{
			
            this.envelopeDeID = envelopeDeID;
            this.envelopeID = envelopeID;
			this.docTypeID = docTypeID;
            this.docTypeName = docTypeName;

            this.contractNo = contractNo;
            this.mrcQuantity = mrcQuantity;

            this.aWB = aWB;
			this.sipCode = sipCode;

            
            this.receptionDate = receptionDate;
            this.nextUser = nextUser;
            this.previousUser = previousUser;
            this.comment = comment;
            this.deletable = deletable;
            
		}

        public EnvOpenDetails() { }

        
	}

}
