using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.WeldingManage
{
    public partial class ShowPointItem : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string toPoint = Request.Params["toPoint"];

                var jots = (from x in BLL.Funs.DB.V_JOINTVIEW
                            where x.PW_PointID == toPoint
                            select x).ToList();

                if (jots.Count > 0)
                {
                    this.gvTrustItem.Visible = true;
                    this.gvTrustItem.DataSource = jots;
                    this.gvTrustItem.DataBind();
                }
            }

        }
    }
}