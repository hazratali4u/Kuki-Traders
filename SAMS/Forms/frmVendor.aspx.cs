using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmVendor : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session.Remove("VendorID");
            ltrlNew.Text = "<a href='frmVendorAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2'>Add Vendor</a>";
            LoadVendor();
        }
    }

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadVendor()
    {
        VenderEntryController VendorCtl = new VenderEntryController();
        DataTable dtVendor = VendorCtl.GetVendor(Constants.IntNullValue);

        if (dtVendor != null)
        {
            Session.Add("dtVendor", dtVendor);
            gvVendor.DataSource = dtVendor;
            gvVendor.DataBind();
            if (dtVendor.Rows.Count > 0)
            {
                lblCount.Text = dtVendor.Rows.Count.ToString();
            }

        }
    }

    /// <summary>
    /// Sets Customer Data For Edit. This Function Runs When An Existing Customer Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvVendor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Add("VendorID", gvVendor.Rows[e.NewEditIndex].Cells[0].Text);
        Response.Redirect("frmVendorAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&TopID=2");
    }

    protected void gvVendor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VenderEntryController VendorCtl = new VenderEntryController();

        int VendorID = int.Parse(gvVendor.Rows[e.RowIndex].Cells[0].Text);
        if (VendorCtl.UpdateVendor(VendorID, null, null, null, null, null, null, Constants.IntNullValue, null, Constants.DateNullValue, null, null, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,false, Constants.IntNullValue, false))
        {
            this.LoadVendor();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtVendor"];

        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                dt.DefaultView.RowFilter = "VENDOR_NAME" + " like '%" + "" + "%'";
                break;
        }
        gvVendor.DataSource = dt.DefaultView;
        gvVendor.DataBind();

        lblCount.Text = dt.DefaultView.Count.ToString();

        SetTableSorter();
    }

    private void SetTableSorter()
    {
        if (gvVendor.Rows.Count > 1)
        {
            gvVendor.UseAccessibleHeader = true;
            gvVendor.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}