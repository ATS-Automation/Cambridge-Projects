using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCWebserviceWrappers
{
    public class GCWebServiceWrapper
    {
        public static bool IsUserGroupMember(string emailAddress, string groupName, string userName)
        {
            var ws = new maingcwebservice.ATSGlobalCatalogQuery();
            ws.PreAuthenticate = true;
            ws.UseDefaultCredentials = true;
            string isInGroup = ws.IsUserGroupMember(emailAddress, groupName,  userName);
            ws.Dispose();
            ws = null;
            if (isInGroup == "true")
                return true;
            else
                return false;
        }
    }
}
