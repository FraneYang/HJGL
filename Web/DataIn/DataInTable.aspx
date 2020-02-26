<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataInTable.aspx.cs" Inherits="Web.DataIn.DataInTable" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowDataInTableEdit(tempId) {
            var result = window.showModalDialog("DataInTableEdit.aspx?tempId=" + tempId, "", "status=no;dialogWidth=1200px;dialogHeight=520px;menu=no;resizeable=no;scroll=yes;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("imgDetail").click();
            }
        }

        function ShowDataInTableProgressBar(fileName,type) {
            var result = window.showModalDialog("DataInTableProgressBar.aspx?fileName=" + fileName + "&type=" + type, "", "status=no;dialogWidth=520px;dialogHeight=150px;menu=no;resizeable=no;scroll=yes;scrollbars=yes;center=yes;edge=raise;location=no");
            if (result != null && result != "") {              
                document.getElementById("imgDetail").click();
            }
        }   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">      
       <tr style="height: 30px">       
            <td align="right" style="width: 10%">
                <asp:Label ID="Label6" runat="server" Text="数据文件："></asp:Label>
            </td>     
            <td align="left" style="width: 30%">
                <asp:FileUpload ID="FileExcel" runat="server" Width="98%    "/>
            </td>  
             <td align="left" style="width: 10%">
                <asp:Button ID="imgbtnImport" runat="server" OnClick="imgbtnImport_Click" Text="上传" ToolTip="文件上传！"/>               
            </td>                             
            <td  align="right">
                <asp:CheckBox ID="ckSorIndex" runat="server" Checked="true" Text="按EXCEL行号排序" 
                    Font-Bold="true" ToolTip="按行号或管线焊口号排序" AutoPostBack="true" 
                    oncheckedchanged="ckSorIndex_CheckedChanged" />                  
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" 
                    onclick="btnSave_Click" /> 
                <asp:ImageButton ID="btnAllDelete" runat="server" ImageUrl="~/Images/deletebutton.gif" 
                    onclick="btnAllDelete_Click" ToolTip="删除当前人所有临时导入记录" onclientclick="return confirm(&quot;确定要删除当前所有记录吗？&quot;);"/>
                <asp:ImageButton ID="imgbtnUpload" runat="server" ImageUrl="~/Images/Template.gif"  OnClick="imgbtnUpload_Click" />
                <asp:ImageButton ID="imgDetail" runat="server" Width="0" OnClick="imgDetail_Click" />
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto; height: 480px;">
        <table id="Table3" width="4750px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvJointInfo" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="15" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" 
                        onrowcommand="gvJointInfo_RowCommand" ondatabound="gvJointInfo_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="TempDelete" ToolTip="删除" ImageUrl="~/Images/DeleteBtn.gif" 
                                        CommandArgument='<%# Bind("TempId") %>' onclientclick="return confirm(&quot;确定要删除此条记录吗？&quot;);" ItemStyle-Width="70px"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="序号" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                                <ItemTemplate>
                                    <%# gvJointInfo.PageIndex * gvJointInfo.PageSize + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="EXCEL行号" DataField="RowNo" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="单位代码"  DataField="Value1" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="工区编号"  DataField="Value2" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="管线代号"  DataField="Value3" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left"/>                                                      
                             <asp:TemplateField HeaderText="焊口代号" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Value4" runat="server" CommandName="TempClick" CommandArgument= '<%# Bind("TempId") %>'
                                        Text='<%# Bind("Value4") %>' CssClass="ItemLink" ToolTip='<%# Bind("ToopValue") %>' >
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="材质1代号"  DataField="Value5" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="材质2代号"  DataField="Value6" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="探伤比例代号"  DataField="Value7" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="焊缝类型代号"  DataField="Value8" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="焊接区域"  DataField="Value9" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="焊口属性"  DataField="Value10" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="达因数"  DataField="Value11" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="规格(mm)"  DataField="Value12" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="壁厚"  DataField="Value13" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="焊接方法代码"  DataField="Value14" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="试验压力"  DataField="Value15" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="焊条代号"  DataField="Value16" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="焊丝代号"  DataField="Value17" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="介质代号"  DataField="Value18" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="单线图号"  DataField="Value19" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="设计压力"  DataField="Value20" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="设计温度"  DataField="Value21" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="坡口代号"  DataField="Value22" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="管线等级代号"  DataField="Value23" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="组件一代号"  DataField="Value24" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="组件二代号"  DataField="Value25" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="炉批号一"  DataField="Value26" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="炉批号二"  DataField="Value27" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="所属管段"  DataField="Value28" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="预热温度"  DataField="Value29" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="是否热处理"  DataField="Value30" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="热处理编号"  DataField="Value31" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField HeaderText="焊接位置"  DataField="Value32" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="外径"  DataField="Value33" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="硬度检测比例(数值)"  DataField="Value34" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField HeaderText="探伤类型"  DataField="Value35" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="合格等级"  DataField="Value36" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="是否大管"  DataField="Value37" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="管道编号"  DataField="Value38" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="试压包编号"  DataField="Value39" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="日报编号"  DataField="Value40" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="焊接日期"  DataField="Value41" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                            <asp:BoundField HeaderText="打底焊工"  DataField="Value42" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="盖面焊工"  DataField="Value43" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="点口单号"  DataField="Value44" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="点口日期"  DataField="Value45" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                            <asp:BoundField HeaderText="委托单号"  DataField="Value46" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="委托日期"  DataField="Value47" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                            <asp:BoundField HeaderText="探伤单号"  DataField="Value48" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="探伤日期"  DataField="Value49" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                            <asp:BoundField HeaderText="拍片日期"  DataField="Value50" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                            <asp:BoundField HeaderText="报告日期"  DataField="Value51" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"/>
                            <asp:BoundField HeaderText="拍片总数"  DataField="Value52" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="合格片数"  DataField="Value53" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="报告编号"  DataField="Value54" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField HeaderText="拍片规格"  DataField="Value55" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"/>
                        </Columns>
                        <AlternatingRowStyle CssClass="GridBgColr"/>
                        <HeaderStyle CssClass="GridBgColr"/>
                        <RowStyle CssClass="GridRow" />      
                            <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1"  runat="server"  />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />                            
                    </asp:GridView>
                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.DataInTableService" 
                            SelectCountMethod="getListCount" SelectMethod="getListData"  EnablePaging="true"
                            EnableCaching="false" onselecting="ObjectDataSource1_Selecting">
                         <SelectParameters>
                            <asp:Parameter Name="projectId" />
                            <asp:Parameter Name="userId" />
                            <asp:Parameter Name="isRowNo" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>           
        </table >                    
    </div>   
        <table id="Table4" width="100%" cellpadding="0" cellspacing="0" runat="server">
             <tr style="height:25px">
                  <td align="left" style="width:40%">                    
                    <asp:LinkButton ID="lkAchievements" runat="server" Text="导入说明下载" CssClass="ItemLink" onclick="DataHelp_Click" Font-Bold="true">
                    </asp:LinkButton>
                </td>
                <td align="right" style="width:10%">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red">当前数据说明：</asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lbCout" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                </td>                
            </tr>
        </table>
    </form>
</body>
</html>
