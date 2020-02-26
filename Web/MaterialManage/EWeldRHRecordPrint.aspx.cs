using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MaterialManage
{
    public partial class EWeldRHRecordPrint : PPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["startDate"] = Request.Params["startDate"];
            e.InputParameters["endDate"] = Request.Params["endDate"];
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEWeldRHRecordPrint_DataBound(object sender, EventArgs e)
        {
            if (this.gvEWeldRHRecordPrint.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvEWeldRHRecordPrint.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvEWeldRHRecordPrint;
        }
    }
}