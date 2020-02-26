namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Data.Linq;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using Model;
    using System.Collections;

    public static class ProjectService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.Base_Project> qq = from x in db.Base_Project  select x;

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Base_Project> q = qq;
            
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.ProjectId,
                       x.ProjectCode,
                       x.ProjectName,
                       x.ProjectAddress,
                       x.StartDate,
                       x.Remark,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId)
        {
            return count;
        }

        /// <summary>
        /// 获取数据列表数
        /// </summary>
        /// <returns></returns>
        public static int getCount()
        {
            return qq.Count();
        }

        /// <summary>
        ///获取项目信息
        /// </summary>
        /// <returns></returns>
        public static Model.Base_Project GetProjectByProjectId(string projectId)
        {
            return Funs.DB.Base_Project.FirstOrDefault(e => e.ProjectId == projectId);
        }

        /// <summary>
        /// 增加项目信息
        /// </summary>
        /// <returns></returns>
        public static void AddProject(Model.Base_Project project)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Project newProject = new Base_Project();
            newProject.ProjectId = project.ProjectId;
            newProject.ProjectCode = project.ProjectCode;
            newProject.ProjectName = project.ProjectName;
            newProject.ProjectAddress = project.ProjectAddress;
            newProject.StartDate = project.StartDate;
           
            newProject.Remark = project.Remark;

            db.Base_Project.InsertOnSubmit(newProject);
            db.SubmitChanges();
        }

        /// <summary>
        ///修改项目信息 
        /// </summary>
        /// <param name="project"></param>
        public static void UpdateProject(Model.Base_Project project)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Project newProject = db.Base_Project.First(e => e.ProjectId == project.ProjectId);
            newProject.ProjectCode = project.ProjectCode;
            newProject.ProjectName = project.ProjectName;
            newProject.ProjectAddress = project.ProjectAddress;
            newProject.StartDate = project.StartDate;
            newProject.Remark = project.Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据项目Id删除一个项目信息
        /// </summary>
        /// <param name="projectId"></param>
        public static void DeleteProject(string projectId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Project project = db.Base_Project.First(e => e.ProjectId == projectId);

            db.Base_Project.DeleteOnSubmit(project);
            db.SubmitChanges();
        }
        
        /// <summary>
        /// 获取项目项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetProjectList()
        {
            var q = (from x in Funs.DB.Base_Project orderby x.ProjectCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].ProjectCode + "-" + q[i].ProjectName ?? "", q[i].ProjectId.ToString());
            }
            return item;
        }
    }
}
