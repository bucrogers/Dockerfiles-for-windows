<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>
            &nbsp;</h1>
        <asp:Label ID="Label1" runat="server" Text="City"></asp:Label>
        <asp:TextBox ID="cityCtl" runat="server">Chicago</asp:TextBox>
        <asp:Button ID="ctrlRefresh" runat="server" Text="Refresh" OnClick="ctrlRefresh_Click" />
        <asp:Label ID="errorCtl" runat="server" ForeColor="Red" Text="ErrorText"></asp:Label>
    </div>

        <div class="jumbotron">
            AM Commute<br />
            Temp: 
            <asp:Label ID="amTempCtl" runat="server" Text="-1F"></asp:Label>
            &nbsp;
            Wind:
            <asp:Label ID="amWindCtl" runat="server" Text="-1 NNW"></asp:Label>
            &nbsp;
            Precip:
            <asp:Label ID="amPrecipCtl" runat="server" Text="-1in Rain"></asp:Label>
    </div>

    
        <div class="jumbotron">
            PM Commute<br />
            Temp: 
            <asp:Label ID="pmTempCtl" runat="server" Text="-1F"></asp:Label>
            &nbsp;
            Wind:
            <asp:Label ID="pmWindCtl" runat="server" Text="-1 NNW"></asp:Label>
            &nbsp;
            Precip:
            <asp:Label ID="pmPrecipCtl" runat="server" Text="-1in Rain"></asp:Label>
    </div>

    <div class="row">
    </div>

</asp:Content>
