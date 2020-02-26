using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{    
    public partial class PointManage :PPage
    {
        #region 定义项
        /// <summary>
        /// 点口主键
        /// </summary>
        public string PW_PointID
        {
            get
            {
                return (string)ViewState["PW_PointID"];
            }
            set
            {
                ViewState["PW_PointID"] = value;
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
            this.txtPointID.Enabled = !readOnly;
            this.drpUnit.Enabled = !readOnly;
            this.ddlInstallationId.Enabled = !readOnly;
            this.txtPointDate.Enabled = !readOnly;
            this.drpTabler.Enabled = !readOnly;
            this.txtTableDate.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
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
        /// 焊口集合
        /// </summary>
        private List<Model.PW_JointInfo> jointInfos = new List<Model.PW_JointInfo>();

        #region 加载页面
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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PointMenuId);
                this.txtPointManageDate.Visible = true;
                txtSearchCode.Visible = false;

                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);

                this.txtPointManageDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>PointSearch();</script>");
            }
        }
        #endregion

        #region TreeView
        /// <summary>
        /// 查询Treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReportSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPointManageDate.Value.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtPointManageDate.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);


                this.tvControlItem.Nodes.Clear();
                List<Model.Base_Unit> units = null;
                var unit =BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit == null || unit.UnitType == "1" || unit.UnitType == "4")
                {
                    if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                    {
                        units = (from x in Funs.DB.Base_Unit
                                 join y in Funs.DB.Base_WorkArea on x.UnitId equals y.UnitId
                                 where (x.UnitId == this.CurrUser.UnitId || y.SupervisorUnitId == this.CurrUser.UnitId) && x.ProjectId == this.CurrUser.ProjectId
                                 select x).Distinct().ToList();
                    }
                    else
                    {
                        units = BLL.UnitService.GetUnitsByUnitType("2", this.CurrUser.ProjectId);
                    }
                }
                else
                {
                    units = BLL.UnitService.GetUnits(this.CurrUser.UnitId);
                }
                if (units != null)
                {
                    foreach (var item in units)
                    {
                        TreeNode rootNode = new TreeNode();//定义根节点
                        rootNode.Text = item.UnitName;
                        rootNode.Value = item.UnitId;
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
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择日报月份！')", true);
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
                var install = (from x in Funs.DB.Base_Installation
                               join y in Funs.DB.BO_Point on x.InstallationId equals y.InstallationId
                               where y.PW_PointDate >= startTime && y.PW_PointDate < endTime && y.BSU_ID == parentId
                               select x).Distinct();

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
                    newNode.Text = this.txtPointManageDate.Value.Trim();
                    newNode.Value = this.txtPointManageDate.Value.Trim();
                    nodes.Add(newNode);
                }
                if (node.Depth == 2)
                {
                    var days = (from x in Funs.DB.BO_Point
                                where x.InstallationId.ToString() == node.Parent.Value
                                && x.BSU_ID == node.Parent.Parent.Value
                                && x.PW_PointDate >= startTime && x.PW_PointDate < endTime
                                select x.PW_PointDate).Distinct();
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
                    var dReports = from x in Funs.DB.BO_Point
                                   where x.InstallationId.ToString() == node.Parent.Parent.Value
                                   && x.BSU_ID == node.Parent.Parent.Parent.Value
                                   && x.PW_PointDate == Convert.ToDateTime(parentId)
                                   orderby x.PW_PointNo
                                   select x;
                    foreach (var item in dReports)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = item.PW_PointNo;
                        newNode.Value = item.PW_PointID;
                        nodes.Add(newNode);
                    }
                }
            }

            else
            {
                if (node.Depth == 1)
                {
                    var pointInfo = from x in BLL.Funs.DB.BO_Point
                                    where x.PW_PointNo == txtSearchCode.Text.Trim()
                                    && x.ProjectId == this.CurrUser.ProjectId
                                    && x.InstallationId.ToString() == node.Value
                                    && x.BSU_ID == node.Parent.Value
                                    select x;
                    if (pointInfo.Count() > 0 && pointInfo.First().PW_PointDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM}", pointInfo.First().PW_PointDate);
                        newNode.Value = string.Format("{0:yyyy-MM}", pointInfo.First().PW_PointDate);
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 2)
                {
                    var pointInfo = from x in BLL.Funs.DB.BO_Point
                                    where x.PW_PointNo == txtSearchCode.Text.Trim()
                                    && x.ProjectId == this.CurrUser.ProjectId
                                    && x.InstallationId.ToString() == node.Parent.Value
                                    && x.BSU_ID == node.Parent.Parent.Value
                                    select x;
                    if (pointInfo.Count() > 0 && pointInfo.First().PW_PointDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = string.Format("{0:yyyy-MM-dd}", pointInfo.First().PW_PointDate);
                        newNode.Value = string.Format("{0:yyyy-MM-dd}", pointInfo.First().PW_PointDate);
                        nodes.Add(newNode);
                    }
                }

                if (node.Depth == 3)
                {
                    var pointInfo = from x in BLL.Funs.DB.BO_Point
                                    where x.PW_PointNo == txtSearchCode.Text.Trim()
                                    && x.ProjectId == this.CurrUser.ProjectId
                                    && x.InstallationId.ToString() == node.Parent.Parent.Value
                                    && x.BSU_ID == node.Parent.Parent.Parent.Value
                                    select x;
                    if (pointInfo.Count() > 0 && pointInfo.First().PW_PointDate != null)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Value = pointInfo.First().PW_PointID;
                        newNode.Text = pointInfo.First().PW_PointNo;
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

        #region 增加按扭
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CurrUser.Account == BLL.Const.AdminId || ButtonList.Contains(BLL.Const.BtnAdd))
            {
                this.TextIsReadOnly(false);
                this.ButtonIsEnabled(true);

                this.PW_PointID = null;
                drpUnit.Items.Clear();
                drpTabler.Items.Clear();
                ddlInstallationId.Items.Clear();
                jointInfos.Clear();
                jointInfos = new List<Model.PW_JointInfo>();

                Funs.PleaseSelect(this.drpUnit);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit == null || unit.UnitType == "1" || unit.UnitType == "4")
                {
                    if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                    {
                        this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameBySupervisorUnitIdList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    }
                    else
                    {
                        this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    }
                }
                else
                {
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(this.drpTabler);
                this.drpTabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                this.gvPoint.Visible = false;
                this.txtPointID.Text = string.Empty;
                this.txtRemark.Text = string.Empty;
                

                this.drpTabler.SelectedValue = this.CurrUser.UserId;
                this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtPointDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                if (BLL.SysSetService.IsAuto(2, this.CurrUser.ProjectId) == true)
                {
                    string date = this.txtPointDate.Text.Trim().Replace("-", "");
                    this.txtPointID.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.BO_Point", "PW_PointNo",this.CurrUser.ProjectId, date + "-");
                }
                else
                {
                    this.txtPointID.ReadOnly = false;
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

            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                string pointID = this.tvControlItem.SelectedNode.Value;
                var q = BLL.PointManageService.GetPointByPointID(pointID);
                string installation = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Text;
                string unitname = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Parent.Text;
                string projectName = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;

                string varValue = String.Empty;
                varValue = installation.Replace("/", ",") + "|" + unitname + "|" + projectName.Replace("/", ",") + "|" + q.PW_PointDate.Value.ToString("yyyy-MM-dd") + "|" + q.PW_PointNo.Replace("/", ",");

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>PointInfoPrint('" + BLL.Const.PointReportDayReportId + "','" + pointID + "','" + varValue + "');</script>");
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择一条管线！')", true);
                return;
            }
        }

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(PW_PointID))
                {
                    List<Model.PW_JointInfo> info = BLL.PW_JointInfoService.GetJointInfosByPointID(PW_PointID);
                    bool isTrust = false;
                    foreach (var item in info)
                    {
                        if (!String.IsNullOrEmpty(item.JOT_TrustFlag) && item.JOT_TrustFlag != "00")
                        {
                            isTrust = true;
                            break;
                        }
                    }
                    if (isTrust == false)
                    {
                        foreach (var item in info)
                        {
                            item.PW_PointID = null;
                            item.JOT_JointStatus = "100";
                            BLL.PW_JointInfoService.UpdateJointInfo(item);
                        }
                        BLL.PointManageService.DeletePoint(PW_PointID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除点口管理");

                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！');", true);
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>PointSearch();</script>");
                        this.txtPointID.Text = string.Empty;
                        this.drpUnit.SelectedValue = "0";
                        this.txtPointDate.Text = string.Empty;
                        this.drpTabler.SelectedValue = "0";
                        this.txtTableDate.Text = string.Empty;
                        this.txtRemark.Text = string.Empty;
                        this.gvPoint.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('已委托，不能删除！');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的点口记录！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 查找按钮
        /// <summary>
        /// 查找按钮
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
            else if (this.ddlInstallationId.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择装置！')", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpUnit.SelectedValue + "',"+this.ddlInstallationId.SelectedValue+");</script>");
            }
        }
        #endregion

        #region 查找按钮
        /// <summary>
        /// 查找返回值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            jointInfos.Clear();
            string selectList = this.hdSelectList.Value;
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();
                if (PW_PointID == null)
                {
                    foreach (var item in infos)
                    {
                        Model.PW_JointInfo info = BLL.PW_JointInfoService.GetJointInfoByJotID(item);
                        jointInfos.Add(info);
                    }
                }

                else
                {
                    this.SaveList();
                    foreach (var jot in infos)
                    {
                        Model.PW_JointInfo info = BLL.PW_JointInfoService.GetJointInfoByJotID(jot);

                        if (jointInfos.Where(y => y.JOT_ID == jot).Count() == 0)
                        {
                            jointInfos.Add(info);
                        }
                    }
                }
                jointInfos = (from x in jointInfos join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID orderby y.ISO_IsoNo, x.JOT_JointNo select x).ToList();
                
                if (jointInfos.Count>0)
                {
                    this.gvPoint.Visible = true;
                    this.gvPoint.DataSource = jointInfos;
                    this.gvPoint.DataBind();
                }
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave)||this.CurrUser.Account==BLL.Const.AdminId)
            {
                if (this.gvPoint.Rows.Count>0)
                {
                    SaveList();
                    if (string.IsNullOrEmpty(this.txtPointDate.Text))
                    {
                         ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('点口日期不能为空！')", true);
                        return;
                    }
                    Model.BO_Point point = new Model.BO_Point();
                    point.PW_PointNo = this.txtPointID.Text.Trim();
                    point.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpUnit.SelectedValue!="0")
                    {
                        point.BSU_ID = this.drpUnit.SelectedValue;
                    }
                    if (this.ddlInstallationId.SelectedValue!="0")
                    {
                        point.InstallationId =Convert.ToInt32(this.ddlInstallationId.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(this.txtPointDate.Text))
                    {
                        point.PW_PointDate = Convert.ToDateTime(this.txtPointDate.Text);
                    }
                    if (this.drpTabler.SelectedValue!="0")
                    {
                        point.PW_Tabler = this.drpTabler.SelectedValue;
                    }
                    if (!string.IsNullOrEmpty(this.txtTableDate.Text))
                    {
                        point.PW_TablerDate = Convert.ToDateTime(this.txtTableDate.Text);
                    }
                    point.PW_Remark = this.txtRemark.Text.Trim();

                    if (!string.IsNullOrEmpty(PW_PointID))
                    {
                        point.PW_PointID = PW_PointID;
                        BLL.PointManageService.UpdatePoint(point);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改点口信息！");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtPointID.Text.Trim()))
                        {

                            if (BLL.SysSetService.IsAuto(2, this.CurrUser.ProjectId) == true)
                            {
                                string date = this.txtPointDate.Text.Trim().Replace("-", "");
                                point.PW_PointNo = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.BO_Point", "PW_PointNo", this.CurrUser.ProjectId, date + "-");
                            }
                            else
                            {
                                if (!BLL.PointManageService.IsExistPointNO(this.txtPointID.Text))
                                {
                                    point.PW_PointNo = this.txtPointID.Text.Trim();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('点口编号已存在，请重新录入！')", true);
                                    return;
                                }
                            }
                            

                            point.PW_PointID = SQLHelper.GetNewID(typeof(Model.BO_Point));
                            BLL.PointManageService.AddPoint(point);
                            PW_PointID = point.PW_PointID;
                            BLL.LogService.AddLog(this.CurrUser.UserId, "添加点口信息！");
                        }
                    }

                    foreach (var item in jointInfos)
                    {
                        item.PW_PointID = point.PW_PointID;
                        BLL.PW_JointInfoService.UpdateJointPoint(item);
                    }

                    jointInfos.Clear();
                    this.gvPoint.DataSource = null;
                    this.gvPoint.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>PointSearch();</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口信息不能为空！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请联系管理员！')", true);
            }
        }

        /// <summary>
        /// 保存焊口信息
        /// </summary>
        protected void SaveList()
        {
            jointInfos.Clear();

            for (int i = 0; i < this.gvPoint.Rows.Count; i++)
            {
                TextBox txtJOT_JointNo = (TextBox)(this.gvPoint.Rows[i].FindControl("txtJOT_JointNo"));
                HiddenField hdJOT_CellWelder = (HiddenField)(this.gvPoint.Rows[i].FindControl("hdJOT_CellWelder"));               
                HiddenField hdJOT_ID = (HiddenField)(this.gvPoint.Rows[i].FindControl("hdJOT_ID"));

                var isoId = BLL.PW_JointInfoService.GetJointInfoByJotID(hdJOT_ID.Value).ISO_ID;
                Model.PW_JointInfo info = new Model.PW_JointInfo();

                info.ISO_ID = isoId;
                info.JOT_ID = hdJOT_ID.Value;
                info.JOT_JointNo = txtJOT_JointNo.Text.Trim();
                info.JOT_CellWelder = hdJOT_CellWelder.Value;
                info.JOT_JointStatus = "101";               
                
                jointInfos.Add(info);
            }
        }
        #endregion

        #region 点击Treeview
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
                PW_PointID = this.tvControlItem.SelectedValue;
                jointInfos = new List<Model.PW_JointInfo>();
                drpUnit.Items.Clear();
                ddlInstallationId.Items.Clear();
                drpTabler.Items.Clear();

                Funs.PleaseSelect(this.drpUnit);
                Funs.PleaseSelect(this.ddlInstallationId);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit == null || unit.UnitType == "1" || unit.UnitType == "4")
                {
                    if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                    {
                        this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameBySupervisorUnitIdList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    }
                    else
                    {
                        this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    }
                }
                else
                {
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                Funs.PleaseSelect(drpTabler);
                this.drpTabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                if (!string.IsNullOrEmpty(PW_PointID))
                {
                    Model.BO_Point point = BLL.PointManageService.GetPointByPointID(PW_PointID);
                    this.txtPointID.Text = point.PW_PointNo;
                    if (!string.IsNullOrEmpty(point.BSU_ID))
                    {
                        this.drpUnit.SelectedValue = point.BSU_ID;
                    }
                    if (point.InstallationId != 0)
                    {
                        var drpInstall = BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.drpUnit.SelectedValue);
                        if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                        {
                            drpInstall = BLL.InstallationService.GetInstallationBySupervisorUnitIdList(this.CurrUser.ProjectId, this.drpUnit.SelectedValue, this.CurrUser.UnitId);

                        }
                        this.ddlInstallationId.Items.AddRange(drpInstall);
                        var isExitInstall = drpInstall.FirstOrDefault(x => x.Value == point.InstallationId.ToString());
                        if (isExitInstall != null)
                        {
                            this.ddlInstallationId.SelectedValue = Convert.ToString(point.InstallationId);
                        }
                        else
                        {
                            this.ddlInstallationId.SelectedValue = "0";
                        }
                    }
                    if (point.PW_PointDate != null)
                    {
                        this.txtPointDate.Text = string.Format("{0:yyyy-MM-dd}", point.PW_PointDate);
                    }
                    if (!string.IsNullOrEmpty(point.PW_Tabler))
                    {
                        this.drpTabler.SelectedValue = point.PW_Tabler;
                    }
                    if (point.PW_TablerDate != null)
                    {
                        this.txtTableDate.Text = string.Format("{0:yyyy-MM-dd}", point.PW_TablerDate);
                    }
                    this.txtRemark.Text = point.PW_Remark;
                    jointInfos = BLL.PW_JointInfoService.GetJointInfosByPointID(PW_PointID);
                    jointInfos = (from x in jointInfos join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID orderby y.ISO_IsoNo, x.JOT_JointNo select x).ToList();
                    if (jointInfos.Count > 0)
                    {
                        this.gvPoint.Visible = true;
                        this.gvPoint.DataSource = jointInfos;
                        this.gvPoint.DataBind();
                    }
                }
            }
            else
            {
                PW_PointID = null;
                this.gvPoint.Visible = false;
            }
        }
        #endregion

        #region 获取值
        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveList();
        }

        protected void imgBtnAll3_Click(object sender, ImageClickEventArgs e)
        {
            SaveList();
            this.gvPoint.DataSource = jointInfos;
            this.gvPoint.DataBind();
        }

        /// <summary>
        /// 查管线所属的区域
        /// </summary>
        /// <param name="ISO_ID"></param>
        /// <returns></returns>
        protected string GetBAW_ID(object ISO_ID)
        {
            if (ISO_ID != null)
            {
                return BLL.WorkAreaService.getWorkAreaByWorkAreaId(BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString()).BAW_ID).WorkAreaCode;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取管线编号
        /// </summary>
        /// <param name="ISO_ID"></param>
        /// <returns></returns>
        protected string GetISO_IsoNo(object ISO_ID)
        {
            if (ISO_ID != null)
            {
                return BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString()).ISO_IsoNo;
            }
            else
            {
                return "";
            }
        }
       
        /// <summary>
        /// 获取焊工号
        /// </summary>
        /// <param name="JOT_CellWelder"></param>
        /// <returns></returns>
        protected string GetPersonNameByJOT_CellWelder(object JOT_CellWelder)
        {
            if (JOT_CellWelder != null)
            {
                return BLL.PersonManageService.GetBSWelderByTeamWEDID(JOT_CellWelder.ToString()).WED_Code;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPoint_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SaveList();
            string JOT_ID = e.CommandArgument.ToString();
            if (e.CommandName=="del")
            {
                foreach (Model.PW_JointInfo info in jointInfos)
                {
                    if (info.JOT_ID==JOT_ID)
                    {
                        var i = BLL.TrustManageEditService.GetCH_TrustItemByJOT_ID(JOT_ID);
                        if (i.Count() == 0)
                        {
                            jointInfos.Remove(info);
                            Model.PW_JointInfo joint = BLL.PW_JointInfoService.GetJointInfoByJotID(JOT_ID);
                            joint.PW_PointID = null;
                            joint.JOT_JointStatus = "100";
                            BLL.PW_JointInfoService.UpdateJointInfo(joint);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此焊口已委托，不能删除！');", true);
                        }
                        break;
                    }
                }
            }
            this.gvPoint.DataSource = jointInfos;
            this.gvPoint.DataBind();
        }
        #endregion

        #region GreeView绑定行
        /// <summary>
        /// 绑定行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// 生成委托单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerate_Click(object sender, ImageClickEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.PW_PointID))
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowTrustManageSet('" + this.PW_PointID + "');</script>");
            }
        }

        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlInstallationId.Items.Clear();
            Funs.PleaseSelect(this.ddlInstallationId);
            if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
            {
                this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationBySupervisorUnitIdList(this.CurrUser.ProjectId, this.drpUnit.SelectedValue, this.CurrUser.UnitId));
            }
            else
            {
                this.ddlInstallationId.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.drpUnit.SelectedValue));
            }
        }

        protected void txtPointDate_TextChanged(object sender, EventArgs e)
        {
            if (BLL.SysSetService.IsAuto(2, this.CurrUser.ProjectId) == true)
            {
                if (!string.IsNullOrEmpty(this.txtPointDate.Text.Trim()))
                {
                    string date = this.txtPointDate.Text.Trim().Replace("-", "");
                    this.txtPointID.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.BO_Point", "PW_PointNo", this.CurrUser.ProjectId, date + "-");
                }
            }
        }

        protected void drpSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSearch.SelectedItem.Text == "按月份")
            {
                this.txtPointManageDate.Visible = true;
                txtSearchCode.Visible = false;
                txtSearchCode.Text = string.Empty;
                this.txtPointManageDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
            }
            else
            {
                txtPointManageDate.Visible = false;
                txtSearchCode.Visible = true;
            }
        }
    }
}