<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTrustItem.aspx.cs" Inherits="Web.TrustManage.ShowTrustItem" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server"  width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <td style="width:100%; background:url('../Images/bg-1.gif')">
              <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:60%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;对应委托单明细
                    </td>
                     <td align="right" valign="middle" style="width: 40%; height: 30px;">
                         &nbsp;</td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr>
             <td>
               <div id="Div1" runat="server" style=" height:420px; overflow: auto;">
                 <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
                         <tr>
                            <td style="width:100%">
                                <asp:GridView ID="gvTrustItem" runat="server" AllowSorting="True" PageSize="500"
                                            AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" 
                                            AlternatingRowStyle-CssClass="GridBgColr">
                                            <AlternatingRowStyle CssClass="GridBgColr" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="管线编号" HeaderStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISO_IsoNo" runat="server" Text='<%# Bind("ISO_IsoNo") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>                 
                                                <asp:TemplateField HeaderText="焊口编号" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbJOT_JointNo" runat="server" Text='<%# Bind("JOT_JointNo") %>' ></asp:Label>
                                                    </ItemTemplate> 
                                                </asp:TemplateField>                                                                                       
                                                <asp:TemplateField HeaderText="焊接区域">
                                                    <ItemTemplate>
                                                        <asp:Label ID="WLO_Code" runat="server" Text='<%# Bind("WLO_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="焊接方法" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="WME_Name" runat="server" Text='<%# Bind("WME_Name") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                </asp:TemplateField> 
                                            </Columns>
                                            <HeaderStyle CssClass="GridBgColr" />
                                            <RowStyle CssClass="GridRow" />
                                        </asp:GridView>
                            </td>
                         </tr>
                    </table>
                 </div>
             </td>
          </tr>
          </table>
    </form>
</body>
</html>

