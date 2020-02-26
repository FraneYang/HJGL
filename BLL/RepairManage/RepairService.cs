using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class RepairService
    {
        public static Model.HJGLDB db = Funs.DB;

        #region 更新焊口号
        /// <summary>
        /// 返修\切除更新焊口号
        /// </summary>
        /// <param name="jot_Id"></param>
        /// <param name="value"></param>
        public static void UpdateNewJointNo(string jot_Id, string value)
        {
            Model.HJGLDB db = Funs.DB;
            var jointInfo = db.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == jot_Id);
            jointInfo.OldJointNo = jointInfo.JOT_JointNo;
            string jointNo = jointInfo.JOT_JointNo;
            string newJointNo = string.Empty;
            ////焊口字符串长度
            int noLength = jointNo.Length;
            ////焊口包含R的索引                
            int indexR = jointNo.LastIndexOf("R");
            ////焊口包含K的索引                
            int indexK = jointNo.LastIndexOf("K1");
            ////焊口包含K1R的索引  
            int indexK1R = jointNo.LastIndexOf("K1R");

            jointInfo.JOT_JointStatus = "101"; ////点口
            if (indexK1R > 0)
            {
                if (value == "C")
                {
                    newJointNo = jointNo.Substring(0, indexK1R);
                    newJointNo = newJointNo + value;                   
                }
                else
                {
                    ////截取R 前的字符串
                    newJointNo = jointNo.Substring(0, indexK1R + 3);
                    string subNo = jointNo.Substring(indexK1R + 3);
                    if (!string.IsNullOrEmpty(subNo))
                    {
                        newJointNo = newJointNo + (Convert.ToInt32(subNo) + 1);
                    }
                    else
                    {
                        newJointNo = newJointNo + "1";
                    }
                }
            }
            else if (indexR > 0)
            {
                if (value == "C")
                {
                    newJointNo = jointNo.Substring(0, indexR);
                    newJointNo = newJointNo + value;

                }
                else
                {
                    ////截取R 前的字符串
                    newJointNo = jointNo.Substring(0, indexR) + value;
                    if (value == "R")
                    {
                        ////截取R 后的字符
                        string subNo = jointNo.Substring(indexR + 1);
                        if (!string.IsNullOrEmpty(subNo))
                        {
                            newJointNo = newJointNo + (Convert.ToInt32(subNo) + 1);
                        }
                        else
                        {
                            newJointNo = newJointNo + "1";
                        }
                    }
                }
            }
            else if (indexK > 0)
            {
                if (value == "C")
                {
                    newJointNo = jointNo.Substring(0, indexK) + value;
                    newJointNo = newJointNo + value;

                }
                else
                {
                    newJointNo = jointNo + "R1";                   
                }
            }
            else
            {
                newJointNo = jointNo + value;
                if (value == "R")
                {
                    newJointNo = newJointNo + "1";
                }
                else
                {
                    jointInfo.JOT_JointStatus = "102"; ////扩透口
                }
            }
            

            if (value == "C")
            {
                jointInfo.JOT_JointStatus = "104"; ////切除口
            }
            
            jointInfo.JOT_JointNo = newJointNo;
            db.SubmitChanges();
        }
        #endregion

        #region 取消更新焊口号
        /// <summary>
        /// 返修\切除更新焊口号
        /// </summary>
        /// <param name="jot_Id"></param> 
        /// <param name="value"></param>
        public static void UpdateCancelAuditJointNo(string jot_Id)
        {
            Model.HJGLDB db = Funs.DB;
            var jointInfo = db.PW_JointInfo.FirstOrDefault(x => x.JOT_ID == jot_Id);

            string jointNo = jointInfo.JOT_JointNo;
            string newJointNo = string.Empty;
            ////焊口字符串长度
            int noLength = jointNo.Length;
            ////焊口包含R的索引                
            int indexR = jointNo.LastIndexOf("R");
            ////焊口包含K的索引                
            int indexK = jointNo.LastIndexOf("K1");

            ////焊口包含C的索引                
            int indexC = jointNo.LastIndexOf("C");
            string jointStatus = jointInfo.JOT_JointStatus;
            if (indexR > 0)
            {
                ////截取R 前的字符串
                newJointNo = jointNo.Substring(0, indexR);               
                ////截取R 后的字符
                string subNo = jointNo.Substring(indexR + 1);
                if (!string.IsNullOrEmpty(subNo))
                {
                    jointStatus = "101"; ////点口
                    if (Convert.ToInt32(subNo) > 1)
                    {
                        if (indexK <= 0)
                        {
                            newJointNo = newJointNo + (Convert.ToInt32(subNo) - 1);   
                        }
                    }                    
                }
            }
            else
            {
                if (indexK > 0)
                {
                    ////截取R 前的字符串
                    newJointNo = jointNo.Substring(0, indexK);
                    ////截取R 后的字符
                    string subNo = jointNo.Substring(indexK + 1);
                    if (!string.IsNullOrEmpty(subNo))
                    {
                        jointStatus = "100"; ////正常口
                        if (Convert.ToInt32(subNo) > 1)
                        {
                            newJointNo = newJointNo + (Convert.ToInt32(subNo) - 1);
                            jointStatus = "102"; ////扩透口
                        }
                    }
                }                
            }

            if (indexC > 0)
            {
                ////截取R 前的字符串
                newJointNo = jointNo.Substring(0, indexC);
                jointStatus = "101"; ////点口
            }
            if (!string.IsNullOrEmpty(jointInfo.OldJointNo))
            {
                jointInfo.JOT_JointNo = jointInfo.OldJointNo;
            }
            else
            {
                if (!string.IsNullOrEmpty(newJointNo))
                {
                    jointInfo.JOT_JointNo = newJointNo;
                }
            }

            jointInfo.JOT_JointStatus = jointStatus;
            db.SubmitChanges();
        }
        #endregion
    }
}
