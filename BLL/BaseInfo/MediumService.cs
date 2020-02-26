using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 介质
    /// </summary>
    public static class MediumService
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
        private static IQueryable<Model.BS_Service> qq = from x in db.BS_Service orderby x.SER_Code select x;

          /// <summary>
       /// 获取介质表
       /// </summary>
       /// <param name="searchItem"></param>
       /// <param name="searchValue"></param>
       /// <param name="projectId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.BS_Service> q = qq;
            if (searchItem!="0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem==BLL.Const.SER_Code)
                    {
                        q = q.Where(e => e.SER_Code.Contains(searchValue));
                    }
                    if (searchItem==BLL.Const.SER_Name)
                    {
                        q = q.Where(e => e.SER_Name.Contains(searchValue));
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
                       x.SER_ID,
                       x.SER_Code,
                       x.SER_Name,
                       x.SER_Remark
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
       /// 根据介质Id获取介质信息
       /// </summary>
       /// <param name="ser_id"></param>
       /// <returns></returns>
        public static Model.BS_Service GetServiceBySERID(string ser_id)
        {
            return Funs.DB.BS_Service.FirstOrDefault(e => e.SER_ID == ser_id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="service"></param>
        public static void AddService(Model.BS_Service service)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.BS_Service));
            Model.BS_Service newService = new Model.BS_Service();

            newService.SER_ID = newKeyID;
            newService.SER_Code = service.SER_Code;
            newService.SER_Name = service.SER_Name;
            newService.SER_Remark = service.SER_Remark;

            db.BS_Service.InsertOnSubmit(newService);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="service"></param>
        public static void UpdateService(Model.BS_Service service)
        {
            Model.HJGLDB db = Funs.DB;
            Model.BS_Service newService = db.BS_Service.FirstOrDefault(e => e.SER_ID == service.SER_ID);
            
            newService.SER_Code = service.SER_Code;
            newService.SER_Name = service.SER_Name;
            newService.SER_Remark = service.SER_Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ser_id"></param>
        public static void DeleteService(string ser_id)
        {
            Model.HJGLDB db = Funs.DB;

            Model.BS_Service service = db.BS_Service.FirstOrDefault(e => e.SER_ID == ser_id);

            db.BS_Service.DeleteOnSubmit(service);
            db.SubmitChanges();
        }

        /// <summary>
        /// 判断是否存在相同的焊缝类型编号
        /// </summary>
        /// <param name="ser_code"></param>
        /// <returns></returns>
        public static bool IsExitSERCode(string ser_code)
        {
            Model.HJGLDB db = Funs.DB;

            var q = from x in db.BS_Service where x.SER_Code == ser_code select x;

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
            list[0] = new ListItem("介质代号", BLL.Const.SER_Code);
            list[1] = new ListItem("介质描述", BLL.Const.SER_Name);

            return list;
        }

        /// <summary>
        /// 根据介质代码获取介质信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.BS_Service GetServiceByServiceCode(string serviceCode)
        {
            return Funs.DB.BS_Service.FirstOrDefault(x => x.SER_Code == serviceCode);
        }

        /// <summary>
        /// 获取介质信息项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetBSServiceList()
        {
            var q = (from x in Funs.DB.BS_Service orderby x.SER_Code select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].SER_Name ?? "", q[i].SER_ID.ToString());
            }
            return item;
        }
    }
}
