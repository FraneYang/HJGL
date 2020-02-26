using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class ShowSearch : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                Funs.PleaseSelect(this.drpSER);
                this.drpSER.Items.AddRange(BLL.MediumService.GetBSServiceList());

                var q2 = (from x in Funs.DB.BS_NDTType orderby x.NDT_Code select x).ToList();
                ListItem[] list2 = new ListItem[q2.Count()];
                for (int i = 0; i < q2.Count(); i++)
                {
                    list2[i] = new ListItem(q2[i].NDT_Name ?? "", q2[i].NDT_ID.ToString());
                }
                Funs.PleaseSelect(this.drpNDT);
                this.drpNDT.Items.AddRange(list2);

                var q3 = (from x in Funs.DB.BS_Steel orderby x.STE_Code select x).ToList();
                ListItem[] list3 = new ListItem[q3.Count()];
                for (int i = 0; i < q3.Count(); i++)
                {
                    list3[i] = new ListItem(q3[i].STE_Name ?? "", q3[i].STE_ID.ToString());
                }
                Funs.PleaseSelect(this.drpSTE);
                this.drpSTE.Items.AddRange(list3);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            string result = string.Empty; ;
            if (!string.IsNullOrEmpty(this.txtISO_IsoNo.Text.Trim()))
            {
                result += this.txtISO_IsoNo.Text.Trim() + "|ISO_IsoNo" + ",";
            }
            if (this.drpSER.SelectedValue != "0")
            {
                result += this.drpSER.SelectedValue + "|SER" + ",";
            }
            if (this.drpNDT.SelectedValue != "0")
            {
                result += this.drpNDT.SelectedValue + "|NDT" + ",";
            }
            if (!string.IsNullOrEmpty(this.txtISO_IsoNumber.Text.Trim()))
            {
                result += this.txtISO_IsoNumber.Text.Trim() + "|ISO_IsoNumber" + ",";
            }
            if (this.drpSTE.SelectedValue != "0")
            {
                result += this.drpSTE.SelectedValue + "|STE" + ",";
            }
            if (!string.IsNullOrEmpty(this.txtISO_Specification.Text.Trim()))
            {
                result += this.txtISO_Specification.Text.Trim() + "|ISO_Specification" + ",";
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(0, result.LastIndexOf(","));
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowWorkStageClose('" + result + "');</script>");
            }
        }
    }
}