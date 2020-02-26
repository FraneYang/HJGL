namespace Web
{
    using System;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Web;
    using BLL;

    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// 自动启用插件标志文件路径
        /// </summary>
        private static string applicationActiveFlagFilePhysicalPath = String.Empty;

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["UserNum"] = 0;

            //// 自动开启插件标志文件

            //if (File.Exists(applicationActiveFlagFilePhysicalPath))
            //{
            //    BLL.ErrLogInfo.WriteLog("Application_start----AUTO StartRunScheduler");               
            //    File.Delete(applicationActiveFlagFilePhysicalPath);
            //}

            try
            {
                // 日志文件所在目录
                ErrLogInfo.DefaultErrLogFullPath = Server.MapPath("~/ErrLog.txt");
                Funs.ConnString = ConfigurationManager.AppSettings["ConnectionString"];
                Funs.UnitSet = ConfigurationManager.AppSettings["UnitSet"];
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
                AppDomain.Unload(AppDomain.CurrentDomain);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 36000;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            StringBuilder errLog = null;
            Exception ex = null;
            Model.Sys_ErrLogInfo newErr = new Model.Sys_ErrLogInfo();
            newErr.ErrLogId = SQLHelper.GetNewID(typeof(Model.Sys_ErrLogInfo));
            try
            {
                // 获取错误类
                ex = Server.GetLastError().InnerException;
                if (ex == null)
                {
                    ex = Server.GetLastError().GetBaseException();
                }              
                errLog = new StringBuilder();
                errLog.Append(String.Format(CultureInfo.InvariantCulture, "出错文件:{0}\r\n", Request.Url.AbsoluteUri));
                newErr.ErrUrl = Request.Url.AbsoluteUri;

                if (Request.UserHostAddress != null)
                {
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "IP地址:{0}\r\n", Request.UserHostAddress));
                    newErr.ErrIP = Request.UserHostAddress;
                }

                if (Session != null && Session["CurrUser"] != null)
                {                    
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "操作人员:{0}\r\n", ((Model.Sys_User)Session["CurrUser"]).UserName));
                    newErr.UserName = ((Model.Sys_User)Session["CurrUser"]).UserId;
                }
                else
                {
                    PPage.ZXRefresh(Request.ApplicationPath + "/LogOff.aspx");
                }

                if (ex is HttpRequestValidationException)
                {
                    PPage.ZXRefresh(Request.ApplicationPath + "/Wrong.aspx?Message=0");
                }
                else if (ex is FriendlyException)
                {
                    PPage.ZXRefresh(Request.ApplicationPath + "/Wrong.aspx?MessageText=" + ex.Message);
                }
            }
            catch
            {
                try
                {
                    PPage.ZXRefresh(Request.ApplicationPath + "/OperationError.aspx");
                }
                catch
                {
                }
            }
            finally
            {
                if (errLog != null)
                {
                    Funs.DB.Sys_ErrLogInfo.InsertOnSubmit(newErr);
                    Funs.DB.SubmitChanges();
                }

                ErrLogInfo.WriteLog(newErr.ErrLogId, ex, errLog == null ? null : errLog.ToString());
                Server.ClearError();

                PPage.ZXRefresh(Request.ApplicationPath + "/OperationError.aspx");
            }

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
