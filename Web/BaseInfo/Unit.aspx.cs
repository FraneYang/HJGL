using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Unit : PPage
    {
        #region 定义项
        /// <summary>
        ///单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }

        /// <summary>
        /// 操作状态:增加、修改、删除
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

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.UnitMenuId);

                Funs.PleaseSelect(this.ddlSearch);
                this.ddlSearch.Items.AddRange(BLL.UnitService.SearchList());
                ListItem[] lis = new ListItem[4];
                lis[0] = new ListItem("EPC", "1");
                lis[1] = new ListItem("施工", "2");
                lis[2] = new ListItem("检测", "3");
                lis[3] = new ListItem("监理", "4");
                this.ddlUnitType.Items.AddRange(lis);

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(true);
            }
        }
        #endregion

        #region 文本框是否可编辑、按钮是否可用
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            if (this.OperateState == BLL.Const.Modify)
            {
                string unitType = this.ddlUnitType.SelectedValue;
                this.ddlUnitType.SelectedValue = unitType;
            }

            Model.Base_Unit u = BLL.UnitService.GetUnit(UnitId);
            this.ddlUnitType.Enabled = !readOnly;
            this.txtUnitCode.Enabled = !readOnly;
            this.txtUnitName.Enabled = !readOnly;
            this.txtTelephone.Enabled = !readOnly;
            this.txtCorporate.Enabled = !readOnly;
            this.txtProjectRange.Enabled = !readOnly;
            this.txtAddress.Enabled = !readOnly;
            this.txtFax.Enabled = !readOnly;
            this.txtSortIndex.Enabled = !readOnly;
        }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        private void ButtonIsEnabled(bool enabled)
        {
            this.btnModify.Enabled = enabled;
            this.btnSave.Enabled = !enabled;
            this.btncancel.Enabled = !enabled;
        }
        #endregion

        #region 绑定参数
        /// <summary>
        /// 绑定参数每次执行Select()和SelectCount前都会引发一次该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["searchItem"] = this.ddlSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text.ToString();
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
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
            this.UnitGridView.PageIndex = 0;
            this.UnitGridView.DataBind();
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView基本操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UnitGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UnitId = e.CommandArgument.ToString();
            if (e.CommandName == "UpdateUnit")
            {
                Model.Base_Unit u = BLL.UnitService.GetUnit(e.CommandArgument.ToString());
                if (this.OperateState != BLL.Const.Add)
                {
                    this.ddlUnitType.Text = u.UnitType;
                }
                if (this.OperateState == BLL.Const.Modify && u.UnitType == "3")
                {
                    this.txtUnitCode.Enabled = false;
                    this.txtUnitName.Enabled = false;
                    this.txtTelephone.Enabled = false;
                    this.txtCorporate.Enabled = false;
                    this.txtProjectRange.Enabled = false;
                    this.txtAddress.Enabled = false;
                    this.txtFax.Enabled = false;
                    this.txtSortIndex.Enabled = false;
                }
                else if (this.OperateState == BLL.Const.Modify && u.UnitType != "3")
                {
                    this.txtUnitCode.Enabled = true;
                    this.txtUnitName.Enabled = true;
                    this.txtTelephone.Enabled = true;
                    this.txtCorporate.Enabled = true;
                    this.txtProjectRange.Enabled = true;
                    this.txtAddress.Enabled = true;
                    this.txtFax.Enabled = true;
                    this.txtSortIndex.Enabled = true;
                }
                this.txtUnitCode.Text = u.UnitCode.Trim();
                this.txtUnitName.Text = u.UnitName;
                this.txtCorporate.Text = u.Corporate;
                this.txtAddress.Text = u.Address;
                this.txtTelephone.Text = u.Telephone;
                this.txtFax.Text = u.Fax;
                this.txtDuration.Text = u.Duration != null ? u.Duration.ToString() : "";
                this.txtProjectRange.Text = u.ProjectRange;
                this.txtSortIndex.Text = u.SortIndex != null ? u.SortIndex.Value.ToString() : "";
                this.txtInTime.Value = string.Format("{0:yyyy-MM-dd}", u.InTime);
                this.txtOutTime.Value = string.Format("{0:yyyy-MM-dd}", u.OutTime);
            }
            if (e.CommandName == "UnitDelete")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.UnitService.DeleteUnitByUnitId(UnitId);
                        this.UnitGridView.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除单位");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
            if (e.CommandName == "particular")
            {
                Response.Redirect("UnitParticular.aspx?unitId=" + UnitId);
            }

            if (e.CommandName == "DepartSet")
            {
                Response.Redirect("DepartList.aspx?unitId=" + UnitId);
            }
            if (e.CommandName == "organization")
            {
                Response.Redirect("OrganizationUnit.aspx?unitId=" + UnitId);
            }
            if (e.CommandName == "unitQuality")
            {
                Model.Base_Unit u = BLL.UnitService.GetUnit(e.CommandArgument.ToString());
                Response.Redirect("../QualityAudit/SubUnitQualityParticular.aspx?fromPage=unit&subUnitCode=" + u.UnitCode);
            }
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";

            if (BLL.WorkAreaService.GetWorkAreaCountByUnitId(UnitId) > 0)
            {
                content = "施工区域已经用了该单位，不能删除！";
            }
            if (BLL.PointManageService.GetPointCountByUnitId(UnitId) > 0)
            {
                content = "点口管理已经用了该单位，不能删除！";
            }
            if (BLL.WeldReportService.GetWeldReportByUnitId(UnitId) > 0)
            {
                content = "焊接日报已经用了该单位，不能删除！";
            }
            if (BLL.PersonManageService.GetPersonByUnitId(UnitId) > 0)
            {
                content = "人员管理已经用了该单位，不能删除！";
            }
            if (BLL.CheckManageService.GetCheckByUnitId(UnitId) > 0)
            {
                content = "检测管理已经使用了该单位，不能删除！";
            }
            if (BLL.ShowRepairSearchService.GetRepairByUnitId(UnitId)>0)
            {
                content = "返修委托已经使用了该单位，不能删除！";
            }
            if (BLL.HotProessManageEditService.GetHotProessByUnitId(UnitId)>0)
            {
                content = "热处理已经使用了该单位，不能删除！";
            }
            if (BLL.TeamGroupService.GetTeamGroupByUnitId(UnitId)>0)
            {
                content = "班组管理已经使用了该单位，不能删除！";
            }
            if (BLL.PW_IsoInfoService.GetIsoInfoByUnitId(UnitId)>0)
            {
                content = "管线中已经使用了该单位，不能删除！";
            }
            if (BLL.UserService.GetUserCountByUnitId(UnitId)>0)
            {
                content = "用户信息已经使用了该单位，不能删除！";
            }
            if (BLL.TestPackageManageEditService.GetTestPackageByUnitId(UnitId)>0)
            {
                content = "试压包已经使用了该单位，不能删除！";
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
        #endregion

        #region GridView绑定
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UnitGridView_DataBound(object sender, EventArgs e)
        {
            if (this.UnitGridView.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.UnitGridView.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.UnitGridView;
        }
        #endregion

        #region 转换类型
        /// <summary>
        /// 转换单位类型
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        protected string ConvertStr(object unitType)
        {
            if (unitType != null)
            {
                if (unitType.ToString() == "1")
                {
                    return "EPC";
                }
                else if (unitType.ToString() == "2")
                {
                    return "施工";
                }
                else if (unitType.ToString() == "3")
                {
                    return "检测";
                }
                else if (unitType.ToString() == "4")
                {
                    return "监理";
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.OperateState = BLL.Const.Modify;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.txtUnitCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        ///保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                int? duration = null;
                int? sortIndex = null;
                DateTime? inTime = null;
                DateTime? outTime = null;
                if (!string.IsNullOrEmpty(this.txtDuration.Text))
                {
                    duration = Convert.ToInt32(this.txtDuration.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtInTime.Value.Trim()))
                {
                    inTime = Convert.ToDateTime(this.txtInTime.Value.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtOutTime.Value.Trim()))
                {
                    outTime = Convert.ToDateTime(this.txtOutTime.Value.Trim());
                }

                if (!string.IsNullOrEmpty(this.txtSortIndex.Text.Trim()))
                {
                    sortIndex = Convert.ToInt32(this.txtSortIndex.Text.Trim());
                }
                string projectid = this.CurrUser.ProjectId;


                if (OperateState == BLL.Const.Add)
                {
                    BLL.UnitService.AddUnit(this.txtUnitCode.Text, this.txtUnitName.Text, this.ddlUnitType.SelectedValue, projectid, this.txtCorporate.Text, this.txtAddress.Text, this.txtTelephone.Text, this.txtFax.Text, this.txtProjectRange.Text.Trim(), inTime, outTime, duration, sortIndex);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加单位");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('添加单位成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    BLL.UnitService.updateUnit(UnitId, this.txtUnitCode.Text, this.txtUnitName.Text, this.ddlUnitType.SelectedValue, projectid, this.txtCorporate.Text, this.txtAddress.Text, this.txtTelephone.Text, this.txtFax.Text, this.txtProjectRange.Text.Trim(), inTime, outTime, duration, sortIndex);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改单位");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('修改单位成功！')", true);
                }
                this.UnitGridView.DataBind();
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.ButtonIsEnabled(true);
            this.TextIsReadOnly(true);
            this.ddlUnitType.Enabled = false;
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                UnitId = string.Empty;
                this.ddlUnitType.SelectedIndex = 0;
                this.txtUnitCode.Text = string.Empty;
                this.txtUnitName.Text = string.Empty;
                this.txtDuration.Text = string.Empty;
                this.txtTelephone.Text = string.Empty;
                this.txtCorporate.Text = string.Empty;
                this.txtProjectRange.Text = string.Empty;
                this.txtAddress.Text = string.Empty;
                this.txtInTime.Value = string.Empty;
                this.txtOutTime.Value = string.Empty;
                this.txtFax.Text = string.Empty;
                this.txtSortIndex.Text = string.Empty;

                this.OperateState = BLL.Const.Add;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.ddlUnitType.Enabled = true;
                this.ddlUnitType.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion
    }
}