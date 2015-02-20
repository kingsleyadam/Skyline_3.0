<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Skyline_3._0.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlLogin" CssClass="" role="dialog" runat="server">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:LoginView ID="lgnView" runat="server">
                    <LoggedInTemplate>
                        <div class="modal-header">
                            <h4>Logged In</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblLoggedInText" runat="server" Text="You are already logged into the site. Please return to the"></asp:Label>
                            <asp:HyperLink ID="lnkDefault" runat="server" NavigateUrl="~/default.aspx">home page</asp:HyperLink><asp:Label ID="lblHomePage" runat="server" Text="."></asp:Label>
                        </div>
                    </LoggedInTemplate>
                    <AnonymousTemplate>
                        <asp:Login ID="lgnForm" runat="server" RenderOuterTable="False" OnLoginError="lgnForm_LoginError" OnPreRender="lgnForm_PreRender">
                            <LayoutTemplate>
                                <div class="modal-header">
                                    <h4>Login</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group has-feedback">
                                        <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="Username"></asp:Label>
                                        <asp:TextBox ID="UserName" CssClass="textbox-top form-control" placeholder="Username" runat="server" />
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="None" ControlToValidate="UserName" ErrorMessage="UserName is required." ToolTip="User Name is required." ValidationGroup="ctl00$lgnForm" Text="Required" ForeColor="Red" />
                                    </div>
                                    <div class="form-group has-feedback">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="Password"></asp:Label>
                                        <asp:TextBox ID="Password" CssClass=" textbox-bottom form-control" placeholder="Password" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="None" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$lgnForm" Text="Required" ForeColor="Red" />
                                    </div>
                                    <div>
                                        <asp:ValidationSummary ID="valSummary" CssClass="valSummary" ValidationGroup="ctl00$lgnForm" runat="server" />
                                    </div>
                                    <asp:Panel ID="pnlFailureText" runat="server" class="valPanel" Visible="false">
                                        <asp:Label ID="FailureText" runat="server" EnableViewState="False"></asp:Label>
                                    </asp:Panel>

                                    <div class="row">
                                        <div class="col-md-9">
                                            <asp:CheckBox ID="RememberMe" CssClass="checkbox-inline" runat="server" Text="Remember Me?" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HyperLink ID="lnkForgotPwd" runat="server" NavigateUrl="~/user/forgot_password.aspx">Forgot Password?</asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                                <input type="text" name="loginSource" id="loginSource" value="" class="hidden" />
                                <div class="modal-footer">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Login" ValidationGroup="ctl00$lgnForm" CssClass="btn btn-primary" />
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                    </AnonymousTemplate>
                </asp:LoginView>


            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
