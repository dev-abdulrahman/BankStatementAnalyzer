using System.Collections.Generic;

namespace BankStatementAnalyzer.WebUI.ViewModels.UsersVM
{
    public class UserCompany
    {
        public UserCompany()
        {
            Users = new List<string>();
            MappedUsers = new List<MappedUser>();
        }

        public List<string> Users { get; set; }

        public List<MappedUser> MappedUsers { get; set; }
    }

    public class MappedUser
    {
        public string UserName { get; set; }
    }
}