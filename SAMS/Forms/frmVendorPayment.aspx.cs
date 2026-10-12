using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;

public partial class Forms_frmVendorPayment : System.Web.UI.Page
{
    readonly LedgerController ledgerCtl = new LedgerController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadBank();
            LoadVendorPayment();
            txtChequeDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            
            hfVId.Value = "";
            hfLId.Value = "";
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBank();
        LoadVendorPayment();
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 2, true);
    }

    protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadVendorPayment();
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlPrincipal, m_dt, 0, 1, true);
    }

    protected void LoadBank()
    {
        ddlBank.Items.Clear();
        if (ddlPaymentMode.SelectedValue == "18")
        {
            Configuration.GetAccountHead();
            AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(Configuration.BankDefaultType));
            clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 4);
        }
        else
        {
          
            ddlBank.Items.Add(new ListItem("Cash In Hand", "42"));
            
        }
    }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue == "18")
        {
            txtCheque.Enabled = true;
            txtSlip.Enabled = true;
            txtCheque.Text = "";
            txtSlip.Text = "";
            txtNarration.Text = "";
            ibnEndDate.Enabled = true;
            //txtNarration.Enabled = true;
            drpChequeStatus.Enabled = true;
        }
        else
        {
            txtCheque.Enabled = false;
            txtSlip.Enabled = false;
            ibnEndDate.Enabled = false;
            txtCheque.Text = "";
            txtSlip.Text = "";
            txtNarration.Text = "";
            drpChequeStatus.Enabled = false;
           
        }

        LoadBank();
        LoadVendorPayment();
    }
    protected void drpChequeStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hfVId.Value == "")
        {
            LoadVendorPayment();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerDataController LedgCtl = new CustomerDataController();
        DataControl dc = new DataControl();
        long AccontHeadID = 652;
        decimal Amount = Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
      

        if (Amount < 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter amount.');", true);
            return;
        }


        if (ddlBank.Items.Count > 0 && ddlLocation.Items.Count > 0 && ddlPrincipal.Items.Count > 0)
        {
            if (btnSave.Text == "Update")
            {
               
                if (ledgerCtl.DeleteVendorPayment2(Convert.ToInt32(ddlLocation.SelectedValue),
                    Convert.ToInt32(hfLId.Value), hfVId.Value))
                {
                    ledgerCtl.InsertVendorLedger(AccontHeadID, Convert.ToInt32(ddlLocation.SelectedValue),
                   Amount , 0, Convert.ToDateTime(Session["CurrentWorkDate"]),
                    txtNarration.Text, Convert.ToInt32(ddlPrincipal.SelectedValue),
                   Constants.LongNullValue, Constants.Document_Purchase, Convert.ToInt32(Session["UserID"]),
                   Convert.ToInt32(ddlPaymentMode.SelectedValue), Convert.ToDateTime(txtChequeDate.Text),
                   txtCheque.Text, txtSlip.Text, short.Parse(drpChequeStatus.SelectedValue));

                    ledgerCtl.InsertVendorLedger(Convert.ToInt32(ddlBank.SelectedValue),
                        Convert.ToInt32(ddlLocation.SelectedValue), 0, Amount,
                        Convert.ToDateTime(Session["CurrentWorkDate"]), txtNarration.Text,
                        Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.LongNullValue, Constants.Document_Purchase,
                        Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlPaymentMode.SelectedValue),
                        Convert.ToDateTime(txtChequeDate.Text), txtCheque.Text, txtSlip.Text, short.Parse(drpChequeStatus.SelectedValue));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Some error occured. Opening Credit not deleted.');", true);
                    return;
                }
            }
            else
            {


                ledgerCtl.InsertVendorLedger(AccontHeadID, Convert.ToInt32(ddlLocation.SelectedValue),
                    Amount , 0, Convert.ToDateTime(Session["CurrentWorkDate"]),
                    txtNarration.Text, Convert.ToInt32(ddlPrincipal.SelectedValue),
                    Constants.LongNullValue, Constants.Document_Purchase, Convert.ToInt32(Session["UserID"]),
                    Convert.ToInt32(ddlPaymentMode.SelectedValue), Convert.ToDateTime(txtChequeDate.Text),
                    txtCheque.Text, txtSlip.Text, short.Parse(drpChequeStatus.SelectedValue));
                ledgerCtl.InsertVendorLedger(Convert.ToInt32(ddlBank.SelectedValue),
                    Convert.ToInt32(ddlLocation.SelectedValue), 0, Amount,
                    Convert.ToDateTime(Session["CurrentWorkDate"]), txtNarration.Text,
                    Convert.ToInt32(ddlPrincipal.SelectedValue), Constants.LongNullValue, Constants.Document_Purchase,
                    Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlPaymentMode.SelectedValue),
                    Convert.ToDateTime(txtChequeDate.Text), txtCheque.Text, txtSlip.Text, short.Parse(drpChequeStatus.SelectedValue));
            }
        }
        else
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('InValid Inputs !');", true);
            return;
        }


        ClearForm();
        LoadVendorPayment();
    }

    private void ClearForm()
    {
        txtCheque.Text = "";
        txtSlip.Text = "";
        txtAmount.Text = "";
        txtDiscount.Text = "";
        txtIncomeTax.Text = "";
        txtNarration.Text = "";
        btnSave.Text = "Save";
        ddlPaymentMode.Enabled = true;
        ddlLocation.Enabled = true;
        ddlPrincipal.Enabled = true;
        hfVId.Value = "";
        hfLId.Value = "";
    }

    private void LoadVendorPayment()
    {
        DataTable dt;
        if (ddlPaymentMode.SelectedValue == "18")
        {
            dt = ledgerCtl.GetVendorPayment(int.Parse(ddlLocation.SelectedValue.ToString()), Convert.ToInt32(ddlPrincipal.SelectedValue), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToInt32(ddlPaymentMode.SelectedValue), short.Parse(drpChequeStatus.SelectedValue));

        }
        else
        {
            dt = ledgerCtl.GetVendorPayment(int.Parse(ddlLocation.SelectedValue.ToString()), Convert.ToInt32(ddlPrincipal.SelectedValue), DateTime.Parse(Session["CurrentWorkDate"].ToString()), Convert.ToInt32(ddlPaymentMode.SelectedValue), Constants.ShortNullValue);

        }
        GrdOrder.DataSource = dt;
        GrdOrder.DataBind();
    }

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        //if (ledgerCtl.DeleteVendorPayment2(Convert.ToInt32(ddlLocation.SelectedValue),
          //  Convert.ToInt32(ddlPrincipal.SelectedValue), GrdOrder.Rows[e.RowIndex].Cells[11].Text))
        if (ledgerCtl.DeleteVendorPayment(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlPrincipal.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlPaymentMode.SelectedValue), GrdOrder.Rows[e.RowIndex].Cells[0].Text.Replace("&nbsp;", ""), GrdOrder.Rows[e.RowIndex].Cells[1].Text.Replace("&nbsp;", ""), Convert.ToDateTime(GrdOrder.Rows[e.RowIndex].Cells[2].Text.Replace("&nbsp;", ""))))
        {
            ClearForm();
            LoadVendorPayment();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured. Payment not deleted.');", true);
        }
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ddlLocation.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        ddlPrincipal.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
        ddlPaymentMode.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
        if (ddlPaymentMode.SelectedValue == "18")
        {
            txtCheque.Enabled = true;
            txtSlip.Enabled = true;
            ibnEndDate.Enabled = true;
            drpChequeStatus.Enabled = true;
            //txtNarration.Enabled = true;
        }
        else
        {
            txtCheque.Enabled = false;
            txtSlip.Enabled = false;
            ibnEndDate.Enabled = false;
            drpChequeStatus.Enabled = false;
            // txtNarration.Enabled = false;
        }
        txtCheque.Text = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text.Replace("&nbsp;", "");
        txtSlip.Text = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        txtChequeDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
        txtNarration.Text = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text.Replace("&nbsp;", "");
        hfLId.Value = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text;
        hfVId.Value = GrdOrder.Rows[e.NewEditIndex].Cells[11].Text;
        ddlBank.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[12].Text;
        if (GrdOrder.Rows[e.NewEditIndex].Cells[13].Text.Replace("0", "") != "")
        {
            drpChequeStatus.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[13].Text;
        }
        btnSave.Text = "Update";

        ddlPrincipal.Enabled = false;
        ddlLocation.Enabled = false;
        ddlPaymentMode.Enabled = false;
    }
}