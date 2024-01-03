<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="LMS_ProjectTraining.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ErrorPage</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center; width:90%">

            <a href="default.aspx"> <img src="LogoImg/404Page.gif" alt="404 error" /></a>
            <div style="text-align:center; font-size:large; border:solid 2px Blue">
                <asp:Label ID="errorDesp" runat="server" Text=" " ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
