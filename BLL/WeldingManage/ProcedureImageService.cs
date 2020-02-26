using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
   public static class ProcedureImageService
    {
       public static Model.HJGLDB db = Funs.DB;

         /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

       /// <summary>
       /// 定义变量
       /// </summary>
        public static IQueryable<Model.PW_ProcedureImageManage> qq = from x in db.PW_ProcedureImageManage select x;

       /// <summary>
       /// 获取分页信息列表
       /// </summary>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
        public static IEnumerable GetListData(int startRowIndex, int maximumRows)
        {
            IQueryable<Model.PW_ProcedureImageManage> q = qq;
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.ImageId,
                       x.ImageContent,
                       x.AttachUrl,
                       WME_Name = (from y in db.BS_WeldMethod where y.WME_ID==x.WME_ID select y.WME_Name).First(),
                       x.Thickness,
                       JOTY_Name =(from y in db.BS_JointType where y.JOTY_ID==x.JOTY_ID select y.JOTY_Name).First(),
                       JST_Name=(from y in db.BS_SlopeType where y.JST_ID==x.JST_ID select y.JST_Name).First()
                   };
        }

       /// <summary>
       /// 获取列表数
       /// </summary>
       /// <returns></returns>
        public static int GetListCount()
        {
            return count;
        }

        /// <summary>
        /// 根据主键获取图片信息
        /// </summary>
        /// <param name="pictureId">主键</param>
        /// <returns>图片信息</returns>
        public static Model.PW_ProcedureImageManage GetImageById(string ImageId)
        {
            return (from x in Funs.DB.PW_ProcedureImageManage where x.ImageId == ImageId select x).FirstOrDefault();
        }
       /// <summary>
       /// 添加工艺图片
       /// </summary>
       /// <param name="procedureImage"></param>
       public static void AddProcedureImage(Model.PW_ProcedureImageManage procedureImage)
       {
           Model.HJGLDB db = Funs.DB;
           Model.PW_ProcedureImageManage newProcedureImage = new Model.PW_ProcedureImageManage();
           newProcedureImage.ImageId = procedureImage.ImageId;
           newProcedureImage.ImageContent = procedureImage.ImageContent;
           newProcedureImage.AttachUrl = procedureImage.AttachUrl;
           newProcedureImage.WME_ID = procedureImage.WME_ID;
           newProcedureImage.Thickness = procedureImage.Thickness;
           newProcedureImage.JOTY_ID = procedureImage.JOTY_ID;
           newProcedureImage.JST_ID = procedureImage.JST_ID;

           db.PW_ProcedureImageManage.InsertOnSubmit(newProcedureImage);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改工艺管道图片
       /// </summary>
       /// <param name="procedureImage"></param>
       public static void UpdateProcedureImage(Model.PW_ProcedureImageManage procedureImage)
       {
           Model.HJGLDB db = Funs.DB;
           Model.PW_ProcedureImageManage newProcedureImage = db.PW_ProcedureImageManage.FirstOrDefault(e => e.ImageId == procedureImage.ImageId);
           newProcedureImage.ImageContent = procedureImage.ImageContent;
           newProcedureImage.AttachUrl = procedureImage.AttachUrl;
           newProcedureImage.WME_ID = procedureImage.WME_ID;
           newProcedureImage.Thickness = procedureImage.Thickness;
           newProcedureImage.JOTY_ID = procedureImage.JOTY_ID;
           newProcedureImage.JST_ID = procedureImage.JST_ID;

           db.SubmitChanges();
       }

       /// <summary>
       /// 删除工艺图片
       /// </summary>
       /// <param name="imageId"></param>
       public static void DeleteProcedureImage(string imageId)
       {
           Model.HJGLDB db = Funs.DB;
           Model.PW_ProcedureImageManage procedureImage = db.PW_ProcedureImageManage.FirstOrDefault(e => e.ImageId == imageId);
           db.PW_ProcedureImageManage.DeleteOnSubmit(procedureImage);
           db.SubmitChanges();
       }

       /// <summary>
       /// 获取工艺图片内容项
       /// </summary>
       /// <returns></returns>
       public static ListItem[] GetImageContentList()
       {
           var q = (from x in Funs.DB.PW_ProcedureImageManage select x).ToList();
           ListItem[] item = new ListItem[q.Count()];
           for (int i = 0; i < q.Count(); i++)
           {
               item[i] = new ListItem(q[i].ImageContent ?? "", q[i].ImageId.ToString());
           }
           return item;
       }
    }
}
