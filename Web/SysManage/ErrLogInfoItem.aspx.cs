using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.SysManage
{
    public partial class ErrLogInfoItem : PPage
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string logId
        {
            get
            {
                return (string)ViewState["logId"];
            }
            set
            {
                ViewState["logId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.logId = Request.Params["logId"];
                string Begin = Request.Params["Begin"];
                string End = Request.Params["End"];

                var errlogs = (from x in Funs.DB.View_ErrLogInfoList 
                               where x.ErrMessage != null 
                               orderby x.ErrTime descending 
                               select x).ToList();
               
                if (!string.IsNullOrEmpty(logId))
                {
                    errlogs = errlogs.Where(x=>x.ErrLogId == this.logId).ToList();
                }

                if (!string.IsNullOrEmpty(Begin))
                {
                    errlogs = errlogs.Where(x => x.ErrTime > Convert.ToDateTime(Begin).AddDays(-1)).ToList();
                }

                if (!string.IsNullOrEmpty(End))
                {
                    errlogs = errlogs.Where(x => x.ErrTime < Convert.ToDateTime(End).AddDays(1)).ToList();
                }

                if (errlogs.Count() > 0)
                {
                    string text = "错误日志：";
                    foreach (var errlog in errlogs)
                    {
                        text += "错误信息开始=====> \r\n";
                        text += "错误类型:" + errlog.ErrType + "\r\n";
                        text += "错误信息:" + errlog.ErrMessage + "\r\n";
                        text += "错误堆栈:" + errlog.ErrStackTrace + "\r\n";
                        if (errlog.ErrTime.HasValue)
                        {
                            text += "出错时间:" + errlog.ErrTime.ToString() + "\r\n";
                        }
                        else
                        {
                            text += "出错时间: \r\n";
                        }
                        text += "出错文件:" + errlog.ErrUrl + "\r\n";
                        text += "IP地址:" + errlog.ErrIP + "\r\n";
                        text += "操作项目:" + errlog.ProjectName + "\r\n";
                        text += "操作单位:" + errlog.UnitName + "\r\n";   
                        text += "操作人员:" + errlog.UserName + "\r\n";                       
                    }

                    this.txtErr.Text = text;
                }                
            }
        }     
    }
}