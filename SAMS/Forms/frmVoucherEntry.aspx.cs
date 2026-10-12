using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Vochers
/// </summary>
public partial class Forms_frmVoucherEntry : System.Web.UI.Page
{
    DataTable dtVoucher;
    private static int RowId = -1;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadAccountDetail();
            this.CreatTable();
            this.LoadAccountHead();
            this.HideShowControls();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtDueDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            RowId = -1;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;

            DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
            DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
        }
    }

    /// <summary>
    /// Loads Account Heads To Bank Combo
    /// </summary>
    private void LoadAccountHead()
    {
        SAMSCommonLayer.Classes.Configuration.GetAccountHead();
        if (DrpVoucherType.Items.Count > 0)
        {
            AccountHeadController mAccountController = new AccountHeadController();

            if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == 14)
            {
                
                DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommonLayer.Classes.Configuration.CashDefaultType), 0);
                clsWebFormUtil.FillDropDownList(drpBanks, dt, 0, 4, true);
            }
            else if (int.Parse(DrpVoucherType.SelectedValue.ToString()) == 15)
            {

                DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(SAMSCommonLayer.Classes.Configuration.BankDefaultType), 0);
                clsWebFormUtil.FillDropDownList(drpBanks, dt, 0, 4, true);
            }
            else
            {
                drpBanks.Items.Clear();
                drpBanks.Items.Add(new ListItem("N/A", Constants.IntNullValue.ToString()));
            }
        }
    }
    
    /// <summary>
    /// Creates Datatable To Hold Voucher Data
    /// </summary>
    private void CreatTable()
    {
        dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("InvoiceNo", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
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
    /// Loads Account Heads To ListBox
    /// </summary>
    private void LoadAccountDetail()
    {
        //Load Voucher Type by User Rights 

        LedgerController mLedgerController = new LedgerController();
        DataTable dt = mLedgerController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpVoucherType, dt, 0, 1, true);

        // Load Account Head 
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.SelectAccountHeadLocation(Constants.AC_AccountHeadId, Constants.LongNullValue,Convert.ToInt32(drpDistributor.SelectedValue));
       
        clsWebFormUtil.FillDropDownList(this.ddlAccountHead, dtHead, "ACCOUNT_HEAD_ID", "ACCOUNT_NAME",true);
        this.Session.Add("dtHead", dtHead);
    }
    
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtTotalCredit.Text = "0";
        txtTotalDebit.Text = "0";
        txtRemarks.Text = "";
        txtAccountDes.Text = "";
        GrdOrder.DataSource = null;
        GrdOrder.DataBind();
        this.Session.Remove("dtVoucher");
        this.CreatTable();
        txtpayeesName.Text = "";
        drpDistributor.Enabled = true;
        DrpVoucherType.Enabled = true;
        drpDistributor.Enabled = true;
        DrpVoucherSubType.Enabled = true;
        
    }
    
    /// <summary>
    /// Shows Vouchers in Crystal Report For Print Purpose
    /// </summary>
    /// <param name="VoucherNo">Voucher</param>
    private void PrintVoucher(string VoucherNo)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        int VoucherType = Constants.IntNullValue;
        string VoucherType2 = "";
        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            VoucherType2 = DrpVoucherType.SelectedItem.Text;
        }
        else
        {
            VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            VoucherType2 = DrpVoucherSubType.SelectedItem.Text;
        }

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), VoucherNo, VoucherType, Constants.IntNullValue);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
        CrpReport.SetParameterValue("VoucherSubType", VoucherType2);
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    
    /// <summary>
    /// Loads Account Heads To ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {        
        this.ClearAll();
        this.LoadAccountHead();
        this.HideShowControls();
        txtDebitAmount.Enabled = true;
        txtCreditAmount.Enabled = true;
        if (DrpVoucherType.SelectedIndex != 2)
        {
            DrpVoucherSubType_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Adds Voucher Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;

        DataControl dc = new DataControl();
        DataTable dtHead = (DataTable)this.Session["dtHead"];
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        if (RowId >= 0)
        {
            DataRow dr = dtVoucher.Rows[RowId];
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedValue;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["REMARKS"] = txtAccountDes.Text;

        }
        else
        {
            DataRow dr = dtVoucher.NewRow();
            dr["Ledger_ID"] = Constants.LongNullValue.ToString();
            dr["ACCOUNT_HEAD_ID"] = ddlAccountHead.SelectedValue;
            dr["ACCOUNT_CODE"] = "";
            dr["ACCOUNT_NAME"] = ddlAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebitAmount.Text));
            dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCreditAmount.Text));
            dr["REMARKS"] = txtAccountDes.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dtVoucher.Rows.Add(dr);
        }

        #region Set Total

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        }
        txtTotalDebit.Text = decimal.Round(TotalDebit, 2).ToString();
        txtTotalCredit.Text = decimal.Round(TotalCredit, 2).ToString();

        #endregion

        #region Clear Txtbox
                
        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtAccountDes.Text = "";
        RowId = -1;

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        if (GrdOrder.Rows.Count > 0)
        {
            DrpVoucherSubType.Enabled = false;
            DrpVoucherType.Enabled = false;
            drpDistributor.Enabled = false;
        }
        this.Session.Add("dtVoucher", dtVoucher);
        ScriptManager.GetCurrent(Page).SetFocus(ddlAccountHead);        
    }

    /// <summary>
    /// Deletes Voucher
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        decimal TotalDebit = 0;
        decimal TotalCredit = 0;
        dtVoucher = (DataTable)this.Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);

        #region Set Total

        //foreach (DataRow dr in dtVoucher.Rows)
        //{
        //    TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        //    TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
        //}
        txtTotalDebit.Text = TotalDebit.ToString();
        txtTotalCredit.Text = TotalCredit.ToString();

        #endregion

        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        RowId = -1;
    }

    /// <summary>
    /// Sets Voucher For Edit. This Function Runs When An Existing Voucher Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlAccountHead.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtAccountDes.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtDebitAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
        txtCreditAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;        
    }

    /// <summary>
    /// Saves Voucher
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDone_Click(object sender, EventArgs e)
    {
        LedgerController mLController = new LedgerController();
        DataControl dc = new DataControl();
        dtVoucher = (DataTable)this.Session["dtVoucher"];

        DataTable dtHead = (DataTable)this.Session["dtHead"];

            decimal TotalDebit = 0;
            decimal TotalCredit = 0;

            #region Set Total

            foreach (DataRow dr in dtVoucher.Rows)
            {
                TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
                TotalCredit += decimal.Parse(dr["CREDIT"].ToString());
            }

            #endregion

            #region In Case of Bank & Cash Voucher

            if (drpBanks.SelectedItem.Text != "N/A")
            {
                decimal differnceAmt = TotalDebit - TotalCredit;

                if (differnceAmt > 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = drpBanks.SelectedValue;
                    dr1["ACCOUNT_CODE"] = "";
                    dr1["ACCOUNT_NAME"] = drpBanks.SelectedItem.Text;
                    dr1["REMARKS"] = txtRemarks.Text;
                    dr1["CREDIT"] = differnceAmt;
                    dr1["DEBIT"] = 0;
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);

                }
                else if (differnceAmt < 0)
                {
                    DataRow dr1 = dtVoucher.NewRow();
                    dr1["Ledger_ID"] = Constants.LongNullValue.ToString();
                    dr1["ACCOUNT_HEAD_ID"] = drpBanks.SelectedValue;
                    dr1["ACCOUNT_CODE"] = "";
                    dr1["ACCOUNT_NAME"] = drpBanks.SelectedItem.Text;
                    dr1["REMARKS"] = txtRemarks.Text;
                    dr1["CREDIT"] = 0;
                    dr1["DEBIT"] = -(differnceAmt);
                    dr1["Principal"] = "";
                    dr1["Principal_id"] = dtVoucher.Rows[0]["Principal_id"].ToString();
                    dtVoucher.Rows.Add(dr1);
                }
            }
           
            #endregion

            int VoucherType = Constants.IntNullValue;

            if (DrpVoucherType.SelectedValue == "16")
            {
                VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }
        
        DateTime ChequeDate;
        DateTime  Voucherdate;
        DateTime DueDate;        

        Voucherdate = DateTime.Now;
        ChequeDate = Constants.DateNullValue;
        
        if (txtDueDate.Text.Length > 0 && txtDueDate.Visible)
        {
            DueDate = DateTime.Parse(txtDueDate.Text + " 00:00:00");
        }
        else
        {
            DueDate = Constants.DateNullValue;
        }

        dtVoucher = (DataTable)this.Session["dtVoucher"];
        string MaxDocumentId = mLController.SelectMaxVoucherId(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()),Voucherdate);

        if (TotalDebit == TotalCredit || DrpVoucherType.SelectedValue != "16")
        {
            bool IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, VoucherType,
                   DateTime.Parse(Session["currentworkdate"].ToString()), int.Parse(DrpPaymentMode.SelectedValue.ToString()), txtpayeesName.Text, txtRemarks.Text, DueDate, txtChequeNo.Text, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtSlipNo.Text, DueDate, false);

            if (IsSaveVoucher == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);
                ClearAll();
                PrintVoucher(MaxDocumentId);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occur Wrong Entry');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Debit Must Equal To Credit Balance');", true);
        }
    }

    /// <summary>
    /// Sets Grid Footer Columns
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = txtTotalDebit.Text;
            e.Row.Cells[7].Text = txtTotalCredit.Text;
        }
    }

    private void HideShowControls()
    {
        if (DrpVoucherType.SelectedValue == "14")
        {            
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
            lblSlipNo.Visible = false;
            txtSlipNo.Visible = false;
            lblDueDate.Visible = false;
            txtDueDate.Visible = false;
            ibnEndDate.Visible = false;

            lblPaymentMode.Visible = true;
            DrpPaymentMode.Visible = true;
            lblBankHead.Visible = true;
            drpBanks.Visible = true;

            DrpVoucherSubType.Items.Clear();
            DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));
            lblSubType.Visible = true;
            DrpVoucherSubType.Visible = true;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
        else if (DrpVoucherType.SelectedValue == "16")
        {

            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
            lblSlipNo.Visible = false;
            txtSlipNo.Visible = false;
            lblDueDate.Visible = false;
            txtDueDate.Visible = false;
            ibnEndDate.Visible = false;

            lblPaymentMode.Visible = false;
            DrpPaymentMode.Visible = false;
            lblBankHead.Visible = false;
            drpBanks.Visible = false;

            lblSubType.Visible = false;
            DrpVoucherSubType.Visible = false;

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = true;
        }
        else
        {   
            lblChequeNo.Visible = true;
            txtChequeNo.Visible = true;
            lblSlipNo.Visible = true;
            txtSlipNo.Visible = true;
            lblDueDate.Visible = true;
            txtDueDate.Visible = true;
            ibnEndDate.Visible = true;
            lblPaymentMode.Visible = true;
            DrpPaymentMode.Visible = true;
            lblBankHead.Visible = true;
            drpBanks.Visible = true;


            DrpVoucherSubType.Items.Clear();
            DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));

            lblSubType.Visible = true;
            DrpVoucherSubType.Visible = true;

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
    }

    protected void DrpVoucherSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpPaymentMode.Items.Clear();
        if (DrpVoucherSubType.SelectedIndex == 0)
        {
            if (DrpVoucherType.SelectedValue == "14")
            {                
                DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }
            else if (DrpVoucherType.SelectedValue == "15")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }

            txtDebitAmount.Enabled = false;
            txtCreditAmount.Enabled = true;
        }
        else if (DrpVoucherSubType.SelectedIndex == 1)
        {
            if (DrpVoucherType.SelectedValue == "14")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cash", "19"));
                DrpPaymentMode.Items.Add(new ListItem("Online Transfer", "30"));
            }
            else if (DrpVoucherType.SelectedValue == "15")
            {
                DrpPaymentMode.Items.Add(new ListItem("Cheque", "18"));
                DrpPaymentMode.Items.Add(new ListItem("Pay Order", "21"));
            }

            txtDebitAmount.Enabled = true;
            txtCreditAmount.Enabled = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountDetail();
        this.LoadAccountHead();
    }
}