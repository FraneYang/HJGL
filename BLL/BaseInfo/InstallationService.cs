using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class InstallationService
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
        private static IQueryable<Model.Base_Installation> qq = from x in db.Base_Installation orderby x.InstallationId select x;

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string searchItem, string searchValue, string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Base_Installation> q = qq;
            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    q = q.Where(e => e.InstallationName.Contains(searchValue));
                }
            }
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
                       x.InstallationId,
                       x.InstallationName,
                       x.InstallationCode,
                       x.Def
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int getListCount(string searchItem, string searchValue, string projectId)
        {
            return count;
        }

        /// <summary>
        /// 是否存在装置名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistInstallationName(string InstallationName,string projectId)
        {
            var q = from x in Funs.DB.Base_Installation where x.InstallationName == InstallationName && x.ProjectId == projectId select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据名称获取装置
        /// </summary>
        /// <param name="InstallationName">名称</param>
        /// <returns></returns>
        public static Model.Base_Installation GetInstallationByName(string InstallationName)
        {
            return (from x in Funs.DB.Base_Installation where x.InstallationName == InstallationName select x).FirstOrDefault();
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="InstallationId"></param>
        /// <returns></returns>
        public static Model.Base_Installation GetInstallationByInstallationId(string InstallationId)
        {
            return Funs.DB.Base_Installation.FirstOrDefault(e => e.InstallationId.ToString() == InstallationId);
        }

        /// <summary>
        /// 添加装置
        /// </summary>
        /// <param name="Installation"></param>
        public static void AddInstallation(Model.Base_Installation Installation)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Installation newInstallation = new Model.Base_Installation();
            newInstallation.InstallationId = BLL.SQLHelper.GetMaxId("Base_Installation", "InstallationId");
            newInstallation.InstallationCode = Installation.InstallationCode;
            newInstallation.InstallationName = Installation.InstallationName;
            newInstallation.Def = Installation.Def;
            newInstallation.IsUsed = Installation.IsUsed;
            newInstallation.ProjectId = Installation.ProjectId;

            db.Base_Installation.InsertOnSubmit(newInstallation);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改装置
        /// </summary>
        /// <param name="Installation"></param>
        public static void UpdateInstallation(Model.Base_Installation Installation)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Installation newInstallation = db.Base_Installation.First(e => e.InstallationId == Installation.InstallationId);
            newInstallation.InstallationCode = Installation.InstallationCode;
            newInstallation.InstallationName = Installation.InstallationName;
            newInstallation.Def = Installation.Def;
            newInstallation.IsUsed = Installation.IsUsed;
            newInstallation.ProjectId = Installation.ProjectId;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据装置Id删除一个装置信息
        /// </summary>
        /// <param name="InstallationId"></param>
        public static void DeleteInstallation(string InstallationId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Installation Installation = db.Base_Installation.First(e => e.InstallationId.ToString() == InstallationId);
            db.Base_Installation.DeleteOnSubmit(Installation);
            db.SubmitChanges();
        }

        /// <summary>
        /// 工作区域下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchList()
        {
            ListItem[] lis = new ListItem[1];
            lis[0] = new ListItem("装置名称", "InstallationName");
            return lis;
        }

        /// <summary>
        /// 获取装置/单元名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetInstallationList(string projectId)
        {
            var q = (from x in Funs.DB.Base_Installation where x.ProjectId == projectId orderby x.InstallationId select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].InstallationName ?? "", q[i].InstallationId.ToString());
            }
            return item;
        }

        public static ListItem[] GetInstallationList(string projectId,string unitId)
        {
            var q = (from x in BLL.Funs.DB.Base_Installation
                     join y in BLL.Funs.DB.Base_WorkArea on x.InstallationId equals y.InstallationId
                     where x.ProjectId == projectId && y.UnitId == unitId
                     orderby x.InstallationId
                     select x).Distinct().ToList();//q = (from x in Funs.DB.Base_Installation where x.ProjectId == projectId orderby x.InstallationId select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].InstallationName ?? "", q[i].InstallationId.ToString());
            }
            return item;
        }

        public static ListItem[] GetInstallationBySupervisorUnitIdList(string projectId, string unitId, string supervisorUnitId)
        {
            var q = (from x in BLL.Funs.DB.Base_Installation
                     join y in BLL.Funs.DB.Base_WorkArea on x.InstallationId equals y.InstallationId
                     where x.ProjectId == projectId && y.UnitId == unitId && y.SupervisorUnitId == supervisorUnitId
                     orderby x.InstallationId
                     select x).Distinct().ToList();//q = (from x in Funs.DB.Base_Installation where x.ProjectId == projectId orderby x.InstallationId select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].InstallationName ?? "", q[i].InstallationId.ToString());
            }
            return item;
        }
    }
}
