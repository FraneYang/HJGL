namespace Model
{
    using System;
    using System.Data.Linq;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Linq.Mapping;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;

    public partial class HJGLDB : System.Data.Linq.DataContext
    {     
        /// <summary>
        /// �õ��˵�
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_Sys_GetMenuByUserId]")]
        public IEnumerable<SpSysMenuItem> SpGetMenuByUserId([Parameter(DbType = "nvarchar(50)")] string UserName, [Parameter(DbType = "nvarchar(50)")] string projectId)
        {                        
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), UserName, projectId);
            return (ISingleResult<SpSysMenuItem>)result.ReturnValue;
        }

        /// <summary>
        /// ��λ�������ȷ���
        /// </summary>
        /// <param name="baw_areano"></param>
        /// <param name="bsu_unitcode"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_unit_baw_analyze]")]
        public IEnumerable<SpRptUnitBawAnalyze> SpRptUnitBawAnalye([Parameter(DbType = "NVARCHAR(400)")] string unitNo, [Parameter(DbType = "NVARCHAR(50)")] string areaNo, [Parameter(DbType = "int")] int? installationId, [Parameter(DbType = "nvarchar(50)")] string ste_steeltype, [Parameter(DbType = "datetime")] DateTime? startTime, [Parameter(DbType = "datetime")] DateTime? endTime, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitNo, areaNo, installationId, ste_steeltype, startTime, endTime, projectId, supervisorUnitId);
            return (ISingleResult<SpRptUnitBawAnalyze>)result.ReturnValue;
        }

        /// <summary>
        /// ��λ������������
        /// </summary>
        /// <param name="unitNo">��λ</param>
        /// <param name="areaNo">����</param>
        /// <param name="installationId">װ��</param>
        /// <param name="date1">��ֹ����</param>
        /// <param name="date2">��������</param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_baw_zlfx]")]
        public IEnumerable<SpRptBawZlfx> SpRptBawZlfx([Parameter(DbType = "NVARCHAR(400)")] string unitNo, [Parameter(DbType = "NVARCHAR(50)")] string areaNo, [Parameter(DbType = "int")] int? installationId, [Parameter(DbType = "datetime")] DateTime? date1, [Parameter(DbType = "datetime")] DateTime? date2, [Parameter(DbType = "NVARCHAR(50)")] string ste_steeltype, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitNo, areaNo, installationId, date1, date2, ste_steeltype, projectId, supervisorUnitId);
            return (ISingleResult<SpRptBawZlfx>)result.ReturnValue;
        }

        /// <summary>
        /// ����ҵ������
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="workareacode"></param>
        /// <param name="steel"></param>
        /// <param name="wloName"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_welderPerformance]")]
        public IEnumerable<SpRpWelderPerformance> SpRptWelderPerformance([Parameter(DbType = "nvarchar(50)")] string unitcode, [Parameter(DbType = "nvarchar(50)")] string workareacode, [Parameter(DbType = "nvarchar(50)")] string steel, [Parameter(DbType = "nvarchar(50)")] string wloName, [Parameter(DbType = "datetime")] DateTime? date1, [Parameter(DbType = "datetime")] DateTime? date2, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitcode, workareacode, steel, wloName, date1, date2, projectId, supervisorUnitId);
            return (ISingleResult<SpRpWelderPerformance>)result.ReturnValue;
        }

        /// <summary>
        /// �����ۺϷ���
        /// </summary>
        /// <param name="unitNo"></param>
        /// <param name="isono"></param>
        /// <param name="areaNo"></param>
        /// <param name="steel"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_iso_analyze]")]
        public IEnumerable<SpRptIsoAnalyze> SpRptIsoAnalyze([Parameter(DbType = "NVARCHAR(50)")] string unitNo, [Parameter(DbType = "VARCHAR(50)")] string isono, [Parameter(DbType = "NVARCHAR(50)")] string areaNo, [Parameter(DbType = "nvarchar(50)")] string steel, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitNo, isono, areaNo, steel, projectId, supervisorUnitId);
            return (ISingleResult<SpRptIsoAnalyze>)result.ReturnValue;
        }

        /// <summary>
        /// Ԥ�ư�װ����
        /// </summary>
        /// <param name="unitNo"></param>
        /// <param name="areaNo"></param>
        /// <param name="steel"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_iso_yzazjd]")]
        public IEnumerable<SpRptIsoYzazjd> SpRptIsoYzazjd([Parameter(DbType = "NVARCHAR(400)")] string unitNo, [Parameter(DbType = "NVARCHAR(50)")] string areaNo, [Parameter(DbType = "nvarchar(50)")] string steel, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitNo, areaNo, steel, projectId, supervisorUnitId);
            return (ISingleResult<SpRptIsoYzazjd>)result.ReturnValue;
        }

        /// <summary>
        /// �����ۺϷ���
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="workareacode"></param>
        /// <param name="sername"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_service]")]
        public IEnumerable<SpRptService> SpRptService([Parameter(DbType = "nvarchar(50)")] string unitcode, [Parameter(DbType = "nvarchar(50)")]string workareacode, [Parameter(DbType = "nvarchar(50)")]string sername, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitcode, workareacode, sername, projectId, supervisorUnitId);
            return (ISingleResult<SpRptService>)result.ReturnValue;
        }

        /// <summary>
        /// ̽���ۺϷ���
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_ndtreport]")]
        public IEnumerable<SpRptndtReport> SpRptndtReport([Parameter(DbType = "nvarchar(50)")] string unitcode, [Parameter(DbType = "nvarchar(50)")] string isono, [Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "NVARCHAR(50)")] string supervisorUnitId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitcode, isono, projectId, supervisorUnitId);
            return (ISingleResult<SpRptndtReport>)result.ReturnValue;
        }

        /// <summary>
        /// �ܵ����ӹ�����¼
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[spJointWorkRecordWeldReportExport]")]
        public IEnumerable<SpRpWeldReportExport> SpRpWeldReportExport([Parameter(DbType = "NVARCHAR(50)")] string projectId, [Parameter(DbType = "nvarchar(50)")] string unitId, [Parameter(DbType = "NVARCHAR(50)")] string workareaId, [Parameter(DbType = "VARCHAR(50)")] string iso_IsoNo, [Parameter(DbType = "datetime")] DateTime? date1, [Parameter(DbType = "datetime")] DateTime? date2)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), projectId, unitId, workareaId, iso_IsoNo, date1, date2);
            return (ISingleResult<SpRpWeldReportExport>)result.ReturnValue;
        }


        /// <summary>
        /// ί�м������һ����
        /// </summary>
        /// <param name="unitcode"></param>
        /// <param name="isono"></param>
        /// <returns></returns>
        [Function(Name = "[dbo].[sp_rpt_Trust_Check]")]
        public IEnumerable<TrustCheckReport> SpTrustCheckReport([Parameter(DbType = "nvarchar(50)")] string unitId, [Parameter(DbType = "NVARCHAR(50)")] string workAreaId, [Parameter(DbType = "NVARCHAR(50)")] string projectId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)MethodInfo.GetCurrentMethod()), unitId, workAreaId, projectId);
            return (ISingleResult<TrustCheckReport>)result.ReturnValue;
        }
    }
}
