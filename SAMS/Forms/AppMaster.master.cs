using System;
using System.Web.UI.WebControls;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.Text;

public partial class AppMaster : System.Web.UI.MasterPage
{
    MenuController MenuCtl = new MenuController();
    RoleManagementController ObjRole = new RoleManagementController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int RoleID = (int)Session["RoleID"];
            lblUser.Text = Session["UserName"].ToString();
            lblWorkingDate.Text = ((DateTime)this.Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");

            ltrlMenu.Text = GetTopMenu();
            LoadLeftMenu(RoleID);
        }
    }

    private string GetTopMenu()
    {
        DataTable dtBreadCrumb = MenuCtl.GetMenu(Constants.IntNullValue, Constants.IntNullValue, 12, (int)Session["RoleID"]);
        StringBuilder strBreadCrumb = new StringBuilder();
        if (dtBreadCrumb != null && dtBreadCrumb.Rows.Count > 0)
        {
            foreach (DataRow row in dtBreadCrumb.Rows)
            {
                if (row["FirstLink"].ToString() != "")
                {
                    strBreadCrumb.Append("<a href='" + row["FirstLink"].ToString() + "?LevelType=3&LevelID=" + row["FirstLinkID"] + "&TopID=" + row["MENU_ID"] + "'>" + row["MENU_DESCRIPTION"].ToString() + "</a>");
                }
            }
        }
        return strBreadCrumb.ToString();
    }

    private void LoadLeftMenu(int RoleID)
    {
        if (Request.QueryString["TopID"] != null)
        {
            DataTable dtParent = ObjRole.SelectRoleWiseModule(RoleID, 3, Convert.ToInt32(Request.QueryString["TopID"]));
            if (dtParent != null)
            {
                gvLeftMenu.DataSource = dtParent;
                gvLeftMenu.DataBind();
            }
        }
    }

    protected void gvLeftMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvLeftMenuSub = (GridView)e.Row.FindControl("gvLeftMenuSub");
            int ParentID = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
            DataTable dtChild = ObjRole.SelectRoleWiseModule(Convert.ToInt32(Session["RoleID"]), 4, ParentID);
            if (dtChild != null)
            {
                gvLeftMenuSub.DataSource = dtChild;
                gvLeftMenuSub.DataBind();
            }
        }
    }
}
