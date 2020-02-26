using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class ShowTestPackageSearchService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.View_TestPackageSearch> qq = from x in db.View_TestPackageSearch
                                                         orderby x.ISO_IsoNo
                                                         select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string[] checkList, string isoNo)
        {
            IQueryable<Model.View_TestPackageSearch> q = qq;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            if (!String.IsNullOrEmpty(isoNo))
            {
                q = q.Where(e => e.ISO_IsoNo.Contains(isoNo));
            }
            if (checkList != null)
            {
                q = q.Where(e => checkList.ToList().Contains(e.BAW_ID));
            }

            return from x in q
                   select new
                   {
                       x.ISO_ID,
                       x.ISO_IsoNo,
                       x.ISO_IsoNumber,
                       x.ISO_Specification,
                       x.ISO_DesignPress,
                       x.ISO_DesignTemperature,
                       x.ISO_TestPress,
                       x.ISO_TestTemperature,
                       x.ISO_NDTClass,
                       x.SER_Name
                   };
        }
    }
}
