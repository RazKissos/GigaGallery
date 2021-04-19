<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 167px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <table>
        <tr>
            <td>
                <label>Parameter to Search By</label>
            </td>
            <td>
                <label>Enter Value to Search By</label>
            </td>
        </tr>
        <tr>
            <td>
                <select id="SerchBySelector" >
                    <option>id</option>
                    <option>username</option>
                    <option>date of birth</option>
                    <option>email</option>
                    <option>is admin</option>
                    <option>password</option>
                    <option>album count</option>
                </select>
            </td>

            <td>
                <input id="SearchBar">
            </td>

            <td>
                <asp:Button runat="server" id="searchDataBtn" onclick="searchDataBtn_Click" Text="Search"></asp:Button>
            </td>
            <td>
                <td>
                <asp:GridView ID="resultsGridView" runat="server"></asp:GridView>
            </td>
            </td>
        </tr>
        <tr>
            <td>
                <label>Enter Values to Use</label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="idTB" runat="server" placeholder="id" TextMode="Number"></asp:TextBox>
            </td>

            <td>
                <asp:textbox ID="usernameTB" runat="server" placeholder="username"></asp:textbox>  
            </td>
            
            <td>
                <asp:TextBox ID="passwordTB" runat="server" placeholder="password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="emailTB" runat="server" placeholder="email" TextMode="Email"></asp:TextBox>
            </td>

            <td>                
                <asp:TextBox ID="birthdayTB" runat="server" placeholder="birthday" TextMode="Date"></asp:TextBox>
            </td>

            <td>
                <asp:CheckBox ID="isAdminCB" runat="server" Text="Administrator"/>
            </td>
        </tr>
        <tr>
            <td>
                <label>Select Action to Perform</label>
            </td>
        </tr>
        <tr>
            <td>
                <select id="ActionSelector">
                    <option>add</option>
                    <option>delete</option>
                    <option>update</option>
                </select>
            </td>

            <td>
                <asp:Button runat="server" id="performActionBtn" onclick="performActionBtn_Click" Text="Perform Action"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
