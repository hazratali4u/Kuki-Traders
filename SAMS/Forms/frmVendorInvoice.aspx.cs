using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.Data;

public partial class Forms_frmVendorInvoice : System.Web.UI.Page
{

    readonly PurchaseController _purchaseCtrl = new PurchaseController();
    readonly VenderEntryController mVenderController = new VenderEntryController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            LoadDistributor();
            LoadVendor();

            LoadVendorMaster();

            btnSave.Attributes.Add("onclick", "return ValidateValueForm();");
        }
    }
    private void LoadDistributor()
    {
        var dController = new DistributorController();
        var dt = dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
    private void LoadVendor()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();

        DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));

        clsWebFormUtil.FillDropDownList(DrpVendor, dtVendor, 0, 1, true);

        DrpVendor.Focus();

    }
    
    private void LoadVendorMaster()
    {
        GrdVendorMaster.DataSource = null;
        GrdVendorMaster.DataBind();

        try
        {
            DataTable dtOrder = mVenderController.SelectVendorMaster(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpVendor.SelectedValue));

            GrdVendorMaster.DataSource = dtOrder;
            GrdVendorMaster.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }

    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadVendorMaster();
    }
    protected void DrpVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadVendorMaster();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime InvoiceDate;
            try
            {
                if (txtInvoiceDate.Text.Length == 10)
                {
                    InvoiceDate = DateTime.Parse(ConvertDate.British_To_American(txtInvoiceDate.Text));

                }
                else
                {
                    InvoiceDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Correct Invoice Date Pattern is DD/MM/YYYY.');", true);
                return;
            }

            DataControl dc = new DataControl();
         

            bool IsValidInsert = false;

            if ((hfVMId.Value == "") && (btnSave.Text=="Save"))
            {
                IsValidInsert = mVenderController.Add_VenderInvoice(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpVendor.SelectedValue), int.Parse(Session["UserID"].ToString())
                       , DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), dc.chkNull_0(txtInvNo.Text), InvoiceDate, txtRemarks.Text
                       ,Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)));

            }
            else
            {
                IsValidInsert = mVenderController.Update_VenderInvoice(int.Parse(drpDistributor.SelectedValue), Convert.ToInt64(hfVMId.Value)
                    , int.Parse(DrpVendor.SelectedValue)
                    , int.Parse(this.Session["UserID"].ToString()), dc.chkNull_0(txtInvNo.Text)
                    , InvoiceDate, txtRemarks.Text
                    , false,Convert.ToDecimal(dc.chkNull_0(txtAmount.Text)));
            }
            if (IsValidInsert)
            {
                
                LoadVendorMaster();
                ClearALL();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearALL();
        LoadVendor();
        
        LoadVendorMaster();
    }
    private void ClearALL()
    {
        hfVMId.Value = "";
        txtInvNo.Text = "";
        txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtRemarks.Text = "";
        txtAmount.Text = "";
        btnSave.Text = "Save";
        DrpVendor.Enabled = true;
        drpDistributor.Enabled = true;        
    }

    #region Grid Operations

    protected void GrdVendorMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {

        int RowId = e.NewEditIndex;

        hfVMId.Value = GrdVendorMaster.Rows[RowId].Cells[0].Text;
        drpDistributor.SelectedValue = GrdVendorMaster.Rows[RowId].Cells[3].Text;
        txtInvNo.Text = GrdVendorMaster.Rows[RowId].Cells[5].Text;
       
        txtInvoiceDate.Text = GrdVendorMaster.Rows[RowId].Cells[6].Text;
        txtRemarks.Text = GrdVendorMaster.Rows[RowId].Cells[8].Text.Replace("&nbsp;", "");
        txtAmount.Text = GrdVendorMaster.Rows[RowId].Cells[7].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
        DrpVendor.Enabled = false;
        drpDistributor.Enabled = false;
       // GrdVendorMaster.Rows[RowId].BackColor = System.Drawing.Color.Coral;
    }

    protected void GrdVendorMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            bool IsValidInsert = mVenderController.Delete_VenderInvoice(int.Parse(drpDistributor.SelectedValue), Convert.ToInt64(GrdVendorMaster.Rows[e.RowIndex].Cells[0].Text)
                , int.Parse(DrpVendor.SelectedValue), int.Parse(this.Session["UserID"].ToString()), null, Constants.DateNullValue, null, true, Constants.DecimalNullValue);

            if (IsValidInsert)
            {
                LoadVendorMaster();
                ClearALL();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.');", true);
        }
    
    }

    #endregion
}