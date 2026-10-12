using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.IO;

/// <summary>
/// From to Add, Edit, Delete Geo Heirarical Data
/// </summary>
public partial class Forms_frmVendorAdd : System.Web.UI.Page
{    
    readonly VenderEntryController VendorCtl = new VenderEntryController();

    readonly AccountHeadController mah_Controller = new AccountHeadController();
    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetAccountHeads();
            LoadTown();
            if (Request.QueryString["mode"] != null)
            {
                
                LoadBasicInformation();
                LoadOwnerInformation();
                LoadOpeningInformation();
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;
              
            }
            btnSaveBasic.Attributes.Add("onclick", "return ValidateBasicInformation()");
        }
    }

    #region Basic Information

    protected void LoadTown()
    {
        GeoHierarchyController gController = new GeoHierarchyController();
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(Session["CompanyId"].ToString()));
        ddlTown.DataSource = dt;
        ddlTown.DataTextField = "GEO_NAME";
        ddlTown.DataValueField = "GEO_ID";
        ddlTown.DataBind();
    }

    private void GetAccountHeads()
    {

       //// DataTable dt = mah_Controller.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue, Constants.IntNullValue);
       // DataTable dt = mah_Controller.SelectAccountHeadManual(3, 20, 1);//03-01-00-0000 Creditors
       // clsWebFormUtil.FillDropDownList(drpCreditors, dt, 0, 1, true);

       // DataTable dt2 = mah_Controller.SelectAccountHeadManual(4, 25, 3);//04-02-04-0000 Sales Tax Receiveable
       // clsWebFormUtil.FillDropDownList(drpSaleTaxRble, dt2, 0, 1, true);
    }
    /// <summary>
    /// Loads Existing Regions To All Region Combos And Region Grid Of Region Tab.
    /// </summary>
    protected void LoadBasicInformation()
    {

        DataTable dtVendor = VendorCtl.GetVendor(Convert.ToInt32(Session["VendorID"]));
        if (dtVendor.Rows.Count > 0)
        {
            txtName.Text = dtVendor.Rows[0]["VENDOR_NAME"].ToString();
            txtOfficeAddress.Text = dtVendor.Rows[0]["ADDRESS1"].ToString();
            txtFactoryAddress.Text = dtVendor.Rows[0]["ADDRESS2"].ToString();
            txtRegisteredOfficeAddress.Text = dtVendor.Rows[0]["ADDRESS3"].ToString();
            txtSalesTax.Text = dtVendor.Rows[0]["GST"].ToString();
            txtIncomeTax.Text = dtVendor.Rows[0]["NTN"].ToString();
            ddlBusinessType.SelectedValue = dtVendor.Rows[0]["BUSINESS_TYPE_ID"].ToString();
          // drpCreditors.SelectedValue = dtVendor.Rows[0]["FREIGHT_HEAD_ID"].ToString();
           // drpSaleTaxRble.SelectedValue = dtVendor.Rows[0]["SaleTax_HEAD_ID"].ToString();

            txtExemption.Text = dtVendor.Rows[0]["EXEMPTION"].ToString();
            if (dtVendor.Rows[0]["EXEMPTION_DATE"].ToString() != "")
            {
                txtTaxDate.Text = Convert.ToDateTime(dtVendor.Rows[0]["EXEMPTION_DATE"]).ToString("dd-MMM-yyyy");
            }
            txtConcern.Text = dtVendor.Rows[0]["CONTACT_PERSON"].ToString();
            txtConcernMobileNo.Text = dtVendor.Rows[0]["CONTACT_NO"].ToString();
            ddlBusinessDeal.SelectedValue = dtVendor.Rows[0]["DEAL_TYPE"].ToString();
            ddlCurrency.SelectedValue = dtVendor.Rows[0]["CURRENCY_ID"].ToString();
            ddlVendorType.SelectedValue = dtVendor.Rows[0]["TYPE"].ToString();


            rdbVendorType.SelectedValue = dtVendor.Rows[0]["vendorType"].ToString();
            ddlTown.SelectedValue = dtVendor.Rows[0]["townId"].ToString();

            chkIsActive.Checked = Convert.ToBoolean(dtVendor.Rows[0]["IS_ACTIVE"]);

            lblVendorInfo.Text = txtName.Text + " - " + ddlTown.SelectedItem.Text;
            btnSaveBasic.Text = "Update";
        }
    }
    
    protected void btnSaveBasic_Click(object sender, EventArgs e)
    {
        DateTime dtExemption = Constants.DateNullValue;
        if(txtTaxDate.Text.Length > 0)
        {
            dtExemption = Convert.ToDateTime(txtTaxDate.Text);
        }
        if (btnSaveBasic.Text == "Save")
        {
            int VendorID = VendorCtl.InsertVendor(txtName.Text, txtOfficeAddress.Text, txtFactoryAddress.Text, txtRegisteredOfficeAddress.Text
                , txtIncomeTax.Text, txtSalesTax.Text, Convert.ToInt32(ddlBusinessType.SelectedValue), txtExemption.Text, dtExemption
                , txtConcern.Text, txtConcernMobileNo.Text, Convert.ToInt32(ddlBusinessDeal.SelectedValue)
                , Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(ddlVendorType.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlTown.SelectedValue), Convert.ToBoolean(rdbVendorType.SelectedValue));
            if(VendorID > 0)
            {
                Session.Add("VendorID", VendorID);
                Response.Redirect("frmVendor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&TopID=2");
            }
        }
        else
        {
            if (VendorCtl.UpdateVendor(Convert.ToInt32(Session["VendorID"]), txtName.Text, txtOfficeAddress.Text, txtFactoryAddress.Text, txtRegisteredOfficeAddress.Text
                , txtIncomeTax.Text, txtSalesTax.Text, Convert.ToInt32(ddlBusinessType.SelectedValue), txtExemption.Text, dtExemption, txtConcern.Text
                , txtConcernMobileNo.Text, Convert.ToInt32(ddlBusinessDeal.SelectedValue), Convert.ToInt32(ddlCurrency.SelectedValue)
                , Convert.ToInt32(ddlVendorType.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, chkIsActive.Checked, Convert.ToInt32(ddlTown.SelectedValue), Convert.ToBoolean(rdbVendorType.SelectedValue)))
            {

                Response.Redirect("frmVendor.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&TopID=2");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.');", true);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mode"] != null)
        {
            Response.Redirect("frmVendorAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&mode=edit&TopID=2");
        }
        else
        {
            Response.Redirect("frmVendorAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2");
        }
    }
        
    #endregion
    
    #region Owner Information

    private void LoadOwnerInformation()
    {
        DataTable dtOwner = VendorCtl.GetVendorOwner(Constants.LongNullValue, Convert.ToInt64(Session["VendorID"]));
        if (dtOwner.Rows.Count > 0)
        {
            Session.Add("OwnerID", dtOwner.Rows[0]["VENDOR_OWNER_ID"]);
            txtOwnerName.Text = dtOwner.Rows[0]["OWNER_NAME"].ToString();
            txtFatherName.Text = dtOwner.Rows[0]["FATHER_NAME"].ToString();
            txtOwnerAddress.Text = dtOwner.Rows[0]["RESIDENTIAL_ADDRESS"].ToString();
            txtOwnerCNIC.Text = dtOwner.Rows[0]["CNIC_NO"].ToString();
            if (dtOwner.Rows[0]["CNIC_EXPIRY"].ToString() != "")
            {
                txtOwnerCNICExpiry.Text = Convert.ToDateTime(dtOwner.Rows[0]["CNIC_EXPIRY"]).ToString("dd-MMM-yyyy");
            }
            txtOwnerAddress2.Text = dtOwner.Rows[0]["ADDRESS"].ToString();
            txtOwnerEmail.Text = dtOwner.Rows[0]["EMAIL_ADDRESS"].ToString();
            txtOwnerContactNo.Text = dtOwner.Rows[0]["CONTACT_NO"].ToString();
            txtOwnerMobileNo.Text = dtOwner.Rows[0]["MOBILE_NO"].ToString();            
            txtOwnerPTCL.Text = dtOwner.Rows[0]["PTCL"].ToString();
            Session["Pic"] = dtOwner.Rows[0]["PIC"].ToString();
            btnSaveOwner.Text = "Update";
        }
    }

    protected void btnSaveOwner_Click(object sender, EventArgs e)
    {
        DateTime dtExpiry = Constants.DateNullValue;
        if(txtOwnerCNICExpiry.Text.Length > 0)
        {
            dtExpiry = Convert.ToDateTime(txtOwnerCNICExpiry.Text);
        }
        string fileName = null;
        if (fuPic.HasFile)
        {
            string path = Server.MapPath("~/Pics");
            string fExtension = "";
            FileInfo oFileInfo = new FileInfo(fuPic.PostedFile.FileName);
            fExtension = Path.GetExtension(fuPic.FileName);
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "pic" + fExtension;
            string fullFileName = path + "\\" + fileName;
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fuPic.PostedFile.SaveAs(fullFileName);
        }
        if (btnSaveOwner.Text == "Save")
        {
            if (VendorCtl.InsertVendorOwner(Convert.ToInt64(Session["VendorID"]), txtOwnerName.Text, txtFatherName.Text, txtOwnerAddress.Text, txtOwnerCNIC.Text, dtExpiry, txtOwnerAddress2.Text, txtOwnerEmail.Text, txtOwnerContactNo.Text, txtOwnerMobileNo.Text, txtOwnerPTCL.Text, fileName))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Owner Added Successfully.');", true);
            }
        }
        else
        {
            if (VendorCtl.UpdateVendorOwner(Convert.ToInt64(Session["OwnerID"]), Convert.ToInt64(Session["VendorID"]), txtOwnerName.Text, txtFatherName.Text, txtOwnerAddress.Text, txtOwnerCNIC.Text, dtExpiry, txtOwnerAddress2.Text, txtOwnerEmail.Text, txtOwnerContactNo.Text, txtOwnerMobileNo.Text, txtOwnerPTCL.Text, fileName))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Owner Updated Successfully.');", true);
            }
        }
    }

    #endregion

    #region Opening Balance

    private void LoadDistributor()
    {

        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);


    }
    private void LoadOpeningInformation()
    {
        DataTable dtOpening = VendorCtl.GetVendorOpening(Constants.LongNullValue, Convert.ToInt64(Session["VendorID"]), Constants.IntNullValue);
        LoadDistributor();
        if (dtOpening.Rows.Count > 0)
        {
            Session.Add("OpeningID", dtOpening.Rows[0]["VENDOR_OPENING_ID"]);
            if (dtOpening.Rows[0]["OPENING_DATE"].ToString() != "")
            {
                txtOpeningDate.Text = Convert.ToDateTime(dtOpening.Rows[0]["OPENING_DATE"]).ToString("dd-MMM-yyyy");
            }
            txtOpeningBalance.Text = dtOpening.Rows[0]["BALANCE"].ToString();
            txtOpeningBalanceRemarks.Text = dtOpening.Rows[0]["REMARKS"].ToString();
            rblOpening.SelectedValue = dtOpening.Rows[0]["TYPE_ID"].ToString();
            
            drpDistributor.SelectedValue = dtOpening.Rows[0]["DISTRIBUTOR_ID"].ToString();
            drpDistributor.Enabled = false;

            btnSaveOpeningBalance.Text = "Update";
        }
    }

    protected void btnOpeningBalance_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        LedgerController ledgerCtl = new LedgerController();
        DateTime dtOpening = Constants.DateNullValue;
        if (txtOpeningDate.Text.Length > 0)
        {
            dtOpening = Convert.ToDateTime(txtOpeningDate.Text);
        }
        if (btnSaveOpeningBalance.Text == "Save")
        {
            if (VendorCtl.InsertVendorOpening(Convert.ToInt64(Session["VendorID"]), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(rblOpening.SelectedValue), dtOpening, Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)), txtOpeningBalanceRemarks.Text))
            {
                ledgerCtl.InsertVendorOpening(Convert.ToInt32(drpDistributor.SelectedValue), txtOpeningBalanceRemarks.Text, int.Parse(rblOpening.SelectedValue), Convert.ToDateTime(txtOpeningDate.Text), int.Parse(Session["VendorID"].ToString()), int.Parse(Session["VendorID"].ToString()), Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)),
                       0, 0, false, 0, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(Session["VendorID"].ToString()));

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Opening Added Successfully.');", true);
            }
        }
        else
        {
             CustomerDataController cdc = new CustomerDataController();
             DataTable dt = cdc.SelectOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt64(Session["VendorID"]), Constants.DateNullValue, 1);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    ledgerCtl.DeleteOpeningCredit(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(Session["VendorID"]), 25, Constants.DateNullValue, Convert.ToInt64(Session["VendorID"]), Convert.ToInt64(dt.Rows[0]["SALE_INVOICE_MASTER_ID"]), Convert.ToInt32(this.Session["UserId"]), 1);
                }
            }
            if (VendorCtl.UpdateVendorOpening(Convert.ToInt64(Session["OpeningID"]), Convert.ToInt64(Session["VendorID"]), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(rblOpening.SelectedValue), dtOpening, Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)), txtOpeningBalanceRemarks.Text))
            {

                ledgerCtl.InsertVendorOpening(Convert.ToInt32(Session["DistributorID"]), txtOpeningBalanceRemarks.Text, int.Parse(rblOpening.SelectedValue), Convert.ToDateTime(txtOpeningDate.Text), int.Parse(Session["VendorID"].ToString()), int.Parse(Session["VendorID"].ToString()), Convert.ToDecimal(dc.chkNull_0(txtOpeningBalance.Text)),
                        0, 0, false, 0, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(Session["VendorID"].ToString()));

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Opening Updated Successfully.');", true);
            }
        }
    }

    #endregion
}