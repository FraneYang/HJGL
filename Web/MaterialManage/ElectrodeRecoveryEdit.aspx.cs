using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.MaterialManage
{
    public partial class ElectrodeRecoveryEdit :PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ElectrodeRecoveryId
        {
            get {
                return (string)ViewState["ElectrodeRecoveryId"];
            }
            set
            {
                ViewState["ElectrodeRecoveryId"] = value;
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
        /// 焊条发放回收记录明细集合
        /// </summary>
        private List<Model.ElectrodeRecoveryItem> recoveryItems = new List<Model.ElectrodeRecoveryItem>();

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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ElectrodeRecoveryMenuId);

                this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
                this.txtCompileDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                ElectrodeRecoveryId = Request.Params["electrodeRecoveryId"];
                if (!string.IsNullOrEmpty(ElectrodeRecoveryId))
                {
                    Model.ElectrodeRecovery recovery = BLL.ElectrodeRecoveryService.GetElectrodeRecoveryByID(ElectrodeRecoveryId);
                    this.txtEletrodeCode.Text = recovery.ElectrodeRecoveryCode;
                    this.txtElectrodeRecoveryDate.Value = string.Format("{0:yyyy-MM-dd}", recovery.ElectrodeRecoveryDate);
                    this.txtCompileDate.Value = string.Format("{0:yyyy-MM-dd}", recovery.CompileDate);
                    this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;

                    recoveryItems = BLL.ElectrodeRecoveryService.GetElectrodeRecoveryItemByRecoveryID(ElectrodeRecoveryId);
                    this.gvElectrodeCovery.DataSourceID = null;
                    this.gvElectrodeCovery.DataSource = recoveryItems;
                    this.gvElectrodeCovery.DataBind();
                }
            }
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
        {
            Model.ElectrodeRecoveryItem reconveryItem = new Model.ElectrodeRecoveryItem();
            jerqueSaveList();
            recoveryItems.Add(reconveryItem);
            this.gvElectrodeCovery.DataSourceID = null;
            this.gvElectrodeCovery.DataSource = recoveryItems;
            this.gvElectrodeCovery.DataBind();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnSave)||this.CurrUser.Account==BLL.Const.AdminId)
            {
                Model.ElectrodeRecovery recovery = new Model.ElectrodeRecovery();
                recovery.ElectrodeRecoveryCode = this.txtEletrodeCode.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtElectrodeRecoveryDate.Value))
                {
                    recovery.ElectrodeRecoveryDate =Convert.ToDateTime(this.txtElectrodeRecoveryDate.Value);
                }
                if (!string.IsNullOrEmpty(this.txtCompileDate.Value))
                {
                    recovery.CompileDate = Convert.ToDateTime(this.txtCompileDate.Value);
                }
                recovery.CompileMan = this.CurrUser.UserId;
                recovery.ProjectId = this.CurrUser.ProjectId;
               
                if (!string.IsNullOrEmpty(ElectrodeRecoveryId))
                {
                    recovery.ElectrodeRecoveryId = ElectrodeRecoveryId;
                    BLL.ElectrodeRecoveryService.UpdateElectrodeRecovery(recovery);
                    BLL.ElectrodeRecoveryService.DeleteElectrodeRecoveryItem(this.CurrUser.ProjectId,this.ElectrodeRecoveryId);                   
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊条发放回收记录！");                   
                }
                else
                {
                    recovery.ElectrodeRecoveryId = SQLHelper.GetNewID(typeof(Model.ElectrodeRecovery));
                    BLL.ElectrodeRecoveryService.AddElectrodeRecovery(recovery);                    
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊条发放回收记录！");                    
                }

                jerqueSaveList();
                foreach (var item in recoveryItems)
                {
                    item.ElectrodeRecoveryId = recovery.ElectrodeRecoveryId;
                    BLL.ElectrodeRecoveryService.AddElectrodeRecoveryItem(item);
                    
                    int count = 0;
                    if (item.RecipientsCount.HasValue)
                    {
                        count = count - item.RecipientsCount.Value;
                    }

                    if (item.RecoveryCount.HasValue)
                    {
                        count = count + item.RecoveryCount.Value;
                    }

                    BLL.EMInventoryRecordsService.UpdateEMInventoryRecords(this.CurrUser.ProjectId, item.WMT_ID, item.ElectrodeRecoveryModel, item.Specifications, count);
                }

                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('保存成功！');window.location.href='ElectrodeRecovery.aspx'", true);
            }
        }
        /// <summary>
        /// 检查并保存焊条发放回收记录集合
        /// </summary>
        private void jerqueSaveList()
        {
            recoveryItems.Clear();
            int rowsCount = this.gvElectrodeCovery.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.ElectrodeRecoveryItem recoveryItem = new Model.ElectrodeRecoveryItem();
                string model = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtModel"))).Text;//型号
                string electrodeGrade = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtElectrodeGrade"))).Text;//牌号
                string batchNumber = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtBatchNumber"))).Text;//批号
                string inLibCode = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtInLibCode"))).Text;//入库自编号
                string specifications = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtSpecifications"))).Text;//规格
                string welderCode = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtWelderCode"))).Text;//焊工代号
                string useSite = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtUseSite"))).Text;//使用部位
                string weldingMaterial = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtWeldingMaterial"))).Text;//焊件材质
                string recipientsCount = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtRecipientsCount"))).Text;//领用数量
                string recoveryCount = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtRecoveryCount"))).Text;//回收数量
                string grantMan = ((TextBox)(this.gvElectrodeCovery.Rows[i].FindControl("txtGrantMan"))).Text;//发放人
                string materialName = ((DropDownList)(this.gvElectrodeCovery.Rows[i].FindControl("drpWME_ID"))).SelectedValue;//材料名称

                if (materialName != "0")
                {
                    recoveryItem.WMT_ID = materialName;
                }
                recoveryItem.ElectrodeRecoveryItemID = ((ImageButton)(this.gvElectrodeCovery.Rows[i].FindControl("imgbtnDelete"))).CommandArgument.ToString();
                recoveryItem.ElectrodeRecoveryModel = model;
                recoveryItem.ElectrodeGrade = electrodeGrade;
                recoveryItem.BatchNumber = batchNumber;
                recoveryItem.InLibCode = inLibCode;
                recoveryItem.Specifications = specifications;
                recoveryItem.WelderCode = welderCode;
                recoveryItem.UseSite = useSite;
                recoveryItem.WeldingMaterial = weldingMaterial;
                if (recipientsCount!="")
                {
                    recoveryItem.RecipientsCount = Convert.ToInt32(recipientsCount);
                }
                if (recoveryCount!="")
                {
                    recoveryItem.RecoveryCount = Convert.ToInt32(recoveryCount);
                }                
                recoveryItem.GrantMan = grantMan;

                recoveryItems.Add(recoveryItem);
            }
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ElectrodeRecovery.aspx");
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeCovery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string recoveryItemId = e.CommandArgument.ToString();
            if (e.CommandName=="del")
            {
                this.jerqueSaveList();
                foreach (Model.ElectrodeRecoveryItem item in recoveryItems)
                {
                    if (item.ElectrodeRecoveryItemID==recoveryItemId)
                    {
                        recoveryItems.Remove(item);
                        break;
                    }
                }
            }
            this.gvElectrodeCovery.DataSourceID = null;
            this.gvElectrodeCovery.DataSource = recoveryItems;
            this.gvElectrodeCovery.DataBind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeCovery_DataBound(object sender, EventArgs e)
        {
            int rowsCount = this.gvElectrodeCovery.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                DropDownList drpWME_ID = (DropDownList)(this.gvElectrodeCovery.Rows[i].FindControl("drpWME_ID"));
                HiddenField hdWMEID = (HiddenField)(this.gvElectrodeCovery.Rows[i].FindControl("hdWMEID"));
                Funs.PleaseSelect(drpWME_ID);
                drpWME_ID.Items.AddRange(BLL.ConsumablesService.GetMaterialList());
                drpWME_ID.SelectedValue = hdWMEID.Value;
            }
        }
    }
}