<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 176px;
        }
        .loginDIv {
            background-color: #D3D3D3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="loginDIv"> 
        <h4 align="center"> Log In</h4>
        <table align="center">
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="emailTB" runat="server" Height="25px" TextMode="Email" Width="175px" placeholder="email"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="RequiredEmailLabel" runat="server" Text="Must specify an email!" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="passwordTB" runat="server" Height="23px" TextMode="Password" Width="175px" placeholder="password"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="RequiredPasswordLabel" runat="server" Text="Must specify a password!" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                
                    <asp:Button ID="forgotpasswordBtn" runat="server" Text="Forgot Password?" Width="183px" OnClick="forgotpasswordBtn_Click"/>
                
                </td>
                <td>
                    <asp:Button ID="submitBtn" runat="server" Text="Submit" OnClick="submitBtn_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red"></asp:Label>   
                </td>
            </tr>
        </table>
    </div>
</asp:Content>