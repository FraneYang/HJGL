using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace Web.TrustManage
{
    public partial class TrustManageSet : PPage
    {
        #region 定义项       
        /// <summary>
        /// 点口主键
        /// </summary>
        public string PW_PointID
        {
            get
            {
                return (string)ViewState["PW_PointID"];
            }
            set
            {
                ViewState["PW_PointID"] = value;
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
               
        #region 页面加载时
        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.TrustManageEditMenuId);
                this.PW_PointID = Request.Params["pointID"];
                this.txtCH_TrustCode.Focus();
                Funs.PleaseSelect(drpCH_TrustUnit);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit == null || unit.UnitType == "1" || unit.UnitType == "4")
                {
                    if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                    {
                        this.drpCH_TrustUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameBySupervisorUnitIdList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    }
                    else
                    {
                        this.drpCH_TrustUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    }
                }
                else
                {
                    this.drpCH_TrustUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(drpCH_NDTRate);
                this.drpCH_NDTRate.Items.AddRange(BLL.DetectionService.GetNDTRateNameList());

                Funs.PleaseSelect(drpCH_NDTMethod);
                this.drpCH_NDTMethod.Items.AddRange(BLL.TestingService.GetNDTTypeNameList());

                Funs.PleaseSelect(drpCH_CheckUnit);
                this.drpCH_CheckUnit.Items.AddRange(BLL.UnitService.GetCheckUnitList(this.CurrUser.ProjectId));

                this.txtCH_TrustDate.Value = String.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.txtCH_TableDate.Value = String.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

                var bo_Point = BLL.PointManageService.GetPointByPointID(this.PW_PointID); ////获取点口信息
                if (bo_Point != null)
                {
                    this.drpCH_TrustUnit.SelectedValue = bo_Point.BSU_ID;
                }
            }
        }
        #endregion       

        #region 保存事件
        /// <summary>
       /// 保存
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {   
                Model.HJGLDB db = Funs.DB;
                var bo_Point = BLL.PointManageService.GetPointByPointID(this.PW_PointID); ////获取点口信息
                if (bo_Point == null)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('点口单未保存不能生成委托单！');", true);
                    return;
                }

                var jointInfos = BLL.PW_JointInfoService.GetJointInfosByPointID(this.PW_PointID); ////获取点口中的焊口信息
                foreach(var pitem in jointInfos)
                {
                    var CH_TrustItem = BLL.TrustManageEditService.GetCH_TrustItemByJOT_ID(pitem.JOT_ID);
                    if (CH_TrustItem.Count() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('点口单下的焊口已生成委托单！');", true);
                        return;
                    }
                }

                Model.CH_Trust trust = new Model.CH_Trust();
                trust.ProjectId = this.CurrUser.ProjectId;
                trust.CH_RequestDate = System.DateTime.Now; 
                trust.CH_TrustCode = this.txtCH_TrustCode.Text.Trim();
                if (this.drpCH_NDTRate.SelectedValue != "0")
                {
                    trust.CH_NDTRate = this.drpCH_NDTRate.SelectedValue;
                }
                if (this.drpCH_TrustUnit.SelectedValue != "0")
                {
                    trust.CH_TrustUnit = this.drpCH_TrustUnit.SelectedValue;
                }
                if (this.drpCH_NDTMethod.SelectedValue != "0")
                {
                    trust.CH_NDTMethod = this.drpCH_NDTMethod.SelectedValue;
                }
                if (!String.IsNullOrEmpty(this.txtCH_TrustDate.Value))
                {
                    trust.CH_TrustDate = DateTime.Parse(this.txtCH_TrustDate.Value);
                }
                if (this.drpCH_CheckUnit.SelectedValue != "0")
                {
                    trust.CH_CheckUnit = this.drpCH_CheckUnit.SelectedValue;
                }
                if (!String.IsNullOrEmpty(this.txtCH_TableDate.Value))
                {
                    trust.CH_TableDate = DateTime.Parse(this.txtCH_TableDate.Value);
                }
                trust.CH_Remark = this.txtCH_Remark.Text.Trim();

                trust.CH_TrustType = "1";
                trust.CH_Tabler = this.CurrUser.UserId;
                trust.InstallationId = bo_Point.InstallationId;

                trust.CH_TrustID = SQLHelper.GetNewID(typeof(Model.CH_Trust));              
                BLL.TrustManageEditService.AddCH_Trust(trust);
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加委托单信息");

                foreach (var item in jointInfos)
                {
                    Model.CH_TrustItem newitem = new CH_TrustItem();
                    newitem.CH_TrustID = trust.CH_TrustID;
                    newitem.JOT_ID = item.JOT_ID;                                      
                    BLL.TrustManageEditService.AddCH_TrustItem(newitem);

                    var jointInfo = Funs.DB.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == newitem.JOT_ID);
                    jointInfo.JOT_TrustFlag = "01";
                    Funs.DB.SubmitChanges();
                }

                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('生成成功！');OnClientClick=window.close();", true);
            }
            else
            {
               ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请联系管理员！')", true);               
            }
        }
        #endregion
    }
}