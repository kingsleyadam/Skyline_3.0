<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Skyline_3._0.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlLogin" CssClass="" role="dialog" runat="server">
        <div class="form-signin">
            <asp:LoginView ID="lgnView" runat="server">
                <LoggedInTemplate>
                    <h2 class="form-signin-heading line-break">Logged In</h2>
                    <asp:Label ID="lblLoggedInText" runat="server" Text="You are already logged into the site. Please return to the"></asp:Label>
                    <asp:HyperLink ID="lnkDefault" runat="server" NavigateUrl="~/default.aspx">home page</asp:HyperLink><asp:Label ID="lblHomePage" runat="server" Text="."></asp:Label>
                </LoggedInTemplate>
                <AnonymousTemplate>
                    <asp:Login ID="lgnForm" runat="server" RenderOuterTable="False" OnLoginError="lgnForm_LoginError" OnPreRender="lgnForm_PreRender">
                        <LayoutTemplate>
                            <h2 class="form-signin-heading line-break">Login</h2>
                            <div class="form-group line-break">
                                <div class="login-controls" role="group">
                                    <h4 class="no-margin hidden"><asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="Username"></asp:Label></h4>
                                    <asp:TextBox ID="UserName" CssClass="textbox-top form-control input-lg" placeholder="Username" runat="server" type="username" />
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="None" ControlToValidate="UserName" ErrorMessage="UserName is required." ToolTip="User Name is required." ValidationGroup="ctl00$lgnForm" Text="Required" ForeColor="Red" />
                                    <h4 class="no-margin hidden"><asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="Password"></asp:Label></h4>
                                    <asp:TextBox ID="Password" CssClass=" textbox-bottom form-control input-lg" placeholder="Password" TextMode="Password" runat="server" />
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="None" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctl00$lgnForm" Text="Required" ForeColor="Red" />
                                </div>
                                <div>
                                    <asp:ValidationSummary ID="valSummary" CssClass="valSummary" ValidationGroup="ctl00$lgnForm" runat="server" />
                                </div>
                                <asp:Panel ID="pnlFailureText" runat="server" class="valPanel" Visible="false">
                                    <asp:Label ID="FailureText" runat="server" EnableViewState="False"></asp:Label>
                                </asp:Panel>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-xs-6 form-group text-left">
                                    <asp:CheckBox ID="RememberMe" CssClass="checkbox-inline" runat="server" Text="Remember Me?" />
                                </div>
                                <div class="col-md-6 col-xs-6 form-group text-right">
                                    <asp:HyperLink ID="lnkForgotPwd" runat="server" NavigateUrl="~/user/forgot_password.aspx">Forgot Password?</asp:HyperLink>
                                </div>
                            </div>

                            <input type="text" name="loginSource" id="loginSource" value="" class="hidden" />
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Login" ValidationGroup="ctl00$lgnForm" CssClass="btn btn-primary" />
                        </LayoutTemplate>
                    </asp:Login>
                </AnonymousTemplate>
            </asp:LoginView>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
