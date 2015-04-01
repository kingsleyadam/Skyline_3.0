<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="Skyline_3._0.user.change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ChangePassword ID="changePwd" runat="server" RenderOuterTable="false" FailureTextStyle-CssClass="valPanel" OnChangePasswordError="changePwd_ChangePasswordError">
        <ChangePasswordTemplate>
            <div class="panel panel-default no-margin">
                <!-- Default panel contents -->
                <div class="panel-heading">Change Password</div>
                <div class="panel-body">
                    <p>Fill in the below form to change your current password. You'll need to provide your current password and confirm your new password twice.</p>

                    <div class="form-group">
                        <asp:Label ID="lblCurrentPwd" runat="server" Text="Current Password" AssociatedControlID="CurrentPassword"></asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="form-control" placeholder="Current Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" Display="None" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblNewPassword" runat="server" Text="New Password" AssociatedControlID="NewPassword"></asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" placeholder="New Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" Display="None" ControlToValidate="NewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblNewPasswordConfirm" runat="server" Text="Confirm New Password" AssociatedControlID="ConfirmNewPassword"></asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" placeholder="Confirm New Password" TextMode="Password"></asp:TextBox>
                        <asp:CustomValidator ID="CompareValidator" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="New Password text must match the Confirm New Password text." ClientValidationFunction="ComparePwd" ValidateEmptyText="true" Display="None" ValidationGroup="ChangePassword1"></asp:CustomValidator>
                    </div>

                    <div>
                        <asp:ValidationSummary ID="valSummary" CssClass="valSummary" ValidationGroup="ChangePassword1" runat="server" />
                    </div>

                    <asp:Panel ID="pnlFailureText" runat="server" class="valPanel" Visible="false">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    </asp:Panel>
                    
                </div>
                <div class="panel-footer">
                    <asp:Button ID="ChangePasswordPushButton" runat="server" Text="Change Password" CssClass="btn btn-primary" CommandName="ChangePassword" ValidationGroup="ChangePassword1" />
                </div>
            </div>
        </ChangePasswordTemplate>
        <SuccessTemplate>
            <div class="panel panel-success no-margin">
                <!-- Default panel contents -->
                <div class="panel-heading">Change Password</div>
                <div class="panel-body">
                    Success! You've successfully update your password. The next time you log-in you'll use the new credentials.
                </div>
            </div>
        </SuccessTemplate>
    </asp:ChangePassword>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type="text/javascript">
        function ComparePwd(oSrc, args) {
            var v = document.getElementById('<%=changePwd.Controls[0].FindControl("NewPassword").ClientID %>').value;

            if (args.Value.length > 0) {
                if (v == args.Value) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = false;
            }
        }
    </script>
</asp:Content>
