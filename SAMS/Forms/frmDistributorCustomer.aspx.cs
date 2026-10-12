using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From To Add, Edit Customer
/// </summary>
public partial class Forms_frmDistributorCustomer : System.Web.UI.Page
{
    readonly DataControl dc = new DataControl();
    readonly GeoHierarchyController GControler = new GeoHierarchyController();
    CustomerDataController _customerCtrl = new CustomerDataController();
    static int ContactId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadChannelType();
            LoadVolumClass();
           
            LoadTown();
            LoadDesignation();
            LoadPriceGroup();
            if (Request.QueryString["mode"] != null)
            {

                LoadBasicInformation();

                tbContact.Visible = true;
                LoadContactInformation();
                
            }
            btnSave.Attributes.Add("onclick", "return ValidateForm()");

          
            
        }
    }

    #region Load
    private void LoadPriceGroup()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectGroup();
        drpPriceGroup.Items.Add(new clsListItems("Default", "0"));
        clsWebFormUtil.FillDropDownList(drpPriceGroup, dt, 0, 1, false);
    }
    private void LoadBasicInformation()
    {
        DataTable dtCustomer = _customerCtrl.GetCustomerByID(Convert.ToInt32(Request.QueryString["DID"]), Convert.ToInt64(Session["CustomerID"]), 1);

        DrpDistributor.Enabled = false;
        DrpDistributor.SelectedValue = dtCustomer.Rows[0]["DISTRIBUTOR_ID"].ToString();
        txtCustomerName.Text = dtCustomer.Rows[0]["CUSTOMER_NAME"].ToString();
        txtPhoneNo.Text = dtCustomer.Rows[0]["CONTACT_NO"].ToString();
        txtEmailAddress.Text = dtCustomer.Rows[0]["EMAIL_ADDRESS"].ToString();
        txtAddress.Text = dtCustomer.Rows[0]["ADDRESS"].ToString();
        txtGstNo.Text = dtCustomer.Rows[0]["GST_NUMBER"].ToString();   
        chkIsActive.Checked = bool.Parse(dtCustomer.Rows[0]["IS_ACTIVE"].ToString());
      
        // txtRegdate.Text = dtCustomer.Rows[0]["REG_DATE"].ToString();
        txtFAX.Text = dtCustomer.Rows[0]["FAX_NO"].ToString();
        txtNTN.Text = dtCustomer.Rows[0]["NTN"].ToString();
        txtAllowedDays.Text = dtCustomer.Rows[0]["ALLOWED_DAYS"].ToString();
        txtCreditLimit.Text = dtCustomer.Rows[0]["CREDIT_LIMIT"].ToString();
       // DrpCountry.SelectedValue = dtCustomer.Rows[0]["CountryId"].ToString();
        txtTaxRate.Text = dtCustomer.Rows[0]["TAX_RATE"].ToString();
        LoadTown();
        DrpTown.SelectedValue = dtCustomer.Rows[0]["TownId"].ToString();
        DrpChannelType.SelectedValue = dtCustomer.Rows[0]["channelType"].ToString();

        DrpVolumeClass.SelectedValue = dtCustomer.Rows[0]["volumeId"].ToString();

        txtDiscount.Text = dtCustomer.Rows[0]["discount"].ToString();
        txtITaxDeduction.Text = dtCustomer.Rows[0]["INCOME_TAX"].ToString().Replace("&nbsp;","");
        txtWHTaxDeduction.Text = dtCustomer.Rows[0]["WH_TAX"].ToString().Replace("&nbsp;", "");
        drpPriceGroup.SelectedValue = dtCustomer.Rows[0]["priceGroupId"].ToString();

        btnSave.Text = "Update";

    }
    private void LoadDistributor()
    {
        DistributorController _customerCtrl = new DistributorController();
        DataTable dt = _customerCtrl.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

        DataTable dt2 = dt.Clone();

        DataRow[] foundRows = dt.Select("SUBZONE_ID <> 2");
        if (foundRows.Length > 0)
        {
            foreach (DataRow t in foundRows)
            {
                dt2.ImportRow(t);
            }
        }
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt2, 0, 2, true);
    }

    
    private void LoadTown()
    {
        DrpTown.Items.Clear();

            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpTown, dt, 0, 4, true);
        
           
        
    }
    
    private void LoadChannelType()
    {
        SLASHCodesController _customerCtrl = new SLASHCodesController();
        DataTable dt = _customerCtrl.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpChannelType, dt, 0, 2, true);
    }
    private void LoadVolumClass()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerVolumeClassType, null, Constants.IntNullValue, bool.Parse("True"));

        clsWebFormUtil.FillDropDownList(DrpVolumeClass, dt, 0, 2, true);
    }
    private void LoadDesignation()
    {
        SLASHCodesController _customerCtrl = new SLASHCodesController();
        DataTable dt = _customerCtrl.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Designation_Id, null, Constants.IntNullValue, bool.Parse("True"));
        clsWebFormUtil.FillDropDownList(DrpDesignation, dt, 0, 2, true);
    }
    
    #endregion

    #region Sel/Index 

    

    #endregion

    #region Click Operations

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            

            CustomerDataController _customerCtrl = new CustomerDataController();
            string result = null;
            if (btnSave.Text == "Save")
            {
                SETTINGS_TABLE_Controller mSettingsTableControl = new SETTINGS_TABLE_Controller();
                DataTable dtSettingsTable = mSettingsTableControl.Select_SETTINGS_TABLE("CUSTOMER", "CUSTOMER_ID",int.Parse(DrpDistributor.SelectedValue));

                if (dtSettingsTable.Rows.Count > 0)
                {
                    long CustomerID = long.Parse(dtSettingsTable.Rows[0]["Value"].ToString()) + 1;
                    string StrCode = "";


                    if (CustomerID.ToString().Length == 1)
                    {
                        StrCode = "0000" + CustomerID.ToString();
                    }
                    else if (CustomerID.ToString().Length == 2)
                    {
                        StrCode = "000" + CustomerID.ToString();
                    }
                    else if (CustomerID.ToString().Length == 3)
                    {
                        StrCode = "00" + CustomerID.ToString();
                    }
                    else if (CustomerID.ToString().Length == 4)
                    {
                        StrCode = "0" + CustomerID.ToString();
                    }
                   
                    result = _customerCtrl.InsertCustomer(CustomerID, false, chkIsActive.Checked, Constants.IntNullValue, int.Parse(DrpTown.SelectedValue), int.Parse(DrpDistributor.SelectedValue), txtGstNo.Text, txtContactPerson.Text
                       , txtPhoneNo.Text,txtEmailAddress.Text, StrCode, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue
                       , decimal.Parse(dc.chkNull_0(txtTaxRate.Text)), txtFAX.Text, txtNTN.Text, Convert.ToInt32(dc.chkNull_0(txtAllowedDays.Text)), Convert.ToDecimal(dc.chkNull_0(txtCreditLimit.Text))
                       , int.Parse(DrpChannelType.SelectedValue), int.Parse(DrpVolumeClass.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtITaxDeduction.Text)), Convert.ToDecimal(dc.chkNull_0(txtWHTaxDeduction.Text)), Convert.ToInt32(drpPriceGroup.SelectedValue));

                     Session.Add("CustomerID", CustomerID);

                     tbContact.Visible = true;
                }
            }
            else
            {
                result = _customerCtrl.UpdateCustomer(long.Parse(Session["CustomerID"].ToString()), false, chkIsActive.Checked, Constants.IntNullValue, int.Parse(DrpTown.SelectedValue), int.Parse(DrpDistributor.SelectedValue), txtGstNo.Text, txtContactPerson.Text,
                txtPhoneNo.Text, txtEmailAddress.Text, decimal.Parse(dc.chkNull_0(txtTaxRate.Text)), null, txtCustomerName.Text, txtAddress.Text, Constants.DateNullValue
                , txtFAX.Text, txtNTN.Text, Convert.ToInt32(dc.chkNull_0(txtAllowedDays.Text)), Convert.ToDecimal(dc.chkNull_0(txtCreditLimit.Text))
                , int.Parse(DrpChannelType.SelectedValue), int.Parse(DrpVolumeClass.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtITaxDeduction.Text)), Convert.ToDecimal(dc.chkNull_0(txtWHTaxDeduction.Text)), Convert.ToInt32(drpPriceGroup.SelectedValue));
           
                Response.Redirect("frmCustomerView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=null&TopID=2");
           
            }
            if (result != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Update');", true);
               
                ClearAll();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mode"] != null)
        {
            Response.Redirect("frmCustomerView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&TopID=2");
        }
        else
        {
            ClearAll();
            Response.Redirect("frmDistributorCustomer.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2");
        }
       
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    LoadContactInformation();
    //    SetTableSorter();
    //}
    
    #endregion

   
    private void ClearAll()
    {
        txtCustomerName.Text = "";
     
        txtAddress.Text = "";
        txtPhoneNo.Text = "";
        txtEmailAddress.Text = "";
        txtNTN.Text = string.Empty;
        txtFAX.Text = string.Empty;
        txtTaxRate.Text = "";
        txtGstNo.Text = "";
        txtAllowedDays.Text = "";
        txtCreditLimit.Text = "";
        txtWHTaxDeduction.Text = "";
        txtITaxDeduction.Text = "";
        txtDiscount.Text = "";
        btnSave.Text = "Save";
        

    }

    #region Customer Contact

    private void LoadContactInformation()
    {
        Grid_users.DataSource = null;
        Grid_users.DataBind();

            DataTable dtContact = _customerCtrl.GetCustomerContact(int.Parse(DrpDistributor.SelectedValue), Convert.ToInt64(Session["CustomerID"]), Convert.ToInt32(Constants.IntNullValue));
            if (dtContact.Rows.Count > 0)
            {
                Grid_users.DataSource = dtContact;
                Grid_users.DataBind();
            }
        
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ContactId=Convert.ToInt32(Grid_users.Rows[e.NewEditIndex].Cells[0].Text);
        
        DrpDesignation.SelectedValue = Grid_users.Rows[e.NewEditIndex].Cells[1].Text;
        txtContactPerson.Text = Grid_users.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        txtContactNo.Text = Grid_users.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtContactEmail.Text = Grid_users.Rows[e.NewEditIndex].Cells[5].Text.Replace("&nbsp;","");
        chkIsActiveContact.Checked =Convert.ToBoolean(Grid_users.Rows[e.NewEditIndex].Cells[6].Text);

        btnSaveContact.Text = "Update";
    }

    protected void btnSaveContact_Click(object sender, EventArgs e)
    {
        try
        {
            string result = null;

            if (btnSaveContact.Text == "Save")
            {
                result = _customerCtrl.InsertCustomerContact(long.Parse(Session["CustomerID"].ToString()), chkIsActiveContact.Checked, int.Parse(DrpDistributor.SelectedValue), txtContactPerson.Text
                       , txtContactNo.Text, txtContactEmail.Text, int.Parse(DrpDesignation.SelectedValue));

            }
            else
            {
                result = _customerCtrl.UpdateCustomerContact(ContactId, chkIsActiveContact.Checked, int.Parse(DrpDistributor.SelectedValue), txtContactPerson.Text
                , txtContactNo.Text, txtContactEmail.Text, int.Parse(DrpDesignation.SelectedValue));

               
            }
            if (result != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + result + "');", true);

                ClearContact();
                LoadContactInformation();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred');", true);
        }
    }

    private void ClearContact()
    
    {
        txtContactPerson.Text = "";
        txtContactEmail.Text = "";
        txtContactNo.Text = "";
      //  txtSeach.Text = "";
        chkIsActiveContact.Checked = true;

        btnSaveContact.Text = "Save";

    }

    #endregion
}
