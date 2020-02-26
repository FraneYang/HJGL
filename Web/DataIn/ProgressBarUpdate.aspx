<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgressBarUpdate.aspx.cs" Inherits="Web.DataIn.ProgressBarUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据审核进度页面</title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function SetPorgressBar(pos) {
            //设置进度条居中
            var screenHeight = 250;
            var screenWidth = 480;
            ProgressBarSide.style.width = 400;
            ProgressBarSide.style.left = 40;
            ProgressBarSide.style.top = 50;
            ProgressBarSide.style.height = "21px";
            ProgressBarSide.style.display = "";

            //设置进度条百分比                       
            ProgressBar.style.width = pos + "%";
            ProgressText.innerHTML = pos + "%";
        }

        //完成后隐藏进度条
        function SetCompleted(result) {
            ProgressBarSide.style.display = "none";
            window.returnValue = result;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div id="div1" runat="server" style="height: 20px;">
        <input id="hdWorkArea" type="hidden" runat="server" />
       <table id="tabbtn" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold">
                &nbsp;&nbsp;&nbsp;&nbsp;数据审核处理中，请稍等......
            </td>
        </tr>
       </table>
    </div>
    
    <div id="ProgressBarSide" style="position: absolute; height: 21px; width: 100px;
        color: Silver; border-width: 1px; border-style: Solid; display: none">
        <div id="ProgressBar" style="position: absolute; left: 0px; height: 21px; width: 0%;
            background-color: #3366FF">
        </div>
        <div id="ProgressText" style="position: absolute; left: 0px; height: 21px; width: 100%;
            text-align: center">
        </div>
    </div>
    </form>
</body>
</html>
