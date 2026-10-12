using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form To Add Opening Credit
/// </summary>
public partial class Forms_frmVendorOpening : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos, ListBox And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadVendorOpening();

        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
            
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtAmount.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Save";
    }

    /// <summary>
    /// Loads Principals To Supplier Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
    }
      
    /// <summary>
    /// Loads Opening Credits Detail To Grid
    /// </summary>
    private void LoadVendorOpening()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController cdc = new CustomerDataController();
            DataTable dt = cdc.GetVendorOpening(int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }
        
    /// <summary>
    /// Loads Markets To Market Combo, Customers To Customer ListBox, Deliveryment To Deliveryman Combo And Opening Credits Detail To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadVendorOpening();
        btnSave.Text = "Save";
    }

    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DrpPrincipal.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;

        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;

        hfPurchaseID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        btnSave.Text = "Update";
        DrpPrincipal.Enabled = false;
        drpDistributor.Enabled = false;
    }
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LedgerCtl = new LedgerController();
        if (LedgerCtl.DeleteVendorOpening(Convert.ToInt32(GrdOrder.Rows[e.RowIndex].Cells[3].Text), Convert.ToInt32(this.Session["UserId"])))
        {
            this.ClearAll();
            this.LoadVendorOpening();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured. Opening Credit not deleted.');", true);
        }

    }

    /// <summary>
    /// Saves/Updates Opening Credit Record
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LedgerController LedgerCtl = new LedgerController();
        CustomerDataController CDC = new CustomerDataController();
        DataControl DC = new DataControl();
        if (IsDayClosed())
        {
            UserController UserCtl = new UserController();

            UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            this.Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (txtAmount.Text.Length > 0)
            {

                if (btnSave.Text == "Update")
                {
                    var ledgerCtl = new LedgerController();
                    if (ledgerCtl.UpdateVendorOpening(Convert.ToInt32(hfPurchaseID.Value),
                        Convert.ToInt32(Session["UserId"]), txtRemarks.Text, decimal.Parse(txtAmount.Text), 1))
                    {
                        ClearAll();
                        LoadVendorOpening();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                            "alert('Some error occured. Opening Credit not deleted.');", true);
                    }

                }
                else
                {
                    //TypeID = 0 for Opening Credit
                    LedgerCtl.InsertVendorOpening(int.Parse(drpDistributor.SelectedValue.ToString()), txtRemarks.Text, 0, Convert.ToDateTime(Session["CurrentWorkDate"]), int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToDecimal(DC.chkNull_0(txtAmount.Text)),
                        0, 0, false, 0, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()));
                    this.ClearAll();
                    this.LoadVendorOpening();
                }
            }
        }
    }
    
    /// <summary>
    /// Cancels Opening Credit Entry
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    private bool IsDayClosed()
    {
        bool flag = false;
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(drpDistributor.SelectedValue), 3);
        if (Convert.ToDateTime(Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
        {
            flag = false;
        }
        else
        {
            flag = true;
        }

        return flag;
    }

}
