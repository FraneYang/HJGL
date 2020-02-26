using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class ShowPerson : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // this.gvHazardTemplate.DataBind();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["drpUnitS"] = Request.Params["unitId"];
            e.InputParameters["drpEducationS"] = string.Empty;
            e.InputParameters["txtCodeS"] = this.txtCodeS.Text.Trim();
            e.InputParameters["txtNameS"] = this.txtNameS.Text.Trim();
            e.InputParameters["txtWorkCodeS"] = string.Empty;
            e.InputParameters["txtClassS"] = string.Empty;
            e.InputParameters["project"] = this.CurrUser.ProjectId;
            e.InputParameters["IfOnGuard"] = "1";
        }

              /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvHazardTemplate_DataBound(object sender, EventArgs e)
        {
            if (this.gvHazardTemplate.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvHazardTemplate.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvHazardTemplate;
        }

        protected void imgbtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            int a = 0;
            string result = string.Empty;
            for (int i = 0; i < this.gvHazardTemplate.Rows.Count; i++)
            {
                CheckBox ckbHazardTemplate = (CheckBox)(this.gvHazardTemplate.Rows[i].FindControl("ckbHazardTemplate"));
                Label lblWED_ID = (Label)(this.gvHazardTemplate.Rows[i].FindControl("lblWED_ID"));
                Label lblWED_Code = (Label)(this.gvHazardTemplate.Rows[i].FindControl("lblWED_Code"));
                if (ckbHazardTemplate.Checked)
                {
                    a++;
                    result = lblWED_ID.Text + "," + lblWED_Code.Text;
                    break;
                }
            }
            if (a > 0)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>HazardTemplateClose('" + result + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择人员再保存！')", true);
                return;
            }
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbHazardTemplate_CheckedChanged(object sender, EventArgs e)
        {
            bool result = true;
            CheckBox ckbNewHazardTemplate = sender as CheckBox;
            int rowsCount = this.gvHazardTemplate.Rows.Count;
            int an = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbHazardTemplate = (CheckBox)(this.gvHazardTemplate.Rows[i].FindControl("ckbHazardTemplate"));
                if (ckbNewHazardTemplate.ClientID != ckbHazardTemplate.ClientID)
                {
                    if (ckbHazardTemplate.Checked == true)
                    {
                        result = false;
                    }
                }
                else
                {
                    an = i;
                }
            }
            if (result == false)
            {
                ((CheckBox)(this.gvHazardTemplate.Rows[an].FindControl("ckbHazardTemplate"))).Checked = false;
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('只能选择一条信息！')", true);
            }
        }

        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvHazardTemplate.PageIndex = 0;
            this.gvHazardTemplate.DataBind();
        }
    }
}