using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
    public class TKLDetails
    {

        private string tksLetterID;
        private string envelopeDeID;
        
        private string contractNo;
        private string receptionDate;
        private string sipCode;       
                
        private string status;
        private string qualified;
        private string previousUser;
        private string awb;        
        private string comment;


        public string TksLetterID
        {
            get { return tksLetterID; }
            set { tksLetterID = value; }
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

        public string SipCode
        {
            get { return sipCode; }
            set { sipCode = value; }
        }



        
        


        
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Qualified
        {
            get { return qualified; }
            set { qualified = value; }
        }



        public string PreviousUser
        {
            get { return previousUser; }
            set { previousUser = value; }
        }



        public string Awb
        {
            get { return awb; }
            set { awb = value; }
        }

        

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }







        public TKLDetails(string tksLetterID, string envelopeDeID, string contractNo, string receptionDate, string sipCode,
            string status, string qualified, string previousUser, string awb, string comment)
        {

            this.tksLetterID = tksLetterID;
            this.envelopeDeID = envelopeDeID;
            
            this.contractNo = contractNo;
            this.comment = comment;
            this.sipCode = sipCode;

            this.receptionDate = receptionDate;
            this.awb = awb;
            this.previousUser = previousUser;

            this.qualified = qualified;
            this.status = status;
            
        }

        public TKLDetails() { }



    }

}
