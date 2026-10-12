using System;
using System.Data;
using System.Text;
using SAMSBusinessLayer.Classes;

public partial class Forms_PageMaster : System.Web.UI.MasterPage
{
    DistributorController CtlDist = new DistributorController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BreadCum.Text = GetBreadCrumb();
        }
        else
        {
            if (Session["Title"] != null)
            {
                Page.Title = Session["Title"].ToString();
            }
        }
    }

    private string GetBreadCrumb()
    {
        StringBuilder strBreadCrumb = new StringBuilder();
        if (Request.QueryString["LevelID"] != null)
        {
            DataTable dtBreadCrumb = CtlDist.GetBreadCrumb(Convert.ToInt32(Request.QueryString["LevelID"]), Convert.ToInt32(Session["RoleID"]));

            strBreadCrumb.Append("<div>");
            if (dtBreadCrumb != null && dtBreadCrumb.Rows.Count > 0)
            {
                if (dtBreadCrumb.Rows[0]["ID2"].ToString() != "")
                {
                    if (dtBreadCrumb.Rows[0]["TYPE_ID2"].ToString() != "11")
                    {
                        if (dtBreadCrumb.Rows[0]["TYPE_ID2"].ToString() == "12")
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["FirstLink2"] + "?LevelID=" + dtBreadCrumb.Rows[0]["FirstLinkID2"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID2"] + "\">");

                        }
                        else if (dtBreadCrumb.Rows[0]["TYPE_ID2"].ToString() == "14")
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["KEY2"] + "?LevelID=" + dtBreadCrumb.Rows[0]["ID2"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID2"] + "\">");
                        }
                        else
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["FirstLink02"] + "?LevelID=" + dtBreadCrumb.Rows[0]["FirstLinkID02"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID2"] + "\">");
                        }
                        strBreadCrumb.Append(dtBreadCrumb.Rows[0]["DESCRIPTION2"].ToString());
                        Page.Title = dtBreadCrumb.Rows[0]["DESCRIPTION2"].ToString();
                        Session.Add("Title", Page.Title);
                        strBreadCrumb.Append("</a>");
                    }
                }

                if (dtBreadCrumb.Rows[0]["ID3"].ToString() != "")
                {
                    if (dtBreadCrumb.Rows[0]["TYPE_ID3"].ToString() != "11")
                    {
                        if (dtBreadCrumb.Rows[0]["TYPE_ID3"].ToString() == "12")
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["FirstLink3"] + "?LevelID=" + dtBreadCrumb.Rows[0]["FirstLinkID3"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID3"] + "\">");

                        }
                        else if (dtBreadCrumb.Rows[0]["TYPE_ID3"].ToString() == "14")
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["KEY3"] + "?LevelID=" + dtBreadCrumb.Rows[0]["ID3"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID3"] + "\">");

                        }
                        else
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["FirstLink03"] + "?LevelID=" + dtBreadCrumb.Rows[0]["FirstLinkID03"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID2"] + "\">");
                        }
                        strBreadCrumb.Append(dtBreadCrumb.Rows[0]["DESCRIPTION3"].ToString());
                        Page.Title = dtBreadCrumb.Rows[0]["DESCRIPTION3"].ToString();
                        Session.Add("Title", Page.Title);
                        strBreadCrumb.Append("</a>");
                    }
                }

                if (dtBreadCrumb.Rows[0]["ID4"].ToString() != "")
                {
                    if (dtBreadCrumb.Rows[0]["TYPE_ID4"].ToString() != "11")
                    {
                        if (dtBreadCrumb.Rows[0]["TYPE_ID4"].ToString() == "12")
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["FirstLink4"] + "?LevelID=" + dtBreadCrumb.Rows[0]["FirstLinkID4"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID4"] + "\">");

                        }
                        else if (dtBreadCrumb.Rows[0]["TYPE_ID4"].ToString() == "14")
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["KEY4"] + "?LevelID=" + dtBreadCrumb.Rows[0]["ID4"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID2"] + "\">");

                        }
                        else
                        {
                            strBreadCrumb.Append("<a href=\"" + dtBreadCrumb.Rows[0]["FirstLink04"] + "?LevelID=" + dtBreadCrumb.Rows[0]["FirstLinkID04"] + "&LevelType=3&TopID=" + dtBreadCrumb.Rows[0]["ID2"] + "\">");
                        }
                        strBreadCrumb.Append(dtBreadCrumb.Rows[0]["DESCRIPTION4"]);
                        Page.Title = dtBreadCrumb.Rows[0]["DESCRIPTION4"].ToString();
                        Session.Add("Title", Page.Title);
                        strBreadCrumb.Append("</a>");
                    }
                }

            }
            strBreadCrumb.Append("</div>");
        }
        return strBreadCrumb.ToString();
    }
}
