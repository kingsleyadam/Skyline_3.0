using Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserInfo;

namespace Skyline_3._0.user
{
    public partial class forgot_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDropDownMethod_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            if (btn.CommandName == "Method")
            {
                if (btn.CommandArgument == "Email")
                {
                    btnSelectMethod.Text = "Email <span class='caret'></span>";
                    pnlEmailStep2.Visible = true;
                    pnlUsernameStep2.Visible = false;
                    pnlForgotPasswordFooterEmail.Visible = true;
                    pnlForgotPasswordFooterUserName.Visible = false;
                    pnlUsernameStep3.Visible = false;
                    Form.DefaultButton = btnEmailResetPwd.UniqueID;
                    txtEmail.Focus();
                }
                else if (btn.CommandArgument == "UserName")
                {
                    btnSelectMethod.Text = "UserName <span class='caret'></span>";
                    pnlEmailStep2.Visible = false;
                    pnlUsernameStep2.Visible = true;
                    pnlForgotPasswordFooterEmail.Visible = false;
                    pnlForgotPasswordFooterUserName.Visible = false;
                    pnlUsernameStep3.Visible = false;
                    txtUserName.Focus();

                    txtUserName.Attributes.Remove("disabled");
                    btnUserNameNextStep.Attributes.Remove("disabled");
                    btnUserNameNextStep.Enabled = true;
                    pnlNoUserNameFound.Visible = false;
                }
            }
        }

        protected void btnEmailResetPwd_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text, tempPwd = "";
            string connectionString = ConfigurationManager.ConnectionStrings["skylinebigredConnectionString"].ConnectionString;
            SkylineUser su = new SkylineUser(email, connectionString);
            MembershipProvider mp = Membership.Providers["SqlProviderAdmin"];
            
            if (su.UserName != null)
            {
                MembershipUser user = mp.GetUser(su.UserName, false);
                try
                {
                    if (user.IsLockedOut)
                    {
                        user.UnlockUser();
                        Membership.UpdateUser(user);
                    }

                    tempPwd = user.ResetPassword();

                    SendEmail se = new SendEmail();
                    string systemMessage = se.ResetPasswordEmail(user.UserName, user.Email, tempPwd);

                    if (systemMessage.Length > 0)
                    {
                        pnlForgotPassword.Visible = false;
                        pnlForgotPasswordFailure.Visible = true;
                        lblFailure.Text = "Something didn't go as planned. Please retry resetting your password again. More Details: " + systemMessage;
                    }
                    else
                    {
                        pnlForgotPassword.Visible = false;
                        pnlForgotPasswordSuccess.Visible = true;
                        lblSuccess.Text = "You've successfully reset your password. You should be getting an email shortly with your new password. Once logged in you can update your password.";
                    }
                }
                catch (MembershipPasswordException ex) 
                {
                    pnlForgotPassword.Visible = false;
                    pnlForgotPasswordFailure.Visible = true;
                    lblFailure.Text = ex.Message;
                }
            }
            else
            {
                pnlNoEmailFound.Visible = true;
                lblNoEmailFound.Text = "Sorry, we couldn't find any user in the system with that email address. Or you have multiple accounts with the same email.";
            }

        }

        protected void btnUserNameNextStep_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser(txtUserName.Text);
            ViewState.Add("UserName", txtUserName.Text);
            if (user != null)
            {
                if (Roles.IsUserInRole(user.UserName,"User")) 
                {
                    txtUserName.Attributes.Add("disabled", "");
                    pnlUsernameStep3.Visible = true;
                    pnlForgotPasswordFooterUserName.Visible = true;
                    btnUserNameNextStep.Enabled = false;
                    pnlNoUserNameFound.Visible = false;
                    btnUserNameNextStep.Attributes.Add("disabled", "");
                    Form.DefaultButton = btnUserNameResetPwd.UniqueID;
                    txtSecurityAnswer.Focus();

                    lblSecurityQuestion.Text = "Please Answer: " + user.PasswordQuestion;
                }
                else
                {
                    lblNoUserNameFound.Text = "This user cannot have its password reset.";
                    pnlNoUserNameFound.Visible = true;
                }
            }
            else
            {
                lblNoUserNameFound.Text = "Sorry, we could not find any user in the system with that username.";
                pnlNoUserNameFound.Visible = true;
            }
        }

        protected void btnUserNameResetPwd_Click(object sender, EventArgs e)
        {
            string userName = ViewState["UserName"].ToString();
            MembershipUser user = Membership.GetUser(userName);
            string tempPwd = "";

            try
            {
                if (user.IsLockedOut)
                {
                    user.UnlockUser();
                    Membership.UpdateUser(user);
                }

                tempPwd = user.ResetPassword(txtSecurityAnswer.Text);
                user.ChangePassword(tempPwd, txtNewPassword.Text);

                pnlForgotPassword.Visible = false;
                pnlForgotPasswordSuccess.Visible = true;
                lblSuccess.Text = "You've successfully updated your password. You can now log into the site with your new password";
            }
            catch (MembershipPasswordException ex)
            {
                pnlUsernameFailureToReset.Visible = true;
                lblUsernameFailureToReset.Text = ex.Message;
                txtUserName.Text = userName;
            }
            catch (Exception ex)
            {
                pnlForgotPassword.Visible = false;
                pnlForgotPasswordFailure.Visible = true;
                lblFailure.Text = ex.Message;
            }
        }
    }
}