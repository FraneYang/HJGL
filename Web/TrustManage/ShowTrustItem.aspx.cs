using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.TrustManage
{
    public partial class ShowTrustItem : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string toTrust = Request.Params["toTrust"];
                
                 List<Model.View_CH_TrustItem> trustItems = new List<Model.View_CH_TrustItem>();
                trustItems = BLL.TrustManageEditService.GetView_CH_TrustItemByCH_TrustID(toTrust);

                if (trustItems.Count > 0)
                {
                    this.gvTrustItem.Visible = true;
                    this.gvTrustItem.DataSource = trustItems;
                    this.gvTrustItem.DataBind();
                }
            }  

        }
    }
}