using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class AWBDetails
	{

        private string envelopeID;
        private string receptionDate;
        private string nextUser;

		private string aWB;
		private string channelName;
		private string sipCode;
        private string comment;
        private string deletable;


        public string EnvelopeID
        {
            get { return envelopeID; }
            set { envelopeID = value; }
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


        
		public string AWB
		{
			get {return aWB;}
			set {aWB = value;}
		}

		public string ChannelName
		{
			get {return channelName;}
			set {channelName = value;}
		}
		public string SipCode
		{
			get {return sipCode;}
			set {sipCode = value;}
		}

        public string Comment
		{
			get {return comment;}
			set {comment = value;}
		}

        public string Deletable
        {
            get { return deletable; }
            set { deletable = value; }
        }

        public AWBDetails(string envelopeID, string receptionDate, string aWB, string channelName, string sipCode, string comment, string nextUser, string deletable)
		{
			this.aWB = aWB;
			this.channelName = channelName;
			this.sipCode = sipCode;
			this.comment = comment;
            this.envelopeID = envelopeID;
            this.receptionDate = receptionDate;
            this.nextUser = nextUser;
            this.deletable = deletable;


		}
        
        public AWBDetails() { }

        
	}

}
