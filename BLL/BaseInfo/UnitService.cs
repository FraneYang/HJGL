namespace BLL
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Data.Linq;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using Model;
    using BLL;
    using System.Collections.Generic;

    public static class UnitService
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
        private static IQueryable<Model.Base_Unit> qq = from x in db.Base_Unit where x.UnitType != "5" orderby x.SortIndex select x;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string searchItem, string searchValue, string projectId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.Base_Unit> q = qq;
            if (searchItem != "0")
            {
                if (!string.IsNullOrEmpty(searchValue))
                {
                    if (searchItem == BLL.Const.UnitCode)
                    {
                        q = q.Where(e => e.UnitCode.Contains(searchValue));
                    }
                    if (searchItem == BLL.Const.UnitName)
                    {
                        q = q.Where(e => e.UnitName.Contains(searchValue));
                    }
                }
            }
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.UnitId,
                       x.UnitCode,
                       x.UnitName,
                       x.UnitType,
                       ProjectName = (from p in db.Base_Project where p.ProjectId == x.ProjectId select p.ProjectName).First(),//处理外键
                       x.Corporate,
                       x.Address,
                       x.Telephone,
                       x.Fax,
                       x.ProjectRange,
                       x.Duration,
                       x.InTime,
                       x.OutTime,
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int GetListCount(string searchItem, string searchValue, string projectId)
        {
            return count;
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static Model.Base_Unit GetUnit(string unitId)
        {
            return Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == unitId);
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnits(string unitId)
        {
            return (from x in db.Base_Unit where x.UnitId == unitId select x).ToList();
        }

        /// <summary>
        /// 根据单位名称获取单位信息
        /// </summary>
        /// <param name="unitName"></param>
        /// <returns></returns>
        public static Model.Base_Unit GetUnitByUnitName(string unitName)
        {
            return Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitName == unitName);
        }

        /// <summary>
        /// 根据单位代码获取单位信息
        /// </summary>
        /// <param name="unitCode"></param>
        /// <returns></returns>
        public static Model.Base_Unit GetUnitByUnitCode(string unitCode, string projectId)
        {
            return Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitCode == unitCode && x.ProjectId == projectId);
        }

        /// <summary>
        /// 根据单位类型获取单位信息
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static Model.Base_Unit GetUnitByUnitType(string unitType, string projectId)
        {
            return Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitType == unitType && x.ProjectId == projectId);
        }

        /// <summary>
        /// 根据已作日报单位类型获取单位信息
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetWeldReportUnitsByUnitType(string unitType, string projectId)
        {
            return (from x in db.Base_Unit
                    join y in db.BO_WeldReportMain on x.UnitId equals y.BSU_ID
                    where x.UnitType == unitType && x.ProjectId == projectId && y.ProjectId == projectId
                    select x).Distinct().ToList();
        }

        /// <summary>
        /// 根据单位类型获取单位信息
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitsByUnitType(string unitType, string projectId)
        {
            return (from x in db.Base_Unit where x.UnitType == unitType && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 根据单位名称模糊查询单位信息
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitsByUnitName(string unitName)
        {
            return (from x in db.Base_Unit where x.UnitName.Contains(unitName) select x).ToList();
        }

        /// <summary>
        /// 根据单位编号模糊查询单位信息
        /// </summary>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public static List<Model.Base_Unit> GetUnitsByUnitCode(string unitCode)
        {
            return (from x in db.Base_Unit where x.UnitCode.Contains(unitCode) select x).ToList();
        }

        /// <summary>
        /// 添加单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void AddUnit(string unitCode, string unitName, string unitType, string projectId, string corporate, string address, string telephone, string fax, string projectRange, DateTime? inTime, DateTime? outTime, int? duration,int? sortIndex)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Base_Unit));
            Model.Base_Unit newUnit = new Model.Base_Unit();

            newUnit.UnitId = newKeyID;
            newUnit.UnitCode = unitCode;
            newUnit.UnitName = unitName;
            newUnit.UnitType = unitType;
            newUnit.ProjectId = projectId;
            newUnit.Corporate = corporate;
            newUnit.Address = address;
            newUnit.Telephone = telephone;
            newUnit.Fax = fax;
            newUnit.ProjectRange = projectRange;
            newUnit.InTime = inTime;
            newUnit.OutTime = outTime;
            newUnit.Duration = duration;
            newUnit.SortIndex = sortIndex;

            db.Base_Unit.InsertOnSubmit(newUnit);
            db.SubmitChanges();
        }

        /// <summary>
        /// 添加单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void AddUnit(string unitId, string unitCode, string unitName, string unitType, string projectId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Unit newUnit = new Model.Base_Unit();

            newUnit.UnitId = unitId;
            newUnit.UnitCode = unitCode;
            newUnit.UnitName = unitName;
            newUnit.UnitType = unitType;
            newUnit.ProjectId = projectId;

            db.Base_Unit.InsertOnSubmit(newUnit);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void updateUnit(string unitId, string unitCode, string unitName, string unitType, string projectId, string corporate, string address, string telephone, string fax, string projectRange, DateTime? inTime, DateTime? outTime, int? duration, int? sortIndex)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.First(e => e.UnitId == unitId);
            newUnit.UnitCode = unitCode;
            newUnit.UnitName = unitName;
            newUnit.UnitType = unitType;
            newUnit.Corporate = corporate;
            newUnit.Address = address;
            newUnit.Telephone = telephone;
            newUnit.Fax = fax;
            newUnit.ProjectRange = projectRange;
            newUnit.InTime = inTime;
            newUnit.OutTime = outTime;
            newUnit.Duration = duration;
            newUnit.SortIndex = sortIndex;

            db.SubmitChanges();
        }

        /// <summary>
        /// 修改单位信息
        /// </summary>
        /// <param name="unit"></param>
        public static void updateUnit(string unitId, string unitType)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.First(e => e.UnitId == unitId);
            newUnit.UnitType = unitType;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位ID删除单位信息
        /// </summary>
        /// <param name="unitId"></param>
        public static void DeleteUnitByUnitId(string unitId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.Base_Unit newUnit = db.Base_Unit.First(e => e.UnitId == unitId);
            db.Base_Unit.DeleteOnSubmit(newUnit);
            db.SubmitChanges();
        }

        /// <summary>
        /// 排序下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] SearchList()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("单位代码", BLL.Const.UnitCode);
            list[1] = new ListItem("单位名称", BLL.Const.UnitName);
            return list;
        }

        /// <summary>
        /// 获取单位项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetUnitList()
        {
            var q = (from x in Funs.DB.Base_Unit orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitCode ?? "", q[i].UnitId.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取单位名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetUnitNameList(string projectId)
        {
            var q = (from x in Funs.DB.Base_Unit where x.ProjectId == projectId orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitName ?? "", q[i].UnitId.ToString());
            }
            return list;
        }


        /// <summary>
        /// 获取检测单位名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetCheckUnitList(string projectId)
        {
            var q = (from x in Funs.DB.Base_Unit where x.ProjectId == projectId && x.UnitType == "3" orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitName ?? "", q[i].UnitId.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取分包单位名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSubUnitNameList(string projectId)
        {
            var q = (from x in Funs.DB.Base_Unit where x.ProjectId == projectId && x.UnitType == "2" orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitName ?? "", q[i].UnitId.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取登陆用户分包单位名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSubUnitNameList(string projectId, string unitId)
        {
            var q = (from x in Funs.DB.Base_Unit where x.ProjectId == projectId && x.UnitId == unitId && x.UnitType == "2" orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitName ?? "", q[i].UnitId.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取登陆用户分包单位名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetUnitNameByUnitTypeList(string projectId, string unitType)
        {
            var q = (from x in Funs.DB.Base_Unit where x.ProjectId == projectId && x.UnitType == unitType orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitName ?? "", q[i].UnitId.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取登陆用户分包单位名称项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetSubUnitNameBySupervisorUnitIdList(string projectId, string unitId)
        {
            var q = (from x in Funs.DB.Base_Unit 
                     join y in Funs.DB.Base_WorkArea on x.UnitId equals y.UnitId
                     where x.ProjectId == projectId && (x.UnitId == unitId || y.SupervisorUnitId == unitId) && x.UnitType == "2"
                     orderby x.SortIndex
                     select x).Distinct().ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].UnitName ?? "", q[i].UnitId.ToString());
            }
            return list;
        }

        /// <summary>
        /// 根据项目主键获得单位的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <returns></returns>
        public static int GetUnitCountByProjectId(string projectId)
        {
            var q = (from x in Funs.DB.Base_Unit where x.ProjectId == projectId select x).ToList();
            return q.Count();
        }
    }
}
