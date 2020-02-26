using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 试压类型
    /// </summary>
    public static class PressureService
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
        private static IQueryable<Model.BS_PackageTestType> qq = from x in db.BS_PackageTestType orderby x.TPT_Code select x;
        /// <summary>
        /// 获取试压类型表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_PackageTestType> q = qq;
            if (searchItem!="0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem==BLL.Const.TPT_Code)
                    {
                        q = q.Where(e => e.TPT_Code.Contains(searchValue));
                    }
                    if (searchItem==BLL.Const.TPT_TypeName)
                    {
                        q = q.Where(e => e.TPT_TypeName.Contains(searchValue));
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
                       x.TPT_ID,
                       x.TPT_Code,
                       x.TPT_TypeName,
                       x.TPT_Remark
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
        /// 根据试压类型ID获取试压类型信息
        /// </summary>
        /// <param name="tpt_ip"></param>
        /// <returns></returns>
        public static Model.BS_PackageTestType GetPackageTestTypeByTPTID(string tpt_ip)
        {
            return Funs.DB.BS_PackageTestType.FirstOrDefault(e => e.TPT_ID == tpt_ip);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="packageTestType"></param>
        public static void AddPackageTestType(Model.BS_PackageTestType packageTestType)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_PackageTestType newPackageTestType = new Model.BS_PackageTestType();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_PackageTestType));

            newPackageTestType.TPT_ID = newKeyID;
            newPackageTestType.TPT_Code = packageTestType.TPT_Code;
            newPackageTestType.TPT_TypeName = packageTestType.TPT_TypeName;
            newPackageTestType.TPT_Remark = packageTestType.TPT_Remark;

            db.BS_PackageTestType.InsertOnSubmit(newPackageTestType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="packageTestType"></param>
        public static void UpdatePackageTestType(Model.BS_PackageTestType packageTestType)
        { 
            Model.HJGLDB db =Funs.DB;
            Model.BS_PackageTestType newPackageTestType = db.BS_PackageTestType.FirstOrDefault(e => e.TPT_ID == packageTestType.TPT_ID);

            newPackageTestType.TPT_Code = packageTestType.TPT_Code;
            newPackageTestType.TPT_TypeName = packageTestType.TPT_TypeName;
            newPackageTestType.TPT_Remark = packageTestType.TPT_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tpt_id"></param>
        public static void DeletePackageTestType(string tpt_id)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_PackageTestType packageTestType = db.BS_PackageTestType.FirstOrDefault(e => e.TPT_ID == tpt_id);

            db.BS_PackageTestType.DeleteOnSubmit(packageTestType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的试压类型代号
        /// </summary>
        /// <param name="tpt_code"></param>
        /// <returns></returns>
        public static bool IsExitTPTCode(string tpt_code)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_PackageTestType where x.TPT_Code == tpt_code select x;

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
            list[0] = new ListItem("试压代号", BLL.Const.TPT_Code);
            list[1] = new ListItem("试压名称", BLL.Const.TPT_TypeName);

            return list;
        }

        /// <summary>
        /// 获取试验类型
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetBS_PackageTestTypeList()
        {
            var q = (from x in Funs.DB.BS_PackageTestType orderby x.TPT_Code select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].TPT_TypeName ?? "", q[i].TPT_ID.ToString());
            }
            return list;
        }
    }
}