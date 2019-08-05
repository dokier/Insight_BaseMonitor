using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class LoginMembershipRepository : ILoginMembershipRepository
    {
        public LoginMembership Insert(LoginMembership obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.LoginMembership.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }
    }
}
