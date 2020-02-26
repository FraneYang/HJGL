using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

namespace Web
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public class PPage : System.Web.UI.Page
    {
        /// <summary>
        /// 当前登录人信息。
        /// </summary>
        public Model.Sys_User CurrUser
        {
            get
            {
                if (Session["CurrUser"] == null) return null;
                return (Model.Sys_User)Session["CurrUser"];                
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(PPage_Load);
            this.Unload += new EventHandler(this.PPage_UNLoad);
            base.OnInit(e);
        } 

        /// <summary>
        /// 页面登录成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PPage_Load(object sender, EventArgs e)
        {
            //这是后置式的权限管理策略.
            //页面装载完成以后才检查是否有权限打开此页....
            //anyway,its ok.

            this.Title = "诺必达管道焊接管理系统V3.0";
            if (CurrUser == null)
            {
                if (this.Page.Request.AppRelativeCurrentExecutionFilePath != "~/Login.aspx")
                    Response.Redirect("~/Login.aspx");
            } 
        }

        /// <summary>
        /// UNLOAD事件，发生在页面装载顺序的最后。
        /// 在这里处理的是DBLIST，数据库连接字典。
        /// </summary>
        /// <param name="sender">S</param>
        /// <param name="e">E</param>
        protected void PPage_UNLoad(object sender, EventArgs e)
        {
            if (BLL.Funs.DBList.ContainsKey(System.Threading.Thread.CurrentThread.ManagedThreadId))
            {
                BLL.Funs.DBList.Remove(System.Threading.Thread.CurrentThread.ManagedThreadId);
            }

            if (BLL.SQLHelper.GetConn().State == ConnectionState.Open)
            {
                BLL.SQLHelper.GetConn().Close();
            }
        }

        /// <summary>
        /// 最外层FRAME跳转到新地址
        /// </summary>
        /// <param name="urlstr">URL</param>
        public static void ZXRefresh(string urlstr)
        {
            System.Web.HttpContext.Current.Response.Write("<script>top.location.href='" + "/" + urlstr.TrimStart('/') + "';</script>");
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 搜索下一个可获得焦点的控件的位置
        /// </summary>
        /// <param name="searchBeginIndex">搜索起始位置</param>
        /// <param name="controls">搜索控件数组</param>
        /// <returns>可获得焦点的控件的位置</returns>
        private static int? GetNextCanForcusControl(int searchBeginIndex, params Control[] controls)
        {
            if (controls != null && searchBeginIndex < controls.Length)
            {
                while (searchBeginIndex < controls.Length)
                {
                    HtmlControl htmlControl = controls[searchBeginIndex] as HtmlControl;
                    if (htmlControl != null)
                    {
                        if ((!htmlControl.Visible) || htmlControl.Disabled)
                        {
                            searchBeginIndex++;
                            continue;
                        }
                        else
                        {
                            return searchBeginIndex;
                        }
                    }

                    WebControl webControl = controls[searchBeginIndex] as WebControl;
                    if (webControl != null)
                    {
                        if ((!webControl.Visible) || (!webControl.Enabled))
                        {
                            searchBeginIndex++;
                            continue;
                        }
                        else
                        {
                            return searchBeginIndex;
                        }
                    }

                    searchBeginIndex++;
                }
            }

            return null;
        }


        /// <summary>
        /// 设置一系列输入控件的焦点顺序
        /// </summary>
        /// <param name="isCirculate">是否最后的控件回车后定位到第一个控件(循环)</param>
        /// <param name="controls">输入控件序列</param>
        public static void SetNextFocus(bool isCirculate, params Control[] controls)
        {
            int? nextFocusControlIndex;
            int theEndFocusIndex = 0;

            for (int i = 0; i < controls.Length - 1; )
            {
                IAttributeAccessor c = controls[i] as IAttributeAccessor;
                if (c != null)
                {
                    nextFocusControlIndex = PPage.GetNextCanForcusControl(i + 1, controls);
                    if (nextFocusControlIndex != null)
                    {
                        c.SetAttribute("onkeydown", "fnNextFocus('" + controls[nextFocusControlIndex.Value].ClientID + "',event);");
                        i = theEndFocusIndex = nextFocusControlIndex.Value;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    i++;
                }
            }

            if (isCirculate && theEndFocusIndex > 0)
            {
                nextFocusControlIndex = PPage.GetNextCanForcusControl(0, controls);
                if (nextFocusControlIndex != null)
                {
                    ((IAttributeAccessor)controls[theEndFocusIndex]).SetAttribute("onkeydown", "fnNextFocus('" + controls[nextFocusControlIndex.Value].ClientID + "',event);");
                }
            }
        }

        /// <summary>
        /// 设置一系列输入控件的焦点顺序(非循环模式，最后的控件回车后不会定位到第一个控件)
        /// </summary>
        /// <param name="controls">输入控件序列</param>
        protected void SetNextFocus(params Control[] controls)
        {
            PPage.SetNextFocus(false, controls);
        }

        /// <summary>
        /// 某给定区域内的N个输入控件回车后，自动跳转到目标按钮
        /// </summary>
        /// <param name="controlsHolder">区域</param>
        /// <param name="targetControl">目标区域</param>
        protected void SetDefaltButton(Control controlsHolder, Control targetControl)
        {
            string setNextFocus = string.Format(
                "setNextFocus(document.getElementById('{0}'),document.getElementById('{1}'));",
                Convert.ToString(controlsHolder.ClientID),
                Convert.ToString(targetControl.ClientID));

            if (ScriptManager.GetCurrent(this.Page) == null)
            {
                this.Page.ClientScript.RegisterStartupScript(
                        this.Page.GetType(),
                        "Key_SetNextFocus_" + Convert.ToString(controlsHolder.ClientID),
                        setNextFocus,
                        true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                        this.Page,
                        this.Page.GetType(),
                        "Key_SetNextFocus_" + Convert.ToString(controlsHolder.ClientID),
                        setNextFocus,
                        true);
            }
        }

        public void InitTree(Page page, TreeView tv)
        {
            string tvID = tv.ClientID;
            string postBackEventRef = page.GetPostBackEventReference(tv);
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript'>\n<!--\n");
            sb.Append("function " + tvID + "_initTree(){\n");
            sb.Append(tvID + ".onSelectedIndexChange=function(){\n");
            sb.Append("if(event.newTreeNodeIndex !=event.oldTreeNodeIndex){\n");
            sb.Append("this.queueEvent('onselectedindexchange', event.oldTreeNodeIndex + ',' + event.newTreeNodeIndex);}\n");
            sb.Append("window.setTimeout('" + postBackEventRef.Replace("'", "\"") + "\',0)}}\n");
            sb.Append("-->\n</script>");
            if (!page.IsClientScriptBlockRegistered("TreeView" + tvID))
            {
                page.RegisterClientScriptBlock("TreeView" + tvID, sb.ToString());
            }
        } 
    }
}
