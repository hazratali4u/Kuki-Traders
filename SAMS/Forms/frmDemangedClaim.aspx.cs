using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Customer Claim
/// </summary>
public partial class Forms_frmDemangedClaim : System.Web.UI.Page
{
    DataTable ClaimedSKU;
    private static int RowId;
    private static int URowId;
    private static long VoucherNo;
    LedgerController LController = new LedgerController();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadClaimType();
            LoadAccountHead();
            
            CreatTableValue();
           
            LoadRoute();
            LoadCustomer();
            LoadGrid();
            btnAddNew.Attributes.Add("onclick", "return ValidateValueForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
        }
    }

    private void LoadDistributor()
    {
        var dController = new DistributorController();
        var dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));        
        RbdClaimType.SelectedIndex = 0;         
    }
    
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId,Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
        #region Commited Code
        /*
        DrpAccountHead.Items.Clear();
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("SECURITY DEPOSIT BOTTLE", "424"));
        }
        else if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("Adv. against Empty Bottle", "511"));
            DrpAccountHead.Items.Add(new ListItem("Sales Return", "245"));
            DrpAccountHead.Items.Add(new ListItem("Sale Discount - Claimable", "493"));
        }
         */
       
        /*
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectClaimHead(int.Parse(RbdClaimType.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 2, true);
         */
      
        #endregion
    }
        
    private void CreatTableValue()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();  

    }
    
    private void LoadRoute()
    {
        //DistributorAreaController mController = new DistributorAreaController();
        //DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        //clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 6, true);

        ddlRoute.Items.Clear();
        GeoHierarchyController gController = new GeoHierarchyController();
    
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDropDownList(ddlRoute, dt, "GEO_ID", "GEO_NAME");


        ddlRoute.Enabled = true;     
    }
    
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0 && ddlRoute.Items.Count > 0)
        {
            var mController = new CustomerDataController();
          

            var dt = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlRoute.SelectedValue),3);
            clsWebFormUtil.FillDropDownList(DrpCustomer, dt, 0, 3, true);


            if (dt.Rows.Count > 0)
            {
                DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + DrpCustomer.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    hfZoneId.Value = Convert.ToString(foundRows[0]["zoneId"]);
                }
            }
            Session.Add("dt", dt);

        }
        else
        {
            DrpCustomer.Items.Clear();   
        }
    }
    
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            var lController = new LedgerController();
            var dt = lController.SelectClaimDetail(int.Parse(drpDistributor.SelectedValue),int.Parse(RbdClaimType.SelectedValue),
                DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()),0);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }


    private void ClearGrd_Order()
    {
        txtRemarks.Text = "";
        txtAmount.Text = "";
        btnAddNew.Text = "Save";
        
    }
 
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[4].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)),Constants.LongNullValue  , decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[9].Text)),2);

        ClearGrd_Order();
        LoadGrid();
        
    }
    private void EnableDisable(bool flag)
    {
        if (flag == true)
        {
            drpDistributor.Enabled = false;
            ddlRoute.Enabled = false;
            DrpCustomer.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            ddlRoute.Enabled = true;
            DrpCustomer.Enabled = true;
        }
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        URowId = Convert.ToInt32(GrdOrder.Rows[e.NewEditIndex].Cells[0].Text);
        ddlRoute.SelectedValue = Convert.ToString( GrdOrder.Rows[e.NewEditIndex].Cells[11].Text);
        LoadCustomer();
        DrpCustomer.SelectedValue = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[1].Text);
        DrpAccountHead.SelectedValue = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[2].Text);
        txtAmount.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[9].Text);
        txtRemarks.Text = Convert.ToString(GrdOrder.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;",""));
        VoucherNo = Convert.ToInt64(GrdOrder.Rows[e.NewEditIndex].Cells[8].Text);
        EnableDisable(true);

        btnAddNew.Text = "Update";
    }

    private void InsertGL()
    {
        SAMSCommonLayer.Classes.Configuration.GetAccountHead();
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue);
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim from " + DrpCustomer.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = Session["VoucherNo"].ToString();

            dtVoucher.Rows.Add(dr);

            DataRow dr1 = dtVoucher.NewRow();

            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
           
            if (hfZoneId.Value == "1")
            {
                dr1["ACCOUNT_HEAD_ID"] = Configuration.SouthZone;
            }
            else if (hfZoneId.Value == "5")
            {
                dr1["ACCOUNT_HEAD_ID"] = Configuration.EastZoneSouthPunjab;
            }
            else if (hfZoneId.Value == "6")
            {
                dr1["ACCOUNT_HEAD_ID"] = Configuration.EastZoneCentralPunjab;
            }
            else if (hfZoneId.Value == "7")
            {
                dr1["ACCOUNT_HEAD_ID"] = Configuration.NortZone;
            }
            else if (hfZoneId.Value == "8")
            {
                dr1["ACCOUNT_HEAD_ID"] = Configuration.WestZone;
            }
            else
            {
                dr1["ACCOUNT_HEAD_ID"] = Configuration.SouthZone;
            }

            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim from " + DrpCustomer.SelectedItem.Text;

            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
            dr1["Principal_Id"] = Session["VoucherNo"].ToString();

            dtVoucher.Rows.Add(dr1);
        }
        else
        {
            ///Credit Side Entry
            DataRow dr = dtVoucher.NewRow();
            dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
            
            if (hfZoneId.Value == "1")
            {
                dr["ACCOUNT_HEAD_ID"] = Configuration.SouthZone;
            }
            else if (hfZoneId.Value == "5")
            {
                dr["ACCOUNT_HEAD_ID"] = Configuration.EastZoneSouthPunjab;
            }
            else if (hfZoneId.Value == "6")
            {
                dr["ACCOUNT_HEAD_ID"] = Configuration.EastZoneCentralPunjab;
            }
            else if (hfZoneId.Value == "7")
            {
                dr["ACCOUNT_HEAD_ID"] = Configuration.NortZone;
            }
            else if (hfZoneId.Value == "8")
            {
                dr["ACCOUNT_HEAD_ID"] = Configuration.WestZone;
            }
            else
            {
                dr["ACCOUNT_HEAD_ID"] = Configuration.SouthZone;
            }

            dr["DEBIT"] = decimal.Parse(txtAmount.Text);
            dr["CREDIT"] = 0;
            dr["Principal_Id"] = Session["VoucherNo"].ToString();
            dr["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim from " + DrpCustomer.SelectedItem.Text;

            dtVoucher.Rows.Add(dr);

            DataRow dr1 = dtVoucher.NewRow();
            dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
            dr1["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountHead.SelectedValue);
            dr1["DEBIT"] = 0;
            dr1["CREDIT"] = decimal.Parse(txtAmount.Text); 
            dr1["Principal_Id"] = Session["VoucherNo"].ToString();
            dr1["REMARKS"] = DrpAccountHead.SelectedItem.Text + " Claim from " + DrpCustomer.SelectedItem.Text;


            dtVoucher.Rows.Add(dr1);
        }

        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

        LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue),int.Parse(Session["VoucherNo"].ToString()), MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]),int.Parse(RbdClaimType.SelectedValue), Session["VoucherNo"].ToString(), "Defualt " + DrpAccountHead.SelectedItem.Text + " Voucher", Convert.ToDateTime(Session["CurrentWorkDate"]), null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue,true);
    }
    
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (btnAddNew.Text == "Save")
        {
            string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()),0);
            Session.Add("VoucherNo", MaxDocumentId);

            if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
            {
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                   DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                    null, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaim, "");
               
                InsertGL();
            }
            else
            {
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                    DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                     null, int.Parse(Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, "");
                
                InsertGL();
            }
        }
        else
        {
            DataControl dc = new DataControl();

            LController.UpdateLedgerClaim(int.Parse(drpDistributor.SelectedValue), URowId,VoucherNo, long.Parse(DrpAccountHead.SelectedValue), decimal.Parse(dc.chkNull_0(txtAmount.Text)), Convert.ToString(txtRemarks.Text + "- Claim "  + DrpCustomer.SelectedItem.Text), int.Parse(RbdClaimType.SelectedValue));
           
            EnableDisable(false);
            
        }
       
       
        ClearGrd_Order();
       // LoadAccountHead();
        LoadGrid();
    }

    #region Index/Change

    /// <summary>
    /// Loads Account Heads And Claim Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();
        LoadGrid();
      
        if (ddlClaimMode.SelectedIndex == 1)
        {
            LoadDocumentNo();
        }
    }

    protected void ddlClaimMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlClaimMode.SelectedIndex == 0)
        //{
        //    lblDocumentNo.Visible = false;
        //    ddlDocumentNo.Visible = false;
           

        //    txtAmount.Visible = true;
        //    lblAmount.Visible = true;
        //    btnAddNew.Visible = true;
        //    pnlValue.Visible = true;
        //}
        //else
        //{
        //    txtAmount.Visible = false;
        //    lblAmount.Visible = false;
        //    btnAddNew.Visible = false;
        //    pnlValue.Visible = false;

        //    pnlProduct.Visible = true;
        //    lblDocumentNo.Visible = true;
        //    ddlDocumentNo.Visible = true;
        //    LoadDocumentNo();
        //    CreatTable();
        //    LoadPurchaseGrid();
        //}
    }

    protected void ddlDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (long.Parse(ddlDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            DataTable dt = (DataTable)Session["dtDocumentNo"];
            DataRow[] foundRows = dt.Select("CLAIM_MASTER_ID  = '" + ddlDocumentNo.SelectedItem.Text + "'");
            if (foundRows.Length > 0)
            {
                DrpAccountHead.SelectedValue = foundRows[0]["ACCOUNT_HEAD_ID"].ToString();
                txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
               // txtTotalAmount.Text = foundRows[0]["AMOUNT"].ToString();
                LoadClaimDetail();
                ClearAll();
           //     ScriptManager.GetCurrent(Page).SetFocus(txtskuCode);                
            }
        }
        else
        {
            ClearAll();
           
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRoute();
        LoadCustomer();
        if (ddlClaimMode.SelectedIndex == 1)
        {
            LoadDocumentNo();
        }
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();

        if (ddlClaimMode.SelectedIndex == 1)
        {
            LoadDocumentNo();
        }
       
    }

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClaimMode.SelectedIndex == 1)
        {
            LoadDocumentNo();
        }
        if (Session["dt"] != null)
        {
            DataTable dt = (DataTable)Session["dt"];

            DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + DrpCustomer.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
                hfZoneId.Value = Convert.ToString(foundRows[0]["zoneId"]);
            }
        }
    }
    #endregion

    #region Load

    /// <summary>
    /// Loads Document Nos
    /// </summary>
    private void LoadDocumentNo()
    {
        if (DrpCustomer.Items.Count > 0)
        {
            LedgerController LedgrCtl = new LedgerController();
            ddlDocumentNo.Items.Clear();
            DataTable dtDocumentNo = LedgrCtl.GetCustomerClaimMaster(Constants.IntNullValue, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), RbdClaimType.SelectedIndex);
            ddlDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlDocumentNo, dtDocumentNo, "LEDGER_ID", "CLAIM_MASTER_ID");
            Session.Add("dtDocumentNo", dtDocumentNo);
        }
    }

    private void LoadClaimDetail()
    {
        LedgerController LegrCtl = new LedgerController();
        ClaimedSKU = LegrCtl.GetCustomerClaimDetail(Convert.ToInt32(ddlDocumentNo.SelectedItem.Text));
        Session.Add("ClaimedSKU", ClaimedSKU);
       // LoadPurchaseGrid();
    }

    /// <summary>
    /// Loads SKU Data To SKU ListBox
    /// </summary>
   
    #endregion
   

    private void ClearAll()
    {
      
        txtRemarks.Text = "";
        txtAmount.Text = "";
    }

 

  
}
