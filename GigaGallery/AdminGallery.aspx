<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminGallery.aspx.cs" Inherits="AdminGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <asp:SqlDataSource ID="imagesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LaptopConnectionString %>" ProviderName="<%$ ConnectionStrings:LaptopConnectionString.ProviderName %>" SelectCommand="SELECT [image_creation_time], [image_name] FROM [images] WHERE (([image_owner_id] = ?) AND ([image_album_id] = ?)) ORDER BY [image_creation_time] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="image_owner_id" SessionField="pUserId" Type="Int32" />
            <asp:SessionParameter Name="image_album_id" SessionField="selectedAlbumId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <div name="imageAndAlbumSelector" align="center">
        <h4 align="center">Your Gallery</h4>
        <table align="center" align-contet="left" class="auto-style1">
            <tr>
                <td>
                    <asp:Label ID="errorLabel" runat="server" ForeColor="Red" Visible="false" Width="200px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="albumDropDown" runat="server" Width="200px"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button id="selectAlbumBtn" runat="server" OnClick="selectAlbumBtn_Click" Text="Select Album"/>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                    <asp:GridView ID="imagesGridView" runat="server" OnSelectedIndexChanged="imagesGridView_SelectedIndexChanged" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="imagesDataSource" ForeColor="Black" GridLines="Horizontal">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="image_creation_time" HeaderText="image_creation_time" SortExpression="image_creation_time" />
                            <asp:BoundField DataField="image_name" HeaderText="image_name" SortExpression="image_name" />
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
                </td>
                <td>
                    <asp:Image id="selectedImagePreview" runat="server" Width="300px" Height="300px"/>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <th></th>
                <th>
                    <h4>Edit Selected Image</h4>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Button id="deleteImageBtn" runat="server" Text="Delete Selcted Image" OnClick="deleteImageBtn_Click"/>
                </td>
                <td>
                    <asp:TextBox ID="changeImageNameTB" runat="server" placeholder="New Image Name"></asp:TextBox>
                </td>
                <td>
                    <asp:Button id="changeImageNameBtn" runat="server" Text="Change Selected Image Name" OnClick="changeImageNameBtn_Click"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <th></th>
                <th>
                    <h4>Add Image To Current Album</h4>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload id="imageFileUpload" runat="server" Width="224px"/>
                </td>
                <td>
                    <asp:TextBox ID="newImageNameTB" runat="server" placeholder="Image Name"></asp:TextBox>
                </td>
                <td>
                    <asp:Button id="addImageBtn" runat="server" Text="Add Image" OnClick="addImageBtn_Click"/>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <th></th>
                <th>
                    <h4>Edit Selected Album</h4>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Button id="deleteAlbumBtn" runat="server" Text="Delete Selected Album" OnClick="deleteAlbumBtn_Click"/>
                </td>
                <td>
                    <asp:TextBox ID="changeAlbumNameTB" runat="server" placeholder="Change Album Name"></asp:TextBox>
                </td>
                <td>
                    <asp:Button id="changeAlbumNameBtn" runat="server" Text="Change Selected Album's Name" OnClick="changeAlbumNameBtn_Click"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <th></th>
                <th>
                    <h4>Create New Album</h4>
                </th>
            </tr>
            <tr>
                <td>&nbsp</td>
                <td>
                    <asp:TextBox ID="newAlbumName" runat="server" placeholder="New Album Name"></asp:TextBox>
                </td>
                <td>
                    <asp:Button id="createAlbumBtn" runat="server" Text="Create Album" OnClick="createAlbumBtn_Click"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

