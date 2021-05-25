<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminProfile.aspx.cs" Inherits="AdminProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <style type="text/css">
        .head-tr-style{
            height: 20px;
        }
        .prof-img{
            width: 200px;
            height: 200px;
            border-radius: 50%;
        }
        .prof-div {
            background-color: #D3D3D3;
        }
        .td-style {
            width: 250px;
            height: 225px;
        }
        .label-style{

            width: 75px;
        }
    </style>
    <div class="prof-div">
        <table align="center">
            <tr class="head-tr-style">
                <h4 align="center">Profile Page</h4>
            </tr>
            <tr>
                <td class="td-style">

                </td>
                <td class="td-style" id="profilePic">
                    <img class="prof-img" src="Design/images/blankProfilePic.png" />
                </td>
            </tr>
            <tr>
                <td class="td-style" id="InfoCell">
                    <label class="label-style">
                        Username: 
                    </label>
                    &nbsp;
                    <asp:Label ID="UsernameLabel" runat="server" Font-Bold="true">Avi Avraham</asp:Label>
                    <br>
                    <label class="label-style">
                        Email:  
                    </label>
                    &nbsp;
                    <asp:Label ID="EmailLabel" runat="server" Font-Bold="true">Avi@gmail.com</asp:Label>
                </td>
                <td class="td-style" id="ChangeNameOrPassword">
                    <asp:Button ID="ChangeUsernameOrPassword" runat="server" Text="Change Username / Password"/>
                </td>
                <td class="td-style">
                    
                    <asp:Button ID="logoutBtn" runat="server" OnClick="logoutBtn_Click" text="log out" />
                    
                </td>
            </tr>

        </table>
    </div>
</asp:Content>
