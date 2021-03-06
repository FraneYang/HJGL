﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.RepairManage
{
    public partial class ShowRepairSearch : PPage
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
        /// 选择列表
        /// </summary>
        public string[] CheckList
        {
            get
            {
                return (string[])ViewState["CheckList"];
            }
            set
            {
                ViewState["CheckList"] = value;
            }
        } 
        
        /// <summary>
        /// 选择列表
        /// </summary>
        public string[] jotList
        {
            get
            {
                return (string[])ViewState["jotList"];
            }
            set
            {
                ViewState["jotList"] = value;
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
        /// 装置id
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

        /// <summary>
        /// 委托id
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
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UnitId = Request.Params["unitId"];
                string installationId = Request.Params["installationId"];
                this.CH_TrustID = Request.Params["CH_TrustID"];
                InstallationId = installationId;

                var workAreas = (from x in Funs.DB.Base_WorkArea where x.UnitId == UnitId && x.InstallationId.ToString() == installationId select x).ToList();
                ListItem[] list = new ListItem[workAreas.Count()];
                for (int i = 0; i < workAreas.Count(); i++)
                {
                    list[i] = new ListItem(workAreas[i].WorkAreaCode ?? "", workAreas[i].WorkAreaId.ToString());
                }

                Funs.PleaseSelect(this.drpWorkArea);
                this.drpWorkArea.Items.AddRange(list);
                this.tvControlItem.Visible = false;
                this.gvPW_JointInfo.Visible = false;
            }

            tvControlItem.Attributes.Add("onclick", "postBackByObject()");

        }

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["checkList"] = this.CheckList;
            e.InputParameters["CH_TrustID"] = this.CH_TrustID;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["jotList"] = this.jotList;
            e.InputParameters["ckISO"] = this.ckISO.Checked;
            e.InputParameters["ckWeld"] = this.ckWeld.Checked;
            e.InputParameters["workAreaId"] = this.drpWorkArea.SelectedValue;
            e.InputParameters["unitId"] = this.UnitId;
        }
        #endregion

        #region 树查询事件
        /// <summary>
        /// 树查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.tvControlItem.Visible = true;
            this.gvPW_JointInfo.Visible = false;
            this.tvControlItem.Nodes.Clear();
            if (this.drpWorkArea.SelectedValue != "0")
            {
                TreeNode rootNode = new TreeNode();//定义根节点
                rootNode.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(this.drpWorkArea.SelectedValue).WorkAreaCode;
                rootNode.Value = this.drpWorkArea.SelectedValue;
                rootNode.Expanded = true;

                this.tvControlItem.Nodes.Add(rootNode);
                this.GetNodes(rootNode.ChildNodes, rootNode.Value);
            }
            else
            {
                var workAreas = (from x in Funs.DB.Base_WorkArea 
                                 where x.UnitId == UnitId && x.InstallationId.ToString() == InstallationId select x).ToList();
                foreach (var q in workAreas)
                {
                    var trustSearch = from x in Funs.DB.View_CH_RepairSearch
                                      where x.ProjectId == this.CurrUser.ProjectId && x.BSU_ID == UnitId && x.WorkAreaId == q.WorkAreaId
                                      select x;
                    if (trustSearch.Count() > 0)
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
        #endregion

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
                var trustSearch = from x in Funs.DB.View_CH_RepairSearch
                                  where x.ProjectId == this.CurrUser.ProjectId && x.BSU_ID == UnitId
                                  && x.WorkAreaId == parentId && (x.RepairTrustId == null || x.RepairTrustId == this.CH_TrustID)
                                  select x;
                if (!string.IsNullOrEmpty(this.txtISO_ID.Text.Trim()))
                {
                    trustSearch = trustSearch.Where(x => x.ISO_IsoNo.Contains(this.txtISO_ID.Text.Trim()));
                }

                var checkID = (from x in trustSearch select x.CHT_CheckID).Distinct();
                if (checkID.Count() > 0)
                {
                    foreach (var id in checkID)
                    {
                        var point = from x in Funs.DB.CH_Check
                                    where x.CHT_CheckID == id.ToString()
                                    select x;
                        foreach (var itm in point)
                        {
                            TreeNode newNode = new TreeNode();
                            newNode.Text = itm.CHT_CheckCode;
                            newNode.Value = itm.CHT_CheckID;

                            nodes.Add(newNode);
                        }
                    }
                }
            }
        }
        #endregion

        #region 控件被绑定后激发
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

        protected void gvSearch_DataBound(object sender, EventArgs e)
        {
            if (this.gvSearch.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvSearch.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvSearch;
        }
        #endregion

        #region gv全选事件
        /// <summary>
        /// gv1全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbAll_gvSearchCheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbAll = (CheckBox)(this.gvSearch.HeaderRow.FindControl("ckbAll"));
            int rowsCount = this.gvSearch.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                ((CheckBox)(this.gvSearch.Rows[i].FindControl("ckbJOT_ID"))).Checked = ckbAll.Checked;
            }
        }
        #endregion


        /// <summary>
        /// 选中树节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            var q = from x in Funs.DB.CH_Check
                    where x.CHT_CheckID == e.Node.Value
                    select x;
            if (q.Count() > 0)
            {
                this.OnTreeNodeCheckChanged();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择检测单！')", true);
            }
        }

       /// <summary>
       /// 树选择事件
       /// </summary>
        protected void OnTreeNodeCheckChanged()
        {
            this.gvPW_JointInfo.Visible = true;
            this.GetCheckList();
            this.gvPW_JointInfo.DataBind();
            this.GetCheckgvList();
        }

        /// <summary>
        /// 选择树节点
        /// </summary>
        protected void GetCheckList()
        {
            this.CheckList = null;
            string str = null;
            for (int j = 0; j < this.tvControlItem.Nodes.Count; j++)
            {
                for (int i = 0; i < this.tvControlItem.Nodes[j].ChildNodes.Count; i++)
                {
                    if (this.tvControlItem.Nodes[j].ChildNodes[i].Checked)
                    {
                        if (String.IsNullOrEmpty(str))
                        {
                            str = this.tvControlItem.Nodes[j].ChildNodes[i].Value;
                        }
                        else
                        {
                            str += "|" +this.tvControlItem.Nodes[j].ChildNodes[i].Value ;
                        }
                    }
                }               
            }

            if (!string.IsNullOrEmpty(str))
            {
                this.CheckList = str.Split('|');
            }
        }

        /// <summary>
        /// gvPW_JointInfo 选中某项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbJOT_CheckedChanged(object sender, EventArgs e)
        {
            this.GetCheckgvList();
        }

        /// <summary>
        ///  选择列表某项事件
        /// </summary>
        protected void GetCheckgvList()
        {
            this.jotList = null;
            string str = null;
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                if (String.IsNullOrEmpty(str))
                {

                    str = ((Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblJOT_ID"))).Text;
                }
                else
                {
                    str += "|" + ((Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblJOT_ID"))).Text;
                }
            }

            if (!string.IsNullOrEmpty(str))
            {
                this.jotList = str.Split('|');
            }

            this.gvSearch.Visible = true;
            this.gvSearch.DataBind();
        }
             

        #region 页面变化事件
        /// <summary>
        /// 页面变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int rowsCount = this.gvSearch.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbJOT_ID = (CheckBox)(this.gvSearch.Rows[i].FindControl("ckbJOT_ID"));
                if (ckbJOT_ID.Checked == true)
                {
                    Label lblJOT_ID = (Label)(this.gvSearch.Rows[i].FindControl("lblJOT_ID"));
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
        #endregion

        #region 确定按钮事件
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
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

            if (rowsCount > 0)
            {
                int rowsCount2 = this.gvSearch.Rows.Count;
                for (int i = 0; i < rowsCount2; i++)
                {
                    CheckBox ckbJOT_ID = (CheckBox)(this.gvSearch.Rows[i].FindControl("ckbJOT_ID"));
                    if (ckbJOT_ID.Checked == true)
                    {
                        Label lblJOT_ID = (Label)(this.gvSearch.Rows[i].FindControl("lblJOT_ID"));
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckISO_CheckedChanged(object sender, EventArgs e)
        {
            this.GetCheckgvList();
        }

        protected void ckWeld_CheckedChanged(object sender, EventArgs e)
        {
            this.GetCheckgvList();
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
    }
}