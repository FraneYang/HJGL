using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.PersonManage
{
    public partial class TeamGroup : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        private string EDU_ID
        {
            get
            {
                return (string)ViewState["EDU_ID"];
            }
            set
            {
                ViewState["EDU_ID"] = value;
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

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.TeamGroupMenuId);

                Funs.PleaseSelect(this.drpUnit);
                Funs.PleaseSelect(this.drpUnitId);
                this.drpUnitId.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));
                this.drpUnit.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));

                this.btnSave.Visible = false;
                this.btncancel.Visible = false;

                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                {
                    drpUnit.Enabled = true;
                    drpUnitId.Enabled = true;
                }
                else
                {
                    drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    drpUnit.SelectedValue = this.CurrUser.UnitId;
                    drpUnit.Enabled = false;
                    drpUnitId.Enabled = false;

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
            e.InputParameters["EDU_Code"] = this.txtCode.Text.Trim();
            e.InputParameters["EDU_Name"] = this.txtName.Text.Trim();
            e.InputParameters["EDU_Unit"] = this.drpUnitId.SelectedValue.ToString();
            e.InputParameters["EDU_Main"] = this.txtPerson.Text.Trim();
            e.InputParameters["project"] = this.CurrUser.ProjectId;
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
                this.divEdit.Visible = true;
                this.divSearch.Visible = false;
                this.btnSave.Visible = true;
                this.btncancel.Visible = true;
                this.OperateState = Const.Add;
                this.txtTeamGroupCode.Text = string.Empty;
                this.txtTeamGroupName.Text = string.Empty;
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                {
                    drpUnit.SelectedValue = "0";
                    drpUnit.Enabled = true;
                }
                else
                {
                    drpUnit.SelectedValue = this.CurrUser.UnitId;
                    drpUnit.Enabled = false;
                }
                this.txtPersonName.Text = string.Empty;
                this.txtTeamGroupCode.Focus();
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
                this.divEdit.Visible = false;
                this.btnSave.Visible = false;
                this.btncancel.Visible = false;
                this.txtCode.Text = string.Empty;
                this.txtName.Text = string.Empty;
                this.txtPerson.Text = string.Empty;

                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                {
                    drpUnitId.SelectedValue = "0";
                    drpUnitId.Enabled = true;
                }
                else
                {
                    drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    drpUnitId.Enabled = false;
                }
               
                this.txtCode.Focus();
                this.gvTeamGroup.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 点击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.HS_Education teamGroup = new Model.HS_Education();
                teamGroup.EDU_Code = this.txtTeamGroupCode.Text.Trim();
                teamGroup.EDU_Name = this.txtTeamGroupName.Text.Trim();
                if (teamGroup.EDU_Unit != "0")
                {
                    teamGroup.EDU_Unit = this.drpUnit.SelectedValue.ToString();
                }
                else
                {
                    teamGroup.EDU_Unit = null;
                }

                teamGroup.ProjectId = this.CurrUser.ProjectId;
                teamGroup.EDU_Main = this.txtPersonName.Text;

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.TeamGroupService.IsExistTeamGroupCode(this.txtTeamGroupCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('班组编号已存在！')", true);
                        return;
                    }
                    BLL.TeamGroupService.AddTeamGroup(teamGroup);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加班组信息");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('添加成功')", true);
                }

                if (OperateState == BLL.Const.Modify)
                {
                    teamGroup.EDU_ID = EDU_ID;
                    teamGroup.ProjectId = this.CurrUser.ProjectId;
                    string EDU_Code = BLL.TeamGroupService.GetTeamGroupByTeamGroupId(EDU_ID).EDU_Code;
                    if (EDU_Code != this.txtTeamGroupCode.Text.Trim())
                    {
                        if (BLL.TeamGroupService.IsExistTeamGroupCode(this.txtTeamGroupCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('班组编号已存在！')", true);
                            return;
                        }
                    }

                    BLL.TeamGroupService.UpdateTeamGroup(teamGroup);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改班组信息！");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('修改成功')", true);
                }
                this.gvTeamGroup.DataBind();
                this.divEdit.Visible = false;
                this.btnSave.Visible = false;
                this.btncancel.Visible = false;
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
            this.txtTeamGroupCode.Text = string.Empty;
            this.txtTeamGroupName.Text = string.Empty;
            if (this.CurrUser.Account != BLL.Const.AdminId)
            {
                drpUnit.SelectedValue = this.CurrUser.UnitId;
                drpUnit.Enabled = false;
            }
            else
            {
                drpUnit.SelectedValue = "0";
                drpUnit.Enabled = true;
            }
            this.txtPersonName.Text = string.Empty;
            this.divEdit.Visible = false;
            this.btnSave.Visible = false;
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
            this.txtCode.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtPerson.Text = string.Empty;
            this.drpUnitId.SelectedValue = "0";
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
            EDU_ID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                this.OperateState = BLL.Const.Modify;
                this.divEdit.Visible = true;
                this.divSearch.Visible = false;
                this.btnSave.Visible = true;
                this.btncancel.Visible = true;
                Model.HS_Education teamGroup = BLL.TeamGroupService.GetTeamGroupByTeamGroupId(EDU_ID);
                this.txtTeamGroupCode.Text = teamGroup.EDU_Code;
                this.txtTeamGroupName.Text = teamGroup.EDU_Name;
                if (!String.IsNullOrEmpty(teamGroup.EDU_Unit))
                {
                    this.drpUnit.SelectedValue = teamGroup.EDU_Unit;
                }

                this.txtPersonName.Text = teamGroup.EDU_Main;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.TeamGroupService.DeleteTeamGroup(EDU_ID);
                        this.gvTeamGroup.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除班组信息！");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }

        /// <summary>
        /// 判断是否可删除

        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PersonManageService.GetPersonByeduId(EDU_ID) > 0)
            {
                content = "人员中已经使用了该班组，不能删除！";
            }
            if (BLL.PW_JointInfoService.GetJointInfoByFloorGroup(EDU_ID) > 0 || BLL.PW_JointInfoService.GetJointInfoByCellGroup(EDU_ID) > 0)
            {
                content = "焊口中已经使用了该班组，不能删除！";
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
    }
}