using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingReport
{
    public partial class JointComprehensive :PPage
    {
        /// <summary>
        /// 按钮权限
        /// </summary>
        public string[] ButtonList
        {
            get
            {
                return (string[])ViewState["ButtonList"];
            }
            set
            {
                ViewState["ButtonList"] = value;
            }
        }

        public string Flag
        {
            get
            {
                return (string)ViewState["Flag"];
            }
            set
            {
                ViewState["Flag"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                if (this.CurrUser.ProjectId == null)
                {
                    this.trProject.Visible = true;
                    Funs.PleaseSelect(this.drpProject);
                    this.drpProject.Items.AddRange(BLL.ProjectService.GetProjectList());
                }
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.jointComMenuId);

                Funs.PleaseSelect(ddlWorkArea);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit != null && unit.UnitType == "2")
                {
                    this.ddlWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                else
                {
                    if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                    {
                        this.ddlWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListBySupervisor(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    }
                    else
                    {

                        this.ddlWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaList(this.CurrUser.ProjectId));
                    }
                }

                this.Flag = "0";
            }
        }
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointCompre_DataBound(object sender, EventArgs e)
        {
            if (this.gvJointCompre.BottomPagerRow==null)
            {
                return;    
            }
            ((Web.Controls.GridNavgator)this.gvJointCompre.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvJointCompre;
        }
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["workarea"] = this.ddlWorkArea.SelectedValue;
            e.InputParameters["isono"] = this.txtIsoNo.Text.Trim();
            e.InputParameters["jointDesc"] = this.txtJointDesc.Text.Trim();
            if (this.CurrUser.ProjectId == null)
            {
                e.InputParameters["projectId"] = this.drpProject.SelectedValue;
            }
            else
            {
                e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            }
            e.InputParameters["flag"] = Flag;
            if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
            {
                e.InputParameters["supervisorUnitId"] = this.CurrUser.UnitId;
            }
            else
            {
                e.InputParameters["supervisorUnitId"] = null;
            }
        }
        /// <summary>
        /// 查找按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CurrUser.ProjectId == null)
            {
                if (this.drpProject.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择项目！')", true);
                    return;
                }
            }
            this.Flag = "1";
            this.gvJointCompre.PageIndex = 0;
            this.gvJointCompre.DataBind();
        }


        /// <summary>
        /// 导出焊口综合信息表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            this.gvJointCompre.PageSize = 50000;//BLL.JointComprehensiveService.count
            this.gvJointCompre.PageIndex = 0;
            this.gvJointCompre.DataBind();
            this.gvJointCompre.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            DateTime dt = DateTime.Now;
            string filename = "焊口综合信息表" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvJointCompre.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
           
        }


            
       
        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlWorkArea.Items.Clear();
            Funs.PleaseSelect(this.ddlWorkArea);
            if (this.drpProject.SelectedValue != "0")
            {
                this.ddlWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaList(this.drpProject.SelectedValue));
            }
        }
    }
}