<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="WebUI.CaptchaControl" %>

<asp:Image ID="img" runat="server" ImageUrl="~/CaptchaHandler.ashx" />
<asp:TextBox ID="txtInput" runat="server"></asp:TextBox>