using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class RolePower : PPage
    {
        /// <summary>
        /// 角色主键
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
        /// 页面登录成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.RoleBindData();
                this.MenuBindData();

                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.RolePowerMenuId);
            }
        }

        #region 加载树



        /// <summary>
        /// 加载角色树：动态加载

        /// </summary>
        private void RoleBindData()
        {
            this.RoleTree.Nodes.Clear();

            TreeNode rootNode = new TreeNode();//定义根节点



            rootNode.Text = "角色";
            rootNode.Value = "0";

            this.RoleTree.Nodes.Add(rootNode);

            var role = from x in BLL.Funs.DB.Sys_Role where x.ProjectId == this.CurrUser.ProjectId orderby x.SortIndex select x;
            foreach (var q in role)
            {
                TreeNode roleNode = new TreeNode();
                roleNode.Text = q.RoleName;
                roleNode.Value = q.RoleId;
                rootNode.ChildNodes.Add(roleNode);
            }
        }

        /// <summary>
        /// 加载岗位树：动态加载 


        /// </summary>
        private void MenuBindData()
        {
            this.MenuTree.Nodes.Clear();

            TreeNode rootNode = new TreeNode();//定义根节点



            rootNode.Text = "系统功能";
            rootNode.Value = "0";

            this.MenuTree.Nodes.Add(rootNode);
            this.GetNodes(rootNode.ChildNodes, null);

        }

        #endregion

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            List<Model.Sys_Menu> menu = null;
            if (parentId == null)
            {
                menu = (from x in BLL.Funs.DB.Sys_Menu where x.SuperMenu == "0" orderby x.SortIndex select x).ToList();
            }
            else
            {
                menu = (from x in BLL.Funs.DB.Sys_Menu where x.SuperMenu == parentId orderby x.SortIndex select x).ToList();
            }

            foreach (var q in menu)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = q.MenuName;
                newNode.Value = q.MenuId;
                nodes.Add(newNode);

                if (q.SuperMenu != "0")
                {
                    var buttons = (from x in BLL.Funs.DB.ButtonToMenu where x.MenuId == q.MenuId orderby x.SortIndex select x).ToList();
                    foreach (var b in buttons)
                    {
                        TreeNode bt = new TreeNode();
                        bt.Text = b.ButtonName;
                        bt.Value = b.ButtonToMenuId;
                        newNode.ChildNodes.Add(bt);
                    }
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].ChildNodes, nodes[i].Value);
            }
        }

        /// <summary>
        /// 遍历获取有权限的节点
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="power">权限集合</param>
        private void GetCheckedNodes(TreeNodeCollection nodes, string[] powerToMenuId)
        {
            foreach (TreeNode tn in nodes)
            {
                tn.Checked = false;
                tn.Expanded = false;
                //foreach (var m in power)
                //{
                //    if (m.MenuId == tn.Value)
                //    {
                //        tn.Checked = true;
                //        tn.Expanded = true;
                //    }
                //}
                if (powerToMenuId.Contains(tn.Value))
                {
                    tn.Checked = true;
                    tn.Expanded = true;
                }
                if (tn.Depth < 4)
                {
                    if (tn.Depth == 3 && powerToMenuId.Contains(tn.Parent.Value))
                    {
                        var btpower = from x in BLL.Funs.DB.Sys_ButtonPower where x.RoleId == RoleId && x.MenuId == tn.Parent.Value select x.ButtonToMenuId;

                        if (btpower.Contains(tn.Value))
                        {
                            tn.Checked = true;
                        }

                    }
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetCheckedNodes(nodes[i].ChildNodes, powerToMenuId);
            }
        }
        #endregion

        /// <summary>
        /// 保存岗位授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!String.IsNullOrEmpty(RoleId))
                {
                    BLL.ButtonPowerService.DeleteButtonPower(RoleId);
                    BLL.RolePowerService.DeleteRolePower(RoleId);

                    foreach (TreeNode tn in this.MenuTree.CheckedNodes)
                    {
                        if (tn.Value != "0")
                        {
                            if (BLL.RolePowerService.IsExistMenu(tn.Value))
                            {
                                Model.Sys_RolePower newPower = new Model.Sys_RolePower();
                                newPower.RoleId = RoleId;
                                newPower.MenuId = tn.Value;
                                BLL.RolePowerService.SaveRolePower(newPower);
                            }

                            if (BLL.ButtonPowerService.isExistButtonToMenu(tn.Value))
                            {
                                Model.Sys_ButtonPower btn = new Model.Sys_ButtonPower();
                                btn.RoleId = RoleId;
                                btn.MenuId = tn.Parent.Value;
                                btn.ButtonToMenuId = tn.Value;
                                BLL.ButtonPowerService.SaveButtonPower(btn);
                            }
                        }
                    }

                    BLL.LogService.AddLog(this.CurrUser.UserId, "保存角色菜单授权");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择角色！')", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }

        }

        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            if (!String.IsNullOrEmpty(RoleId))
            {
                string[] powerToMenuId = BLL.RolePowerService.GetMenuIdByRoleId(RoleId);
                if (powerToMenuId != null)
                {
                    GetCheckedNodes(this.MenuTree.Nodes[0].ChildNodes, powerToMenuId);
                }
                else
                {

                    this.MenuBindData();
                    this.MenuTree.Nodes[0].Expanded = true;
                    this.MenuTree.ExpandDepth = 1;

                }
            }
        }

        /// <summary>
        /// 选择岗位节点发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PostTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (BLL.RoleService.IsExistRole(this.RoleTree.SelectedNode.Value))
            {
                this.lblText.Text = this.RoleTree.SelectedNode.Text.ToString();
                RoleId = this.RoleTree.SelectedNode.Value;
                RoleTree.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                RoleId = String.Empty;
            }

            if (!String.IsNullOrEmpty(RoleId))
            {
                string[] powerToMenuId = BLL.RolePowerService.GetMenuIdByRoleId(RoleId);
                if (powerToMenuId != null)
                {
                    GetCheckedNodes(this.MenuTree.Nodes[0].ChildNodes, powerToMenuId);
                }
                else
                {

                    this.MenuBindData();
                    this.MenuTree.Nodes[0].Expanded = true;
                    this.MenuTree.ExpandDepth = 1;

                }
            }
        }
    }
}