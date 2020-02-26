using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 焊缝类型
    /// </summary>
   public static class WeldService
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
       private static IQueryable<Model.BS_JointType> qq = from x in db.BS_JointType orderby x.JOTY_Code select x;

       /// <summary>
       /// 获取焊缝类型类表
       /// </summary>
       /// <param name="searchItem"></param>
       /// <param name="searchValue"></param>
       /// <param name="projectId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.BS_JointType> q = qq;
           if (searchItem!="0")
           {
               if (!string.IsNullOrEmpty(searchValue))
               {
                   if (searchItem==BLL.Const.JOTY_Code)
                   {
                       q = q.Where(e => e.JOTY_Code.Contains(searchValue));
                   }
                   if (searchItem==BLL.Const.JOTY_Name)
                   {
                       q = q.Where(e => e.JOTY_Name.Contains(searchValue));                       
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
                      x.JOTY_ID,
                      x.JOTY_Code,
                      x.JOTY_Name,
                      x.JOTY_Remark
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
       /// 根据焊缝类型Id获取焊缝类型信息
       /// </summary>
       /// <param name="jot_id"></param>
       /// <returns></returns>
       public static Model.BS_JointType GetJointTypeByJotID(string jot_id)
       {
           return Funs.DB.BS_JointType.FirstOrDefault(e => e.JOTY_ID == jot_id);
       }

       /// <summary>
       /// 添加焊缝类型
       /// </summary>
       /// <param name="jointType"></param>
       public static void AddJointType(Model.BS_JointType jointType)
       {
           Model.HJGLDB db = Funs.DB;
           string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_JointType));
           Model.BS_JointType newJointType = new Model.BS_JointType();
           newJointType.JOTY_ID = newKeyID;
           newJointType.JOTY_Code = jointType.JOTY_Code;
           newJointType.JOTY_Name = jointType.JOTY_Name;
           newJointType.JOTY_Remark = jointType.JOTY_Remark;

           db.BS_JointType.InsertOnSubmit(newJointType);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改焊缝类型
       /// </summary>
       /// <param name="jointType"></param>
       public static void UpdateJointType(Model.BS_JointType jointType)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BS_JointType newJointType = db.BS_JointType.FirstOrDefault(e => e.JOTY_ID == jointType.JOTY_ID);
           newJointType.JOTY_Code = jointType.JOTY_Code;
           newJointType.JOTY_Name = jointType.JOTY_Name;
           newJointType.JOTY_Remark = jointType.JOTY_Remark;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除焊缝类型
       /// </summary>
       /// <param name="jot_id"></param>
       public static void DeleteJointType(string jot_id)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BS_JointType jointType = db.BS_JointType.FirstOrDefault(e => e.JOTY_ID == jot_id);
           db.BS_JointType.DeleteOnSubmit(jointType);
           db.SubmitChanges();
       }
       /// <summary>
       /// 判断是否存在相同的焊缝类型编号
       /// </summary>
       /// <param name="joty_code"></param>
       /// <returns></returns>
       public static bool IsExitJotyCode(string joty_code)
       {
           Model.HJGLDB db = Funs.DB;

           var q = from x in db.BS_JointType where x.JOTY_Code == joty_code select x;

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
       /// 获取焊缝类型名称
       /// </summary>
       /// <returns></returns>
       public static ListItem[] GetJointTypeNameList()
       {
           var q = (from x in Funs.DB.BS_JointType orderby x.JOTY_Code select x).ToList();
           ListItem[] list = new ListItem[q.Count()];
           for (int i = 0; i < q.Count(); i++)
           {
               list[i] = new ListItem(q[i].JOTY_Name ?? "", q[i].JOTY_ID.ToString());
           }
           return list;
       }

       /// <summary>
       /// 查询下拉列表值
       /// </summary>
       /// <returns></returns>
       public static ListItem[] SearchItem()
       {
           ListItem[] list = new ListItem[2];
           list[0] = new ListItem("焊缝类型代号", BLL.Const.JOTY_Code);
           list[1] = new ListItem("焊缝类型名称", BLL.Const.JOTY_Name);

           return list;
       }

       /// <summary>
       /// 根据焊缝类型获取焊缝类型信息
       /// </summary>
       /// <param name="unitCode"></param>
       /// <returns></returns>
       public static Model.BS_JointType GetJointTypeByJointTypeName(string jointTypeName)
       {
           return Funs.DB.BS_JointType.FirstOrDefault(x => x.JOTY_Name == jointTypeName);
       }

   }
}
