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

namespace Web.RepairManage
{
    public partial class RepairManageEdit : PPage
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

        /// <summary>
        /// 焊口列表
        /// </summary>
        public string[] jointInfo
        {
            get
            {
                return (string[])ViewState["jointInfo"];
            }
            set
            {
                ViewState["jointInfo"] = value;
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
            this.txtCH_Press.Text = string.Empty;
            this.drpCH_TrustUnit.SelectedValue = "0";
            this.drpInstallationId.SelectedValue = "0";
            this.drpCH_NDTRate.SelectedValue = "0";
            this.txtCH_WorkNo.Text = string.Empty;
            this.drpCH_TrustMan.SelectedValue = this.CurrUser.UserId;
            this.txtCH_ItemName.Text = string.Empty;
            this.txtCH_NDTCriteria.Text = "NB/T47013-2015";
            this.drpCH_SlopeType.SelectedValue = "0";
            this.drpCH_AcceptGrade.SelectedValue = "0";
            this.txtCH_ServiceTemp.Text = string.Empty;
            this.drpCH_CheckUnit.SelectedValue = "0";
            this.txtCH_Remark.Text = string.Empty;
            this.drpCH_WeldMethod.SelectedValue = "0";
            this.drpCH_NDTMethod.SelectedValue = "0";
            this.gvTrustItem.Visible = false;            
            this.drpCH_Tabler.SelectedValue = this.CurrUser.UserId;
            this.txtCH_RequestDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.txtCH_TrustDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.gvTrustItem.DataSourceID = null;
            this.gvTrustItem.DataSource = trustItems;
            this.gvTrustItem.DataBind();
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.RepairManageEditMenuId);
                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);
                txtReportDate.Visible = true;
                txtSearchCode.Visible = false;

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

                Funs.PleaseSelect(this.drpCH_CheckUnit);
                this.drpCH_CheckUnit.Items.AddRange(BLL.UnitService.GetCheckUnitList(this.CurrUser.ProjectId));
                
                Funs.PleaseSelect(drpCH_TrustUnit);
                Funs.PleaseSelect(this.drpInstallationId);
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1" || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "4")
                {
                    this.drpCH_TrustUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.drpCH_TrustUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(drpCH_SlopeType);
                this.drpCH_SlopeType.Items.AddRange(BLL.GrooveService.GetSlopeTypeNameList());

                Funs.PleaseSelect(drpCH_AcceptGrade);
                this.drpCH_AcceptGrade.Items.AddRange(BLL.TrustManageEditService.GetAcceptGradeList());                           

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
        /// 查询视图集合
        /// </summary>
        private  List<Model.View_CH_TrustItem> trustItems = new List<Model.View_CH_TrustItem>();

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
                this.CH_TrustID = null;
                trustItems.Clear();
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
                if (this.gvTrustItem.Rows.Count > 0)
                {
                    SaveList();
                    {
                        if (trustItems.Count < 0)
                        {                          
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择委托明细项！')", true);
                            return;
                        }
                    }

                    string isoId = null;
                    var isoNum = (from x in trustItems select x.ISO_IsoNo).Distinct();
                    if (BLL.SysSetService.IsAuto(7, this.CurrUser.ProjectId) == true)  //7表示无损委托单对应管线，True表示只对应一条管线
                    {
                        if (isoNum.Count() > 1)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('一个委托单只对应一条管线！')", true);
                            return;
                        }
                        else
                        {
                            isoId = BLL.PW_JointInfoService.GetJointInfoByJotID(trustItems.FirstOrDefault().JOT_ID).ISO_ID;
                        }
                    }

                    Model.CH_Trust trust = new Model.CH_Trust();
                    trust.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpInstallationId.SelectedValue != "0")
                    {
                        trust.InstallationId = int.Parse(this.drpInstallationId.SelectedValue);
                    }

                    trust.CH_TrustCode = this.txtCH_TrustCode.Text.Trim();
                    trust.CH_Press = this.txtCH_Press.Text.Trim();
                    trust.CH_WorkNo = this.txtCH_WorkNo.Text.Trim();
                    trust.CH_ItemName = this.txtCH_ItemName.Text.Trim();
                    trust.CH_NDTCriteria = this.txtCH_NDTCriteria.Text.Trim();
                    if (this.drpCH_SlopeType.SelectedValue != "0")
                    {
                        trust.CH_SlopeType = this.drpCH_SlopeType.SelectedValue;
                    }
                    if (this.drpCH_AcceptGrade.SelectedValue != "0")
                    {
                        trust.CH_AcceptGrade = this.drpCH_AcceptGrade.SelectedValue;
                    }
                    trust.CH_ServiceTemp = this.txtCH_ServiceTemp.Text.Trim();
                    trust.CH_Remark = this.txtCH_Remark.Text.Trim();
                    if (!String.IsNullOrEmpty(this.txtCH_RequestDate.Value))
                    {
                        trust.CH_RequestDate = DateTime.Parse(this.txtCH_RequestDate.Value); 
                    }
                    if (this.drpCH_TrustUnit.SelectedValue != "0")
                    {
                        trust.CH_TrustUnit = this.drpCH_TrustUnit.SelectedValue;
                    }
                    if (this.drpCH_WeldMethod.SelectedValue != "0")
                    {
                        trust.CH_WeldMethod = this.drpCH_WeldMethod.SelectedValue;
                    }

                    if (!String.IsNullOrEmpty(this.txtCH_TrustDate.Value))
                    {
                        trust.CH_TrustDate = DateTime.Parse(this.txtCH_TrustDate.Value.Trim());
                    }
                   
                    if (this.drpCH_NDTRate.SelectedValue != "0")
                    {
                        trust.CH_NDTRate = this.drpCH_NDTRate.SelectedValue;
                    }
                    if (this.drpCH_NDTMethod.SelectedValue != "0")
                    {
                        trust.CH_NDTMethod = this.drpCH_NDTMethod.SelectedValue;
                    }
                    if (this.drpCH_TrustMan.SelectedValue != "0")
                    {
                        trust.CH_TrustMan = this.drpCH_TrustMan.SelectedValue;
                    }
                    if (this.drpCH_Tabler.SelectedValue != "0")
                    {
                        trust.CH_Tabler = this.drpCH_Tabler.SelectedValue;
                    }
                    if (this.drpCH_CheckUnit.SelectedValue != "0")
                    {
                        trust.CH_CheckUnit = this.drpCH_CheckUnit.SelectedValue;
                    }

                    trust.CH_TrustType = "2";
                    trust.ToIso_Id = isoId;

                    var updatetrust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                    if (updatetrust != null && updatetrust.CH_AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此条委托单已审核不能修改！');", true);
                        return;
                    }
                   
                    if (updatetrust != null && !string.IsNullOrEmpty(CH_TrustID))
                    {
                        trust.CH_TrustID = CH_TrustID;
                        BLL.TrustManageEditService.UpdateCH_Trust(trust);
                        BLL.TrustManageEditService.DeleteCH_TrustItemByCH_TrustID(CH_TrustID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改委托单信息");
                        if (jointInfo != null)
                        {
                            foreach (string j in jointInfo)
                            {
                                var trustItem = BLL.TrustManageEditService.GetCH_TrustItemByJOT_IDAndJotId(trust.CH_TrustID,j);
                               if (trustItem != null)
                               {
                                   BLL.TrustManageEditService.UpdateJOT_TrustFlag(j, "2"); 
                               }                                
                            }
                        }
                    }
                    else
                    {
                        trust.CH_TrustID = SQLHelper.GetNewID(typeof(Model.CH_Trust));
                        this.CH_TrustID = trust.CH_TrustID;
                        BLL.TrustManageEditService.AddCH_Trust(trust);                       
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加委托单信息");
                    }

                    foreach (var item in trustItems)
                    {
                        Model.CH_TrustItem newitem = new CH_TrustItem();
                        newitem.CH_TrustID = this.CH_TrustID;
                        newitem.JOT_ID = item.JOT_ID;
                        newitem.CH_Remark = item.CH_Remark;
                        BLL.TrustManageEditService.AddCH_TrustItem(newitem);
                        BLL.TrustManageEditService.UpdateJOT_TrustFlag(newitem.JOT_ID, "1");
                        ////更新焊口 是否扩透 切除口
                        var jotitem = BLL.PW_JointInfoService.GetJointInfoByJotID(item.JOT_ID);
                        jotitem.JOT_JointStatus = item.JOT_JointStatus;
                        BLL.PW_JointInfoService.UpdateJointPoint(jotitem);

                        BLL.CheckManageService.UpdateCheckIsRepair(item.JOT_ID, this.CH_TrustID, true);
                    }

                    trustItems.Clear();
                    this.gvTrustItem.DataSource = null;
                    this.gvTrustItem.DataBind();
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
            trustItems.Clear();
            for (int i = 0; i < this.gvTrustItem.Rows.Count; i++)
            {
                HiddenField hdJOT_ID = (HiddenField)(this.gvTrustItem.Rows[i].FindControl("hdJOT_ID"));               
                TextBox CH_RequestDate = (TextBox)(this.gvTrustItem.Rows[i].FindControl("CH_RequestDate"));
                TextBox CH_Remark = (TextBox)(this.gvTrustItem.Rows[i].FindControl("CH_Remark"));
                CheckBox rblPointType = (CheckBox)(this.gvTrustItem.Rows[i].FindControl("rblPointType"));

                Model.View_CH_TrustItem item = new Model.View_CH_TrustItem();               
                item.JOT_ID = hdJOT_ID.Value;
                var view = BLL.TrustManageEditService.GetView_CH_TrustItemByJotID(item.JOT_ID, this.CurrUser.ProjectId);                
                item.InstallationId = view.InstallationId;
                item.CH_Remark = CH_Remark.Text.Trim();
                item.ISO_IsoNo = view.ISO_IsoNo;
                item.JOT_JointNo = view.JOT_JointNo;
                item.JOT_Dia = view.JOT_Dia;
                item.JOT_Sch = view.JOT_Sch;
                item.WLO_Code = view.WLO_Code;
                item.WME_Name = view.WME_Name;
                if (rblPointType.Visible)
                {
                    if (rblPointType.Checked)
                    {
                        item.JOT_JointStatus = "104";
                    }
                    else
                    {
                        item.JOT_JointStatus = "101";
                    }
                }
                else
                {
                    item.JOT_JointStatus = "102";
                }

                trustItems.Add(item);
            }
        }
        #endregion

        #region 当 GridView 内生成事件时激发
        /// <summary>
        /// 当 GridView 内生成事件时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTrustItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                string JOT_ID = e.CommandArgument.ToString();
                if (e.CommandName == "del")
                {
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                    if (trust != null && trust.CH_AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此委托单已审核不能删除！');", true);
                    }
                    else
                    {
                        SaveList();
                        foreach (Model.View_CH_TrustItem info in trustItems)
                        {
                            if (info.JOT_ID == JOT_ID)
                            {
                                var item = BLL.TrustManageEditService.GetCH_TrustItemByJOT_ID(JOT_ID);
                                if (item.Count() > 0)
                                {
                                    BLL.TrustManageEditService.UpdateJOT_TrustFlag(JOT_ID, "2");
                                }
                                trustItems.Remove(info);
                                break;
                            }
                        }

                        this.gvTrustItem.DataSource = trustItems;
                        this.gvTrustItem.DataBind();
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
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.drpCH_TrustUnit.SelectedValue == "0")
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
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpCH_TrustUnit.SelectedValue + "'," + this.drpInstallationId.SelectedValue + ",'" + this.CH_TrustID + "');</script>");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            trustItems.Clear();
            string selectList = this.hdSelectList.Value;
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();
                if (CH_TrustID == null)
                {
                    foreach (var item in infos)
                    {
                        var info = BLL.TrustManageEditService.GetView_CH_TrustItemByJotID(item, this.CurrUser.ProjectId);
                        if (info != null)
                        {
                            trustItems.Add(info);
                        }
                    }
                }

                else
                {
                    this.SaveList();
                    foreach (var jotid in infos)
                    {
                        var item = BLL.TrustManageEditService.GetCH_TrustItemByJOT_ID(jotid);
                        if (item.Where(y => y.JOT_ID == jotid).Count() == 0)
                        {
                            var info = BLL.TrustManageEditService.GetView_CH_TrustItemByJotID(jotid, this.CurrUser.ProjectId);
                            if (info != null)
                            {
                                trustItems.Add(info);
                            }
                        }
                    }

                }
            }

            if (trustItems.Count > 0)
            {
                this.gvTrustItem.Visible = true;
                this.gvTrustItem.DataSource = trustItems;
                this.gvTrustItem.DataBind();
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
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1" || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "4")
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
                ///装置
                var install = (from x in Funs.DB.Base_Installation
                               join y in Funs.DB.CH_Trust on x.InstallationId equals y.InstallationId
                               where y.CH_TrustType == "2" && y.CH_TrustUnit == parentId
                               orderby x.InstallationCode select x).Distinct();

                foreach (var q in install)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = q.InstallationName;
                    newNode.Value = q.InstallationId.ToString();
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
                    var days = (from x in Funs.DB.CH_Trust
                                where x.CH_TrustType == "2" && x.CH_TrustDate >= startTime && x.CH_TrustDate < endTime
                                && x.InstallationId.ToString() == node.Parent.Value && x.CH_TrustUnit == node.Parent.Parent.Value
                                select x.CH_TrustDate).Distinct();
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
                    var dReports = from x in Funs.DB.CH_Trust
                                   where x.CH_TrustType == "2" && x.CH_TrustDate == Convert.ToDateTime(parentId) &&
                                   x.InstallationId.ToString() == node.Parent.Parent.Value
                                   && x.CH_TrustUnit == node.Parent.Parent.Parent.Value
                                   orderby x.CH_TrustCode
                                   select x;
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
                    var trustInfo = from x in BLL.Funs.DB.CH_Trust
                                    where x.CH_TrustUnit == node.Parent.Value && x.CH_TrustCode == txtSearchCode.Text.Trim()
                                    && x.ProjectId == this.CurrUser.ProjectId && x.CH_TrustType == "2"
                                    select x;
                    if (trustInfo.Count() > 0 && trustInfo.First().CH_TrustDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM}", trustInfo.First().CH_TrustDate);
                        newNode.Value = string.Format("{0:yyyy-MM}", trustInfo.First().CH_TrustDate);
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 2)
                {
                    var trustInfo = from x in BLL.Funs.DB.CH_Trust
                                    where x.CH_TrustUnit == node.Parent.Parent.Value && x.CH_TrustCode == txtSearchCode.Text.Trim()
                                    && x.ProjectId == this.CurrUser.ProjectId && x.CH_TrustType == "2"
                                    select x;
                    if (trustInfo.Count() > 0 && trustInfo.First().CH_TrustDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM-dd}", trustInfo.First().CH_TrustDate);
                        newNode.Value = string.Format("{0:yyyy-MM-dd}", trustInfo.First().CH_TrustDate);
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 3)
                {
                    var trustInfo = from x in BLL.Funs.DB.CH_Trust
                                    where x.CH_TrustUnit == node.Parent.Parent.Parent.Value && x.CH_TrustCode == txtSearchCode.Text.Trim()
                                    && x.ProjectId == this.CurrUser.ProjectId && x.CH_TrustType == "2"
                                    select x;
                    if (trustInfo.Count() > 0 && trustInfo.First().CH_TrustDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Value = trustInfo.First().CH_TrustID;
                        if (trustInfo.First().CH_AuditDate == null)
                        {
                            newNode.Text = "<font color='#EE0000'>" + trustInfo.First().CH_TrustCode + "</font>";
                        }
                        else
                        {
                            newNode.Text = trustInfo.First().CH_TrustCode;
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
                this.TextIsReadOnly(false);                
                this.ButtonIsEnabled(true);
                CH_TrustID = this.tvControlItem.SelectedValue;              
              
                if (!string.IsNullOrEmpty(CH_TrustID))
                {
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                    if (trust != null)
                    {
                        this.txtCH_TrustCode.Text = trust.CH_TrustCode;
                          
                        this.txtCH_Press.Text = trust.CH_Press ;
                        if (trust.InstallationId.HasValue)
                        {
                            this.drpInstallationId.SelectedValue = trust.InstallationId.ToString();
                        }

                        if (!String.IsNullOrEmpty(trust.CH_TrustUnit))
                        {
                            this.drpCH_TrustUnit.SelectedValue = trust.CH_TrustUnit;
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
                            this.drpCH_SlopeType.SelectedValue = trust.CH_SlopeType;
                        }
                        if (!String.IsNullOrEmpty(trust.CH_AcceptGrade))
                        {
                            this.drpCH_AcceptGrade.SelectedValue = trust.CH_AcceptGrade;
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
                        
                        if (trust.CH_TrustDate.HasValue)
                        {
                            this.txtCH_TrustDate.Value = String.Format("{0:yyyy-MM-dd}", trust.CH_TrustDate);
                        }
                        if (trust.CH_RequestDate.HasValue)
                        {
                            this.txtCH_RequestDate.Value = String.Format("{0:yyyy-MM-dd}", trust.CH_RequestDate);
                        }

                        trustItems.Clear();                       
                        trustItems = BLL.TrustManageEditService.GetView_CH_TrustItemByCH_TrustID(CH_TrustID);
                        
                        int i = 0;
                        jointInfo = new string[trustItems.Count];
                        foreach (var item in trustItems)
                        {
                            jointInfo[i] = item.JOT_ID;
                            i++;
                        }

                        if (trustItems.Count > 0)
                        {
                            this.gvTrustItem.Visible = true;
                            this.gvTrustItem.DataSource = trustItems;
                            this.gvTrustItem.DataBind();
                        }
                    }
                }               
            }

            else
            {
                CH_TrustID = null;
                this.gvTrustItem.Visible = false;
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
                if (!string.IsNullOrEmpty(CH_TrustID))
                {
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID);
                    if (trust != null && !trust.CH_AuditDate.HasValue)
                    {
                        BLL.TrustManageEditService.DeleteCH_TrustItemByCH_TrustID(CH_TrustID);
                        BLL.TrustManageEditService.DeleteCH_TrustByCH_TrustID(CH_TrustID);
                        
                        if (jointInfo != null)
                        {
                            foreach (string j in jointInfo)
                            {
                                BLL.TrustManageEditService.UpdateJOT_TrustFlag(j, "2");
                                BLL.CheckManageService.UpdateCheckIsRepair(j, CH_TrustID, false);
                            }
                        }

                        this.TextIsReadOnly(true);
                        this.ButtonIsEnabled(false);
                        trustItems.Clear();
                        this.TextIsEmpty();
                        this.CH_TrustID = null;
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除返修委托");
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
                        var q = BLL.SysSetService.GetSysSet(3, this.CurrUser.ProjectId);
                        if (q.SetValue == "3")
                        {
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TrustInfoPrint('" + BLL.Const.TrustReport3Id + "','" + reportId + "','');</script>");
                        }
                        else  // 4表示为神化委托单
                        {
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TrustInfoPrint('" + BLL.Const.TrustReport4Id + "','" + reportId + "','');</script>");
                        }
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的委托单！')", true);
                    return;
                }
            }
        }

        protected void drpCH_TrustUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpInstallationId.Items.Clear();
            Funs.PleaseSelect(this.drpInstallationId);
            this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.drpCH_TrustUnit.SelectedValue));  
        }

        protected void drpCH_AcceptGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpCH_AcceptGrade.SelectedValue != "0")
            {
                txtCH_NDTCriteria.Text = "NB/T47013-2015" + "(" + this.drpCH_AcceptGrade.SelectedItem.Text + ")";
            }

            else
            {
                txtCH_NDTCriteria.Text = "NB/T47013-2015";
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

        /// <summary>
        /// gv 绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTrustItem_DataBound(object sender, EventArgs e)
        {
            int rowsCount = this.gvTrustItem.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                HiddenField hdJOT_JointStatus = (HiddenField)(this.gvTrustItem.Rows[i].FindControl("hdJOT_JointStatus"));
                CheckBox rblPointType = (CheckBox)(this.gvTrustItem.Rows[i].FindControl("rblPointType"));
                if (hdJOT_JointStatus.Value == "100" || hdJOT_JointStatus.Value == "102")
                {
                    rblPointType.Visible = false;
                }
                else if (hdJOT_JointStatus.Value == "104")
                {
                    rblPointType.Checked = true;
                }
                else
                {
                    rblPointType.Checked = false;
                }
            }
        }
    }
}