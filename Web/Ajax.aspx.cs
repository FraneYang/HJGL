using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Text;

namespace Web
{
    public partial class Ajax : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxRequest(); 
        }

        /// <summary>
        /// 搜索要执行的方法
        /// </summary>
        private void AjaxRequest()
        {
            string operate = Request.Params["operate"];
            MethodRefelect.InvokeMethod(this, "Web", "Ajax", operate);
        }
                
        /// <summary>
        /// 根据用户姓名模糊查询所有用户信息
        /// </summary>
        public void GetUsersByUserName()
        {
            //string userName = Request.QueryString["q"];
            //StringBuilder sbstr = new StringBuilder();
            //List<Model.Sys_User> users = BLL.UserService.GetUsersByUserName(userName);
            //sbstr.Append("[");
            //for (int i = 0; i < users.Count(); i++)
            //{
            //    if (i == users.Count() - 1)
            //    {
            //        sbstr.Append("{name:'" + users[i].UserName + "',to:'" + users[i].UserId + "'}");
            //    }
            //    else
            //    {
            //        sbstr.Append("{name:'" + users[i].UserName + "',to:'" + users[i].UserId + "'},");
            //    }
            //}
            //sbstr.Append("]");
            //Response.Write(sbstr);
            //Response.End();
        }
    }
}