using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class UserDetails
	{

        private string userID;
        private string userName;
        private string fullName;
		private string offToday;
		

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }


        
		public string OffToday
		{
			get {return offToday;}
            set { offToday = value; }
		}


        public UserDetails(string userID, string userName, string fullName, string offToday)
		{
            this.userID = userID;
            this.userName = userName;
            this.fullName = fullName;
            this.offToday = offToday;
            
		}

        public UserDetails() { }

        
	}

}
