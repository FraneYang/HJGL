using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace Web.PersonManage
{
    public partial class PersonItem : PPage
    {
        #region 权限
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
        {
            get
            {
                return (string)ViewState["RoleId"];
            }
            set
            {
                ViewState["RoleId"] = value;
            }
        }

        /// <summary>
        /// 按钮权限列表
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
        /// 人员id
        /// </summary>
        public string WED_ID
        {
            get
            {
                return (string)ViewState["WED_ID"];
            }
            set
            {
                ViewState["WED_ID"] = value;
            }
        }
        #endregion

        #region 集合
        /// <summary>
        /// 材质集合
        /// </summary>
        private List<Model.BS_Steel> steelLists = new List<Model.BS_Steel>();
        
        /// <summary>
        /// 焊接方法集合
        /// </summary>
        private List<Model.BS_WeldMethod> weldMethodLists = new List<Model.BS_WeldMethod>();
        #endregion

        #region 页面加载时
        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PersonManageMenuId);
                               
                this.WED_ID = Request.Params["wED_ID"];
                this.WelderTitle.Text = "焊工资质条件"; 
                if (!String.IsNullOrEmpty(WED_ID))
                {
                    var person = BLL.PersonManageService.GetWelderByWenId(WED_ID);
                    if (person != null)
                    {
                        this.WelderTitle.Text = "焊工资质条件(" + person.WED_Name + ")"; 
                    }
                }
                steelLists = BLL.PersonItemService.GetSteelList();
                this.gvBS_Steel.DataSource = steelLists;
                this.gvBS_Steel.DataBind();

                weldMethodLists = BLL.WeldMethodItemService.GetWeldMethodList();
                this.gvWeldMethod.DataSource = weldMethodLists;
                this.gvWeldMethod.DataBind();

            }
        }
        #endregion

        #region gv事件
        /// <summary>
        /// 每次执行Select()和SelectCount前都会引发一次该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {          
        }

        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBS_Steel_DataBound(object sender, EventArgs e)
        {
            int rowsCount = this.gvBS_Steel.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                TextBox txtThicknessMin = (TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMin"));
                TextBox txtThicknessMax = (TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMax"));
                TextBox txtSizesMin = (TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMin"));
                TextBox txtSizesMax = (TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMax"));
                Label labSteel = ((Label)(this.gvBS_Steel.Rows[i].FindControl("lblBS_Steel")));

                if (BLL.PersonItemService.IsInBS_WelderItemBS_Steel(this.WED_ID, labSteel.Text))
                {
                    ((CheckBox)(this.gvBS_Steel.Rows[i].FindControl("ckbBS_Steel"))).Checked = true;
                    txtThicknessMin.ReadOnly = false;
                    txtThicknessMax.ReadOnly = false;
                    txtSizesMin.ReadOnly = false;
                    txtSizesMax.ReadOnly = false;
                }
                else
                {
                    txtThicknessMin.ReadOnly = true;
                    txtThicknessMax.ReadOnly = true;
                    txtSizesMin.ReadOnly = true;
                    txtSizesMax.ReadOnly = true;
                }

                if (BLL.PersonItemService.GetWelderToSteel(this.WED_ID, labSteel.Text) != null)
                {
                    var q = BLL.PersonItemService.GetWelderToSteel(this.WED_ID, labSteel.Text);
                    txtThicknessMin.Text = q.ThicknessMin != null ? q.ThicknessMin.Value.ToString() : "";
                    txtThicknessMax.Text = q.ThicknessMax != null ? q.ThicknessMax.Value.ToString() : "";
                    txtSizesMin.Text = q.SizesMin != null ? q.SizesMin.Value.ToString() : "";
                    txtSizesMax.Text = q.SizesMax != null ? q.SizesMax.Value.ToString() : "";

                }

            }
        }

        protected void ckbBS_Steel_CheckedChanged(object sender, EventArgs e)
        {
            bool result = true;
            int rowsCount = this.gvBS_Steel.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbBS_Steel = (CheckBox)(this.gvBS_Steel.Rows[i].FindControl("ckbBS_Steel"));
                if (ckbBS_Steel.Checked == false)
                {
                    result = false;
                }
            }
            if (result)
            {
                ((CheckBox)(this.gvBS_Steel.HeaderRow.FindControl("ckbAll"))).Checked = true;
            }
            else
            {
                ((CheckBox)(this.gvBS_Steel.HeaderRow.FindControl("ckbAll"))).Checked = false;
            }

            CheckBox checkedBS_Steel = sender as CheckBox;
            for (int i = 0; i < rowsCount; i++)
            {
                CheckBox ckbBS_Steel = (CheckBox)(this.gvBS_Steel.Rows[i].FindControl("ckbBS_Steel"));
                if (checkedBS_Steel.ClientID == ckbBS_Steel.ClientID)
                {
                    if (ckbBS_Steel.Checked)
                    {
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMin"))).ReadOnly = false;
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMax"))).ReadOnly = false;
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMin"))).ReadOnly = false;
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMax"))).ReadOnly = false;
                    }
                    else
                    {
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMin"))).ReadOnly = true;
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMax"))).ReadOnly = true;
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMin"))).ReadOnly = true;
                        ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMax"))).ReadOnly = true;
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// 点击查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvBS_Steel.PageIndex = 0;
            this.gvBS_Steel.DataBind();
        }
        #endregion

        #region 点击全选按钮
        /// <summary>
        /// 点击全选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbAll = (CheckBox)(this.gvBS_Steel.HeaderRow.FindControl("ckbAll"));

            int rowsCount = this.gvBS_Steel.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    ((CheckBox)(this.gvBS_Steel.Rows[i].FindControl("ckbBS_Steel"))).Checked = ckbAll.Checked;
                }          
        }
        #endregion

        #region GV绑定下拉项
        /// <summary>
        ///  钢材类型
        /// </summary>
        /// <param name="applyTypeId"></param>
        /// <returns></returns>
        protected string ConvertSteelType(object STE_SteelType)
        {
            if (STE_SteelType == null)
            {
                return null;
            }
            else
            {
                if (STE_SteelType.ToString() == "1")
                {
                    return "碳钢";
                }
                else if (STE_SteelType.ToString() == "2")
                {
                    return "不锈钢";
                }
                else if (STE_SteelType.ToString() == "3")
                {
                    return "鉻钼钢";
                }
                else
                {
                    return "其他";
                }
            }
        } 
        #endregion

        #region 保存按钮事件
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                //材质
                bool res = false;              
                int rowsCount = this.gvBS_Steel.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    CheckBox ckbBS_Steel = (CheckBox)(this.gvBS_Steel.Rows[i].FindControl("ckbBS_Steel"));
                    if (ckbBS_Steel.Checked == true)
                    {
                        res = true;
                        break;
                    }
                }
                if (!res)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('没有选中任何材质操作项！')", true);
                    return;
                }
                //焊接方法
                bool weldMethodRes = false;
                int weldMethodRowCount = this.gvWeldMethod.Rows.Count;
                for (int i = 0; i < weldMethodRowCount; i++)
                {
                    CheckBox ckbBS_WeldMethod = (CheckBox)(this.gvWeldMethod.Rows[i].FindControl("ckbBS_WeldMethod"));
                    if (ckbBS_WeldMethod.Checked == true)
                    {
                        weldMethodRes = true;
                        break;
                    }
                }
                if (!weldMethodRes)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('没有选中任何焊接方法操作项！')", true);
                    return;
                }

                Model.BS_Welder welder = new Model.BS_Welder();
                welder.WED_ID = WED_ID;
                welder.ProjectId = this.CurrUser.ProjectId;
               
                BLL.PersonManageService.UpdateBSWelderItem(welder);
                BLL.PersonItemService.DeleteItemByWenId(this.WED_ID);

                for (int i = 0; i < rowsCount; i++)
                {
                    CheckBox ckbBS_Steel = (CheckBox)(this.gvBS_Steel.Rows[i].FindControl("ckbBS_Steel"));
                    if (ckbBS_Steel.Checked == true)
                    {
                        Label lblBS_Steel = ((Label)(this.gvBS_Steel.Rows[i].FindControl("lblBS_Steel")));
                        TextBox txtThicknessMin = ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMin")));
                        TextBox txtThicknessMax = ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtThicknessMax")));
                        TextBox txtSizesMin = ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMin")));
                        TextBox txtSizesMax = ((TextBox)(this.gvBS_Steel.Rows[i].FindControl("txtSizesMax")));

                        Model.BS_WelderItem welderItem = new Model.BS_WelderItem();

                        welderItem.WEDItem_ID = SQLHelper.GetNewID(typeof(Model.BS_WelderItem));
                        welderItem.WED_ID = this.WED_ID;
                        welderItem.STE_ID = lblBS_Steel.Text;

                        if (txtThicknessMin.Text != string.Empty)
                        {
                            welderItem.ThicknessMin = Convert.ToDecimal(txtThicknessMin.Text.Trim());
                        }
                        else
                        {
                            welderItem.ThicknessMin = null;
                        }

                        if (txtThicknessMax.Text != string.Empty)
                        {
                            welderItem.ThicknessMax = Convert.ToDecimal(txtThicknessMax.Text.Trim());
                        }
                        else
                        {
                            welderItem.ThicknessMax = null;
                        }

                        if (txtSizesMin.Text != string.Empty)
                        {
                            welderItem.SizesMin = Convert.ToDecimal(txtSizesMin.Text.Trim());
                        }
                        else
                        {
                            welderItem.SizesMin = null;
                        }

                        if (txtSizesMax.Text != string.Empty)
                        {
                            welderItem.SizesMax = Convert.ToDecimal(txtSizesMax.Text.Trim());
                        }
                        else
                        {
                            welderItem.SizesMax = null;
                        }

                        BLL.PersonItemService.AddWelderItem(welderItem);
                    }
                }

                 BLL.WeldMethodItemService.DeleteWeldMethodItem(this.WED_ID);
                 for (int i = 0; i < weldMethodRowCount; i++)
                 {
                     CheckBox ckbBS_WeldMethod = (CheckBox)(this.gvWeldMethod.Rows[i].FindControl("ckbBS_WeldMethod"));
                     if (ckbBS_WeldMethod.Checked == true)
                     {
                         Label lblBS_WeldMethod = ((Label)(this.gvWeldMethod.Rows[i].FindControl("lblBS_WeldMethod")));
                         Model.BS_WeldMethodItem weldMethodItem = new Model.BS_WeldMethodItem();
                         weldMethodItem.WeldMethodItemId = SQLHelper.GetNewID(typeof(Model.BS_WeldMethodItem));
                         weldMethodItem.WED_ID = this.WED_ID;
                         weldMethodItem.WME_ID = lblBS_WeldMethod.Text;
                         BLL.WeldMethodItemService.AddWeldMethodItem(weldMethodItem);
                     }
                 }
                 this.gvBS_Steel.DataBind();
                 this.gvWeldMethod.DataBind();
                 ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');window.opener.location=window.opener.location;OnClientClick=window.close();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        /// <summary>
        /// 绑定gvWeldMethod焊接方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldMethod_DataBound(object sender, EventArgs e)
        {
            //if (this.gvWeldMethod.BottomPagerRow == null)
            //{
            //    return;
            //}

            //((Web.Controls.GridNavgator)this.gvWeldMethod.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvWeldMethod;

            int rowsCount = this.gvWeldMethod.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Label labWeldMethod = ((Label)(this.gvWeldMethod.Rows[i].FindControl("lblBS_WeldMethod")));
                if (BLL.WeldMethodItemService.IsInBS_WeldMethodItem(this.WED_ID, labWeldMethod.Text))
                {
                    ((CheckBox)(this.gvWeldMethod.Rows[i].FindControl("ckbBS_WeldMethod"))).Checked = true;
                }
            }
        }
        /// <summary>
        /// 全选焊接方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckbWeldMethodAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbWeldMethodAll = (CheckBox)(this.gvWeldMethod.HeaderRow.FindControl("ckbWeldMethodAll"));

            int rowsCount = this.gvWeldMethod.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                ((CheckBox)(this.gvWeldMethod.Rows[i].FindControl("ckbBS_WeldMethod"))).Checked = ckbWeldMethodAll.Checked;
            }          
        }

      
    }
}