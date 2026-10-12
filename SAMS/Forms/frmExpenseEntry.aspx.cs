using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Branch Expense And Salary
/// </summary>
public partial class frmExpenseEntry : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();            
            LoadAccountHead();
            LoadPettyAccountHead();
            CreatTable();
            LoadVoucherNo();
            btnAddNew.Attributes.Add("onclick", "return ValidateForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// </summary>
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetLocationAccountHead(Constants.IntNullValue, Convert.ToInt32(rblDetailAccountType.SelectedValue),Convert.ToInt32(drpDistributor.SelectedValue),0);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
    }

  

    private void LoadPettyAccountHead()
    {

        SAMSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
     //   DataTable dt = mAccountController.GetLocationPettyAccountHead(Convert.ToInt32(drpDistributor.SelectedValue), 1);
     


        DataTable dt = mAccountController.SelectAccountHeadManual(long.Parse(SAMSCommonLayer.Classes.Configuration.ExpensDefaultType),Constants.AC_AccountHeadId, 1);
        clsWebFormUtil.FillDropDownList(ddlPettyHead, dt, 0, 1, true);
    }
            
    /// <summary>
    /// Creates Datatable For Expense And Salary
    /// </summary>
    private void CreatTable()
    {
        DataTable  dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        dtVoucher.Columns.Add("ACCOUNT_PARENT_ID", typeof(string));
        Session.Add("dtVoucher", dtVoucher);

    }

    /// <summary>
    /// Loads Voucher Nos To Voucher No Combo
    /// </summary>
    private void LoadVoucherNo()
    {
        LedgerController LController = new LedgerController();
        //DataTable  dt  = LController.SelectVoucherNo(((DateTime)Session["CurrentWorkDate"]), int.Parse(drpDistributor.SelectedValue.ToString()), 24, int.Parse(Session["UserId"].ToString()),null);

        DataTable dt = LController.SelectVoucherNo(((DateTime)Session["CurrentWorkDate"]), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.Journal_Voucher, int.Parse(Session["UserId"].ToString()), null);
       
        DrpVoucherNo.Items.Clear();
        DrpVoucherNo.Items.Add(new ListItem("New", "0"));
        clsWebFormUtil.FillDropDownList(DrpVoucherNo, dt, 3, 3);   
    }

    /// <summary>
    /// Deletes Expense And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable  dtVoucher = (DataTable)Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        lblRowId.Text = "-1";

        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        }
        txtMainCash.Text = TotalDebit.ToString();
  
    }

    /// <summary>
    /// Sets Expense And Salary Data For Edit. This Function Runs When An Existing Expense And Salary Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblRowId.Text  = e.NewEditIndex.ToString();
        LoadAccountHead();        
        DrpAccountHead.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
    }

    /// <summary>
    /// Adds Record To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        DataTable  dtVoucher = (DataTable)Session["dtVoucher"];
        if (lblRowId.Text == "-1")
        {
            DataRow dr = dtVoucher.NewRow();
            dr["ACCOUNT_HEAD_ID"] = ddlPettyHead.SelectedValue.ToString();
            dr["ACCOUNT_NAME"] = ddlPettyHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = txtRemarks.Text;
            dr["Principal"] = "General Expense";
            dr["Principal_id"] = "0";
            dr["ACCOUNT_PARENT_ID"] = Constants.IntNullValue;
            dtVoucher.Rows.Add(dr);

        }
        else
        {
            DataRow dr = dtVoucher.Rows[int.Parse(lblRowId.Text)];
            dr["ACCOUNT_HEAD_ID"] = ddlPettyHead.SelectedValue.ToString();
            dr["ACCOUNT_NAME"] = ddlPettyHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = "Petty Voucher";
            dr["Principal"] = "General Expense";
            dr["Principal_id"] = "0";
            dr["ACCOUNT_PARENT_ID"] = Constants.IntNullValue;
        }
        
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        txtAmount.Text = "";
        //txtRemarks.Text = "";
        lblRowId.Text = "-1";
        ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);
        
        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        }
        txtMainCash.Text = TotalDebit.ToString();
    }

    /// <summary>
    /// Save Or Updates Expenses And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LedgerController mLController = new LedgerController();
        SAMSCommonLayer.Classes.Configuration.GetAccountHead();
        DataControl dc = new DataControl();
        DataTable dtVoucher = (DataTable)Session["dtVoucher"];
        bool IsSaveVoucher;
        int VoucherTypeID = 16;

        if (ddlPettyHead.SelectedValue == "258" || ddlPettyHead.SelectedValue == "633" || ddlPettyHead.SelectedValue == "634")
        {
            VoucherTypeID = 24;
        }

        string MaxDocumentId;
        if (ddlPettyHead.Items.Count > 0)
        {
            if (dtVoucher.Rows.Count > 0)
            {
                DataRow dr = dtVoucher.NewRow();
                dr["ACCOUNT_HEAD_ID"] = ddlPettyHead.SelectedValue;
                dr["ACCOUNT_NAME"] = "Peatty Cash";
                dr["DEBIT"] = 0;
                dr["Credit"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                dr["REMARKS"] = "Petty Voucher";
                dr["Principal"] = "General Expense";
                dr["Principal_id"] = "0";
                dtVoucher.Rows.Add(dr);
                if (DrpVoucherNo.SelectedValue.ToString() == "0")
                {
                    MaxDocumentId = mLController.SelectMaxVoucherId(VoucherTypeID, int.Parse(drpDistributor.SelectedValue.ToString()), ((DateTime)Session["CurrentWorkDate"]));

                    IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, VoucherTypeID,
                           ((DateTime)Session["CurrentWorkDate"]), Constants.Document_Petty_Cash, null, txtRemarks.Text, Constants.DateNullValue, null, dtVoucher, int.Parse(Session["UserId"].ToString()), "");
                }
                else
                {
                    MaxDocumentId = DrpVoucherNo.SelectedValue.ToString();

                    IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, VoucherTypeID,
                           ((DateTime)Session["CurrentWorkDate"]), Constants.Document_Petty_Cash, null, txtRemarks.Text, Constants.DateNullValue, null, dtVoucher, int.Parse(Session["UserId"].ToString()), "");
                }

                if (IsSaveVoucher == true)
                {
                    dtVoucher.Rows.Clear();
                    GrdOrder.DataSource = null;
                    GrdOrder.DataBind();
                    LoadAccountHead();
                    txtAmount.Text = "";
                    txtRemarks.Text = "";
                    lblRowId.Text = "-1";
                    ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);

                    decimal TotalDebit = 0;

                    foreach (DataRow dr1 in dtVoucher.Rows)
                    {
                        TotalDebit += decimal.Parse(dr1["DEBIT"].ToString());
                    }
                    txtMainCash.Text = TotalDebit.ToString();
                    LoadVoucherNo();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some Error in Voucher')", true);
                    return;
                }
            }
            else if (lblRowId.Text == "-1")
            {
                MaxDocumentId = DrpVoucherNo.SelectedValue.ToString();

                IsSaveVoucher = mLController.Delete_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), MaxDocumentId, VoucherTypeID);
                Session.Remove("dtVoucher");
                LoadVoucherNo();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Petty Head not found')", true);
            return;
        }
    }

    /// <summary>
    /// Loads Expense And Salary Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {       
        LedgerController LController = new LedgerController();
        DataTable dtVoucher = (DataTable)Session["dtVoucher"];

        if (DrpVoucherNo.SelectedValue.ToString() != "0")
        {
            dtVoucher = LController.SelectVoucherDetail(int.Parse(drpDistributor.SelectedValue.ToString()), DrpVoucherNo.SelectedValue.ToString(), Constants.Journal_Voucher);
            for (int i = 0; i < dtVoucher.Rows.Count; i++)
            {
                if (Decimal.Parse(dtVoucher.Rows[i]["CREDIT"].ToString()) > 0)
                {
                    dtVoucher.Rows.RemoveAt(i);
                }
            }
            Session.Add("dtVoucher", dtVoucher);

            decimal TotalDebit = 0;

            foreach (DataRow dr in dtVoucher.Rows)
            {
                TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            }
            txtMainCash.Text = String.Format("{0:0.00}", TotalDebit);

            GrdOrder.DataSource = dtVoucher;
            GrdOrder.DataBind();
        }
        else
        {
            dtVoucher.Rows.Clear();  
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();

        }
        lblRowId.Text = "0";
        Session.Add("dtVoucher", dtVoucher);
       
    }
    
    protected void rblDetailAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();        
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();
        LoadPettyAccountHead();
    }
}
