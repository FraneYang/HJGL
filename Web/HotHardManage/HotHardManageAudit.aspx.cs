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

namespace Web.HotHardManage
{
    public partial class HotHardManageAudit : PPage
    {
        #region 定义项
        /// <summary>
        /// 委托主键
        /// </summary>
        public string HotHardID
        {
            get
            {
                return (string)ViewState["HotHardID"];
            }
            set
            {
                ViewState["HotHardID"] = value;
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

        #region 文本框是否可编辑
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtHotHardCode.Enabled = !readOnly;
            this.drpHotHardUnit.Enabled = !readOnly;
            this.txtHotHardDate.Enabled = !readOnly;
            this.drpInstallationId.Enabled = !readOnly;
            this.drpCheckUnit.Enabled = !readOnly;
            this.txtNDTMethod.Enabled = !readOnly;
            this.ckDetectionTime0.Enabled = !readOnly;
            this.ckDetectionTime1.Enabled = !readOnly;
            this.txtNDTRate.Enabled = !readOnly;
            this.txtSendee.Enabled = !readOnly;
            this.txtStandards.Enabled = !readOnly;
            this.txtInspectionNum.Enabled = !readOnly;
            this.txtCheckNum.Enabled = !readOnly;
            this.txtTestWeldNum.Enabled = !readOnly;
            this.drpHotHardMan.Enabled = !readOnly;
        }
        #endregion

        #region 文本清空
        /// <summary>
        ///  文本清空
        /// </summary>
        private void TextIsEmpty()
        {
            this.txtHotHardCode.Text = string.Empty;
            this.txtAuditMan.SelectedValue = "0";
            this.drpHotHardUnit.SelectedValue = "0";
            this.txtAuditDate.Value = string.Empty;
            this.drpInstallationId.SelectedValue = "0";
            this.drpCheckUnit.SelectedValue = "0";
           
            this.gvHotHardItem.Visible = false;
            this.txtHotHardDate.Text = string.Empty;

            this.txtNDTMethod.Text = string.Empty;
            this.txtNDTRate.Text = string.Empty;
            this.txtSendee.Text = string.Empty;
            this.txtStandards.Text = string.Empty;
            this.txtInspectionNum.Text = string.Empty;
            this.txtCheckNum.Text = string.Empty;
            this.txtTestWeldNum.Text = string.Empty;

            this.ckDetectionTime0.Checked = false;
            this.ckDetectionTime1.Checked = false;
            this.drpHotHardMan.SelectedValue = "0";
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.HotHardManageAuditMenuId);
                this.TextIsReadOnly(true);
                this.txtAuditMan.Focus();
                Funs.PleaseSelect(drpHotHardUnit);
                this.drpHotHardUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
             
                Funs.PleaseSelect(drpCheckUnit);
                this.drpCheckUnit.Items.AddRange(BLL.UnitService.GetCheckUnitList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(txtAuditMan);
                this.txtAuditMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpInstallationId);
                this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpHotHardMan);
                this.drpHotHardMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));    

                this.txtReportDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");

                var HotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                if (HotHard != null && HotHard.AuditDate.HasValue)
                {
                    this.btnCancelAudit.Visible = true;
                    this.btnAudit.Visible = false;
                }
                else
                {
                    this.btnCancelAudit.Visible = false;
                    this.btnAudit.Visible = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 每次执行Select()和SelectCount前都会引发一次该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
        }

        /// <summary>
        /// 视图集合
        /// </summary>
        private List<Model.View_HotHardItem> HotHardItems = new List<Model.View_HotHardItem>();

        #region 树查询
        /// <summary>
        /// 树查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReportSearClick(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtReportDate.Value.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtReportDate.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                this.tvControlItem.Nodes.Clear();

                List<Model.Base_Unit> units = null;
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                {
                    units = BLL.UnitService.GetUnitsByUnitType("2", this.CurrUser.ProjectId);
                }
                else
                {
                    units = BLL.UnitService.GetUnits(this.CurrUser.UnitId);
                }
                if (units != null)
                {
                    foreach (var unit in units)
                    {
                        TreeNode rootNode = new TreeNode();//定义根节点
                        rootNode.Text = unit.UnitName;
                        rootNode.Value = unit.UnitId;
                        rootNode.Expanded = true;

                        this.tvControlItem.Nodes.Add(rootNode);
                        this.GetNodes(rootNode.ChildNodes, rootNode.Value, rootNode, startTime, endTime);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先增加施工单位！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择委托月份！')", true);
                return;
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId, TreeNode node, DateTime startTime, DateTime endTime)
        {
            if (node.Depth == 0)
            {
                ///装置
                var install = (from x in Funs.DB.Base_Installation
                               join y in Funs.DB.HotHard on x.InstallationId equals y.InstallationId
                               where y.HotHardUnit == parentId
                               orderby x.InstallationCode
                               select x).Distinct();

                foreach (var q in install)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = q.InstallationName;
                    newNode.Value = q.InstallationId.ToString();
                    nodes.Add(newNode);
                }
            }
            if (node.Depth == 1)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = this.txtReportDate.Value.Trim();
                newNode.Value = this.txtReportDate.Value.Trim();
                nodes.Add(newNode);
            }
            if (node.Depth == 2)
            {
                var days = (from x in Funs.DB.HotHard
                            where x.HotHardDate >= startTime && x.HotHardDate < endTime
                            && x.InstallationId.ToString() == node.Parent.Value
                            && x.HotHardUnit == node.Parent.Parent.Value
                            select x.HotHardDate).Distinct();
                foreach (var item in days)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = string.Format("{0:yyyy-MM-dd}", item);
                    newNode.Value = item.ToString();
                    nodes.Add(newNode);
                }
            }
            if (node.Depth == 3)
            {
                var dReports = from x in Funs.DB.HotHard
                               where x.HotHardDate == Convert.ToDateTime(parentId) &&
                               x.InstallationId.ToString() == node.Parent.Parent.Value
                               && x.HotHardUnit == node.Parent.Parent.Parent.Value
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.HotHardCode;
                    newNode.Value = item.HotHardID;
                    nodes.Add(newNode);
                }
            }


            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].ChildNodes, nodes[i].Value, nodes[i], startTime, endTime);
            }
        }
        #endregion
        #endregion

        #region 选择树节点
        /// <summary>
        ///  选择树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.TextIsEmpty();
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                this.tvControlItem.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;
                this.TextIsReadOnly(true);
                
                HotHardID = this.tvControlItem.SelectedValue;

                if (!string.IsNullOrEmpty(HotHardID))
                {
                    var HotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                    if (HotHard != null)
                    {
                        this.txtHotHardCode.Text = HotHard.HotHardCode;
                        if (!String.IsNullOrEmpty(HotHard.HotHardUnit))
                        {
                            this.drpHotHardUnit.SelectedValue = HotHard.HotHardUnit;
                        }
                        if (HotHard.InstallationId.HasValue)
                        {
                            this.drpInstallationId.SelectedValue = HotHard.InstallationId.ToString();
                        }
                                              
                        if (!String.IsNullOrEmpty(HotHard.CheckUnit))
                        {
                            this.drpCheckUnit.SelectedValue = HotHard.CheckUnit;
                        }
                        if (!String.IsNullOrEmpty(HotHard.HotHardMan))
                        {
                            this.drpHotHardMan.SelectedValue = HotHard.HotHardMan;
                        }

                        this.txtNDTRate.Text = HotHard.NDTRate;
                        this.txtSendee.Text = HotHard.Sendee;
                        this.txtStandards.Text = HotHard.Standards;
                        this.txtInspectionNum.Text = HotHard.InspectionNum;
                        this.txtCheckNum.Text = HotHard.CheckNum;
                        this.txtTestWeldNum.Text = HotHard.TestWeldNum;
                        this.txtNDTMethod.Text = HotHard.NDTMethod;

                        if (HotHard.DetectionTime == "0")
                        {
                            this.ckDetectionTime0.Checked = true;
                        }
                        if (HotHard.DetectionTime == "1")
                        {
                            this.ckDetectionTime1.Checked = true;
                        }
                       
                        if (HotHard.HotHardDate.HasValue)
                        {
                            this.txtHotHardDate.Text = String.Format("{0:yyyy-MM-dd}", HotHard.HotHardDate);
                        }
                        if (HotHard.AuditDate.HasValue)
                        {
                            this.txtAuditDate.Value = String.Format("{0:yyyy-MM-dd}", HotHard.AuditDate);
                            this.btnCancelAudit.Visible = true;
                            this.btnAudit.Visible = false;
                        }
                        else
                        {
                            this.txtAuditDate.Value = String.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                            this.btnCancelAudit.Visible = false;
                            this.btnAudit.Visible = true;
                        }
                        if (HotHard.AuditMan != "0" && !String.IsNullOrEmpty(HotHard.AuditMan))
                        {
                            this.txtAuditMan.SelectedValue = HotHard.AuditMan;
                        }
                        else
                        {
                            this.txtAuditMan.SelectedValue = this.CurrUser.UserId;
                        }
                        
                        HotHardItems.Clear();
                        HotHardItems = BLL.HotHardManageEditService.GetView_HotHardItemByHotHardID(HotHardID);
                        if (HotHardItems.Count > 0)
                        {
                            this.gvHotHardItem.Visible = true;
                            this.gvHotHardItem.DataSource = HotHardItems;
                            this.gvHotHardItem.DataBind();
                        }
                    }
                }
            }
        }
        #endregion

        #region 打印
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnPrint) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                {
                    string reportId = this.tvControlItem.SelectedNode.Value;
                    //ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>HotHardInfoPrint('" + BLL.Const.HotHardReportId + "','" + reportId + "','');</script>");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的委托单！')", true);
                    return;
                }
            }
        }
        #endregion

        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAuditing) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                var updateHotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                if (updateHotHard != null && !String.IsNullOrEmpty(this.HotHardID))
                {
                    Model.HotHard HotHard = new Model.HotHard();
                    HotHard.HotHardID = this.HotHardID;
                    if (!String.IsNullOrEmpty(this.txtAuditDate.Value) && this.txtAuditMan.SelectedValue != "0")
                    {
                        HotHard.AuditDate = DateTime.Parse(this.txtAuditDate.Value);
                        HotHard.AuditMan = this.txtAuditMan.SelectedValue;
                        BLL.HotHardManageEditService.AuditHotHard(HotHard);

                        var HotHardItems = from x in Funs.DB.HotHardItem where x.HotHardID == this.HotHardID select x;                       
                        this.TextIsReadOnly(true);
                        this.btnCancelAudit.Visible = true;
                        this.btnAudit.Visible = false;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('审核完成！')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请填写审核人和审核日期！')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要审核的单据！')", true);
                }
            }
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelAudit_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnCancelAuditing) || this.CurrUser.Account == BLL.Const.AdminId)
            {               
                var updateHotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                if (updateHotHard != null && !String.IsNullOrEmpty(this.HotHardID))
                {
                    Model.HotHard HotHard = new Model.HotHard();
                    HotHard.HotHardID = this.HotHardID;
                    HotHard.AuditDate = null;
                    HotHard.AuditMan = null;
                    this.TextIsReadOnly(true);
                    this.btnCancelAudit.Visible = false;
                    this.btnAudit.Visible = true;
                    BLL.HotHardManageEditService.AuditHotHard(HotHard);                                        
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('取消审核完成！')", true);
                }
            }
        }
    }
}