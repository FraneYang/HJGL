using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class ShowColumn : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem[] list = new ListItem[34];
                list[0] = new ListItem("总达因数", "2");
                list[1] = new ListItem("总焊口量", "3");
                list[2] = new ListItem("单位", "4");
                list[3] = new ListItem("介质", "5");
                list[4] = new ListItem("探伤比例", "6");
                list[5] = new ListItem("探伤类型", "7");
                list[6] = new ListItem("施工区域", "8");
                list[7] = new ListItem("系统号", "9");
                list[8] = new ListItem("分系统号", "10");

                list[9] = new ListItem("工作包号", "11");
                list[10] = new ListItem("单线图号", "12");
                list[11] = new ListItem("图纸版次", "13");
                list[12] = new ListItem("页数", "14");

                list[13] = new ListItem("总管段数", "15");
                list[14] = new ListItem("涂漆类别", "16");
                list[15] = new ListItem("绝热类别", "17");
                list[16] = new ListItem("材质", "18");
                list[17] = new ListItem("执行标准", "19");
               
                list[18] = new ListItem("修改人", "20");
                list[19] = new ListItem("修改日期", "21");
                list[20] = new ListItem("建档人", "22");
                list[21] = new ListItem("建档日期", "23");
                list[22] = new ListItem("设计压力", "24");
                list[23] = new ListItem("设计温度", "25");
                list[24] = new ListItem("试验压力", "26");
                list[25] = new ListItem("试验温度", "27");
                list[26] = new ListItem("合格等级", "28");
                list[27] = new ListItem("渗透比例", "29");
                list[28] = new ListItem("管道等级", "30");
                list[29] = new ListItem("渗透等级", "31");
                list[30] = new ListItem("是否酸洗", "32");
                list[31] = new ListItem("是否抛光", "33");
                list[32] = new ListItem("试压包编号", "34");
                list[33] = new ListItem("备注", "35");
                this.chblColumn.DataSourceID = null;
                this.chblColumn.DataSource = list;
                this.chblColumn.DataBind();
                Model.Sys_UserShowColumns c = BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId, "1");
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
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            string column = string.Empty; ;
            int count = this.chblColumn.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.chblColumn.Items[i].Selected)
                {
                    column += this.chblColumn.Items[i].Value + ",";
                }
            }
            if (column != "")
            {
                column = column.Substring(0, column.LastIndexOf(","));
                Model.Sys_UserShowColumns columns = new Model.Sys_UserShowColumns();
                 Model.Sys_UserShowColumns c=BLL.UserShowColumnsService.GetColumnsByUserId(this.CurrUser.UserId,"1");
                 if (c == null)
                 {
                     columns.UserId = this.CurrUser.UserId;
                     columns.Columns = column;
                     columns.ShowType = "1";
                     BLL.UserShowColumnsService.AddUserShowColumns(columns);
                 }
                 else
                 {
                     c.Columns = column;
                     BLL.UserShowColumnsService.UpdateUserShowColumns(c);
                 }
            }
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowWorkStageClose('" + column + "');</script>");
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