using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Vochers
/// </summary>
public partial class Forms_frmAssets : System.Web.UI.Page
{
    DataTable dtVoucher;

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
            txtVoucherDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
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
        // Load Account Head 
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dtHead = mAccountController.GetAssignAccountHead(4, 5);
        this.Session.Add("dtHead", dtHead);
        gvOpening.DataSource = dtHead;
        gvOpening.DataBind();
    }
    
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtNarration.Text = "";
        this.Session.Remove("dtVoucher");        
        this.CreatTable();
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
       
        DateTime  Voucherdate;

        Voucherdate = DateTime.Now;        
        
        if (txtVoucherDate.Text.Length > 0 && txtVoucherDate.Visible)
        {
            Voucherdate = DateTime.Parse(txtVoucherDate.Text + " 00:00:00");
        }
        else
        {
            Voucherdate = Constants.DateNullValue;
        }
        bool IsSaveVoucher = true;
        string MaxDocumentId = "";
        foreach (GridViewRow gvr in gvOpening.Rows)
        {
            TextBox txtDebit = (TextBox)gvr.FindControl("txtDebit");
            TextBox txtCredit = (TextBox)gvr.FindControl("txtCredit");
            TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");
            if (Convert.ToDecimal(dc.chkNull_0(txtDebit.Text)) > 0 || Convert.ToDecimal(dc.chkNull_0(txtCredit.Text)) > 0)
            {
                DataRow dr = dtVoucher.NewRow();
                dr["Ledger_ID"] = Constants.LongNullValue.ToString();
                dr["ACCOUNT_HEAD_ID"] = gvr.Cells[0].Text;
                dr["ACCOUNT_CODE"] = "";
                dr["ACCOUNT_NAME"] = "";
                dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtDebit.Text));
                dr["CREDIT"] = decimal.Parse(dc.chkNull_0(txtCredit.Text));
                dr["REMARKS"] = txtRemarks.Text;
                dr["Principal"] = "0";
                dr["Principal_id"] = "0";
                dtVoucher.Rows.Add(dr);

                MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()), Voucherdate);

                IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, Constants.Journal_Voucher,
                       Voucherdate, 19, "", txtNarration.Text, Voucherdate, "", dtVoucher, int.Parse(this.Session["UserId"].ToString()), "", Voucherdate,false);
            }
        }
        if (IsSaveVoucher == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Voucher No : " + MaxDocumentId + " has been Saved');", true);
            this.ClearAll();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occur Wrong Entry');", true);
        }
    }
        
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }
}