using System;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToInt32(Session["UserID"]) > 0 && Page.Request.UrlReferrer != null)
            {
                Response.Redirect("Forms/Home.aspx");
            }

            else {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
        }
    }
}