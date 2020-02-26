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
    public partial class HotHardManageEdit : PPage
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
            this.txtHotHardDate.Disabled = readOnly;
            this.drpInstallationId.Enabled = !readOnly;
            this.txtNDTRate.Enabled = !readOnly;            
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
            this.drpHotHardUnit.SelectedValue = "0";
            this.drpInstallationId.SelectedValue = "0";
                
            this.drpCheckUnit.SelectedValue = "0";
           
            this.gvHotHardItem.Visible = false;   
            this.txtHotHardDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.ckDetectionTime0.Checked = false;
            this.ckDetectionTime1.Checked = false;

            this.txtNDTMethod.Text = string.Empty;
            this.txtNDTRate.Text = string.Empty;
            this.txtSendee.Text = string.Empty;
            this.txtStandards.Text = string.Empty;
            this.txtInspectionNum.Text = string.Empty;
            this.txtCheckNum.Text = string.Empty;
            this.txtTestWeldNum.Text = string.Empty;
            this.drpHotHardMan.SelectedValue = "0";

            this.gvHotHardItem.DataSource = HotHardItems;
            this.gvHotHardItem.DataBind();
        }
        #endregion
        
        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        private void ButtonIsEnabled(bool enabled)
        {
            this.btnSave.Enabled = enabled;
            this.imgSearch.Enabled = enabled;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        private void Valueload()
        {
            this.txtNDTMethod.Text = "硬度（HB）";
            this.txtNDTRate.Text = "100%";
            this.txtStandards.Text = "SH3501-2011";
            this.drpHotHardMan.SelectedValue = this.CurrUser.UserId;
        }

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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.HotHardManageEditMenuId);
                this.drpCheckUnit.Focus();
                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false); 

                Funs.PleaseSelect(this.drpCheckUnit);
                this.drpCheckUnit.Items.AddRange(BLL.UnitService.GetCheckUnitList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpHotHardUnit);
                Funs.PleaseSelect(this.drpInstallationId);
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                {
                    this.drpHotHardUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.drpHotHardUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(drpHotHardMan);
                this.drpHotHardMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                this.txtReportDate.Value = string.Format("{0:yyyy-MM}",DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
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
                this.HotHardID = null;
                HotHardItems.Clear();
                this.TextIsEmpty();
                this.Valueload();
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
                if (this.gvHotHardItem.Rows.Count > 0)
                {
                    SaveList();
                    {
                        if (HotHardItems.Count < 0)
                        {                            
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择委托明细项！')", true);
                            return;
                        }
                    }

                    Model.HotHard HotHard = new Model.HotHard();
                    HotHard.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpInstallationId.SelectedValue != "0")
                    {
                        HotHard.InstallationId = int.Parse(this.drpInstallationId.SelectedValue);
                    }

                    HotHard.HotHardCode = this.txtHotHardCode.Text.Trim();
                 
                    if (this.drpHotHardUnit.SelectedValue != "0")
                    {
                        HotHard.HotHardUnit = this.drpHotHardUnit.SelectedValue;
                    }
               
                    if (!String.IsNullOrEmpty(this.txtHotHardDate.Value))
                    {
                        HotHard.HotHardDate = DateTime.Parse(this.txtHotHardDate.Value.Trim());
                    }

                    HotHard.NDTRate = this.txtNDTRate.Text;
                    HotHard.Sendee = this.txtSendee.Text;
                    HotHard.Standards = this.txtStandards.Text;
                    HotHard.InspectionNum = this.txtInspectionNum.Text;
                    HotHard.CheckNum = this.txtCheckNum.Text;
                    HotHard.TestWeldNum = this.txtTestWeldNum.Text;
                    
                    HotHard.NDTMethod = this.txtNDTMethod.Text.Trim();

                    if (this.drpHotHardMan.SelectedValue != "0")
                    {
                        HotHard.HotHardMan = this.drpHotHardMan.SelectedValue;
                    }
                    if (this.drpCheckUnit.SelectedValue != "0")
                    {
                        HotHard.CheckUnit = this.drpCheckUnit.SelectedValue;
                    }

                    if (this.ckDetectionTime0.Checked)
                    {
                        HotHard.DetectionTime = "0";
                    }

                    if (this.ckDetectionTime1.Checked)
                    {
                        HotHard.DetectionTime = "1";
                    }                

                    var updateHotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                    if (updateHotHard != null && updateHotHard.AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此条委托单已审核不能修改！');", true);
                        return;
                    }
                   
                    if (updateHotHard != null && !string.IsNullOrEmpty(HotHardID))
                    {
                        HotHard.HotHardID = HotHardID;
                        BLL.HotHardManageEditService.UpdateHotHard(HotHard);
                        BLL.HotHardManageEditService.DeleteHotHardItemByHotHardID(HotHardID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改委托单信息");
                    }
                    else
                    {
                        HotHard.HotHardID = SQLHelper.GetNewID(typeof(Model.HotHard));
                        this.HotHardID = HotHard.HotHardID;
                        BLL.HotHardManageEditService.AddHotHard(HotHard);                       
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加委托单信息");
                    }
                   
                    foreach (var item in HotHardItems)
                    {
                        Model.HotHardItem newitem = new HotHardItem();
                        newitem.HotHardID = this.HotHardID;
                        newitem.JOT_ID = item.JOT_ID;                       
                        newitem.Remark = item.Remark;
                        BLL.HotHardManageEditService.AddHotHardItem(newitem);                        
                    }
                    HotHardItems.Clear();
                    gvHotHardItem.DataSource = null;
                    gvHotHardItem.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                   
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('委托单信息不能为空！')", true);
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
            HotHardItems.Clear();
            for (int i = 0; i < this.gvHotHardItem.Rows.Count; i++)
            {               
                HiddenField hdJOT_ID = (HiddenField)(this.gvHotHardItem.Rows[i].FindControl("hdJOT_ID"));
                
                TextBox Remark = (TextBox)(this.gvHotHardItem.Rows[i].FindControl("Remark"));
                Model.View_HotHardItem item = new Model.View_HotHardItem();               
                item.JOT_ID = hdJOT_ID.Value;
                var view = BLL.HotHardManageEditService.GetView_HotHardItemByJotID(item.JOT_ID, this.CurrUser.ProjectId);                
                item.InstallationId = view.InstallationId;
                item.Remark = Remark.Text.Trim();
                item.ISO_IsoNumber = view.ISO_IsoNumber;
                item.ISO_IsoNo = view.ISO_IsoNo;
                item.JOT_JointNo = view.JOT_JointNo;
                item.CellWelderCode = view.CellWelderCode;
                item.STE_Name = view.STE_Name;
                item.JOT_JointDesc = view.JOT_JointDesc;
                HotHardItems.Add(item);
            }
        }
        #endregion

        #region 当 GridView 内生成事件时激发
        /// <summary>
        /// 当 GridView 内生成事件时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvHotHardItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                string JOT_ID = e.CommandArgument.ToString();
                if (e.CommandName == "del")
                {
                    var HotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                    if (HotHard != null && HotHard.AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此委托单已审核不能删除！');", true);
                    }
                    else
                    {                       
                        foreach (Model.View_HotHardItem info in HotHardItems)
                        {
                            if (info.JOT_ID == JOT_ID)
                            {
                                HotHardItems.Remove(info);
                                break;
                            }
                        }

                        this.gvHotHardItem.DataSource = HotHardItems;
                        this.gvHotHardItem.DataBind();
                    }
                }                
            }
        }
        #endregion
     
        #region 委托查询
        /// <summary>
        /// 委托查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearClick(object sender, ImageClickEventArgs e)
        {
            if (this.drpHotHardUnit.SelectedValue == "0")
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
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpHotHardUnit.SelectedValue + "'," + this.drpInstallationId.SelectedValue + ");</script>");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearClick(object sender, ImageClickEventArgs e)
        {
            HotHardItems.Clear();
            string selectList = this.hdSelectList.Value;
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();
                foreach (var item in infos)
                {
                    var info = BLL.HotHardManageEditService.GetView_HotHardItemByJotID(item, this.CurrUser.ProjectId);
                    if (info != null)
                    {                        
                        info.Remark = null;
                        HotHardItems.Add(info);
                    }                   
                }
            }
            
            if (HotHardItems.Count > 0)
            {
                this.txtCheckNum.Text = HotHardItems.Count.ToString();
                this.txtTestWeldNum.Text = HotHardItems.Count.ToString();

                this.gvHotHardItem.Visible = true;
                this.gvHotHardItem.DataSource = HotHardItems;
                this.gvHotHardItem.DataBind();
            }

            this.txtHotHardCode.Focus();
        }
        #endregion
        
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
                               where y.HotHardUnit==parentId
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
                var days = (from x in Funs.DB.HotHard 
                            where x.HotHardDate >= startTime && x.HotHardDate < endTime
                            && x.InstallationId.ToString() == node.Parent.Value
                            && x.HotHardUnit == node.Parent.Parent.Value
                            select x.HotHardDate).Distinct();
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
                this.TextIsReadOnly(false);
                this.ButtonIsEnabled(true);
                HotHardID = this.tvControlItem.SelectedValue;

                if (!string.IsNullOrEmpty(HotHardID))
                {
                    var HotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                    if (HotHard != null)
                    {
                        this.txtHotHardCode.Text = HotHard.HotHardCode;


                        if (HotHard.InstallationId.HasValue)
                        {
                            this.drpInstallationId.SelectedValue = HotHard.InstallationId.ToString();
                        }

                        if (!String.IsNullOrEmpty(HotHard.HotHardUnit))
                        {
                            this.drpHotHardUnit.SelectedValue = HotHard.HotHardUnit;
                        }

                        this.txtNDTRate.Text =HotHard.NDTRate ;
                        this.txtSendee.Text = HotHard.Sendee;
                        this.txtStandards.Text = HotHard.Standards;
                        this.txtInspectionNum.Text = HotHard.InspectionNum;
                        this.txtCheckNum.Text = HotHard.CheckNum;
                        this.txtTestWeldNum.Text = HotHard.TestWeldNum;

                        if (!String.IsNullOrEmpty(HotHard.CheckUnit))
                        {
                            this.drpCheckUnit.SelectedValue = HotHard.CheckUnit;
                        }

                        this.txtNDTMethod.Text = HotHard.NDTMethod;

                        if (HotHard.HotHardDate.HasValue)
                        {
                            this.txtHotHardDate.Value = String.Format("{0:yyyy-MM-dd}", HotHard.HotHardDate);
                        }

                        if (HotHard.DetectionTime == "0")
                        {
                            this.ckDetectionTime0.Checked = true;
                        }
                        if (HotHard.DetectionTime == "1")
                        {
                            this.ckDetectionTime1.Checked = true;
                        }

                        if (!String.IsNullOrEmpty(HotHard.HotHardMan))
                        {
                            this.drpHotHardMan.SelectedValue = HotHard.HotHardMan;
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
                if (!string.IsNullOrEmpty(HotHardID))
                {
                    var HotHard = BLL.HotHardManageEditService.GetHotHardByID(HotHardID);
                    if (HotHard != null && !HotHard.AuditDate.HasValue)
                    {
                        BLL.HotHardManageEditService.DeleteHotHardItemByHotHardID(HotHardID);
                        BLL.HotHardManageEditService.DeleteHotHardByHotHardID(HotHardID);
                       
                        this.TextIsReadOnly(true);
                        this.ButtonIsEnabled(false);
                        HotHardItems.Clear();
                        this.TextIsEmpty();
                        this.HotHardID = null;
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除无损委托");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！');", true);
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此委托单已审核不能删除！');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的委托记录！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 检测时机
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckDetectionTime0_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckDetectionTime1.Checked && this.ckDetectionTime0.Checked)
            {
                this.ckDetectionTime1.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckDetectionTime1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckDetectionTime0.Checked && this.ckDetectionTime1.Checked)
            {
                this.ckDetectionTime0.Checked = false;
            }
        }
        #endregion
       
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
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>HotHardInfoPrint('" + BLL.Const.HardCheckReportId + "','" + reportId + "','');</script>");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的硬度委托单！')", true);
                    return;
                }
            }
        }

        protected void drpHotHardUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallationId.Items.Clear();
            Funs.PleaseSelect(this.drpInstallationId);
            this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.drpHotHardUnit.SelectedValue));  
        }
    }
}