namespace BLL
{
    using System.Collections;
    using System.Linq;

    public static class DataInTableService
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
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string userId,bool isRowNo, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Sys_DataInTemp> q = from x in db.Sys_DataInTemp
                                                 where x.ProjectId == projectId && x.UserId == userId                                               
                                                 select x;
            if (isRowNo)
            {
                q = q.OrderBy(x => x.RowNo);
            }
            else
            {
                q = q.OrderBy(x => x.Value1).ThenBy(x => x.Value2).ThenBy(x => x.Value3).ThenBy(x => x.Value4);
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.TempId,
                       x.ProjectId,
                       x.UserId,
                       x.Time,
                       x.RowNo,
                       x.Value1,
                       x.Value2,
                       x.Value3,
                       x.Value4,
                       x.Value5,
                       x.Value6,
                       x.Value7,
                       x.Value8,
                       x.Value9,
                       x.Value10,
                       x.Value11,
                       x.Value12,
                       x.Value13,
                       x.Value14,
                       x.Value15,
                       x.Value16,
                       x.Value17,
                       x.Value18,
                       x.Value19,
                       x.Value20,
                       x.Value21,
                       x.Value22,
                       x.Value23,
                       x.Value24,
                       x.Value25,
                       x.Value26,
                       x.Value27,
                       x.Value28,
                       x.Value29,
                       x.Value30,
                       x.Value31,
                       x.Value32,
                       x.Value33,
                       x.Value34,
                       x.Value35,
                       x.Value36,
                       x.Value37,
                       x.Value38,
                       x.Value39,
                       x.Value40,
                       x.Value41, 
                       x.Value42,
                       x.Value43,
                       x.Value44,
                       x.Value45,
                       x.Value46,
                       x.Value47,
                       x.Value48,
                       x.Value49,
                       x.Value50,
                       x.Value51,
                       x.Value52,
                       x.Value53,
                       x.Value54,
                       x.Value55,
                       x.Value56,
                       x.Value57,
                       x.Value58,
                       x.Value59,
                       x.Value60,
                       x.Value61,
                       x.Value62,
                       x.Value63,
                       x.Value64,
                       x.Value65,
                       x.Value66,
                       x.Value67,
                       x.Value68,
                       x.Value69,
                       x.Value70,
                       x.ToopValue,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string projectId, string userId, bool isRowNo)
        {
            return count;
        }

        /// <summary>
        /// 根据主键获取导入临时表信息
        /// </summary>
        /// <param name="tempId">Id</param>
        /// <returns></returns>
        public static Model.Sys_DataInTemp GetDataInTempByTempId(string tempId)
        {
            return Funs.DB.Sys_DataInTemp.FirstOrDefault(x => x.TempId == tempId);
        }

        /// <summary>
        /// 增加导入临时表记录
        /// </summary>
        /// <param name="dataInTemp">委托实体</param>
        public static void AddDataInTemp(Model.Sys_DataInTemp dataInTemp)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_DataInTemp newDataInTemp = new Model.Sys_DataInTemp();
            newDataInTemp.TempId = dataInTemp.TempId;
            newDataInTemp.ProjectId = dataInTemp.ProjectId;
            newDataInTemp.UserId = dataInTemp.UserId;
            newDataInTemp.Time = dataInTemp.Time;
            newDataInTemp.RowNo = dataInTemp.RowNo;
            newDataInTemp.Value1 = dataInTemp.Value1;
            newDataInTemp.Value2 = dataInTemp.Value2;
            newDataInTemp.Value3 = dataInTemp.Value3;
            newDataInTemp.Value4 = dataInTemp.Value4;
            newDataInTemp.Value5 = dataInTemp.Value5;
            newDataInTemp.Value6 = dataInTemp.Value6;
            newDataInTemp.Value7 = dataInTemp.Value7;
            newDataInTemp.Value8 = dataInTemp.Value8;
            newDataInTemp.Value9 = dataInTemp.Value9;
            newDataInTemp.Value10 = dataInTemp.Value10;
            newDataInTemp.Value11 = dataInTemp.Value11;
            newDataInTemp.Value12 = dataInTemp.Value12;
            newDataInTemp.Value13 = dataInTemp.Value13;
            newDataInTemp.Value14 = dataInTemp.Value14;
            newDataInTemp.Value15 = dataInTemp.Value15;
            newDataInTemp.Value16 = dataInTemp.Value16;
            newDataInTemp.Value17 = dataInTemp.Value17;
            newDataInTemp.Value18 = dataInTemp.Value18;
            newDataInTemp.Value19 = dataInTemp.Value19;
            newDataInTemp.Value20 = dataInTemp.Value20;
            newDataInTemp.Value21 = dataInTemp.Value21;
            newDataInTemp.Value22 = dataInTemp.Value22;
            newDataInTemp.Value23 = dataInTemp.Value23;
            newDataInTemp.Value24 = dataInTemp.Value24;
            newDataInTemp.Value25 = dataInTemp.Value25;
            newDataInTemp.Value26 = dataInTemp.Value26;
            newDataInTemp.Value27 = dataInTemp.Value27;
            newDataInTemp.Value28 = dataInTemp.Value28;
            newDataInTemp.Value29 = dataInTemp.Value29;
            newDataInTemp.Value30 = dataInTemp.Value30;
            newDataInTemp.Value31 = dataInTemp.Value31;
            newDataInTemp.Value32 = dataInTemp.Value32;
            newDataInTemp.Value33 = dataInTemp.Value33;
            newDataInTemp.Value34 = dataInTemp.Value34;
            newDataInTemp.Value35 = dataInTemp.Value35;
            newDataInTemp.Value36 = dataInTemp.Value36;
            newDataInTemp.Value37 = dataInTemp.Value37;
            newDataInTemp.Value38 = dataInTemp.Value38;
            newDataInTemp.Value39 = dataInTemp.Value39;
            newDataInTemp.Value40 = dataInTemp.Value40;
            newDataInTemp.Value41 = dataInTemp.Value41;
            newDataInTemp.Value42 = dataInTemp.Value42;
            newDataInTemp.Value43 = dataInTemp.Value43;
            newDataInTemp.Value44 = dataInTemp.Value44;
            newDataInTemp.Value45 = dataInTemp.Value45;
            newDataInTemp.Value46 = dataInTemp.Value46;
            newDataInTemp.Value47 = dataInTemp.Value47;
            newDataInTemp.Value48 = dataInTemp.Value48;
            newDataInTemp.Value49 = dataInTemp.Value49;
            newDataInTemp.Value50 = dataInTemp.Value50;
            newDataInTemp.Value51 = dataInTemp.Value51;
            newDataInTemp.Value52 = dataInTemp.Value52;
            newDataInTemp.Value53 = dataInTemp.Value53;
            newDataInTemp.Value54 = dataInTemp.Value54;
            newDataInTemp.Value55 = dataInTemp.Value55;
            newDataInTemp.Value56 = dataInTemp.Value56;
            newDataInTemp.Value57 = dataInTemp.Value57;
            newDataInTemp.Value58 = dataInTemp.Value58;
            newDataInTemp.Value59 = dataInTemp.Value59;
            newDataInTemp.Value60 = dataInTemp.Value60;
            newDataInTemp.Value61 = dataInTemp.Value61;
            newDataInTemp.Value62 = dataInTemp.Value62;
            newDataInTemp.Value63 = dataInTemp.Value63;
            newDataInTemp.Value64 = dataInTemp.Value64;
            newDataInTemp.Value65 = dataInTemp.Value65;
            newDataInTemp.Value66 = dataInTemp.Value66;
            newDataInTemp.Value67 = dataInTemp.Value67;
            newDataInTemp.Value68 = dataInTemp.Value68;
            newDataInTemp.Value69 = dataInTemp.Value69;
            newDataInTemp.Value70 = dataInTemp.Value70;
            newDataInTemp.ToopValue = dataInTemp.ToopValue;
            db.Sys_DataInTemp.InsertOnSubmit(newDataInTemp);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改导入临时表记录
        /// </summary>
        /// <param name="weldReport">焊接实体</param>
        public static void UpdateDataInTemp(Model.Sys_DataInTemp dataInTemp)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_DataInTemp newDataInTemp = db.Sys_DataInTemp.FirstOrDefault(e => e.TempId == dataInTemp.TempId);
            if (newDataInTemp != null)
            {
                newDataInTemp.UserId = dataInTemp.UserId;
                newDataInTemp.Time = dataInTemp.Time;
                newDataInTemp.Value1 = dataInTemp.Value1;
                newDataInTemp.Value2 = dataInTemp.Value2;
                newDataInTemp.Value3 = dataInTemp.Value3;
                newDataInTemp.Value4 = dataInTemp.Value4;
                newDataInTemp.Value5 = dataInTemp.Value5;
                newDataInTemp.Value6 = dataInTemp.Value6;
                newDataInTemp.Value7 = dataInTemp.Value7;
                newDataInTemp.Value8 = dataInTemp.Value8;
                newDataInTemp.Value9 = dataInTemp.Value9;
                newDataInTemp.Value10 = dataInTemp.Value10;
                newDataInTemp.Value11 = dataInTemp.Value11;
                newDataInTemp.Value12 = dataInTemp.Value12;
                newDataInTemp.Value13 = dataInTemp.Value13;
                newDataInTemp.Value14 = dataInTemp.Value14;
                newDataInTemp.Value15 = dataInTemp.Value15;
                newDataInTemp.Value16 = dataInTemp.Value16;
                newDataInTemp.Value17 = dataInTemp.Value17;
                newDataInTemp.Value18 = dataInTemp.Value18;
                newDataInTemp.Value19 = dataInTemp.Value19;
                newDataInTemp.Value20 = dataInTemp.Value20;
                newDataInTemp.Value21 = dataInTemp.Value21;
                newDataInTemp.Value22 = dataInTemp.Value22;
                newDataInTemp.Value23 = dataInTemp.Value23;
                newDataInTemp.Value24 = dataInTemp.Value24;
                newDataInTemp.Value25 = dataInTemp.Value25;
                newDataInTemp.Value26 = dataInTemp.Value26;
                newDataInTemp.Value27 = dataInTemp.Value27;
                newDataInTemp.Value28 = dataInTemp.Value28;
                newDataInTemp.Value29 = dataInTemp.Value29;
                newDataInTemp.Value30 = dataInTemp.Value30;
                newDataInTemp.Value31 = dataInTemp.Value31;
                newDataInTemp.Value32 = dataInTemp.Value32;
                newDataInTemp.Value33 = dataInTemp.Value33;
                newDataInTemp.Value34 = dataInTemp.Value34;
                newDataInTemp.Value35 = dataInTemp.Value35;
                newDataInTemp.Value36 = dataInTemp.Value36;
                newDataInTemp.Value37 = dataInTemp.Value37;
                newDataInTemp.Value38 = dataInTemp.Value38;
                newDataInTemp.Value39 = dataInTemp.Value39;
                newDataInTemp.Value40 = dataInTemp.Value40;
                newDataInTemp.Value41 = dataInTemp.Value41;
                newDataInTemp.Value42 = dataInTemp.Value42;
                newDataInTemp.Value43 = dataInTemp.Value43;
                newDataInTemp.Value44 = dataInTemp.Value44;
                newDataInTemp.Value45 = dataInTemp.Value45;
                newDataInTemp.Value46 = dataInTemp.Value46;
                newDataInTemp.Value47 = dataInTemp.Value47;
                newDataInTemp.Value48 = dataInTemp.Value48;
                newDataInTemp.Value49 = dataInTemp.Value49;
                newDataInTemp.Value50 = dataInTemp.Value50;
                newDataInTemp.Value51 = dataInTemp.Value51;
                newDataInTemp.Value52 = dataInTemp.Value52;
                newDataInTemp.Value53 = dataInTemp.Value53;
                newDataInTemp.Value54 = dataInTemp.Value54;
                newDataInTemp.Value55 = dataInTemp.Value55;
                newDataInTemp.Value56 = dataInTemp.Value56;
                newDataInTemp.Value57 = dataInTemp.Value57;
                newDataInTemp.Value58 = dataInTemp.Value58;
                newDataInTemp.Value59 = dataInTemp.Value59;
                newDataInTemp.Value60 = dataInTemp.Value60;
                newDataInTemp.Value61 = dataInTemp.Value61;
                newDataInTemp.Value62 = dataInTemp.Value62;
                newDataInTemp.Value63 = dataInTemp.Value63;
                newDataInTemp.Value64 = dataInTemp.Value64;
                newDataInTemp.Value65 = dataInTemp.Value65;
                newDataInTemp.Value66 = dataInTemp.Value66;
                newDataInTemp.Value67 = dataInTemp.Value67;
                newDataInTemp.Value68 = dataInTemp.Value68;
                newDataInTemp.Value69 = dataInTemp.Value69;
                newDataInTemp.Value70 = dataInTemp.Value70;
                newDataInTemp.ToopValue = dataInTemp.ToopValue;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除导入临时表记录
        /// </summary>
        /// <param name="tempId">委托主键</param>
        public static void DeleteDataInTempByDataInTempID(string tempId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Sys_DataInTemp dataInTemp = db.Sys_DataInTemp.FirstOrDefault(e => e.TempId == tempId);
            if (dataInTemp != null)
            {
                db.Sys_DataInTemp.DeleteOnSubmit(dataInTemp);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目用户主键删除导入临时表记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        public static void DeleteDataInTempByProjectIdUserId(string projectId, string userId)
        {
            Model.HJGLDB db = Funs.DB;
            var dataInTemp = from x in db.Sys_DataInTemp where x.ProjectId == projectId && x.UserId == userId select x;
            if (dataInTemp.Count() > 0)
            {
                db.Sys_DataInTemp.DeleteAllOnSubmit(dataInTemp);
                db.SubmitChanges();
            }
        }      
    }
}
