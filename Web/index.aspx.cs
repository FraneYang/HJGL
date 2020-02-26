using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;

namespace Web
{
    public partial class index : PPage
    {
        protected string unitSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.CurrUser != null)
            {
                this.lblUserName.Text = this.CurrUser.UserName;
                if (!IsPostBack)
                {
                    if (this.CurrUser.ProjectId != null)
                    {
                        var pro = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId);
                        if (pro != null)
                        {
                            this.unitSet = pro.ProjectName;
                        }
                    }
                    else
                    {
                        this.unitSet = "新建项目";
                    }

                    string projectId = Const.GLY;
                    if (this.CurrUser.ProjectId != null)
                    {
                        projectId = this.CurrUser.ProjectId;
                    }
                
                    sysMenuItem = Funs.DB.SpGetMenuByUserId(this.CurrUser.Account, projectId).ToList();
                    if (sysMenuItem.Count() > 0)
                    {
                        BoundTree(TreeView1.Nodes);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请您与管理员联系设置相应权限！')", true);
                    }
                }
            }
        }

        private List<Model.SpSysMenuItem> sysMenuItem = new List<Model.SpSysMenuItem>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetMenuString()
        {
            return CreateHTML();
        }

        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.SpSysMenuItem> GetNewMenu(string parentId)
        {                       
            return (from x in sysMenuItem where x.SuperMenu == parentId select x).ToList();;
        }

        public string CreateHTML()
        {
            StringBuilder sb = new StringBuilder();
            var menus = this.GetNewMenu("0");
            string _tempHtml;

            foreach (var item in menus)
            {
                sb.Append("{title:'" + item.MenuName + "',autoScroll:true,border:false,iconCls:'nav',");
                var sonMenu = this.GetNewMenu(item.MenuId);
                if (sonMenu.Count > 0)
                {
                    _tempHtml = "<ul class=\"LeftNav\">";
                    foreach (var sonItem in sonMenu)
                    {
                        if (!sonItem.Url.Contains("index.aspx"))
                        {
                            _tempHtml += "<li><a target=\"main\" href=\"" + sonItem.Url + "\"> " + sonItem.MenuName + "</a></li>";
                        }
                        else
                        {
                            _tempHtml += "<li><a href=\"" + ResolveUrl(sonItem.Url) + "\"> " + sonItem.MenuName + "</a></li>";
                        }
                    }

                    if (_tempHtml != "<ul>")
                    {
                        _tempHtml += "</ul>";
                        sb.Append("html:'" + _tempHtml + "'}");
                    }
                    else
                    {
                        sb.Append("html:''}");
                    }
                }
                else
                {
                    sb.Append("html:''}");
                }

                sb.Append(",");
            }

            return sb.ToString().TrimEnd(',');
        }
        
        private void BoundTree(TreeNodeCollection nodes)
        {
            var  dt = GetNewMenu("0");
            if (dt != null)
            {
                TreeNode tn = null;              
                TreeNode _tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode();
                    tn.Text = dr.MenuName;
                    tn.Value = dr.MenuId;
                    tn.NavigateUrl = dr.Url;
                    tn.Target = "main";
                    tn.SelectAction = TreeNodeSelectAction.SelectExpand;

                    nodes.Add(tn);

                    var _dt = GetNewMenu(dr.MenuId);
                    if (_dt.Count > 0)
                    {
                        foreach (var _dr in _dt)
                        {
                            _tn = new TreeNode();
                            _tn.Text = _dr.MenuName;
                            _tn.NavigateUrl = _dr.Url;
                            _tn.Target = "main";
                            _tn.Value = _dr.MenuId;
                            _tn.SelectAction = TreeNodeSelectAction.SelectExpand;
                            tn.ChildNodes.Add(_tn);
                        }
                    }
                }
            }
        }
    }
}