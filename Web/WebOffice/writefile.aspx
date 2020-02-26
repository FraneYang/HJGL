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
 string connStr = ConfigurationManager.AppSettings["ConnectionString"];
     SqlConnection  connstr = new SqlConnection(connStr);
     connstr.Open();
string strId = Request.QueryString["id"];
string typeFlag = Request["typeFlag"];
string strsql = "";

if (typeFlag == "01")
{
    strsql = "select FileContent from dbo.ActionPlan_ManagerRule where ManagerRuleId='" + strId + "'";
}
    
if (typeFlag == "02")
{
    //strsql = "select o_file from dt_document where o_pkid=" + strId;
    strsql = "select FileContent from Unqualified_RectifyNotice where RectifyNoticeCode='" + strId + "'";
}

if (typeFlag == "03")
{
    strsql = "select FileContent from Unqualified_PauseNotice where PauseNoticeCode='" + strId + "'";
}
    
if (typeFlag == "04")
{
    strsql = "select FileContent from Unqualified_RepriseApply where RepriseApplyCode='" + strId + "'";
}
    
if (typeFlag == "05")
{
    strsql = "select FileContent from Nonconformity_NonconformityNotice where NonconformityNoticeCode='" + strId + "'";
}

if (typeFlag == "06")
{
    strsql = "select FileContent from Nonconformity_NonconformityReview where NonconformityReviewCode='" + strId + "'";
}

if (typeFlag == "07")
{
    strsql = "select FileContent from dbo.Meeting_MeetingList where MeetingListCode='" + strId + "'";
}
        
if (typeFlag == "08")
{
    strsql = "select FileContent from Nonconformity_NoFourLetoff where NoFourLetoffCode='" + strId + "'";
}

if (typeFlag == "09")
{
    strsql = "select FileContent from Solution_ConstructSolution where ConstructSolutionId='" + strId + "'";
}

if (typeFlag == "10")
{
    strsql = "select FileContent from Drawing_DrawingAudit where DrawingAuditCode='" + strId + "'";
}
    
if (typeFlag == "80")
{
    strsql = "select FileContent from dbo.Solution_ConstructSolutionModel  WHERE ConstructSolutionId='" + strId + "'";
}

if (typeFlag == "90")
{
    strsql = "select o_file from dbo.TempFile where o_pkid ='" + strId + "'";
}

if (typeFlag == "100")
{
    strsql = "select o_file from dbo.dt_document where o_pkid ='" + strId + "'";
}

if (typeFlag == "110")
{
    strsql = "select FileContent from dbo.View_FinalFile where FinalFileCode ='" + strId + "'";
}
    
byte[] binValue;
SqlCommand comm;
 SqlDataReader rs;
 comm =new SqlCommand(strsql, connstr); 
 rs = comm.ExecuteReader();
 if( rs.Read() )
 {
     if (!rs.GetSqlBinary(0).IsNull)
     {
         binValue = rs.GetSqlBinary(0).Value;
         Response.BinaryWrite(binValue);
     }
 }
%>
