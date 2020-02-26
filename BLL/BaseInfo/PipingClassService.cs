using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 管道等级
    /// </summary>
    public static class PipingClassService
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
        private static IQueryable<Model.BS_IsoClass> qq = from x in db.BS_IsoClass orderby x.ISC_IsoCode select x;

        /// <summary>
        /// 获取管道等级列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue,  int startRowIndex, int maximumRows)
        {           
            IQueryable<Model.BS_IsoClass> q = qq;

            if (searchItem!="0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem==BLL.Const.PipingClassCode)
                    {
                        q = q.Where(e => e.ISC_IsoCode.Contains(searchValue));
                    }
                    if (searchItem==BLL.Const.PipingClassName)
                    {
                        q = q.Where(e => e.ISC_IsoName.Contains(searchValue));
                    }
                }                
            }
           
            count = q.Count();
            if (count ==0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.ISC_ID,
                       x.ISC_IsoCode,
                       x.ISC_IsoClass,
                       x.ISC_IsoName,
                       x.ISC_Remark
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
        /// 根据管道等级ID获取管道等级信息
        /// </summary>
        /// <param name="pipingClassId"></param>
        /// <returns></returns>
        public static Model.BS_IsoClass GetPipingClassByPipingClassId(string pipingClassId)
        {
            return Funs.DB.BS_IsoClass.FirstOrDefault(e => e.ISC_ID == pipingClassId);
        }

       /// <summary>
       /// 添加管道等级信息
       /// </summary>
       /// <param name="pipingClass"></param>
        public static void AddPipingClass(Model.BS_IsoClass pipingClass)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_IsoClass));
            Model.BS_IsoClass newPipingClass = new Model.BS_IsoClass();
            newPipingClass.ISC_ID = newKeyID;
            newPipingClass.ISC_IsoCode = pipingClass.ISC_IsoCode;
            newPipingClass.ISC_IsoClass = pipingClass.ISC_IsoClass;
            newPipingClass.ISC_IsoName = pipingClass.ISC_IsoName;
            newPipingClass.ISC_Remark = pipingClass.ISC_Remark;

            db.BS_IsoClass.InsertOnSubmit(newPipingClass);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改管道等级信息
        /// </summary>
        /// <param name="pipingClass"></param>
        public static void UpdatePipingClass(Model.BS_IsoClass pipingClass)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_IsoClass newPipingClass = db.BS_IsoClass.First(e => e.ISC_ID == pipingClass.ISC_ID);
            newPipingClass.ISC_IsoCode = pipingClass.ISC_IsoCode;
            newPipingClass.ISC_IsoClass = pipingClass.ISC_IsoClass;
            newPipingClass.ISC_IsoName = pipingClass.ISC_IsoName;
            newPipingClass.ISC_Remark = pipingClass.ISC_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除管道等级信息
        /// </summary>
        /// <param name="pipingClassId"></param>
        public static void DeletePipingClass(string pipingClassId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_IsoClass pipingClass = db.BS_IsoClass.First(e => e.ISC_ID == pipingClassId);

            db.BS_IsoClass.DeleteOnSubmit(pipingClass);
            db.SubmitChanges();

        }

        /// <summary>
        /// 判断是否存在该管道等级代号
        /// </summary>
        /// <param name="pipingClassCode"></param>
        /// <returns></returns>
        public static bool IsExistPipingClassCode(string pipingClassCode)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.BS_IsoClass where x.ISC_IsoCode == pipingClassCode select x;

            if (q.Count()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询的下来列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchItem()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("等级代号", BLL.Const.PipingClassCode);
            list[1] = new ListItem("等级名称", BLL.Const.PipingClassName);
            return list;
        }

        /// <summary>
        /// 获取管线等级下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetIsoClassNameList()
        {
            var q = (from x in Funs.DB.BS_IsoClass orderby x.ISC_IsoCode select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].ISC_IsoName ?? "", q[i].ISC_ID.ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据单位代码获取单位信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_IsoClass GetIsoClassByIsoClassCode(string isoClassCode)
        {
            return Funs.DB.BS_IsoClass.FirstOrDefault(x => x.ISC_IsoCode == isoClassCode);
        }
    }
}
