using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class Const
    {        
        #region 按钮操作状态

        /// <summary>
        /// 增加
        /// </summary>
        public const string Add = "Add";

        /// <summary>
        /// 保存
        /// </summary>
        public const string Save = "Save";

        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = "Delete";

        /// <summary>
        /// 取消、作废

        /// </summary>
        public const string Cancel = "Cancel";

        /// <summary>
        /// 修改、调整

        /// </summary>
        public const string Modify = "Modify";

        #endregion

        #region 查询字段：系统设置

        /// <summary>
        /// 管理员ID
        /// </summary>
        public const string GLY = "gly";

        /// <summary>
        /// 管理员ID
        /// </summary>
        public const string AdminId = "admin";

        /// <summary>
        /// 部门表：部门编号
        /// </summary>
        public const string DepartCode = "DepartCode";

        /// <summary>
        /// 部门表：部门名称
        /// </summary>
        public const string DepartName = "DepartName";

        /// <summary>
        /// 人员表：人员名称
        /// </summary>
        public const string UserName = "UserName";

        /// <summary>
        /// 项目表：项目编号
        /// </summary>
        public const string ProjectCode = "ProjectCode";

        /// <summary>
        /// 项目表：项目名称
        /// </summary>
        public const string ProjectName = "ProjectName";

        /// <summary>
        /// 单位信息表：单位代码
        /// </summary>
        public const string UnitCode = "UnitCode";

        /// <summary>
        /// 单位信息表：单位名称
        /// </summary>
        public const string UnitName = "UnitName";

        /// <summary>
        /// 作业区域表：作业区域编号
        /// </summary>
        public const string WorkAreaCode = "WorkAreaCode";

        /// <summary>
        /// 焊接接口表：焊接接口代码
        /// </summary>
        public const string ConsumablesCode = "ConsumablesCode";

        /// <summary>
        /// 焊接等级表：等级代号
        /// </summary>
        public const string PipingClassCode = "PipingClassCode";

        /// <summary>
        /// 焊接等级表：等级名称
        /// </summary>
        public const string PipingClassName = "PipingClassName";

        /// <summary>
        /// 探伤类型表：探伤类型代号
        /// </summary>
        public const string TestingCode = "TestingCode";

        /// <summary>
        /// 探伤类型表：探伤类型名称
        /// </summary>
        public const string TestingType = "TestingType";

        /// <summary>
        /// 探伤比例：探伤比例代码
        /// </summary>
        public const string NDTRCode = "NDTR_Code";

        /// <summary>
        /// 探伤比例：探伤比例名称
        /// </summary>
        public const string NDTRName = "NDTR_Name";

        /// <summary>
        /// 材质：材质代码
        /// </summary>
        public const string STE_Code = "STE_Code";

        /// <summary>
        /// 材质：材质名称
        /// </summary>
        public const string STE_Name = "STE_Name";

        /// <summary>
        /// 材质：钢材类型
        /// </summary>
        public const string STE_SteType = "STE_SteType";

        /// <summary>
        /// 焊缝类型表：焊缝类型编号
        /// </summary>
        public const string JOTY_Code = "JOTY_Code";

        /// <summary>
        /// 焊缝类型表：焊缝类型名称
        /// </summary>
        public const string JOTY_Name = "JOTY_Name";

        /// <summary>
        /// 安装组件表：组件代号
        /// </summary>
        public const string COM_Code = "COM_Code";

        /// <summary>
        /// 安装组件表：组件名称
        /// </summary>
        public const string COM_Name = "COM_Name";

        /// <summary>
        /// 焊接方法表：焊接方法代码
        /// </summary>
        public const string WME_Code = "WME_Code";

        /// <summary>
        /// 焊接方法表：焊接方法名称
        /// </summary>
        public const string WME_Name = "WME_Name";

        /// <summary>
        /// 坡口类型表：坡口代号
        /// </summary>
        public const string JST_Code = "JST_Code";

        /// <summary>
        /// 坡口类型表：坡口名称
        /// </summary>
        public const string JST_Name = "JST_Name";

        /// <summary>
        /// 介质表：介质代号
        /// </summary>
        public const string SER_Code = "SER_Code";

        /// <summary>
        /// 介质表：介质描述
        /// </summary>
        public const string SER_Name = "SER_Name";

        /// <summary>
        /// 试压类型表：试压代号
        /// </summary>
        public const string TPT_Code = "TPT_Code";

        /// <summary>
        /// 试压类型表：试压名称
        /// </summary>
        public const string TPT_TypeName = "TPT_TypeName";

        /// <summary>
        /// 直径寸径对照表：直径代号
        /// </summary>
        public const string BST_DN = "BST_DN";

        #endregion

        #region 系统设置

        /// <summary>
        /// 角色
        /// </summary>
        public const string RoleMenuId = "EBAD373C-8EB4-49A1-91F6-6794FFCAF9B6";

        /// <summary>
        /// 部门设置
        /// </summary>
        public const string DepartMenuId = "0076C79F-39D1-408A-BBFF-30A817A3A93E";

        /// <summary>
        /// 用户
        /// </summary>
        public const string UserMenuId = "E6F0167E-B0FD-4A32-9C47-25FB9E0FDC4E";

        /// <summary>
        /// 修改密码
        /// </summary>
        public const string UpdatePasswordMenuId = "BAB254FA-C879-4463-B9DE-387C241A8623";
        
        /// <summary>
        /// 菜单对应按钮
        /// </summary>
        public const string ButtonToMenuMenuId = "A20C110D-29ED-488B-9676-06ABDF653AEE";

        /// <summary>
        /// 系统功能授权
        /// </summary>
        public const string RolePowerMenuId = "2231022B-3519-42FC-A2E6-1DB9A98039DD";

        /// <summary>
        /// 数据库备份
        /// </summary>
        public const string DataBakMenuId = "9F3A6DDF-C2EA-4AE6-A918-96B7E819A00E";

        /// <summary>
        /// 系统环境设置
        /// </summary>
        public const string SysSetMenuId = "CFB38E5F-AD72-4CB5-8097-5C9C9BF02465";

        /// <summary>
        /// 日志管理
        /// </summary>
        public const string LogMenuId = "3E208088-31E7-4849-A9A1-FCA64B288EF3";

        #endregion

        #region 基础信息

        /// <summary>
        /// 装置设置
        /// </summary>
        public const string InstallationMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0001";

        /// <summary>
        /// 单位设置
        /// </summary>
        public const string UnitMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0002";

        /// <summary>
        /// 施工区域
        /// </summary>
        public const string WorkAreaMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0003";

        /// <summary>
        /// 焊接耗材
        /// </summary>
        public const string ConsumablesMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0004";

        /// <summary>
        /// 管道等级
        /// </summary>
        public const string PipingMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0005";

        /// <summary>
        /// 探伤类型
        /// </summary>
        public const string TestingMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0006";

        /// <summary>
        /// 探伤比例
        /// </summary>
        public const string DetectionMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0007";

        /// <summary>
        /// 焊缝类型
        /// </summary>
        public const string WeldMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0008";

        /// <summary>
        /// 安装组件
        /// </summary>
        public const string ComponentsMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0009";

        /// <summary>
        /// 焊接方法
        /// </summary>
        public const string WeldingMethodMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0010";

        /// <summary>
        /// 直径寸径对照
        /// </summary>
        public const string ControlMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0011";

        /// <summary>
        /// 材质焊材对照
        /// </summary>
        public const string MaterialCompareMenuId = "671B079F-5576-42D7-BEDE-16C6E496BA67";

        /// <summary>
        /// 坡口类型
        /// </summary>
        public const string GrooveMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0012";

        /// <summary>
        /// 材质定义
        /// </summary>
        public const string MaterialMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0013";

        /// <summary>
        /// 介质定义
        /// </summary>
        public const string MediumMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0014";

        /// <summary>
        /// 试压类型
        /// </summary>
        public const string PressureMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0015";

        #endregion

        #region 人员管理

        /// <summary>
        /// 班组管理
        /// </summary>
        public const string TeamGroupMenuId = "8545C52F-2582-4DEB-905E-6E9DCFC13D40";

        /// <summary>
        /// 人员管理
        /// </summary>
        public const string PersonManageMenuId = "1908E4C9-4A63-4A6C-9009-DA4910C2A8C7";
        #endregion

        #region 焊接工艺评定
        /// <summary>
        /// 焊接工艺评定
        /// </summary>
        public const string WeldProcedureMenuId = "EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL";

        /// <summary>
        /// 焊接工艺评定图片
        /// </summary>
        public const string ImageMenuId = "F4B02718-0616-4623-ABCE-885698DDBEB1";

        /// <summary>
        /// 图片路径
        /// </summary>
        public static string ImageUrl = "FileUpload\\Image\\";
        #endregion

        #region 焊接材料
        /// <summary>
        /// 焊丝烘烤记录
        /// </summary>
        public const string ElectrodeBakeMenuId = "B255A0C2-3D63-4A25-92F4-FA8F37F22361";

        /// <summary>
        /// 焊条发放回收记录
        /// </summary>
        public const string ElectrodeRecoveryMenuId = "28EA8CEC-3D63-4FEB-97C7-F33FBC269ABE";

        /// <summary>
        /// 材料到货登记及验收记录
        /// </summary>
        public const string EMaterialRegistMenuId = "7308FFE7-1C6F-4023-AA8C-0B9F103E289D";

        /// <summary>
        /// 焊材库温湿度记录表
        /// </summary>
        public const string EWeldRHRecordMenuId = "5BADA90D-EB19-47DE-9487-CA22517AF415";
        #endregion

        #region 焊接管理

        /// <summary>
        /// 管线管理
        /// </summary>
        public const string PipelineManageMenuId = "8IDKGJE2-09B1-6UIO-3EFM-5TGED48F0001";

        /// <summary>
        ///焊口信息初始化
        /// </summary>
        public const string JointInfoMenuId = "32F5CC8C-E0F4-456C-AB88-77E36269FA50";

        /// <summary>
        /// 焊接日常管理
        /// </summary>
        public const string WeldReportMenuId = "5TYHMD2F-2582-4DEB-905E-6E9DCFEFBGHO";

        /// <summary>
        /// 点口管理
        /// </summary>
        public const string PointMenuId = "3ACE25CE-C5CE-4CEC-AD27-0D5CF1DF2F01";

        #endregion

        #region 检测管理
        /// <summary>
        /// 检测单录入
        /// </summary>
        public const string CheckMenuId = "CDECC461-1897-4D88-BD13-0824E540F565";

        /// <summary>
        /// 检测单录入
        /// </summary>
        public const string CheckAuditMenuId = "3FB934E7-BEFF-48BA-B2D7-268780FA1CE2";

        #endregion

        #region 试压管理
        /// <summary>
        /// 试压包录入
        /// </summary>
        public const string TestPackageManageEditMenuId = "1C6F9CA9-FDAC-4CE5-A19C-5536538851E1";

        /// <summary>
        /// 试压包审核
        /// </summary>
        public const string TestPackageManageAuditMenuId = "55976B16-2C33-406E-B514-2FE42D031071";

        /// <summary>
        /// A项尾工录入
        /// </summary>
        public const string AItemEndCheckMenuId = "24941EDC-CED6-4176-8CCD-EB5F08156D08";

        /// <summary>
        /// B项尾工录入
        /// </summary>
        public const string BItemEndCheckMenuId = "B7EF02DC-45AB-4CFB-ADC8-8340D85D57AD";

        /// <summary>
        /// 试压包完成
        /// </summary>
        public const string TestPackageManageViewMenuId = "82951D78-9029-4F69-A032-00C47551B3E6";

        #endregion

        #region 无损委托
        /// <summary>
        /// 委托录入
        /// </summary>
        public const string TrustManageEditMenuId = "10123AB9-2CC1-494A-8555-FEAB50FCACBE";

        /// <summary>
        /// 委托审核
        /// </summary>
        public const string TrustManageAuditMenuId = "DDAB7C10-91C3-4DE0-8B42-4F055A95235F";
        #endregion

        #region 返修委托
        /// <summary>
        /// 返修委托录入
        /// </summary>
        public const string RepairManageEditMenuId = "352C276B-0E56-4DB5-ACB2-2385AD1E51B1";

        /// <summary>
        /// 返修委托审核
        /// </summary>
        public const string RepairManageAuditMenuId = "D8084A7D-6240-4145-A8AD-4ADA33E7687B";
        #endregion

        #region 热处理管理
        /// <summary>
        /// 热处理录入
        /// </summary>
        public const string HotProessManageEditMenuId = "90579BE7-E38C-4CD2-A3BC-755169FF3BB2";

        /// <summary>
        /// 硬度委托录入
        /// </summary>
        public const string HotHardManageEditMenuId = "72B3E508-1315-4CC3-939F-E840FA701A0E";

        /// <summary>
        /// 硬度委托审核
        /// </summary>
        public const string HotHardManageAuditMenuId = "B0355AED-E873-4398-963C-07243E296A1D";
        #endregion

        #region 焊接报表
        /// <summary>
        /// 单位工区进度分析
        /// </summary>
        public const string UnitAreaAnalyzeMenuId = "66A76F90-96A7-4C1F-B8D9-125DDEACEF52";

        /// <summary>
        /// 单位工区质量分析
        /// </summary>
        public const string UnitAreaQualityMenuId = "88CDDC68-54DE-4E24-9524-A33B80EC0E12";

        /// <summary>
        /// 焊工业绩分析
        /// </summary>
        public const string WelderPerformanceMenuId = "41C22E63-36B7-4C44-89EC-F765BFBB7C55";

        /// <summary>
        /// 管线综合分析
        /// </summary>
        public const string IsoCmprehensiveMenuId = "0ADD01FB-8088-4595-BB40-6A73F332A229";

        /// <summary>
        /// 预制安装进度
        /// </summary>
        public const string PreInstallMenuId = "05495F29-B583-43D9-89D3-3384D6783A3F";

        /// <summary>
        /// 介质综合分析
        /// </summary>
        public const string MediaComMenuId = "575C5154-A135-4737-8682-A129EA717660";

        /// <summary>
        /// 探伤综合分析
        /// </summary>
        public const string DetectionAnalyzeMenuId = "3E2F2FFD-ED2E-4914-8370-D97A68398814";

        /// <summary>
        /// 焊口综合信息
        /// </summary>
        public const string jointComMenuId = "0DC90447-F99B-4002-8E5D-7981A7837A1F";

        /// <summary>
        /// 管线综合信息
        /// </summary>
        public const string IsoCompreInfoMenuId = "CF3CB43C-4031-4CFD-905F-154DC1CB881E";


        #endregion

        #region 通用导入

        /// <summary>
        /// 数据导入
        /// </summary>
        public const string DataInMenuId = "ERDXV53M-09B1-6UIO-3EFM-5DVZDF329001";

        /// <summary>
        /// 数据导入说明
        /// </summary>
        public const string DataInHelpUrl = "Doc\\数据导入说明.doc";

        #endregion

        #region 按钮描述

        /// <summary>
        /// 添加
        /// </summary>
        public const string BtnAdd = "增加";

        /// <summary>
        /// 修改
        /// </summary>
        public const string BtnModify = "修改";

        /// <summary>
        /// 删除
        /// </summary>
        public const string BtnDelete = "删除";

        /// <summary>
        /// 保存
        /// </summary>
        public const string BtnSave = "保存";

        /// <summary>
        /// 打印
        /// </summary>
        public const string BtnPrint = "打印";

        /// <summary>
        /// 数据库备份
        /// </summary>
        public const string BtnDataBak = "数据库备份";

        /// <summary>
        /// 审核
        /// </summary>
        public const string BtnAuditing = "审核";

        /// <summary>
        /// 取消审核
        /// </summary>
        public const string BtnCancelAuditing = "取消审核";

        /// <summary>
        /// 导入
        /// </summary>
        public const string BtnIn = "导入";

        /// <summary>
        /// 导出
        /// </summary>
        public const string BtnOut = "导出";


        #endregion

        #region 角色定义
        /// <summary>
        /// 项目经理
        /// </summary>
        public static string ProjectPrincipalRole = "A184835B-73AF-47FB-9F83-20740CE2FAD7";

        /// <summary>
        /// 项目副经理

        /// </summary>
        public static string LitProjectPrincipalRole = "A184835B-73AF-ERHM-9F83-20740CE2FAD7";

        /// <summary>
        /// 施工经理
        /// </summary>
        public static string CNPrincipalRole = "DBF78A47-F59C-4FE8-9C43-2DD304CB2108";

        /// <summary>
        /// 施工副经理

        /// </summary>
        public static string LitCNPrincipalRole = "AAE579CF-A249-4336-BAFE-7FB4D5753451";

        /// <summary>
        /// 质量经理
        /// </summary>
        public static string CQPrincipalRole = "5CB64EF3-AB0A-40BC-824D-CC314598D5DC";

        /// <summary>
        /// 项目HSSE经理
        /// </summary>
        public static string SecurePrincipalRole = "5F14753D-E295-4D87-B938-E8C8EF5C17BC";

        /// <summary>
        /// 现场HSSE经理
        /// </summary>
        public static string MaterialPrincipalRole = "012B3348-0813-46C0-828E-41CB6457FACD";

        /// <summary>
        /// HSSE工程师

        /// </summary>
        public static string EngineerRole = "9E1180FA-D941-4CC7-9319-BDF61D3F6A2B";

        /// <summary>
        /// 计划工程师

        /// </summary>
        public static string PlanRole = "E1D2CAAF-0564-4ED4-A5C5-1DB3C437FB1D";

        /// <summary>
        /// 专业工程师

        /// </summary>
        public static string PERole = "AAE579CF-A249-4336-BAFE-7FB4D5753451";

        /// <summary>
        /// 土建工程师

        /// </summary>
        public static string CVRole = "AAE5734F-A249-4336-BAFE-7FB4D5753451";

        /// <summary>
        /// 钢结构工程师
        /// </summary>
        public static string STRole = "AAE5734F-A249-4336-BAFE-7FB4D575WE6J";

        /// <summary>
        /// 静设备工程师
        /// </summary>
        public static string EQRole = "AAE576UJ-A249-4336-BAFE-7FB4D5WSYH51";

        /// <summary>
        /// 动设备工程师
        /// </summary>
        public static string MERole = "AAE576UJ-A249-4336-E6YM-7FB4D5753451";

        /// <summary>
        /// 工业炉工程师
        /// </summary>
        public static string FURole = "AAE5ED8I-A249-4336-E6YM-7FB4D5753451";

        /// <summary>
        /// 配管工程师

        /// </summary>
        public static string PPRole = "AAE578IL-A249-4336-BAFE-7FB4D5753451";

        /// <summary>
        /// 暖通工程师
        /// </summary>
        public static string HVRole = "AAE578IL-A249-EBJL-BAFE-7FB4D5753451";

        /// <summary>
        /// 电气工程师

        /// </summary>
        public static string ELRole = "AAE570JA-A249-4336-BAFE-7FB4D5753451";

        /// <summary>
        /// 仪表工程师

        /// </summary>
        public static string INRole = "AAE5757G-A249-4336-BAFE-7FB4D5753451";

        /// <summary>
        /// 焊接工程师

        /// </summary>
        public static string WTRole = "AAE577ID-A249-4336-BAFE-7FB4D5753451";

        #endregion

        #region 模版文件原始的虚拟路径

        /// <summary>
        /// 数据导入模版文件原始的虚拟路径
        /// </summary>
        public const string DataInTemplateUrl = "File\\Excel\\DataIn\\数据导入模版.xls";

        /// <summary>
        /// 焊接日报导入模版文件原始的虚拟路径
        /// </summary>
        public const string WeldReportDataInTemplateUrl = "File\\Excel\\DataIn\\焊接日报导入模板.xls";

        #endregion

        #region 初始化上传路径

        /// <summary>
        /// Excel附件路径
        /// </summary>
        public const string ExcelUrl = "File\\Excel\\";

        #endregion

        #region 报表对应ID
        /// <summary>
        /// 管道焊接工作记录
        /// </summary>
        public const string JointInfoReportId = "1";

        /// <summary>
        /// 管道焊口日报表
        /// </summary>
        public const string JointReportDayReportId = "2";

        /// <summary>
        /// 管道点口日报表
        /// </summary>
        public const string PointReportDayReportId = "3";

        /// <summary>
        /// 无损检测委托单
        /// </summary>
        public const string TrustReportId = "4";

        /// <summary>
        /// 检测通知单
        /// </summary>
        public const string CheckReportId = "5";

        /// <summary>
        /// 硬度检测日委托单
        /// </summary>
        public const string HardCheckReportId = "6";

        /// <summary>
        /// 合格焊工登记表
        /// </summary>
        public const string WelderRecordReportId = "7";

        /// <summary>
        /// 无损检测委托单(2)
        /// </summary>
        public const string TrustReport2Id = "8";

        /// <summary>
        /// 无损检测委托单(第三方)
        /// </summary>
        public const string TrustReport3Id = "9";

        /// <summary>
        /// 无损检测委托单(第三方-神化)
        /// </summary>
        public const string TrustReport4Id = "10";

        /// <summary>
        /// 射线结果确认表
        /// </summary>
        public const string RTCheckResultReportId = "20";

        /// <summary>
        /// 管道焊接接头射线检测比例确认表（一）
        /// </summary>
        public const string WeldJointRTCheck1ReportId = "21";

        /// <summary>
        /// 管道焊接接头射线检测比例确认表（二）
        /// </summary>
        public const string WeldJointRTCheck2ReportId = "22";

        /// <summary>
        /// 管道焊接接头热处理报告（一）
        /// </summary>
        public const string HotHandle1ReportId = "32";

        /// <summary>
        /// 管道焊接接头热处理报告（二）
        /// </summary>
        public const string HotHandle2ReportId = "33";

        /// <summary>
        /// 管道对接焊接接头报检/检查记录
        /// </summary>
        public const string WeldJointCheckReportId = "36";

        #endregion
    }
}
