using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 安装组件
    /// </summary>
   public static class ComponentsService
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
       private static IQueryable<Model.BS_Component> qq = from x in db.BS_Component orderby x.COM_Code select x;

       /// <summary>
       /// 获取安装组件
       /// </summary>
       /// <param name="searchItem"></param>
       /// <param name="searchValue"></param>
       /// <param name="projectId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.BS_Component> q = qq;
           if (searchItem!="0")
           {
               if (!string.IsNullOrEmpty(searchValue))
               {
                   if (searchItem==BLL.Const.COM_Code)
                   {
                       q = q.Where(e => e.COM_Code.Contains(searchValue));
                   }
                   if (searchItem==BLL.Const.COM_Name)
                   {
                       q = q.Where(e => e.COM_Name.Contains(searchValue));
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
                      x.COM_ID,
                      x.COM_Code,
                      x.COM_Name,
                      x.COM_Remark
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
       /// 根据安装组件ID获取安装组件信息
       /// </summary>
       /// <param name="com_id"></param>
       /// <returns></returns>
       public static Model.BS_Component GetComponentByComID(string com_id)
       {
           return Funs.DB.BS_Component.FirstOrDefault(e => e.COM_ID == com_id);
       }

       /// <summary>
       /// 添加安装组件
       /// </summary>
       /// <param name="component"></param>
       public static void AddComponent(Model.BS_Component component)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BS_Component newComponent = new Model.BS_Component();
           string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_Component));

           newComponent.COM_ID = newKeyID;
           newComponent.COM_Code = component.COM_Code;
           newComponent.COM_Name = component.COM_Name;
           newComponent.COM_Remark = component.COM_Remark;

           db.BS_Component.InsertOnSubmit(newComponent);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改安装组件
       /// </summary>
       /// <param name="component"></param>
       public static void UpdateComponent(Model.BS_Component component)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BS_Component newComponent = db.BS_Component.FirstOrDefault(e => e.COM_ID == component.COM_ID);

           newComponent.COM_Code = component.COM_Code;
           newComponent.COM_Name = component.COM_Name;
           newComponent.COM_Remark = component.COM_Remark;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除安装组件
       /// </summary>
       /// <param name="com_id"></param>
       public static void DeleteComponent(string com_id)
       {
           Model.HJGLDB db = Funs.DB;
           Model.BS_Component component = db.BS_Component.FirstOrDefault(e => e.COM_ID == com_id);

           db.BS_Component.DeleteOnSubmit(component);
           db.SubmitChanges();
       }

       /// <summary>
       /// 判断是否存在相同的组件代号
       /// </summary>
       /// <param name="com_code"></param>
       /// <returns></returns>
       public static bool IsExitComCode(string com_code)
       {
           Model.HJGLDB db = Funs.DB;

           var q = from x in db.BS_Component where x.COM_Code == com_code select x;

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
           list[0] = new ListItem("组件代号", BLL.Const.COM_Code);
           list[1] = new ListItem("组件名称", BLL.Const.COM_Name);

           return list;
       }

       /// <summary>
       /// 获取焊缝类型名称
       /// </summary>
       /// <returns></returns>
       public static ListItem[] GetComponentNameList()
       {
           var q = (from x in Funs.DB.BS_Component orderby x.COM_Code select x).ToList();
           ListItem[] list = new ListItem[q.Count()];
           for (int i = 0; i < q.Count(); i++)
           {
               list[i] = new ListItem(q[i].COM_Name ?? "", q[i].COM_ID.ToString());
           }
           return list;
       }

       /// <summary>
       /// 组件代码获取组件代号信息
       /// </summary>
       /// <param name="unitCode"></param>
       /// <returns></returns>
       public static Model.BS_Component GetComponentByComponentCode(string componentCode)
       {
           return Funs.DB.BS_Component.FirstOrDefault(x => x.COM_Code == componentCode);
       }
    }
}
