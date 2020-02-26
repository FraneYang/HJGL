using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.PersonManage
{
    public partial class PersonSave : PPage
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
            if (!IsPostBack && this.CurrUser != null)
            {
                this.WED_ID = Request.Params["WED_ID"];
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PersonManageMenuId);

                Funs.PleaseSelect(this.drpUnit);
                this.drpUnit.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));
               
                Funs.PleaseSelect(this.drpEducation);
                this.drpEducation.Items.AddRange(BLL.TeamGroupService.GetEducationList(this.CurrUser.ProjectId));
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit == null || unit.UnitType == "1")
                {
                    drpUnit.Enabled = true;
                }
                else
                {
                    drpUnit.SelectedValue = this.CurrUser.UnitId;
                    drpUnit.Enabled = false;
                }

                if (!String.IsNullOrEmpty(WED_ID))
                {
                    this.PersonLoad();
                    this.OperateState = Const.Modify;
                }
                else
                {
                    this.OperateState = Const.Add;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void PersonLoad()
        {
            var person = BLL.PersonManageService.GetWelderByWenId(WED_ID);
            if (person != null)
            {
                if (!String.IsNullOrEmpty(person.WED_Unit))
                {
                    this.drpUnit.SelectedValue = person.WED_Unit;
                }
                if (!String.IsNullOrEmpty(person.EDU_ID))
                {
                    this.drpEducation.SelectedValue = person.EDU_ID;
                }

                this.txtName.Text = person.WED_Name;
                this.txtCode.Text = person.WED_Code;
                this.txtWorkCode.Text = person.WED_WorkCode;
                this.txtClass.Text = person.WED_Class;
                if (!String.IsNullOrEmpty(person.WED_Sex))
                {
                    this.drpSex.SelectedValue = person.WED_Sex;
                }

                this.txtBirthday.Text = string.Format("{0:yyyy-MM-dd}", person.WED_Birthday);
                this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", person.LimitDate);
                if (person.WED_IfOnGuard.HasValue)
                {
                    this.drpIfOnGuard.Checked = person.WED_IfOnGuard.Value;
                }
                this.txtIdentityCard.Text = person.IdentityCard;
                this.txtSE_EquipmentID.Text = person.SE_EquipmentID;
                this.txtRemark.Text = person.WED_Remark;
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
                Model.BS_Welder welder = new Model.BS_Welder();
                if (this.drpUnit.SelectedValue != "0")
                {
                    welder.WED_Unit = this.drpUnit.SelectedValue;
                }
                if (this.drpEducation.SelectedValue != "0")
                {
                    welder.EDU_ID = this.drpEducation.SelectedValue;
                }
                welder.WED_Code = this.txtCode.Text.Trim();
                welder.WED_Name = this.txtName.Text.Trim();
                if (this.drpSex.SelectedValue != "0")
                {
                    welder.WED_Sex = this.drpSex.SelectedValue;
                }

                if (!String.IsNullOrEmpty(this.txtBirthday.Text))
                {
                    welder.WED_Birthday = DateTime.Parse(this.txtBirthday.Text.Trim());
                }

                if (!String.IsNullOrEmpty(this.txtLimitDate.Text))
                {
                    welder.LimitDate = DateTime.Parse(this.txtLimitDate.Text.Trim());
                }
                welder.WED_WorkCode = this.txtWorkCode.Text.Trim();
                welder.WED_Class = this.txtClass.Text.Trim();
               
                welder.WED_IfOnGuard = this.drpIfOnGuard.Checked;
                welder.IdentityCard = this.txtIdentityCard.Text.Trim();
                welder.SE_EquipmentID = this.txtSE_EquipmentID.Text.Trim();
                welder.WED_Remark =this.txtRemark.Text.Trim();
                welder.ProjectId = this.CurrUser.ProjectId;
                
                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.PersonManageService.IsExistWEDName(this.txtName.Text.Trim(),this.CurrUser.ProjectId))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊工姓名已存在！')", true);
                        return;
                    }
                    if (BLL.PersonManageService.IsExistWEDCode(this.txtCode.Text.Trim(), this.CurrUser.ProjectId))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊工代号已存在！')", true);
                        return;
                    }
                    BLL.PersonManageService.AddBSWelder(welder);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加人员信息");

                    this.txtCode.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');window.opener.location=window.opener.location;OnClientClick=window.close();", true);
                }

                if (OperateState == BLL.Const.Modify)
                {
                    welder.WED_ID = WED_ID;
                    var wed = BLL.PersonManageService.GetBSWelderByTeamWEDID(WED_ID);
                    string name = wed.WED_Name;
                    string wedCode = wed.WED_Code;

                    if (name != this.txtName.Text.Trim())
                    {
                        if (BLL.PersonManageService.IsExistWEDName(this.txtName.Text.Trim(), this.CurrUser.ProjectId))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊工姓名已存在！')", true);
                            return;
                        }
                    }

                    if (wedCode != this.txtCode.Text.Trim())
                    {
                        if (BLL.PersonManageService.IsExistWEDCode(this.txtCode.Text.Trim(), this.CurrUser.ProjectId))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊工代号已存在！')", true);
                            return;
                        }
                    }

                    BLL.PersonManageService.UpdateBSWelder(welder);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改人员信息！");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');window.opener.location=window.opener.location;OnClientClick=window.close();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }      
    }
}