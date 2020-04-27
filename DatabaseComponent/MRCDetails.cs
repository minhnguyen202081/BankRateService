using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class MRCDetails
	{

        private string mrcID;
        private string envelopeDeID;
        
        private string contractNo;
        private string receptionDate;
        private string previousUser;        

        private string sipCode;
		private string aWB;		
                
        private string status;
        private string mrcQuantity;
        private string enteredQuantity;
        
        public string MrcID
        {
            get { return mrcID; }
            set { mrcID = value; }
        }

        public string EnvelopeDeID
        {
            get { return envelopeDeID; }
            set { envelopeDeID = value; }
        }

                

        public string ContractNo
        {
            get { return contractNo; }
            set { contractNo = value; }
        }


        


        public string ReceptionDate
        {
            get { return receptionDate; }
            set { receptionDate = value; }
        }

        
        public string PreviousUser
        {
            get { return previousUser; }
            set { previousUser = value; }
        }


        
        public string SipCode
		{
			get {return sipCode;}
			set {sipCode = value;}
		}

		public string AWB
		{
			get {return aWB;}
			set {aWB = value;}
		}

		
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
		
        public string MrcQuantity
        {
            get { return mrcQuantity; }
            set { mrcQuantity = value; }
        }

        public string EnteredQuantity
        {
            get { return enteredQuantity; }
            set { enteredQuantity = value; }
        }

		public MRCDetails(string mrcID, string envelopeDeID, string contractNo, string receptionDate, string previousUser,
            string sipCode, string aWB, string status, string mrcQuantity, string enteredQuantity)
		{
            this.envelopeDeID = envelopeDeID;
            this.mrcID = mrcID;
            this.contractNo = contractNo;
            this.receptionDate = receptionDate;
            this.previousUser = previousUser;

            this.aWB = aWB;
			this.sipCode = sipCode;
            this.status = status;

            this.mrcQuantity = mrcQuantity;
            this.enteredQuantity = enteredQuantity;

		}

        public MRCDetails() { }


        
	}

}
