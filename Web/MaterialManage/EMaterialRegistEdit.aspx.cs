using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.MaterialManage
{
    public partial class EMaterialRegistEdit : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string EMaterialRegistId
        {
            get
            {
                return (string)ViewState["EMaterialRegistId"];
            }
            set
            {
                ViewState["EMaterialRegistId"] = value;
            }
        }

        /// <summary>
        /// 按钮权限
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.EMaterialRegistMenuId);

                this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
                this.txtEMaterialRegistDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                this.EMaterialRegistId = Request.Params["eMaterialRegistId"];
                if (!string.IsNullOrEmpty(this.EMaterialRegistId))
                {
                    Model.EMaterialRegist eMaterialRegist = BLL.EMaterialRegistService.GetEMaterialRegistByID(this.EMaterialRegistId);
                    this.txtUnitName1.Text = eMaterialRegist.UnitName;
                    this.txtEMaterialRegistCode.Text = eMaterialRegist.EMaterialRegistCode;
                    this.txtEMaterialRegistDate.Value = string.Format("{0:yyyy-MM-dd}", eMaterialRegist.EMaterialRegistDate);
                    this.txtDeliveryMan.Text = eMaterialRegist.DeliveryMan;
                    this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(eMaterialRegist.ProjectId).ProjectName;

                    eMaterialRegistItems = BLL.EMaterialRegistService.GetEMaterialRegistItemByRegistId(this.EMaterialRegistId);
                    this.gvEmaterialRegist.DataSourceID = null;
                    this.gvEmaterialRegist.DataSource = eMaterialRegistItems;
                    this.gvEmaterialRegist.DataBind();
                }
            }
        }

        /// <summary>
        /// 视图集合
        /// </summary>
        private List<Model.EMaterialRegistItem> eMaterialRegistItems = new List<Model.EMaterialRegistItem>();

        /// <summary>
        /// 添加行按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
        {
            Model.EMaterialRegistItem item = new Model.EMaterialRegistItem();
            jerqueSaveList();
            eMaterialRegistItems.Add(item);
            this.gvEmaterialRegist.DataSourceID = null;
            this.gvEmaterialRegist.DataSource = eMaterialRegistItems;
            this.gvEmaterialRegist.DataBind();
        }
        /// <summary>
        /// 检查并保焊材到货登记及验收报告记录
        /// </summary>
        private void jerqueSaveList()
        {
            eMaterialRegistItems.Clear();
            int rowsCount = this.gvEmaterialRegist.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.EMaterialRegistItem item = new Model.EMaterialRegistItem();
                item.EMaterialRegistItemId = ((ImageButton)(this.gvEmaterialRegist.Rows[i].FindControl("imgbtnDelete"))).CommandArgument.ToString();

                string materialName = ((DropDownList)(this.gvEmaterialRegist.Rows[i].FindControl("drpWME_ID"))).SelectedValue;//材料名称
                string specificationsModel = ((TextBox)(this.gvEmaterialRegist.Rows[i].FindControl("txtSpecificationsModel"))).Text;//规格
                string unitName = ((TextBox)(this.gvEmaterialRegist.Rows[i].FindControl("txtUnitName2"))).Text;//单位
                string count = ((TextBox)(this.gvEmaterialRegist.Rows[i].FindControl("txtMaterialCount"))).Text;//数量
                string code = ((TextBox)(this.gvEmaterialRegist.Rows[i].FindControl("txtItemCode"))).Text;//随货资料及编号
                string testrecords = ((TextBox)(this.gvEmaterialRegist.Rows[i].FindControl("txtTestrecords"))).Text;//验收记录
                string models = ((TextBox)(this.gvEmaterialRegist.Rows[i].FindControl("txtModels"))).Text;//型号

                if (materialName != "0")
                {
                    item.WMT_ID = materialName;
                }

                item.SpecificationsModel = specificationsModel;
                item.UnitName = unitName;
                if (count != "")
                {
                    item.MaterialCount = Convert.ToInt32(count);
                }
                item.ItemCode = code;
                item.Testrecords = testrecords;
                item.Models = models;

                eMaterialRegistItems.Add(item);
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.EMaterialRegist eMaterialRegist = new Model.EMaterialRegist();
                eMaterialRegist.EMaterialRegistCode = this.txtEMaterialRegistCode.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtEMaterialRegistDate.Value))
                {
                    eMaterialRegist.EMaterialRegistDate = Convert.ToDateTime(this.txtEMaterialRegistDate.Value);
                }
                eMaterialRegist.DeliveryMan = this.txtDeliveryMan.Text.Trim();
                eMaterialRegist.UnitName = this.txtUnitName1.Text.Trim();
                eMaterialRegist.CompileMan = this.CurrUser.UserId;
                eMaterialRegist.CompileDate = DateTime.Now;
                eMaterialRegist.ProjectId = this.CurrUser.ProjectId;

                 //修改
                if (!string.IsNullOrEmpty(this.EMaterialRegistId))
                {
                    eMaterialRegist.EMaterialRegistId = this.EMaterialRegistId;
                    BLL.EMaterialRegistService.UpdateEMaterialRegist(eMaterialRegist);
                    /// 删除到货明细
                    BLL.EMaterialRegistService.DeleteEMaterialRegistItem(this.CurrUser.ProjectId, this.EMaterialRegistId);                   
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改材料到货登记及验收记录！");
                    
                }
                else  //添加
                {
                    eMaterialRegist.EMaterialRegistId = SQLHelper.GetNewID(typeof(Model.EMaterialRegist));
                    this.EMaterialRegistId = eMaterialRegist.EMaterialRegistId;
                    BLL.EMaterialRegistService.AddEMaterialRegist(eMaterialRegist);                   
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加材料到货登记及验收记录！");                    
                }

                jerqueSaveList();
                foreach (var item in eMaterialRegistItems)
                {
                    item.EMaterialRegistId = this.EMaterialRegistId;
                    item.EMaterialRegistItemId = SQLHelper.GetNewID(typeof(Model.EMaterialRegistItem));
                    BLL.EMaterialRegistService.AddEMaterialRegistItem(item);
                    if (item.MaterialCount.HasValue)
                    {
                        BLL.EMInventoryRecordsService.UpdateEMInventoryRecords(this.CurrUser.ProjectId, item.WMT_ID, item.Models, item.SpecificationsModel, item.MaterialCount.Value);
                    }

                }

                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('保存成功！');window.location.href='EMaterialRegist.aspx'", true);
            }
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EMaterialRegist.aspx");
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEmaterialRegist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string eMaterialRegistItemId = e.CommandArgument.ToString();
            if (e.CommandName=="del")
            {
                this.jerqueSaveList();
                foreach (Model.EMaterialRegistItem item in eMaterialRegistItems)
                {
                    if (item.EMaterialRegistItemId == eMaterialRegistItemId)
                    {
                        eMaterialRegistItems.Remove(item);
                        break;
                    }
                }
            }

            this.gvEmaterialRegist.DataSourceID = null;
            this.gvEmaterialRegist.DataSource = eMaterialRegistItems;
            this.gvEmaterialRegist.DataBind();
        }

        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.open('EMaterialRegistPrint.aspx?eMaterialRegistId=" + EMaterialRegistId + "')", true);
        }

        /// <summary>
        /// GV绑定        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEmaterialRegist_DataBound(object sender, EventArgs e)
        {
            int rowsCount = this.gvEmaterialRegist.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                DropDownList drpWME_ID = (DropDownList)(this.gvEmaterialRegist.Rows[i].FindControl("drpWME_ID"));
                HiddenField hdWMEID = (HiddenField)(this.gvEmaterialRegist.Rows[i].FindControl("hdWMEID"));
                Funs.PleaseSelect(drpWME_ID);
                drpWME_ID.Items.AddRange(BLL.ConsumablesService.GetMaterialList());
                drpWME_ID.SelectedValue = hdWMEID.Value;               
            }
        }
    }
}