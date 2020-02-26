using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.CheckManage
{
    public partial class ShowTrust : PPage
    {
        /// <summary>
        /// 单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["CH_TrustUnit"];
            }
            set
            {
                ViewState["CH_TrustUnit"] = value;
            }
        }
        /// <summary>
        /// 装置ID
        /// </summary>
        public int InstallationId
        {
            get
            {
                return (int)ViewState["InstallationId"];
            }
            set
            {
                ViewState["InstallationId"] = value;
            }
        }

        /// <summary>
        /// 委托单主键
        /// </summary>
        public string Trust_ID
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
        /// 被选择项列表
        /// </summary>
        public string SelectedList
        {
            get
            {
                return (string)ViewState["SelectedList"];
            }
            set
            {
                ViewState["SelectedList"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                UnitId = Request.Params["unitId"];
                InstallationId =Convert.ToInt32(Request.Params["InstallationId"].ToString());
             
                Funs.PleaseSelect(this.drpWorkArea);
                this.drpWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByInstallUnit(this.CurrUser.ProjectId, this.InstallationId.ToString(), this.UnitId));

                if (!String.IsNullOrEmpty(Request.Params["ch_TrustID"]))
                {
                    Trust_ID = Request.Params["ch_TrustID"].ToString();
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(Trust_ID);
                    if(trust != null)
                    {
                        this.txtTrust_ID.Text = trust.CH_TrustCode;
                    }
                    this.drpWorkArea.Enabled = false;
                    this.txtTrust_ID.Enabled = false;
                }
                else
                {
                    this.drpWorkArea.Enabled = true;
                    this.txtTrust_ID.Enabled = true;
                }
                this.tvControlItem.Visible = false;
                this.gvPW_JointInfo.Visible = false;
            }
        }
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["trustId"] = Trust_ID;
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPW_JointInfo_DataBound(object sender, EventArgs e)
        {
            if (this.gvPW_JointInfo.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvPW_JointInfo.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPW_JointInfo;
        }

        private List<Model.View_CH_CheckSerch> viewCheckSerchs = new List<Model.View_CH_CheckSerch>();
        /// <summary>
        /// 查找按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            viewCheckSerchs.Clear();
            this.tvControlItem.Visible = true;
            this.gvPW_JointInfo.Visible = false;
            this.tvControlItem.Nodes.Clear();
            viewCheckSerchs = (from x in Funs.DB.View_CH_CheckSerch where x.ProjectId == this.CurrUser.ProjectId && x.BSU_ID == UnitId select x).ToList();
            if (this.drpWorkArea.SelectedValue!="0")
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Text = this.drpWorkArea.SelectedItem.Text;
                rootNode.Value = this.drpWorkArea.SelectedValue;
                rootNode.Expanded = true;
                viewCheckSerchs = viewCheckSerchs.Where(x => x.WorkAreaId == this.drpWorkArea.SelectedValue).ToList();                
                this.tvControlItem.Nodes.Add(rootNode);
                this.GetNodes(rootNode.ChildNodes, rootNode.Value);
            }
            else
            {
                var workAreas = (from x in Funs.DB.Base_WorkArea where x.UnitId == UnitId && x.InstallationId == InstallationId select x).ToList();
                var workAreasIds = workAreas.Select(x=>x.WorkAreaId);
                viewCheckSerchs = viewCheckSerchs.Where(x => workAreasIds.Contains(x.WorkAreaId)).ToList();                
                foreach (var q in workAreas)
                {                    
                    if (viewCheckSerchs.Count() > 0)
                    {
                        var isExWork = Funs.DB.View_CH_CheckSerch.FirstOrDefault(x => x.WorkAreaId == q.WorkAreaId);
                        if (isExWork != null)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Text = q.WorkAreaCode;
                            tn.Value = q.WorkAreaId;
                            this.tvControlItem.Nodes.Add(tn);
                            this.GetNodes(tn.ChildNodes, tn.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 遍历节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            if (parentId != null)
            {                
                if (this.CurrUser.UnitId != null && BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "3")
                {   //显示和检测单位相关的单子
                    viewCheckSerchs = viewCheckSerchs.Where(x => x.CH_CheckUnit == this.CurrUser.UnitId).ToList();
                }

                if (!string.IsNullOrEmpty(this.txtTrust_ID.Text.Trim()))
                {
                    viewCheckSerchs = viewCheckSerchs.Where(x => x.CH_TrustCode.Contains(this.txtTrust_ID.Text.Trim())).ToList();
                }

                if (!String.IsNullOrEmpty(Trust_ID)) // 直接查找委托单明细
                {
                    var trust = BLL.TrustManageEditService.GetCH_TrustByID(Trust_ID);
                    TreeNode newNode = new TreeNode();
                    newNode.Text = trust.CH_TrustCode;
                    newNode.Value = trust.CH_TrustID;
                    nodes.Add(newNode);
                }
                else
                {
                    var trustIds = (from x in viewCheckSerchs
                                    where x.WorkAreaId == parentId
                                    select new { x.CH_TrustID, x.CH_TrustCode }).Distinct();
                    if (trustIds != null)
                    {

                        foreach (var trustId in trustIds)
                        {
                            // 判断这个委托单是不是在检测单中存在，如果存在则不加载
                            int num = BLL.CheckManageService.GetCheckByTrustId(trustId.CH_TrustID);
                            if (num == 0)
                            {
                                TreeNode newNode = new TreeNode();
                                newNode.Text = trustId.CH_TrustCode;
                                newNode.Value = trustId.CH_TrustID;
                                nodes.Add(newNode);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbAll = (CheckBox)(this.gvPW_JointInfo.HeaderRow.FindControl("ckbAll"));
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                ((CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbJot_ID"))).Checked = ckbAll.Checked;
                
            }
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode.Depth == 1)
            {
                this.tvControlItem.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;
                int rowsCount = this.gvPW_JointInfo.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    CheckBox ckbJOT_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbJOT_ID"));
                    if (ckbJOT_ID.Checked == true)
                    {
                        Label lblJOT_ID = (Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblJOT_ID"));
                        if (!string.IsNullOrEmpty(SelectedList))
                        {
                            if (!SelectedList.Contains(lblJOT_ID.Text))
                            {
                                SelectedList += lblJOT_ID.Text + ",";
                            }
                        }
                        else
                        {
                            SelectedList += lblJOT_ID.Text + ",";
                        }
                    }

                }
            }
            this.gvPW_JointInfo.Visible = true;
            Trust_ID = this.tvControlItem.SelectedNode.Value;
            this.gvPW_JointInfo.DataBind();
        }

        /// <summary>
        /// 改变页数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPW_JointInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbJOT_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbJOT_ID"));
                if (ckbJOT_ID.Checked==true)
                {
                    Label lblJOT_ID = (Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblJOT_ID"));
                    if (!string.IsNullOrEmpty(SelectedList))
                    {
                        if (!SelectedList.Contains(lblJOT_ID.Text))
                        {
                            SelectedList += lblJOT_ID.Text + ",";
                        }
                    }
                    else
                    {
                        SelectedList += lblJOT_ID.Text + ",";
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbJOT_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbJOT_ID"));
                if (ckbJOT_ID.Checked==true)
                {
                    Label lblJOT_ID = (Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblJOT_ID"));
                    if (!string.IsNullOrEmpty(SelectedList))
                    {
                        if (!SelectedList.Contains(lblJOT_ID.Text))
                        {
                            SelectedList += lblJOT_ID.Text + ",";
                        }
                    }
                    else
                    {
                        SelectedList += lblJOT_ID.Text + ",";
                    }
                }
            }
            if (!string.IsNullOrEmpty(SelectedList))
            {
                SelectedList = SelectedList.Substring(0, SelectedList.LastIndexOf(","));
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type ='text/javascript'>ShowWorkStageClose('" + SelectedList + "');</script>");
                SelectedList = string.Empty;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择委托单号再保存！')", true);
                return;
            }
        }
    }
}