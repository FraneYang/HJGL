using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class ShowTrustSearchService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 定义变量
        /// </summary>
        //private static IQueryable<Model.View_CH_TrustSearch> qq = from x in db.View_CH_TrustSearch 
        //                                                          orderby x.ISO_IsoNo ,x.JOT_JointNo 
        //                                                          select x;

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string[] checkList, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_CH_TrustSearch> q = from x in db.View_CH_TrustSearch
                                                      where x.ProjectId == projectId
                                                      orderby x.ISO_IsoNo, x.JOT_JointNo
                                                      select x;
            q = q.Where(e => checkList.Contains(e.PW_PointID));

          count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.JOT_ID,
                       x.ISO_ID,
                       x.ISO_IsoNo,
                       x.BSU_ID,
                       x.WorkAreaId,
                       x.ProjectId,
                       x.PW_PointID,
                       x.PW_PointNo,
                       x.PW_PointType,
                       x.JOT_JointNo,
                       JOT_JointStatus = (x.JOT_JointStatus =="101"?"点口":(x.JOT_JointStatus == "102"?"扩透":"")),
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId, string[] checkList)
        {
            return count;
        }
    }
}
