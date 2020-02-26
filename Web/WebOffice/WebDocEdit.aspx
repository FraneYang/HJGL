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
string strHeight = "100%";
string strTitlebar = "1";
string strToolbar = "1";
string strHost = Request.ServerVariables["HTTP_HOST"];
string strURL = Request.ServerVariables["URL"];
string strDefaultRoot = "http://" + strHost + strURL.Substring(0,strURL.LastIndexOf("/")) ;
string strOpenUrl = "http://" + strHost + strURL.Substring(0,strURL.LastIndexOf("/")) + "/writefile.aspx";

strURL = "http://" + strHost + strURL.Substring(0,strURL.LastIndexOf("/")) + "/uploadedit.aspx";

string strFlsid = Request["flsid"];
string strNum =  Request["num"];
string strFname  =  Request["fname"];
string strFcreator =  Request["fcreator"];
string strFlag = Request["flag"];
    
string flsid,strId;
flsid= "";
 
 string connStr = ConfigurationManager.AppSettings["ConnectionString"];
 SqlConnection  connstr = new SqlConnection(connStr);
 connstr.Open();
    
string strsql;
SqlCommand comm;
SqlDataReader rs;
string strOper = Request["oper"];
string typeFlag = Request["typeFlag"];

if (strOper == "addnew" || strOper == "doedit")
{
    Response.Write("<script language=javascript>window.close();</script>");
    Response.End();
}

if (strOper == "edit" || strOper == "read")
{
    strId = Request.QueryString["id"];
    if (typeFlag == "01")
    {
        strsql = "select * from ActionPlan_ManagerRule where ManagerRuleId='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["ManagerRuleId"].ToString();
            strFname = rs["FileName"].ToString();
            strNum = rs["FileCode"].ToString();
            strFlag = "1";  //word Ϊ1��excelΪ2
        }
        rs.Close();
    }
    
    if (typeFlag == "02")
    {
        strsql = "select * from Unqualified_RectifyNotice where RectifyNoticeCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["RectifyNoticeCode"].ToString();
            strFname = rs["FileName"].ToString();
            strNum = rs["RectifyNoticeCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "03")
    {
        strsql = "select * from Unqualified_PauseNotice where PauseNoticeCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["PauseNoticeCode"].ToString();
            strFname = rs["FileName"].ToString();
            strNum = rs["PauseNoticeCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "04")
    {
        strsql = "select * from Unqualified_RepriseApply where RepriseApplyCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["RepriseApplyCode"].ToString();
            if (rs["RepriseApplyName"].ToString() == "" || rs["RepriseApplyName"] == null)
            {
                strFname = "";
            }
            else
            {
                strFname = rs["RepriseApplyName"].ToString();
            }
            strNum = rs["RepriseApplyCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "05")
    {
        strsql = "select * from Nonconformity_NonconformityNotice where NonconformityNoticeCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["NonconformityNoticeCode"].ToString();
            if (rs["FileName"].ToString() == "" || rs["FileName"] == null)
            {
                strFname = "";
            }
            else
            {
                strFname = rs["FileName"].ToString();
            }
            strNum = rs["NonconformityNoticeCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }


    if (typeFlag == "06")
    {
        strsql = "select * from Nonconformity_NonconformityReview where NonconformityReviewCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["NonconformityReviewCode"].ToString();
            if (rs["FileName"].ToString() == "" || rs["FileName"] == null)
            {
                strFname = "";
            }
            else
            {
                strFname = rs["FileName"].ToString();
            }
            strNum = rs["NonconformityReviewCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "07")
    {
        strsql = "select * from Meeting_MeetingList where MeetingListCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["MeetingListCode"].ToString();
            strFname = rs["MeetingName"].ToString();
            strNum = rs["MeetingListCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }


    if (typeFlag == "08")
    {
        strsql = "select * from Nonconformity_NoFourLetoff where NoFourLetoffCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["NoFourLetoffCode"].ToString();
            strFname = rs["AccidentName"].ToString();
            strNum = rs["NoFourLetoffCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "09")
    {
        strsql = "select * from Solution_ConstructSolution where ConstructSolutionId='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["ConstructSolutionId"].ToString();
            if (rs["FileName"].ToString() == "" || rs["FileName"] == null)
            {
                strFname = "";
            }
            else
            {
                strFname = rs["FileName"].ToString();
            }
            strNum = rs["ConstructSolutionCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "10")
    {
        strsql = "select * from Drawing_DrawingAudit where DrawingAuditCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["DrawingAuditCode"].ToString();
            if (rs["FileName"].ToString() == "" || rs["FileName"] == null)
            {
                strFname = "";
            }
            else
            {
                strFname = rs["FileName"].ToString();
            }
            strNum = rs["DrawingAuditCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "80")
    {
        strsql = "select a.ConstructSolutionId,a.ConstructSolutionCode,b.ProfessionalName from dbo.Solution_ConstructSolutionModel a left join dbo.WBS_CNProfessional b on a.CNProfessionalCode=b.CNProfessionalCode where a.ConstructSolutionId = '" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["ConstructSolutionId"].ToString();
            strFname = rs["ProfessionalName"].ToString();
            strNum = rs["ConstructSolutionCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "90")
    {
        strsql = "select * from TempFile where o_pkid = '" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["o_pkid"].ToString();
            strFname = rs["o_name"].ToString();
            strNum = rs["o_pkid"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "100")
    {
        strsql = "select * from dt_document where o_pkid='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["o_flsid"].ToString();
            strFname = rs["o_name"].ToString();
            strNum = rs["o_number"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }

    if (typeFlag == "110")
    {
        strsql = "select * from View_FinalFile where FinalFileCode='" + strId + "'";
        comm = new SqlCommand(strsql, connstr);
        rs = comm.ExecuteReader();

        if (rs.Read())
        {
            flsid = rs["FinalFileCode"].ToString();
            strFname = rs["FinalFileName"].ToString();
            strNum = rs["FinalFileCode"].ToString();
            strFlag = "1";
        }
        rs.Close();
    }
    
    Response.Write("<script language=javascript>flag='1" + strFlag + "'</script>");
    strOpenUrl = strOpenUrl + "?id=" + strId + "&typeFlag=" + typeFlag;

    strURL = strURL + "?oper=edit&flsid=" + flsid + "&flag=" + strFlag + "&typeFlag=" + typeFlag;

    if (strOper != "read")
    {
        strOper = "doedit";
    }
    else
    {
        strHeight = "500";
        strTitlebar = "0";
        strToolbar = "0";
    }
}


if (strOper == "new")
{
    strFlag = Request.QueryString["flag"];
    if (strFlag != "")
    {
        Response.Write("<script language=javascript>flag='" + strFlag + "';</script>");
    }
    else
    {
        Response.Write("<script language=javascript>alert('�������󣬷������ԣ�');window.close();</script>");
        Response.End();
    }


    flsid = "" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

    strURL = strURL + "?oper=new&flsid=" + flsid;
    strNum = "N" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
    strOper = "addnew";
}   
%>

<html>
<title>WebOffice�칫�ĵ��ؼ���ʾ��--Word,Officeǿ�ƺۼ�����web�ĵ��ؼ�,��дǩ��,���Ӹ���(����ӡ�¡�����ǩ��),֧��ȫ���˵�,��ӡԤ��</title>
<style type="text/css">
        #WebOffice
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            height: 783px;
        }
    </style>
<script language=javascript>
	var strRoot;
var flag;
var strOpenUrl;
var strURL;
strOpenUrl = '<%=strOpenUrl%>';
strURL='<%=strURL%>';
strRoot = '<%=strDefaultRoot%>';
</script>
<script language=javascript src="weboffice.js"></script>

<body topmargin=12 leftmargin=0 onload="javascript:WebOpen();">

<form action="WebDocEdit.aspx?oper=<%=strOper%>" name=frm method="post" onsubmit="return WebSave();"><input type=hidden value=<%=strFlag%> name=flag>
<table width="98%" bgcolor="green" cellpadding="1" cellspacing="1">
    <tr>
        <td bgcolor="white" colspan="2" align="left">
            <%if (strOper != "read")
              {%><br>
            <input type="submit" class="button" value="����ر�" style="width: 80"/>
            <input type="button" class="button" value="�ص��ĵ�" onclick="WebDocReload()"/>
            <input type="button" class="button" value="�򿪱����ļ�" onclick="WebOpenLocal()"/>
            <input type="button" class="button" value="��Ϊ�����ļ�" onclick="WebSaveLocal()"/>
            <%}%>
            <input type=button class="button"   value="���عر�" onclick="javascript:window.close();" style="width:80"/>
            <input type=button class="button"   value="ҳ������"  onclick="WebDocPageSetup()"/>
            <input type=button class="button"   value="��ӡ�ĵ�"  onclick="WebDocPrint()"/>
            <!--<input type=button class="button"   value="�鿴��ҳHTML����" style="width:120"  onclick="window.location = 'view-source:' + window.location.href">
                 <input type=button class="button"   value="�鿴�����ؼ���Javascript����" style="width:220" onclick="window.location = 'view-source:<%=strDefaultRoot%>weboffice.js'">-->
            <br>
            <!--
                &nbsp;&nbsp;<input type=button class="button"   value="���عر�" onclick="javascript:window.close();" style="width:80">
-->
            <table width="100%">
                <tr>
                    <td nowrap>
                        <input type="hidden" name="flsid" value="<%=flsid%>">
                        �ļ���ţ�<input type="text" class="text" name="num" value="<%=strNum%>" style="width:300px"/>
                        &nbsp;&nbsp;�ļ�����<input type="text" class="text" style="width: 300px" name="fname" value="<%=strFname%>">
                    </td>
                </tr>
            </table>
</td></tr><tr>

<td valign=bottom bgcolor=white align=center>
 <object classid="clsid:FF09E4FA-BFAA-486E-ACB4-86EB0AE875D5" 
            codebase="webOffice.ocx#Version=2012,13,17,16" 
            id="WebOffice" width="100%" >
         <param name="BorderStyle" value="1">
          <param name="Caption" value="���Կؼ�">
         <param name="TitlebarColor" value="56000">
         <param name="TitlebarTextColor" value="0">
          <param name="BorderColor" value="10000">  

         <param name="ForeColor" value="1">  
         <param name="Titlebar" value="<%=strTitlebar%>">  
         <param name="Toolbars" value="<%=strToolbar%>">
</object>

</td></tr></table>
<div align=center>

<b style="color:red; font-size:11pt">����</b>
<div align=left style="font-size:10pt">
���������������棬��ô���������������ȫ��[ѡ��˵�]������==��Internetѡ�� ����ѡ��Ի�����ѡ�� ��ȫ ����� �Զ��弶�� �ڵ����İ�ȫ���ÿ������� ��û�б��Ϊ��ȫ��ActiveX�ؼ����г�ʼ���ͽű����� Ϊ<font color=red>����</font>״̬;�鿴ͼ����<a href="sec.jpg" target="_blank">��ȫ����ͼʾ</a>
</div>
<br>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="font-size:10pt" bgcolor="#cccccc" align="center">
                <div>
                    <font color="red">&copy;</font>2004 All Rights Reserved!<br>
                    ��ַ��<a href="http://www.officectrl.com/">http://www.officectrl.com</a>
                    <br>
                </div>
            </td>
        </tr>
    </table>
 
