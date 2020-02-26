using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.WeldingManage
{
    public partial class JointInfoShowColumn : PPage
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem[] list = new ListItem[44];
                list[0] = new ListItem("是否焊接", "2");
                list[1] = new ListItem("焊口状态", "3");
                list[2] = new ListItem("委托情况", "4");
                list[3] = new ListItem("探伤情况", "5");
                list[4] = new ListItem("施工区域", "6");              
                list[5] = new ListItem("焊接日期", "7");
                list[6] = new ListItem("焊接日报告号", "8");
                list[7] = new ListItem("材质", "9");
                list[8] = new ListItem("组件1号", "10");
                list[9] = new ListItem("盖面焊工", "11");
                list[10] = new ListItem("打底焊工", "12");
                list[11] = new ListItem("组件2号", "13");
                list[12] = new ListItem("焊口规格", "14");
                list[13] = new ListItem("外径", "15");
                list[14] = new ListItem("尺寸", "16");
                list[15] = new ListItem("壁厚", "17");
                list[16] = new ListItem("实际壁厚", "18");
                list[17] = new ListItem("坡口类型", "19");
                list[18] = new ListItem("焊缝类型", "20");
                list[19] = new ListItem("焊接方法", "21");
                list[20] = new ListItem("焊丝代号", "22");
                list[21] = new ListItem("焊条代号", "23");
                list[22] = new ListItem("焊接区域", "24");
                list[23] = new ListItem("完成达因", "25");
                list[24] = new ListItem("预热温度", "26");
                list[25] = new ListItem("焊口属性", "27");
                list[26] = new ListItem("层间温度", "28");
                list[27] = new ListItem("后热温度", "29");
                list[28] = new ListItem("炉批号1", "30");
                list[29] = new ListItem("炉批号2", "31");
                list[30] = new ListItem("点口日期", "32");
                list[31] = new ListItem("点口报告号", "33");

                list[32] = new ListItem("委托编号", "34");
                list[33] = new ListItem("委托日期", "35");

                list[34] = new ListItem("外检结果", "36");
                list[35] = new ListItem("外检日期", "37");
                list[36] = new ListItem("外检人员", "38");
                list[37] = new ListItem("是否热处理", "39");

                list[38] = new ListItem("所属管段", "40");
                list[39] = new ListItem("焊接电流", "41");
                list[40] = new ListItem("焊接电压", "42");
                list[41] = new ListItem("热处理日期", "43");
                list[42] = new ListItem("热处理报告号", "44");
                list[43] = new ListItem("备注", "45");
                
                this.chblColumn.DataSourceID = null;
                this.chblColumn.DataSource = list;
                this.chblColumn.DataBind();

                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "2");
                if (c != null)
                {
                    if (!string.IsNullOrEmpty(c.Columns))
                    {
                        List<string> columns = c.Columns.Split(',').ToList();
                        foreach (var item in columns)
                        {
                            foreach (ListItem i in this.chblColumn.Items)
                            {
                                if (i.Value == item)
                                {
                                    i.Selected = true;
                                }
                            }

                        }
                    }
                }
            }
        }


        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            string cl = string.Empty;
            int count = this.chblColumn.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.chblColumn.Items[i].Selected)
                {
                    cl += this.chblColumn.Items[i].Value + ",";
                }
            }
            if (cl!="")
            {
                cl = cl.Substring(0, cl.LastIndexOf(","));
                Model.Sys_UserShowColumns columns = new Model.Sys_UserShowColumns();
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId,"2");
                if (c == null)
                {
                    columns.UserId = this.CurrUser.UserId;
                    columns.Columns = cl;
                    columns.ShowType = "2";
                    BLL.UserShowColumnsService.AddUserShowColumns(columns);
                }
                else
                {
                    c.Columns = cl;
                    BLL.UserShowColumnsService.UpdateUserShowColumns(c);
                }
            } 
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowWorkStageClose('" + cl + "');</script>");
        }

        protected void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            int count = this.chblColumn.Items.Count;
            for (int i = 0; i < count; i++)
            {
                this.chblColumn.Items[i].Selected = ckAll.Checked;
            }
        }
    }
}