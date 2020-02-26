<%@ Page Language="c#" AutoEventWireup="false" %>
<% @ Import Namespace="System" %> 
<% @ Import Namespace="System.Configuration" %>
<% @ Import Namespace="System.Data" %>
<% @ Import Namespace="System.Data.SqlClient" %> 
<% @ Import Namespace="System.Web" %>
<% @ Import Namespace="System.Web.Security" %>
<% @ Import Namespace="System.Web.UI" %>
<% @ Import Namespace="System.Web.UI.HtmlControls" %>
<% @ Import Namespace="System.Web.UI.WebControls" %>
<% @ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<% @ Import Namespace="System.Xml" %> 
<% @ Import Namespace="System.IO" %>
<%
string strOper =  Request.QueryString["oper"];
string strFlsid = Request.QueryString["flsid"];
string typeFlag = Request["typeFlag"];

byte[] b = new byte[Convert.ToInt32( Request.InputStream.Length)];
Request.InputStream.Read(b, 0, Convert.ToInt32(Request.InputStream.Length));

string connStr = ConfigurationManager.AppSettings["ConnectionString"];
     SqlConnection  connstr = new SqlConnection(connStr);
     connstr.Open();
    
SqlCommand comm;
string sql = "";
    
 if (strOper == "new")
 {
     sql = "insert into  dt_document(o_file,o_size,o_flsid) values(@Image,@osize,@flsid)";
 }
 else
 {
     if (typeFlag == "01")
     {
         sql = "Update ActionPlan_ManagerRule Set  FileContent=@Image  WHERE ManagerRuleId=@flsid";
     }
     
     if (typeFlag == "02")
     {
         sql = "Update Unqualified_RectifyNotice Set  FileContent=@Image  WHERE RectifyNoticeCode=@flsid";
         //sql = "update  dt_document set o_file=@Image,o_size=@osize,o_flsid=@flsid  where o_flsid='" + strFlsid + "'";
     }

     if (typeFlag == "03")
     {
         sql = "Update Unqualified_PauseNotice Set  FileContent=@Image  WHERE PauseNoticeCode=@flsid";
         //sql = "update  dt_document set o_file=@Image,o_size=@osize,o_flsid=@flsid  where o_flsid='" + strFlsid + "'";
     }

     if (typeFlag == "04")
     {
         sql = "Update Unqualified_RepriseApply Set  FileContent=@Image  WHERE RepriseApplyCode=@flsid";
     }

     if (typeFlag == "05")
     {
         sql = "Update Nonconformity_NonconformityNotice Set  FileContent=@Image  WHERE NonconformityNoticeCode=@flsid";
     }

     if (typeFlag == "06")
     {
         sql = "Update Nonconformity_NonconformityReview Set  FileContent=@Image  WHERE NonconformityReviewCode=@flsid";
     }
     
     if (typeFlag == "07")
     {
         sql = "Update Meeting_MeetingList Set  FileContent=@Image  WHERE MeetingListCode=@flsid";
     }

     if (typeFlag == "08")
     {
         sql = "Update dbo.Nonconformity_NoFourLetoff Set  FileContent=@Image  WHERE NoFourLetoffCode=@flsid";
     }

     if (typeFlag == "09")
     {
         sql = "Update dbo.Solution_ConstructSolution Set  FileContent=@Image  WHERE ConstructSolutionId=@flsid";
     }

     if (typeFlag == "10")
     {
         sql = "Update dbo.Drawing_DrawingAudit Set  FileContent=@Image  WHERE DrawingAuditCode = @flsid";
     }

     if (typeFlag == "80")
     {
         sql = "Update dbo.Solution_ConstructSolutionModel Set  FileContent=@Image  WHERE ConstructSolutionId=@flsid";
     }

     if (typeFlag == "90")
     {
         sql = "Update dbo.TempFile Set  o_file=@Image  WHERE o_pkid=@flsid";
     }

     if (typeFlag == "100")
     {
         sql = "Update dt_document Set o_size=@osize, o_file=@Image  WHERE o_flsid=@flsid";
     }
 }
 try
 {
     comm = new SqlCommand(sql, connstr);
     comm.Parameters.Add("@Image", SqlDbType.Binary, Convert.ToInt32(Request.InputStream.Length)).Value = b;
     comm.Parameters.Add("@osize", SqlDbType.BigInt, 8).Value = Convert.ToInt32(Request.InputStream.Length);
     comm.Parameters.Add("@flsid", SqlDbType.VarChar, 60).Value = strFlsid;

     comm.ExecuteNonQuery();
 }
 catch (Exception err)
 {
     //  Response.Write("³¤¶È" +Request.InputStream.Length);
     Response.End();
 }
            finally
            {
           
                connstr.Dispose();
                connstr.Close();
            }
%>
