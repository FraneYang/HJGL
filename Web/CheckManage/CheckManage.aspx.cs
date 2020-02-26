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

namespace Web.CheckManage
{
    public partial class CheckManage : PPage
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
        /// 焊口信息
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
            this.ddlAuditMan.Enabled = !readOnly;
            this.txtAuditDate.Disabled = readOnly;
            this.ddlCheckMan.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
        }

        /// <summary>
        /// 文本置空
        /// </summary>
        private void TextEmpty()
        {
            this.txtCheckCode.Text = string.Empty;
            this.ddlUnit.SelectedValue = "0";
            this.ddlInstallationId.SelectedValue = "0";
            this.txtCheckDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.txtCheckType.Text = "C1";
            this.ddlTabler.SelectedValue = "0";
            this.txtTableDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.ddlAuditMan.SelectedValue = "0";
            this.txtAuditDate.Value = string.Empty;
            this.ddlCheckMan.SelectedValue = "0";
            this.txtRemark.Text = string.Empty;           
            this.ckAllFilmDate.Checked = true;
            this.ckAllReportDate.Checked = true;
            this.lbtnToTrust.Text = string.Empty;    
        }

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
        /// 检测单细表集合
        /// </summary>
        public List<Model.View_CH_CheckItem> checkItems = new List<Model.View_CH_CheckItem>();

        #region 页面加载
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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.CheckMenuId);

                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);
                this.txtCheckTime.Visible = true;
                txtSearchCode.Visible = false;

                Funs.PleaseSelect(ddlUnit); 
                Funs.PleaseSelect(ddlInstallationId);
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1" || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
                {
                    this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));  
                }
               
                
                Funs.PleaseSelect(ddlTabler);
                this.ddlTabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(ddlAuditMan);
                this.ddlAuditMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(ddlCheckMan);
                this.ddlCheckMan.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                this.txtCheckDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtTableDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.ddlTabler.SelectedValue = this.CurrUser.UserId;
                this.txtCheckType.Text = "C1";

                this.txtCheckTime.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>CheckSearch();</script>");
            }
         }
        #endregion

        #region 增加按钮
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
                this.ddlTabler.Enabled = false;
                this.txtTableDate.Disabled = true;
                this.ddlAuditMan.Enabled = false;
                this.txtAuditDate.Disabled = true;
                this.CH_TrustID = null;
                TextEmpty();
                checkItems.Clear();
                this.gvCheckItem.Visible = false;
                CHT_CheckID = null;
                this.txtCheckDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtTableDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.ddlTabler.SelectedValue = this.CurrUser.UserId;
                this.txtCheckType.Text = "C1";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }
        #endregion

        #region 查询按钮
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ddlUnit.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择单位！')", true);
                return;
            }
            if (this.ddlInstallationId.SelectedValue=="0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择装置！')", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.ddlUnit.SelectedValue + "'," + this.ddlInstallationId.SelectedValue + ",'" + CH_TrustID + "');</script>");
            }
        }
        #endregion

        #region 查询返回值
        /// <summary>
        /// 查询返回结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            checkItems.Clear();
            string selectList = this.hdSelectList.Value;
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();

                // 由于删除和其实原因造成委托标志没改变，这里处理下（02表示一次委托已审核）
                foreach (var item in infos)
                {
                    Model.HJGLDB db = Funs.DB;
                    Model.PW_JointInfo jot = db.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == item);
                    if (jot != null)
                    {
                        if (jot.JOT_TrustFlag == "00")
                        {
                            jot.JOT_TrustFlag = "02";
                            db.SubmitChanges();
                        }
                    }
                }

                if (CHT_CheckID == null)
                {
                    foreach (var item in infos)
                    {
                        Model.View_CH_CheckItem info = Funs.DB.View_CH_CheckItem.FirstOrDefault(x => x.JOT_ID == item && x.CHT_CheckID == null);
                        if (info != null)
                        {
                            checkItems.Add(info);
                        }
                    }

                    if (checkItems.Count() > 0)
                    {
                        lbtnToTrust.Text = checkItems.FirstOrDefault().CH_TrustCode;
                    }
                }
                else
                {
                    this.SaveList();
                    foreach (var jotid in infos)
                    {
                        Model.CH_CheckItem item = BLL.CheckItemManageService.GetCheckItemByJotId(jotid);
                        if (item == null)
                        {
                            Model.View_CH_CheckItem info = BLL.CheckItemManageService.GetViewCheckItemByJOTID(jotid);
                            checkItems.Add(info);
                        }
                    }

                }

                if (checkItems.Count > 0)
                {
                    checkItems = checkItems.OrderBy(x => x.JOT_JointNo).ToList();//(from x in checkItems orderby x.JOT_JointNo select x).ToList();

                    this.gvCheckItem.Visible = true;
                    this.gvCheckItem.DataSource = checkItems;
                    this.gvCheckItem.DataBind();
                }
            }

        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (this.gvCheckItem.Rows.Count > 0)
                {
                    SaveList();
                    foreach (var item in checkItems)
                    {
                        if (item.CHT_FilmDate == null)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('拍片日期不能为空！')", true);
                            return;
                        }

                        if (item.CHT_TotalFilm == null || item.CHT_PassFilm == null)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('拍片总数和拍片数不能为空！')", true);
                            return;
                        }

                        if (item.CHT_TotalFilm < item.CHT_PassFilm)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('拍片数不能大于拍片总数！')", true);
                            return;
                        }
                    }

                    Model.CH_Check check = new Model.CH_Check();
                    check.ProjectId = this.CurrUser.ProjectId;
                    check.CHT_CheckCode = this.txtCheckCode.Text.Trim();

                    if (checkItems.Count() > 0)
                    {
                        var q = from x in Funs.DB.CH_TrustItem where x.CH_TrustItemID == checkItems.First().CH_TrustItemID select x;
                        check.CH_TrustID = q.First().CH_TrustID;
                    }

                    if (this.ddlUnit.SelectedValue != "0")
                    {
                        check.UnitId = this.ddlUnit.SelectedValue;
                    }
                    if (this.ddlInstallationId.SelectedValue != "0")
                    {
                        check.InstallationId = Convert.ToInt32(this.ddlInstallationId.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(this.txtCheckDate.Value))
                    {
                        check.CHT_CheckDate = Convert.ToDateTime(this.txtCheckDate.Value);
                    }
                    check.CHT_CheckType = this.txtCheckType.Text.Trim();
                    if (this.ddlTabler.SelectedValue != "0")
                    {
                        check.CHT_Tabler = this.ddlTabler.SelectedValue;
                    }
                    if (!string.IsNullOrEmpty(this.txtTableDate.Value))
                    {
                        check.CHT_TableDate = Convert.ToDateTime(this.txtTableDate.Value);
                    }
                    if (this.ddlAuditMan.SelectedValue != "0")
                    {
                        check.CHT_AuditMan = this.ddlAuditMan.SelectedValue;
                    }
                    if (!string.IsNullOrEmpty(this.txtAuditDate.Value))
                    {
                        check.CHT_AuditDate = Convert.ToDateTime(this.txtAuditDate.Value);
                    }
                    if (this.ddlCheckMan.SelectedValue != "0")
                    {
                        check.CHT_CheckMan = this.ddlCheckMan.SelectedValue;
                    }
                    check.CHT_Remark = this.txtRemark.Text.Trim();

                    var updateCheck = BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID);
                    if (updateCheck != null && updateCheck.CHT_AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此检测单已审核不能修改！');", true);
                        return;
                    }

                    if (updateCheck != null && !string.IsNullOrEmpty(CHT_CheckID))
                    {
                        if (updateCheck.CHT_CheckCode != this.txtCheckCode.Text.Trim())
                        {
                            if (BLL.CheckManageService.IsExistCheckCode(this.CurrUser.ProjectId, this.txtCheckCode.Text.Trim()))
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此检测单号已经存在！')", true);
                                return;
                            }
                        }

                        check.CHT_CheckID = CHT_CheckID;
                        BLL.CheckManageService.UpdateCheck(check);
                       
                        if (jointInfo != null)
                        {
                            foreach (var j in jointInfo)
                            {
                                var chckItem = BLL.CheckItemManageService.GetTrustItemByCheckAndJotId(check.CHT_CheckID, j);
                                if (chckItem != null)
                                {
                                    BLL.CheckItemManageService.UpdateJointCheckFlag(j, "2");
                                }
                            }
                        } 
                        BLL.CheckItemManageService.DeleteCheckItemByCheckId(CHT_CheckID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改检测单信息！");
                    }
                    else
                    {
                        if (BLL.CheckManageService.IsExistCheckCode(this.CurrUser.ProjectId, this.txtCheckCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此检测单号已经存在！')", true);
                            return;
                        }

                        check.CHT_CheckID = SQLHelper.GetNewID(typeof(Model.CH_Check));
                        BLL.CheckManageService.AddCheck(check);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加检测单信息！");
                    }

                    foreach (var item in checkItems)
                    {
                        Model.CH_CheckItem checkitem = new Model.CH_CheckItem();
                        checkitem.CHT_CheckID = check.CHT_CheckID;
                        checkitem.JOT_ID = item.JOT_ID;
                        checkitem.CH_TrustItemID = item.CH_TrustItemID;
                        checkitem.CHT_CheckMethod = item.CH_NDTMethod;
                        checkitem.CHT_RequestDate = item.CHT_RequestDate;
                        checkitem.CHT_RepairLocation = item.CHT_RepairLocation;
                        checkitem.CHT_TotalFilm = item.CHT_TotalFilm;
                        checkitem.CHT_PassFilm = item.CHT_PassFilm;
                        checkitem.CHT_CheckResult = item.CHT_CheckResult;
                        checkitem.CHT_CheckNo = item.CHT_CheckNo;
                        checkitem.CHT_FilmDate = item.CHT_FilmDate;
                        checkitem.CHT_ReportDate = item.CHT_ReportDate;
                        checkitem.CHT_FilmSpecifications = item.CHT_FilmSpecifications;
                        BLL.CheckItemManageService.AddCheckItem(checkitem);

                        BLL.CheckItemManageService.UpdateJointCheckFlag(checkitem.JOT_ID, "1");
                    }

                    checkItems.Clear();
                    this.gvCheckItem.DataSource = null;
                    this.gvCheckItem.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>CheckSearch();</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('检测单信息不能为空！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请联系管理员！')", true);
            }
        }

        /// <summary>
        /// 保存检测单细表
        /// </summary>
        protected void SaveList()
        {
            checkItems.Clear();
            for (int i = 0; i < this.gvCheckItem.Rows.Count; i++)
            {
                HiddenField hdJOT_ID = (HiddenField)(this.gvCheckItem.Rows[i].FindControl("hdJOT_ID"));
                HiddenField hdTrustItemId = (HiddenField)(this.gvCheckItem.Rows[i].FindControl("hdTrustItemId"));
                HiddenField hdNDTMethod = (HiddenField)(this.gvCheckItem.Rows[i].FindControl("hdNDTMethod"));
                TextBox txtRepairLocation = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_RepairLocation"));
                TextBox txtTotalFilm = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_TotalFilm"));
                TextBox txtPassFilm = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_PassFilm"));
                TextBox txtFilmDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_FilmDate"));
                TextBox txtReportDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_ReportDate"));
                TextBox txtCheckNo = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_CheckNo"));
                TextBox txtRemark = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_FilmSpecifications"));
                TextBox txtCheckResult = (TextBox)(this.gvCheckItem.Rows[i].FindControl("txtCheckResult"));
                Label lblISO_IsoNo = (Label)(this.gvCheckItem.Rows[i].FindControl("ISO_IsoNo"));
                Label lbJOT_JointNo = (Label)(this.gvCheckItem.Rows[i].FindControl("lbJOT_JointNo"));
                Label lbNDT_Name = (Label)(this.gvCheckItem.Rows[i].FindControl("lbNDT_Name"));
               
                Model.View_CH_CheckItem item = new Model.View_CH_CheckItem();
                item.JOT_ID = hdJOT_ID.Value;
                item.ISO_IsoNo = lblISO_IsoNo.Text;
                item.JOT_JointNo = lbJOT_JointNo.Text;
                item.NDT_Name = lbNDT_Name.Text;
                item.CH_TrustItemID = hdTrustItemId.Value;
                item.CH_NDTMethod = hdNDTMethod.Value;
                item.CHT_RepairLocation = txtRepairLocation.Text;
                if (txtTotalFilm.Text != string.Empty)
                {
                    item.CHT_TotalFilm = Convert.ToInt32(txtTotalFilm.Text);
                }
                if (txtPassFilm.Text != string.Empty)
                {
                    item.CHT_PassFilm = Convert.ToInt32(txtPassFilm.Text);
                }
                if (txtFilmDate.Text != string.Empty)
                {
                    item.CHT_FilmDate = DateTime.Parse(txtFilmDate.Text.Trim());
                }
                if (txtReportDate.Text != string.Empty)
                {
                    item.CHT_ReportDate = DateTime.Parse(txtReportDate.Text.Trim());
                }
                item.CHT_CheckNo = txtCheckNo.Text;
                item.CHT_FilmSpecifications = txtRemark.Text;
                item.CHT_CheckResult = txtCheckResult.Text;
                
                checkItems.Add(item);
            }
        }
        #endregion

        #region 删除按钮
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(CHT_CheckID))
                {
                    var check = BLL.CheckManageService.GetCheckByCHT_CheckID(CHT_CheckID);
                    if (check != null && !check.CHT_AuditDate.HasValue)
                    {
                        BLL.CheckItemManageService.DeleteCheckItemByCheckId(CHT_CheckID);
                        BLL.CheckManageService.DeleteCheck(CHT_CheckID);

                        if (jointInfo != null)
                        {
                            foreach (var j in jointInfo)
                            {
                                BLL.CheckItemManageService.UpdateJointCheckFlag(j, "2");
                            }
                        }

                        this.TextIsReadOnly(true);
                        this.ButtonIsEnabled(false);
                        checkItems.Clear();
                        this.TextEmpty();
                        this.CHT_CheckID = null;
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除检测单");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！');", true);
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>CheckSearch();</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此检测单已审核不能删除！');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的检测单！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 打印按钮
        /// <summary>
        /// 打印按钮
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
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CheckReportPrint('" + BLL.Const.CheckReportId + "','" + reportId + "','');</script>");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要打印的无损检测单！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region Treeview
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
                    if (this.CurrUser.UnitId != null && BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
                    {
                        var trustID = from z in BLL.Funs.DB.CH_Trust where z.CH_CheckUnit == this.CurrUser.UnitId select z.CH_TrustID;
                        trustcode = (from y in trustcode where trustID.Contains(y.CH_TrustID) select y).ToList();
                    }

                    foreach (var item in trustcode)
                    {
                        TreeNode newNode = new TreeNode();
                        //newNode.Text = item.CHT_CheckCode;
                        newNode.Value = item.CHT_CheckID;
                        if (item.CHT_AuditDate == null)
                        {
                            newNode.Text = "<font color='#EE0000'>" + item.CHT_CheckCode + "</font>";
                        }
                        else
                        {
                            newNode.Text = item.CHT_CheckCode;
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

                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) != null && BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
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

                    if (days.Count() > 0 && days.First().CHT_CheckDate != null)
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
                this.TextIsReadOnly(false);
                this.ButtonIsEnabled(true);
                CHT_CheckID = this.tvControlItem.SelectedValue;
                checkItems = new List<Model.View_CH_CheckItem>();

                if (!string.IsNullOrEmpty(CHT_CheckID))
                {
                    //var q = from x in BLL.Funs.DB.CH_CheckItem where x.CHT_CheckID == CHT_CheckID select x;
                    //if (q.Count() > 0)
                    //{
                    //    string trustItemId = q.First().CH_TrustItemID;

                    //    CH_TrustID = (from x in BLL.Funs.DB.CH_TrustItem where x.CH_TrustItemID == trustItemId select x.CH_TrustID).FirstOrDefault();
                    //    if (BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID) != null)
                    //    {
                    //        this.lbtnToTrust.Text = BLL.TrustManageEditService.GetCH_TrustByID(CH_TrustID).CH_TrustCode;
                    //    }
                    //}

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

                    if (check.InstallationId.HasValue)
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
                    if (check.CHT_AuditDate != null)
                    {
                        this.txtAuditDate.Value = string.Format("{0:yyyy-MM-dd}", check.CHT_AuditDate);
                    }
                    if (!string.IsNullOrEmpty(check.CHT_CheckMan))
                    {
                        this.ddlCheckMan.SelectedValue = check.CHT_CheckMan;
                    }
                    this.txtRemark.Text = check.CHT_Remark;
                    List<Model.CH_CheckItem> items = BLL.CheckItemManageService.GetTrustItemByCheck(CHT_CheckID);
                    foreach (var t in items)
                    {
                        var checkItem = BLL.CheckItemManageService.GetTrustItemByCheckItem(t.CHT_CheckItemID);
                        if (checkItem != null)
                        {
                            checkItems.Add(checkItem);
                        }
                    }
                    //checkItems = BLL.CheckItemManageService.GetCheckItemByCheckID(CHT_CheckID);
                    int i = 0;
                    jointInfo = new string[checkItems.Count];
                    foreach (var item in checkItems)
                    {
                        jointInfo[i] = item.JOT_ID;
                        i++;
                    }

                    if (checkItems.Count > 0)
                    {
                        this.gvCheckItem.Visible = true;
                        this.gvCheckItem.DataSource = checkItems;
                        this.gvCheckItem.DataBind();
                    }
                }
            }
            else
            {
                CHT_CheckID = null;
                this.gvCheckItem.Visible = false;
            }
        }
        #endregion

        #region GridView
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCheckItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string jot_id = e.CommandArgument.ToString();

            if (e.CommandName == "del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    var check = BLL.CheckManageService.GetCheckByCHT_CheckID(this.CHT_CheckID);
                    if (check != null && check.CHT_AuditDate.HasValue)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此委托单已审核不能删除！');", true);
                    }
                    else
                    {
                        SaveList();

                        foreach (Model.View_CH_CheckItem item in checkItems)
                        {
                            if (item.JOT_ID == jot_id)
                            {
                                //Model.CH_CheckItem chitem = BLL.CheckItemManageService.GetCheckItemByJotId(jot_id);
                                //if (chitem != null)
                                //{
                                //    BLL.CheckItemManageService.UpdateJointCheckFlag(jot_id, "2"); 
                                //}
                                checkItems.Remove(item);
                                break;
                            }
                        }
                    }
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
            this.gvCheckItem.DataSourceID = null;
            this.gvCheckItem.DataSource = checkItems;
            this.gvCheckItem.DataBind();

        }

        /// <summary>
        /// GridView绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCheckItem_DataBound(object sender, GridViewRowEventArgs e)
        {
            bool? isAuto = BLL.SysSetService.IsAuto(5, this.CurrUser.ProjectId);
            int rowsCount = this.gvCheckItem.Rows.Count;

            for (int i = 0; i < rowsCount; i++)
            {
                TextBox CHT_FilmDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_FilmDate"));
                TextBox txtTotalFilm = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_TotalFilm"));
                HiddenField hdJOT_Dia = (HiddenField)(this.gvCheckItem.Rows[i].FindControl("hdJOT_Dia"));
                HiddenField hdJOT_Component1 = (HiddenField)(this.gvCheckItem.Rows[i].FindControl("hdJOT_Component1"));

                if (!string.IsNullOrEmpty(CHT_FilmDate.Text))
                {
                    CHT_FilmDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(CHT_FilmDate.Text));
                }
                TextBox CHT_ReportDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_ReportDate"));
                if (!string.IsNullOrEmpty(CHT_ReportDate.Text))
                {
                    CHT_ReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(CHT_ReportDate.Text));
                }

                ((TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_PassFilm"))).Attributes["onblur"] = "GetCheckRestult()";//是否合格

                if (this.ckAllFilmDate.Checked == true)
                {
                    ((TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_FilmDate"))).Attributes["onchange"] = "GetFilmDate()"; //批量填充拍片日期
                }
                if (this.ckAllReportDate.Checked==true)
                {
                    ((TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_ReportDate"))).Attributes["onchange"] = "GetReportDate()"; //批量填充报告日期
                }

                if (!string.IsNullOrEmpty(hdJOT_Dia.Value) && isAuto == true)
                {
                    if (Convert.ToDecimal(hdJOT_Dia.Value) < 90)
                    {
                        txtTotalFilm.Text = "2";
                        if (!string.IsNullOrEmpty(hdJOT_Component1.Value))
                        {
                            string c = hdJOT_Component1.Value;
                            var com = BLL.ComponentsService.GetComponentByComID(c);
                            if (com.COM_Name.Contains("法兰"))
                            {
                                txtTotalFilm.Text = "3";
                            }
                        }   
                    }
                    if (Convert.ToDecimal(hdJOT_Dia.Value) < 477 && Convert.ToDecimal(hdJOT_Dia.Value) >= 90)
                    {
                        txtTotalFilm.Text = "6";
                    }

                    if (Convert.ToDecimal(hdJOT_Dia.Value) >= 477)
                    { 
                        int num=0;
                        num = Convert.ToInt32((Convert.ToDouble(hdJOT_Dia.Value) * 3.14) * 100000) / (250 * 100000) + 1;
                        if (num / 2 == 0)
                        {
                            txtTotalFilm.Text = num.ToString();
                        }
                        else
                        {
                            txtTotalFilm.Text = (num + 1).ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region 批量填充
        /// <summary>
        /// 是否批量填充拍片日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnAll2_Click(object sender, ImageClickEventArgs e)
        {
            this.SaveList();
            for (int i = 0; i < this.gvCheckItem.Rows.Count; i++)
            {
                TextBox txtFilmDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_FilmDate"));
                string id = txtFilmDate.Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in checkItems)
                    {
                        item.CHT_FilmDate = Convert.ToDateTime(id);
                    }
                    this.gvCheckItem.DataSource = checkItems;
                    this.gvCheckItem.DataBind();
                }
            }
        }

        /// <summary>
        /// 批量填充报告日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnReportDate_Click(object sender, ImageClickEventArgs e)
        {
            this.SaveList();
            for (int i = 0; i < this.gvCheckItem.Rows.Count; i++)
            {
                TextBox txtReportDate = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_ReportDate"));
                string id = txtReportDate.Text;
                if (!string.IsNullOrEmpty(id))
                {
                    foreach (var item in checkItems)
                    {
                        item.CHT_ReportDate = Convert.ToDateTime(id);
                    }
                    this.gvCheckItem.DataSource = checkItems;
                    this.gvCheckItem.DataBind();
                }
            }
        }
        #endregion   

        #region 获取检测结果
        /// <summary>
        /// 获取检测结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgCheckResult_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = 0; i < this.gvCheckItem.Rows.Count; i++)
            {
                TextBox txtTotalFilm = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_TotalFilm"));
                TextBox txtPassFilm = (TextBox)(this.gvCheckItem.Rows[i].FindControl("CHT_PassFilm"));
                TextBox txtCheckResult = (TextBox)(this.gvCheckItem.Rows[i].FindControl("txtCheckResult"));

                if (!string.IsNullOrEmpty(txtTotalFilm.Text) && !string.IsNullOrEmpty(txtPassFilm.Text))
                {
                    if (Convert.ToInt32(txtTotalFilm.Text.Trim()) > Convert.ToInt32(txtPassFilm.Text.Trim()))
                    {
                        txtCheckResult.Text = "不合格";
                    }
                    else if (Convert.ToInt32(txtTotalFilm.Text.Trim()) == Convert.ToInt32(txtPassFilm.Text.Trim()))
                    {
                        txtCheckResult.Text = "合格";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('合格片数不能大于总片数！')", true);
                        txtCheckResult.Text = string.Empty;
                        return;
                    }
                }
            }
        }
        #endregion

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

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlInstallationId.Items.Clear();
            Funs.PleaseSelect(this.ddlInstallationId);
            this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));  
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