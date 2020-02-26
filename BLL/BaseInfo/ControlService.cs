using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 直径寸径对照
    /// </summary>
    public static class ControlService
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
        private static IQueryable<Model.BS_SchTab> qq = from x in db.BS_SchTab orderby x.BST_Code select x;

        /// <summary>
        /// 获取直径寸径对照
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_SchTab> q = qq;
            if (searchItem!="0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.BST_DN)
                    {
                        q = q.Where(e => e.BST_DN.Contains(searchValue));
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
                       x.BST_ID,
                       x.BST_Code,
                       x.BST_Dia,
                       x.BST_DN,
                       x.BST_Inch,
                       x.BST_Sch5s,
                       x.BST_Sch10s,
                       x.BST_Sch10,
                       x.BST_Sch20,
                       x.BST_Sch30,
                       x.BST_Sch40s,
                       x.BST_STD,
                       x.BST_Sch40,
                       x.BST_Sch60,
                       x.BST_Sch80s,
                       x.BST_XS,
                       x.BST_Sch80,
                       x.BST_Sch100,
                       x.BST_Sch120,
                       x.BST_Sch140,
                       x.BST_Sch160,
                       x.BST_XXS,
                       x.BST_Remark
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
        /// 根据ID获取直径寸径对照
        /// </summary>
        /// <param name="bst_id"></param>
        /// <returns></returns>
        public static Model.BS_SchTab GetSchTabByBSTID(string bst_id)
        {
            return Funs.DB.BS_SchTab.FirstOrDefault(e => e.BST_ID == bst_id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="schTab"></param>
        public static void AddSchTab(Model.BS_SchTab schTab)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_SchTab));
            Model.BS_SchTab newSchTab = new Model.BS_SchTab();

            newSchTab.BST_ID = newKeyID;
            newSchTab.BST_Code = schTab.BST_Code;
            newSchTab.BST_Dia = schTab.BST_Dia;
            newSchTab.BST_DN =schTab.BST_DN;
            newSchTab.BST_Inch = schTab.BST_Inch;
            newSchTab.BST_Sch5s = schTab.BST_Sch5s;
            newSchTab.BST_Sch10s = schTab.BST_Sch10s;
            newSchTab.BST_Sch10 = schTab.BST_Sch10;
            newSchTab.BST_Sch120 = schTab.BST_Sch20;
            newSchTab.BST_Sch30 = schTab.BST_Sch30;
            newSchTab.BST_Sch40s = schTab.BST_Sch40s;
            newSchTab.BST_STD = schTab.BST_STD;
            newSchTab.BST_Sch40 = schTab.BST_Sch40;
            newSchTab.BST_Sch60 = schTab.BST_Sch60;
            newSchTab.BST_Sch80s = schTab.BST_Sch80s;
            newSchTab.BST_XS = schTab.BST_XS;
            newSchTab.BST_Sch80 = schTab.BST_Sch80;
            newSchTab.BST_Sch100 = schTab.BST_Sch100;
            newSchTab.BST_Sch120 = schTab.BST_Sch120;
            newSchTab.BST_Sch140 = schTab.BST_Sch140;
            newSchTab.BST_Sch160 = schTab.BST_Sch160;
            newSchTab.BST_XXS = schTab.BST_XXS;
            newSchTab.BST_Remark = schTab.BST_Remark;

            db.BS_SchTab.InsertOnSubmit(newSchTab);
            db.SubmitChanges();   
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="schTab"></param>
        public static void UpdateSchTab(Model.BS_SchTab schTab)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_SchTab newSchTab = db.BS_SchTab.FirstOrDefault(e => e.BST_ID == schTab.BST_ID);

            newSchTab.BST_Code = schTab.BST_Code;
            newSchTab.BST_Dia = schTab.BST_Dia;
            newSchTab.BST_DN = schTab.BST_DN;
            newSchTab.BST_Inch = schTab.BST_Inch;
            newSchTab.BST_Sch5s = schTab.BST_Sch5s;
            newSchTab.BST_Sch10s = schTab.BST_Sch10s;
            newSchTab.BST_Sch10 = schTab.BST_Sch10;
            newSchTab.BST_Sch120 = schTab.BST_Sch20;
            newSchTab.BST_Sch30 = schTab.BST_Sch30;
            newSchTab.BST_Sch40s = schTab.BST_Sch40s;
            newSchTab.BST_STD = schTab.BST_STD;
            newSchTab.BST_Sch40 = schTab.BST_Sch40;
            newSchTab.BST_Sch60 = schTab.BST_Sch60;
            newSchTab.BST_Sch80s = schTab.BST_Sch80s;
            newSchTab.BST_XS = schTab.BST_XS;
            newSchTab.BST_Sch80 = schTab.BST_Sch80;
            newSchTab.BST_Sch100 = schTab.BST_Sch100;
            newSchTab.BST_Sch120 = schTab.BST_Sch120;
            newSchTab.BST_Sch140 = schTab.BST_Sch140;
            newSchTab.BST_Sch160 = schTab.BST_Sch160;
            newSchTab.BST_XXS = schTab.BST_XXS;
            newSchTab.BST_Remark = schTab.BST_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bst_id"></param>
        public static void DeleteSchTab(string bst_id)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_SchTab schTab = db.BS_SchTab.FirstOrDefault(e => e.BST_ID == bst_id);

            db.BS_SchTab.DeleteOnSubmit(schTab);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的直径代号
        /// </summary>
        /// <param name="bst_code"></param>
        /// <returns></returns>
        public static bool IsExitBSTCode(string bst_DN)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_SchTab where x.BST_DN == bst_DN select x;

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
            ListItem[] list = new ListItem[1];
            list[0] = new ListItem("公称直径", BLL.Const.BST_DN);

            return list;
        }
    }
}
