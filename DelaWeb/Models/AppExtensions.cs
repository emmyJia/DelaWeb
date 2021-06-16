using DelaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace App.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetCustomerID(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CustomerID");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetCustomerType(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CustomerType");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static bool IsMaster(this IIdentity identity)
        {
            return Int32.Parse(identity.GetCustomerType()) == (int)DelaWeb.Models.CustomerType.Master;

        }
    }
}