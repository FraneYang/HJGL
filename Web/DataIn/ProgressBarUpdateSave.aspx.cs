using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.IO;
using System.Linq;
using BLL;
using System.Data.OleDb;
using System.Collections.Generic;

namespace Web.DataIn
{
    public partial class ProgressBarUpdateSave : PPage
    {
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 焊口集合
        /// </summary>
        private List<Model.PW_JointInfo> jointInfos = new List<Model.PW_JointInfo>();

        /// <summary>
        /// 更新焊口集合
        /// </summary>
        //private List<Model.PW_JointInfo> jointInfosUpdate = new List<Model.PW_JointInfo>();

        /// <summary>
        /// 管线集合
        /// </summary>
        private List<Model.PW_IsoInfo> isoInfos = new List<Model.PW_IsoInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string rootPath = Server.MapPath("~/");
            string fileName = rootPath + initPath + Request.Params["fileName"];
            this.hdWorkArea.Value = Request.Params["workAreaId"];
            AddDatasetToSQL();
            //if (fileName != string.Empty && System.IO.File.Exists(fileName))
            //{
            //    System.IO.File.Delete(fileName);//删除上传的XLS文件
            //}
        }

        #region 进度条
        private void beginProgress()
        {
            //根据ProgressBar.htm显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBarUpdateSave.aspx");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("GB2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
        }

        private void setProgress(int percent)
        {
            string jsBlock = "<script>SetPorgressBar('" + percent.ToString() + "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
        }

        private void finishProgress(string result)
        {
            string jsBlock = "<script>SetCompleted('" + result + "');</script>";
            Response.Write(jsBlock);
            Response.Flush();
        }
        #endregion

        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL()
        {
            beginProgress();
            //int a = 0;
            string projectId = this.CurrUser.ProjectId;
            if (Session["jointInfos"] != null)
            {
                jointInfos = Session["jointInfos"] as List<Model.PW_JointInfo>;
            }
            if (Session["isoInfos"] != null)
            {
                isoInfos = Session["isoInfos"] as List<Model.PW_IsoInfo>;
            }
            //if (Session["jointInfosUpdate"] != null)
            //{
            //    jointInfosUpdate = Session["jointInfosUpdate"] as List<Model.PW_JointInfo>;
            //}
            int a = isoInfos.Count();
            int b = jointInfos.Count();
            //int c = jointInfosUpdate.Count();
            int d = a + b;
            for (int i = 0; i < d; i++)
            {
                if (i % (d / 100 + 1) == 0 && i > 0)
                {
                    setProgress(i / (d / 100 + 1));

                    //此处用线程休眠代替实际的操作，如加载数据等
                    //System.Threading.Thread.Sleep(50);
                }
                if (i < a)
                {
                    bool isExistIso = BLL.PW_IsoInfoService.IsExistIsoInfoCode(isoInfos[i].ISO_IsoNo, Request.Params["workAreaId"]);
                    if (isExistIso == false)
                    {
                        isoInfos[i].ISO_ID = SQLHelper.GetNewID(typeof(Model.PW_IsoInfo));
                        isoInfos[i].ProjectId = this.CurrUser.ProjectId;
                        isoInfos[i].ISO_Specification = (from x in jointInfos where x.ISO_ID == isoInfos[i].ISO_IsoNo orderby x.JOT_Size descending select x.JOT_JointDesc).FirstOrDefault();
                        BLL.PW_IsoInfoService.AddIsoInfo(isoInfos[i]);
                    }
                    else
                    {
                        var iso = from x in Funs.DB.PW_IsoInfo
                                where x.ProjectId == projectId && x.ISO_IsoNo == isoInfos[i].ISO_IsoNo
                                && x.BAW_ID == this.hdWorkArea.Value
                                select x;
                       if (iso.Count() > 0)
                       {
                           isoInfos[i].ISO_ID = iso.First().ISO_ID;
                           BLL.PW_IsoInfoService.UpdateExportIso(isoInfos[i]);
                       }
                    }
                }
                else
                {
                    var q = from x in BLL.Funs.DB.PW_IsoInfo where x.ISO_IsoNo == jointInfos[i - a].ISO_ID 
                                && x.BAW_ID == Request.Params["workAreaId"] select x;
                    if (q.Count() > 0)
                    {
                         var jot = from x in BLL.Funs.DB.PW_JointInfo where x.ISO_ID == q.First().ISO_ID
                                && x.JOT_JointNo ==jointInfos[i - a].JOT_JointNo 
                                   select x;
                         
                        if (jot.Count() > 0)
                         {
                             jointInfos[i - a].JOT_ID = jot.First().JOT_ID;
                             BLL.PW_JointInfoService.UpdateExportJoint(jointInfos[i - a]);
                         }
                         else
                         {
                             jointInfos[i - a].ISO_ID = q.First().ISO_ID;
                             jointInfos[i - a].ProjectId = this.CurrUser.ProjectId;
                             //jointInfos[i - a].JOT_JointStatus = "100"; //正常口
                             BLL.PW_JointInfoService.AddJointInfo(jointInfos[i - a]);
                         }
                    }
                }
                //else
                //{
                //    jointInfosUpdate[i - a - b].ISO_ID = BLL.PW_JointInfoService.GetJointInfoByJotID(jointInfosUpdate[i - a - b].JOT_ID).ISO_ID;
                //    jointInfosUpdate[i - a - b].JOT_JointStatus = "100"; //正常口
                //    BLL.PW_JointInfoService.UpdateJointInfo(jointInfosUpdate[i - a - b]);
                //}
            }
            finishProgress("OK");
            return true;
        }
    }
}