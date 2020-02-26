using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MaterialManage
{
    public partial class EMaterialRegistPrint : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.Params["eMaterialRegistId"];
                if (!string.IsNullOrEmpty(id))
                {
                    Model.EMaterialRegist regist = BLL.EMaterialRegistService.GetEMaterialRegistByID(id);
                    this.lblUnit.Text = regist.UnitName;
                    this.lblCode.Text = regist.EMaterialRegistCode;
                    this.lblMan.Text = regist.DeliveryMan;
                    this.lblYear.Text = Convert.ToString(regist.EMaterialRegistDate).Substring(0, 4);
                    this.lblMonth.Text = Convert.ToString(regist.EMaterialRegistDate).Substring(5,2);
                    this.lblDay.Text = Convert.ToString(regist.EMaterialRegistDate).Substring(8,2);
                }
            }
        }
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["eMaterialRegistId"] = Request.Params["eMaterialRegistId"];
        }

        protected void gvEMaterialRegistPrint_DataBound(object sender, EventArgs e)
        {
            if (this.gvEMaterialRegistPrint.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvEMaterialRegistPrint.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvEMaterialRegistPrint;
        }
    }
}