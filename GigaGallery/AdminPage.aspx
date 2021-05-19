<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 166px;
        }
        .grayDiv {
            background-color: #D3D3D3;
        }
        .auto-style2 {
            width: 177px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <div class="grayDiv" align="center">
        <h2>Admin Page</h2>
        <table>
            <tr>
                <td class="auto-style2">
                    <h4>Add User</h4>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <label>Enter Values to Use</label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
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
                <td class="auto-style2">
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
                <td class="auto-style2">
                    <asp:Button runat="server" id="addUserBtn" onclick="addUserBtn_Click" Text="Add User"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <h4>Search Table</h4>
                </td>
            </tr>
            <tr>
                <%// Find a way to get table names and column names from database and not hardcode it in asp. //%>
                <td class="auto-style2">
                    <label>Table to search in: </label>
                </td>
                <td>
                    <select id="TableSelector" class="auto-style1" >
                        <option>Users</option>
                        <option>Albums</option>
                        <option>Images</option>
                    </select>
                </td>
            <tr>
                <td>
                    <label>Value to search by: </label>
                </td>
                <td>
                    <select id="SerchBySelector" class="auto-style1" >
                        <option>id</option>
                        <option>username</option>
                        <option>date of birth</option>
                        <option>email</option>
                        <option>is admin</option>
                        <option>password</option>
                        <option>album count</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="SearchBar">
                </td>

                <td>
                    <asp:Button runat="server" id="searchDataBtn" onclick="searchDataBtn_Click" Text="Search"></asp:Button>
                </td>
            </tr>
        </table>
        </br>
        <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDeleting="UsersGridView_RowDeleting" OnSelectedIndexChanging="UsersGridView_SelectedIndexChanging">
            <Columns>
                <asp:CommandField />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowSelectButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:SqlDataSource ID="Users" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [albums] WHERE [album_id] = ? AND [album_owner_id] = ? AND [album_name] = ? AND [album_creation_time] = ? AND (([album_size] = ?) OR ([album_size] IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO [albums] ([album_id], [album_owner_id], [album_name], [album_creation_time], [album_size]) VALUES (?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [albums]" UpdateCommand="UPDATE [albums] SET [album_owner_id] = ?, [album_name] = ?, [album_creation_time] = ?, [album_size] = ? WHERE [album_id] = ? AND [album_owner_id] = ? AND [album_name] = ? AND [album_creation_time] = ? AND (([album_size] = ?) OR ([album_size] IS NULL AND ? IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_album_id" Type="Int32" />
                <asp:Parameter Name="original_album_owner_id" Type="Int32" />
                <asp:Parameter Name="original_album_name" Type="String" />
                <asp:Parameter Name="original_album_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="album_id" Type="Int32" />
                <asp:Parameter Name="album_owner_id" Type="Int32" />
                <asp:Parameter Name="album_name" Type="String" />
                <asp:Parameter Name="album_creation_time" Type="DateTime" />
                <asp:Parameter Name="album_size" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="album_owner_id" Type="Int32" />
                <asp:Parameter Name="album_name" Type="String" />
                <asp:Parameter Name="album_creation_time" Type="DateTime" />
                <asp:Parameter Name="album_size" Type="Int32" />
                <asp:Parameter Name="original_album_id" Type="Int32" />
                <asp:Parameter Name="original_album_owner_id" Type="Int32" />
                <asp:Parameter Name="original_album_name" Type="String" />
                <asp:Parameter Name="original_album_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        </br>
        <asp:GridView ID="AlbumGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="album_id" DataSourceID="Albums" ForeColor="Black" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="album_id" HeaderText="album_id" InsertVisible="False" ReadOnly="True" SortExpression="album_id" />
                <asp:BoundField DataField="album_owner_id" HeaderText="album_owner_id" SortExpression="album_owner_id" />
                <asp:BoundField DataField="album_name" HeaderText="album_name" SortExpression="album_name" />
                <asp:BoundField DataField="album_creation_time" HeaderText="album_creation_time" SortExpression="album_creation_time" />
                <asp:BoundField DataField="album_size" HeaderText="album_size" SortExpression="album_size" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
         <asp:SqlDataSource ID="Albums" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" DeleteCommand="DELETE FROM [albums] WHERE [album_id] = ? AND [album_owner_id] = ? AND [album_name] = ? AND [album_creation_time] = ? AND (([album_size] = ?) OR ([album_size] IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO [albums] ([album_id], [album_owner_id], [album_name], [album_creation_time], [album_size]) VALUES (?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM [albums]" UpdateCommand="UPDATE [albums] SET [album_owner_id] = ?, [album_name] = ?, [album_creation_time] = ?, [album_size] = ? WHERE [album_id] = ? AND [album_owner_id] = ? AND [album_name] = ? AND [album_creation_time] = ? AND (([album_size] = ?) OR ([album_size] IS NULL AND ? IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_album_id" Type="Int32" />
                <asp:Parameter Name="original_album_owner_id" Type="Int32" />
                <asp:Parameter Name="original_album_name" Type="String" />
                <asp:Parameter Name="original_album_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="album_id" Type="Int32" />
                <asp:Parameter Name="album_owner_id" Type="Int32" />
                <asp:Parameter Name="album_name" Type="String" />
                <asp:Parameter Name="album_creation_time" Type="DateTime" />
                <asp:Parameter Name="album_size" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="album_owner_id" Type="Int32" />
                <asp:Parameter Name="album_name" Type="String" />
                <asp:Parameter Name="album_creation_time" Type="DateTime" />
                <asp:Parameter Name="album_size" Type="Int32" />
                <asp:Parameter Name="original_album_id" Type="Int32" />
                <asp:Parameter Name="original_album_owner_id" Type="Int32" />
                <asp:Parameter Name="original_album_name" Type="String" />
                <asp:Parameter Name="original_album_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
                <asp:Parameter Name="original_album_size" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
