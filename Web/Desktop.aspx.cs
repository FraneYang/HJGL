using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Data.SqlClient;

namespace Web
{
    public partial class Desktop : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// 在点击按钮并定义关联的命令时激发(管理通知)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnNotice_Command(object sender, CommandEventArgs e)
        {
            string noticeId = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Response.Redirect("~/Administrative/NoticeParticular.aspx?noticeId=" + noticeId);
            }
        }


        /// <summary>
        /// 在点击按钮并定义关联的命令时激发(未响应)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnHazardResponse_Command(object sender, CommandEventArgs e)
        {
            string url = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                //Model.Hazard_HazardSelectedItem hazardSelectedItem = BLL.HazardSelectedItemService.GetHazardSelectedItemByHazardSelectedCode(hazardSelectedCode);
                //Response.Redirect("Hazard/Response.aspx?hazardListCode=" + hazardSelectedItem.HazardListCode + "&type=desktop&hazardSelectedCode=" + hazardSelectedCode);
                Response.Redirect(url);
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowResponse('" + hazardSelectedCode + "');</script>");
            }
        }

        protected void Show10_Click(object sender, EventArgs e)
        {           
            if (this.LinkShow10.Visible)
            {
                SqlParameter[] value = new SqlParameter[]       
                    {
                        new SqlParameter("@project",this.CurrUser.ProjectId),
                    };
                DataTable dt1 = BLL.SQLHelper.GetDataTableRunProc("SpGetWelder", value);
                this.dlReceiveFileManager2.DataSource = dt1;
                this.dlReceiveFileManager2.DataBind();
            }
            else
            {
                this.dlReceiveFileManager2.DataSource = null;
                this.dlReceiveFileManager2.DataBind();
            }

            this.LinkShow10.Visible = !this.LinkShow10.Visible;
            this.LinkShow11.Visible = !this.LinkShow11.Visible;
        }

        protected void Show20_Click(object sender, EventArgs e)
        {
            if (this.LinkShow20.Visible)
            {
                SqlParameter[] welderLimitPara = new SqlParameter[]
                {
                    new SqlParameter("@project",this.CurrUser.ProjectId),
                };
                DataTable dt = BLL.SQLHelper.GetDataTableRunProc("SpGetWelderLimitDate", welderLimitPara);
                this.dlAuditingManage2.DataSource = dt;
                this.dlAuditingManage2.DataBind();

            }
            else
            {
                this.dlAuditingManage2.DataSource = null;
                this.dlAuditingManage2.DataBind();
            }

            this.LinkShow20.Visible = !this.LinkShow20.Visible;
            this.LinkShow21.Visible = !this.LinkShow21.Visible;
        }

        protected void Show30_Click(object sender, EventArgs e)
        {
            if (this.LinkShow30.Visible)
            {
                DataSet ds = BLL.SQLHelper.RunSqlString("select JOT_JointNo+'  ' + PW_IsoInfo.ISO_IsoNo+'  '+Base_WorkArea.WorkAreaCode as PointNoTrust from PW_JointInfo left join PW_IsoInfo on PW_IsoInfo.ISO_ID=PW_JointInfo.ISO_ID left join Base_WorkArea on Base_WorkArea.WorkAreaId=PW_IsoInfo.BAW_ID where PW_PointID is not null and PW_JointInfo.ProjectId='" + this.CurrUser.ProjectId + "' and JOT_ID not in (select JOT_ID from dbo.CH_TrustItem where PW_JointInfo.ProjectId='" + this.CurrUser.ProjectId + "')", "PW_JointInfo");
                this.dlPointNoTrust2.DataSource = ds.Tables[0];
                this.dlPointNoTrust2.DataBind();

            }
            else
            {
                this.dlPointNoTrust2.DataSource = null;
                this.dlPointNoTrust2.DataBind();
            }

            this.LinkShow30.Visible = !this.LinkShow30.Visible;
            this.LinkShow31.Visible = !this.LinkShow31.Visible;
        }

        protected void Show40_Click(object sender, EventArgs e)
        {
            if (this.LinkShow40.Visible)
            {
                DataSet dsRepairCheck = BLL.SQLHelper.RunSqlString("select distinct CHT_CheckCode + '       ' + CONVERT(varchar(100), CHT_CheckDate, 23) AS RepairCheck FROM  dbo.CH_Check LEFT JOIN dbo.CH_CheckItem AS CheckItem ON CH_Check.CHT_CheckID =CheckItem.CHT_CheckID where  RepairTrustId IS NULL AND CHT_AuditDate is not null  AND CheckItem.CHT_PassFilm != CheckItem.CHT_TotalFilm and ProjectId='" + this.CurrUser.ProjectId + "'", "CH_Check");
                this.dlTrustNoAudit2.DataSource = dsRepairCheck.Tables[0];
                this.dlTrustNoAudit2.DataBind();
            }
            else
            {
                this.dlTrustNoAudit2.DataSource = null;
                this.dlTrustNoAudit2.DataBind();
            }

            this.LinkShow40.Visible = !this.LinkShow40.Visible;
            this.LinkShow41.Visible = !this.LinkShow41.Visible;
        }

        protected void Show50_Click(object sender, EventArgs e)
        {
            if (this.LinkShow50.Visible)
            {
                SqlParameter[] trustNoCheckPara = new SqlParameter[]
                {
                    new SqlParameter("@projectId",this.CurrUser.ProjectId),
                };
                DataTable dttc = BLL.SQLHelper.GetDataTableRunProc("spTrustNoCheck", trustNoCheckPara);
                this.dlTrustNoCheck2.DataSource = dttc;
                this.dlTrustNoCheck2.DataBind();
            }
            else
            {
                this.dlTrustNoCheck2.DataSource = null;
                this.dlTrustNoCheck2.DataBind();
            }

            this.LinkShow50.Visible = !this.LinkShow50.Visible;
            this.LinkShow51.Visible = !this.LinkShow51.Visible;
        }

        protected void Show60_Click(object sender, EventArgs e)
        {
            if (this.LinkShow60.Visible)
            {
                DataSet dsCheckNoAudit = BLL.SQLHelper.RunSqlString("select (CHT_CheckCode+'   '+CONVERT(varchar(100), CHT_CheckDate, 23)) as CheckNoAudit from dbo.CH_Check where CHT_AuditDate is null and ProjectId='" + this.CurrUser.ProjectId + "'", "CH_Check");
                this.dlCheckNoAudit2.DataSource = dsCheckNoAudit.Tables[0];
                this.dlCheckNoAudit2.DataBind();
            }
            else
            {
                this.dlCheckNoAudit2.DataSource = null;
                this.dlCheckNoAudit2.DataBind();
            }

            this.LinkShow60.Visible = !this.LinkShow60.Visible;
            this.LinkShow61.Visible = !this.LinkShow61.Visible;
        }
    }
}
