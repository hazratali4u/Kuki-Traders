using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseOrderEntry : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadPrincipal();
            LoadDistributor();
            LoadSKUDetail();
            CreatTable();
            GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];

            txtNetAmount.Attributes.Add("readonly", "readonly");
            txtGstValue.Attributes.Add("readonly", "readonly");
            txtAmountFC.Attributes.Add("readonly", "readonly");
            txtTotalQuantity.Attributes.Add("readonly", "readonly");

            ddlCurrency.Enabled = false;
        }
    }

    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
      
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(int));
        PurchaseSKU.Columns.Add("PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        Session.Add("PurchaseSKU", PurchaseSKU);

    }

    #region Check_Duplication
    
    //Stop replication for ref no
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || Session["hfBillBookNo"].ToString() != txtRefNo.Text) && txtRefNo.Text.Trim().Length > 0)
        {
            OrderEntryController OEC = new OrderEntryController();
            DataTable dtBillBookNo = OEC.SelectBillBookNo(int.Parse(drpDistributor.SelectedValue), txtRefNo.Text, 0);
            if (dtBillBookNo.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }

    private bool CheckDublicateSKU()
    {
       
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
       
            return false;
       
    }
    #endregion

    #region Load
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue), 0, Constants.DateNullValue);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, "PURCHASE_ORDER_ID","VENDOR_NAME");
    }

    
    private void LoadPrincipal()
    {
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(drpPrincipal, m_dt, 0, 1, true);
    }
   
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }
   
    private void LoadDocumentDetail()
    {
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), 0,Constants.DateNullValue);
        if (dt.Rows.Count > 0)
        {
            txtRefNo.Text = dt.Rows[0]["REF_NO"].ToString().Replace("&nbsp;","");
            txtgst.Text = dt.Rows[0]["gst"].ToString();
            txtGstValue.Text = dt.Rows[0]["gstValue"].ToString();
            txtNetAmount.Text = string.Format("{0:0.00}", dt.Rows[0]["netAmount"]);
            txtCreditTerm.Text = dt.Rows[0]["creditTerm"].ToString();
            ddlCurrency.SelectedValue = dt.Rows[0]["CURRENCY_ID"].ToString();
            txtExchangeRate.Text = dt.Rows[0]["EXCHANGE_RATE"].ToString();
            drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            RbUnitType.SelectedValue = dt.Rows[0]["QTY_TYPE"].ToString();

            PurchaseSKU = mPurchase.SelectPurchaseOrderDetail(long.Parse(dt.Rows[0][0].ToString()), Convert.ToInt32(drpDistributor.SelectedValue));
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
    
    private void LoadGird()
    {
        int TotalValue = 0;
        decimal TotalAmountFc = 0;
        decimal NetAmount = 0;
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());
            TotalAmountFc += Convert.ToDecimal(dr["AMOUNT"]);
            NetAmount += Convert.ToDecimal(dr["AMOUNT"]);
        }

        txtTotalQuantity.Text = TotalValue.ToString();
        txtAmountFC.Text = Math.Round(TotalAmountFc, 2).ToString();
        //txtAmountRs.Text = string.Format("{0:0.00}", TotalAmountFc * Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)));
        txtNetAmount.Text = NetAmount.ToString();
    }

    private void LoadSKUDetail()
    {
        SkuController SKUCtl = new SkuController();
        if (drpPrincipal.Items.Count > 0)
        {
            DataTable Dtsku_Price = SKUCtl.SelectSkuData(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(drpPrincipal.SelectedValue));
            if (Dtsku_Price != null)
            {
                clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, 0, 4, true);

                if (Dtsku_Price.Rows.Count > 0)
                {
                    lblMinStock.Text = Dtsku_Price.Rows[0]["MIN_STOCK"].ToString();
                    lblMaxStock.Text = Dtsku_Price.Rows[0]["MAX_STOCK"].ToString();
                }

                Session.Add("Dtsku_Price", Dtsku_Price);
            }
        }
    }

    #endregion

    #region Grid Operations

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];

        RowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;

        if (RbUnitType.SelectedValue == "0")
        {
            txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
            txtRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
            txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        }
        else
        {
            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text + "'");

            int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
            int qty = Convert.ToInt32(GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text);
            txtQuantity.Text = Convert.ToString(qty / mPackSize);
            qty=Convert.ToInt32(txtQuantity.Text); 

            txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
            decimal amount= Convert.ToDecimal(GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text);
            txtRate.Text =Convert.ToString(amount / qty);
            txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        }



        txtQuantity.Focus();
        btnSave.Text = "Update";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }

    #endregion

    #region Click OPerations

   //Grid Operation
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");

        int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

        if (decimal.Parse(dc.chkNull_0(lblMaxStock.Text)) >= decimal.Parse(dc.chkNull_0(txtQuantity.Text)))
        {
            if (foundRows.Length > 0)
            {
                if (btnSave.Text == "Add")
                {
                    if (CheckDublicateSKU())
                    {

                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];

                        dr["SKU_Name"] = ddlSKuCde.SelectedItem.Text;



                        if (RbUnitType.SelectedValue == "0")
                        {
                            dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));

                            dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                            dr["QUANTITY_CTN"] = 0;

                            dr["AMOUNT"] = decimal.Parse(txtRate.Text) * Convert.ToInt32(dr["Quantity"]);
                        }
                        else
                        {
                            dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;

                            dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                            decimal rate = Convert.ToDecimal(txtRate.Text);

                            decimal qty = Convert.ToDecimal(dr["Quantity"]);

                            dr["PRICE"] = Convert.ToDecimal((txtAmount.Text)) / (qty);

                            dr["AMOUNT"] = Convert.ToDecimal(txtAmount.Text.ToString());
                        }


                        PurchaseSKU.Rows.Add(dr);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                        return;
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                   // dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                    dr["SKU_Name"] = ddlSKuCde.SelectedItem.Text;

                    if (RbUnitType.SelectedValue == "0")
                    {
                        dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtRate.Text));

                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        dr["AMOUNT"] = decimal.Parse(txtRate.Text) * Convert.ToInt32(dr["Quantity"]);
                    }
                    else
                    {
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;

                        decimal rate = Convert.ToDecimal(txtRate.Text);

                        decimal qty = Convert.ToDecimal(dr["Quantity"]);

                        dr["PRICE"] = Convert.ToDecimal((txtAmount.Text)) / (qty);

                        dr["AMOUNT"] = Convert.ToDecimal(txtAmount.Text.ToString());


                    }


                }
                Session.Add("PurchaseSKU", PurchaseSKU);
                ClearAll();
                LoadGird();
                DisAbaleOption(true);
                ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong SKU Select');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ddlSKuCde.SelectedItem.Text +  " Max Stock is " + lblMaxStock.Text + "');", true);

        }
    }
   
    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];

        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            if (CalculatePurchase(DateTime.Parse(Session["CurrentWorkDate"].ToString())))
            {
                PurchaseSKU = (DataTable)Session["PurchaseSKU"];
                PurchaseSKU.Rows.Clear();
                Session.Add("PurchaseSKU", PurchaseSKU);
                LoadGird();
                GetDocumentNo();

                ClearAll();

                ClearMaster();
                DisAbaleOption(false);

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record save successfully');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DisAbaleOption(false);
        CreatTable();
        LoadGird();
        ClearAll();
        txtRefNo.Text = "";
        txtExchangeRate.Text = "";
    }

    #endregion

    #region Index/Change

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            CreatTable();
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            ClearAll();
            ClearMaster();
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            RbUnitType.Enabled = true;
        }
        else
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            RbUnitType.Enabled = false;

            LoadDocumentDetail();
            LoadSKUDetail();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        GetDocumentNo();
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    /// 
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        LoadSKUDetail();
    }
    protected void ddlSkuCde_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
       
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            lblMinStock.Text = foundRows[0]["MIN_STOCK"].ToString();

            lblMaxStock.Text = foundRows[0]["MAX_STOCK"].ToString();
        }
        txtQuantity.Focus();
    }    
    #endregion

    
    private bool CalculatePurchase(DateTime CWorkDate)
    {
        bool flag = false;
        hfClosingDate.Value = Convert.ToString(CWorkDate);
        decimal mTotalAmount = 0;

        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];

        foreach (DataRow dr in dtPurchaseDetail.Rows)
        {
            mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

        }

        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            if (!IsBillBookNoExist())
            {
                long P_ID = mController.InsertPurchaseOrder(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), txtRefNo.Text, int.Parse(RbUnitType.SelectedValue)
                , Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)), CWorkDate, mTotalAmount, int.Parse(Session["UserId"].ToString()), dtPurchaseDetail
                , txtCreditTerm.Text,decimal.Parse(dc.chkNull_0(txtNetAmount.Text)), decimal.Parse(dc.chkNull_0(txtgst.Text)), decimal.Parse(dc.chkNull_0(txtGstValue.Text)));

                if (P_ID > 0)
                {
                    flag = true;
                    Print(P_ID);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('This Ref No already exist,Kindly enter different Ref No');", true);
            }
        }

        else
        {
           flag= mController.UpdatePurchaseOrderDetail(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtRefNo.Text, Convert.ToInt32(ddlCurrency.SelectedValue)
            , mTotalAmount, Convert.ToDecimal(dc.chkNull_0(txtExchangeRate.Text)), int.Parse(Session["UserId"].ToString()), dtPurchaseDetail
             , txtCreditTerm.Text,decimal.Parse(dc.chkNull_0(txtNetAmount.Text)), decimal.Parse(dc.chkNull_0(txtgst.Text)), decimal.Parse(dc.chkNull_0(txtGstValue.Text)));

            if (Convert.ToInt64(drpDocumentNo.SelectedValue) != Constants.LongNullValue)
            {
               
                Print(Convert.ToInt64(drpDocumentNo.SelectedValue));
            }

        }
        return flag;
    }

    private void DisAbaleOption(bool IsDisable)
    {
        if (IsDisable == true)
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            drpDocumentNo.Enabled = false;
            RbUnitType.Enabled = false;
            txtRefNo.Enabled = false;
            ddlCurrency.Enabled = false;
            txtExchangeRate.Enabled = false;
        }
        else
        {
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            drpDocumentNo.Enabled = true;
            drpDocumentNo.SelectedIndex = 0;
            txtRefNo.Enabled = true;
            ddlCurrency.Enabled = true;
            txtExchangeRate.Enabled = true;
            RbUnitType.Enabled = true;
        }
    }

    private void ClearAll()
    {
        
        txtQuantity.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "0.00";
         
        btnSave.Text = "Add";
    }

    private void ClearMaster()
    {
        txtExchangeRate.Text = "";
        txtRefNo.Text = "";
        txtNetAmount.Text = "";
        txtCreditTerm.Text = "";
        txtgst.Text = "";
        txtGstValue.Text = "0";
        
    }

    private void Print(long mResult)
    {

        RptInventoryController _rptInventoryCtl = new RptInventoryController();
        DocumentPrintController _dPrint = new DocumentPrintController();

        var crpReport = new SAMSBusinessLayer.Reports.CrpPurchaseOrderGst();

        DataTable dt = _dPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue));
        DataSet ds = _rptInventoryCtl.SelectPurchaseDocument(mResult, int.Parse(drpDistributor.SelectedValue), Constants.IntNullValue, DateTime.Parse(hfClosingDate.Value), DateTime.Parse(hfClosingDate.Value),1);

        crpReport.SetDataSource(ds);
        crpReport.Refresh();

        crpReport.SetParameterValue("companyName", dt.Rows[0]["COMPANY_NAME"].ToString().ToUpper());
        crpReport.SetParameterValue("companyAddress", dt.Rows[0]["ADDRESS1"].ToString());
        

        Session.Add("CrpReport", crpReport);
        Session.Add("ReportType", 0);
        const string url = "'Default.aspx'";
        const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);


    }
}