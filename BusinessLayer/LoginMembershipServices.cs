using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;
using DataLayer;

namespace BusinessLayer
{
    public class LoginMembershipServices
    {
        static ILoginMembershipRepository repository;

        static LoginMembershipServices()
        {
            repository = new LoginMembershipRepository();
        }
        public static LoginMembership Insert(LoginMembership obj)
        {
            return repository.Insert(obj);
        }
    }
}
