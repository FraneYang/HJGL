using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class PipelineManage : PPage
    {
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
        /// 按钮权限列表
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
        /// 区域列表
        /// </summary>
        public string AreaWork
        {
            get
            {
                return (string)ViewState["AreaWork"];
            }
            set
            {
                ViewState["AreaWork"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {               
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PipelineManageMenuId);
                this.CheckItemSetDataBind();
                this.divControlItemDetailDisplay.Visible = false;
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId,"1");
                if (c != null)
                {
                    if (!string.IsNullOrEmpty(c.Columns))
                    {
                        this.gvIsoInfo.Columns[2].Visible = false;
                        this.gvIsoInfo.Columns[3].Visible = false;
                        this.gvIsoInfo.Columns[4].Visible = false;
                        this.gvIsoInfo.Columns[5].Visible = false;
                        this.gvIsoInfo.Columns[6].Visible = false;
                        this.gvIsoInfo.Columns[7].Visible = false;
                        this.gvIsoInfo.Columns[8].Visible = false;
                        this.gvIsoInfo.Columns[9].Visible = false;
                        this.gvIsoInfo.Columns[10].Visible = false;
                        this.gvIsoInfo.Columns[11].Visible = false;
                        this.gvIsoInfo.Columns[12].Visible = false;
                        this.gvIsoInfo.Columns[13].Visible = false;
                        this.gvIsoInfo.Columns[14].Visible = false;
                        this.gvIsoInfo.Columns[15].Visible = false;
                        this.gvIsoInfo.Columns[16].Visible = false;
                        this.gvIsoInfo.Columns[17].Visible = false;
                        this.gvIsoInfo.Columns[18].Visible = false;
                        this.gvIsoInfo.Columns[19].Visible = false;
                        this.gvIsoInfo.Columns[20].Visible = false;
                        this.gvIsoInfo.Columns[21].Visible = false;
                        this.gvIsoInfo.Columns[22].Visible = false;
                        this.gvIsoInfo.Columns[23].Visible = false;
                        this.gvIsoInfo.Columns[24].Visible = false;
                        this.gvIsoInfo.Columns[25].Visible = false;
                        this.gvIsoInfo.Columns[26].Visible = false;
                        this.gvIsoInfo.Columns[27].Visible = false;
                        this.gvIsoInfo.Columns[28].Visible = false;
                        this.gvIsoInfo.Columns[29].Visible = false;
                        this.gvIsoInfo.Columns[30].Visible = false;
                        this.gvIsoInfo.Columns[31].Visible = false;
                        this.gvIsoInfo.Columns[32].Visible = false;
                        this.gvIsoInfo.Columns[33].Visible = false;
                        this.gvIsoInfo.Columns[34].Visible = false;
                        this.gvIsoInfo.Columns[35].Visible = false;
                        List<string> columns = c.Columns.Split(',').ToList();
                        foreach (var item in columns)
                        {
                            this.gvIsoInfo.Columns[Convert.ToInt32(item)].Visible = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 参数赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["iSO_IsoNo"] = this.hdISO_IsoNo.Value;
            e.InputParameters["sER_ID"] = this.hdSER_ID.Value;
            e.InputParameters["nDT_ID"] = this.hdNDT_ID.Value;
            e.InputParameters["iSO_IsoNumber"] = this.hdISO_IsoNumber.Value;
            e.InputParameters["sTE_ID"] = this.hdSTE_ID.Value;
            e.InputParameters["iSO_Specification"] = this.hdISO_Specification.Value;
            e.InputParameters["workAreaId"] = AreaWork;

        }

        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvIsoInfo_DataBound(object sender, EventArgs e)
        {
            if (this.gvIsoInfo.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvIsoInfo.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvIsoInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvIsoInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ISO_ID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                if (tvControlItem.SelectedNode != null)
                {
                    if (tvControlItem.SelectedNode.Depth == 3)
                    {
                        if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
                        {
                            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowModifyPipeline('" + ISO_ID + "');</script>");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择施工区域！')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择施工区域！')", true);
                }
            }
            if (e.CommandName == "del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        var tP_IsoList = (from x in BLL.Funs.DB.TP_IsoList where x.ISO_ID == ISO_ID select x);
                        if (tP_IsoList.Count() > 0)
                        {
                            BLL.Funs.DB.TP_IsoList.DeleteAllOnSubmit(tP_IsoList);
                            BLL.Funs.DB.SubmitChanges();
                        }
                        BLL.PW_IsoInfoService.DeleteIsoInfo(ISO_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除管线信息");
                        this.gvIsoInfo.DataBind();
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }

        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void CheckItemSetDataBind()
        {
            this.tvControlItem.Nodes.Clear();

            TreeNode rootNode = new TreeNode();//定义根节点
            rootNode.Text = "装置-单位-工作区";
            rootNode.Value = "0";
            rootNode.Expanded = true;

            this.tvControlItem.Nodes.Add(rootNode);
            this.GetNodes(rootNode.ChildNodes, null, null);
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId, TreeNode node)
        {
            var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);

            if (parentId == null)
            {
                List<Model.Base_Installation> installations = null;
             
                if (unit == null || unit.UnitType == "1")
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
                if (unit == null || unit.UnitType == "1")
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
                var workAreas = (from x in BLL.Funs.DB.Base_WorkArea
                                 where x.ProjectId == this.CurrUser.ProjectId && x.InstallationId.ToString() == node.Parent.Value && x.UnitId == parentId                                 
                                 select x).Distinct();
                workAreas = workAreas.OrderByDescending(x => x.WorkAreaCode);
                foreach (var q in workAreas)
                {
                    int a = (from x in BLL.Funs.DB.PW_IsoInfo where x.ProjectId == this.CurrUser.ProjectId && x.BSU_ID == parentId && x.BAW_ID == q.WorkAreaId select x).Count();
                    TreeNode newNode = new TreeNode();
                    newNode.Text = q.WorkAreaCode + "【" + a.ToString() + "】管线";
                    newNode.Value = q.WorkAreaId.ToString();
                    nodes.Add(newNode);
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].ChildNodes, nodes[i].Value, nodes[i]);
            }
        }
        #endregion

        /// <summary>
        /// 选择节点发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 3)
            {
                ClearHideValue();
                AreaWork = this.tvControlItem.SelectedNode.Value;
                this.divControlItemDetailDisplay.Visible = true;
                this.gvIsoInfo.PageIndex = 0;
                this.gvIsoInfo.DataBind();
                CalcFooter();
                
                this.hdUnitId.Value = this.tvControlItem.SelectedNode.Parent.Value;
            }
        }

        protected void btn_AddDetail_Click(object sender, ImageClickEventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 3)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowAddPipeline('" + this.hdUnitId.Value + "','" + this.tvControlItem.SelectedNode.Value + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择施工区域！')", true);
            }
        }

        protected void imgDetail_Click(object sender, ImageClickEventArgs e)
        {
            this.gvIsoInfo.DataBind();
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnColumn_Click(object sender, ImageClickEventArgs e)
        {
            string column = this.hdColumn.Value.Trim();
            if (!string.IsNullOrEmpty(column))
            {
                this.gvIsoInfo.Columns[2].Visible = false;
                this.gvIsoInfo.Columns[3].Visible = false;
                this.gvIsoInfo.Columns[4].Visible = false;
                this.gvIsoInfo.Columns[5].Visible = false;
                this.gvIsoInfo.Columns[6].Visible = false;
                this.gvIsoInfo.Columns[7].Visible = false;
                this.gvIsoInfo.Columns[8].Visible = false;
                this.gvIsoInfo.Columns[9].Visible = false;
                this.gvIsoInfo.Columns[10].Visible = false;
                this.gvIsoInfo.Columns[11].Visible = false;
                this.gvIsoInfo.Columns[12].Visible = false;
                this.gvIsoInfo.Columns[13].Visible = false;
                this.gvIsoInfo.Columns[14].Visible = false;
                this.gvIsoInfo.Columns[15].Visible = false;
                this.gvIsoInfo.Columns[16].Visible = false;
                this.gvIsoInfo.Columns[17].Visible = false;
                this.gvIsoInfo.Columns[18].Visible = false;
                this.gvIsoInfo.Columns[19].Visible = false;
                this.gvIsoInfo.Columns[20].Visible = false;
                this.gvIsoInfo.Columns[21].Visible = false;
                this.gvIsoInfo.Columns[22].Visible = false;
                this.gvIsoInfo.Columns[23].Visible = false;
                this.gvIsoInfo.Columns[24].Visible = false;
                this.gvIsoInfo.Columns[25].Visible = false;
                this.gvIsoInfo.Columns[26].Visible = false;
                this.gvIsoInfo.Columns[27].Visible = false;
                this.gvIsoInfo.Columns[28].Visible = false;
                this.gvIsoInfo.Columns[29].Visible = false;
                this.gvIsoInfo.Columns[30].Visible = false;
                this.gvIsoInfo.Columns[31].Visible = false;
                this.gvIsoInfo.Columns[32].Visible = false;
                this.gvIsoInfo.Columns[33].Visible = false;
                this.gvIsoInfo.Columns[34].Visible = false;
                this.gvIsoInfo.Columns[35].Visible = false;
                List<string> columns = column.Split(',').ToList();
                foreach (var item in columns)
                {
                    this.gvIsoInfo.Columns[Convert.ToInt32(item)].Visible = true;
                }
            }
            this.gvIsoInfo.DataBind();
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.hdSearch.Value))
            {
                ClearHideValue();
                AreaWork = null;
                List<string> searchs = this.hdSearch.Value.Split(',').ToList();
                foreach (var item in searchs)
                {
                    if (item.Contains("ISO_IsoNo"))
                    {
                        this.hdISO_IsoNo.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("SER"))
                    {
                        this.hdSER_ID.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("NDT"))
                    {
                        this.hdNDT_ID.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("ISO_IsoNumber"))
                    {
                        this.hdISO_IsoNumber.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("STE"))
                    {
                        this.hdSTE_ID.Value = item.Split('|')[0];
                    }
                    else if (item.Contains("ISO_Specification"))
                    {
                        this.hdISO_Specification.Value = item.Split('|')[0];
                    }
                }
                this.divControlItemDetailDisplay.Visible = true;
                this.gvIsoInfo.PageIndex = 0;
                this.gvIsoInfo.DataBind();
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbAll = (CheckBox)(this.gvIsoInfo.HeaderRow.FindControl("ckbAll"));
            int rowsCount = this.gvIsoInfo.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                ((CheckBox)(this.gvIsoInfo.Rows[i].FindControl("ckbISO_ID"))).Checked = ckbAll.Checked;
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (Funs.DB.PW_JointInfo.FirstOrDefault(x=>x.ISO_ID == ISO_ID) != null)
            {
                content = "该管线下焊口已有焊接信息，不能删除！";
            }
            if (BLL.AItemEndCheckService.IsExistAItemEndCheck(ISO_ID))
            {
                content = "A项尾工已经使用了该管线，不能删除！";
            }
            if (BLL.BItemEndCheckService.IsExistBItemEndCheck(ISO_ID))
            {
                content = "B项尾工已经使用了该管线，不能删除！";
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

        private void ClearHideValue()
        {
            this.hdISO_IsoNo.Value = string.Empty;
            this.hdSER_ID.Value = string.Empty;
            this.hdNDT_ID.Value = string.Empty;
            this.hdISO_IsoNumber.Value = string.Empty;
            this.hdSTE_ID.Value = string.Empty;
            this.hdISO_Specification.Value = string.Empty;
        }

        /// <summary>
        /// 计算合计数量
        /// </summary>
        private void CalcFooter()
        {
            this.gvIsoInfo.Columns[2].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gvIsoInfo.Columns[2].FooterStyle.Height = Unit.Pixel(25);
            if (this.tvControlItem.SelectedNode != null)
            {
                string str = BLL.SQLHelper.getStr("select SUM(ISNULL(JOT_Size,0)) from dbo.PW_JointInfo where ISO_ID in(select ISO_ID from dbo.PW_IsoInfo where BAW_ID='" + this.tvControlItem.SelectedNode.Value + "')");
                if (str != string.Empty)
                {
                    decimal total = Convert.ToDecimal(str);
                    this.gvIsoInfo.Columns[2].FooterText = total.ToString("F");
                }
                
                this.gvIsoInfo.Columns[3].FooterText = BLL.SQLHelper.getStr("select COUNT(JOT_ID) from dbo.PW_JointInfo where ISO_ID in(select ISO_ID from  dbo.PW_IsoInfo where BAW_ID='" + this.tvControlItem.SelectedNode.Value + "')").ToString();
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                int rowsCount = this.gvIsoInfo.Rows.Count;
                int tatol = 0;
                string isoRes = string.Empty;

                for (int i = 0; i < rowsCount; i++)
                {
                    CheckBox ckbISO_ID = (CheckBox)(this.gvIsoInfo.Rows[i].FindControl("ckbISO_ID"));
                    if (ckbISO_ID.Checked == true)
                    {
                        tatol += 1;
                        HiddenField hdISO_ID = (HiddenField)(this.gvIsoInfo.Rows[i].FindControl("hdISO_ID"));
                        Model.PW_IsoInfo q = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(hdISO_ID.Value);
                        if (q != null)
                        {
                            if (!BLL.PW_JointInfoService.IsExistJointInfoWeld(q.ISO_ID))
                            {
                                BLL.PW_JointInfoService.DeleteJointInfoByIsoId(q.ISO_ID);
                                var tP_IsoList = (from x in BLL.Funs.DB.TP_IsoList where x.ISO_ID == q.ISO_ID select x).FirstOrDefault();
                                if (tP_IsoList != null)
                                {
                                    BLL.Funs.DB.TP_IsoList.DeleteOnSubmit(tP_IsoList);
                                    BLL.Funs.DB.SubmitChanges();
                                }
                                BLL.PW_IsoInfoService.DeleteIsoInfo(hdISO_ID.Value);
                                BLL.LogService.AddLog(this.CurrUser.UserId, "删除管线信息");
                            }

                            else
                            {
                                if (string.IsNullOrEmpty(isoRes))
                                {
                                    isoRes = q.ISO_IsoNo;
                                }
                                else
                                {
                                    isoRes += "," + q.ISO_IsoNo;
                                }
                            }

                        }
                    }
                }
                if (tatol == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的管线！')", true);
                }

                if (!string.IsNullOrEmpty(isoRes))
                {
                    this.gvIsoInfo.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('管线" + isoRes + "存在焊口的焊接信息！')", true);
                }
                else
                {
                    this.gvIsoInfo.DataBind();
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