using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Wrong : System.Web.UI.Page
    {
        /// <summary>
        /// 错误代码及对应的错误信息
        /// </summary>
        private static readonly string[] wrongMessage =

            new string[]
            {
               "您输入了非法的字符值，请检查文本输入框内的内容，去掉包含的<>\\\'%\";()&符号！",
               "1、你没有权限访问此页面<br>2、当前不允许此操作"
            };

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender">页面</param>
        /// <param name="e">事件参数</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = Request.Params["Message"];

            if (s != null)
            {
                try
                {
                    Message.InnerHtml = wrongMessage[Int32.Parse(s)];
                }
                catch (Exception ee)
                {
                    BLL.ErrLogInfo.WriteLog(string.Empty, ee);
                }
            }
            else if (Request.Params["MessageText"] != null)
            {
                try
                {
                    Message.InnerText = System.Web.HttpUtility.HtmlDecode(Request.Params["MessageText"]);
                }
                catch (Exception ee)
                {
                    BLL.ErrLogInfo.WriteLog(string.Empty, ee);
                }
            }
        }
    }
}
