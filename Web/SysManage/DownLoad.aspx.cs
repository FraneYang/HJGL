using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class DownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = Request["path"].ToString();
            string path = Server.MapPath(s);

            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(System.IO.Path.GetFileName(path)));
            Response.AppendHeader("Content-Length", fi.Length.ToString());
            Response.WriteFile(path);
            Response.Flush();
            Response.End(); 
        }
    }
}