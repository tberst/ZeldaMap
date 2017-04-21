using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace zeldassistant
{
    public class Helper
    {
        public static int GetUserId(ClaimsIdentity claims)
        {
            foreach (Claim claim in claims.Claims)
            {
                if (claim.Type == "id")
                {
                    return int.Parse(claim.Value);
                }
            }
            return -1;
        }
    }
}
