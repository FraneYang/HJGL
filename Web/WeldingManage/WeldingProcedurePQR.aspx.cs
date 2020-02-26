using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.WeldingManage
{

    public partial class WeldingProcedurePQR : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string WeldingProcedureId
        {
            get
            {
                return (string)ViewState["WeldingProcedureId"];
            }
            set
            {
                ViewState["WeldingProcedureId"] = value;
            }
        }

        /// <summary>
        /// 加载页面
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
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>AddPQR();</script>");
        }

        /// <summary>
        /// GridView行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPQR_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPQR_DataBound(object sender, EventArgs e)
        {
            if (this.gvPQR.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvPQR.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPQR;
        }

        /// <summary>
        /// 返回评定类型
        /// </summary>
        /// <param name="workAreaId"></param>
        /// <returns></returns>
        protected string ConvertType(object WType)
        {
            if (WType != null)
            {
                if (WType.ToString() == "1")
                {
                    return "对接焊缝工艺评定";
                }
                else if (WType.ToString() == "2")
                {
                    return "换热器工艺评定";
                }
                else if (WType.ToString() == "3")
                {
                    return "符合ASME锅炉压力容器规范（国际性规范）第Ⅸ卷的焊接工艺评定";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// GridView行绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPQR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("ckbProcedureId");
                if (cb != null)
                {
                    cb.Attributes.Add("onclick", "ProcedureCheck(" + cb.ClientID + ")");
                }

            }
            catch
            {
                ;
            }
        }
    }
}