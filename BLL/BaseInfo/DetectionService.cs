using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 探伤比例
    /// </summary>
    public static class DetectionService
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
        private static IQueryable<Model.BS_NDTRate> qq = from x in db.BS_NDTRate orderby x.NDTR_Code select x;

        /// <summary>
        /// 获取探伤比例列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_NDTRate> q = qq;
            if (searchItem!="0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem==BLL.Const.NDTRCode)
                    {
                        q = q.Where(e => e.NDTR_Code.Contains(searchValue));
                    }
                    if (searchItem==BLL.Const.NDTRName)
                    {
                        q = q.Where(e => e.NDTR_Name.Contains(searchValue));
                    }
                }
            }
           
            count = q.Count();
            if (count==0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.NDTR_ID,
                       x.NDTR_Code,
                       x.NDTR_Name,
                       x.NDTR_Rate,
                       x.NDTR_Remark
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetListCount(string searchItem, string searchValue)
        {
            return count;
        }

        /// <summary>
        /// 根据探伤比例Id获取探伤比例
        /// </summary>
        /// <param name="ndtrId"></param>
        /// <returns></returns>
        public static Model.BS_NDTRate GetNDTRateByNDTRID(string ndtrId)
        {
            return Funs.DB.BS_NDTRate.FirstOrDefault(e => e.NDTR_ID == ndtrId);
        }

        /// <summary>
        /// 添加探伤比例
        /// </summary>
        /// <param name="ndtrate"></param>
        public static void AddNDTRate(Model.BS_NDTRate ndtrate)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_NDTRate newNDTRate = new Model.BS_NDTRate();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_NDTRate));
            newNDTRate.NDTR_ID = newKeyID;
            newNDTRate.NDTR_Code = ndtrate.NDTR_Code;
            newNDTRate.NDTR_Name = ndtrate.NDTR_Name;
            newNDTRate.NDTR_Rate = ndtrate.NDTR_Rate;
            newNDTRate.NDTR_Remark = ndtrate.NDTR_Remark;

            db.BS_NDTRate.InsertOnSubmit(newNDTRate);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ndtrate"></param>
        public static void UpdateNDTRate(Model.BS_NDTRate ndtrate)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_NDTRate newNDTRate = db.BS_NDTRate.First(e => e.NDTR_ID == ndtrate.NDTR_ID);
            newNDTRate.NDTR_Code = ndtrate.NDTR_Code;
            newNDTRate.NDTR_Name = ndtrate.NDTR_Name;
            newNDTRate.NDTR_Rate = ndtrate.NDTR_Rate;
            newNDTRate.NDTR_Remark = ndtrate.NDTR_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ndtrateId"></param>
        public static void DeleteNDTRate(string ndtrateId)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_NDTRate ndtrate = db.BS_NDTRate.First(e => e.NDTR_ID == ndtrateId);
            db.BS_NDTRate.DeleteOnSubmit(ndtrate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的探伤类型代号
        /// </summary>
        /// <param name="ndtrateCode"></param>
        /// <returns></returns>
        public static bool IsExitNDTRateCode(string ndtrateCode)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_NDTRate where x.NDTR_Code == ndtrateCode select x;

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
        /// 查询下拉列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchItem()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("探伤比例代号", BLL.Const.NDTRCode);
            list[1] = new ListItem("探伤比例名称", BLL.Const.NDTRName);

            return list;
        }

        /// <summary>
        /// 获取探伤比例下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetNDTRateNameList()
        {
            var q = (from x in Funs.DB.BS_NDTRate orderby x.NDTR_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].NDTR_Code + "-" + q[i].NDTR_Rate.ToString() + "%", q[i].NDTR_ID.ToString());
            }
            return list;
        }


        /// <summary>
        /// 根据探伤值获取探伤类型信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_NDTRate GetRateByRateName(string rateName)
        {
            return Funs.DB.BS_NDTRate.FirstOrDefault(x => x.NDTR_Name == rateName);
        }
    }
}
