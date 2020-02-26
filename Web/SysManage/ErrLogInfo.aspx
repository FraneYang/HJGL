<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrLogInfo.aspx.cs" Inherits="Web.SysManage.ErrLogInfo" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ShowErrLogItem(logId, Begin, End) {
            var iWidth = 700;
            var iHeight = 500;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("ErrLogInfoItem.aspx?logId=" + logId + "&Begin=" + Begin + "&End=" + End, "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no");
        }
  </script>
</head>
<body>
    <form id="form1" runat="server">
       <table id="Table1" runat="server"  width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <td style="width:100%; background:url('../Images/bg-1.gif')">
              <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:45%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;错误日志
                    </td>
                      <td align="right" valign="middle" style="width:55%; height:30px;">   
                       
                            <asp:ImageButton ID="btnReturn"  runat="server" ImageUrl="~/Images/Return.gif"
                            onclick="btnReturn_Click" />&nbsp;
                      </td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr id="SelectId">
             <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">                   
                    <tr style="height:32px">
                      <td align="right" style="width:15%">
                         <asp:Label ID="Label3" runat="server" Text="错误日期"></asp:Label>&nbsp;
                      </td>
                      <td colspan="2" align="left" style="width:45%">
                        <input id="txtBegin" runat="server" readonly="readonly" class="Wdate" 
                              style="width:30%; cursor: hand" 
                              onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"/>&nbsp;至

                          <input id="txtEnd" runat="server" readonly="readonly" class="Wdate" 
                              style="width:30%; cursor: hand" 
                              onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"/>
                      </td>
                      <td align="left" style="width:30%">
                        <asp:ImageButton ID="btnSearch"  runat="server" ImageUrl="~/Images/Search.gif"
                            onclick="btnSearch_Click" />
                        <asp:ImageButton ID="btnExport"  runat="server" ImageUrl="~/Images/Export.gif"
                            onclick="btnExport_Click" />
                         <asp:ImageButton ID="btnDel"  runat="server" ImageUrl="~/Images/deletebutton.gif"
                            onclick="btnDel_Click" />&nbsp;
                      </td>
                   </tr>
                </table>
             </td>
          </tr>
          <tr>
             <td>
                 <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
                         <tr>
                            <td>
                                <asp:GridView ID="gvErrLog" runat="server" AllowPaging="True" AllowSorting="True"
                                    PageSize="12" DataSourceID="ObjectDataSource1"
                                    AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" 
                                    ondatabound="gvErrLog_DataBound" onrowcommand="gvErrLog_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# gvErrLog.PageIndex * gvErrLog.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="错误时间">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnErrTime" runat="server" CommandName="click" CssClass="ItemLink"
                                                    Text='<%# Bind("ErrTime") %>' CommandArgument='<%# Bind("ErrLogId") %>' ToolTip="查看错误详细信息"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="12%" />
                                        </asp:TemplateField>                   
                                         <asp:TemplateField HeaderText="错误文件">
                                            <ItemTemplate>
                                                <asp:Label ID="lblErrUrl" runat="server" Text='<%# Bind("ErrUrlShort") %>' ToolTip='<%# Bind("ErrUrl") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="错误信息">
                                            <ItemTemplate>
                                                <asp:Label ID="lblErrMessage" runat="server" Text='<%# Bind("ErrMessageShort") %>' ToolTip='<%# Bind("ErrMessage") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:TemplateField>                                        
                                         <asp:BoundField HeaderText="项目名称" HeaderStyle-Width="10%" DataField="ProjectName"  /> 
                                        <asp:BoundField HeaderText="单位名称" HeaderStyle-Width="10%" DataField="UnitName"  /> 
                                        <asp:BoundField HeaderText="操作人" HeaderStyle-Width="7%" DataField="UserName" />           
                                                                             
                                     <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/DeleteBtn.gif"
                                                CommandArgument='<%# Bind("ErrLogId") %>' CommandName="Del" OnClientClick="return confirm(&quot;确定要删除此错误信息吗？&quot;)"
                                                ToolTip="删除" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                   </Columns>
                                    <AlternatingRowStyle CssClass="GridBgColr"/>
                                    <HeaderStyle CssClass="GridBgColr"/>
                                    <RowStyle CssClass="GridRow" />      
                                     <PagerTemplate>
                                        <uc1:GridNavgator ID="GridNavgator1"  runat="server"  />
                                    </PagerTemplate>
                                    <PagerStyle HorizontalAlign="Left" />                            
                                </asp:GridView>
                                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.ErrLogInfoService" 
                                     SelectCountMethod="getListCount" SelectMethod="getListData" 
                                    EnablePaging="true"
                                     EnableCaching="false" onselecting="ObjectDataSource1_Selecting">
                                      <SelectParameters>                                     
                                        <asp:Parameter Name ="startTime" />   
                                        <asp:Parameter Name ="endTime" />                                                                                     
                                      </SelectParameters>

                                </asp:ObjectDataSource>
                            </td>
                         </tr>
                    </table>
             </td>
          </tr>
          </table>
    </form>
</body>
</html>
