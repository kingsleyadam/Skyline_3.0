using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class SendEmail
    {
        public SendEmail()
        {

        }

        public void SendAdminUsersEmail(string subject, string body, string connectionString, out string errorMessage)
        {
            DataSet ds = GetAdminUsers(connectionString);
            errorMessage = "";

            if (ds.Tables["Users"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["Users"].Rows)
                {
                    string toAddress = dr["Email"].ToString();
                    if (toAddress.Length > 0)
                        errorMessage = SendIt(toAddress, subject, body, true, "Skyline Administration");
                }
            }
        }

        public string ResetPasswordEmail(string userName, string emailAddress, string newPassword)
        {
            string subject = "Password has been reset at SkylineBigRedDistributing.com";
            string body = "Hello, <br /><br />As requested, your password at SkylineBigRedDistributing.com has been reset. Please find below your new username and password combination.<br /><br />Username: " + userName + "<br />Password:" + newPassword +
                                        "<br/><br/>You can login and click <a href='http://hhwskylinedist.com/user/change_password.aspx'>Change Password</a> to update your new password." +
                                        "<br/><br/>If you did not request your password to be changed please visit our <a href='http://hhwskylinedist.com/user/forgot_password.aspx'>Forgot Password</a> page to reset your password properly.";
            string systemMessage = SendIt(emailAddress, subject, body, true, "Reset Password - Skyline");

            return systemMessage;
        }

        private string SendIt(string toAddr, string subject, string body, bool isHTML, string fromDisplayName)
        {
            string systemMessage = "";
            //Get Config Values
            string fromEmailAddress = "donotreply@hhwskylinedist.com";
            string fromEmailPassword = "Augjak6!";
            string smtpHost = "mail.hhwskylinedist.com";
            int smtpPort = 25;
            //Set Mail Settings
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            MailAddress from = new MailAddress(fromEmailAddress, fromDisplayName);
            MailAddress toMail = new MailAddress(toAddr);
            MailMessage message = new MailMessage();
            message.From = from;
            message.To.Add(toMail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHTML;
            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                systemMessage = ex.Message + ex.StackTrace + "<br />" + ex.InnerException;
            }
            catch (Exception ex)
            {
                systemMessage = ex.Message + ex.StackTrace + "<br />" + ex.InnerException;
            }
            return systemMessage;
        }

        public static DataSet GetAdminUsers(string ConnectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);

            using (SqlCommand cmd = new SqlCommand("admGetUsersEmailNotification", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 200);
                paramRoleName.Direction = ParameterDirection.Input;

                paramRoleName.Value = "Admin";

                cmd.Parameters.Add(paramRoleName);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("Users");
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }
    }
}
