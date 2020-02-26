using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 焊口信息初始化
    /// </summary>
    public static class ShowJointInfoViewService
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
        private static IQueryable<Model.View_JointInfo> qq = from x in db.View_JointInfo orderby x.JOT_JointNo select x;

        /// <summary>
        /// 获取焊口列表
        /// </summary>
        /// <param name="iso_id">管线</param>
        /// <param name="projectId">项目id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string iso_id,string projectId,int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_JointInfo> q = qq;

            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            
            if (!string.IsNullOrEmpty(iso_id))
            {
                q = q.Where(e => e.ISO_ID == iso_id);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.ISO_ID,
                       x.JOT_ID,
                       x.JOT_JointNo,
                       x.Is_hj,
                       x.JointStatus,
                       x.JOT_TrustFlag,
                       x.JOT_CheckFlag,
                       x.JOT_JointAttribute,
                       x.JOT_WeldDate,
                       x.WED_Name1,
                       x.WED_Name2,
                   };
        }

        /// <summary>
        /// 获取焊口列表数
        /// </summary>
        /// <param name="iso_id">管线</param>
        /// <param name="projectId">项目</param>  
        /// <returns></returns>
        public static int GetListCount(string iso_id, string projectId)
        {
            return count;
        }
    }
}
