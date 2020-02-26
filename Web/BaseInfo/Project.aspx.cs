using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Project : PPage
    {
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        /// <summary>
        /// 操作状态：增加、修改、删除
        /// </summary>
        public string OperateState
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.CurrUser.Account == BLL.Const.GLY)
                {
                    this.btnAdd.Visible = true;
                    this.btncancel.Visible = false;
                    this.btnSave.Visible = false;
                }
                else
                {
                    this.btnAdd.Visible = false;
                    this.btncancel.Visible = false;
                    this.btnSave.Visible = false;
                }

            }
        }

        /// <summary>
        /// 增加项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            
                this.divSearch.Visible = true;
                this.btnSave.Visible = true;
                this.btncancel.Visible = true;

                ProjectId = string.Empty;
                this.txtProjectCode.Text = string.Empty;
                this.txtProjectName.Text = string.Empty;
                this.txtStartDate.Text = string.Empty;
                this.txtProjectAddress.Text = string.Empty;
                this.txtRemark.Text = string.Empty;

                this.OperateState = BLL.Const.Add;
                this.txtProjectCode.Focus();
                this.ProjectGridView.DataBind();
           
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
                Model.Base_Project project = new Model.Base_Project();
                project.ProjectCode = this.txtProjectCode.Text.Trim();
                project.ProjectName = this.txtProjectName.Text.Trim();
                project.StartDate = Convert.ToDateTime(this.txtStartDate.Text);
                project.ProjectAddress = this.txtProjectAddress.Text.Trim();
                project.Remark = this.txtRemark.Text.Trim();
            
               
                if (OperateState == BLL.Const.Add)
                {
                    string newKeyID = SQLHelper.GetNewID(typeof(Model.Base_Project));
                    project.ProjectId = newKeyID;
                    BLL.ProjectService.AddProject(project);

                    // 给新项目添加一个管理员
                    Model.Sys_User user = new Model.Sys_User();
                    user.Account = "admin";
                    user.Password = "B59C67BF196A4758191E42F76670CEBA";
                    user.ProjectId = newKeyID;
                    user.UserName = "管理员";
                    user.IsPost = true;
                    BLL.UserService.AddUser(user);

                    // 给新项目拷贝一份报表
                    string reportSql = "insert into dbo.ReportServer select ReportId,TabContent, ReportName,InitTabContent,'" + newKeyID + "'  from dbo.ReportServer where projectId='0'";
                    BLL.SQLHelper.ExecutSql(reportSql);

                    // 给新项目拷贝一份系统环境设置
                    string setSql = "insert into dbo.Sys_Set select SetId,SetName,IsAuto,SetValue,'" + newKeyID + "'  from dbo.Sys_Set where ProjectId='0'";
                    BLL.SQLHelper.ExecutSql(setSql);

                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加项目信息");
                }

                if (OperateState == BLL.Const.Modify)
                {
                    project.ProjectId = ProjectId;
                    BLL.ProjectService.UpdateProject(project);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改项目信息");
                }

                this.ProjectGridView.DataBind();
                this.divSearch.Visible = false;
                this.btnSave.Visible = false;
                this.btncancel.Visible = false;
                this.ProjectGridView.Visible = true;
            
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.ProjectGridView.Visible = true;
            this.divSearch.Visible = false;
            this.btnSave.Visible = false;
            this.btncancel.Visible = false;
        }

        /// <summary>
        /// 绑定参数每次执行Select()和SelectCount前都会引发一次该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProjectGridView_DataBound(object sender, EventArgs e)
        {
            if (this.ProjectGridView.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.ProjectGridView.BottomPagerRow.FindControl
                ("GridNavgator1")).GridView = this.ProjectGridView;
        }

        /// <summary>
        /// 点击每行的GridView的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProjectGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProjectId = e.CommandArgument.ToString();

            if (e.CommandName == "ProjectName")
            {
                this.divSearch.Visible = true;
                if (this.CurrUser.Account == BLL.Const.GLY)
                {
                    this.btnSave.Visible = true;
                }
                else
                {
                    this.btnSave.Visible = false;
                }
                this.OperateState = BLL.Const.Modify;
                this.ProjectGridView.Visible = false;

                Model.Base_Project pro = BLL.ProjectService.GetProjectByProjectId(e.CommandArgument.ToString());
                this.txtProjectCode.Text = pro.ProjectCode;
                this.txtProjectName.Text = pro.ProjectName;
                this.txtProjectAddress.Text = pro.ProjectAddress;
                this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", pro.StartDate);
                this.txtRemark.Text = pro.Remark;


            }

            if (e.CommandName == "ProjectDelete")
            {
                if (this.CurrUser.Account == BLL.Const.GLY)
                {
                    if (judgementDelete())
                    {
                        var q = from x in BLL.Funs.DB.Sys_User where x.ProjectId == ProjectId && x.Account == BLL.Const.AdminId select x;
                        if (q.Count() > 0)
                        {
                            BLL.UserService.DeleteUser(q.First().UserId);
                        }
                        this.ProjectGridView.DataBind();

                        BLL.ProjectService.DeleteProject(ProjectId);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除项目");
                        this.ProjectGridView.DataBind();
                    }

                    if (this.CurrUser.Account == BLL.Const.GLY)
                    {
                        this.btnAdd.Visible = true;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }

        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
           
            if (BLL.UnitService.GetUnitCountByProjectId(ProjectId) > 0)
            {
                content = "单位中已经使用了该项目，不能删除！";
            }
           
            if (content == "")
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + content + "')", true);
                return false;
            }
        }

    }
}