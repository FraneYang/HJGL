using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public static class WelderScoreService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.BS_WelderScore> qq = from x in db.BS_WelderScore select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string wed_id,int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_WelderScore> q = qq;
            if (!string.IsNullOrEmpty(wed_id))
            {
                q = q.Where(e => e.WED_ID == wed_id);
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.WelderScoreId,
                       x.WED_ID,
                       WED_Name = (from y in db.BS_Welder where y.WED_ID == x.WED_ID select y.WED_Name).First(),
                       x.ProjectName,
                       x.UnitName,
                       x.TotalJot,
                       x.QualifiedJot,
                       x.WeldRange,
                       x.Remark
                   };
        }

        /// <summary>
        /// 获取分页列表数
        /// </summary>
        /// <returns></returns>
        public static int GetListCount(string wed_id)
        {
            return count;
        }

        /// <summary>
        /// 添加焊工业绩
        /// </summary>
        /// <param name="welderScore"></param>
        public static void AddWelderScore(Model.BS_WelderScore welderScore)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WelderScore newWelderScore = new Model.BS_WelderScore();

            newWelderScore.WelderScoreId = welderScore.WelderScoreId;
            newWelderScore.WED_ID = welderScore.WED_ID;
            newWelderScore.ProjectName = welderScore.ProjectName;
            newWelderScore.UnitName = welderScore.UnitName;
            newWelderScore.TotalJot = welderScore.TotalJot;
            newWelderScore.QualifiedJot = welderScore.QualifiedJot;
            newWelderScore.WeldRange = welderScore.WeldRange;
            newWelderScore.Remark = welderScore.Remark;

            db.BS_WelderScore.InsertOnSubmit(newWelderScore);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改焊工业绩信息
        /// </summary>
        /// <param name="welderScore"></param>
        public static void UpdateWelderScore(Model.BS_WelderScore welderScore)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WelderScore newWelderScore = db.BS_WelderScore.FirstOrDefault(e => e.WelderScoreId == welderScore.WelderScoreId);

            newWelderScore.WED_ID = welderScore.WED_ID;
            newWelderScore.ProjectName = welderScore.ProjectName;
            newWelderScore.UnitName = welderScore.UnitName;
            newWelderScore.TotalJot = welderScore.TotalJot;
            newWelderScore.QualifiedJot = welderScore.QualifiedJot;
            newWelderScore.WeldRange = welderScore.WeldRange;
            newWelderScore.Remark = welderScore.Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据焊工业绩Id删除焊工业绩
        /// </summary>
        /// <param name="welderScoreId"></param>
        public static void DeleteWelderScore(string welderScoreId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WelderScore welderScore = db.BS_WelderScore.FirstOrDefault(e => e.WelderScoreId == welderScoreId);
            db.BS_WelderScore.DeleteOnSubmit(welderScore);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据焊工Id删除所有相关的焊工业绩信息
        /// </summary>
        /// <param name="wed_id"></param>
        public static void DeleteWelderScoreBywed_id(string wed_id)
        {
            Model.HJGLDB db = Funs.DB;
            var q = (from x in db.BS_WelderScore where x.WED_ID == wed_id select x);
            db.BS_WelderScore.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据焊工业绩Id获取焊工业绩
        /// </summary>
        /// <param name="wed_id"></param>
        /// <returns></returns>
        public static Model.BS_WelderScore GetWelderScoreByWelderScoreId(string welderScoreId)
        {
            return Funs.DB.BS_WelderScore.FirstOrDefault(e => e.WelderScoreId == welderScoreId);
        }
    }
}
