using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.HotProessManage
{
    public partial class ShowHotProessSearch : PPage
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }

        /// <summary>
        /// 管线号
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
        /// 被选择项列表
        /// </summary>
        public string InstallationId
        {
            get
            {
                return (string)ViewState["InstallationId"];
            }
            set
            {
                ViewState["InstallationId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                UnitId = Request.Params["unitId"];
                InstallationId = Request.Params["installationId"];                
                Funs.PleaseSelect(this.drpWorkArea);
                this.drpWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByInstallUnit(this.CurrUser.ProjectId, this.InstallationId, this.UnitId));
                this.tvControlItem.Visible = false;
                this.gvPW_JointInfo.Visible = false;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["iso_id"] = ISO_ID;
        }

        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.drpWorkArea.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择区域！')", true);
                return;
            }
            this.tvControlItem.Visible = true;
            this.gvPW_JointInfo.Visible = false;
            this.tvControlItem.Nodes.Clear();

            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(this.drpWorkArea.SelectedValue).WorkAreaCode;
            rootNode.Value = this.drpWorkArea.SelectedValue;
            rootNode.Expanded = true;

            this.tvControlItem.Nodes.Add(rootNode);
            this.GetNodes(rootNode.ChildNodes, rootNode.Value);          
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            if (parentId != null)
            {
                List<Model.PW_IsoInfo> isoInfos = new List<Model.PW_IsoInfo>();
                if (!string.IsNullOrEmpty(this.txtISO_ID.Text.Trim()))
                {
                    isoInfos = (from x in BLL.Funs.DB.PW_IsoInfo
                                where x.ProjectId == this.CurrUser.ProjectId
                                    && x.BSU_ID == UnitId && x.BAW_ID == parentId && x.ISO_IsoNo.Contains(this.txtISO_ID.Text.Trim())
                                orderby x.ISO_IsoNo
                                select x).ToList();
                }
                else
                {
                    isoInfos = (from x in BLL.Funs.DB.PW_IsoInfo
                                where x.ProjectId == this.CurrUser.ProjectId
                                    && x.BSU_ID == UnitId && x.BAW_ID == parentId
                                orderby x.ISO_IsoNo
                                select x).ToList();
                }

                foreach (var q in isoInfos)
                {
                    var hot = from x in Funs.DB.PW_JointInfo
                              where x.ISO_ID == q.ISO_ID
                                  && x.JOT_HotRpt == null && x.DReportID != null
                                  && x.IS_Proess == "1"
                              select x;
                    if (hot.Count() > 0)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.ISO_IsoNo;
                        newNode.Value = q.ISO_ID.ToString();
                        nodes.Add(newNode);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 在控件被绑定后激发
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
                ((CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbJOT_ID"))).Checked = ckbAll.Checked;
            }
        }

        protected string ConvertIsoNo(object ISO_ID)
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
                this.gvPW_JointInfo.Visible = true;
                ISO_ID = this.tvControlItem.SelectedNode.Value;
                this.gvPW_JointInfo.DataBind();
            }
        }

        protected void gvPW_JointInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
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

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
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
            if (!string.IsNullOrEmpty(SelectedList))
            {
                SelectedList = SelectedList.Substring(0, SelectedList.LastIndexOf(","));
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowWorkStageClose('" + SelectedList + "');</script>");
                SelectedList = string.Empty;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择焊口再保存！')", true);
                return;
            }
        }
    }
}