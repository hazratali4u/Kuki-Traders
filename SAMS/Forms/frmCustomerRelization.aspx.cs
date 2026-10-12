using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form For Bank Transaction
/// </summary>
public partial class Forms_frmCustomerRelization : System.Web.UI.Page
{
    readonly CustomerDataController mController = new CustomerDataController();
    readonly LedgerController LController = new LedgerController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SAMSCommonLayer.Classes.Configuration.GetAccountHead();
            LoadDistributor();
            LoadArea();
            LoadData();
           // LoadCreditCustomer();
            SelectCreditInvoice();
           

            LoadAccountHead();  
            LoadGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SetTableSorter();
            txtTransferDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtTransferDate.Attributes.Add("readonly", "readonly");
            LoadPaymentRecieved();
        }
    }

    private void SetTableSorter()
    {
        if (GrdOrder.Rows.Count > 1)
        {
            GrdOrder.UseAccessibleHeader = true;
            GrdOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #region Load

   
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    private void LoadArea()
    {
        //DistributorAreaController mController = new DistributorAreaController();
        //DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        //clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);

        DrpRoute.Items.Clear();
        GeoHierarchyController gController = new GeoHierarchyController();
      //  DataTable dt = gController.SelectGeoHierarchy(int.Parse(drpDistributor.SelectedValue.ToString()));
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Town, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(Session["CompanyId"].ToString()));
       
        DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, "GEO_ID", "GEO_NAME");
    }
    private void LoadData()
    {
        DrpCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            
            try
            {
                DataTable dtCustomer = mController.SelectAllCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpRoute.SelectedValue.ToString()), 3);
                clsWebFormUtil.FillDropDownList(DrpCustomer, dtCustomer, 0, 2, true);
                DrpRoute.Enabled = true;

                if (dtCustomer.Rows.Count > 0)
                {
                    DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID  = '" + DrpCustomer.SelectedValue + "'");
                    if (foundRows.Length > 0)
                    {
                        hfZoneId.Value = Convert.ToString(foundRows[0]["zoneId"]);
                    }
                }
                Session.Add("dtCustomer", dtCustomer);
            }
            catch (Exception ex)
            {                
            }
            
        }
    }
   
    private void SelectCreditInvoice()
    {
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0 )
        {
            DataTable dtCredit = LController.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpCustomer.SelectedValue.ToString()), 0);
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
        }
    }
   
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
         DataTable dt = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpAccountType.SelectedValue),
                    DateTime.Parse(Session["CurrentWorkDate"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()));
             
            Session.Add("dt", dt);

            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();

            decimal TotalAmount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                TotalAmount += decimal.Parse(dr["Balance"].ToString());
            }

            txttotalAmount.Text = string.Format("{0:0.00}", TotalAmount);
        }
        else
        {
            txttotalAmount.Text = "0.00";
        }
    }
    private void LoadAccountHead()
    {
        DrpAccountDetail.Items.Clear();
        AccountHeadController mAccountController = new AccountHeadController();
       // if (ParentID == 382)
       // {
        if (DrpAccountType.SelectedValue == "21" || (DrpAccountType.SelectedValue == "211"))
        {
            
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, int.Parse(SAMSCommonLayer.Classes.Configuration.CashDefaultType),0);
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
        }
        else
        {

            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, int.Parse(SAMSCommonLayer.Classes.Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
        }
       // }
     //   else
        //{
         //   DataTable dt = mAccountController.SelectAccountHeadLocation(Constants.AC_AccountHeadId, ParentID, Convert.ToInt32(drpDistributor.SelectedValue));
        //    clsWebFormUtil.FillDropDownList(DrpAccountDetail, dt, 0, 4, true);
       // }
    }

    private void LoadPaymentRecieved()
    {
        
        LedgerController LController = new LedgerController();


        if (drpDistributor.Items.Count > 0)
        {
            DataSet dsReceived = LController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue,
                DateTime.Parse(Session["CurrentWorkDate"].ToString()));

            DataTable dtRealized = dsReceived.Tables[2];

            if (dtRealized.Rows.Count > 0)
            {
                lblAmount.Text = string.Format("{0:0,0.00}", Convert.ToDecimal(dtRealized.Rows[0][0].ToString()));

            }

        }
    }
  

    #endregion

    #region Sel/Index Change

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadData();
        LoadGrid();
        if (DrpAccountType.SelectedValue == "35")
        {
           // LoadAccountHead(257);
        }
        else
        {
            LoadAccountHead();
        }
        GrdOrder.Visible = true;
        SetTableSorter();
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
      
        
        //LoadOnlineCash();
        SetTableSorter();
        SelectCreditInvoice();
        LoadGrid();
    }
    protected void DrpAccountDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();        
        SetTableSorter();
    }
    protected void DrpAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpAccountType.SelectedValue == "21" )
        {
            toggleControls(DrpAccountType.SelectedValue);
           
        }
      
        else if (DrpAccountType.SelectedValue == "33")
        {
            toggleControls(DrpAccountType.SelectedValue);
            
         
        }
      
        LoadAccountHead();
       
        LoadGrid();
        GrdOrder.Visible = true;
        SetTableSorter();
    }

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["dtCustomer"] != null)
        {
            DataTable dt = (DataTable)Session["dtCustomer"];
            DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + DrpCustomer.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
                hfZoneId.Value = Convert.ToString(foundRows[0]["zoneId"]);
            }
        }
        SetTableSorter();
        
        SelectCreditInvoice();
        LoadGrid();
    }
   
   
    #endregion

    #region Grid Operations

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataControl dc = new DataControl();

            if (DrpAccountType.SelectedValue == "21" || DrpAccountType.SelectedValue == "33")
            {
                long invoiceId = Convert.ToInt64(GrdOrder.Rows[e.RowIndex].Cells[3].Text.Replace("&nbsp;", "0"));
                
                //2 voucher type id ,5 voucher no, 8 balnce amount, 10 document no
                //latest  //2 voucher type id ,1 voucher no, 9 balnce amount, 3 document no
               if(LController.DeleteCashBankTransction(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
                int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[1].Text)), invoiceId, decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[9].Text))))
               {
                   SelectCreditInvoice();
                   LoadGrid();
                   SetTableSorter();
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record remove successfully.')", true);
               }
                 else
               {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.')", true);
               }
            }
            //else
            //{
            //    LController.DeleteCashBankTransction(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
            //              int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[1].Text)), long.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[3].Text)), decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[9].Text)));

            //}
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.')", true);
        }
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ChequeEntryController Ccontroller = new ChequeEntryController();

            DrpCustomer.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;

            txtChequeNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
          
            txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
            txtSlipNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
            txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
            DrpAccountDetail.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
            try
            {
                //   DrpDeliveryMan.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[14].Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Sale Force not found.');", true);
            }

            btnSave.Text = "Update";
            SelectCreditInvoice();

            //DataTable dt = Ccontroller.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 0);

            //foreach (GridViewRow dr in GrdCredit.Rows)
            //{
            //    foreach (DataRow dbr in dt.Rows)
            //    {

            //        if (Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]) == Convert.ToInt64(dbr["SALE_INVOICE_ID"]))
            //        {
            //            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            //            chRelized.Checked = true;
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Invoice not found for selected cheque');", true);
        }
    }


  #endregion

    private DataTable CreateTable()
    {
        DataTable dtLedger = new DataTable();
        dtLedger.Columns.Add("VOUCHER_TYPE_ID", typeof(int));
        dtLedger.Columns.Add("VOUCHER_NO", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtLedger.Columns.Add("Distributor_ID", typeof(int));
        dtLedger.Columns.Add("Debit", typeof(decimal));
        dtLedger.Columns.Add("Credit", typeof(decimal));
        dtLedger.Columns.Add("Ledger_Date", typeof(DateTime));
        dtLedger.Columns.Add("Remarks", typeof(string));
        dtLedger.Columns.Add("TimeStamp", typeof(DateTime));
        dtLedger.Columns.Add("Customer_ID", typeof(int));
        dtLedger.Columns.Add("Principal_ID", typeof(int));
        dtLedger.Columns.Add("Cheque_NO", typeof(string));
        dtLedger.Columns.Add("UserId", typeof(int));
        dtLedger.Columns.Add("Document_ID", typeof(long));
        dtLedger.Columns.Add("Manual_Document_ID", typeof(int));
        dtLedger.Columns.Add("DocumentTypeID", typeof(int));
        dtLedger.Columns.Add("SlipNo", typeof(string));
        dtLedger.Columns.Add("ChequeDate", typeof(DateTime));
        dtLedger.Columns.Add("PaymentMode", typeof(int));
        dtLedger.Columns.Add("PayeesName", typeof(string));
        Session.Add("dtLedger", dtLedger);
        return dtLedger;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            int InvoiceCount = Constants.IntNullValue;
            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
                if (chRelized.Checked == true)
                {
                    InvoiceCount++;
                    break;
                }
            }


            if (InvoiceCount == Constants.IntNullValue)
            {
                CashAdvance();
                InsertGL();
                Session.Remove("VoucherNo");
                flag = true;
            }
            else
            {
                if (DrpAccountType.SelectedValue == "33")
                {
                    if (InvoiceCount == Constants.IntNullValue)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Select Invoice.')", true);
                        return;
                    }
                }
                CashRealization();
                InsertGL();
                Session.Remove("VoucherNo");
                flag = true;
            }

            ClearAll();
            SelectCreditInvoice();
            LoadGrid();
            SetTableSorter();
            LoadPaymentRecieved();
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record saved successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.')", true);
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
       
            DataTable dt = (DataTable)Session["dt"];

            switch (ddSearchType.SelectedIndex)
            {
                case 1:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSearch.Text + "%'";
                    break;
                case 2:
                    dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSearch.Text + "%'";
                    break;
               
                default:
                    dt.DefaultView.RowFilter = "Voucher_type" + " like '%" + "" + "%'";
                    break;
            }
            GrdOrder.DataSource = dt.DefaultView;
            GrdOrder.DataBind();
        
    }

    private void toggleControls(string pAccountType)
    {
        if (pAccountType == "33")// || (pAccountType == "333"))
        {
            lblSlipNo.Visible = true;
            txtSlipNo.Visible = true;
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
           // gvCheques.Visible = false;
            lblTransferDate.Visible = true;
            txtTransferDate.Visible = true;
            imgTransferDate.Visible = true;
           
        }
        else {
            lblSlipNo.Visible = false;
            txtSlipNo.Visible = false;
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
           // gvCheques.Visible = false;
            lblTransferDate.Visible = false;
            txtTransferDate.Visible = false;
            imgTransferDate.Visible = false;
           
            GrdOrder.Visible = true;
            txtAmount.Text = "";
            DrpAccountDetail.Enabled = true;
            imgTransferDate.Enabled = true;
            txtSlipNo.Enabled = true;
            txtAmount.Enabled = true;
            txtRemarks.Enabled = true;
        }
    }

    private void CashRealization()
    {
       
        LedgerController LController = new LedgerController();
        
        
        decimal OfferAmount = decimal.Parse(txtAmount.Text);
        decimal realizeAmount = 0;
        
        string remarks = "Cash " + txtRemarks.Text;
        int VoucherType = Constants.Cash_Voucher;
        int PaymentMode = Constants.Cash_Advance;//For Cash Advance (21) and Cash Realization (19)
        int CustomerbankId = 0;
        DateTime chqDate = Constants.DateNullValue;
       
        if (DrpAccountType.SelectedValue == "33")
        {
            PaymentMode = 33;
            VoucherType = Constants.Bank_Voucher;
            remarks = "Online Transfer " + DrpAccountDetail.SelectedItem.Text + ", " + txtRemarks.Text;
           
            chqDate = Convert.ToDateTime(txtTransferDate.Text);
        }
        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()),0);
        Session.Add("VoucherNo", MaxDocumentId);

        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                string manualNo = "0";
                if (Convert.ToString(dr.Cells[5].Text) == "opng")
                {
                    manualNo = "opng";
                }
                if (decimal.Parse(dr.Cells[3].Text) >= OfferAmount)
                {
                    realizeAmount += OfferAmount;
                
                    LController.PostingCash_Bank_Account(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.AccountReceivable), int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                    DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId,
                    txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice, txtSlipNo.Text, chqDate, PaymentMode, "");

                    LController.PostingCash_Bank_Account(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpAccountDetail.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                    DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId,
                    txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice, txtSlipNo.Text, chqDate, PaymentMode, "");

                    OfferAmount = decimal.Parse(dr.Cells[3].Text) - OfferAmount;

                    LController.UpdateSaleInvoice(Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount);

                    break;
                }
                else if (decimal.Parse(dr.Cells[3].Text) <= OfferAmount)
                {
                    realizeAmount += decimal.Parse(dr.Cells[3].Text);
               
                    LController.PostingCash_Bank_Account(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.AccountReceivable), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(dr.Cells[3].Text),
                    DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId,
                    txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice, txtSlipNo.Text, chqDate, PaymentMode, "");

                    LController.PostingCash_Bank_Account(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpAccountDetail.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(dr.Cells[3].Text), 0,
                    DateTime.Parse(Session["CurrentWorkDate"].ToString()), remarks, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId,
                    txtChequeNo.Text, int.Parse(Session["UserId"].ToString()), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["SALE_INVOICE_ID"]), manualNo, Constants.Document_Invoice, txtSlipNo.Text, chqDate, PaymentMode, "");

                    OfferAmount = OfferAmount - decimal.Parse(dr.Cells[3].Text);
                    LController.UpdateSaleInvoice(long.Parse(dr.Cells[1].Text), int.Parse(drpDistributor.SelectedValue.ToString()), 0);
                    
                
                }

            }

        }
        if (realizeAmount < decimal.Parse(txtAmount.Text))
        {
          
            LController.PostingCash_Bank_Account(VoucherType, long.Parse(MaxDocumentId), int.Parse(Configuration.AccountReceivable)
                , int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount, DateTime.Parse(Session["CurrentWorkDate"].ToString())
                , remarks + " (Advance)", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId, txtChequeNo.Text, int.Parse(Session["UserId"].ToString())
                , 0, "0", Constants.Document_Invoice, txtSlipNo.Text, chqDate, PaymentMode, "");

            LController.PostingCash_Bank_Account(VoucherType, long.Parse(MaxDocumentId), long.Parse(DrpAccountDetail.SelectedValue.ToString())
                , int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString())
            , remarks + " (Advance)", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), CustomerbankId, txtChequeNo.Text, int.Parse(Session["UserId"].ToString())
            , 0, "0", Constants.Document_Invoice, txtSlipNo.Text, chqDate, PaymentMode, "");

       
        }
       
    }
    private void CashAdvance()
    {
        int CustomerbankId = 0;

        string remarks = "Cash " + txtRemarks.Text;

       
        
        int VoucherType = Constants.Cash_Voucher;
        if (DrpAccountType.SelectedValue == "33")
        {
            VoucherType = Constants.Bank_Voucher;
            
                remarks = "Online Transfer " + DrpAccountDetail.SelectedItem.Text + ", " + txtRemarks.Text;
            
        }
        string MaxDocumentID = LController.SelectLedgerMaxDocumentId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()),0);
        Session.Add("VoucherNo", MaxDocumentID);
        DataTable dtLedger = CreateTable();

        //Credit from Account Receivable (Party Wise)
        DataRow dr = dtLedger.NewRow();

        dr["VOUCHER_TYPE_ID"] = VoucherType;
        dr["VOUCHER_NO"] = MaxDocumentID;
        dr["ACCOUNT_HEAD_ID"] = Configuration.AccountReceivable;
        dr["Distributor_ID"] = drpDistributor.SelectedValue;
        dr["DEBIT"] = 0;
        dr["CREDIT"] = txtAmount.Text;
        dr["Ledger_Date"] = Session["CurrentWorkDate"].ToString();
        dr["Remarks"] = remarks;
        dr["TimeStamp"] = DateTime.Now;
       
        dr["Principal_ID"] = CustomerbankId;
        dr["Cheque_NO"] = txtChequeNo.Text;
        dr["UserID"] = Session["UserId"].ToString();
        dr["Document_ID"] = Constants.LongNullValue;
        dr["Manual_Document_ID"] = 0;
        dr["DocumentTypeID"] = Constants.IntNullValue;
        dr["SlipNo"] = txtSlipNo.Text;
        if (DrpAccountType.SelectedValue == "21")
        {
            dr["ChequeDate"] = Constants.DateNullValue;
        }
        else
        {
            dr["ChequeDate"] = Convert.ToDateTime(txtTransferDate.Text);
        }
        dr["PaymentMode"] = DrpAccountType.SelectedValue;
        
            dr["PayeesName"] = "";
            dr["Customer_ID"] = DrpCustomer.SelectedValue;
        
        dtLedger.Rows.Add(dr);

        //Debit to selected Account Head
        DataRow dr2 = dtLedger.NewRow();

        dr2["VOUCHER_TYPE_ID"] = VoucherType;
        dr2["VOUCHER_NO"] = MaxDocumentID;
        dr2["ACCOUNT_HEAD_ID"] = DrpAccountDetail.SelectedValue;
        dr2["Distributor_ID"] = drpDistributor.SelectedValue;
        dr2["DEBIT"] = txtAmount.Text;
        dr2["CREDIT"] = 0;
        dr2["Ledger_Date"] = Session["CurrentWorkDate"].ToString();
        dr2["Remarks"] = remarks;
        dr2["TimeStamp"] = DateTime.Now;
       
        dr2["Principal_ID"] = CustomerbankId;
        dr2["Cheque_NO"] = txtChequeNo.Text;
        dr2["UserID"] = Session["UserId"].ToString();
        dr2["Document_ID"] = Constants.LongNullValue;
        dr2["Manual_Document_ID"] = 0;
        dr2["DocumentTypeID"] = Constants.IntNullValue;
        dr2["SlipNo"] = txtSlipNo.Text;
        if (DrpAccountType.SelectedValue == "21")
        {
            dr2["ChequeDate"] = Constants.DateNullValue;
        }
        else
        {
            dr2["ChequeDate"] = Convert.ToDateTime(txtTransferDate.Text);
        }
        dr2["PaymentMode"] = DrpAccountType.SelectedValue;
        
        
            dr2["PayeesName"] = "";
            dr2["Customer_ID"] = DrpCustomer.SelectedValue;
                dtLedger.Rows.Add(dr2);

        LController.PostingCash_Bank_Account(dtLedger);
    }
   
    private void InsertGL() 
    {
        /*
        int CustomerbankId = 0;
       

        SAMSCommonLayer.Classes.Configuration.GetAccountHead();
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtVoucher.Columns.Add("DEBIT", typeof(decimal));
        dtVoucher.Columns.Add("CREDIT", typeof(decimal));
        dtVoucher.Columns.Add("REMARKS", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));

        DataRow dr = dtVoucher.NewRow();
        dr["LEDGER_ID"] = Constants.LongNullValue.ToString();
        dr["ACCOUNT_HEAD_ID"] = Convert.ToInt64(DrpAccountDetail.SelectedValue);
       
            dr["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
        
        
        dr["DEBIT"] = decimal.Parse(txtAmount.Text);
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 0;
        dtVoucher.Rows.Add(dr);
        ///Credit Side Entry
        DataRow dr1 = dtVoucher.NewRow();
        dr1["LEDGER_ID"] = Constants.LongNullValue.ToString();
       
        dr1["DEBIT"] = 0;
        dr1["CREDIT"] = decimal.Parse(txtAmount.Text);
        dr1["Principal_Id"] = 0;
        
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
            dr1["REMARKS"] = DrpAccountType.SelectedItem.Text + " received from " + DrpCustomer.SelectedItem.Text;
       
        dtVoucher.Rows.Add(dr1);


        string MaxDocumentId = "";

        //using chequeDate as Transfer Date
        if (DrpAccountType.SelectedValue == "21")
        {
            MaxDocumentId = LController.SelectMaxVoucherId(Constants.Cash_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

            LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, Constants.Cash_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Cash_Relization, Session["VoucherNo"].ToString(), "Defualt " + DrpAccountType.SelectedItem.Text + " Voucher", Convert.ToDateTime(Session["CurrentWorkDate"]), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true);

        }
        else
        {
             MaxDocumentId = LController.SelectMaxVoucherId(Constants.Bank_Voucher, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

             LController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), CustomerbankId, MaxDocumentId, Constants.Bank_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.Bank_Deposit, Session["VoucherNo"].ToString(), "Defualt " + DrpAccountType.SelectedItem.Text + " Voucher", Convert.ToDateTime(txtTransferDate.Text.ToString()), txtChequeNo.Text, dtVoucher, Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, Constants.DateNullValue, true);
        }
          */
    }

    private void ClearAll()
    {
        txtTransferDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtRemarks.Text = "";
        txtSlipNo.Text = "";
        btnSave.Text = "Save";
        
    }

   
}