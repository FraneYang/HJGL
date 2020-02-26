using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class JointInfoBatchEdit : PPage
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string iso_id = Request.Params["iso_id"];
                string workareaid = Request.Params["workArea"];
                this.txtWorkAreaId.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(workareaid).WorkAreaCode;

                //材质1、2
                var q2 = (from x in Funs.DB.BS_Steel orderby x.STE_Code select x).ToList();
                ListItem[] list2 = new ListItem[q2.Count()];
                for (int i = 0; i < q2.Count(); i++)
                {
                    list2[i] = new ListItem(q2[i].STE_Name ?? "", q2[i].STE_ID.ToString());
                }
                Funs.PleaseSelect(ddlSTE1);
                this.ddlSTE1.Items.AddRange(list2);
                Funs.PleaseSelect(ddlSTE2);
                this.ddlSTE2.Items.AddRange(list2);

                //焊缝类型
                var q3 = (from x in Funs.DB.BS_JointType orderby x.JOTY_Code select x).ToList();
                ListItem[] list4 = new ListItem[q3.Count()];
                for (int i = 0; i < q3.Count(); i++)
                {
                    list4[i] = new ListItem(q3[i].JOTY_Name ?? "", q3[i].JOTY_ID.ToString());
                }
                Funs.PleaseSelect(ddlJOTY_ID);
                this.ddlJOTY_ID.Items.AddRange(list4);

                var q6 = (from x in Funs.DB.BS_WeldMethod orderby x.WME_Code select x).ToList();
                ListItem[] list6 = new ListItem[q6.Count()];
                for (int i = 0; i < q6.Count(); i++)
                {
                    list6[i] = new ListItem(q6[i].WME_Name ?? "", q6[i].WME_ID.ToString());
                }
                Funs.PleaseSelect(ddlWME_ID);
                this.ddlWME_ID.Items.AddRange(list6);

                var q7 = (from x in BLL.Funs.DB.BS_WeldMaterial orderby x.WMT_MatCode select x).ToList();
                ListItem[] list7 = new ListItem[q7.Count()];
                for (int i = 0; i < q7.Count(); i++)
                {
                    list7[i] = new ListItem(q7[i].WMT_MatName ?? "", q7[i].WMT_ID.ToString());
                }
                Funs.PleaseSelect(ddlWeldSilk);
                this.ddlWeldSilk.Items.AddRange(list7);
                Funs.PleaseSelect(ddlWeldMat);
                this.ddlWeldMat.Items.AddRange(list7);

                this.txtWorkAreaId.ReadOnly = true;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Model.PW_JointInfo jointInfo = new Model.PW_JointInfo();
            jointInfo.ProjectId = this.CurrUser.ProjectId;
            if (!string.IsNullOrEmpty(Request.Params["iso_id"]))
            {
                jointInfo.ISO_ID = Request.Params["iso_id"];
            }

            jointInfo.WLO_Code = this.ddlWLO.SelectedValue;

            if (this.ddlSTE1.SelectedValue != "0")
            {
                jointInfo.STE_ID = this.ddlSTE1.SelectedValue.ToString();
            }
            if (this.ddlSTE2.SelectedValue != "0")
            {
                jointInfo.STE_ID2 = this.ddlSTE2.SelectedValue.ToString();
            }
            jointInfo.JOT_JointDesc = this.txtJointDesc.Text.Trim();
            if (this.ddlJOTY_ID.SelectedValue != "0")
            {
                jointInfo.JOTY_ID = this.ddlJOTY_ID.SelectedValue.ToString();
            }
            if (!string.IsNullOrEmpty(this.txtSize.Text.Trim()))
            {
                jointInfo.JOT_Size = Convert.ToDecimal(this.txtSize.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtDia.Text.Trim()))
            {
                jointInfo.JOT_Dia = Convert.ToDecimal(this.txtDia.Text.Trim());
            }
            jointInfo.JOT_JointAttribute = this.ddlJointAttribute.SelectedValue;
            jointInfo.JOT_Sch = this.txtSch.Text.Trim();           
           
            jointInfo.JOT_TrustFlag = "00";
            jointInfo.JOT_CheckFlag = "00";
            jointInfo.JOT_JointStatus = "100";

            if (this.ddlWeldMat.SelectedValue != "0")
            {
                jointInfo.JOT_WeldMat = this.ddlWeldMat.SelectedValue;
            }

            if (this.ddlWeldSilk.SelectedValue != "0")
            {
                jointInfo.JOT_WeldSilk = this.ddlWeldSilk.SelectedValue.ToString();
            }

            if (ddlWME_ID.SelectedValue != "0")
            {
                jointInfo.WME_ID = this.ddlWME_ID.SelectedValue;
            }

            jointInfo.IS_Proess = this.drpIS_Proess.SelectedValue;
            
            int jointNo1 =Convert.ToInt32(this.txtJointNo1.Text.Trim());
            int jointNo2 =Convert.ToInt32(this.txtJointNo2.Text.Trim());
            if (jointNo1 > jointNo2)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口号1必须小于焊口2！')", true);
                return;
            }
            else
            {
                for (int i = jointNo1; i <= jointNo2; i++)
                {
                    if (i < 10)
                    {
                        jointInfo.JOT_JointNo = this.txtJointNo.Text.Trim() + "0" + Convert.ToString(i);
                    }
                    else
                    {
                        jointInfo.JOT_JointNo = this.txtJointNo.Text.Trim() + Convert.ToString(i);
                    }
                    if (jointInfo.JOT_JointNo == BLL.PW_JointInfoService.GetJointInfoByJOTNO(Request.Params["iso_id"], jointInfo.JOT_JointNo))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + jointInfo.JOT_JointNo + "焊口号已经存在！')", true);
                        return;
                    }
                    BLL.PW_JointInfoService.AddJointInfoFatch(jointInfo);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊口信息！");
                }
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "WindowClose('OK')", true);
        }
        #endregion
    }
}