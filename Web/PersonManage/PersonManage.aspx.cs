using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.PersonManage
{
    public partial class PersonManage : PPage
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string WED_ID
        {
            get
            {
                return (string)ViewState["WED_ID"];
            }
            set
            {
                ViewState["WED_ID"] = value;
            }
        }

        /// <summary>
        /// 操作状态：增加、修改、删除

        /// </summary>
        public string OperateState
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }

        /// <summary>
        /// 按钮权限列表
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
                if (this.CurrUser != null)
                {
                    string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                    this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PersonManageMenuId);

                    Funs.PleaseSelect(this.drpUnitS);
                    this.drpUnitS.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));
                    Funs.PleaseSelect(this.drpEducationS);
                    this.drpEducationS.Items.AddRange(BLL.TeamGroupService.GetEducationList(this.CurrUser.ProjectId));
                    this.btncancel.Visible = false;

                    var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                    if (unit == null || unit.UnitType == "1")
                    {
                        drpUnitS.Enabled = true;
                    }
                    else
                    {
                        drpUnitS.SelectedValue = this.CurrUser.UnitId;
                        drpUnitS.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (this.drpUnitS.SelectedValue != "0")
            {
                e.InputParameters["drpUnitS"] = this.CurrUser.UnitId;
            }
            else
            {
                e.InputParameters["drpUnitS"] = this.drpUnitS.SelectedValue;
            }
            e.InputParameters["drpEducationS"] = this.drpEducationS.SelectedValue;
            e.InputParameters["txtCodeS"] = this.txtCodeS.Text.ToString();
            e.InputParameters["txtNameS"] = this.txtNameS.Text.Trim();
            e.InputParameters["txtWorkCodeS"] = this.txtWorkCodeS.Text.Trim();
            e.InputParameters["txtClassS"] = this.txtClassS.Text.Trim();
            e.InputParameters["project"] = this.CurrUser.ProjectId;
            e.InputParameters["IfOnGuard"] = string.Empty;
        }

        /// <summary>
        /// 点击增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowPersonSave('');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 点击查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.divSearch.Visible = true; 
                this.btncancel.Visible = false;
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                { 
                    drpUnitS.Enabled = true; 
                }
                else
                {
                    drpUnitS.SelectedValue = this.CurrUser.UnitId;
                    drpUnitS.Enabled = false;
                }
                this.drpEducationS.SelectedValue = "0";
                this.txtCodeS.Text = string.Empty;
                this.txtNameS.Text = string.Empty;
                this.txtWorkCodeS.Text = string.Empty;
                this.txtClassS.Text = string.Empty;
                this.drpUnitS.Focus();

                this.gvTeamGroup.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }       

        /// <summary>
        /// 点击取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {           
            this.btncancel.Visible = false;
        }

        /// <summary>
        /// 查询确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            this.gvTeamGroup.PageIndex = 0;
            this.gvTeamGroup.DataBind();
            this.divSearch.Visible = false;
        }

        /// <summary>
        /// 查询取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnCancal_Click(object sender, ImageClickEventArgs e)
        {
            this.divSearch.Visible = false;
        }

        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTeamGroup_DataBound(object sender, EventArgs e)
        {
            if (this.gvTeamGroup.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvTeamGroup.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvTeamGroup;
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTeamGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "click")
            {
                if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    WED_ID = e.CommandArgument.ToString();
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowPersonSave('" + WED_ID + "');</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }

            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        WED_ID = e.CommandArgument.ToString();
                        BLL.WelderScoreService.DeleteWelderScoreBywed_id(WED_ID);
                        BLL.WeldMethodItemService.DeleteWeldMethodItem(WED_ID);
                        BLL.PersonItemService.DeleteItemByWenId(WED_ID);
                        BLL.PersonManageService.DeleteBSWelder(WED_ID);
                        this.gvTeamGroup.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除人员信息！");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }

            if (e.CommandName == "BS_SteelClick")
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowPersonItem('" + e.CommandArgument.ToString() + "');</script>");
            }
            if (e.CommandName == "BS_WelderScore") //焊工业绩
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>WelderScoreEdit('" + e.CommandArgument.ToString() + "');</script>");
            }
        }      

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PW_JointInfoService.GetJointInfoByCellWelder(WED_ID) > 0 || BLL.PW_JointInfoService.GetJointInfoByFloorWelder(WED_ID) > 0 || BLL.PW_JointInfoService.GetJointInfoByFloorWeld(WED_ID) > 0 || BLL.PW_JointInfoService.GetJointInfoByFloorWeld2(WED_ID) > 0 || BLL.PW_JointInfoService.GetJointInfoByCellWeld(WED_ID) > 0 || BLL.PW_JointInfoService.GetJointInfoByRepairID1(WED_ID) > 0)
            {
                content = "焊口中已使用了该人员，不能删除！";
            }
           
            if (content == "")
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + content + "')", true);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            string projectName = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
            string varValue = String.Empty;
            varValue = projectName.Replace("/", ",");

            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>WelderRecordPrint('" + BLL.Const.WelderRecordReportId + "','" + this.CurrUser.ProjectId + "','" + varValue + "');</script>");
        }
    }
}