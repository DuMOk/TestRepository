<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="MyORMAcc.aspx.cs" Inherits="WebUI.MyORMAcc" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>MyORM Accessor</h2>
    <p>
        Select the table:<br />
        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="SelectTbl" Text="Student" />
        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="SelectTbl" Text="Group" />
    </p>
    <p>
    <asp:Button ID="BtnAll" runat="server" OnClick="BtnAll_Click" Text="GetAll" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnFind" runat="server" OnClick="BtnFind_Click" Text="GetByName" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnDelete" runat="server" Text="DeleteByName" Width="108px" OnClick="BtnDelete_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnInsert" runat="server" Text="Insert" OnClick="BtnInsert_Click" />
    </p>
    <asp:Panel ID="Panel2" runat="server" Height="119px" Width="194px">
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
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnOk" runat="server" Text="Ok" OnClick="BtnOk_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClear" runat="server" Text="Cancel" OnClick="BtnClear_Click" />
        <br />
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" Height="90px" Width="194px">
        <asp:Label ID="LbInfo0" runat="server" Text="New Group"></asp:Label>
        <br />
        <asp:Label ID="LbIdGrp" runat="server" Text="Id Group : "></asp:Label>
        <asp:TextBox ID="TbIdGroup" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LbNameGrp" runat="server" Text="Name Group: "></asp:Label>
        <asp:TextBox ID="TbNameGroup" runat="server"></asp:TextBox>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnOk1" runat="server" Text="Ok" OnClick="BtnOk1_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnClear1" runat="server" Text="Cancel" OnClick="BtnClear1_Click" />
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
