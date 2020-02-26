using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.WeldingManage
{
    public partial class JointInfo : PPage
    {
        #region 定义项
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

        public string Iso_No
        {
            get
            {
                return (string)ViewState["Iso_No"];
            }
            set
            {
                ViewState["Iso_No"] = value;
            }
        }

        public string WorkAreaId
        {
            get
            {
                return (string)ViewState["WorkAreaId"];
            }
            set
            {
                ViewState["WorkAreaId"] = value;
            }
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.InitTree(this.Page, this.tvControlItem);
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.JointInfoMenuId);
                CheckItemSetDataBind();
                
                this.divControlItemDetailDisplay.Visible = false;
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId,"2");
                if (c != null)
                {
                    this.GetShowColumn(c.Columns);
                }
            }
        }
        #endregion

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void CheckItemSetDataBind()
        {
            this.tvControlItem.Nodes.Clear();

            TreeNode rootNode = new TreeNode();
            rootNode.Text = "装置-单位-工作区";
            rootNode.Value = "0";
            rootNode.Expanded = true;

            this.tvControlItem.Nodes.Add(rootNode);
            this.GetNodes(rootNode.ChildNodes, null, null);
        }
        #endregion

        #region 遍历节点
        /// <summary>
        /// 遍历节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        /// <param name="node"></param>
        private void GetNodes(TreeNodeCollection nodes, string parentId, TreeNode node)
        {
            if (parentId == null)
            {
                List<Model.Base_Installation> installations = null;
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                { 
                    installations = (from x in BLL.Funs.DB.Base_Installation
                                     where x.ProjectId == this.CurrUser.ProjectId
                                     orderby x.InstallationId
                                     select x).ToList();
                }
                else
                {
                   installations = (from x in BLL.Funs.DB.Base_Installation
                                     join y in BLL.Funs.DB.Base_WorkArea on x.InstallationId equals y.InstallationId
                                     where x.ProjectId == this.CurrUser.ProjectId && y.UnitId == this.CurrUser.UnitId
                                     orderby x.InstallationId
                                     select x).Distinct().ToList();
                }

                foreach (var q in installations)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = q.InstallationName;
                    newNode.Value = q.InstallationId.ToString();
                    nodes.Add(newNode);
                }
            }
            else if (node.Depth == 1)
            {
                List<Model.Base_Unit> units = null;
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) == null || BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "1")
                { 
                    units = (from x in BLL.Funs.DB.Base_Unit
                             join y in BLL.Funs.DB.Base_WorkArea on x.UnitId equals y.UnitId
                             where x.ProjectId == this.CurrUser.ProjectId && y.InstallationId.ToString() == parentId && x.UnitType == "2"
                             orderby x.UnitCode
                             select x).Distinct().ToList();   
                }
                else
                {
                   units = (from x in BLL.Funs.DB.Base_Unit
                             join y in BLL.Funs.DB.Base_WorkArea on x.UnitId equals y.UnitId
                             where x.ProjectId == this.CurrUser.ProjectId && y.InstallationId.ToString() == parentId
                             && x.UnitId == this.CurrUser.UnitId && x.UnitType == "2"
                             orderby x.UnitCode
                             select x).Distinct().ToList();
                }
                foreach (var q in units)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = q.UnitName;
                    newNode.Value = q.UnitId.ToString();
                    nodes.Add(newNode);
                }
            }
            else if (node.Depth == 2)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = "temp";
                newNode.Value = "temp";
                nodes.Add(newNode);                
            }         
            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].ChildNodes, nodes[i].Value, nodes[i]);
            }
        }

        /// <summary>
        /// 节点展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            Model.HJGLDB db = BLL.Funs.DB;
            if (e.Node.Depth == 2)
            {
                 e.Node.ChildNodes.Clear();
                var workAreas = (from x in db.Base_WorkArea
                                 where x.ProjectId == this.CurrUser.ProjectId
                                 && x.InstallationId.ToString() == e.Node.Parent.Value && x.UnitId == e.Node.Value
                                 select x).Distinct();

                workAreas = workAreas.OrderByDescending(x => x.WorkAreaCode);
                foreach (var q in workAreas)
                {
                    int a = (from x in db.PW_IsoInfo
                             where x.ProjectId == this.CurrUser.ProjectId
                                 && x.BSU_ID == e.Node.Value
                                 && x.BAW_ID == q.WorkAreaId
                             select x).Count();

                    TreeNode newNode = new TreeNode();
                    newNode.Text = q.WorkAreaCode + "【" + a.ToString() + "】管线";
                    newNode.Value = q.WorkAreaId.ToString();
                    e.Node.ChildNodes.Add(newNode);

                    TreeNode temp = new TreeNode();
                    temp.Text = "temp";
                    temp.Value = "temp";

                    newNode.ChildNodes.Add(temp);
                }
            }
            if (e.Node.Depth == 3)
            {
                e.Node.ChildNodes.Clear();
                IEnumerable<Model.PW_IsoInfo> isoInfo = null;

                if (!String.IsNullOrEmpty(this.txtIsoNo.Text))
                {
                    isoInfo = (from x in db.PW_IsoInfo
                               join y in db.Base_WorkArea on x.BAW_ID equals y.WorkAreaId
                               where x.ProjectId == this.CurrUser.ProjectId && x.BAW_ID == e.Node.Value.Trim()
                               && x.ISO_IsoNo.Contains(this.txtIsoNo.Text.Trim())
                               select x).Distinct();
                }
                else
                {
                    isoInfo = (from x in db.PW_IsoInfo
                               join y in db.Base_WorkArea on x.BAW_ID equals y.WorkAreaId
                               where x.ProjectId == this.CurrUser.ProjectId && x.BAW_ID == e.Node.Value.Trim()
                               select x).Distinct();
                }

                isoInfo = isoInfo.OrderBy(u => u.ISO_IsoNo);

                foreach (var item in isoInfo)
                {
                    TreeNode newNode = new TreeNode();
                    var b = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(item.ISO_ID);
                    if (b != null && !String.IsNullOrEmpty(b.ISO_Sheet))
                    {
                        newNode.Text = item.ISO_IsoNo + "(" + b.ISO_Sheet + ")";
                    }
                    else
                    {
                        newNode.Text = item.ISO_IsoNo;
                    }
                    newNode.Value = item.ISO_ID;

                    e.Node.ChildNodes.Add(newNode);
                }
                
            }

        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AddDetail_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowAddJoint('" + this.hdISOID.Value + "','" + this.tvControlItem.SelectedNode.Parent.Value + "');</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择一条管线！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 绑定参数
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["workAreaId"] = WorkAreaId;
            e.InputParameters["jointNo"] = this.hdJointNo.Value;
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                e.InputParameters["iso_id"] = this.tvControlItem.SelectedNode.Value;
            }
            else
            {
                e.InputParameters["iso_id"] = string.Empty;
            }
            e.InputParameters["wlo_Code"] = this.hdWLO_Code.Value;
            e.InputParameters["jointDesc"] = this.hdJointDesc.Value;
            e.InputParameters["joty_id"] = this.hdJOTY_ID.Value;
            e.InputParameters["wme_id"] = this.hdWME_ID.Value;
            e.InputParameters["DReportID"] = this.hdDReportID.Value;
            e.InputParameters["PW_PointID"] = this.hdPW_PointID.Value;
        }
        #endregion

        #region GridView绑定
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointInfo_DataBound(object sender, EventArgs e)
        {
            if (this.gvJointInfo.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvJointInfo.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvJointInfo;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string jot_id = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                if (this.tvControlItem.SelectedNode != null)
                {
                    if (this.tvControlItem.SelectedNode.Depth == 4)
                    {
                        if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
                        {
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowModifyJoint('" + this.hdISOID.Value + "','" + this.tvControlItem.SelectedNode.Parent.Value + "','" + jot_id + "');</script>");

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                        }
                    }
                }
            }

            if (e.CommandName == "del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    Model.PW_JointInfo q = BLL.PW_JointInfoService.GetJointInfoByJotID(jot_id);
                    if (q != null && String.IsNullOrEmpty(q.DReportID))
                    {
                        BLL.PW_JointInfoService.DeleteJointInfo(jot_id);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊口信息");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
                        this.gvJointInfo.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('已焊接，不能删除！')", true);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }
        #endregion

        #region 选择节点时发生改变
        /// <summary>
        /// 选择节点时发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                this.ClearHdValue();
                this.Iso_No = tvControlItem.SelectedNode.Text;
                WorkAreaId = this.tvControlItem.SelectedNode.Parent.Value;
                this.divControlItemDetailDisplay.Visible = true;
                this.gvJointInfo.PageIndex = 0;
                this.gvJointInfo.DataBind();
                this.hdISOID.Value = this.tvControlItem.SelectedNode.Value;
                
                //this.tvControlItem.SelectedNodeStyle.BackColor=
            }
        }
        #endregion

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbAll = (CheckBox)(this.gvJointInfo.HeaderRow.FindControl("ckbAll"));
            int rowsCount = this.gvJointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                ((CheckBox)(this.gvJointInfo.Rows[i].FindControl("ckbJOT_ID"))).Checked = ckbAll.Checked;
            }
        }

        #region 转换字符串类型
        /// <summary>
        /// 转换字符串型
        /// </summary>
        /// <param name="isuse"></param>
        /// <returns></returns>     
        protected string ConvertString(object b)
        {
            if (b != null)
            {
                if (b.ToString() == "1")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            return "";
        }

        /// <summary>
        /// 转换委托字符串
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected string ConvertStringTrustFlag(object flag)
        {
            if (flag != null)
            {
                if (flag.ToString() == "00")
                {
                    return "未下委托";
                }
                else if (flag.ToString() == "01")
                {
                    return "一次委托,未审核";
                }
                else if (flag.ToString() == "02")
                {
                    return "一次委托,已审核";
                }
                else if (flag.ToString() == "11")
                {
                    return "二次委托,未审核";
                }
                else if (flag.ToString() == "12")
                {
                    return "二次委托,已审核";
                }
                else if (flag.ToString() == "21")
                {
                    return "三次委托,未审核";
                }
                else if (flag.ToString() == "22")
                {
                    return "三次委托,已审核";
                }
            }
            return "";
        }

        /// <summary>
        /// 转换探伤字符串
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected string ConvertStringCheckFlag(object flag)
        {
            if (flag != null)
            {
                if (flag.ToString() == "00")
                {
                    return "未检测";
                }
                else if (flag.ToString() == "01")
                {
                    return "一次检测,未审核";
                }
                else if (flag.ToString() == "02")
                {
                    return "一次检测,已审核";
                }
                else if (flag.ToString() == "11")
                {
                    return "二次检测,未审核";
                }
                else if (flag.ToString() == "12")
                {
                    return "二次检测,已审核";
                }
                else if (flag.ToString() == "21")
                {
                    return "三次检测,未审核";
                }
                else if (flag.ToString() == "22")
                {
                    return "三次检测,已审核";
                }
            }
            return "";
        }
        /// <summary>
        /// 转换焊口状态字符串
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string ConverStringJointStatus(object status)
        {
            if (status != null)
            {
                if (status.ToString() == "100")
                {
                    return "正常";
                }
                else if (status.ToString() == "102")
                {
                    return "扩透";
                }
                else if (status.ToString() == "101")
                {
                    return "点口";
                }
                else if (status.ToString() == "104")
                {
                    return "已切除";
                }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.hdSearch.Value))
            {
                this.ClearHdValue();

                List<string> list = this.hdSearch.Value.Split(',').ToList();
                foreach (var item in list)
                {
                    if (item.Contains("JOT_JointNo"))
                    {
                        this.hdJointNo.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("ISO_ID"))
                    {
                        this.hdISO_ID.Value = item.Split('|')[0];
                        this.Iso_No = item.Split('|')[0];
                    }
                    else if (item.Contains("WLO_Code"))
                    {
                        this.hdWLO_Code.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("JOT_JointDesc"))
                    {
                        this.hdJointDesc.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("JOTY_ID"))
                    {
                        this.hdJOTY_ID.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("WME_ID"))
                    {
                        this.hdWME_ID.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("DReportID"))
                    {
                        this.hdDReportID.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("PW_PointID"))
                    {
                        this.hdPW_PointID.Value = item.Split('|')[0];
                    }
                }
                this.gvJointInfo.DataBind();
            }
        }

        /// <summary>
        /// 自定义列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnColumn_Click(object sender, ImageClickEventArgs e)
        {
            string column = this.hdColumn.Value.Trim();
            this.GetShowColumn(column);
        }

        private void GetShowColumn(string column)
        {
            if (!string.IsNullOrEmpty(column))
            {
                this.gvJointInfo.Columns[2].Visible = false;
                this.gvJointInfo.Columns[3].Visible = false;
                this.gvJointInfo.Columns[4].Visible = false;
                this.gvJointInfo.Columns[5].Visible = false;
                this.gvJointInfo.Columns[6].Visible = false;
                this.gvJointInfo.Columns[7].Visible = false;
                this.gvJointInfo.Columns[8].Visible = false;
                this.gvJointInfo.Columns[9].Visible = false;
                this.gvJointInfo.Columns[10].Visible = false;
                this.gvJointInfo.Columns[11].Visible = false;
                this.gvJointInfo.Columns[12].Visible = false;
                this.gvJointInfo.Columns[13].Visible = false;
                this.gvJointInfo.Columns[14].Visible = false;
                this.gvJointInfo.Columns[15].Visible = false;
                this.gvJointInfo.Columns[16].Visible = false;
                this.gvJointInfo.Columns[17].Visible = false;
                this.gvJointInfo.Columns[18].Visible = false;
                this.gvJointInfo.Columns[19].Visible = false;
                this.gvJointInfo.Columns[20].Visible = false;
                this.gvJointInfo.Columns[21].Visible = false;
                this.gvJointInfo.Columns[22].Visible = false;
                this.gvJointInfo.Columns[23].Visible = false;
                this.gvJointInfo.Columns[24].Visible = false;
                this.gvJointInfo.Columns[25].Visible = false;
                this.gvJointInfo.Columns[26].Visible = false;
                this.gvJointInfo.Columns[27].Visible = false;
                this.gvJointInfo.Columns[28].Visible = false;
                this.gvJointInfo.Columns[29].Visible = false;
                this.gvJointInfo.Columns[30].Visible = false;
                this.gvJointInfo.Columns[31].Visible = false;
                this.gvJointInfo.Columns[32].Visible = false;
                this.gvJointInfo.Columns[33].Visible = false;
                this.gvJointInfo.Columns[34].Visible = false;
                this.gvJointInfo.Columns[35].Visible = false;
                this.gvJointInfo.Columns[36].Visible = false;
                this.gvJointInfo.Columns[37].Visible = false;
                this.gvJointInfo.Columns[38].Visible = false;
                this.gvJointInfo.Columns[39].Visible = false;
                this.gvJointInfo.Columns[40].Visible = false;
                this.gvJointInfo.Columns[41].Visible = false;
                this.gvJointInfo.Columns[42].Visible = false;
                this.gvJointInfo.Columns[43].Visible = false;
                this.gvJointInfo.Columns[44].Visible = false;
                this.gvJointInfo.Columns[45].Visible = false;
              
                List<string> columns = column.Split(',').ToList();
                int num = 1;
                foreach (var item in columns)
                {
                    this.gvJointInfo.Columns[Convert.ToInt32(item)].Visible = true;
                    num++;
                }

                if (num > 30)
                {
                    this.Table3.Width = "3810px";
                }
                else if (num <= 30 && num > 20)
                {
                    this.Table3.Width = "2500px";
                }
                else
                {
                    this.Table3.Width = "1500px";
                }

            }
        }

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                string isoId = this.tvControlItem.SelectedNode.Value;
                string installation = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Value;

                string varValue = String.Empty;
                varValue = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName.Replace("/", ",") + "|" + BLL.InstallationService.GetInstallationByInstallationId(installation).InstallationName.Replace("/", ",");

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>JointInfoPrint('" + BLL.Const.JointInfoReportId + "','" + isoId + "','" + varValue + "');</script>");
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择一条管线！')", true);
                return;
            }
        }

        /// <summary>
        /// 批量增加焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_BatchAddDetail_Click(object sender, ImageClickEventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowBatchAddJoint('" + this.hdISOID.Value + "','" + this.tvControlItem.SelectedNode.Parent.Value + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择一条管线！')", true);
                return;
            }
        }

        /// <summary>
        /// 重新加载GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgDetail_Click(object sender, ImageClickEventArgs e)
        {
            this.gvJointInfo.DataBind();
        }

        /// <summary>
        /// 导出焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnOut) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.gvJointInfo.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
                int num = this.gvJointInfo.Columns.Count;
                this.gvJointInfo.Columns[num-1].Visible = false;
                //foreach (GridViewRow dg in this.gvJointInfo.Rows)
                //{
                //    dg.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
                //    dg.Cells[7].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
                //}
                DateTime dt = DateTime.Now;
                string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

                Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("焊口信息表" + filename, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                this.gvJointInfo.RenderControl(oHtmlTextWriter);
                Response.Write(oStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        /// <summary>
        /// 管线查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgReportSearch_Click(object sender, ImageClickEventArgs e)
        {
            CheckItemSetDataBind();
            if (!String.IsNullOrEmpty(this.txtIsoNo.Text))
            {
                this.tvControlItem.ExpandAll();
            }
        }

        private void ClearHdValue()
        {
            this.hdJointNo.Value = string.Empty;
            this.hdISO_ID.Value = string.Empty;
            this.hdWLO_Code.Value = string.Empty;
            this.hdJointDesc.Value = string.Empty;
            this.hdJOTY_ID.Value = string.Empty;
            this.hdWME_ID.Value = string.Empty;
            this.Iso_No = string.Empty;
            this.hdDReportID.Value = string.Empty;
            this.hdPW_PointID.Value = string.Empty;
            this.WorkAreaId = string.Empty;
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.HotProessManageEditService.GetHotProessByJotId(this.hdJointNo.Value) > 0)
            {
                content = "热处理已经使用了该焊口，不能删除！";
            }
            if (content == "")
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + content + "')", true);
                return false;
            }
        }

        /// <summary>
        /// 删除焊口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                int rowsCount = this.gvJointInfo.Rows.Count;
                int tatol = 0;
                string jotRes = string.Empty;

                for (int i = 0; i < rowsCount; i++)
                {
                    CheckBox ckbJOT_ID = (CheckBox)(this.gvJointInfo.Rows[i].FindControl("ckbJOT_ID"));
                    if (ckbJOT_ID.Checked == true)
                    {
                        tatol += 1;
                        HiddenField hdJOT_ID = (HiddenField)(this.gvJointInfo.Rows[i].FindControl("hdJOT_ID"));
                        Model.PW_JointInfo q = BLL.PW_JointInfoService.GetJointInfoByJotID(hdJOT_ID.Value);
                        if (q != null)
                        {
                            if (String.IsNullOrEmpty(q.DReportID))
                            {
                                BLL.PW_JointInfoService.DeleteJointInfo(hdJOT_ID.Value);
                                BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊口信息");                                
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(jotRes))
                                {
                                    jotRes = q.JOT_JointNo;
                                }
                                else
                                {
                                    jotRes += "," + q.JOT_JointNo;
                                }
                            }

                        }
                    }
                }
                if (tatol == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的焊口！')", true);
                }

                if (!string.IsNullOrEmpty(jotRes))
                {
                    this.gvJointInfo.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口" + jotRes + "已存在焊接日报！')", true);
                }
                else
                {
                    this.gvJointInfo.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }            
    }
}