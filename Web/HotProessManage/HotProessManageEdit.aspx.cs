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

namespace Web.HotProessManage
{
    public partial class HotProessManageEdit : PPage
    {
        #region 定义项
        /// <summary>
        /// 热处理主键
        /// </summary>
        public string HotProessId
        {
            get
            {
                return (string)ViewState["HotProessId"];
            }
            set
            {
                ViewState["HotProessId"] = value;
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
            this.drpUnit.Enabled = !readOnly;
            this.drpInstallationId.Enabled = !readOnly;
            this.txtHotProessNo.Enabled = !readOnly;
            this.txtProessDate.Enabled = !readOnly;
            this.txtProessMethod.Enabled = !readOnly;
            this.txtProessEquipment.Enabled = !readOnly;
            this.drpTabler.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
        }
        #endregion

        #region 文本清空
        /// <summary>
        ///  文本清空
        /// </summary>
        private void TextIsEmpty()
        {
            this.drpUnit.SelectedValue = "0";
            this.drpInstallationId.SelectedValue = "0";
            this.txtHotProessNo.Text = String.Empty;
            this.txtProessDate.Text = String.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            this.txtProessMethod.Text = String.Empty;
            this.txtProessEquipment.Text = String.Empty;
            this.drpTabler.Text = this.CurrUser.UserId;
            this.txtRemark.Text = String.Empty;

            this.gvTestPackage.Visible = false;
            this.gvTestPackage.DataSourceID = null;
            this.gvTestPackage.DataSource = viewHotProessItems;
            this.gvTestPackage.DataBind();
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
            this.imgSearch.Enabled = enabled;
        }
        #endregion

        /// <summary>
        /// 热处理明细集合
        /// </summary>
        private List<Model.View_HotProessItem> viewHotProessItems = new List<Model.View_HotProessItem>();

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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.HotProessManageEditMenuId);
                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);

                Funs.PleaseSelect(this.drpUnit);
                Funs.PleaseSelect(this.drpInstallationId);
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                {
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(drpTabler);
                this.drpTabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpSelectPrint);
                this.drpSelectPrint.Items.AddRange(BLL.ReportPrintService.HotHandleSelectPrint());

                this.txtReportDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
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
                this.HotProessId = null;
                viewHotProessItems.Clear();
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
                if (this.gvTestPackage.Rows.Count > 0)
                {
                    SaveList();
                    {
                        foreach (var item in viewHotProessItems)
                        {
                            if (item.JOT_ID == null)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口不能为空！')", true);
                                return;
                            }
                        }
                    }

                    Model.HotProess hotProess = new Model.HotProess();
                    hotProess.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpInstallationId.SelectedValue != "0")
                    {
                        hotProess.InstallationId = int.Parse(this.drpInstallationId.SelectedValue);
                    }
                    if (this.drpUnit.SelectedValue != "0")
                    {
                        hotProess.UnitId = this.drpUnit.SelectedValue;
                    }

                    hotProess.HotProessNo = this.txtHotProessNo.Text.Trim();
                    if (!String.IsNullOrEmpty(this.txtProessDate.Text))
                    {
                        hotProess.ProessDate = DateTime.Parse(this.txtProessDate.Text.Trim());
                    }
                    if (this.drpTabler.SelectedValue != "0")
                    {
                        hotProess.Tabler = this.drpTabler.SelectedValue;
                    }
                    hotProess.Remark = this.txtRemark.Text.Trim();
                    hotProess.ProessMethod = this.txtProessMethod.Text.Trim();
                    hotProess.ProessEquipment = this.txtProessEquipment.Text.Trim();
                    if (!string.IsNullOrEmpty(HotProessId))
                    {
                        hotProess.HotProessId = HotProessId;
                        BLL.HotProessManageEditService.UpdateHotProess(hotProess);
                        BLL.HotProessManageEditService.DeleteHotProessItemByHotProessId(HotProessId);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改热处理单信息");
                    }
                    else
                    {
                        hotProess.HotProessId = SQLHelper.GetNewID(typeof(Model.TP_TestPackage));
                        this.HotProessId = hotProess.HotProessId;
                        BLL.HotProessManageEditService.AddHotProess(hotProess);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加热处理单信息");
                    }

                    foreach (var item in viewHotProessItems)
                    {
                        Model.HotProessItem newitem = new HotProessItem();
                        newitem.HotProessId = this.HotProessId;
                        newitem.JOT_ID = item.JOT_ID;
                        newitem.PointCount = item.PointCount;
                        newitem.RequiredT = item.RequiredT;
                        newitem.ActualT = item.ActualT;
                        newitem.RequestTime = item.RequestTime;
                        newitem.ActualTime = item.ActualTime;
                        newitem.RecordChartNo = item.RecordChartNo;
                        newitem.HardnessReportNo = item.HardnessReportNo;
                        BLL.HotProessManageEditService.AddHotProessItem(newitem, this.txtHotProessNo.Text, this.txtProessDate.Text);
                    }

                    viewHotProessItems.Clear();
                    this.gvTestPackage.DataSource = null;
                    this.gvTestPackage.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('热处理单信息不能为空！')", true);
                    return;
                }
            }
        }
        #endregion

        #region 得到列表详细
        /// <summary>
        /// 保存列表详细信息
        /// </summary>
        protected void SaveList()
        {
            viewHotProessItems.Clear();
            for (int i = 0; i < this.gvTestPackage.Rows.Count; i++)
            {
                HiddenField hdJOT_ID = (HiddenField)(this.gvTestPackage.Rows[i].FindControl("hdJOT_ID"));

                Label ISO_IsoNo = (Label)(this.gvTestPackage.Rows[i].FindControl("ISO_IsoNo"));
                Label JOT_JointNo = (Label)(this.gvTestPackage.Rows[i].FindControl("JOT_JointNo"));
                Label STE_Name = (Label)(this.gvTestPackage.Rows[i].FindControl("STE_Name"));
                Label ISO_Specification = (Label)(this.gvTestPackage.Rows[i].FindControl("ISO_Specification"));
                Label PointCount = (Label)(this.gvTestPackage.Rows[i].FindControl("PointCount"));
                TextBox RequiredT = (TextBox)(this.gvTestPackage.Rows[i].FindControl("RequiredT"));
                TextBox ActualT = (TextBox)(this.gvTestPackage.Rows[i].FindControl("ActualT"));
                TextBox RequestTime = (TextBox)(this.gvTestPackage.Rows[i].FindControl("RequestTime"));
                TextBox ActualTime = (TextBox)(this.gvTestPackage.Rows[i].FindControl("ActualTime"));
                TextBox RecordChartNo = (TextBox)(this.gvTestPackage.Rows[i].FindControl("RecordChartNo"));
                TextBox HardnessReportNo = (TextBox)(this.gvTestPackage.Rows[i].FindControl("HardnessReportNo"));

                Model.View_HotProessItem item = new Model.View_HotProessItem();
                item.JOT_ID = hdJOT_ID.Value;
                item.ISO_IsoNo = ISO_IsoNo.Text;
                item.JOT_JointNo = JOT_JointNo.Text;
                item.STE_Name = STE_Name.Text;
                item.ISO_Specification = ISO_Specification.Text;
                if (!String.IsNullOrEmpty(PointCount.Text))
                {
                    item.PointCount = int.Parse(PointCount.Text);
                }
                item.RequiredT = RequiredT.Text;
                item.ActualT = ActualT.Text;
                item.RequestTime = RequestTime.Text;
                item.ActualTime = ActualTime.Text;
                item.RecordChartNo = RecordChartNo.Text;
                item.HardnessReportNo = HardnessReportNo.Text;
                viewHotProessItems.Add(item);
            }
        }
        #endregion

        #region 当 GridView 内生成事件时激发
        /// <summary>
        /// 当 GridView 内生成事件时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTestPackage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                string JOT_ID = e.CommandArgument.ToString();
                if (e.CommandName == "del")
                {
                    var viewItem = from x in viewHotProessItems orderby x.PointCount descending select x;
                    foreach (Model.View_HotProessItem info in viewItem)
                    {////不能只根据焊口id删除
                        if (info.JOT_ID == JOT_ID)
                        {
                            viewHotProessItems.Remove(info);
                            break;
                        }
                    }

                    this.gvTestPackage.DataSource = viewHotProessItems;
                    this.gvTestPackage.DataBind();
                }
            }
        }
        #endregion

        #region 热处理查询
        /// <summary>
        /// 热处理查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.drpUnit.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择单位！')", true);
                return;
            }
            else if (this.drpInstallationId.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择装置！')", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpUnit.SelectedValue + "'," + this.drpInstallationId.SelectedValue + ");</script>");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            viewHotProessItems.Clear();
            string selectList = this.hdSelectList.Value;
            int pointCount = int.Parse(this.drpPointCount.SelectedValue);
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();

                foreach (var item in infos)
                {
                    int count = (from x in viewHotProessItems where x.JOT_ID == item select x).Count();
                    int c = pointCount - count;
                    if (c > 0)
                    {
                        for (int i = 0; i < c; i++)
                        {
                            Model.View_HotProessItem info = Funs.DB.View_HotProessItem.FirstOrDefault(x => x.JOT_ID == item);
                            info.PointCount = count + i + 1;
                            viewHotProessItems.Add(info);
                        }
                    }
                }
            }

            if (viewHotProessItems.Count > 0)
            {
                this.gvTestPackage.Visible = true;
                this.gvTestPackage.DataSource = viewHotProessItems;
                this.gvTestPackage.DataBind();
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
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择热处理月份！')", true);
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
                               join y in Funs.DB.HotProess on x.InstallationId equals y.InstallationId
                               where y.UnitId == parentId
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
                var days = (from x in Funs.DB.HotProess
                            where x.ProessDate >= startTime && x.ProessDate < endTime
                            && x.InstallationId.ToString() == node.Parent.Value
                            && x.UnitId == node.Parent.Parent.Value
                            select x.ProessDate).Distinct();
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
                var dReports = from x in Funs.DB.HotProess
                               where x.ProessDate == Convert.ToDateTime(parentId) &&
                               x.InstallationId.ToString() == node.Parent.Parent.Value
                                && x.UnitId == node.Parent.Parent.Parent.Value
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.HotProessNo;
                    newNode.Value = item.HotProessId;
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
                this.TextIsReadOnly(false);
                this.ButtonIsEnabled(true);
                HotProessId = this.tvControlItem.SelectedValue;

                if (!string.IsNullOrEmpty(HotProessId))
                {
                    var hotProess = BLL.HotProessManageEditService.GetHotProessByID(HotProessId);
                    if (hotProess != null)
                    {
                        this.HotProessId = hotProess.HotProessId;
                        if (hotProess.InstallationId.HasValue)
                        {
                            this.drpInstallationId.SelectedValue = hotProess.InstallationId.ToString();
                        }

                        if (!String.IsNullOrEmpty(hotProess.UnitId))
                        {
                            this.drpUnit.SelectedValue = hotProess.UnitId;
                        }
                        this.txtHotProessNo.Text = hotProess.HotProessNo;
                        if (hotProess.ProessDate.HasValue)
                        {
                            this.txtProessDate.Text = String.Format("{0:yyyy-MM-dd}", hotProess.ProessDate);
                        }

                        this.txtProessMethod.Text = hotProess.ProessMethod;
                        this.txtProessEquipment.Text = hotProess.ProessEquipment;

                        if (!String.IsNullOrEmpty(hotProess.Tabler))
                        {
                            this.drpTabler.SelectedValue = hotProess.Tabler;
                        }

                        this.txtRemark.Text = hotProess.Remark;


                        viewHotProessItems.Clear();
                        viewHotProessItems = BLL.HotProessManageEditService.GetHotProessItemByID(HotProessId);
                        if (viewHotProessItems.Count > 0)
                        {
                            this.gvTestPackage.Visible = true;
                            this.gvTestPackage.DataSource = viewHotProessItems;
                            this.gvTestPackage.DataBind();
                        }
                    }
                }
            }
        }
        #endregion

        #region 删除按钮事件
        /// <summary>
        ///  删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(HotProessId))
                {
                    BLL.HotProessManageEditService.DeleteHotProessItemByHotProessId(HotProessId);
                    BLL.HotProessManageEditService.DeleteHotProessByHotProessID(HotProessId);

                    this.TextIsReadOnly(true);
                    this.ButtonIsEnabled(false);
                    viewHotProessItems.Clear();
                    this.TextIsEmpty();
                    this.HotProessId = null;
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除热处理");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！');", true);

                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的热处理记录！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            string varValue = String.Empty;
            string hotId = this.tvControlItem.SelectedValue;
            if (ButtonList.Contains(BLL.Const.BtnPrint) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (this.drpSelectPrint.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的报表！')", true);
                    return;
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.HotHandle1ReportId)
                {
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CommonPrint('" + BLL.Const.HotHandle1ReportId + "','" + hotId + "','" + varValue + "');</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的热处理单！')", true);
                        return;
                    }
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.HotHandle2ReportId)
                {
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CommonPrint('" + BLL.Const.HotHandle2ReportId + "','" + hotId + "','" + varValue + "');</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的热处理单！')", true);
                        return;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallationId.Items.Clear();
            Funs.PleaseSelect(this.drpInstallationId);
            this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.drpUnit.SelectedValue));  
        }

        

    }
}