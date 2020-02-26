using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.CheckManage
{
    public partial class CheckManageAudit : PPage
    {
        #region  定义变量
        /// <summary>
        /// 检测主键
        /// </summary>
        public string CHT_CheckID
        {
            get
            {
                return (string)ViewState["CHT_CheckID"];
            }
            set
            {
                ViewState["CHT_CheckID"] = value;
            }
        }

        /// <summary>
        /// 委托单号
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

        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtCheckCode.Enabled = !readOnly;
            this.ddlUnit.Enabled = !readOnly;
            this.ddlInstallationId.Enabled = !readOnly;
            this.txtCheckDate.Disabled = readOnly;
            this.txtCheckType.Enabled = !readOnly;
            this.ddlTabler.Enabled = !readOnly;
            this.txtTableDate.Disabled = readOnly;
            this.ddlAuditMan.Enabled = readOnly;
            this.txtAuditDate.Disabled = !readOnly;
            this.ddlCheckMan.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
        }

        #endregion

        /// <summary>
        /// 检测单细表集合
        /// </summary>
        public static List<Model.View_CH_CheckItem> checkItems = new List<Model.View_CH_CheckItem>();

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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.CheckAuditMenuId);
                this.txtCheckTime.Visible = true;
                txtSearchCode.Visible = false;

                Funs.PleaseSelect(ddlUnit);
                this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(ddlInstallationId);
                this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(ddlTabler);
                this.ddlTabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(ddlAuditMan);
                this.ddlAuditMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(ddlCheckMan);
                this.ddlCheckMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                this.ddlAuditMan.SelectedValue = this.CurrUser.UserId;
                this.txtAuditDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtCheckTime.Value = string.Format("{0:yyyy-MM}", DateTime.Now);

                this.TextIsReadOnly(true);

                Funs.PleaseSelect(this.drpSelectPrint);
                this.drpSelectPrint.Items.AddRange(BLL.ReportPrintService.NDTCheckSelectPrint());

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>CheckSearch();</script>");

                //var check = BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID);
                //if (check != null && check.CHT_AuditDate.HasValue)
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

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAuditing) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(CHT_CheckID))
                {
                    if (BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID) != null)
                    {
                        Model.CH_Check check = new Model.CH_Check();
                        check.CHT_CheckID = CHT_CheckID;
                        var q = from x in BLL.Funs.DB.CH_CheckItem where x.CHT_CheckID == CHT_CheckID select x;
                        int checkNum = q.Count();
                        string trustItemId = q.First().CH_TrustItemID;
                        string trustId = (from x in BLL.Funs.DB.CH_TrustItem where x.CH_TrustItemID == trustItemId select x.CH_TrustID).FirstOrDefault();
                       
                        int trustNum = (from x in BLL.Funs.DB.CH_TrustItem
                                        join y in BLL.Funs.DB.PW_JointInfo on x.JOT_ID equals y.JOT_ID
                                        where x.CH_TrustID == trustId
                                            && y.JOT_JointStatus != "104" //不包括切除口
                                        select x).Count();
                        if (checkNum == trustNum)
                        {
                            if (!string.IsNullOrEmpty(this.txtAuditDate.Value) && this.ddlAuditMan.SelectedValue != "0")
                            {
                                check.CHT_AuditMan = this.ddlAuditMan.SelectedValue;
                                check.CHT_AuditDate = DateTime.Parse(this.txtAuditDate.Value);
                                BLL.CheckManageService.UpdateCheckAudit(check);

                                BLL.CheckItemManageService.UpdateCheckItemAudioTime(CHT_CheckID, "1");

                                var checkItem = from x in Funs.DB.CH_CheckItem where x.CHT_CheckID == this.CHT_CheckID select x;
                                foreach (var item in checkItem)
                                {
                                    BLL.CheckItemManageService.UpdateJointCheckFlag(item.JOT_ID, "1");
                                    if (item.CHT_PassFilm != item.CHT_TotalFilm)
                                    {
                                        BLL.RepairService.UpdateNewJointNo(item.JOT_ID, "R");
                                    }
                                }
                                this.TextIsReadOnly(true);
                                this.btnCancelAudit.Visible = true;
                                this.btnAudit.Visible = false;
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('审核完成！')", true);
                                ////重新绑定gv
                                this.reSetGvDataBind();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请填写审核人和审核日期！')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('对应的委托单还未检测完，不能审核！')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要审核的单据！')", true);
                    }
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
                if (!string.IsNullOrEmpty(CHT_CheckID))
                {
                    if (BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID) != null)
                    {
                        Model.CH_Check check = new Model.CH_Check();
                        check.CHT_CheckID = CHT_CheckID;
                        check.CHT_AuditMan = null;
                        check.CHT_AuditDate = null;
                        this.TextIsReadOnly(true);
                        this.btnCancelAudit.Visible = false;
                        this.btnAudit.Visible = true;
                        BLL.CheckManageService.UpdateCheckAudit(check);

                        BLL.CheckItemManageService.UpdateCheckItemAudioTime(CHT_CheckID, "2");

                        var checks = from x in Funs.DB.CH_CheckItem where x.CHT_CheckID == this.CHT_CheckID select x;
                        foreach (var item in checks)
                        {
                            BLL.CheckItemManageService.UpdateJointCheckFlag(item.JOT_ID, "2");

                            if (item.CHT_PassFilm != item.CHT_TotalFilm)
                            {
                                BLL.RepairService.UpdateCancelAuditJointNo(item.JOT_ID);
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('取消审核完成！')", true);
                        ////重新绑定gv
                        this.reSetGvDataBind();    
                    }
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnPrint) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                string reportId = this.tvControlItem.SelectedValue;

                if (this.drpSelectPrint.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的检测！')", true);
                    return;
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.WeldJointCheckReportId)
                {
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CheckReportPrint('" + BLL.Const.WeldJointCheckReportId + "','" + reportId + "','');</script>");
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的管道对接焊接接头报检/检查记录！')", true);
                        return;
                    }
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.CheckReportId)
                {
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CheckReportPrint('" + BLL.Const.CheckReportId + "','" + reportId + "','');</script>");
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的检测单！')", true);
                        return;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 查询Treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReportSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCheckTime.Value.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtCheckTime.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);

                this.tvControlItem.Nodes.Clear();
                List<Model.Base_Unit> units = null;
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1" || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
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
                        TreeNode rootNode = new TreeNode();
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
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择检测报告月份！')", true);
                return;
            }

            if (!String.IsNullOrEmpty(this.txtSearchCode.Text))
            {
                this.tvControlItem.ExpandAll();
            }
        }

        /// <summary>
        /// 遍历节点
        /// </summary>
        /// <param name="treeNodeCollection"></param>
        /// <param name="p"></param>
        /// <param name="rootNode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void GetNodes(TreeNodeCollection nodes, string parentId, TreeNode node, DateTime startTime, DateTime endTime)
        {
            if (drpSearch.SelectedItem.Text == "按月份")
            {
                if (node.Depth == 0)
                {
                    var install = (from x in Funs.DB.Base_Installation
                                   join y in Funs.DB.CH_Check on x.InstallationId equals y.InstallationId
                                   where y.UnitId == parentId && y.CHT_CheckDate >= startTime && y.CHT_CheckDate < endTime
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
                    newNode.Text = this.txtCheckTime.Value.Trim();
                    newNode.Value = this.txtCheckTime.Value.Trim();
                    nodes.Add(newNode);
                }
                if (node.Depth == 2)
                {
                    var days = (from x in Funs.DB.CH_Check
                                join y in Funs.DB.CH_Trust on x.CH_TrustID equals y.CH_TrustID
                                where y.CH_TrustUnit == node.Parent.Parent.Value
                                && x.InstallationId.ToString() == node.Parent.Value
                                && x.CHT_CheckDate >= startTime && x.CHT_CheckDate < endTime
                                select x.CHT_CheckDate).Distinct();
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
                    List<Model.CH_Check> trustcode = (from x in Funs.DB.CH_Check
                                                      join y in Funs.DB.CH_Trust on x.CH_TrustID equals y.CH_TrustID
                                                      where y.CH_TrustUnit == node.Parent.Parent.Parent.Value
                                                      && x.InstallationId.ToString() == node.Parent.Parent.Value
                                                      && x.CHT_CheckDate == Convert.ToDateTime(parentId)
                                                      orderby x.CHT_CheckCode
                                                      select x).ToList();
                    if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) != null && BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
                    {
                        var trustID = from z in BLL.Funs.DB.CH_Trust where z.CH_CheckUnit == this.CurrUser.UnitId select z.CH_TrustID;
                        trustcode = (from y in trustcode where trustID.Contains(y.CH_TrustID) select y).ToList();
                    }

                    foreach (var item in trustcode)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Value = item.CHT_CheckID;
                        if (item.CHT_AuditDate == null)
                        {
                            newNode.Text = "<font color='#EE0000'>" + item.CHT_CheckCode + "</font>";
                        }
                        else
                        {
                            var it = BLL.CheckItemManageService.GetCheckItemByCheck(item.CHT_CheckID);
                            bool dd = false;
                            foreach (var m in it)
                            {
                                if (m.CHT_CheckResult.Trim() == "不合格")
                                {
                                    dd = true;
                                    break;
                                }
                            }
                            if (dd == true)
                            {
                                newNode.Text = "<B>" + item.CHT_CheckCode + "</B>";
                            }
                            else
                            {
                                newNode.Text = item.CHT_CheckCode;
                            }
                        }
                        nodes.Add(newNode);
                    }
                }
            }

            else
            {
                List<Model.CH_Check> checkInfo = (from x in BLL.Funs.DB.CH_Check
                                                  where x.CHT_CheckCode == txtSearchCode.Text.Trim()
                                                  && x.ProjectId == this.CurrUser.ProjectId
                                                  select x).ToList();

                if (this.CurrUser.UnitId != null && BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
                {
                    var trustID = from z in BLL.Funs.DB.CH_Trust where z.CH_CheckUnit == this.CurrUser.UnitId select z.CH_TrustID;
                    checkInfo = (from y in checkInfo where trustID.Contains(y.CH_TrustID) select y).ToList();
                }

                if (node.Depth == 0)
                {
                    var install = (from x in Funs.DB.Base_Installation
                                   join y in Funs.DB.CH_Check on x.InstallationId equals y.InstallationId
                                   where y.UnitId == parentId 
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
                    var days = (from x in Funs.DB.CH_Check
                                join y in Funs.DB.CH_Trust on x.CH_TrustID equals y.CH_TrustID
                                where y.CH_TrustUnit == node.Parent.Value
                                && x.InstallationId.ToString() == node.Value
                                && x.CHT_CheckCode == txtSearchCode.Text.Trim()
                                select x.CHT_CheckDate).Distinct();

                    if (days.Count() > 0 && days.First().HasValue)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM}", days.First());
                        newNode.Value = string.Format("{0:yyyy-MM}", days.First());
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 2)
                {
                     var days = (from x in Funs.DB.CH_Check
                                join y in Funs.DB.CH_Trust on x.CH_TrustID equals y.CH_TrustID
                                where y.CH_TrustUnit == node.Parent.Parent.Value
                                && x.InstallationId.ToString() == node.Parent.Value
                                && x.CHT_CheckCode == txtSearchCode.Text.Trim()
                                select x.CHT_CheckDate).Distinct();

                     if (days.Count() > 0 && days.First().HasValue)
                     {
                         TreeNode newNode = new TreeNode();
                         newNode.Text = string.Format("{0:yyyy-MM-dd}", days.First());
                         newNode.Value = string.Format("{0:yyyy-MM-dd}", days.First());
                         nodes.Add(newNode);
                     }
                }

                if (node.Depth == 3)
                {
                    var days = (from x in Funs.DB.CH_Check
                                join y in Funs.DB.CH_Trust on x.CH_TrustID equals y.CH_TrustID
                                where y.CH_TrustUnit == node.Parent.Parent.Parent.Value
                                && x.InstallationId.ToString() == node.Parent.Parent.Value
                                && x.CHT_CheckCode == txtSearchCode.Text.Trim()
                                && x.CHT_CheckDate == Convert.ToDateTime(parentId)
                                select x).Distinct();

                    if (days.Count() > 0 && days.First().CHT_CheckDate!=null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Value = days.First().CHT_CheckID;
                        if (days.First().CHT_AuditDate == null)
                        {
                            newNode.Text = "<font color='#EE0000'>" + days.First().CHT_CheckCode + "</font>";
                        }
                        else
                        {
                            newNode.Text = days.First().CHT_CheckCode;
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

        /// <summary>
        /// 点击Treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                this.tvControlItem.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;               
                CHT_CheckID = this.tvControlItem.SelectedValue;
                checkItems = new List<Model.View_CH_CheckItem>();
                
                if (!string.IsNullOrEmpty(CHT_CheckID))
                {
                    //var q = from x in BLL.Funs.DB.CH_CheckItem where x.CHT_CheckID == CHT_CheckID select x;
                    //string trustItemId = q.First().CH_TrustItemID;
                    //CH_TrustID = (from x in BLL.Funs.DB.CH_TrustItem where x.CH_TrustItemID == trustItemId select x.CH_TrustID).FirstOrDefault();
                    //this.lbtnToTrust.Text = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID).CH_TrustCode;

                    var q = BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID);
                    if (q != null)
                    {
                        CH_TrustID = q.CH_TrustID;
                        if (BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID) != null)
                        {
                            this.lbtnToTrust.Text = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID).CH_TrustCode;
                        }
                    }

                    Model.CH_Check check = BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID);
                    this.txtCheckCode.Text = check.CHT_CheckCode;

                    if (!string.IsNullOrEmpty(check.UnitId))
                    {
                        this.ddlUnit.SelectedValue = check.UnitId;
                    }
                    if (!string.IsNullOrEmpty(check.InstallationId.ToString()))
                    {
                        this.ddlInstallationId.SelectedValue = check.InstallationId.ToString();
                    }
                    if (check.CHT_CheckDate != null)
                    {
                        this.txtCheckDate.Value = string.Format("{0:yyyy-MM-dd}", check.CHT_CheckDate);
                    }
                    this.txtCheckType.Text = check.CHT_CheckType;
                    if (!string.IsNullOrEmpty(check.CHT_Tabler))
                    {
                        this.ddlTabler.SelectedValue = check.CHT_Tabler;
                    }
                    if (check.CHT_TableDate != null)
                    {
                        this.txtTableDate.Value = string.Format("{0:yyyy-MM-dd}", check.CHT_TableDate);
                    }
                    if (!string.IsNullOrEmpty(check.CHT_AuditMan))
                    {
                        this.ddlAuditMan.SelectedValue = check.CHT_AuditMan;
                    }
                    else
                    {
                        this.ddlAuditMan.SelectedValue = this.CurrUser.UserId;
                    }
                    if (check.CHT_AuditDate != null)
                    {
                        this.txtAuditDate.Value = string.Format("{0:yyyy-MM-dd}", check.CHT_AuditDate);
                        this.btnCancelAudit.Visible = true;
                        this.btnAudit.Visible = false;
                    }
                    else
                    {
                        this.txtAuditDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        this.btnCancelAudit.Visible = false;
                        this.btnAudit.Visible = true;
                    }
                    if (!string.IsNullOrEmpty(check.CHT_CheckMan))
                    {
                        this.ddlCheckMan.SelectedValue = check.CHT_CheckMan;
                    }
                    this.txtRemark.Text = check.CHT_Remark;

                    ////重新绑定gv
                    this.reSetGvDataBind();
                }
            }
        }

        /// <summary>
        /// 重新绑定gv
        /// </summary>
        protected void reSetGvDataBind()
        {
            checkItems.Clear();
            List<Model.CH_CheckItem> items = BLL.CheckItemManageService.GetTrustItemByCheck(CHT_CheckID);
            foreach (var t in items)
            {
                var checkItem = BLL.CheckItemManageService.GetTrustItemByCheckItem(t.CHT_CheckItemID);
                checkItems.Add(checkItem);
            }

            if (checkItems.Count > 0)
            {
                this.gvCheckItem.Visible = true;
                this.gvCheckItem.DataSource = checkItems;
                this.gvCheckItem.DataBind();
            }
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCheckItem_DataBound(object sender, EventArgs e)
        {
            int rowsCount = this.gvCheckItem.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                TextBox CHT_FilmDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_FilmDate"));
                if (!string.IsNullOrEmpty(CHT_FilmDate.Text))
                {
                    CHT_FilmDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(CHT_FilmDate.Text));
                }
                TextBox CHT_ReportDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_ReportDate"));
                if (!string.IsNullOrEmpty(CHT_ReportDate.Text))
                {
                    CHT_ReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(CHT_ReportDate.Text));
                }
            }
        }

        protected void lbtnToTrust_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.CH_TrustID))
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowToTrust('" + CH_TrustID + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('没有对应的委托单号!')", true);
            }
        }

        protected void drpSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSearch.SelectedItem.Text == "按月份")
            {
                this.txtCheckTime.Visible = true;
                txtSearchCode.Visible = false;
                txtSearchCode.Text = string.Empty;
                this.txtCheckTime.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
            }
            else
            {
                txtCheckTime.Visible = false;
                txtSearchCode.Visible = true;
            }
        }
    }
}