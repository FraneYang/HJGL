using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.TestPackageManage
{
    public partial class ShowJointInfoView : PPage
    {
        #region 定义项
        /// <summary>
        /// 按钮权限列表
        /// </summary>
        public string ISO_ID
        {
            get
            {
                return (string)ViewState["ISO_ID"];
            }
            set
            {
                ViewState["ISO_ID"] = value;
            }
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ISO_ID = Request.Params["iSOID"];
            }
        }
        #endregion

        #region 绑定参数
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["iso_id"] = this.ISO_ID;
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;       
        }
        #endregion

        #region GridView绑定
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointInfo_DataBound(object sender, EventArgs e)
        {
            if (this.gvJointInfo.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvJointInfo.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvJointInfo;
        }
        #endregion

        #region 转换字符串类型
        /// <summary>
        /// 转换字符串型
        /// </summary>
        /// <param name="isuse"></param>
        /// <returns></returns>     
        protected string ConvertString(object b)
        {
            if (b != null)
            {
                if (b.ToString() == "1")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            return "";
        }

        /// <summary>
        /// 转换委托字符串
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected string ConvertStringTrustFlag(object flag)
        {
            if (flag != null)
            {
                if (flag.ToString() == "00")
                {
                    return "未下委托";
                }
                else if (flag.ToString() == "01")
                {
                    return "一次委托,未审核";
                }
                else if (flag.ToString() == "02")
                {
                    return "一次委托,已审核";
                }
                else if (flag.ToString() == "11")
                {
                    return "二次委托,未审核";
                }
                else if (flag.ToString() == "12")
                {
                    return "二次委托,已审核";
                }
                else if (flag.ToString() == "21")
                {
                    return "三次委托,未审核";
                }
                else if (flag.ToString() == "22")
                {
                    return "三次委托,已审核";
                }
            }
            return "";
        }

        /// <summary>
        /// 转换探伤字符串
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        protected string ConvertStringCheckFlag(object flag)
        {
            if (flag != null)
            {
                if (flag.ToString() == "00")
                {
                    return "未检测";
                }
                else if (flag.ToString() == "01")
                {
                    return "一次检测,未审核";
                }
                else if (flag.ToString() == "02")
                {
                    return "一次检测,已审核";
                }
                else if (flag.ToString() == "11")
                {
                    return "二次检测,未审核";
                }
                else if (flag.ToString() == "12")
                {
                    return "二次检测,已审核";
                }
                else if (flag.ToString() == "21")
                {
                    return "三次检测,未审核";
                }
                else if (flag.ToString() == "22")
                {
                    return "三次检测,已审核";
                }
            }
            return "";
        }
        /// <summary>
        /// 转换焊口状态字符串
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string ConverStringJointStatus(object status)
        {
            if (status != null)
            {
                if (status.ToString() == "100")
                {
                    return "正常";
                }
                else if (status.ToString() == "102")
                {
                    return "扩透";
                }
                else if (status.ToString() == "101")
                {
                    return "点口";
                }
                else if (status.ToString() == "104")
                {
                    return "已切除";
                }
            }
            return "";
        }
        #endregion     
    }
}