using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class ShowHotHardSearchService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.View_HotHardSearch> qq = from x in db.View_HotHardSearch 
                                                                  orderby x.ISO_IsoNo ,x.JOT_JointNo 
                                                                  select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string[] checkList)
        {
            IQueryable<Model.View_HotHardSearch> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            if (checkList != null)
            {
                q = q.Where(e => checkList.ToList().Contains(e.HotProessId));
            }

            if (q.Count() == 0)
            {
                return new object[] { "" };
            }
            return from x in q
                   select new
                   {
                       x.JOT_ID,
                       x.ISO_ID,
                       x.ISO_IsoNo,
                       x.BSU_ID,
                       x.WorkAreaId,
                       x.ProjectId,
                       x.HotProessId,
                       x.HotProessNo,
                       x.JOT_JointNo,                       
                   };
        }
    }
}
