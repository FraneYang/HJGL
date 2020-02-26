using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class JointInfoSearch : PPage
    {
        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var q2 = (from x in Funs.DB.BS_WeldLocation orderby x.WLO_Code select x).ToList();
                ListItem[] list2 = new ListItem[q2.Count()];
                for (int i = 0; i < q2.Count(); i++)
                {
                    list2[i] = new ListItem(q2[i].WLO_Name ?? "", q2[i].WLO_Code.ToString());
                }
                Funs.PleaseSelect(this.ddlWLOCODE);
                this.ddlWLOCODE.Items.AddRange(list2);

                var q3 = (from x in Funs.DB.BS_JointType orderby x.JOTY_Code select x).ToList();
                ListItem[] list3 = new ListItem[q3.Count()];
                for (int i = 0; i < q3.Count(); i++)
                {
                    list3[i] = new ListItem(q3[i].JOTY_Name ?? "", q3[i].JOTY_ID.ToString());
                }
                Funs.PleaseSelect(ddlJOTYID);
                this.ddlJOTYID.Items.AddRange(list3);

                var q4 = (from x in Funs.DB.BS_WeldMethod orderby x.WME_Code select x).ToList();
                ListItem[] list4 = new ListItem[q4.Count()];
                for (int i = 0; i < q4.Count(); i++)
                {
                    list4[i] = new ListItem(q4[i].WME_Name ?? "", q4[i].WME_ID.ToString());
                }
                Funs.PleaseSelect(ddlWMEID);
                this.ddlWMEID.Items.AddRange(list4);
                Funs.PleaseSelect(this.drpDReportID);
                Funs.PleaseSelect(this.drpPW_PointID);
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string result = string.Empty; ;
            if (!string.IsNullOrEmpty(this.txtJOTNO.Text.Trim()))
            {
                result += this.txtJOTNO.Text.Trim() + "|JOT_JointNo" + ",";
            }
            if (!string.IsNullOrEmpty(this.txtISOID.Text.Trim()))
            {
                result += this.txtISOID.Text.Trim() + "|ISO_ID" + ",";
            }
            if (this.ddlWLOCODE.SelectedValue != "0")
            {
                result += this.ddlWLOCODE.SelectedValue.ToString() + "|WLO_Code" + ",";
            }
            if (!string.IsNullOrEmpty(this.txtJointDesc.Text.Trim()))
            {
                result += this.txtJointDesc.Text.Trim() + "|JOT_JointDesc" + ",";
            }
            if (this.ddlJOTYID.SelectedValue != "0")
            {
                result += this.ddlJOTYID.SelectedValue.ToString() + "|JOTY_ID" + ",";
            }
            if (this.ddlWMEID.SelectedValue != "0")
            {
                result += this.ddlWMEID.SelectedValue.ToString() + "|WME_ID" + ",";
            }
            if (this.drpDReportID.SelectedValue != "0")
            {
                result += this.drpDReportID.SelectedValue.ToString() + "|DReportID" + ",";
            }
            if (this.drpPW_PointID.SelectedValue != "0")
            {
                result += this.drpPW_PointID.SelectedValue.ToString() + "|PW_PointID" + ",";
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(0, result.LastIndexOf(","));
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowWorkStageClose('" + result + "');</script>");
            }
        }
        #endregion
    }
}