using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form To Add Opening Credit
/// </summary>
public partial class Forms_frmOpeingCredit : System.Web.UI.Page
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
            txtFromdate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            this.LoadDistributor();
            this.LoadArea();
            this.LoadData();
            this.LoadOpeningCredit();

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
    /// Loads Markets To Market Combo
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            //DistributorAreaController mController = new DistributorAreaController();
          //  DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
          //  clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);

            DrpRoute.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 1);

        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadData()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
         //  DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
         //   clsWebFormUtil.FillListBox(this.ListCustomer, dt, "CUSTOMER_NAME", "CUSTOMER_NAME", true);
         //   this.Session.Add("dt", dt);


            DataTable dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpRoute.SelectedValue.ToString()));
            clsWebFormUtil.FillListBox(this.ListCustomer, dt, 17, 17, true);
            this.Session.Add("dt", dt);
           
        }
    }
    
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtOutletCode.Text = "";
        txtAmount.Text = "";
        txtOutletName.Text = "";
        txtRemarks.Text = "";
        btnSave.Text = "Save";
        txtOutletCode.Focus();
    }
         
    /// <summary>
    /// Loads Opening Credits Detail To Grid
    /// </summary>
    private void LoadOpeningCredit()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController cdc = new CustomerDataController();
            DataTable dt = cdc.SelectOpeningCredit(int.Parse(drpDistributor.SelectedValue.ToString()),DateTime.Parse(txtFromdate.Text));
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
        this.LoadArea();
        this.LoadData();
        this.LoadOpeningCredit();
        btnSave.Text = "Save";
    }

    /// <summary>
    /// Loads Customers To Customer ListBox, Deliveryment To Deliveryman Combo And Opening Credits Detail To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
       this.LoadData();
    }

    /// <summary>
    /// Checks Invoice No in System
    /// </summary>
    /// <param name="p_Type">Type</param>
    /// <returns>True</returns>
    private bool IsBillBookNoExist(int p_Type)
    {
        bool flag = false;
        if (txtChequeNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(Convert.ToInt32(drpDistributor.SelectedValue), txtChequeNo.Text, p_Type);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
        }

        return flag;
    }

    /// <summary>
    /// Sets Opening Credit Data For Edit. This Function Runs When An Existing Opening Credit Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DrpCreditType.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;        
        txtFromdate.Text = Convert.ToDateTime(GrdOrder.Rows[e.NewEditIndex].Cells[9].Text).ToString("dd-MMM-yyyy");
        txtOutletCode.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        txtOutletName.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtChequeNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
        btnSave.Text = "Update";

        hfLegendID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;
        hfPrincipalID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;
        hfCustomerID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        hfSaleInvoiceID.Value = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;

    }

    /// <summary>
    /// Deletes Opening Credit Record
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hfLegendID.Value = GrdOrder.Rows[e.RowIndex ].Cells[7].Text;
        hfPrincipalID.Value = GrdOrder.Rows[e.RowIndex].Cells[1].Text;
        hfCustomerID.Value = GrdOrder.Rows[e.RowIndex].Cells[0].Text;
        hfSaleInvoiceID.Value = GrdOrder.Rows[e.RowIndex].Cells[10].Text;

        LedgerController LedgerCtl = new LedgerController();
        if (LedgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPrincipalID.Value), Convert.ToInt32(hfLegendID.Value), Convert.ToDateTime(txtFromdate.Text), Convert.ToInt32(hfCustomerID.Value), Convert.ToInt64(hfSaleInvoiceID.Value), Convert.ToInt32(this.Session["UserId"]),0))
        {
            this.ClearAll();
            this.LoadOpeningCredit();
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
        if (btnSave.Text == "Update")
        {
            LedgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(hfPrincipalID.Value), Convert.ToInt32(hfLegendID.Value), Convert.ToDateTime(txtFromdate.Text), Convert.ToInt32(hfCustomerID.Value),Convert.ToInt64(hfSaleInvoiceID.Value), Convert.ToInt32(this.Session["UserId"]),0);
        }

        if (!IsBillBookNoExist(1))
        {
            DataTable dtCustomer = (DataTable)this.Session["dt"];
            DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + txtOutletCode.Text.Trim() + "'");
            if (foundRows.Length > 0)
            {
                LedgerCtl.OpeningCredit(int.Parse(drpDistributor.SelectedValue.ToString()), txtChequeNo.Text.ToUpper(), int.Parse(foundRows[0]["TOWN_ID"].ToString()), int.Parse(DrpRoute.SelectedValue.ToString()),
                DateTime.Parse(txtFromdate.Text), 0, long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()), long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()),
                -1, 0, decimal.Parse(txtAmount.Text), int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpCreditType.SelectedValue.ToString()),"");
                ClearAll();
                LoadOpeningCredit();
            }
            ScriptManager.GetCurrent(Page).SetFocus(txtOutletCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Invoice No already exist,Kindly enter different Invoice No');", true);
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
}
