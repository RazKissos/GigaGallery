<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tdStyle{
            width:200px;
            height: 30px;
        }
        .grayDiv {
            background-color: #D3D3D3;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <div class="grayDiv" align="center">
        <h2>Admin Page</h2>
        <table>
            <tr>
                <td class="tdStyle">
                    <h4>Add User</h4>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    <label>Enter Values to Use</label>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    <asp:textbox ID="usernameTB" runat="server" placeholder="username"></asp:textbox>  
                </td>
            
                <td class="tdStyle">
                    <asp:TextBox ID="passwordTB" runat="server" placeholder="password"></asp:TextBox>
                </td>
                <td class="tdStyle">
                    <asp:Label ID="errorLabel" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    <asp:TextBox ID="emailTB" runat="server" placeholder="email" TextMode="Email"></asp:TextBox>
                </td>

                <td class="tdStyle">                
                    <asp:TextBox ID="birthdayTB" runat="server" placeholder="birthday" TextMode="Date"></asp:TextBox>
                </td>

                <td class="tdStyle">
                    <asp:CheckBox ID="isAdminCB" runat="server" Text="Administrator"/>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    <asp:Button runat="server" id="addUserBtn" onclick="addUserBtn_Click" Text="Add User"></asp:Button>
                </td>
            </tr>
            <tr>
                <td class="tdStyle">
                    <h4>Search Table</h4>
                </td>
            </tr>
            <tr>
                <%// Find a way to get table names and column names from database and not hardcode it in asp. //%>
                <td class="tdStyle">
                    <label>Table to search in: </label>
                </td>
                <td class="tdStyle">
                    <select id="TableSelector" class="auto-style1" >
                        <option>Users</option>
                        <option>Albums</option>
                        <option>Images</option>
                    </select>
                </td>
            <tr>
                <td class="tdStyle">
                    <label>Value to search by: </label>
                </td>
                <td class="tdStyle">
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
                <td class="tdStyle">
                    <input id="SearchBar">
                </td>

                <td class="tdStyle">
                    <asp:Button runat="server" id="searchDataBtn" onclick="searchDataBtn_Click" Text="Search"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <h4 align="center">Users</h4>
        <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="user_id" DataSourceID="Users" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="user_id" HeaderText="user_id" InsertVisible="False" ReadOnly="True" SortExpression="user_id" />
                <asp:BoundField DataField="user_name" HeaderText="user_name" SortExpression="user_name" />
                <asp:BoundField DataField="user_password" HeaderText="user_password" SortExpression="user_password" />
                <asp:BoundField DataField="user_email" HeaderText="user_email" SortExpression="user_email" />
                <asp:BoundField DataField="user_album_count" HeaderText="user_album_count" SortExpression="user_album_count" />
                <asp:BoundField DataField="user_birthday" HeaderText="user_birthday" SortExpression="user_birthday" />
                <asp:CheckBoxField DataField="is_admin" HeaderText="is_admin" SortExpression="is_admin" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:SqlDataSource ID="Users" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:LaptopConnectionString %>" DeleteCommand="DELETE FROM [users] WHERE [user_id] = ? AND [user_name] = ? AND [user_password] = ? AND [user_email] = ? AND (([user_album_count] = ?) OR ([user_album_count] IS NULL AND ? IS NULL)) AND [user_birthday] = ? AND [is_admin] = ?" InsertCommand="INSERT INTO [users] ([user_id], [user_name], [user_password], [user_email], [user_album_count], [user_birthday], [is_admin]) VALUES (?, ?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:LaptopConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [users]" UpdateCommand="UPDATE [users] SET [user_name] = ?, [user_password] = ?, [user_email] = ?, [user_album_count] = ?, [user_birthday] = ?, [is_admin] = ? WHERE [user_id] = ? AND [user_name] = ? AND [user_password] = ? AND [user_email] = ? AND (([user_album_count] = ?) OR ([user_album_count] IS NULL AND ? IS NULL)) AND [user_birthday] = ? AND [is_admin] = ?">
            <DeleteParameters>
                <asp:Parameter Name="original_user_id" Type="Int32" />
                <asp:Parameter Name="original_user_name" Type="String" />
                <asp:Parameter Name="original_user_password" Type="String" />
                <asp:Parameter Name="original_user_email" Type="String" />
                <asp:Parameter Name="original_user_album_count" Type="Int32" />
                <asp:Parameter Name="original_user_album_count" Type="Int32" />
                <asp:Parameter Name="original_user_birthday" Type="DateTime" />
                <asp:Parameter Name="original_is_admin" Type="Boolean" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="user_id" Type="Int32" />
                <asp:Parameter Name="user_name" Type="String" />
                <asp:Parameter Name="user_password" Type="String" />
                <asp:Parameter Name="user_email" Type="String" />
                <asp:Parameter Name="user_album_count" Type="Int32" />
                <asp:Parameter Name="user_birthday" Type="DateTime" />
                <asp:Parameter Name="is_admin" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="user_name" Type="String" />
                <asp:Parameter Name="user_password" Type="String" />
                <asp:Parameter Name="user_email" Type="String" />
                <asp:Parameter Name="user_album_count" Type="Int32" />
                <asp:Parameter Name="user_birthday" Type="DateTime" />
                <asp:Parameter Name="is_admin" Type="Boolean" />
                <asp:Parameter Name="original_user_id" Type="Int32" />
                <asp:Parameter Name="original_user_name" Type="String" />
                <asp:Parameter Name="original_user_password" Type="String" />
                <asp:Parameter Name="original_user_email" Type="String" />
                <asp:Parameter Name="original_user_album_count" Type="Int32" />
                <asp:Parameter Name="original_user_album_count" Type="Int32" />
                <asp:Parameter Name="original_user_birthday" Type="DateTime" />
                <asp:Parameter Name="original_is_admin" Type="Boolean" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <h4 align="center">Albums</h4>
        <asp:GridView ID="AlbumGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="album_id" DataSourceID="Albums" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="album_id" HeaderText="album_id" InsertVisible="False" ReadOnly="True" SortExpression="album_id" />
                <asp:BoundField DataField="album_owner_id" HeaderText="album_owner_id" SortExpression="album_owner_id" />
                <asp:BoundField DataField="album_name" HeaderText="album_name" SortExpression="album_name" />
                <asp:BoundField DataField="album_creation_time" HeaderText="album_creation_time" SortExpression="album_creation_time" />
                <asp:BoundField DataField="album_size" HeaderText="album_size" SortExpression="album_size" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
         <asp:SqlDataSource ID="Albums" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:LaptopConnectionString %>" DeleteCommand="DELETE FROM [albums] WHERE [album_id] = ? AND [album_owner_id] = ? AND [album_name] = ? AND [album_creation_time] = ? AND (([album_size] = ?) OR ([album_size] IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO [albums] ([album_id], [album_owner_id], [album_name], [album_creation_time], [album_size]) VALUES (?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:LaptopConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [albums]" UpdateCommand="UPDATE [albums] SET [album_owner_id] = ?, [album_name] = ?, [album_creation_time] = ?, [album_size] = ? WHERE [album_id] = ? AND [album_owner_id] = ? AND [album_name] = ? AND [album_creation_time] = ? AND (([album_size] = ?) OR ([album_size] IS NULL AND ? IS NULL))">
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
        <br />
        <h4 align="center">Images</h4>
        <asp:GridView ID="ImagesGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="image_id" DataSourceID="Images" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="image_id" HeaderText="image_id" InsertVisible="False" ReadOnly="True" SortExpression="image_id" />
                <asp:BoundField DataField="image_album_id" HeaderText="image_album_id" SortExpression="image_album_id" />
                <asp:BoundField DataField="image_owner_id" HeaderText="image_owner_id" SortExpression="image_owner_id" />
                <asp:BoundField DataField="image_name" HeaderText="image_name" SortExpression="image_name" />
                <asp:BoundField DataField="image_creation_time" HeaderText="image_creation_time" SortExpression="image_creation_time" />
                <asp:BoundField DataField="image_file_path" HeaderText="image_file_path" SortExpression="image_file_path" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:SqlDataSource ID="Images" runat="server" ConnectionString="<%$ ConnectionStrings:LaptopConnectionString %>" ProviderName="<%$ ConnectionStrings:LaptopConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [images]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [images] WHERE [image_id] = ? AND (([image_album_id] = ?) OR ([image_album_id] IS NULL AND ? IS NULL)) AND (([image_owner_id] = ?) OR ([image_owner_id] IS NULL AND ? IS NULL)) AND (([image_name] = ?) OR ([image_name] IS NULL AND ? IS NULL)) AND (([image_creation_time] = ?) OR ([image_creation_time] IS NULL AND ? IS NULL)) AND [image_file_path] = ?" InsertCommand="INSERT INTO [images] ([image_id], [image_album_id], [image_owner_id], [image_name], [image_creation_time], [image_file_path]) VALUES (?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [images] SET [image_album_id] = ?, [image_owner_id] = ?, [image_name] = ?, [image_creation_time] = ?, [image_file_path] = ? WHERE [image_id] = ? AND (([image_album_id] = ?) OR ([image_album_id] IS NULL AND ? IS NULL)) AND (([image_owner_id] = ?) OR ([image_owner_id] IS NULL AND ? IS NULL)) AND (([image_name] = ?) OR ([image_name] IS NULL AND ? IS NULL)) AND (([image_creation_time] = ?) OR ([image_creation_time] IS NULL AND ? IS NULL)) AND [image_file_path] = ?">
            <DeleteParameters>
                <asp:Parameter Name="original_image_id" Type="Int32" />
                <asp:Parameter Name="original_image_album_id" Type="Int32" />
                <asp:Parameter Name="original_image_album_id" Type="Int32" />
                <asp:Parameter Name="original_image_owner_id" Type="Int32" />
                <asp:Parameter Name="original_image_owner_id" Type="Int32" />
                <asp:Parameter Name="original_image_name" Type="String" />
                <asp:Parameter Name="original_image_name" Type="String" />
                <asp:Parameter Name="original_image_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_image_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_image_file_path" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="image_id" Type="Int32" />
                <asp:Parameter Name="image_album_id" Type="Int32" />
                <asp:Parameter Name="image_owner_id" Type="Int32" />
                <asp:Parameter Name="image_name" Type="String" />
                <asp:Parameter Name="image_creation_time" Type="DateTime" />
                <asp:Parameter Name="image_file_path" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="image_album_id" Type="Int32" />
                <asp:Parameter Name="image_owner_id" Type="Int32" />
                <asp:Parameter Name="image_name" Type="String" />
                <asp:Parameter Name="image_creation_time" Type="DateTime" />
                <asp:Parameter Name="image_file_path" Type="String" />
                <asp:Parameter Name="original_image_id" Type="Int32" />
                <asp:Parameter Name="original_image_album_id" Type="Int32" />
                <asp:Parameter Name="original_image_album_id" Type="Int32" />
                <asp:Parameter Name="original_image_owner_id" Type="Int32" />
                <asp:Parameter Name="original_image_owner_id" Type="Int32" />
                <asp:Parameter Name="original_image_name" Type="String" />
                <asp:Parameter Name="original_image_name" Type="String" />
                <asp:Parameter Name="original_image_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_image_creation_time" Type="DateTime" />
                <asp:Parameter Name="original_image_file_path" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
    </div>
</asp:Content>
