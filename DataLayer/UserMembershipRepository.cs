using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public class UserMembershipRepository : IUserMembershipRepository
    {
        public UserMembership Insert(UserMembership obj)
        {
            using (InsightEntities db = new InsightEntities())
            {
                db.UserMembership.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }
    }
}
