using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.DataIn
{
    public partial class DataInTableEdit : PPage
    {
        #region  定义项
        /// <summary>
        /// 主键 
        /// </summary>
        public string TempId
        {
            get
            {
                return (string)ViewState["TempId"];
            }
            set
            {
                ViewState["TempId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 材质加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TempId = Request.Params["tempId"];                
                this.txtValue1.Focus();
                var dataInTemp = BLL.DataInTableService.GetDataInTempByTempId(this.TempId);
                if (dataInTemp != null)
                {                  
                    this.txtValue1.Text = dataInTemp.Value1;
                    this.txtValue2.Text = dataInTemp.Value2;
                    this.txtValue3.Text = dataInTemp.Value3;
                    this.txtValue4.Text = dataInTemp.Value4;
                    this.txtValue5.Text = dataInTemp.Value5;
                    this.txtValue6.Text = dataInTemp.Value6;
                    this.txtValue7.Text = dataInTemp.Value7;
                    this.txtValue8.Text = dataInTemp.Value8;
                    this.txtValue9.Text = dataInTemp.Value9;
                    this.txtValue10.Text = dataInTemp.Value10;
                    this.txtValue11.Text = dataInTemp.Value11;
                    this.txtValue12.Text = dataInTemp.Value12;
                    this.txtValue13.Text = dataInTemp.Value13;
                    this.txtValue14.Text = dataInTemp.Value14;
                    this.txtValue15.Text = dataInTemp.Value15;
                    this.txtValue16.Text = dataInTemp.Value16;
                    this.txtValue17.Text = dataInTemp.Value17;
                    this.txtValue18.Text = dataInTemp.Value18;
                    this.txtValue19.Text = dataInTemp.Value19;
                    this.txtValue20.Text = dataInTemp.Value20;
                    this.txtValue21.Text = dataInTemp.Value21;
                    this.txtValue22.Text = dataInTemp.Value22;
                    this.txtValue23.Text = dataInTemp.Value23;
                    this.txtValue24.Text = dataInTemp.Value24;
                    this.txtValue25.Text = dataInTemp.Value25;
                    this.txtValue26.Text = dataInTemp.Value26;
                    this.txtValue27.Text = dataInTemp.Value27;
                    this.txtValue28.Text = dataInTemp.Value28;
                    this.txtValue29.Text = dataInTemp.Value29;
                    this.txtValue30.Text = dataInTemp.Value30;
                    this.txtValue31.Text = dataInTemp.Value31;
                    this.txtValue32.Text = dataInTemp.Value32;
                    this.txtValue33.Text = dataInTemp.Value33;
                    this.txtValue34.Text = dataInTemp.Value34;
                    this.lbCout.Text = dataInTemp.ToopValue;
                }
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            var dataInTemp = BLL.DataInTableService.GetDataInTempByTempId(this.TempId);
            if (this.ckAll.Checked)
            {
                var allDataInTemp = from x in Funs.DB.Sys_DataInTemp where x.ProjectId == this.CurrUser.ProjectId && x.UserId == this.CurrUser.UserId  select x;
                if (dataInTemp.Value1 != this.txtValue1.Text.Trim())
                {
                    var tempValue1 = allDataInTemp.Where(x => x.Value1 == dataInTemp.Value1 || (x.Value1 == null && dataInTemp.Value1 == null));
                    if (tempValue1 != null)
                    {
                        foreach (var item in tempValue1)
                        {
                            item.Value1 = this.txtValue1.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }                        
                    }
                }
                if (dataInTemp.Value2 != this.txtValue2.Text.Trim())
                {
                    var tempValue2 = allDataInTemp.Where(x => x.Value2 == dataInTemp.Value2 || (x.Value2 == null && dataInTemp.Value2 == null));
                    if (tempValue2 != null)
                    {
                        foreach (var item in tempValue2)
                        {
                            item.Value2 = this.txtValue2.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }                        
                    }
                }
                if (dataInTemp.Value3 != this.txtValue3.Text.Trim())
                {
                    var tempValue3 = allDataInTemp.Where(x => x.Value3 == dataInTemp.Value3 || (x.Value3 == null && dataInTemp.Value3 == null));
                    if (tempValue3 != null)
                    {
                        foreach (var item in tempValue3)
                        {
                            item.Value3 = this.txtValue3.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }                        
                    }
                }
                if (dataInTemp.Value5 != this.txtValue5.Text.Trim())
                {
                    var tempValue5 = allDataInTemp.Where(x => x.Value5 == dataInTemp.Value5 || (x.Value5 == null && dataInTemp.Value5 == null));
                    if (tempValue5 != null)
                    {
                        foreach (var item in tempValue5)
                        {
                            item.Value5 = this.txtValue5.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }                        
                    }
                }
                if (dataInTemp.Value6 != this.txtValue6.Text.Trim())
                {
                    var tempValue6 = allDataInTemp.Where(x => x.Value6 == dataInTemp.Value6 || (x.Value6 == null && dataInTemp.Value6 == null));
                    if (tempValue6 != null)
                    {
                        foreach (var item in tempValue6)
                        {
                            item.Value6 = this.txtValue6.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value7 != this.txtValue7.Text.Trim())
                {
                    var tempValue7 = allDataInTemp.Where(x => x.Value7 == dataInTemp.Value7 || (x.Value7 == null && dataInTemp.Value7 == null));
                    if (tempValue7 != null)
                    {
                        foreach (var item in tempValue7)
                        {
                            item.Value7 = this.txtValue7.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value8 != this.txtValue8.Text.Trim())
                {
                    var tempValue8 = allDataInTemp.Where(x => x.Value8 == dataInTemp.Value8 || (x.Value8 == null && dataInTemp.Value8 == null));
                    if (tempValue8 != null)
                    {
                        foreach (var item in tempValue8)
                        {
                            item.Value8 = this.txtValue8.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value9 != this.txtValue9.Text.Trim())
                {
                    var tempValue9 = allDataInTemp.Where(x => x.Value9 == dataInTemp.Value9 || (x.Value9 == null && dataInTemp.Value9 == null));
                    if (tempValue9 != null)
                    {
                        foreach (var item in tempValue9)
                        {
                            item.Value9 = this.txtValue9.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value10 != this.txtValue10.Text.Trim())
                {
                    var tempValue10 = allDataInTemp.Where(x => x.Value10 == dataInTemp.Value10 || (x.Value10 == null && dataInTemp.Value10 == null));
                    if (tempValue10 != null)
                    {
                        foreach (var item in tempValue10)
                        {
                            item.Value10 = this.txtValue10.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value11 != this.txtValue11.Text.Trim())
                {
                    var tempValue11 = allDataInTemp.Where(x => x.Value11 == dataInTemp.Value11 || (x.Value11 == null && dataInTemp.Value11 == null));
                    if (tempValue11 != null)
                    {
                        foreach (var item in tempValue11)
                        {
                            item.Value11 = this.txtValue11.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value12 != this.txtValue12.Text.Trim())
                {
                    var tempValue12 = allDataInTemp.Where(x => x.Value12 == dataInTemp.Value12 || (x.Value12 == null && dataInTemp.Value12 == null));
                    if (tempValue12 != null)
                    {
                        foreach (var item in tempValue12)
                        {
                            item.Value12 = this.txtValue12.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value13 != this.txtValue13.Text.Trim())
                {
                    var tempValue13 = allDataInTemp.Where(x => x.Value13 == dataInTemp.Value13 || (x.Value13 == null && dataInTemp.Value13 == null));
                    if (tempValue13 != null)
                    {
                        foreach (var item in tempValue13)
                        {
                            item.Value13 = this.txtValue13.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value14 != this.txtValue14.Text.Trim())
                {
                    var tempValue14 = allDataInTemp.Where(x => x.Value14 == dataInTemp.Value14 || (x.Value14 == null && dataInTemp.Value14 == null));
                    if (tempValue14 != null)
                    {
                        foreach (var item in tempValue14)
                        {
                            item.Value14 = this.txtValue14.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value15 != this.txtValue15.Text.Trim())
                {
                    var tempValue15 = allDataInTemp.Where(x => x.Value15 == dataInTemp.Value15 || (x.Value15 == null && dataInTemp.Value15 == null));
                    if (tempValue15 != null)
                    {
                        foreach (var item in tempValue15)
                        {
                            item.Value15 = this.txtValue15.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value16 != this.txtValue16.Text.Trim())
                {
                    var tempValue16 = allDataInTemp.Where(x => x.Value16 == dataInTemp.Value16 || (x.Value16 == null && dataInTemp.Value16 == null));
                    if (tempValue16 != null)
                    {
                        foreach (var item in tempValue16)
                        {
                            item.Value16 = this.txtValue16.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value17 != this.txtValue17.Text.Trim())
                {
                    var tempValue17 = allDataInTemp.Where(x => x.Value17 == dataInTemp.Value17 || (x.Value17 == null && dataInTemp.Value17 == null));
                    if (tempValue17 != null)
                    {
                        foreach (var item in tempValue17)
                        {
                            item.Value17 = this.txtValue17.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value18 != this.txtValue18.Text.Trim())
                {
                    var tempValue18 = allDataInTemp.Where(x => x.Value18 == dataInTemp.Value18 || (x.Value18 == null && dataInTemp.Value18 == null));
                    if (tempValue18 != null)
                    {
                        foreach (var item in tempValue18)
                        {
                            item.Value18 = this.txtValue18.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value19 != this.txtValue19.Text.Trim())
                {
                    var tempValue19 = allDataInTemp.Where(x => x.Value19 == dataInTemp.Value19 || (x.Value19 == null && dataInTemp.Value19 == null));
                    if (tempValue19 != null)
                    {
                        foreach (var item in tempValue19)
                        {
                            item.Value19 = this.txtValue19.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value20 != this.txtValue20.Text.Trim())
                {
                    var tempValue20 = allDataInTemp.Where(x => x.Value20 == dataInTemp.Value20 || (x.Value20 == null && dataInTemp.Value20 == null));
                    if (tempValue20 != null)
                    {
                        foreach (var item in tempValue20)
                        {
                            item.Value20 = this.txtValue20.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value21 != this.txtValue21.Text.Trim())
                {
                    var tempValue21 = allDataInTemp.Where(x => x.Value21 == dataInTemp.Value21 || (x.Value21 == null && dataInTemp.Value21 == null));
                    if (tempValue21 != null)
                    {
                        foreach (var item in tempValue21)
                        {
                            item.Value21 = this.txtValue21.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value22 != this.txtValue22.Text.Trim())
                {
                    var tempValue22 = allDataInTemp.Where(x => x.Value22 == dataInTemp.Value22 || (x.Value22 == null && dataInTemp.Value22 == null));
                    if (tempValue22 != null)
                    {
                        foreach (var item in tempValue22)
                        {
                            item.Value22 = this.txtValue22.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value23 != this.txtValue23.Text.Trim())
                {
                    var tempValue23 = allDataInTemp.Where(x => x.Value23 == dataInTemp.Value23 || (x.Value23 == null && dataInTemp.Value23 == null));
                    if (tempValue23 != null)
                    {
                        foreach (var item in tempValue23)
                        {
                            item.Value23 = this.txtValue23.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value24 != this.txtValue24.Text.Trim())
                {
                    var tempValue24 = allDataInTemp.Where(x => x.Value24 == dataInTemp.Value24 || (x.Value24 == null && dataInTemp.Value24 == null));
                    if (tempValue24 != null)
                    {
                        foreach (var item in tempValue24)
                        {
                            item.Value24 = this.txtValue24.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value25 != this.txtValue25.Text.Trim())
                {
                    var tempValue25 = allDataInTemp.Where(x => x.Value25 == dataInTemp.Value25 || (x.Value25 == null && dataInTemp.Value25 == null));
                    if (tempValue25 != null)
                    {
                        foreach (var item in tempValue25)
                        {
                            item.Value25 = this.txtValue25.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value26 != this.txtValue26.Text.Trim())
                {
                    var tempValue26 = allDataInTemp.Where(x => x.Value26 == dataInTemp.Value26 || (x.Value26 == null && dataInTemp.Value26 == null));
                    if (tempValue26 != null)
                    {
                        foreach (var item in tempValue26)
                        {
                            item.Value26 = this.txtValue26.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value27 != this.txtValue27.Text.Trim())
                {
                    var tempValue27 = allDataInTemp.Where(x => x.Value27 == dataInTemp.Value27 || (x.Value27 == null && dataInTemp.Value27 == null));
                    if (tempValue27 != null)
                    {
                        foreach (var item in tempValue27)
                        {
                            item.Value27 = this.txtValue27.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value28 != this.txtValue28.Text.Trim())
                {
                    var tempValue28 = allDataInTemp.Where(x => x.Value28 == dataInTemp.Value28 || (x.Value28 == null && dataInTemp.Value28 == null));
                    if (tempValue28 != null)
                    {
                        foreach (var item in tempValue28)
                        {
                            item.Value28 = this.txtValue28.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value29 != this.txtValue29.Text.Trim())
                {
                    var tempValue29 = allDataInTemp.Where(x => x.Value29 == dataInTemp.Value29 || (x.Value29 == null && dataInTemp.Value29 == null));
                    if (tempValue29 != null)
                    {
                        foreach (var item in tempValue29)
                        {
                            item.Value29 = this.txtValue29.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value30 != this.txtValue30.Text.Trim())
                {
                    var tempValue30 = allDataInTemp.Where(x => x.Value30 == dataInTemp.Value30 || (x.Value30 == null && dataInTemp.Value30 == null));
                    if (tempValue30 != null)
                    {
                        foreach (var item in tempValue30)
                        {
                            item.Value30 = this.txtValue30.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value31 != this.txtValue31.Text.Trim())
                {
                    var tempValue31 = allDataInTemp.Where(x => x.Value31 == dataInTemp.Value31 || (x.Value31 == null && dataInTemp.Value31 == null));
                    if (tempValue31 != null)
                    {
                        foreach (var item in tempValue31)
                        {
                            item.Value31 = this.txtValue31.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value32 != this.txtValue32.Text.Trim())
                {
                    var tempValue32 = allDataInTemp.Where(x => x.Value32 == dataInTemp.Value32 || (x.Value32 == null && dataInTemp.Value32 == null));
                    if (tempValue32 != null)
                    {
                        foreach (var item in tempValue32)
                        {
                            item.Value32 = this.txtValue32.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value33 != this.txtValue33.Text.Trim())
                {
                    var tempValue33 = allDataInTemp.Where(x => x.Value33 == dataInTemp.Value33 || (x.Value33 == null && dataInTemp.Value33 == null));
                    if (tempValue33 != null)
                    {
                        foreach (var item in tempValue33)
                        {
                            item.Value33 = this.txtValue33.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                if (dataInTemp.Value34 != this.txtValue34.Text.Trim())
                {
                    var tempValue34 = allDataInTemp.Where(x => x.Value34 == dataInTemp.Value34 || (x.Value34 == null && dataInTemp.Value34 == null));
                    if (tempValue34 != null)
                    {
                        foreach (var item in tempValue34)
                        {
                            item.Value34 = this.txtValue34.Text.Trim();
                            Funs.DB.SubmitChanges();
                        }
                    }
                }  
            }
            else
            {
                Model.Sys_DataInTemp newDataInTemp = new Model.Sys_DataInTemp();
                newDataInTemp.ProjectId = this.CurrUser.ProjectId;
                newDataInTemp.UserId = this.CurrUser.UserId;
                newDataInTemp.Time = System.DateTime.Now;
                newDataInTemp.Value1 = this.txtValue1.Text.Trim();
                newDataInTemp.Value2 = this.txtValue2.Text.Trim();
                newDataInTemp.Value3 = this.txtValue3.Text.Trim();
                newDataInTemp.Value4 = this.txtValue4.Text.Trim();
                newDataInTemp.Value5 = this.txtValue5.Text.Trim();
                newDataInTemp.Value6 = this.txtValue6.Text.Trim();
                newDataInTemp.Value7 = this.txtValue7.Text.Trim();
                newDataInTemp.Value8 = this.txtValue8.Text.Trim();
                newDataInTemp.Value9 = this.txtValue9.Text.Trim();
                newDataInTemp.Value10 = this.txtValue10.Text.Trim();
                newDataInTemp.Value11 = this.txtValue11.Text.Trim();
                newDataInTemp.Value12 = this.txtValue12.Text.Trim();
                newDataInTemp.Value13 = this.txtValue13.Text.Trim();
                newDataInTemp.Value14 = this.txtValue14.Text.Trim();
                newDataInTemp.Value15 = this.txtValue15.Text.Trim();
                newDataInTemp.Value16 = this.txtValue16.Text.Trim();
                newDataInTemp.Value17 = this.txtValue17.Text.Trim();
                newDataInTemp.Value18 = this.txtValue18.Text.Trim();
                newDataInTemp.Value19 = this.txtValue19.Text.Trim();
                newDataInTemp.Value20 = this.txtValue20.Text.Trim();
                newDataInTemp.Value21 = this.txtValue21.Text.Trim();
                newDataInTemp.Value22 = this.txtValue22.Text.Trim();
                newDataInTemp.Value23 = this.txtValue23.Text.Trim();
                newDataInTemp.Value24 = this.txtValue24.Text.Trim();
                newDataInTemp.Value25 = this.txtValue25.Text.Trim();
                newDataInTemp.Value26 = this.txtValue26.Text.Trim();
                newDataInTemp.Value27 = this.txtValue27.Text.Trim();
                newDataInTemp.Value28 = this.txtValue28.Text.Trim();
                newDataInTemp.Value29 = this.txtValue29.Text.Trim();
                newDataInTemp.Value30 = this.txtValue30.Text.Trim();
                newDataInTemp.Value31 = this.txtValue31.Text.Trim();
                newDataInTemp.Value32 = this.txtValue32.Text.Trim();
                newDataInTemp.Value33 = this.txtValue33.Text.Trim();
                newDataInTemp.Value34 = this.txtValue34.Text.Trim();
                if (!string.IsNullOrEmpty(this.TempId))
                {
                    newDataInTemp.TempId = this.TempId;
                    newDataInTemp.ToopValue = null;
                    BLL.DataInTableService.UpdateDataInTemp(newDataInTemp);
                }
            }
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>WindowClose('OK');</script>");          
        }
        #endregion
    }
}