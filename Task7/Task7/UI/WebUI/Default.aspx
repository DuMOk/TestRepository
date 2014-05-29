<%@ Page Title="Memory" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebUI._Default" %>
<%@ Register TagPrefix="a" Src="~/CaptchaControl.ascx" TagName="CaptchaControl" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Memory Accessor</h2>
    <p>
        <asp:Button ID="Button1" runat="server" onClick="Button1_Click" Text="GetAll" />
    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="GetByName" Width="75px" />
    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="DeleteByName" Width="94px" OnClick="Button3_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="Insert" OnClick="Button4_Click" />
    </p>
    <asp:Panel ID="Panel2" runat="server" Height="95px" Width="284px">
        <asp:Label ID="LbInfo" runat="server" Text="New Person"></asp:Label>
        <br />
        <asp:Label ID="LbFIO" runat="server" Text="Name person : "></asp:Label>
        <asp:TextBox ID="TbNameStud" runat="server"></asp:TextBox>
        <br />
        <a:CaptchaControl ID="captcha" runat="server" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Button ID="BtnOk" runat="server" Text="Ok" OnClick="BtnOk_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClear" runat="server" Text="Cancel" OnClick="BtnClear_Click" />
        <br />
    </asp:Panel>
    <asp:Panel ID="PnlMessage" runat="server" Height="16px" Width="357px">
        <asp:Label ID="LbMessage" runat="server" Text="Label"></asp:Label>
    </asp:Panel>
    <asp:GridView ID="GridView2" runat="server" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
        <AlternatingRowStyle BackColor="Gainsboro" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
</asp:Content>
