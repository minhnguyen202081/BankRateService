using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseComponent
{
	public class UserRoleDetails
	{

        private string userID;
        private string roleID;
        private string userName;
        private string roleName;
		private string screenName;
		

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
                        
		public string ScreenName
		{
			get {return screenName;}
            set { screenName = value; }
		}
        
        public UserRoleDetails(string userID, string roleID, string userName, string roleName, string screenName)
		{
            this.userID = userID;
            this.roleID = roleID;
            this.userName = userName;
            this.roleName = roleName;
            this.screenName = screenName;            
		}
        
        public UserRoleDetails() { }
        
	}

}
