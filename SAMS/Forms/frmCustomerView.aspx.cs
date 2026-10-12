using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

public partial class Forms_frmCustomerView : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session.Remove("CustomerID");

            ltrlNew.Text = "<a style='text-decoration:none' href='frmDistributorCustomer.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2'><img src='../images/addNew.png'  height='20' width='20' alt='' /> Add Customer</a>";
           
            LoadCustomer();
        }
    }
    

    private void LoadCustomer()
    {
        CustomerDataController _customerCtrl = new CustomerDataController();

        gvCustomer.DataSource = null;
        gvCustomer.DataBind();

            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer(int.Parse(Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);

            if (dtCustomer != null)
            {
                Session.Add("dtCustomer", dtCustomer);
                gvCustomer.DataSource = dtCustomer;
                gvCustomer.DataBind();

            }
        
    }

    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvCustomer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Add("CustomerID", gvCustomer.Rows[e.NewEditIndex].Cells[0].Text);

        Response.Redirect("frmDistributorCustomer.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&DID="+gvCustomer.Rows[e.NewEditIndex].Cells[1].Text+ "&TopID=2");
    }

    protected void gvCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VenderEntryController VendorCtl = new VenderEntryController();

        int CustomerID = int.Parse(gvCustomer.Rows[e.RowIndex].Cells[0].Text);
        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtCustomer"];

        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSearch.Text + "%'";
                break;
            case 2:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSearch.Text + "%'";
                break;
            case 3:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSearch.Text + "%'";
                break;
            default:
                dt.DefaultView.RowFilter = "CUSTOMER_NAME" + " like '%" + "" + "%'";
                break;
        }
        gvCustomer.DataSource = dt.DefaultView;
        gvCustomer.DataBind();

    }

}