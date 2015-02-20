<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="account_info.aspx.cs" Inherits="Skyline_3._0.user.account_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlAccountSettings" runat="server" CssClass="panel panel-default no-margin">
        <div class="panel-heading">
            <h3 class="panel-title">Account Settings</h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <asp:Label ID="lblUsernameLabel" runat="server" Text="Username"></asp:Label>
                <p class="help-block">
                    <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                </p>
            </div>

            <div class="form-group">
                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" AssociatedControlID="txtCompanyName"></asp:Label>
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Company Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqCompanyName" runat="server" ErrorMessage="Company Name required (if no company name insert 'N/A'" ControlToValidate="txtCompanyName" Display="None" ValidationGroup="UpdateInfo" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name" AssociatedControlID="txtFirstName"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="First Name required" ControlToValidate="txtFirstName" Display="None" ValidationGroup="UpdateInfo" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name" AssociatedControlID="txtLastName"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="Last Name required" ControlToValidate="txtLastName" Display="None" ValidationGroup="UpdateInfo" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="Email Address" AssociatedControlID="txtEmail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="Email required" ControlToValidate="txtEmail" Display="None" ValidationGroup="UpdateInfo" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblLastLoginDateLabel" runat="server" Text="Last Login Date"></asp:Label>
                <p class="help-block">
                    <asp:Label ID="lblLastLoginDate" runat="server" Text="DATE"></asp:Label>
                </p>
            </div>

            <div class="form-group">
                <asp:CheckBox ID="chkEmailNotify" runat="server" CssClass="checkbox-inline" Text="Receive Email Notifications?" />
            </div>

            <div>
                <asp:ValidationSummary ID="valSummary" CssClass="valSummary" ValidationGroup="UpdateInfo" runat="server" />
            </div>

        </div>
        <div class="panel-footer">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" ValidationGroup="UpdateInfo" />
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
