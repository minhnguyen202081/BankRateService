using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class MRCListDetails
	{

        private string mrcDeID;
        private string mrcID;
        private string mrcCode;
		private string customerName;		
		private string plateNo;
        private string comment;



        public string MrcDeID
        {
            get { return mrcDeID; }
            set { mrcDeID = value; }
        }

        public string MrcID
        {
            get { return mrcID; }
            set { mrcID = value; }
        }

        public string MrcCode
        {
            get { return mrcCode; }
            set { mrcCode = value; }
        }



        public string CustomerName
		{
            get { return customerName; }
            set { customerName = value; }
		}

        public string PlateNo
		{
            get { return plateNo; }
            set { plateNo = value; }
		}
		

        public string Comment
		{
			get {return comment;}
			set {comment = value;}
		}


        public MRCListDetails(string mrcDeID, string mrcID, string mrcCode, string customerName, string plateNo, string comment)
		{
            this.mrcDeID = mrcDeID;
            this.mrcID = mrcID;
            this.mrcCode = mrcCode;
			this.comment = comment;
            this.customerName = customerName;
            this.plateNo = plateNo;
            
		}

        public MRCListDetails() { }

        
	}

}
