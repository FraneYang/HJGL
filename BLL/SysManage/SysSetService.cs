namespace BLL
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Data.Linq;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using Model;
    using BLL;

    public static class SysSetService
    {
        public static bool? IsAuto(int setId, string projectId)
        {
            var q = from x in Funs.DB.Sys_Set where x.SetId == setId && x.ProjectId==projectId select x;
            return q.First().IsAuto;
        }

        public static Model.Sys_Set GetSysSet(int setId, string projectId)
        {
            return Funs.DB.Sys_Set.Where(x => x.SetId == setId && x.ProjectId == projectId).FirstOrDefault();
        }
    }
}
