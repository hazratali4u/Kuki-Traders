using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmSaleForce : System.Web.UI.Page
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
            Session.Remove("EmployeeID");
            ltrlNew.Text = "<a href='frmSaleForceAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2'>Add Employee</a>";
            this.LoadEmployee();
        }
    }

    /// <summary>
    /// Loads Customers To Customer Grid
    /// </summary>
    private void LoadEmployee()
    {
        Distributor_UserController UController = new Distributor_UserController();
        DataTable dt = UController.SelectDistributorUser(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.gvEmployee.DataSource = dt;
        this.gvEmployee.DataBind();
    }

    /// <summary>
    /// Sets Customer Data For Edit. This Function Runs When An Existing Customer Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Add("EmployeeID", gvEmployee.Rows[e.NewEditIndex].Cells[0].Text);
        Response.Redirect("frmSaleForceAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&TopID=2");
    }
    
    /// <summary>
    /// Set Customer Grid For JQuery Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (gvEmployee.Rows.Count > 1)
        {
            gvEmployee.UseAccessibleHeader = true;
            gvEmployee.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}