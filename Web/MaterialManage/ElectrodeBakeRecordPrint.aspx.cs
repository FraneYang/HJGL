using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MaterialManage
{
    public partial class ElectrodeBakeRecordPrint : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
            }
        }
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeBake_DataBound(object sender, EventArgs e)
        {
            if (this.gvElectrodeBake.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvElectrodeBake.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvElectrodeBake;
        }
        /// <summary>
        /// GridView参数绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["startDate"] = Request.Params["startDate"];
            e.InputParameters["endDate"] = Request.Params["endDate"];
        }

        /// <summary>
        /// 格式化截取月份
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int ConvertIntMonth(string b)
        {
            if (b != null)
            {
                b = DateTime.Parse(b).Month.ToString();
            }
            return Convert.ToInt32(b);
        }

        /// <summary>
        /// 格式化截取日
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int ConvertIntDay(string b)
        {
            if (b != null)
            {
                b = DateTime.Parse(b).Day.ToString();
            }
            return Convert.ToInt32(b);
        }
    }
}