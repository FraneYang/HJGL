﻿using System;
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
    public partial class TestPackageManageAudit : PPage
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

        /// <summary>
        /// 未通过数
        /// </summary>
        public int Count
        {
            get
            {
                return (int)ViewState["Count"];
            }
            set
            {
                ViewState["Count"] = value;
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
            this.drpPTP_Modifier.Enabled = !readOnly;
            this.txtPTP_ModifyDate.Enabled = !readOnly;
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

            this.drpPTP_Tabler.Enabled = !readOnly;
            this.txtPTP_TableDate.Enabled = !readOnly;
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
            this.drpPTP_TestType.SelectedValue = "0";
            this.drpPTP_Auditer.SelectedValue = this.CurrUser.UserId;
            this.txtPTP_AduditDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.drpPTP_Modifier.SelectedValue = "0";
            this.txtPTP_ModifyDate.Text = string.Empty;
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
            this.drpPTP_Tabler.SelectedValue = "0";
            this.txtPTP_TableDate.Text = string.Empty;

            this.gvTestPackage.Visible = false;
            this.gvTestPackage.DataSource = isoInfos;
            this.gvTestPackage.DataBind();
        }
        #endregion

         /// <summary>
        /// 管线集合
        /// </summary>
        private  List<Model.View_TestPackageManageAudit> isoInfos = new List<Model.View_TestPackageManageAudit>();

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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.TestPackageManageAuditMenuId);
                this.TextIsReadOnly(true);

                Funs.PleaseSelect(drpBSU_ID);
                this.drpBSU_ID.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(drpPTP_TestType);
                this.drpPTP_TestType.Items.AddRange(BLL.PressureService.GetBS_PackageTestTypeList());

                Funs.PleaseSelect(drpInstallationId);
                this.drpInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpPTP_Modifier);
                this.drpPTP_Modifier.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpPTP_Auditer);
                this.drpPTP_Auditer.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpPTP_Tabler);
                this.drpPTP_Tabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpSelectPrint);
                this.drpSelectPrint.Items.AddRange(BLL.ReportPrintService.TestPackageSelectPrint());

                this.txtReportDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
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
                               where x.PTP_TableDate.Value.Year == date.Year && x.PTP_TableDate.Value.Month == date.Month &&
                                x.PTP_TableDate.Value.Day == date.Day && x.InstallationId.ToString() == node.Parent.Parent.Value
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
                       
                        if (!String.IsNullOrEmpty(testPackage.PTP_Tabler))
                        {
                            this.drpPTP_Tabler.SelectedValue = testPackage.PTP_Tabler;
                        }
                        if (testPackage.PTP_TableDate.HasValue)
                        {
                            this.txtPTP_TableDate.Text = String.Format("{0:yyyy-MM-dd}", testPackage.PTP_TableDate);
                        }
                        if (!String.IsNullOrEmpty(testPackage.PTP_Auditer))
                        {
                            this.drpPTP_Auditer.SelectedValue = testPackage.PTP_Auditer;
                        }

                        if (testPackage.PTP_AduditDate.HasValue)
                        {
                            this.btnCancelAudit.Visible = true;
                            this.btnAudit.Visible = false;
                            this.txtPTP_AduditDate.Value = String.Format("{0:yyyy-MM-dd}", testPackage.PTP_AduditDate);
                        }
                        else
                        {
                            this.btnCancelAudit.Visible = false;
                            this.btnAudit.Visible = true;
                            this.txtPTP_AduditDate.Value = String.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                        }

                        if (!String.IsNullOrEmpty(testPackage.PTP_Modifier))
                        {
                            this.drpPTP_Modifier.SelectedValue = testPackage.PTP_Modifier;
                        }
                        if (testPackage.PTP_ModifyDate.HasValue)
                        {
                            this.txtPTP_ModifyDate.Text = String.Format("{0:yyyy-MM-dd}", testPackage.PTP_ModifyDate);
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
                        isoInfos = BLL.TestPackageManageAuditService.GetTestPackageManageAuditByPTP_ID(PTP_ID);
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
                string reportId = this.tvControlItem.SelectedValue;
                string projectName = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName.Replace("/", ",");
               
                if (this.drpSelectPrint.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的报表！')", true);
                    return;
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.RTCheckResultReportId)
                {
                    //var test = BLL.TestPackageManageEditService.GetTP_TestPackageByID(reportId);
                    //var unit = BLL.UnitService.GetUnitByUnitType("3", this.CurrUser.ProjectId);
                    //if (unit != null)
                    //{
                    //    trustUnitName = unit.UnitName;
                    //}
                    string varValue = string.Empty;
                    //if (test != null)
                    //{
                    //    sysNo = test.PTP_TestPackageName;
                    //}
                    
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        string sysNo = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Text;//单元
                        string unitName = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Parent.Text;
                        varValue = projectName + "|" + sysNo + "|" + unitName + "|" + unitName;

                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TestPackageReportPrint('" + BLL.Const.RTCheckResultReportId + "','" + reportId + "','" + varValue + "');</script>");
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择试压包！')", true);
                        return;
                    }
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.WeldJointRTCheck1ReportId)
                {
                    string varValue = string.Empty;
                    string ndtr = "10%";  // 默认值
                    string ndtType = "X射线";
                    
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        var iso = (from x in Funs.DB.TP_IsoList where x.PTP_ID == this.tvControlItem.SelectedValue select x).FirstOrDefault();
                        if (iso != null)
                        {
                            var isoInfo = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(iso.ISO_ID);
                            if (isoInfo != null)
                            {
                                if (BLL.DetectionService.GetNDTRateByNDTRID(isoInfo.NDTR_ID) != null)
                                {
                                    ndtr = BLL.DetectionService.GetNDTRateByNDTRID(isoInfo.NDTR_ID).NDTR_Name + "%";
                                }
                                if (BLL.TestingService.GetTestingByTestingId(isoInfo.NDT_ID) != null)
                                {
                                    ndtType = BLL.TestingService.GetTestingByTestingId(isoInfo.NDT_ID).NDT_Name;
                                }
                            }
                        }
                        varValue = projectName + "|" + this.tvControlItem.SelectedNode.Parent.Parent.Parent.Text + "|" + ndtType + "|" + ndtr;

                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TestPackageReportPrint('" + BLL.Const.WeldJointRTCheck1ReportId + "','" + reportId + "','" + varValue + "');</script>");
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择试压包！')", true);
                        return;
                    }
                }

                if (this.drpSelectPrint.SelectedValue == BLL.Const.WeldJointRTCheck2ReportId)
                {
                    string varValue = string.Empty;
                   
                    if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                    { 
                        varValue = projectName + "|" + this.tvControlItem.SelectedNode.Parent.Parent.Parent.Text;
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>TestPackageReportPrint('" + BLL.Const.WeldJointRTCheck2ReportId + "','" + reportId + "','" + varValue + "');</script>");
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择试压包！')", true);
                        return;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 在控件被绑定后激发
        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTestPackage_DataBound(object sender, EventArgs e)
        {
            Count = 0;
            int Count1 = 0, Count2 = 0, Count3 = 0, Count4 = 0;
            int rowsCount = this.gvTestPackage.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Label lbIsoInfoCount = ((Label)(this.gvTestPackage.Rows[i].FindControl("IsoInfoCount")));//总焊口
                Label lbIsoInfoCountT = ((Label)(this.gvTestPackage.Rows[i].FindControl("IsoInfoCountT")));//完成总焊口
                Label lbCountS = ((Label)(this.gvTestPackage.Rows[i].FindControl("CountS")));//合格数
                Label lbCountU = ((Label)(this.gvTestPackage.Rows[i].FindControl("CountU")));//不合格数
                HiddenField hdNDTR_Rate = ((HiddenField)(this.gvTestPackage.Rows[i].FindControl("NDTR_Rate")));//应检测比例
                HiddenField hdRatioC = ((HiddenField)(this.gvTestPackage.Rows[i].FindControl("RatioC")));//实际检测比例
               
                int IsoInfoCount = int.Parse(lbIsoInfoCount.Text);
                int IsoInfoCountT = int.Parse(lbIsoInfoCountT.Text);
                int CountS = int.Parse(lbCountS.Text);
                int CountU = int.Parse(lbCountU.Text);
                decimal Rate =0;
                bool convertible = decimal.TryParse(hdNDTR_Rate.Value, out Rate);
                decimal Ratio = decimal.Parse(hdRatioC.Value);

                if (IsoInfoCount > IsoInfoCountT) //未焊完
                {
                    Count1 += 1;                     
                    this.gvTestPackage.Rows[i].BackColor = System.Drawing.Color.Cyan;
                }
                else if (Rate > Ratio) //已焊完，未达检测比例
                {
                    Count2 += 1;
                    this.gvTestPackage.Rows[i].BackColor = System.Drawing.Color.Yellow;
                }
                else if (CountU > 0) //已焊完，已达检测比例，但有不合格
                {
                    Count3 += 1;                 
                    this.gvTestPackage.Rows[i].BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    Count4 += 1;
                    this.gvTestPackage.Rows[i].BackColor = System.Drawing.Color.Purple;
                }
            }

            Count = Count1 + Count2 + Count2;
            this.lab1.Text = Count1.ToString();
            this.lab2.Text = Count2.ToString();
            this.lab3.Text = Count3.ToString();
            this.lab4.Text = Count4.ToString();
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
                if (e.CommandName == "lbLink")
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowJointInfoView('" + ISO_ID + "');</script>");
                }
            }
        }
        #endregion

        #region 审核按钮事件
        /// <summary>
        /// 审核按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAuditing) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                var updateTestPackage = BLL.TestPackageManageEditService.GetTP_TestPackageByID(PTP_ID);
                if (updateTestPackage != null && !String.IsNullOrEmpty(this.PTP_ID))
                {
                    if (Count == 0)
                    {
                        Model.TP_TestPackage testPackage = new Model.TP_TestPackage();
                        testPackage.PTP_ID = this.PTP_ID;
                        if (!String.IsNullOrEmpty(this.txtPTP_AduditDate.Value) && this.drpPTP_Auditer.SelectedValue != "0")
                        {
                            testPackage.PTP_AduditDate = DateTime.Parse(this.txtPTP_AduditDate.Value);
                            testPackage.PTP_Auditer = this.drpPTP_Auditer.SelectedValue;
                            BLL.TestPackageManageAuditService.AuditTP_TestPackage(testPackage);

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
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('管线未全部通过不允许审核操作！')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要审核的单据！')", true);
                }
            }
        }
        #endregion

        #region 取消审核
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelAudit_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnCancelAuditing) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                var updateTestPackage = BLL.TestPackageManageEditService.GetTP_TestPackageByID(PTP_ID);
                if (updateTestPackage != null && !String.IsNullOrEmpty(this.PTP_ID))
                {
                    Model.TP_TestPackage testPackage = new Model.TP_TestPackage();
                    testPackage.PTP_ID = this.PTP_ID;
                    testPackage.PTP_Auditer = null;
                    testPackage.PTP_AduditDate = null;
                    BLL.TestPackageManageAuditService.AuditTP_TestPackage(testPackage);

                    this.TextIsReadOnly(true);
                    this.btnCancelAudit.Visible = false;
                    this.btnAudit.Visible = true;

                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('取消审核完成！')", true);
                }
            }
        }
        #endregion
    }
}