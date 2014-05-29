<%@ Page Title="ADO.Net" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeBehind="ADONetAcc.aspx.cs" Inherits="WebUI.ADONetAcc" %>
<%@ Register TagPrefix="a" Src="~/CaptchaControl.ascx" TagName="CaptchaControl" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>ADONet Accessor</h2>
    <p>
        <asp:Button ID="BtnAll" runat="server" OnClick="BtnAll_Click" Text="GetAll" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnFind" runat="server" OnClick="BtnFind_Click" Text="GetByName" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnDelete" runat="server" Text="DeleteByName" Width="108px" OnClick="BtnDelete_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnInsert" runat="server" Text="Insert" OnClick="BtnInsert_Click" />
    </p>
    <asp:Panel ID="Panel2" runat="server" Height="143px" Width="284px">
        <asp:Label ID="LbInfo" runat="server" Text="New Student"></asp:Label>
        <br />
        <asp:Label ID="LbIdStud" runat="server" Text="Id Student : "></asp:Label>
        <asp:TextBox ID="TbIdStud" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LbFIO" runat="server" Text="FIO Student : "></asp:Label>
        <asp:TextBox ID="TbNameStud" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LbIdGr" runat="server" Text="Id Group : "></asp:Label>
        <asp:TextBox ID="TbIdGrp" runat="server"></asp:TextBox>
        <br />
        <a:CaptchaControl ID="captcha" runat="server" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnOk" runat="server" Text="Ok" OnClick="BtnOk_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClear" runat="server" Text="Cancel" OnClick="BtnClear_Click" />
        <br />
    </asp:Panel>
    <asp:Panel ID="PnlMessage" runat="server" Height="16px" Width="357px">
        <asp:Label ID="LbMessage" runat="server" Text="Label"></asp:Label>
</asp:Panel>
        <asp:GridView ID="GrViStud" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
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
