using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class CPFDetails
	{

        private string cpfID;
        private string envelopeDeID;
        
        private string contractNo;
        private string receptionDate;
        private string previousUser;
        private string nextUser;

        private string sipCode;
		private string aWB;		


        private string qualified;
        private string status;
        private string channelName;   
    
        //modified by Minh

        private string disbursementStatus;
        private string disbursementRemark;

        private string stampingStatus;
        private string stampingRemark;
        
		

        public string EnvelopeDeID
        {
            get { return envelopeDeID; }
            set { envelopeDeID = value; }
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


        public string Qualified
        {
            get { return qualified; }
            set { qualified = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }
        //Modified by Minh 11-june-13

        public string DisbursementStatus
        {
            get { return disbursementStatus; }
            set { disbursementStatus = value; }
        }

        public string DisbursementRemark
        {
            get { return disbursementRemark; }
            set { disbursementRemark = value; }
        }

        public string StampingStatus
        {
            get { return stampingStatus; }
            set { stampingStatus = value; }
        }

        public string StampingRemark
        {
            get { return stampingRemark; }
            set { stampingRemark = value; }
        }

		public CPFDetails(string cpfID, string envelopeDeID, string contractNo, string qualified, string status,
            string receptionDate, string previousUser, string nextUser, string sipCode, string aWB, string channelName, string disbursementStatus, string disbursementRemark, string stampingStatus, string stampingRemark)
		{
            this.envelopeDeID = envelopeDeID;
            this.cpfID = cpfID;
            this.contractNo = contractNo;
            this.aWB = aWB;
			this.sipCode = sipCode;

            this.receptionDate = receptionDate;
            this.nextUser = nextUser;
            this.previousUser = previousUser;

            this.qualified = qualified;
            this.status = status;
            this.channelName = channelName;   
         
            //modified by Minh 11-june-13

            this.disbursementStatus = disbursementStatus;
            this.disbursementRemark = disbursementRemark;

            this.stampingRemark = stampingRemark;
            this.stampingStatus = stampingStatus;

		}

        public CPFDetails() { }


        
	}

}
