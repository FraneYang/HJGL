using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 探伤类型
    /// </summary>
   public static class TestingService
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
       private static IQueryable<Model.BS_NDTType> qq = from x in db.BS_NDTType orderby x.NDT_Code select x;

       /// <summary>
       /// 获取探伤类型列表
       /// </summary>
       /// <param name="searchItem"></param>
       /// <param name="searchValue"></param>
       /// <param name="projectId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.BS_NDTType> q = qq;

           if (searchItem!="0")
           {
               if (!string.IsNullOrEmpty(searchValue))
               {
                   if (searchItem==BLL.Const.TestingCode)
                   {
                       q = q.Where(e => e.NDT_Code.Contains(searchValue));
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
                      x.NDT_ID,
                      x.NDT_Code,
                      x.NDT_Name,
                      x.SysType,
                      x.NDT_Description,
                      x.NDT_SecuritySpace,
                      x.NDT_Harm,                      
                      x.NDT_Remark
                  };
       }

       /// <summary>
       ///获取列表数
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
       /// 根据探伤类型Id获取探伤类型
       /// </summary>
       /// <param name="dnt_id"></param>
       /// <returns></returns>
       public static Model.BS_NDTType GetTestingByTestingId(string dnt_id)
       {
           return Funs.DB.BS_NDTType.FirstOrDefault(e => e.NDT_ID == dnt_id);
       }

       /// <summary>
       /// 添加探伤类型
       /// </summary>
       /// <param name="testing"></param>
       public static void AddTesting(Model.BS_NDTType testing)
       {
           Model.HJGLDB db = Funs.DB;

           Model.BS_NDTType newTesting = new Model.BS_NDTType();
           string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_NDTType));
           newTesting.NDT_ID = newKeyID;

           newTesting.NDT_Code = testing.NDT_Code;
           newTesting.NDT_Name = testing.NDT_Name;
           newTesting.SysType = testing.SysType;
           newTesting.NDT_Description = testing.NDT_Description;
           newTesting.NDT_SecuritySpace = testing.NDT_SecuritySpace;
           newTesting.NDT_Harm = testing.NDT_Harm;         
           newTesting.NDT_Remark = testing.NDT_Remark;

           db.BS_NDTType.InsertOnSubmit(newTesting);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改探伤类型
       /// </summary>
       /// <param name="testing"></param>
       public static void UpdateTesting(Model.BS_NDTType testing)
       {
           Model.HJGLDB db = Funs.DB;

           Model.BS_NDTType newTesting = db.BS_NDTType.First(e => e.NDT_ID == testing.NDT_ID);

           newTesting.NDT_Code = testing.NDT_Code;
           newTesting.NDT_Name = testing.NDT_Name;
           newTesting.SysType = testing.SysType;
           newTesting.NDT_Description = testing.NDT_Description;
           newTesting.NDT_SecuritySpace = testing.NDT_SecuritySpace;
           newTesting.NDT_Harm = testing.NDT_Harm;
           newTesting.NDT_Remark = testing.NDT_Remark;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除探伤类型
       /// </summary>
       /// <param name="testingId"></param>
       public static void DeleteTesting(string testingId)
       {
           Model.HJGLDB db = Funs.DB;

           Model.BS_NDTType testing = db.BS_NDTType.First(e => e.NDT_ID == testingId);

           db.BS_NDTType.DeleteOnSubmit(testing);
           db.SubmitChanges();          
       }

       /// <summary>
       /// 判断是否存在相同的探伤类型代号
       /// </summary>
       /// <param name="testingCode"></param>
       /// <returns></returns>
       public static bool IsExitTestingCode(string testingCode)
       {
           Model.HJGLDB db = Funs.DB;

           var q = from x in db.BS_NDTType where x.NDT_Code == testingCode select x;

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
       /// 查询下拉列表值
       /// </summary>
       /// <returns></returns>
       public static ListItem[] SearchItem()
       {
           ListItem[] list = new ListItem[2];
           list[0] = new ListItem("探伤类型代号", BLL.Const.TestingCode);
           list[1] = new ListItem("探伤类型名称", BLL.Const.TestingType);

           return list;
       }

       /// <summary>
       /// 获取探伤类型下拉框
       /// </summary>
       /// <returns></returns>
       public static ListItem[] GetNDTTypeNameList()
       {
           var q = (from x in Funs.DB.BS_NDTType orderby x.NDT_Code select x).ToList();
           ListItem[] list = new ListItem[q.Count()];
           for (int i = 0; i < q.Count(); i++)
           {
               list[i] = new ListItem(q[i].NDT_Name ?? "", q[i].NDT_ID.ToString());
           }
           return list;
       }
    }
}
