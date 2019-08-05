using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insight_BL;

namespace DataLayer
{
    public interface ILoginMembershipRepository
    {
        LoginMembership Insert(LoginMembership obj);
    }
}
