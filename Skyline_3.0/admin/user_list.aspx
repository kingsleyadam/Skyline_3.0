<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="user_list.aspx.cs" Inherits="Skyline_3._0.admin.user_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlUserList" runat="server" CssClass="panel panel-default no-margin">
        <div class="panel-heading">
            <asp:Label ID="lblUserListHeading" runat="server" Text="User List" />
        </div>

        <div class="panel-body">
            <asp:Label ID="lblHelpText" runat="server" Text="Below is a list of all users in the system. To edit a user click on the row of the user you want to edit."></asp:Label>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="grdUsers" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" DataKeyNames="SkylineID"
                GridLines="None" Width="100%" CssClass="table table-striped table-condensed no-margin"
                OnRowDataBound="grdUsers_RowDataBound" OnRowEditing="grdUsers_RowEditing" OnPreRender="grdUsers_PreRender" OnRowCancelingEdit="grdUsers_RowCancelingEdit" OnRowUpdating="grdUsers_RowUpdating" OnPageIndexChanging="grdUsers_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="99px">
                        <EditItemTemplate>
                            <div class="btn-group">
                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-plain" CommandName="Update">Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-plain" CommandName="Cancel"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SkylineID" HeaderText="SkylineID"
                        InsertVisible="False" ReadOnly="True" SortExpression="SkylineID"
                        Visible="False" />
                    <asp:BoundField DataField="ApplicationName" HeaderText="Application Name"
                        SortExpression="ApplicationName" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName"
                        SortExpression="UserName" ReadOnly="True" />
                    <asp:TemplateField ShowHeader="true" SortExpression="Email">
                        <HeaderTemplate>
                            <asp:Label ID="lblEmailHeader" runat="server" Text="Email"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("Email") %>' CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="true" SortExpression="IsLockedOut" ItemStyle-Width="50px">
                        <HeaderTemplate>
                            <asp:Label ID="lblIsLockedOutHeader" runat="server" Text="Locked?"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsLockedOut" runat="server" Checked='<%# Eval("IsLockedOut") %>' Enabled="false" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkIsLockedOut" runat="server" Checked='<%# Eval("IsLockedOut") %>' Enabled="true" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login On"
                        SortExpression="LastLoginDate" ReadOnly="True" />
                    <asp:BoundField DataField="LastPasswordChangedDate"
                        HeaderText="Password Changed On"
                        SortExpression="LastPasswordChangedDate" ReadOnly="True" />
                    <asp:BoundField DataField="FailedPasswordAttemptCount"
                        HeaderText="Failed Logins"
                        SortExpression="FailedPasswordAttemptCount" ReadOnly="True" ItemStyle-Width="100px" />
                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="EditButton" CommandName="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="table-pager" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
