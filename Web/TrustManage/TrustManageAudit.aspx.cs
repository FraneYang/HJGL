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
    public partial class TrustManageAudit : PPage
    {
        #region 定义项
        /// <summary>
        /// 委托主键
        /// </summary>
        public string CH_TrustID
        {
            get
            {
                return (string)ViewState["CH_TrustID"];
            }
            set
            {
                ViewState["CH_TrustID"] = value;
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
            this.txtCH_TrustCode.Enabled = !readOnly;
            this.txtCH_Press.Enabled = !readOnly;
            this.drpCH_TrustUnit.Enabled = !readOnly;
            this.txtCH_TrustDate.Disabled = readOnly;

            this.drpInstallationId.Enabled = !readOnly;
            this.drpCH_NDTRate.Enabled = !readOnly;
            this.txtCH_WorkNo.Enabled = !readOnly;
            this.drpCH_TrustMan.Enabled = !readOnly;
            this.txtCH_ItemName.Enabled = !readOnly;
            this.txtCH_NDTCriteria.Enabled = !readOnly;
            this.drpCH_Tabler.Enabled = readOnly;
            this.drpCH_SlopeType.Enabled = !readOnly;

            this.drpCH_AcceptGrade.Enabled = !readOnly;
            this.txtCH_ServiceTemp.Enabled = !readOnly;
            this.drpCH_CheckUnit.Enabled = !readOnly;
            this.txtCH_Remark.Enabled = !readOnly;

            this.drpCH_WeldMethod.Enabled = !readOnly;
            this.drpCH_NDTMethod.Enabled = !readOnly;
            this.drpCH_Tabler.Enabled = !readOnly;
            this.txtCH_RequestDate.Disabled = readOnly;
        }
        #endregion

        #region 文本清空
        /// <summary>
        ///  文本清空
        /// </summary>
        private void TextIsEmpty()
        {
            this.txtCH_TrustCode.Text = string.Empty;
            this.txtCH_AuditMan.SelectedValue = "0";
            this.txtCH_Press.Text = string.Empty;
            this.drpCH_TrustUnit.SelectedValue = "0";
            this.txtCH_AuditDate.Value = string.Empty;
            this.drpInstallationId.SelectedValue = "0";
            this.drpCH_NDTRate.SelectedValue = "0";
            this.txtCH_WorkNo.Text = string.Empty;
            this.drpCH_TrustMan.SelectedValue = "0";
            this.txtCH_ItemName.Text = string.Empty;
            this.txtCH_NDTCriteria.Text = string.Empty;
            this.drpCH_SlopeType.SelectedValue = "0";
            this.drpCH_AcceptGrade.SelectedValue = "0";
            this.txtCH_ServiceTemp.Text = string.Empty;
            this.drpCH_CheckUnit.SelectedValue = "0";
            this.txtCH_Remark.Text = string.Empty;
            this.drpCH_WeldMethod.SelectedValue = "0";
            this.drpCH_NDTMethod.SelectedValue = "0";
            this.gvTrustItem.Visible = false;
            this.drpCH_Tabler.SelectedValue = "0";
            this.txtCH_RequestDate.Value = string.Empty;
            this.txtCH_TrustDate.Value = string.Empty;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.TrustManageAuditMenuId);
                this.TextIsReadOnly(true);
                txtReportDate.Visible = true;
                txtSearchCode.Visible = false;

                Funs.PleaseSelect(drpCH_TrustUnit);
                this.drpCH_TrustUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpCH_WeldMethod);
                this.drpCH_WeldMethod.Items.AddRange(BLL.WeldingMethodService.GetWeldMethodNameList());

                Funs.PleaseSelect(drpCH_NDTRate);
                this.drpCH_NDTRate.Items.AddRange(BLL.DetectionService.GetNDTRateNameList());

                Funs.PleaseSelect(drpCH_NDTMethod);
                this.drpCH_NDTMethod.Items.AddRange(BLL.TestingService.GetNDTTypeNameList());

                Funs.PleaseSelect(drpCH_TrustMan);
                this.drpCH_TrustMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpCH_Tabler);
                this.drpCH_Tabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpCH_CheckUnit);
                this.drpCH_CheckUnit.Items.AddRange(BLL.UnitService.GetCheckUnitList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(txtCH_AuditMan);
                this.txtCH_AuditMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpInstallationId);
                this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpCH_SlopeType);
                this.drpCH_SlopeType.Items.AddRange(BLL.GrooveService.GetSlopeTypeNameList());

                Funs.PleaseSelect(drpCH_AcceptGrade);
                this.drpCH_AcceptGrade.Items.AddRange(BLL.TrustManageEditService.GetAcceptGradeList());

                this.txtReportDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");

                //var trust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                //if (trust != null && trust.CH_AuditDate.HasValue)
                //{
                //    this.btnCancelAudit.Visible = true;
                //    this.btnAudit.Visible = false;
                //}
                //else
                //{
                //    this.btnCancelAudit.Visible = false;
                //    this.btnAudit.Visible = true;
                //}
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
        private List<Model.View_CH_TrustItem> trustItems = new List<Model.View_CH_TrustItem>();

        /// <summary>
        /// 委托单集合
        /// </summary>
        private List<Model.CH_Trust> trustLists = new List<Model.CH_Trust>();

        #region 树查询
        /// <summary>
        /// 树查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReportSearch_Click(object sender, ImageClickEventArgs e)
        {
            trustLists.Clear();
            trustLists = (from x in Funs.DB.CH_Trust
                          where x.ProjectId == this.CurrUser.ProjectId && x.CH_TrustType == "1"
                          select x).ToList();

            if (!string.IsNullOrEmpty(this.txtReportDate.Value.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtReportDate.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                trustLists = trustLists.Where(x => x.CH_TrustDate >= startTime && x.CH_TrustDate < endTime).ToList();

                this.tvControlItem.Nodes.Clear();
                List<Model.Base_Unit> units = null;
                var unitInit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unitInit == null || unitInit.UnitType == "1" || unitInit.UnitType == "4")
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

            if (!String.IsNullOrEmpty(this.txtSearchCode.Text))
            {
                this.tvControlItem.ExpandAll();
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
                var installTrust = trustLists.Where(x => x.CH_TrustUnit == parentId).Select(x => x.InstallationId).Distinct();
                foreach (var q in installTrust)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Value = q.ToString();
                    var install = BLL.InstallationService.GetInstallationByInstallationId(q.ToString());
                    if (install != null)
                    {
                        newNode.Text = install.InstallationName;
                    }

                    nodes.Add(newNode);
                }
            }

            if (drpSearch.SelectedItem.Text == "按月份")
            {
                if (node.Depth == 1)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = this.txtReportDate.Value.Trim();
                    newNode.Value = this.txtReportDate.Value.Trim();
                    nodes.Add(newNode);
                }
                if (node.Depth == 2)
                {
                    var days = trustLists.Where(x => x.InstallationId.ToString() == node.Parent.Value && x.CH_TrustUnit == node.Parent.Parent.Value).Select(x => x.CH_TrustDate).Distinct().OrderByDescending(x => x.Value);
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
                    var dReports = trustLists.Where(x => x.CH_TrustDate == Convert.ToDateTime(parentId)
                                && x.InstallationId.ToString() == node.Parent.Parent.Value && x.CH_TrustUnit == node.Parent.Parent.Parent.Value).OrderByDescending(x => x.CH_TrustCode);
                    foreach (var item in dReports)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Value = item.CH_TrustID;
                        if (item.CH_AuditDate == null)
                        {
                            newNode.Text = "<font color='#EE0000'>" + item.CH_TrustCode + "</font>";
                        }
                        else
                        {
                            newNode.Text = item.CH_TrustCode;
                        }
                        nodes.Add(newNode);
                    }
                }

            }
            else
            {
                if (node.Depth == 1)
                {
                    var trustInfo = trustLists.FirstOrDefault(x => x.CH_TrustUnit == node.Parent.Value && x.CH_TrustCode == txtSearchCode.Text.Trim());
                    if (trustInfo != null && trustInfo.CH_TrustDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM}", trustInfo.CH_TrustDate);
                        newNode.Value = string.Format("{0:yyyy-MM}", trustInfo.CH_TrustDate);
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 2)
                {
                    var trustInfo = trustLists.FirstOrDefault(x => x.CH_TrustUnit == node.Parent.Parent.Value && x.CH_TrustCode == txtSearchCode.Text.Trim());
                    if (trustInfo != null && trustInfo.CH_TrustDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM-dd}", trustInfo.CH_TrustDate);
                        newNode.Value = string.Format("{0:yyyy-MM-dd}", trustInfo.CH_TrustDate);
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 3)
                {
                    var trustInfo = trustLists.FirstOrDefault(x => x.CH_TrustUnit == node.Parent.Parent.Parent.Value && x.CH_TrustCode == txtSearchCode.Text.Trim());
                    if (trustInfo != null && trustInfo.CH_TrustDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Value = trustInfo.CH_TrustID;
                        if (trustInfo.CH_AuditDate == null)
                        {
                            newNode.Text = "<font color='#EE0000'>" + trustInfo.CH_TrustCode + "</font>";
                        }
                        else
                        {
                            newNode.Text = trustInfo.CH_TrustCode;
                        }
                        nodes.Add(newNode);
                    }
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
                CH_TrustID = this.tvControlItem.SelectedValue;

                if (!string.IsNullOrEmpty(CH_TrustID))
                {
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                    if (trust != null)
                    {
                        this.txtCH_TrustCode.Text = trust.CH_TrustCode;
                        this.txtCH_Press.Text = trust.CH_Press;

                        if (!String.IsNullOrEmpty(trust.CH_TrustUnit))
                        {
                            this.drpCH_TrustUnit.SelectedValue = trust.CH_TrustUnit;
                        }
                        if (trust.InstallationId.HasValue)
                        {
                            this.drpInstallationId.SelectedValue = trust.InstallationId.ToString();
                        }
                        if (!String.IsNullOrEmpty(trust.CH_NDTRate))
                        {
                            this.drpCH_NDTRate.SelectedValue = trust.CH_NDTRate;
                        }
                        this.txtCH_WorkNo.Text = trust.CH_WorkNo;
                        if (!String.IsNullOrEmpty(trust.CH_TrustMan))
                        {
                            this.drpCH_TrustMan.SelectedValue = trust.CH_TrustMan;
                        }
                        this.txtCH_ItemName.Text = trust.CH_ItemName;
                        this.txtCH_NDTCriteria.Text = trust.CH_NDTCriteria;
                        if (!String.IsNullOrEmpty(trust.CH_SlopeType))
                        {
                            this.drpCH_SlopeType.Text = trust.CH_SlopeType;
                        }
                        if (!String.IsNullOrEmpty(trust.CH_AcceptGrade))
                        {
                            this.drpCH_AcceptGrade.Text = trust.CH_AcceptGrade;
                        }
                        this.txtCH_ServiceTemp.Text = trust.CH_ServiceTemp;
                        if (!String.IsNullOrEmpty(trust.CH_CheckUnit))
                        {
                            this.drpCH_CheckUnit.SelectedValue = trust.CH_CheckUnit;
                        }
                        this.txtCH_Remark.Text = trust.CH_Remark;
                        if (!String.IsNullOrEmpty(trust.CH_WeldMethod))
                        {
                            this.drpCH_WeldMethod.SelectedValue = trust.CH_WeldMethod;
                        }
                        if (!String.IsNullOrEmpty(trust.CH_NDTMethod))
                        {
                            this.drpCH_NDTMethod.SelectedValue = trust.CH_NDTMethod;
                        }
                        if (!String.IsNullOrEmpty(trust.CH_Tabler))
                        {
                            this.drpCH_Tabler.SelectedValue = trust.CH_Tabler;
                        }
                        if (trust.CH_RequestDate.HasValue)
                        {
                            this.txtCH_RequestDate.Value = String.Format("{0:yyyy-MM-dd}", trust.CH_RequestDate);
                        }
                        if (trust.CH_TrustDate.HasValue)
                        {
                            this.txtCH_TrustDate.Value = String.Format("{0:yyyy-MM-dd}", trust.CH_TrustDate);
                        }
                        if (trust.CH_AuditDate.HasValue)
                        {
                            this.txtCH_AuditDate.Value = String.Format("{0:yyyy-MM-dd}", trust.CH_AuditDate);
                            this.btnCancelAudit.Visible = true;
                            this.btnAudit.Visible = false;
                        }
                        else
                        {
                            this.txtCH_AuditDate.Value = String.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                            this.btnCancelAudit.Visible = false;
                            this.btnAudit.Visible = true;
                        }
                        if (trust.CH_AuditMan != "0" && !String.IsNullOrEmpty(trust.CH_AuditMan))
                        {
                            this.txtCH_AuditMan.SelectedValue = trust.CH_AuditMan;
                        }
                        else
                        {
                            this.txtCH_AuditMan.SelectedValue = this.CurrUser.UserId;
                        }
                        trustItems.Clear();
                        trustItems = BLL.TrustManageEditService.GetView_CH_TrustItemByCH_TrustID(CH_TrustID);
                        if (trustItems.Count > 0)
                        {
                            this.gvTrustItem.Visible = true;
                            this.gvTrustItem.DataSource = trustItems;
                            this.gvTrustItem.DataBind();
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
                    if (BLL.SysSetService.IsAuto(3, this.CurrUser.ProjectId) == true)  //3表示无损委托
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TrustInfoPrint('" + BLL.Const.TrustReportId + "','" + reportId + "','');</script>");
                    }
                    if (BLL.SysSetService.IsAuto(3, this.CurrUser.ProjectId) == false)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TrustInfoPrint('" + BLL.Const.TrustReport2Id + "','" + reportId + "','');</script>");
                    }
                    if (BLL.SysSetService.IsAuto(3, this.CurrUser.ProjectId) == null)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TrustInfoPrint('" + BLL.Const.TrustReport3Id + "','" + reportId + "','');</script>");
                    }
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
                var updatetrust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                if (updatetrust != null && !String.IsNullOrEmpty(this.CH_TrustID))
                {
                    Model.CH_Trust trust = new Model.CH_Trust();
                    trust.CH_TrustID = this.CH_TrustID;
                    if (!String.IsNullOrEmpty(this.txtCH_AuditDate.Value) && this.txtCH_AuditMan.SelectedValue != "0")
                    {
                        trust.CH_AuditDate = DateTime.Parse(this.txtCH_AuditDate.Value);
                        trust.CH_AuditMan = this.txtCH_AuditMan.SelectedValue;
                        BLL.TrustManageEditService.AuditCH_Trust(trust);

                        var trustItems = from x in Funs.DB.CH_TrustItem where x.CH_TrustID == this.CH_TrustID select x;
                        foreach (var newitem in trustItems)
                        {
                            BLL.TrustManageEditService.UpdateJOT_TrustFlag(newitem.JOT_ID, "1");
                        }
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
                var trustItem = BLL.TrustManageEditService.GetCH_TrustItemByCH_TrustID(this.CH_TrustID);
                if (trustItem != null)
                {
                    foreach (var item in trustItem)
                    {
                        var nd = from x in Funs.DB.CH_CheckItem where x.CH_TrustItemID == item.CH_TrustItemID select x;
                        if (nd.Count() > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此委托单已在检测不允许取消审核！')", true);
                            return;
                        }
                    }
                }

                var updatetrust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                if (updatetrust != null && !String.IsNullOrEmpty(this.CH_TrustID))
                {
                    Model.CH_Trust trust = new Model.CH_Trust();
                    trust.CH_TrustID = this.CH_TrustID;
                    trust.CH_AuditDate = null;
                    trust.CH_AuditMan = null;
                    this.TextIsReadOnly(true);
                    this.btnCancelAudit.Visible = false;
                    this.btnAudit.Visible = true;
                    BLL.TrustManageEditService.AuditCH_Trust(trust);

                    var trustItems = from x in Funs.DB.CH_TrustItem where x.CH_TrustID == this.CH_TrustID select x;
                    foreach (var newitem in trustItems)
                    {
                        BLL.TrustManageEditService.UpdateJOT_TrustFlag(newitem.JOT_ID, "2");
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('取消审核完成！')", true);
                }
            }
        }

        protected void drpSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSearch.SelectedItem.Text == "按月份")
            {
                txtReportDate.Visible = true;
                txtSearchCode.Visible = false;
                txtSearchCode.Text = string.Empty;
                this.txtReportDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
            }
            else
            {
                txtReportDate.Visible = false;
                txtSearchCode.Visible = true;
            }
        }
    }
}