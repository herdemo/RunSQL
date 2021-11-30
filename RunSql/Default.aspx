<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RunSql.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RUN SQL</title>
    <link rel="stylesheet" href="StyleSheet.css" />
</head>
<body>
    <form id="form1" class="form" runat="server">
        <div class="subscribe">
        <h2 class="subscribe__title">Lets Run Your SQL</h2>
            <div>
                <asp:TextBox ID="TBSQL" runat="server" CssClass="form__email" PlaceHolder="Enter your sql"></asp:TextBox>
                 <asp:Button ID="RunSQL" runat="server" class="form__button" Text="Run" OnClick="btn_RunSQL" />
                 
            </div>
            <div>
                <br />
                <asp:TextBox ID="TBResult" TextMode="MultiLine" runat="server" CssClass="form__result"></asp:TextBox>
            </div>
            <div>
                <br />
                 <asp:TextBox ID="TBConStr" runat="server" CssClass="form__ConString"></asp:TextBox>
            </div>
           

        </div>
    </form>
</body>
</html>
