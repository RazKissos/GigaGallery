<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center">
        <tr>
            <td>
                <asp:TextBox ID="emailTB" runat="server" Height="25px" TextMode="Email" Width="175px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredEmailValidator" runat="server" ErrorMessage="Must specify an email!" ControlToValidate="emailTB"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="passwordTB" runat="server" Height="23px" TextMode="Password" Width="175px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredPasswordValidator" runat="server" ControlToValidate="passwordTB" ErrorMessage="Must specify a password!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                
                <asp:Button ID="forgotpasswordBtn" runat="server" Text="Forgot Password?" Width="183px" OnClick="forgotpasswordBtn_Click"/>
                
            </td>
            <td>
                <asp:Button ID="submitBtn" runat="server" Text="Submit" OnClick="submitBtn_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>