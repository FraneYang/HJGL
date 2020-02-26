using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 焊接方法
    /// </summary>
    public static class WeldingMethodService
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
        public static IQueryable<Model.BS_WeldMethod> qq = from x in db.BS_WeldMethod orderby x.WME_Code select x;

        /// <summary>
        /// 获取焊接方法
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_WeldMethod> q = qq;
            if (searchItem !="0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem==BLL.Const.WME_Code)
                    {
                        q = q.Where(e => e.WME_Code.Contains(searchValue));
                    }
                    if (searchItem==BLL.Const.WME_Name)
                    {
                        q = q.Where(e => e.WME_Name.Contains(searchValue));
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
                       x.WME_ID,
                       x.WME_Code,
                       x.WME_Name,
                       x.WME_Remark
                   };
        }

        /// <summary>
        /// 获取焊接方法列表数
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
        /// 根据焊接方法Id获取焊接方法信息
        /// </summary>
        /// <param name="wme_id"></param>
        /// <returns></returns>
        public static Model.BS_WeldMethod GetWeldMethodByWMEID(string wme_id)
        {
            return Funs.DB.BS_WeldMethod.FirstOrDefault(e => e.WME_ID == wme_id);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="weldMethod"></param>
        public static void AddWeldMethod(Model.BS_WeldMethod weldMethod)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_WeldMethod));
            Model.BS_WeldMethod newWeldMethod = new Model.BS_WeldMethod();

            newWeldMethod.WME_ID = newKeyID;
            newWeldMethod.WME_Code = weldMethod.WME_Code;
            newWeldMethod.WME_Name = weldMethod.WME_Name;
            newWeldMethod.WME_Remark = weldMethod.WME_Remark;

            db.BS_WeldMethod.InsertOnSubmit(newWeldMethod);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="weldMethod"></param>
        public static void UpdateWeldMethod(Model.BS_WeldMethod weldMethod)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WeldMethod newWeldMethod = db.BS_WeldMethod.FirstOrDefault(e => e.WME_ID == weldMethod.WME_ID);

            newWeldMethod.WME_Code = weldMethod.WME_Code;
            newWeldMethod.WME_Name = weldMethod.WME_Name;
            newWeldMethod.WME_Remark = weldMethod.WME_Remark;

            db.SubmitChanges();
        }
        
        /// <summary>
        /// 删除焊接方法
        /// </summary>
        /// <param name="wme_id"></param>
        public static void DeleteWeldMethod(string wme_id)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_WeldMethod weldMethod = db.BS_WeldMethod.FirstOrDefault(e => e.WME_ID == wme_id);

            db.BS_WeldMethod.DeleteOnSubmit(weldMethod);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的焊接方法代码
        /// </summary>
        /// <param name="wme_code"></param>
        /// <returns></returns>
        public static bool IsExitWMECode(string wme_code)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_WeldMethod where x.WME_Code == wme_code select x;

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
            list[0] = new ListItem("焊法代码", BLL.Const.WME_Code);
            list[1] = new ListItem("焊法名称", BLL.Const.WME_Name);

            return list;
        }

        /// <summary>
        /// 获取焊接方法
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetWeldMethodNameList()
        {
            var q = (from x in Funs.DB.BS_WeldMethod orderby x.WME_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].WME_Name ?? "", q[i].WME_ID.ToString());
            }
            return list;
        }


        /// <summary>
        /// 根据焊接方法编号获取焊接方法信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_WeldMethod GetMethodByMethodCode(string wmeCode)
        {
            return Funs.DB.BS_WeldMethod.FirstOrDefault(x => x.WME_Code == wmeCode);
        }
    }
}
