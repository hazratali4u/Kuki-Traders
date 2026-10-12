using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Branch Expense And Salary
/// </summary>
public partial class frmWithDrawnEntry : System.Web.UI.Page
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
            this.LoadDistributor();            
            this.LoadAccountHead();
            this.LoadDocumentNo();
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
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
    /// Loads Account Heads To AccountHead Combo
    /// </summary>
    private void LoadAccountHead()
    {
        long ParentID = Constants.LongNullValue;
        if (drpDistributor.SelectedValue == "3")
        {
            ParentID = 247;
        }
        else if (drpDistributor.SelectedValue == "4")
        {
            ParentID = 248;
        }
        else
        {
            ParentID = 246;
        }

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, ParentID,0);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4,true);
    }        

    /// <summary>
    /// Loads Voucher Nos To Voucher No Combo
    /// </summary>
    private void LoadDocumentNo()
    {
        LedgerController LdgCtl = new LedgerController();
        DataTable dtWithdrawn = LdgCtl.GetPettyWithdrawn(Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(drpDistributor.SelectedValue), Constants.LongNullValue, Constants.IntNullValue, Constants.LongNullValue);
        Session.Add("dtWithdrawn", dtWithdrawn);
        
        DrpVoucherNo.Items.Clear();
        DrpVoucherNo.Items.Add(new ListItem("New", "0"));
        clsWebFormUtil.FillDropDownList(DrpVoucherNo, dtWithdrawn, 0, 0);   
    }
    
    /// <summary>
    /// Save Or Updates Expenses And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool flag = true;
        if (txtAmount.Text.Length <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter Amount')", true);
            return;
        }

        string strChequeNo = "";
        if (txtChequeNo.Visible)
        {
            strChequeNo = txtChequeNo.Text;
        }

        try
        {
            LedgerController LedCtl = new LedgerController();
            if (btnSave.Text == "Save")
            {
                if (LedCtl.InsertPettyWithdDrawn(Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt64(DrpAccountHead.SelectedValue), Convert.ToInt32(rbType.SelectedValue), strChequeNo, Convert.ToDecimal(txtAmount.Text), txtRemarks.Text, Convert.ToInt32(Session["UserID"])))
                {
                    this.LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Petty Withdrawn saved successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured')", true);
                }
            }
            else
            {
                if (LedCtl.UpdatePettyWithDrawn(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt64(DrpAccountHead.SelectedValue), Convert.ToInt32(rbType.SelectedValue), strChequeNo, Convert.ToDecimal(txtAmount.Text), txtRemarks.Text, Convert.ToInt32(Session["UserID"]), Convert.ToInt64(DrpVoucherNo.SelectedValue)))
                {
                    this.LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Petty Withdrawn updated successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured')", true);
                }
            }
            
        }
        catch (Exception ex)
        {
            flag = false;
        }

        if (flag)
        {
            txtAmount.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
    }

    /// <summary>
    /// Loads Expense And Salary Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpVoucherNo.SelectedValue.ToString() != "0")
        {
            DataTable dtWithdrawn = (DataTable)this.Session["dtWithdrawn"];
            DataRow[] foundRecord = dtWithdrawn.Select("PETTY_WITHDRAWN_ID = '" + DrpVoucherNo.SelectedValue + "'");
            if (foundRecord.Length > 0)
            {
                drpDistributor.SelectedValue = foundRecord[0]["DISTRIBUTOR_ID"].ToString();
                DrpAccountHead.SelectedValue = foundRecord[0]["ACCOUNT_HEAD_ID"].ToString();
                rbType.SelectedValue = foundRecord[0]["PETTY_WITHDRAWN_MODE"].ToString();
                rbType_SelectedIndexChanged(null,null);
                txtChequeNo.Text = foundRecord[0]["CHEQUE_NO"].ToString();
                txtAmount.Text = foundRecord[0]["AMOUNT"].ToString();
                txtRemarks.Text = foundRecord[0]["REMARKS"].ToString();
            }

            btnSave.Text = "Update";            
        }
        else
        {
            btnSave.Text = "Save";
            txtAmount.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }   
    }
    
    protected void rbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbType.SelectedIndex == 0)
        {
            lblChequeNo.Visible = true;
            txtChequeNo.Visible = true;
        }
        else
        {
            lblChequeNo.Visible = false;
            txtChequeNo.Visible = false;
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDocumentNo();
        this.LoadAccountHead();
    }
}
