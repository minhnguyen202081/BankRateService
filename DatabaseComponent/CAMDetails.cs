using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
    public class CAMDetails
    {

        private string mistakeID;
        private string envelopeDeID;

        private string mistakeTypeID;
        private string mistakeTypeName;
        private string contractNo;
        private string receptionDate;
        private string qualified;

        private string sipCode;
        private string status;
        private string previousUser;

        private string nextUser;        
        private string comment;


        public string MistakeID
        {
            get { return mistakeID; }
            set { mistakeID = value; }
        }


        public string EnvelopeDeID
        {
            get { return envelopeDeID; }
            set { envelopeDeID = value; }
        }

        
        public string MistakeTypeID
        {
            get { return mistakeTypeID; }
            set { mistakeTypeID = value; }
        }


        public string MistakeTypeName
        {
            get { return mistakeTypeName; }
            set { mistakeTypeName = value; }
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

        public string Qualified
        {
            get { return qualified; }
            set { qualified = value; }
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

        public string PreviousUser
        {
            get { return previousUser; }
            set { previousUser = value; }
        }



        public string NextUser
        {
            get { return nextUser; }
            set { nextUser = value; }
        }

        

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }







        public CAMDetails(string mistakeID, string envelopeDeID, string mistakeTypeID, string mistakeTypeName, string contractNo, string receptionDate, string qualified, 
            string sipCode, string status, string previousUser, string nextUser,  string comment)
        {

            this.mistakeID = mistakeID;
            this.envelopeDeID = envelopeDeID;

            this.mistakeTypeID = mistakeTypeID;
            this.mistakeTypeName = mistakeTypeName;
            this.contractNo = contractNo;
            this.comment = comment;
            this.sipCode = sipCode;

            this.receptionDate = receptionDate;
            this.nextUser = nextUser;
            this.previousUser = previousUser;

            this.qualified = qualified;
            this.status = status;
            
        }

        public CAMDetails() { }



    }

}
