using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.TestPackageManage
{
    public partial class ShowTestPackageSearch : PPage
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
        public string IsoNo
        {
            get
            {
                return (string)ViewState["IsoNo"];
            }
            set
            {
                ViewState["IsoNo"] = value;
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
            if (!IsPostBack)
            {
                this.UnitId = Request.Params["unitId"];
                string installationId = Request.Params["installationId"];
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

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["checkList"] = this.CheckList;
            e.InputParameters["isoNo"] = this.IsoNo;
        }

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
            }
            else
            {
                var workAreas = (from x in Funs.DB.Base_WorkArea where x.UnitId == UnitId && x.InstallationId.ToString() == InstallationId select x).ToList();
                foreach (var q in workAreas)
                {                    
                    TreeNode tn = new TreeNode();
                    tn.Text = q.WorkAreaCode;
                    tn.Value = q.WorkAreaId;                   
                    this.tvControlItem.Nodes.Add(tn);
                }
            }
        }

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
                ((CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbISO_ID"))).Checked = ckbAll.Checked;
            }
        }      

        /// <summary>
        /// 选中树节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            this.OnTreeNodeCheckChanged();
        }
        protected void tvControlItem_CheckedChanged(object sender, EventArgs e)
        {
            this.OnTreeNodeCheckChanged();
        }
        protected void OnTreeNodeCheckChanged()
        {
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbISO_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbISO_ID"));
                if (ckbISO_ID.Checked == true)
                {
                    Label lblISO_ID = (Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblISO_ID"));
                    if (!string.IsNullOrEmpty(SelectedList))
                    {
                        if (!SelectedList.Contains(lblISO_ID.Text))
                        {
                            SelectedList += lblISO_ID.Text + ",";
                        }
                    }
                    else
                    {
                        SelectedList += lblISO_ID.Text + ",";
                    }
                }
            }

            this.gvPW_JointInfo.Visible = true;
            this.GetCheckList();
            this.gvPW_JointInfo.DataBind();
        }
        /// <summary>
        /// 选择树节点
        /// </summary>
        protected void GetCheckList()
        {
            this.CheckList = null;
            string str = null;
            for (int i = 0; i < this.tvControlItem.Nodes.Count; i++)
            {
                if (this.tvControlItem.Nodes[i].Checked)
                {
                    if (String.IsNullOrEmpty(str))
                    {
                        str = this.tvControlItem.Nodes[i].Value;
                    }
                    else
                    {
                        str += "|" + this.tvControlItem.Nodes[i].Value;
                    }
                }
            }

            if (!string.IsNullOrEmpty(str))
            {
                this.CheckList = str.Split('|');
            }

            this.IsoNo = null;
            if (!String.IsNullOrEmpty(this.txtISO_ID.Text))
            {
                this.IsoNo = this.txtISO_ID.Text.Trim();
            }
        }

        protected void gvPW_JointInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbISO_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbISO_ID"));
                if (ckbISO_ID.Checked == true)
                {
                    Label lblISO_ID = (Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblISO_ID"));
                    if (!string.IsNullOrEmpty(SelectedList))
                    {
                        if (!SelectedList.Contains(lblISO_ID.Text))
                        {
                            SelectedList += lblISO_ID.Text + ",";
                        }
                    }
                    else
                    {
                        SelectedList += lblISO_ID.Text + ",";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            int rowsCount = this.gvPW_JointInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbISO_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbISO_ID"));
                if (ckbISO_ID.Checked == true)
                {
                    Label lblISO_ID = (Label)(this.gvPW_JointInfo.Rows[i].FindControl("lblISO_ID"));
                    if (!string.IsNullOrEmpty(SelectedList))
                    {
                        if (!SelectedList.Contains(lblISO_ID.Text))
                        {
                            SelectedList += lblISO_ID.Text + ",";
                        }
                    }
                    else
                    {
                        SelectedList += lblISO_ID.Text + ",";
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

        /// <summary>
        /// 合格等级转换
        /// </summary>
        /// <param name="ISO_NDTClass"></param>
        /// <returns></returns>
        //protected string ConvertISO_NDTClass(object ISO_NDTClass)
        //{
        //    if (!String.IsNullOrEmpty(ISO_NDTClass.ToString()))
        //    {
        //        return BLL.TrustManageEditService.GetAcceptGradeList().FirstOrDefault(x => x.Value == ISO_NDTClass.ToString()).Text;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }
}