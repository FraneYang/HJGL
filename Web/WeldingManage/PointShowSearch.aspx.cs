using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class PointShowSearch : PPage
    {
        #region 定义变量
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
        /// 管线ID
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
        #endregion

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UnitId = Request.Params["unitId"];
                InstallationId =Convert.ToInt32(Request.Params["InstallationId"].ToString());
                
                Funs.PleaseSelect(this.drpWorkArea);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                {
                    this.drpWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByInstallSupervisorUnit(this.CurrUser.ProjectId, this.InstallationId.ToString(), UnitId, this.CurrUser.UnitId));
                }
                else
                {
                    this.drpWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByInstallUnit(this.CurrUser.ProjectId, this.InstallationId.ToString(), UnitId));
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
            e.InputParameters["iso_id"] = ISO_ID;
            e.InputParameters["dreportId"] = null;//BLL.PW_JointInfoService.GetDeReportByJotID(ISO_ID);
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            TreeNode rootNode = new TreeNode();
            rootNode.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(this.drpWorkArea.SelectedValue).WorkAreaCode;
            rootNode.Value = this.drpWorkArea.SelectedValue;
            rootNode.Expanded = true;

            this.tvControlItem.Nodes.Add(rootNode);
            this.GetNodes(rootNode.ChildNodes, rootNode.Value);
        }

        /// <summary>
        /// 遍历节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="p"></param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            if (parentId != null)
            {

                var isoInfos = from x in Funs.DB.PW_JointInfo
                               where x.PW_PointID == null && x.DReportID != null && x.JOT_CellWelder != null && x.JOT_FloorWelder != null
                               && x.JOT_JointStatus == "100"
                               join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID
                               where y.BAW_ID == parentId
                               select new { x.ISO_ID, y.ISO_IsoNo };

                if (!string.IsNullOrEmpty(this.txtISO_ID.Text.Trim()))
                {
                    isoInfos = isoInfos.Where(x => x.ISO_IsoNo.Contains(this.txtISO_ID.Text.Trim()));
                }
                if (isoInfos.Count() > 0)
                {
                    isoInfos = isoInfos.Distinct();
                    foreach (var q in isoInfos)
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = q.ISO_IsoNo;
                        newNode.Value = q.ISO_ID.ToString();
                        nodes.Add(newNode);
                    }
                }
            }
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
        /// 把管线ID转换为字符串类型
        /// </summary>
        /// <param name="ISO_ID"></param>
        /// <returns></returns>
        protected string ConvertIsoNo(object ISO_ID)
        {
            if (ISO_ID!=null)
            {
                return BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString()).ISO_IsoNo;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 焊接日期
        /// </summary>
        /// <param name="DReportID"></param>
        /// <returns></returns>
        protected string ConertDReaportDate(object DReportID)
        {
            if (DReportID!=null)
            {
                return string.Format("{0:yyyy-MM-dd}",BLL.WeldReportService.GetWeldReportByDReportID(DReportID.ToString()).JOT_WeldDate);
            }
            else
            {
                return "";
            }
        }
     
        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            var num = from x in Funs.DB.PW_JointInfo where x.ISO_ID == this.tvControlItem.SelectedValue && x.PW_PointID != null select x;
            var gnum = from x in Funs.DB.PW_JointInfo where x.ISO_ID == this.tvControlItem.SelectedValue && x.PW_PointID != null && x.JOT_JointAttribute == "固定" select x;
            var hnum = from x in Funs.DB.PW_JointInfo where x.ISO_ID == this.tvControlItem.SelectedValue && x.PW_PointID != null && x.JOT_JointAttribute == "活动" select x;
            var total = from x in Funs.DB.PW_JointInfo where x.ISO_ID == this.tvControlItem.SelectedValue select x;
            int totalNum = total.Count();
            this.lblPointNum.Text = num.Count().ToString();
            this.lblPonitRate.Text = (((double)num.Count() / totalNum) * 100).ToString("##.0") + "%";

            this.lblGdk.Text = gnum.Count().ToString();
            this.lblGdkRate.Text = (((double)gnum.Count() / totalNum) * 100).ToString("##.0") + "%";

            this.lblHdk.Text = hnum.Count().ToString();
            this.lblHdkRate.Text = (((double)hnum.Count() / totalNum) * 100).ToString("##.0") + "%";

            if (this.tvControlItem.SelectedNode.Depth==1)
            {
                this.tvControlItem.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;
                int rowsCount = this.gvPW_JointInfo.Rows.Count;
                for(int i = 0; i < rowsCount; i++)
                {
                    CheckBox ckbJOT_ID = (CheckBox)(this.gvPW_JointInfo.Rows[i].FindControl("ckbJot_ID"));
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
                this.gvPW_JointInfo.Visible = true;
                ISO_ID = this.tvControlItem.SelectedNode.Value;
                this.gvPW_JointInfo.PageIndex = 0;
                this.gvPW_JointInfo.DataBind();
            }
        }

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
        /// 保存
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
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择焊口再保存！')", true);
                return;
            }
        }
    }
}