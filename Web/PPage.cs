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
    /// ҳ�����
    /// </summary>
    public class PPage : System.Web.UI.Page
    {
        /// <summary>
        /// ��ǰ��¼����Ϣ��
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
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(PPage_Load);
            this.Unload += new EventHandler(this.PPage_UNLoad);
            base.OnInit(e);
        } 

        /// <summary>
        /// ҳ���¼�ɹ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PPage_Load(object sender, EventArgs e)
        {
            //���Ǻ���ʽ��Ȩ�޹������.
            //ҳ��װ������Ժ�ż���Ƿ���Ȩ�޴򿪴�ҳ....
            //anyway,its ok.

            this.Title = "ŵ�ش�ܵ����ӹ���ϵͳV3.0";
            if (CurrUser == null)
            {
                if (this.Page.Request.AppRelativeCurrentExecutionFilePath != "~/Login.aspx")
                    Response.Redirect("~/Login.aspx");
            } 
        }

        /// <summary>
        /// UNLOAD�¼���������ҳ��װ��˳������
        /// �����ﴦ�����DBLIST�����ݿ������ֵ䡣
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
        /// �����FRAME��ת���µ�ַ
        /// </summary>
        /// <param name="urlstr">URL</param>
        public static void ZXRefresh(string urlstr)
        {
            System.Web.HttpContext.Current.Response.Write("<script>top.location.href='" + "/" + urlstr.TrimStart('/') + "';</script>");
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ������һ���ɻ�ý���Ŀؼ���λ��
        /// </summary>
        /// <param name="searchBeginIndex">������ʼλ��</param>
        /// <param name="controls">�����ؼ�����</param>
        /// <returns>�ɻ�ý���Ŀؼ���λ��</returns>
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
        /// ����һϵ������ؼ��Ľ���˳��
        /// </summary>
        /// <param name="isCirculate">�Ƿ����Ŀؼ��س���λ����һ���ؼ�(ѭ��)</param>
        /// <param name="controls">����ؼ�����</param>
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
        /// ����һϵ������ؼ��Ľ���˳��(��ѭ��ģʽ�����Ŀؼ��س��󲻻ᶨλ����һ���ؼ�)
        /// </summary>
        /// <param name="controls">����ؼ�����</param>
        protected void SetNextFocus(params Control[] controls)
        {
            PPage.SetNextFocus(false, controls);
        }

        /// <summary>
        /// ĳ���������ڵ�N������ؼ��س����Զ���ת��Ŀ�갴ť
        /// </summary>
        /// <param name="controlsHolder">����</param>
        /// <param name="targetControl">Ŀ������</param>
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
