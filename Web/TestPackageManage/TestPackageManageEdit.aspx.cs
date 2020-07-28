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
    public partial class TestPackageManageEdit : PPage
    {
        #region 定义项
        /// <summary>
        /// 试压包主键
        /// </summary>
        public string PTP_ID
        {
            get
            {
                return (string)ViewState["PTP_ID"];
            }
            set
            {
                ViewState["PTP_ID"] = value;
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
            this.drpBSU_ID.Enabled = !readOnly;   
            this.drpInstallationId.Enabled = !readOnly;
            this.txtPTP_TestPackageNo.Enabled = !readOnly;
            this.txtPTP_TestPackageName.Enabled = !readOnly;
            this.txtPTP_TestHeat.Enabled = !readOnly;
            this.txtPTP_TestService.Enabled = !readOnly;
            this.drpPTP_TestType.Enabled = !readOnly;
            this.drpPTP_Tabler.Enabled = !readOnly;
            this.txtPTP_TableDate.Disabled = readOnly;
            this.drpPTP_Modifier.Enabled = !readOnly;
            this.txtPTP_ModifyDate.Disabled = readOnly;           
            this.txtPTP_Remark.Enabled = !readOnly;
            this.txtPTP_TestPackageCode.Enabled = !readOnly;
            this.txtPTP_TestAmbientTemp.Enabled = !readOnly;
            this.txtPTP_TestMediumTemp.Enabled = !readOnly;
            this.txtPTP_TestPressure.Enabled = !readOnly;
            this.txtPTP_TestPressureTemp.Enabled = !readOnly;
            this.txtPTP_TestPressureTime.Enabled = !readOnly;
            this.txtPTP_TightnessTest.Enabled = !readOnly;
            this.txtPTP_TightnessTestTemp.Enabled = !readOnly;
            this.txtPTP_TightnessTestTime.Enabled = !readOnly;
            this.txtPTP_LeakageTestService.Enabled = !readOnly;
            this.txtPTP_LeakageTestPressure.Enabled = !readOnly;
            this.txtPTP_VacuumTestService.Enabled = !readOnly;
            this.txtPTP_VacuumTestPressure.Enabled = !readOnly;
            this.txtPTP_OperationMedium.Enabled = !readOnly;
            this.txtPTP_PurgingMedium.Enabled = !readOnly;
            this.txtPTP_CleaningMedium.Enabled = !readOnly;
            this.txtPTP_AllowSeepage.Enabled = !readOnly;
            this.txtPTP_FactSeepage.Enabled = !readOnly;
        
        }
        #endregion

        #region 文本清空
        /// <summary>
        ///  文本清空
        /// </summary>
        private void TextIsEmpty()
        {
            this.drpBSU_ID.SelectedValue = "0";
            this.drpInstallationId.SelectedValue = "0";
            this.txtPTP_TestPackageNo.Text = string.Empty;
            this.txtPTP_TestPackageName.Text = string.Empty;
            this.txtPTP_TestHeat.Text = string.Empty;
            this.txtPTP_TestService.Text = string.Empty;
            this.drpPTP_TestType.SelectedValue ="0";
            this.drpPTP_Tabler.SelectedValue = this.CurrUser.UserId;
            this.txtPTP_TableDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.drpPTP_Modifier.SelectedValue = "0";
            this.txtPTP_ModifyDate.Value = string.Empty;         
            this.txtPTP_Remark.Text = string.Empty;
            this.txtPTP_TestPackageCode.Text = string.Empty;
            this.txtPTP_TestAmbientTemp.Text = string.Empty;
            this.txtPTP_TestMediumTemp.Text = string.Empty;
            this.txtPTP_TestPressure.Text = string.Empty;
            this.txtPTP_TestPressureTemp.Text = string.Empty;
            this.txtPTP_TestPressureTime.Text = string.Empty;
            this.txtPTP_TightnessTest.Text = string.Empty;
            this.txtPTP_TightnessTestTemp.Text = string.Empty;
            this.txtPTP_TightnessTestTime.Text = string.Empty;
            this.txtPTP_LeakageTestService.Text = string.Empty;
            this.txtPTP_LeakageTestPressure.Text = string.Empty;
            this.txtPTP_VacuumTestService.Text = string.Empty;
            this.txtPTP_VacuumTestPressure.Text = string.Empty;
            this.txtPTP_OperationMedium.Text = string.Empty;
            this.txtPTP_PurgingMedium.Text = string.Empty;
            this.txtPTP_CleaningMedium.Text = string.Empty;
            this.txtPTP_AllowSeepage.Text = string.Empty;
            this.txtPTP_FactSeepage.Text = string.Empty; 

            this.gvTestPackage.Visible = false;   
            this.gvTestPackage.DataSource = isoInfos;
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
        /// 管线集合
        /// </summary>
        private List<Model.PW_IsoInfo> isoInfos = new List<Model.PW_IsoInfo>();

        #region 页面加载时
        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.TestPackageManageEditMenuId);
                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);
                
                Funs.PleaseSelect(drpPTP_TestType);
                this.drpPTP_TestType.Items.AddRange(BLL.PressureService.GetBS_PackageTestTypeList());

                Funs.PleaseSelect(drpBSU_ID);
                // this.drpBSU_ID.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(drpInstallationId);
                //this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                var getunit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (getunit == null || getunit.UnitType == "1" || getunit.UnitType == "3")
                {
                    this.drpBSU_ID.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.drpBSU_ID.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(this.drpPTP_Modifier);
                this.drpPTP_Modifier.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                //Funs.PleaseSelect(this.drpPTP_Finisher);
                //this.drpPTP_Finisher.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpPTP_Tabler);
                this.drpPTP_Tabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

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
                this.PTP_ID = null;
                isoInfos.Clear();
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
                        foreach (var item in isoInfos)
                        {
                            if (item.ISO_IsoNo == null)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('管线号不能为空！')", true);
                                return;
                            }
                        }
                    }

                    Model.TP_TestPackage testPackage = new Model.TP_TestPackage();
                    testPackage.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpInstallationId.SelectedValue != "0")
                    {
                        testPackage.InstallationId = int.Parse(this.drpInstallationId.SelectedValue);
                    }
                    if (this.drpBSU_ID.SelectedValue != "0")
                    {
                        testPackage.BSU_ID = this.drpBSU_ID.SelectedValue;
                    }
                   // testPackage.PT_ID = this.txtPT_ID;

                    testPackage.PTP_TestPackageNo = this.txtPTP_TestPackageNo.Text.Trim();
                    testPackage.PTP_TestPackageName = this.txtPTP_TestPackageName.Text.Trim();
                    testPackage.PTP_TestHeat = this.txtPTP_TestHeat.Text.Trim();
                    testPackage.PTP_TestService = this.txtPTP_TestService.Text.Trim();
                    if (this.drpPTP_TestType.SelectedValue != "0")
                    {
                        testPackage.PTP_TestType = this.drpPTP_TestType.SelectedValue;
                    }
                    //if (this.drpPTP_Finisher.SelectedValue != "0")
                    //{
                    //    testPackage.PTP_Finisher = this.drpPTP_Finisher.SelectedValue;
                    //}
                    //if (!String.IsNullOrEmpty(this.txtPTP_FinishDate.Value))
                    //{
                    //    testPackage.PTP_FinishDate = DateTime.Parse(this.txtPTP_FinishDate.Value.Trim());
                    //}
                    if (this.drpPTP_Tabler.SelectedValue != "0")
                    {
                        testPackage.PTP_Tabler = this.drpPTP_Tabler.SelectedValue;
                    }
                    if (!String.IsNullOrEmpty(this.txtPTP_TableDate.Value))
                    {
                        testPackage.PTP_TableDate = DateTime.Parse(this.txtPTP_TableDate.Value.Trim());
                    }
                    if (this.drpPTP_Modifier.SelectedValue != "0")
                    { 
                        testPackage.PTP_Modifier = this.drpPTP_Modifier.SelectedValue; 
                    }
                    if (!String.IsNullOrEmpty(this.txtPTP_ModifyDate.Value))
                    {
                        testPackage.PTP_ModifyDate = DateTime.Parse(this.txtPTP_ModifyDate.Value.Trim());
                    }
                    testPackage.PTP_Remark = this.txtPTP_Remark.Text.Trim();
                    testPackage.PTP_TestPackageCode = this.txtPTP_TestPackageCode.Text.Trim();
                    testPackage.PTP_TestAmbientTemp = this.txtPTP_TestAmbientTemp.Text.Trim();
                    testPackage.PTP_TestMediumTemp = this.txtPTP_TestMediumTemp.Text.Trim();
                    testPackage.PTP_TestPressure = this.txtPTP_TestPressure.Text.Trim();
                    testPackage.PTP_TestPressureTemp = this.txtPTP_TestPressureTemp.Text.Trim();
                    testPackage.PTP_TestPressureTime = this.txtPTP_TestPressureTime.Text.Trim();
                    testPackage.PTP_TightnessTest = this.txtPTP_TightnessTest.Text.Trim();
                    testPackage.PTP_TightnessTestTemp = this.txtPTP_TightnessTestTemp.Text.Trim();
                    testPackage.PTP_TightnessTestTime = this.txtPTP_TightnessTestTime.Text.Trim();
                    testPackage.PTP_LeakageTestService = this.txtPTP_LeakageTestService.Text.Trim();
                    testPackage.PTP_LeakageTestPressure = this.txtPTP_LeakageTestPressure.Text.Trim();
                    testPackage.PTP_VacuumTestService = this.txtPTP_VacuumTestService.Text.Trim();
                    testPackage.PTP_VacuumTestPressure = this.txtPTP_VacuumTestPressure.Text.Trim();
                    testPackage.PTP_OperationMedium = this.txtPTP_OperationMedium.Text.Trim();
                    testPackage.PTP_PurgingMedium = this.txtPTP_PurgingMedium.Text.Trim();
                    testPackage.PTP_CleaningMedium = this.txtPTP_CleaningMedium.Text.Trim();
                    testPackage.PTP_AllowSeepage = this.txtPTP_AllowSeepage.Text.Trim();
                    testPackage.PTP_FactSeepage = this.txtPTP_FactSeepage.Text.Trim();
                   
                    var updatetestPackage = BLL.TestPackageManageEditService.GetTP_TestPackageByID(PTP_ID);
                    if (updatetestPackage != null && updatetestPackage.PTP_AduditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此条试压单已审核不能修改！');", true);
                        return;
                    }
                    if (isoInfos.Count() > 0 && isoInfos.FirstOrDefault() != null)
                    {
                        testPackage.WorkAreaId = isoInfos.FirstOrDefault().BAW_ID;
                    }

                    if (updatetestPackage != null && !string.IsNullOrEmpty(PTP_ID))
                    {
                        testPackage.PTP_ID = PTP_ID;
                        BLL.TestPackageManageEditService.UpdateTP_TestPackage(testPackage);
                        BLL.TestPackageManageEditService.DeleteTP_IsoListByPTP_ID(PTP_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改试压单信息");
                    }
                    else
                    {
                        testPackage.PTP_ID = SQLHelper.GetNewID(typeof(Model.TP_TestPackage));
                        this.PTP_ID = testPackage.PTP_ID;
                        BLL.TestPackageManageEditService.AddTP_TestPackage(testPackage);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加试压单信息");
                    }                  

                    foreach (var item in isoInfos)
                    {
                        Model.TP_IsoList newitem = new TP_IsoList();
                        newitem.PTP_ID = this.PTP_ID;
                        newitem.ISO_ID = item.ISO_ID;
                        BLL.TestPackageManageEditService.AddTP_IsoList(newitem);
                    }

                    isoInfos.Clear();
                    this.gvTestPackage.DataSource = null;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('试压单信息不能为空！')", true);
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
            isoInfos.Clear();
            for (int i = 0; i < this.gvTestPackage.Rows.Count; i++)
            {
                HiddenField hdISO_ID = (HiddenField)(this.gvTestPackage.Rows[i].FindControl("hdISO_ID"));
                Model.PW_IsoInfo item = new Model.PW_IsoInfo
                {
                    ISO_ID = hdISO_ID.Value
                };
                var view = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(item.ISO_ID);
                isoInfos.Add(view);   
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
                string ISO_ID = e.CommandArgument.ToString();
                if (e.CommandName == "del")
                {
                    SaveList();
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(PTP_ID);
                    if (trust != null && trust.CH_AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此试压单已审核不能删除！');", true);
                    }
                    else
                    {
                        foreach (Model.PW_IsoInfo info in isoInfos)
                        {
                            if (info.ISO_ID == ISO_ID)
                            {
                                isoInfos.Remove(info);
                                break;
                            }
                        }

                        this.gvTestPackage.DataSource = isoInfos;
                        this.gvTestPackage.DataBind();
                    }
                }                
            }
        }
        #endregion
     
        #region 试压查询
        /// <summary>
        /// 试压查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.drpBSU_ID.SelectedValue == "0")
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
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpBSU_ID.SelectedValue + "'," + this.drpInstallationId.SelectedValue + ");</script>");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            isoInfos.Clear();
            string selectList = this.hdSelectList.Value;
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();

                if (this.PTP_ID == null)
                {
                    foreach (var item in infos)
                    {
                        Model.PW_IsoInfo info = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(item);
                        isoInfos.Add(info);
                    }
                }
                else
                {
                    this.SaveList();
                    foreach (var iso in infos)
                    {
                        Model.PW_IsoInfo info = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(iso);

                        if (isoInfos.Where(y => y.ISO_ID == iso).Count() == 0)
                        {
                            isoInfos.Add(info);
                        }
                    }
                }
                //foreach (var item in infos)
                //{
                //    Model.PW_IsoInfo info = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(item);
                //    isoInfos.Add(info);
                //}
            }

            
           
            if (isoInfos.Count > 0)
            {
                this.gvTestPackage.Visible = true;
                this.gvTestPackage.DataSource = isoInfos;
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
                var getunit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (getunit == null || getunit.UnitType == "1" || getunit.UnitType == "3")
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
                               where y.BSU_ID == parentId
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
                var days = (from x in Funs.DB.TP_TestPackage
                            where x.PTP_TableDate >= startTime && x.PTP_TableDate < endTime
                            && x.InstallationId.ToString() == node.Parent.Value
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
                DateTime date = Convert.ToDateTime(parentId);
                var dReports = from x in Funs.DB.TP_TestPackage
                               where x.PTP_TableDate.Value.Year== date.Year && x.PTP_TableDate.Value.Month == date.Month &&
                                x.PTP_TableDate.Value.Day == date.Day &&  x.InstallationId.ToString() == node.Parent.Parent.Value
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.PTP_TestPackageNo;
                    newNode.Value = item.PTP_ID;
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
                PTP_ID = this.tvControlItem.SelectedValue;              
              
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    var testPackage = BLL.TestPackageManageEditService.GetTP_TestPackageByID(PTP_ID);
                    if (testPackage != null)
                    {
                        this.PTP_ID = testPackage.PTP_ID;
                        if (testPackage.InstallationId.HasValue)
                        {
                            this.drpInstallationId.SelectedValue = testPackage.InstallationId.ToString();
                        }

                        if (!String.IsNullOrEmpty(testPackage.BSU_ID))
                        {
                            this.drpBSU_ID.SelectedValue = testPackage.BSU_ID;
                        }
                        this.txtPTP_TestPackageNo.Text = testPackage.PTP_TestPackageNo;
                        this.txtPTP_TestPackageName.Text = testPackage.PTP_TestPackageName;
                        this.txtPTP_TestHeat.Text = testPackage.PTP_TestHeat;
                        this.txtPTP_TestService.Text = testPackage.PTP_TestService;
                        if (!String.IsNullOrEmpty(testPackage.PTP_TestType))
                        {
                            this.drpPTP_TestType.SelectedValue = testPackage.PTP_TestType;
                        }
                        //if (!String.IsNullOrEmpty(testPackage.PTP_Finisher))
                        //{
                        //    this.drpPTP_Finisher.SelectedValue = testPackage.PTP_Finisher;
                        //}
                        //if (testPackage.PTP_FinishDate.HasValue)
                        //{
                        //    this.txtPTP_FinishDate.Value = String.Format("{0:yyyy-MM-dd}", testPackage.PTP_FinishDate);
                        //}
                        if (!String.IsNullOrEmpty(testPackage.PTP_Tabler))
                        {
                            if (this.drpPTP_Tabler.Items.FindByValue(testPackage.PTP_Tabler) != null)
                            {
                                this.drpPTP_Tabler.SelectedValue = testPackage.PTP_Tabler;
                            }
                        }
                        if (testPackage.PTP_TableDate.HasValue)
                        {
                            this.txtPTP_TableDate.Value =  String.Format("{0:yyyy-MM-dd}",testPackage.PTP_TableDate);
                        }
                        if (!String.IsNullOrEmpty(testPackage.PTP_Modifier))
                        {
                            if (this.drpPTP_Modifier.Items.FindByValue(testPackage.PTP_Tabler) != null)
                            { this.drpPTP_Modifier.SelectedValue = testPackage.PTP_Tabler; }                              
                        }
                        if (testPackage.PTP_ModifyDate.HasValue)
                        {
                            this.txtPTP_ModifyDate.Value =  String.Format("{0:yyyy-MM-dd}",testPackage.PTP_ModifyDate);
                        }
                       
                        this.txtPTP_Remark.Text = testPackage.PTP_Remark;
                        this.txtPTP_TestPackageCode.Text = testPackage.PTP_TestPackageCode;
                        this.txtPTP_TestAmbientTemp.Text = testPackage.PTP_TestAmbientTemp;
                        this.txtPTP_TestMediumTemp.Text = testPackage.PTP_TestMediumTemp;
                        this.txtPTP_TestPressure.Text = testPackage.PTP_TestPressure;
                        this.txtPTP_TestPressureTemp.Text = testPackage.PTP_TestPressureTemp;
                        this.txtPTP_TestPressureTime.Text = testPackage.PTP_TestPressureTime;
                        this.txtPTP_TightnessTest.Text = testPackage.PTP_TightnessTest;
                        this.txtPTP_TightnessTestTemp.Text = testPackage.PTP_TightnessTestTemp;
                        this.txtPTP_TightnessTestTime.Text = testPackage.PTP_TightnessTestTime;
                        this.txtPTP_LeakageTestService.Text = testPackage.PTP_LeakageTestService;
                        this.txtPTP_LeakageTestPressure.Text = testPackage.PTP_LeakageTestPressure;
                        this.txtPTP_VacuumTestService.Text = testPackage.PTP_VacuumTestService;
                        this.txtPTP_VacuumTestPressure.Text = testPackage.PTP_VacuumTestPressure;
                        this.txtPTP_OperationMedium.Text = testPackage.PTP_OperationMedium;
                        this.txtPTP_PurgingMedium.Text = testPackage.PTP_PurgingMedium;
                        this.txtPTP_CleaningMedium.Text = testPackage.PTP_CleaningMedium;
                        this.txtPTP_AllowSeepage.Text = testPackage.PTP_AllowSeepage;
                        this.txtPTP_FactSeepage.Text = testPackage.PTP_FactSeepage;
                        
                        isoInfos.Clear();
                        isoInfos = BLL.TestPackageManageEditService.GetIsoInfosByPTP_ID(PTP_ID);                  
                        if (isoInfos.Count > 0)
                        {
                            this.gvTestPackage.Visible = true;
                            this.gvTestPackage.DataSource = isoInfos;
                            this.gvTestPackage.DataBind();
                        }
                    }
                }               
            }
        }
        #endregion
        #region 打印按钮事件
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {

            if (ButtonList.Contains(BLL.Const.BtnPrint) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    string reportId = PTP_ID;
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CheckReportPrint('" + BLL.Const.TestPackageManageReportId + "','" + reportId + "','');</script>");

                    //var test = BLL.TestPackageManageEditService.GetTP_TestPackageByID(PTP_ID);
                    //if (test != null && !test.PTP_AduditDate.HasValue)
                    //{

                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此试压单已审核不能删除！');", true);
                    //}

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的试压记录！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
                if (!string.IsNullOrEmpty(PTP_ID))
                {
                    var test = BLL.TestPackageManageEditService.GetTP_TestPackageByID(PTP_ID);
                    if (test != null && !test.PTP_AduditDate.HasValue)
                    {
                        BLL.TestPackageManageEditService.DeleteTP_IsoListByPTP_ID(PTP_ID);
                        BLL.TestPackageManageEditService.DeleteTP_TestPackageByTP_TestPackageID(PTP_ID);
                     
                        this.TextIsReadOnly(true);
                        this.ButtonIsEnabled(false);
                        isoInfos.Clear();
                        this.TextIsEmpty();
                        this.PTP_ID = null;
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除试压");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！');", true);
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此试压单已审核不能删除！');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的试压记录！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion 
      
        #region 介质转换
        /// <summary>
        /// 介质转换
        /// </summary>
        /// <param name="ISO_NDTClass"></param>
        /// <returns></returns>
        protected string ConvertSER_ID(object SER_ID)
        {
            if (SER_ID != null)
            {
                return BLL.MediumService.GetServiceBySERID(SER_ID.ToString()).SER_Name;
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}