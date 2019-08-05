using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Insight_BL;

namespace BusinessLayer
{
    public class UserMembershipServices
    {
        static IUserMembershipRepository repository;

        static UserMembershipServices()
        {
            repository = new UserMembershipRepository();
        }
        public static UserMembership Insert(UserMembership obj)
        {
            return repository.Insert(obj);
        }
    }
}
