using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.Collections.Generic;

/// <summary>
/// Form To Take Order, Invoice And Sale Return(Step2)
/// </summary>
public partial class Forms_frmDamage_Replacement : System.Web.UI.Page
{
    #region Variables

    DataTable PurchaseSKU;
    DataTable dtFreeSKU;
    private static int mCustomerTypeId;
    private static int mCustomerVolClassId;
    private int mTownId;
    private static int RowId;
    private static int UnitType;
    private static int OrderNo;

    #endregion

    /// <summary>
    /// Page_Load Function Populates All Combos, Grids And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();                                      
            ScriptManager.GetCurrent(Page).SetFocus(txtOutletCode);            
            this.LoadCustomerData();
            this.LoadSKUDetail();
            this.CreatTable();
            this.CreateFreeSKU();
            this.LoadFreeGrid();
            this.LoadDamageReplacemnt();
            this.LoadBatchNo();            
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
        }
    }

    /// <summary>
    /// Creates Datatable For Order, Invoice And Sale Return
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("DistributorId", typeof(int));
        PurchaseSKU.Columns.Add("SALE_ORDER_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("GST_ON",typeof(string));
        PurchaseSKU.Columns.Add("ExpDate",typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("RETAIL_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_EXTRA_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("SED_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(decimal));
        PurchaseSKU.Columns.Add("IS_DELETED", typeof(bool));
        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }

    /// <summary>
    /// Creates Datatable For Free SKU
    /// </summary>
    private void CreateFreeSKU()
    {
        dtFreeSKU = new DataTable();
        dtFreeSKU.Columns.Add("SKU_ID", typeof(int));
        dtFreeSKU.Columns.Add("SKU_Code", typeof(string));
        dtFreeSKU.Columns.Add("SKU_Name", typeof(string));
        dtFreeSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        dtFreeSKU.Columns.Add("Quantity", typeof(int));
        dtFreeSKU.Columns.Add("AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_RATE", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("PROMOTION_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_DETAIL_ID", typeof(int));
        dtFreeSKU.Columns.Add("PROMOTION_OFFER_ID", typeof(int));
        this.Session.Add("dtFreeSKU", dtFreeSKU);
    }

    /// <summary>
    /// Loads Pending Order Nos To Docuemnt No Combo
    /// </summary>
    private void LoadDamageReplacemnt()
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        int PrincipalID = Constants.IntNullValue;
        if (drpTransactionType.SelectedValue == "1")
        {
            PrincipalID = 1;
        }
        else if (drpTransactionType.SelectedValue == "2")
        {
            PrincipalID = 2;
        }
        else if (drpTransactionType.SelectedValue == "3")
        {
            PrincipalID = 0;
        }
        DataTable dtOrder = or.SelectDamageReplacment(Convert.ToInt32(ddlStore.SelectedValue), Constants.IntNullValue, PrincipalID, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 0, int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(this.Session["CurrentWorkDate"]));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, 0, 1);
       
        this.Session.Add("dtOrder", dtOrder);
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (long.Parse(drpDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            txtBillBookNo.Text = string.Empty;

            DataTable dt = (DataTable)this.Session["dtOrder"];
            DataRow[] foundRows = dt.Select("SALES_RETURN_ID   = '" + drpDocumentNo.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
              //  txtGrossAmount.Text = foundRows[0]["TOTAL_AMOUNT"].ToString();
               // numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                //numTxtTotalStndrdDiscnt.Text = foundRows[0]["STANDARD_DISCOUNT_AMOUNT"].ToString();
               // numTxtTotalGST.Text = foundRows[0]["GST_AMOUNT"].ToString();
               // numTxtTotlAmnt.Text = foundRows[0]["TOTAL_NET_AMOUNT"].ToString();
               // numTxtTotalTST.Text = foundRows[0]["TST_AMOUNT"].ToString();
                txtOutletCode.Text = foundRows[0]["CUSTOMER_CODE"].ToString();
                txtOutletName.Text = foundRows[0]["CUSTOMER_NAME"].ToString();
              //  numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
              //  numtxtUnClaimabledist.Text = foundRows[0]["SED_AMOUNT"].ToString();
                this.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
                this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
               // txtBillBookNo.Text = foundRows[0]["MANUAL_ORDER_ID"].ToString();
               // this.Session.Add("hfBillBookNo", foundRows[0]["MANUAL_ORDER_ID"].ToString());
                EnableDisableController(false);               
                //btnCalculate.Enabled = false;
                this.ClearAll();
                ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
               // //RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
            }
        }
        else
        {
            this.CreateFreeSKU();
            this.CreatTable();
            this.LoadGird();
            this.LoadFreeGrid();
            ClearMasterALL();
           this.Session.Remove("hfBillBookNo");
        }
    }

    /// <summary>
    /// Loads Customers To Customer ListBox
    /// </summary>
    private void LoadCustomerData()
    {
        if (ddlStore.Items.Count > 0)
        {
            ListCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(ddlStore.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            clsWebFormUtil.FillListBox(this.ListCustomer, dtCustomer, "CUSTOMER_DETAIL", "CUSTOMER_DETAIL", true);
            this.Session.Add("dtCustomer", dtCustomer);
        }
        else
        {
            ListCustomer.Items.Clear();
        }
    }

    /// <summary>
    /// Loads SKU Data To SKU ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(1, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlStore.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSKuCde, Dtsku_Price, 0, 10, true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }
    
    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }

    /// <summary>
    /// Sets Order SKU For Edit. This Function Runs When An Existing Order SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
      //  txtskuCode.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        hfBatchValue.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtExpDate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text;
        if (UnitType == 0)
        {
            txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        }
        else
        {
            txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[8].Text;
        }
        //txtskuCode.Enabled = false;
        hfSKUID.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        btnSave.Text = "Update SKU";
    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird();
      //  btnCalculate.Enabled = true;        
    }
    
    /// <summary>
    /// Loads Free SKUS To SKU Grid
    /// </summary>
    private void LoadFreeGrid()
    {
        dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
      
    }

    /// <summary>
    /// Verifies Customer Code
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool FindCustomer()
    {
        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];
        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_CODE  = '" + txtOutletCode.Text.Trim() + "'");
        if (foundRows.Length > 0)
        {
            this.Session.Add("CUSTOMER_ID", long.Parse(foundRows[0]["CUSTOMER_ID"].ToString()));
            mCustomerTypeId = int.Parse(foundRows[0]["CHANNEL_TYPE_ID"].ToString());
            mCustomerVolClassId = int.Parse(foundRows[0]["VOLUME_CLASS_ID"].ToString());
            mTownId = int.Parse(foundRows[0]["TOWN_ID"].ToString());
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool CheckDublicateSKU()
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" +ddlSKuCde.SelectedValue + "' AND BATCH_NO = '" + hfBatchValue.Value + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks Free SKU in Free SKU Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private int FreeSKUExist(DataTable dt, int Sku_id)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (int.Parse(dt.Rows[i]["SKU_ID"].ToString()) == Sku_id)
            {
                return i;
            }

        }
        return -1;
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="OrderId">Order</param>
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.SelectDamageReplacmentDetail(Constants.IntNullValue, OrderId);
        //dtFreeSKU = ord.SelectOrderPromotion(Convert.ToInt32(ddlStore.SelectedValue), OrderId);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
       // this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
    }

    /// <summary>
    /// Checks Bill Book No in System
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private bool IsBillBookNoExist()
    {
        bool flag = false;
        //if ((long.Parse(drpDocumentNo.SelectedValue) == Constants.LongNullValue || this.Session["hfBillBookNo"].ToString() != txtBillBookNo.Text) && txtBillBookNo.Text.Trim().Length > 0)
        //{
        //    OrderEntryController OEC = new OrderEntryController();
        //    DataTable dtBillBookNo = OEC.SelectBillBookNo(Convert.ToInt32(ddlStore.SelectedValue), txtBillBookNo.Text, 0);
        //    if (dtBillBookNo.Rows.Count > 0)
        //    {
        //        flag = true;
        //    }
        //    DataTable dtBillBookNo2 = OEC.SelectBillBookNo(Convert.ToInt32(ddlStore.SelectedValue), txtBillBookNo.Text, 1);
        //    if (dtBillBookNo2.Rows.Count > 0)
        //    {
        //        flag = true;
        //    }
        //}
        return flag;
    }
    
    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="CValue">Value</param>
    private void EnableDisableController(bool CValue)
    {
         
        if (CValue == true)
        {
            
            {               
                //txtOutletCode.Enabled = true;
                //txtOutletName.Enabled = true;
                //ddlStore.Enabled = true;
            }
        }
        
    }
   
    /// <summary>
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" +ddlSKuCde.SelectedValue + "'");
        decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));

        int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));

        if (btnSave.Text == "Add Sku")
        {
            if (CheckDublicateSKU())
            {
                DataRow dr = PurchaseSKU.NewRow();
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["BATCH_NO"] = hfBatchValue.Value;

                if (txtExpDate.Text == "")
                {
                    dr["ExpDate"] = this.Session["CurrentWorkDate"].ToString();
                }
                else
                {
                    dr["ExpDate"] = txtExpDate.Text;
                }

                if (UnitType == 0)
                {
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["QUANTITY_CTN"] = 0;

                }
                else
                {
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;
                    dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                }
                dr["UNIT_PRICE"] = mTradePrice.ToString();

                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                    dr["TST_AMOUNT"] = 0;
                    dr["GST_ON"] = "T";
                }
                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["GST_RATE"] = 0;
                    dr["GST_ON"] = "R";

                }
                else
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["TST_AMOUNT"] = 0;
                    dr["GST_RATE"] = 0;
                    dr["GST_ON"] = "E";
                }


                dr["STANDARD_DISCOUNT"] = 0;
                dr["EXTRA_DISCOUNT"] = 0;
                dr["GST_AMOUNT"] = 0;
                dr["NET_AMOUNT"] = 0;
                dr["SED_AMOUNT"] = 0;
                PurchaseSKU.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = PurchaseSKU.Rows[RowId];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["BATCH_NO"] = hfBatchValue.Value;
            if (txtExpDate.Text == "")
            {
                dr["ExpDate"] = this.Session["CurrentWorkDate"].ToString();
            }
            else
            {
                dr["ExpDate"] = txtExpDate.Text;
            }
            if (UnitType == 0)
            {
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
            }
            else
            {
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;
            }
            dr["UNIT_PRICE"] = mTradePrice.ToString();
            dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());

            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
            {
                dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["TST_AMOUNT"] = 0;
                dr["GST_ON"] = "T";

            }
            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["GST_RATE"] = 0;
                dr["GST_ON"] = "R";
            }
            else
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["TST_AMOUNT"] = 0;
                dr["GST_RATE"] = 0;
                dr["GST_ON"] = "E";
            }

            dr["STANDARD_DISCOUNT"] = 0;
            dr["EXTRA_DISCOUNT"] = 0;
            dr["GST_AMOUNT"] = 0;
            dr["NET_AMOUNT"] = 0;
            dr["STANDARD_DISCOUNT_PER"] = 0;
            dr["SED_AMOUNT"] = 0;
        }

        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.EnableDisableController(false);
        this.LoadGird();
        this.ClearAll();
        ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
        if (ChbDiscount.Checked == false)
        {
            decimal ObjGrossSales = 0;
            decimal ObjTaxableAmount = 0;
            foreach (DataRow dr in PurchaseSKU.Rows)
            {
                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                if (dr["GST_ON"].ToString() == "T")
                {
                    ObjTaxableAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                }

            }
          //  txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
            numTxtTotalSED.Text = Convert.ToString(Math.Round(ObjTaxableAmount, 2));
        }
    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        
        if (FindCustomer())
        {
            string ManualID = null;
            if (txtBillBookNo.Text.Trim().Length > 0)
            {
                ManualID = txtBillBookNo.Text.ToUpper();
            }
            DataControl DC = new DataControl();
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
            OrderEntryController mOrderController = new OrderEntryController();
            bool IsValidInsert = true;
            int PrincipalID = Constants.IntNullValue;
            if (drpTransactionType.SelectedValue == "1")
            {
                PrincipalID = 1;
            }
            else if (drpTransactionType.SelectedValue == "2")
            {
                PrincipalID = 2;
            }
            else if (drpTransactionType.SelectedValue == "3")
            {
                PrincipalID = 0;
            }
            if (drpDocumentNo.SelectedValue == Constants.LongNullValue.ToString())  //0 for sales return PrincipalID = 1 for damage   and 2 fro replacment 
            {
                IsValidInsert = mOrderController.Add_Damage(Convert.ToInt32(ddlStore.SelectedValue), mTownId, 0, PrincipalID, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), 0, 0, long.Parse(drpDocumentNo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), 0, 0);
            }
            else
            {
                IsValidInsert = mOrderController.UpdateDamage(Convert.ToInt64(drpDocumentNo.SelectedValue), Convert.ToInt32(ddlStore.SelectedValue), mTownId, 0, PrincipalID, long.Parse(this.Session["CUSTOMER_ID"].ToString()), long.Parse(this.Session["CUSTOMER_ID"].ToString()), 0, 0, long.Parse(drpDocumentNo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), 0, 0);
            }
                
                if (IsValidInsert)
                {
                    this.LoadDamageReplacemnt();
                    this.ClearMasterALL();
                }
            }
        }
    
    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {
       // txtskuCode.Text = "";
       // txtskuName.Text = "";
        txtUnitRate.Text = "";
        txtQuantity.Text = "";
        txtExpDate.Text = "";
       // lblInBatch.Text = "0";
       // lblInStore.Text = "0";
        btnSave.Text = "Add Sku";        
        //btnCalculate.Enabled = true;
       // txtskuCode.Enabled = true;

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        this.EnableDisableController(true);
        this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtFreeSKU");
        this.CreatTable();
        this.CreateFreeSKU();
        txtOutletCode.Text = "";
        txtOutletName.Text = "";
        this.LoadGird();
        this.LoadFreeGrid();       
        numTxtTotalSED.Text = "";
        numTxtTotalSED.Text = "";
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;
        drpDocumentNo.SelectedIndex = 0;
        txtBillBookNo.Text = string.Empty;        
    }

    /// <summary>
    /// Loads Batch Nos
    /// </summary>
    private void LoadBatchNo()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable dtBatchNo = PController.GetBatchNo(Convert.ToInt32(ddlStore.SelectedValue), 0);
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

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        
        clsWebFormUtil.FillDropDownList(this.ddlStore, dt, 0, 2, true);
    }

    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();        
        this.LoadFreeGrid();
        this.LoadDamageReplacemnt();
        this.LoadBatchNo();
        this.LoadCustomerData();
    }

    protected void drpTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpTransactionType.SelectedValue == "1")
        {
            btnSaveOrder.ToolTip = "Save Damage";

            ddlBatch.Enabled = false;
            Label1.Visible = false;           
           

        }
        else if (drpTransactionType.SelectedValue == "2")
        {
            btnSaveOrder.ToolTip = "Save Replacment";
            ddlBatch.Enabled = true;
            Label1.Visible = true;            
        }
        else if (drpTransactionType.SelectedValue == "3") 
        {
            btnSaveOrder.ToolTip = "Save Sale Return";
            ddlBatch.Enabled = true;
            Label1.Visible = true;            
        }
        this.LoadDamageReplacemnt();
        this.CreateFreeSKU();
        this.CreatTable();
        this.LoadGird();
        this.LoadFreeGrid();
        ClearMasterALL();
        this.Session.Remove("hfBillBookNo");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
        this.Session.Remove("PurchaseSKU");
        

    }
}
