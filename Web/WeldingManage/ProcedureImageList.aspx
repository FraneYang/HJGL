<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcedureImageList.aspx.cs" Inherits="Web.WeldingManage.ProcedureImageList" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <table id="Table1" runat="server"  width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <td style="width:100%; background:url('../Images/bg-1.gif')">
              <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:25%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;工艺图片管理
                    </td>
                     <td align="right" valign="middle" style="width: 75%; height: 30px;">
                         <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" 
                             onclick="btnAdd_Click" />
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
                                <asp:GridView ID="gvPictureList" runat="server" AllowPaging="True" AllowSorting="True"
                                    PageSize="12" DataSourceID="ObjectDataSource1"
                                    AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" 
                                    onrowcommand="gvPictureList_RowCommand" ondatabound="gvPictureList_DataBound" 
                                    >
                                    <AlternatingRowStyle CssClass="GridBgColr"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="图片摘要">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnTitle" runat="server" 
                                                    CommandArgument='<%# Bind("ImageId") %>' CommandName="click" 
                                                    CssClass="ItemLink" Text='<%# Bind("ImageContent") %>' ToolTip="修改"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WME_Name" HeaderText="焊接方法">
                                        <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Thickness" HeaderText="厚度">
                                        <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JOTY_Name" HeaderText="焊缝类型">
                                        <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JST_Name" HeaderText="坡口形式">
                                        <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="查看">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnScanUrl" runat="server" 
                                                    CommandArgument='<%# Bind("ImageId") %>' 
                                                    ImageUrl="~/Images/go_see.gif" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" 
                                                    ToolTip="删除" ImageUrl="~/Images/DeleteBtn.gif" 
                                                    CommandArgument='<%# Bind("ImageId") %>' 
                                                    onclientclick="return confirm(&quot;确定要删除此条信息吗？&quot;);" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="GridBgColr"/>
                                    <RowStyle CssClass="GridRow" />      
                                     <PagerTemplate>
                                        <uc1:GridNavgator ID="GridNavgator1"  runat="server"  />
                                    </PagerTemplate>
                                    <PagerStyle HorizontalAlign="Left" />                            
                                </asp:GridView>
                                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.ProcedureImageService" 
                                     SelectCountMethod="GetListCount" SelectMethod="GetListData" 
                                    EnablePaging="true"
                                     EnableCaching="false">
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

