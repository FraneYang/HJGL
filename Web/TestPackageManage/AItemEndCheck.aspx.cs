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

namespace Web.TestPackageManage
{
    public partial class AItemEndCheck : PPage
    {
        #region 定义项
        /// <summary>
        /// 管线
        /// </summary>
        public string ISO_ID
        {
            get
            {
                return (string)ViewState["ISO_ID"];
            }
            set
            {
                ViewState["ISO_ID"] = value;
            }
        }

        /// <summary>
        /// A项尾工id
        /// </summary>
        public string EIC_ID
        {
            get
            {
                return (string)ViewState["EIC_ID"];
            }
            set
            {
                ViewState["EIC_ID"] = value;
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
            this.txtEIC_CheckMan.Enabled = !readOnly;
            this.txtEIC_CheckDate.Enabled = !readOnly;
            this.txtEIC_DealMan.Enabled = !readOnly;
            this.txtEIC_DealDate.Enabled = !readOnly;
            this.txtEIC_Remark.Enabled = !readOnly;
        }
        #endregion

        #region 文本清空
        /// <summary>
        ///  文本清空
        /// </summary>
        private void TextIsEmpty()
        {
            this.txtEIC_CheckMan.Text = string.Empty;
            this.txtEIC_CheckDate.Text = string.Empty;
            this.txtEIC_DealMan.Text = string.Empty;
            this.txtEIC_DealDate.Text = string.Empty;
            this.txtEIC_Remark.Text = string.Empty;
        }
        #endregion

        #region 按钮是否可用
        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        private void ButtonIsEnabled(bool enabled)
        {
            this.btnSave.Enabled = enabled;           
            this.btnCanel.Enabled = enabled; 
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.AItemEndCheckMenuId);
                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);

                this.txtReportDate.Value = string.Format("{0:yyyy-MM}",DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
            }
        }
        #endregion

        #region 增加按钮事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.TextIsReadOnly(false);
                this.ButtonIsEnabled(true);
                this.EIC_ID = null;
                this.TextIsEmpty();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
                if (!String.IsNullOrEmpty(this.ISO_ID))
                {
                    Model.TP_AItemEndCheck newAItemEndCheck = new Model.TP_AItemEndCheck();
                    newAItemEndCheck.ISO_ID = this.ISO_ID;
                    newAItemEndCheck.EIC_CheckMan = this.txtEIC_CheckMan.Text.Trim();
                    if (!String.IsNullOrEmpty(this.txtEIC_CheckDate.Text))
                    {
                        newAItemEndCheck.EIC_CheckDate = DateTime.Parse(this.txtEIC_CheckDate.Text.Trim());
                    }
                    newAItemEndCheck.EIC_DealMan = this.txtEIC_DealMan.Text.Trim();
                    if (!String.IsNullOrEmpty(this.txtEIC_DealDate.Text))
                    {
                        newAItemEndCheck.EIC_DealDate = DateTime.Parse(this.txtEIC_DealDate.Text.Trim());
                    }
                    newAItemEndCheck.EIC_Remark = this.txtEIC_Remark.Text.Trim();
                    if (!string.IsNullOrEmpty(this.EIC_ID))
                    {
                        newAItemEndCheck.EIC_ID = this.EIC_ID;
                        BLL.AItemEndCheckService.UpdateTP_AItemEndCheck(newAItemEndCheck);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改A项尾工检查信息");
                    }
                    else
                    {
                        BLL.AItemEndCheckService.AddTP_AItemEndCheck(newAItemEndCheck);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加A项尾工检查信息");
                    }

                    this.gvAItemEndCheckBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择管线！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }
        #endregion

        #region 当 GridView 内生成事件时激发
        /// <summary>
        /// 当 GridView 内生成事件时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAItemEndCheck_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                if (e.CommandName == "del")
                {
                    if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                    {
                        BLL.AItemEndCheckService.DeleteTP_AItemEndCheckByID(e.CommandArgument.ToString());
                        this.gvAItemEndCheckBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                        return;
                    }
                }
                if (e.CommandName == "RemarkLink")
                {
                    if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
                    {
                        this.TextIsReadOnly(false);
                        this.ButtonIsEnabled(true);

                        var item = BLL.AItemEndCheckService.GetTP_AItemEndCheckByID(e.CommandArgument.ToString());
                        this.EIC_ID = item.EIC_ID;
                        this.txtEIC_CheckMan.Text = item.EIC_CheckMan;
                        if (item.EIC_CheckDate.HasValue)
                        {
                            this.txtEIC_CheckDate.Text = String.Format("{0:yyyy-MM-dd}", item.EIC_CheckDate);
                        }

                        this.txtEIC_DealMan.Text = item.EIC_DealMan;

                        if (item.EIC_DealDate.HasValue)
                        {
                            this.txtEIC_DealDate.Text = String.Format("{0:yyyy-MM-dd}", item.EIC_DealDate);
                        }
                        this.txtEIC_Remark.Text = item.EIC_Remark;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                        return;
                    }
                }
            }
        }
        #endregion
        
        #region 树查询
        /// <summary>
        /// 树查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReportSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtReportDate.Value.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtReportDate.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                this.tvControlItem.Nodes.Clear();
                Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitType("2", this.CurrUser.ProjectId);
                if (unit != null)
                {
                    TreeNode rootNode = new TreeNode();//定义根节点
                    rootNode.Text = unit.UnitName;
                    rootNode.Value = unit.UnitId;
                    rootNode.Expanded = true;

                    this.tvControlItem.Nodes.Add(rootNode);
                    this.GetNodes(rootNode.ChildNodes, rootNode.Value, rootNode, startTime, endTime);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先增加施工单位！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择试压月份！')", true);
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
                               join y in Funs.DB.TP_TestPackage on x.InstallationId equals y.InstallationId    
                               where x.ProjectId == this.CurrUser.ProjectId
                               orderby x.InstallationCode select x).Distinct();

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
                var days = (from x in Funs.DB.TP_TestPackage
                            where x.PTP_TableDate >= startTime && x.PTP_TableDate < endTime
                            && x.InstallationId.ToString() == node.Parent.Value
                            && x.ProjectId == this.CurrUser.ProjectId
                            select x.PTP_TableDate).Distinct();
                foreach (var item in days)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = string.Format("{0:yyyy-MM-dd}", item);
                    newNode.Value =item.ToString();
                    nodes.Add(newNode);
                }
            }
            if (node.Depth == 3)
            {
                var dReports = from x in Funs.DB.TP_TestPackage
                               where x.PTP_TableDate == Convert.ToDateTime(parentId) &&
                               x.InstallationId.ToString() == node.Parent.Parent.Value
                               && x.ProjectId == this.CurrUser.ProjectId
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.PTP_TestPackageNo;
                    newNode.Value = item.PTP_ID;
                    nodes.Add(newNode);
                }
            }

            if (node.Depth == 4)
            {
                var isoInfov = from x in Funs.DB.TP_IsoList
                               join y in Funs.DB.TP_TestPackage on x.PTP_ID equals y.PTP_ID
                               join z in Funs.DB.PW_IsoInfo on x.ISO_ID equals z.ISO_ID
                               where x.PTP_ID == node.Value 
                               && z.ProjectId == this.CurrUser.ProjectId
                               orderby z.ISO_IsoNo
                               select z;
                foreach (var item in isoInfov)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.ISO_IsoNo;
                    newNode.Value = item.ISO_ID;
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
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 5)
            {
                this.tvControlItem.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;               
                this.ISO_ID = this.tvControlItem.SelectedValue;
                this.gvAItemEndCheckBind();
            }
            else
            {
                this.ISO_ID = null;
                this.gvAItemEndCheckBind();
            }
        }
        #endregion

        /// <summary>
        /// 绑定列表
        /// </summary>
        protected void gvAItemEndCheckBind()
        {    
            this.ButtonIsEnabled(false);
            this.EIC_ID = null;
            this.TextIsEmpty();
            this.TextIsReadOnly(true);
            if (!string.IsNullOrEmpty(this.ISO_ID))
            {
                var aItemEndCheck = BLL.AItemEndCheckService.GetTP_AItemEndCheckByISO_ID(ISO_ID);
                this.gvAItemEndCheck.Visible = true;
                this.gvAItemEndCheck.DataSourceID = null;
                this.gvAItemEndCheck.DataSource = aItemEndCheck;
                this.gvAItemEndCheck.DataBind();
            }
            else
            {
                this.gvAItemEndCheck.Visible = true;
                this.gvAItemEndCheck.DataSourceID = null;
                this.gvAItemEndCheck.DataSource = null;
                this.gvAItemEndCheck.DataBind();
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCanel_Click(object sender, ImageClickEventArgs e)
        {
            this.TextIsReadOnly(true);
            this.ButtonIsEnabled(false);
            this.EIC_ID = null;
            this.TextIsEmpty();
        }
    }
}