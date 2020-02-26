using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.MaterialManage
{
    public partial class ElectrodeBakeRecordEdit :PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ElectrodeID
        {
            get
            {
                return (string)ViewState["ElectrodeID"];
            }
            set
            {
                ViewState["ElectrodeID"] = value;
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
        /// 焊条烘烤明细集合
        /// </summary>
        private static List<Model.ElectrodeBakeItem> electrodeBakeItems = new List<Model.ElectrodeBakeItem>();

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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ElectrodeBakeMenuId);

                this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
                this.txtCompileDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                ElectrodeID = Request.Params["electrodeId"];
                if (!string.IsNullOrEmpty(ElectrodeID))
                {
                    Model.ElectrodeBake electrodeBake = BLL.ElectrodeBakeService.GetElecrodeBakeByElectrodeId(ElectrodeID);
                    this.txtEletrodeCode.Text = electrodeBake.ElectrodeCode;
                    this.txtCompileDate.Value = string.Format("{0:yyyy-MM-dd}", electrodeBake.CompileDate);
                    this.txtElectrodeDate.Value = string.Format("{0:yyyy-MM-dd}",electrodeBake.ElectrodeDate);
                    this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(electrodeBake.ProjectId).ProjectName;

                   // electrodeBakeItems = BLL.ElectrodeBakeService.GetElecrodeBakeItemByElecrodeId(ElectrodeID);
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
            if (this.ButtonList.Contains(BLL.Const.BtnSave)||this.CurrUser.Account ==BLL.Const.AdminId)
            {
                Model.ElectrodeBake electrodeBake = new Model.ElectrodeBake();

                electrodeBake.ElectrodeCode = this.txtEletrodeCode.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtElectrodeDate.Value))
                {
                    electrodeBake.ElectrodeDate = Convert.ToDateTime(this.txtElectrodeDate.Value.ToString());
                }
                electrodeBake.UnitId = null;
                electrodeBake.CompileMan = this.CurrUser.UserId;
                if (!string.IsNullOrEmpty(this.txtCompileDate.Value))
                {
                    electrodeBake.CompileDate = Convert.ToDateTime(this.txtCompileDate.Value.ToString());
                }                
                electrodeBake.ProjectId = this.CurrUser.ProjectId;

                if (!string.IsNullOrEmpty(ElectrodeID))
                {
                    electrodeBake.ElectrodeID = ElectrodeID;
                    BLL.ElectrodeBakeService.UpdateElectrodeBake(electrodeBake); //修改焊丝烘烤记录
                    BLL.ElectrodeBakeService.DeleteElectrodeBakeItem(ElectrodeID);
                    jerqueSaveList();
                    foreach (var item in electrodeBakeItems)
                    {
                        item.ElectrodeID = electrodeBake.ElectrodeID;
                        BLL.ElectrodeBakeService.AddElectrodeBakeItem(item);
                    }
                    
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊丝烘烤记录！");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('保存成功！');window.location.href='ElectrodeBakeRecord.aspx'", true);
                }
                else
                {
                    electrodeBake.ElectrodeID = SQLHelper.GetNewID(typeof(Model.ElectrodeBake));
                    BLL.ElectrodeBakeService.AddElectrodeBake(electrodeBake); //添加焊丝烘烤记录主表信息
                    jerqueSaveList();
                    foreach (var item in electrodeBakeItems)
                    {
                        item.ElectrodeID = electrodeBake.ElectrodeID;
                        BLL.ElectrodeBakeService.AddElectrodeBakeItem(item); //添加焊丝烘烤记录细表信息
                    }
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊丝烘烤记录！");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('保存成功！');window.location.href='ElectrodeBakeRecord.aspx'", true);
                }
            }
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ElectrodeBakeRecord.aspx");
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
        {
            Model.ElectrodeBakeItem electrodeBakeItem = new Model.ElectrodeBakeItem();

            jerqueSaveList();

            electrodeBakeItems.Add(electrodeBakeItem);

            this.gvElectrodeBake.DataSourceID = null;
            this.gvElectrodeBake.DataSource = electrodeBakeItems;
            this.gvElectrodeBake.DataBind();
        }

        ///<summary>
        ///检查并保存焊丝烘烤记录集合
        ///</summary>
        private void jerqueSaveList()
        {
            electrodeBakeItems.Clear();
            int rowsCount = this.gvElectrodeBake.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.ElectrodeBakeItem electrodeBakeItem = new Model.ElectrodeBakeItem();

                electrodeBakeItem.ElectrodeItemID = ((ImageButton)(this.gvElectrodeBake.Rows[i].FindControl("imgbtnDelete"))).CommandArgument.ToString();

                string model = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtModel"))).Text;//型号
                string cardCode = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtCardCode"))).Text; //牌号
                string batchCode = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtBatchCode"))).Text; //批号
                string inLibCode = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtInLibCode"))).Text;//入库自编号
                string specifications = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtSpecifications"))).Text; //规格
                string electrodeCount = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtElectrodeCount"))).Text; //数量
                string ovenElectricHours = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtOvenElectricHours"))).Text; //烘箱送电-时间-时
                string ovenElectricMinute = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtOvenElectricMinute"))).Text;//烘箱送电-时间-分
                string ovenElectricTemperature = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtOvenElectricTemperature"))).Text;//烘箱送电-温度
                string constantTemperature = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtConstantTemperature"))).Text;//恒温-温度
                string constantStartHours = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtConstantStartHours"))).Text;//恒温-时间-开始时间-时
                string constantStartMinute = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtConstantStartMinute"))).Text;//恒温-时间-开始时间-分
                string constantEndHours = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtConstantEndHours"))).Text;//恒温-时间-结束时间-时
                string constantEndMinute = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtConstantEndMinute"))).Text;//恒温-时间-结束时间-分
                string moveInBoxHours = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtMoveInBoxHours"))).Text;//移入保温箱-时间-时
                string moveInBoxMinute = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtMoveInBoxMinute"))).Text;//移入保温箱-时间-分
                string moveInTemperature = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtMoveInTemperature"))).Text;//移入保温箱-温度
                string bakeNumber = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtBakeNumber"))).Text;//烘烤次数
                string bakeHead = ((TextBox)(this.gvElectrodeBake.Rows[i].FindControl("txtBakeHead"))).Text; //烘烤负责人

                electrodeBakeItem.ElectrodeModel = model;
                electrodeBakeItem.CardCode = cardCode;
                electrodeBakeItem.BatchCode = batchCode;
                electrodeBakeItem.InLibCode = inLibCode;
                electrodeBakeItem.Specifications = specifications;
                if (electrodeCount != "")
                {
                    electrodeBakeItem.ElectrodeCount = Convert.ToInt32(electrodeCount);
                }
                if (ovenElectricHours != "")
                {
                    electrodeBakeItem.OvenElectricHours = Convert.ToInt32(ovenElectricHours);
                }
                if (ovenElectricMinute != "")
                {
                    electrodeBakeItem.OvenElectricMinute = Convert.ToInt32(ovenElectricMinute);
                }
                if (ovenElectricTemperature != "")
                {
                    electrodeBakeItem.OvenElectricTemperature = Convert.ToInt32(ovenElectricTemperature);
                }
                if (constantTemperature != "")
                {
                    electrodeBakeItem.ConstantTemperature = Convert.ToInt32(constantTemperature);
                }
                if (constantStartHours != "")
                {
                    electrodeBakeItem.ConstantStartHours = Convert.ToInt32(constantStartHours);
                }
                if (constantStartMinute != "")
                {
                    electrodeBakeItem.ConstantStartMinute = Convert.ToInt32(constantStartMinute);
                }
                if (constantEndHours != "")
                {
                    electrodeBakeItem.ConstantEndHours = Convert.ToInt32(constantEndHours);
                }
                if (constantEndMinute != "")
                {
                    electrodeBakeItem.ConstantEndMinute = Convert.ToInt32(constantEndMinute);
                }
                if (moveInBoxHours != "")
                {
                    electrodeBakeItem.MoveInBoxHours = Convert.ToInt32(moveInBoxHours);
                }
                if (moveInBoxMinute != "")
                {
                    electrodeBakeItem.MoveInBoxMinute = Convert.ToInt32(moveInBoxMinute);
                }
                if (moveInTemperature != "")
                {
                    electrodeBakeItem.MoveInTemperature = Convert.ToInt32(moveInTemperature);
                }
                if (bakeNumber != "")
                {
                    electrodeBakeItem.BakeNumber = Convert.ToInt32(bakeNumber);
                }
                electrodeBakeItem.BakeHead = bakeHead;

                electrodeBakeItems.Add(electrodeBakeItem);
            }
        }

        /// <summary>
        /// GridView绑定行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeBake_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtModel = (TextBox)e.Row.FindControl("txtModel");
            //    TextBox txtCardCode = (TextBox)e.Row.FindControl("txtCardCode");
            //    TextBox txtBatchCode = (TextBox)e.Row.FindControl("txtBatchCode");
            //    TextBox txtInLibCode = (TextBox)e.Row.FindControl("txtInLibCode");
            //    TextBox txtSpecifications = (TextBox)e.Row.FindControl("txtSpecifications");
            //    TextBox txtElectrodeCount = (TextBox)e.Row.FindControl("txtElectrodeCount");
            //    TextBox txtOvenElectricHours = (TextBox)e.Row.FindControl("txtOvenElectricHours");
            //    TextBox txtOvenElectricMinute = (TextBox)e.Row.FindControl("txtOvenElectricMinute");
            //    TextBox txtOvenElectricTemperature = (TextBox)e.Row.FindControl("txtOvenElectricTemperature");
            //    TextBox txtConstantTemperature = (TextBox)e.Row.FindControl("txtConstantTemperature");
            //    TextBox txtConstantStartHours = (TextBox)e.Row.FindControl("txtConstantStartHours");
            //    TextBox txtConstantStartMinute = (TextBox)e.Row.FindControl("txtConstantStartMinute");
            //    TextBox txtConstantEndHours = (TextBox)e.Row.FindControl("txtConstantEndHours");
            //    TextBox txtConstantEndMinute = (TextBox)e.Row.FindControl("txtConstantEndMinute");
            //    TextBox txtMoveInBoxHours = (TextBox)e.Row.FindControl("txtMoveInBoxHours");
            //    TextBox txtMoveInBoxMinute = (TextBox)e.Row.FindControl("txtMoveInBoxMinute");
            //    TextBox txtMoveInTemperature = (TextBox)e.Row.FindControl("txtMoveInTemperature");
            //    TextBox txtBakeNumber = (TextBox)e.Row.FindControl("txtBakeNumber");
            //    TextBox txtBakeHead = (TextBox)e.Row.FindControl("txtBakeHead");

            //    if (this.gvElectrodeBake.Rows.Count != 0)
            //    {
            //        if (!string.IsNullOrEmpty(electrodeBakeItems[e.Row.RowIndex].ElectrodeItemID))
            //        {
            //            txtModel.Text = electrodeBakeItems[e.Row.RowIndex].ElectrodeModel;
            //            txtCardCode.Text = electrodeBakeItems[e.Row.RowIndex].CardCode;
            //            txtBatchCode.Text = electrodeBakeItems[e.Row.RowIndex].BatchCode;
            //            txtInLibCode.Text = electrodeBakeItems[e.Row.RowIndex].InLibCode;
            //            txtSpecifications.Text = electrodeBakeItems[e.Row.RowIndex].Specifications;
            //            txtElectrodeCount.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].ElectrodeCount);
            //            txtOvenElectricHours.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].OvenElectricHours);
            //            txtOvenElectricMinute.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].OvenElectricMinute);
            //            txtOvenElectricTemperature.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].OvenElectricTemperature);
            //            txtConstantTemperature.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].ConstantTemperature);
            //            txtConstantStartHours.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].ConstantStartHours);
            //            txtConstantStartMinute.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].ConstantStartMinute);
            //            txtConstantEndHours.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].ConstantEndHours);
            //            txtConstantEndMinute.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].ConstantEndMinute);
            //            txtMoveInBoxHours.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].MoveInBoxHours);
            //            txtMoveInBoxMinute.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].MoveInBoxMinute);
            //            txtMoveInTemperature.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].MoveInTemperature);
            //            txtBakeNumber.Text = Convert.ToString(electrodeBakeItems[e.Row.RowIndex].BakeNumber);
            //            txtBakeHead.Text = electrodeBakeItems[e.Row.RowIndex].BakeHead;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 点击GridView行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeBake_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string electrodeItemId = e.CommandArgument.ToString();
            if (e.CommandName == "del")
            {
                this.jerqueSaveList();
                foreach (Model.ElectrodeBakeItem item in electrodeBakeItems)
                {
                    if (item.ElectrodeItemID == electrodeItemId)
                    {
                        electrodeBakeItems.Remove(item);
                        break;
                    }
                }
            }
            this.gvElectrodeBake.DataSourceID = null;
            this.gvElectrodeBake.DataSource = electrodeBakeItems;
            this.gvElectrodeBake.DataBind();
        }
    }
}