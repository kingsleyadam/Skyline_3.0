<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="forgot_password.aspx.cs" Inherits="Skyline_3._0.user.forgot_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlForgotPassword" runat="server" CssClass="panel panel-default no-margin">
        <asp:Panel ID="pnlHeading" runat="server" CssClass="panel-heading">
            <asp:Label ID="lblForgotPasswordHeading" runat="server" Text="Forgot Password" />
        </asp:Panel>

        <asp:Panel ID="lblForgotPasswordBody" runat="server" CssClass="panel-body">
            <div class="form-group">
                <asp:Label ID="lblStep1" runat="server" Text="Step 1"></asp:Label>
                <p class="help-block">Select whether you want to reset your password via your email address or username.</p>
                <div class="dropdown">
                    <asp:LinkButton ID="btnSelectMethod" runat="server" CssClass="btn btn-default dropdown-toggle" data-toggle="dropdown">
                        Select Reset Method (Select One) <span class="caret"></span>
                    </asp:LinkButton>
                    <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                        <li role="presentation">
                            <asp:LinkButton ID="btnDropDownUsername" runat="server" TabIndex="-1" role="menuitem" CommandName="Method" CommandArgument="UserName" OnClick="btnDropDownMethod_Click">UserName</asp:LinkButton>
                        </li>
                        <li role="presentation">
                            <asp:LinkButton ID="btndropDownEmail" runat="server" TabIndex="-1" role="menuitem" CommandName="Method" CommandArgument="Email" OnClick="btnDropDownMethod_Click">Email Address</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>

            <asp:Panel ID="pnlEmailStep2" runat="server" Visible="false">
                <div class="form-group">
                    <asp:Label ID="lblEmailStep2" runat="server" Text="Step 2"></asp:Label>
                    <p class="help-block">Please fill in your email address in the text box below and click "Reset Password."</p>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Insert Email" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="Email Address is required" ValidationGroup="EmailResetPassword" Display="None" ControlToValidate="txtEmail" />
                </div>
                <div>
                    <asp:ValidationSummary ID="valSummEmail" CssClass="valSummary" ValidationGroup="EmailResetPassword" runat="server" />
                </div>
                <asp:Panel ID="pnlNoEmailFound" runat="server" CssClass="valSummary" Visible="false">
                    <ul>
                        <li>
                            <asp:Label ID="lblNoEmailFound" runat="server" />
                        </li>
                    </ul>
                </asp:Panel>

            </asp:Panel>

            <asp:Panel ID="pnlUsernameStep2" runat="server" CssClass="form-group" Visible="false" DefaultButton="btnUserNameNextStep">
                <asp:Label ID="lblUsernameStep2" runat="server" Text="Step 2"></asp:Label>
                <p class="help-block">Insert your username so we can find your password reset information.</p>
                <div class="form-group">
                    <div class="input-group input-group-small">
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Insert UserName" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqUserName" runat="server" CssClass="input-group-addon valerror" ErrorMessage="UserName Required" Display="Dynamic" ValidationGroup="Step2UserName" ControlToValidate="txtUserName">Required</asp:RequiredFieldValidator>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnUserNameNextStep" runat="server" CssClass="btn btn-default" ValidationGroup="Step2UserName" OnClick="btnUserNameNextStep_Click">Next</asp:LinkButton>
                        </span>
                    </div>
                </div>

                <asp:Panel ID="pnlNoUserNameFound" runat="server" CssClass="valSummary" Visible="false">
                    <ul>
                        <li>
                            <asp:Label ID="lblNoUserNameFound" runat="server" />
                        </li>
                    </ul>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlUsernameStep3" runat="server" Visible="false">
                <div class="form-group">
                    <asp:Label ID="lblUserNameStep3" runat="server" Text="Step 3"></asp:Label>
                    <p class="help-block">Please fill in the information below and click "Reset Password."</p>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-addon">
                            <asp:Label ID="lblSecurityQuestion" runat="server" Text="Please Answer: "></asp:Label>
                        </div>
                        <asp:TextBox ID="txtSecurityAnswer" CssClass="form-control" runat="server" placeholder="Security Answer"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqSecurityAnswer" runat="server" ErrorMessage="Security Answer required in order to reset your password" Display="None" ValidationGroup="UserNameResetPassword" ControlToValidate="txtSecurityAnswer" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-addon">
                            <asp:Label ID="lblNewPassword" runat="server" Text="New Password: "></asp:Label>
                        </div>
                        <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server" placeholder="Password must be 7-12 nonblank characters." TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNewPassword" runat="server" ErrorMessage="New Password is required" Display="None" ValidationGroup="UserNameResetPassword" ControlToValidate="txtNewPassword" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-addon">
                            <asp:Label ID="lblConfirmNewPassword" runat="server" Text="Confirm New Password: "></asp:Label>
                        </div>
                        <asp:TextBox ID="txtConfirmNewPassword" CssClass="form-control" runat="server" placeholder="Password must be 7-12 nonblank characters." TextMode="Password"></asp:TextBox>
                        <asp:CustomValidator ID="CompareValidator" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="New Password text must match the Confirm New Password text." ClientValidationFunction="ComparePwd" ValidateEmptyText="true" Display="None" ValidationGroup="UserNameResetPassword"></asp:CustomValidator>
                    </div>
                </div>
                <div>
                    <asp:ValidationSummary ID="valSummUsername" CssClass="valSummary" ValidationGroup="UserNameResetPassword" runat="server" />
                </div>
                <asp:Panel ID="pnlUsernameFailureToReset" runat="server" CssClass="valSummary" Visible="false">
                    <ul>
                        <li>
                            <asp:Label ID="lblUsernameFailureToReset" runat="server" />
                        </li>
                    </ul>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnlForgotPasswordFooterEmail" runat="server" CssClass="panel-footer" Visible="false">
            <asp:LinkButton ID="btnEmailResetPwd" runat="server" CssClass="btn btn-primary" OnClick="btnEmailResetPwd_Click" ValidationGroup="EmailResetPassword">Reset Password</asp:LinkButton>
        </asp:Panel>

        <asp:Panel ID="pnlForgotPasswordFooterUserName" runat="server" CssClass="panel-footer" Visible="false">
            <asp:LinkButton ID="btnUserNameResetPwd" runat="server" CssClass="btn btn-primary" ValidationGroup="UserNameResetPassword" OnClick="btnUserNameResetPwd_Click">Reset Password</asp:LinkButton>
        </asp:Panel>
    </asp:Panel>


    <asp:Panel ID="pnlForgotPasswordSuccess" runat="server" CssClass="panel panel-success no-margin" Visible="false">
        <asp:Panel ID="pnlForgotPasswordSuccessHeading" runat="server" CssClass="panel-heading">
            <asp:Label ID="lblSuccessHeader" runat="server" Text="Success!"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlForgotPasswordSuccessBody" runat="server" CssClass="panel-body">
            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlForgotPasswordFailure" runat="server" CssClass="panel panel-danger no-margin" Visible="false">
        <asp:Panel ID="pnlForgotPasswordFailuresHeading" runat="server" CssClass="panel-heading">
            <asp:Label ID="lblFailureHeader" runat="server" Text="Reset Password Failed"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlForgotPasswordFailureBody" runat="server" CssClass="panel-body">
            <asp:Label ID="lblFailure" runat="server"></asp:Label>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type="text/javascript">
        function ComparePwd(oSrc, args) {
            var v = document.getElementById('<%=txtNewPassword.ClientID %>').value;

            if (args.Value.length > 0) {
                if (v == args.Value) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else {
                args.IsValid = false;
            }
        }
    </script>
</asp:Content>
