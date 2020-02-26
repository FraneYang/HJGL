<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="Web.SysManage.LogList" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
     <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
                            &nbsp;操作日志
                    </td>
                      <td align="right" valign="middle" style="width:55%; height:30px;">
                         <asp:ImageButton ID="btnErr"  runat="server" ImageUrl="~/Images/ErrLog.gif"
                            onclick="btnErr_Click"  Visible="false"/>
                         <asp:ImageButton ID="btnSearch"  runat="server" ImageUrl="~/Images/Search.gif"
                            onclick="btnSearch_Click" />&nbsp;
                      </td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr id="SelectId">
             <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                   <tr style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label1" runat="server" Text="用户"></asp:Label>&nbsp;
                      </td>
                      <td colspan="2" align="left" style="width:70%">
                         <asp:DropDownList ID="drpUser" runat="server" Height="22px" Width="180px">
                        </asp:DropDownList>
                      </td>
                      <td align="left" style="width:40%">
                          <asp:ImageButton ID="btnSuer"  runat="server" ImageUrl="~/Images/sure.gif"
                            onclick="btnSuer_Click" ValidationGroup="Save"/>&nbsp;
                      </td>
                   </tr>

                    <tr style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label3" runat="server" Text="操作日期"></asp:Label>&nbsp;
                      </td>
                      <td colspan="2" align="left" style="width:25%">
                        <input id="txtBegin" runat="server" readonly="readonly" class="Wdate" 
                              style="width:30%; cursor: hand" 
                              onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"/>&nbsp;至

                          <input id="txtEnd" runat="server" readonly="readonly" class="Wdate" 
                              style="width:30%; cursor: hand" 
                              onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"/><asp:CompareValidator ID="CompareValidator1"
                                runat="server" ControlToCompare="txtBegin" ControlToValidate="txtEnd"
                                ErrorMessage="结束时间必须大于开始时间" ForeColor="Red" Operator="GreaterThan" Type="Date"
                                ValidationGroup="Save"></asp:CompareValidator>
                      </td>
                      <td align="left" style="width:40%">
                        <asp:ImageButton ID="btnCancel"  runat="server" ImageUrl="~/Images/cancel.gif"
                            onclick="btnCancel_Click" />&nbsp;
                      </td>
                   </tr>
                </table>
             </td>
          </tr>
          <tr>
             <td>
                 <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
                         <tr>
                            <td style="width:100%">
                                <asp:GridView ID="LogGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                    PageSize="12" DataSourceID="ObjectDataSource1"
                                    AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" ondatabound="LogGridView_DataBound"
                                    >
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="6%">
                                            <ItemTemplate>
                                                <%# LogGridView.PageIndex * LogGridView.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="姓 名" HeaderStyle-Width="15%" DataField="UserName" ItemStyle-Height="15px"/>
                                        <asp:BoundField HeaderText="操作时间" HeaderStyle-Width="20%" DataField="OperationTime" ItemStyle-Height="20px"/>
                                        <asp:BoundField HeaderText="IP地址" HeaderStyle-Width="15%" DataField="Ip" ItemStyle-Height="15px"/>
                                        <asp:BoundField HeaderText="主机名" HeaderStyle-Width="15%" DataField="HostName"  ItemStyle-Height="15px" />
                                        <asp:BoundField HeaderText="操作日志" HeaderStyle-Width="29%" DataField="OperationLog" ItemStyle-Height="29px" />
                                   
                                   </Columns>
                                    <AlternatingRowStyle CssClass="GridBgColr"/>
                                    <HeaderStyle CssClass="GridBgColr"/>
                                    <RowStyle CssClass="GridRow" />      
                                     <PagerTemplate>
                                        <uc1:GridNavgator ID="GridNavgator1"  runat="server"  />
                                    </PagerTemplate>
                                    <PagerStyle HorizontalAlign="Left" />                            
                                </asp:GridView>
                                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.LogService" 
                                     SelectCountMethod="getListCount" SelectMethod="getListData" 
                                    EnablePaging="true"
                                     EnableCaching="false" onselecting="ObjectDataSource1_Selecting">

                                      <SelectParameters>
                                        <asp:Parameter Name ="userId" />
                                        <asp:Parameter Name ="startTime" />   
                                        <asp:Parameter Name ="endTime" />     
                                             <asp:Parameter Name ="projectId" />                                                                                    
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
