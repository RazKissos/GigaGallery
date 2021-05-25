<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <style>
        .signupDIv {
            background-color: #D3D3D3;
        }
    </style>
    <div class="signupDIv" name="signupPrompt" align="center">
        <h4>Sign Up</h4>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="emailTB" runat="server" placeholder="email" TextMode="Email"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="requiredEmailLabel" runat="server" Text="This Field is Required!" Visible="false" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="emailErrorLabel" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>

                <td>
                    <asp:TextBox ID="usernameTB" runat="server" placeholder="username"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="requiredUsernameLabel" runat="server" Text="This Field is Required!" Visible="false" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="usernameErrorLabel" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="passwordTB" runat="server" placeholder="password" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="showPasswordCB" runat="server" Text="Show Password" OnCheckedChanged="showPasswordCB_CheckedChanged" AutoPostBack="true" Checked="false"/>
                </td>
                <td>
                    <asp:Label ID="requiredPasswordLabel" runat="server" Text="This Field is Required!" Visible="false" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="passwordErrorLabel" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="passwordConfirmTB" runat="server" placeholder="confirm password" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="requiredPasswordConfirmLabel" runat="server" Text="This Field is Required!" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="birthdayLabel" runat="server" Text="Date of Birth:" ForeColor="Gray"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="birthdayTB" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>

                </td>
                <td>
                    <asp:Label ID="requiredBirthdayLabel" runat="server" Text="This Field is Required!" Visible="false" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="birthdayErrorLabel" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="submitBtn" runat="server" Text="submit" onclick="submitBtnClick"/>
                </td>
                <td>
                    <asp:Label ID="errorLabel" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>

