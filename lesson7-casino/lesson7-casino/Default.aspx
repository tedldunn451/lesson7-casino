<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="lesson7_casino.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="dialOneImage" runat="server" Height="150px" Width="150px" />
            <asp:Image ID="dialTwoImage" runat="server" Height="150px" Width="150px" />
            <asp:Image ID="dialThreeImage" runat="server" Height="150px" Width="150px" />
            <br />
            <br />
            Your Bet:
            <asp:TextBox ID="wagerTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="pullLeverButton" runat="server" OnClick="pullLeverButton_Click" Text="Pull The Lever!" />
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="moneyLabel" runat="server"></asp:Label>
            <br />
            <br />
            1 Cherry = 2x Your Bet<br />
            2 Cherries = 3x Your Bet<br />
            3 Cherries = 4x Your Bet<br />
            <br />
            3 7&#39;s = Jackpot! 100x Your Bet<br />
            <br />
            HOWEVER...any BARs and you lose.</div>
    </form>
</body>
</html>
