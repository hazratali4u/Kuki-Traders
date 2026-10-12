using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.Collections.Generic;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseEntry : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();

    private static int RowNo;
    private static int PrivouseQty, FreePrivousQty;

    DataTable PurchaseSKU;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadSKUDetail();
            CreatTable();
            GetDocumentNo();
            LoadBatchNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtExpDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            DrpDocumentType_SelectedIndexChanged(null, null);
        }
    }

    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        PurchaseSKU.Columns.Add("FREE_SKU", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("TIME_STAMP", typeof(DateTime));
        PurchaseSKU.Columns.Add("techId", typeof(int));
        this.Session.Add("PurchaseSKU", PurchaseSKU);

    }

    #region Load

    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
       
        PurchaseController mPurchase = new PurchaseController();

        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    private void LoadToDistributor()
    {
        DrpTransferFor.Items.Clear();

        if (DrpDocumentType.SelectedIndex == 3)
        {
            DataTable dtVendor = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpTransferFor, dtVendor, 0, 1, true);
            {
                DataRow[] foundRows = dtVendor.Select("VENDOR_ID  = '" + DrpTransferFor.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    hfVendorType.Value = Convert.ToString(foundRows[0]["vendorType"]);
                }
            }
            Session.Add("dtVendor", dtVendor);
        }
        else
        {

            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(DrpTransferFor, dt, 0, 2, true);   
        }
    }
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            if (DrpDocumentType.SelectedIndex == 2)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRate.Enabled = false;
            }
            else if (DrpDocumentType.SelectedIndex == 0)
            {
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRate.Enabled = false;
            }
            else if (DrpDocumentType.SelectedIndex == 1)
            {
                DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                txtRate.Enabled = false;
            }
            else if (DrpDocumentType.SelectedIndex == 3)
            {
                //DrpTransferFor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                drpDistributor.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
                txtDocumentNo.Text = dt.Rows[0][2].ToString();
                txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();
                DrpTransferFor.SelectedValue = dt.Rows[0]["PRINCIPAL_ID"].ToString();
                txtRate.Enabled = true;
            }
            PurchaseSKU = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
    private void LoadGird()
    {
        int TotalValue = 0;
        decimal TotalAmount = 0;

        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());
            TotalAmount += decimal.Parse(dr["Amount"].ToString());
        }
        txtTotalQuantity.Text = TotalValue.ToString();
        txtTotalAmount.Text = string.Format("{0:0.00}", TotalAmount);
    }

    private void LoadSKUDetail()
    {
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, "SKU_ID", "SkuPriceDetail2", true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }
    private void LoadBatchNo()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable dtBatchNo = PController.GetBatchNo(Convert.ToInt32(drpDistributor.SelectedValue), 0);
        hfBatchNo.Value = GetJson(dtBatchNo);
    }
    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    #endregion

    #region Grid OPerations

    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        PrivouseQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text);
        //txtExpDate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        
        hfSKUID.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        //  txtskuCode.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update";

    }
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.LoadGird();
        }
    }

    #endregion

    private bool CheckDublicateSKU()
    {

        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue +  "'");
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;

    }

    private int CheckStockStatus(int SKU_ID)
    {
        if (DrpDocumentType.SelectedIndex == 1)
        {
            return -1;
        }
        else
        {
            PhaysicalStockController mController = new PhaysicalStockController();
            DataTable dt = mController.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue.ToString()), SKU_ID, hfBatchValue.Value, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            if (dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty >= int.Parse(txtQuantity.Text))
                {
                    return -1;
                }
                else
                {
                    return int.Parse(dt.Rows[0][0].ToString()) + PrivouseQty + FreePrivousQty;
                }
            }
        }
        return 0;
    }

    private bool CalculatePurchase(DateTime MWorkDate)
    {
        decimal mTotalAmount = 0;
        bool flag = false;
        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];
        foreach (DataRow dr in dtPurchaseDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

        }
        if (DrpDocumentType.SelectedIndex == 0)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                flag  = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), 0, false);
                
            }
            else
            {
                flag  = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(DrpTransferFor.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), 0, false);
                
            }
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                flag  = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), 0, false);
               
            }
            else
            {
                flag  = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpTransferFor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), 0, false);
                
            }
        }
        else if (DrpDocumentType.SelectedIndex == 3)
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                flag  = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, 0, int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpTransferFor.SelectedValue),Convert.ToBoolean(hfVendorType.Value));//, Convert.ToBoolean(hfVendorType.Value)
                
            }
            else
            {
                flag  = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, 0, int.Parse(drpDistributor.SelectedValue.ToString())
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(DrpTransferFor.SelectedValue), Convert.ToBoolean(hfVendorType.Value));
                
            }
        }
        else
        {
            if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
            {
                flag  = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), 0,false);
                
            }
            else
            {
                flag  = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                , mTotalAmount, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), 0, false);
                
            }
        }
        return flag;
    }

    #region Click Operations

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {

            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            DateTime dtExpDate = Constants.DateNullValue;

            if (txtExpDate.Text.Length > 0)
            {
                try
                {
                    dtExpDate = Convert.ToDateTime(txtExpDate.Text);
                }
                catch (Exception ex)
                {
                }
            }

          
            int CurrentStock = CheckStockStatus(int.Parse(dc.chkNull_0(foundRows[0]["SKU_ID"].ToString())));

            if (btnSave.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    if (CurrentStock == -1)
                    {
                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];

                        dr["SKU_Name"] = ddlSKuCde.SelectedItem.Text;
                        dr["FREE_SKU"] = 0;
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        if (DrpDocumentType.SelectedValue == "3")
                        {
                            dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                            dr["AMOUNT"] = decimal.Parse(dc.chkNull_0(txtRate.Text)) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                        }
                        else
                        {
                            dr["PRICE"] = foundRows[0]["TRADE_PRICE"];
                            dr["AMOUNT"] = decimal.Parse(foundRows[0]["TRADE_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                        }
                        dr["TIME_STAMP"] = dtExpDate;
                        dr["techId"] = foundRows[0]["TECHNOLOGY_ID"];
                        PurchaseSKU.Rows.Add(dr);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Current closing Stock is " + CurrentStock.ToString() + "');", true);
                        //  txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                    //   txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSave.Text == "Update")
            {
                if (CurrentStock == -1)
                {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];

                    dr["SKU_Name"] = ddlSKuCde.SelectedItem.Text;
                   
                    
                    dr["Quantity"] = int.Parse(txtQuantity.Text);
                    dr["FREE_SKU"] = 0;

                    if (DrpDocumentType.SelectedValue == "3")
                    {
                        dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));
                        dr["AMOUNT"] = decimal.Parse(dc.chkNull_0(txtRate.Text)) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    }
                    else
                    {
                        dr["PRICE"] = foundRows[0]["TRADE_PRICE"];
                        dr["AMOUNT"] = decimal.Parse(foundRows[0]["TRADE_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    }
                    dr["TIME_STAMP"] = dtExpDate;
                    dr["techId"] = foundRows[0]["TECHNOLOGY_ID"];

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + "Current closing Stock is " + CurrentStock.ToString() + "');", true);
                    // txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.ClearAll();
            this.LoadGird();
            DisAbaleOption(true);
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong SKU Select');", true);

        }
    }
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
        if (DrpDocumentType.SelectedIndex == 0)
        {
            if (drpDistributor.SelectedValue.ToString() == DrpTransferFor.SelectedValue.ToString())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Transfer to Location must be different ');", true);
                return;
            }
        }
       
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                    PurchaseSKU.Rows.Clear();
                    Session.Add("PurchaseSKU", PurchaseSKU);
                    LoadGird();
                    GetDocumentNo();
                    drpDistributor.Enabled = true;
                    DrpDocumentType.Enabled = true;
                    ClearAll();
                    LoadBatchNo();
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";

                    LoadToDistributor();

                    DisAbaleOption(false);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record save successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('some error occurred');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = "";
    }

    #endregion

    #region Index Change

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
        }
        else
        {
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            drpDistributor.Enabled = false;
            DrpDocumentType.Enabled = false;
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
            this.LoadBatchNo();
        }
    }
    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DrpDocumentType.SelectedIndex == 0)
        {
            lblfromLocation.Text = "Transfer From";
            drpDistributor.Enabled = true;
            // LoadToDistributor();
            this.DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Transfer To";
            this.GetDocumentNo();
            txtRate.Enabled = false;
        }
        else if (DrpDocumentType.SelectedIndex == 1)
        {
            lblfromLocation.Text = "Transfer to";
            drpDistributor.Enabled = true;
            //LoadToDistributor();
            this.DrpTransferFor.Visible = true;
            Label4.Visible = true;
            Label4.Text = "Transfer From";
            txtRate.Enabled = false;
            this.GetDocumentNo();
        }
        else if (DrpDocumentType.SelectedIndex == 3)
        {

            lblfromLocation.Text = "Return From";
            drpDistributor.Enabled = true;
            
            Label4.Visible = true;
            DrpTransferFor.Visible = true;
            Label4.Text = "Vendor";
            txtRate.Enabled = true;
            GetDocumentNo();
        }
        else
        {
            lblfromLocation.Text = "Location";
            drpDistributor.Enabled = true;
            this.DrpTransferFor.Visible = false;
            Label4.Visible = false;
            this.GetDocumentNo();
            txtRate.Enabled = false;
        }
        LoadToDistributor();
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
        this.LoadBatchNo();
    }
    protected void DrpTransferFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(DrpDocumentType.SelectedValue) == Constants.Document_Purchase_Return)
        {
            if (Session["dtVendor"] != null)
            {
                DataTable dt = (DataTable)Session["dtVendor"];
                DataRow[] foundRows = dt.Select("VENDOR_ID  = '" + DrpTransferFor.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    hfVendorType.Value = Convert.ToString(foundRows[0]["vendorType"]);
                }
            }
        }
    }
    #endregion

    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            DrpDocumentType.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
        else
        {
            DrpDocumentType.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
        }
    }

    private void ClearAll()
    {
        // txtskuCode.Text = "";
        //txtskuName.Text = "";
        txtQuantity.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "0";
      //  txtExpDate.Text = "";
        // txtskuCode.Enabled = true;
        btnSave.Text = "Add";
        PrivouseQty = 0;
        FreePrivousQty = 0;
    }


}