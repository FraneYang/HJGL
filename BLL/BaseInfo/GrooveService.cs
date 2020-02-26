using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 坡口类型
    /// </summary>
    public static class GrooveService
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
        private static IQueryable<Model.BS_SlopeType> qq = from x in db.BS_SlopeType orderby x.JST_Code select x;


        /// <summary>
        /// 获取坡口类型
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_SlopeType> q = qq;
            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.JST_Code)
                    {
                        q = q.Where(e => e.JST_Code.Contains(searchValue));
                    }
                    if (searchItem == BLL.Const.JST_Name)
                    {
                        q = q.Where(e => e.JST_Name.Contains(searchValue));
                    }
                }
            }
            
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.JST_ID,
                       x.JST_Code,
                       x.JST_Name,
                       x.JST_Remark
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
        /// 根据ID获取坡口类型
        /// </summary>
        /// <param name="jst_id"></param>
        /// <returns></returns>
        public static Model.BS_SlopeType GetSlopeTypeByJSTID(string jst_id)
        {
            return Funs.DB.BS_SlopeType.FirstOrDefault(e => e.JST_ID == jst_id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="slopeType"></param>
        public static void AddSlopeType(Model.BS_SlopeType slopeType)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_SlopeType newSlopeType = new Model.BS_SlopeType();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_SlopeType));

            newSlopeType.JST_ID = newKeyID;
            newSlopeType.JST_Code = slopeType.JST_Code;
            newSlopeType.JST_Name = slopeType.JST_Name;
            newSlopeType.JST_Remark = slopeType.JST_Remark;

            db.BS_SlopeType.InsertOnSubmit(newSlopeType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="slopeType"></param>
        public static void UpdateSlopeType(Model.BS_SlopeType slopeType)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_SlopeType newSlopeType = db.BS_SlopeType.FirstOrDefault(e => e.JST_ID == slopeType.JST_ID);

            newSlopeType.JST_Code = slopeType.JST_Code;
            newSlopeType.JST_Name = slopeType.JST_Name;
            newSlopeType.JST_Remark = slopeType.JST_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="jst_id"></param>
        public static void DeleteSlopeType(string jst_id)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_SlopeType newSlopeType = db.BS_SlopeType.FirstOrDefault(e => e.JST_ID == jst_id);
            db.BS_SlopeType.DeleteOnSubmit(newSlopeType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的直径代号
        /// </summary>
        /// <param name="jst_code"></param>
        /// <returns></returns>
        public static bool IsExitJSTCode(string jst_code)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_SlopeType where x.JST_Code == jst_code select x;

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
            list[0] = new ListItem("坡口代号", BLL.Const.JST_Code);
            list[1] = new ListItem("坡口名称", BLL.Const.JST_Name);

            return list;
        }

        /// <summary>
        /// 根据坡口代码获取坡口类型信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_SlopeType GetSlopeTypeBySlopeTypeCode(string slopeTypeCode)
        {
            return Funs.DB.BS_SlopeType.FirstOrDefault(x => x.JST_Code == slopeTypeCode);
        }

        /// <summary>
        /// 获取坡口类型下拉框方法
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSlopeTypeNameList()
        {
            var q = (from x in Funs.DB.BS_SlopeType orderby x.JST_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].JST_Name ?? "", q[i].JST_ID.ToString());
            }
            return list;
        }
    }
}
