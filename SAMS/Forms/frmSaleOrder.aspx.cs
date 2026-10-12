using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Reports;

public partial class Forms_frmSaleOrder : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();

    private void loadOrder(long OrderId)
    { 
        DataTable dtOrder = oeCtrl.SelectPendingOrder2(Convert.ToInt32(ddlDistributor.SelectedValue), OrderId, Constants.Order_Pending_Id, 0, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
        DataRow[] foundRows = dtOrder.Select("SALE_ORDER_ID  = '" + OrderId + "'");

        if (foundRows.Length > 0)
        {
            ddlDistributor.SelectedValue= foundRows[0]["DISTRIBUTOR_ID"].ToString(); 

            ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
            CustomerLedger();
            LoadContactInformation();

            ddlContactPerson.SelectedValue= foundRows[0]["ORDERBOOKER_ID"].ToString();

            DataTable dtAddress = (DataTable)Session["dtCustomer"];

            DataRow[] foundRows2 = dtAddress.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

            if (foundRows2.Length > 0)
            {
                lblAdress.Text = "Address";

                lblCustomerAddress.Text = foundRows2[0]["ADDRESS"].ToString();
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
            }

            ExistenOrderDetail(Convert.ToInt64(Session["SaleOrderId"]));
            txtRemarks.Text = foundRows[0]["remarks"].ToString();

            EnableDisableController(false);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreateTable();
            LoadDistributors();
            LoadCustomers();
           
            if (Session["SaleOrderId"] != null)
            {
                loadOrder(Convert.ToInt64(Session["SaleOrderId"]));
            }
            else
            {
                CustomerLedger();
                LoadContactInformation();
            }
            LoadSKUDetail();

        }
    }

    private void LoadDistributors()
    {
        DistributorController distCtrl = new DistributorController();
        DataTable dt = distCtrl.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                clsWebFormUtil.FillDropDownList(ddlDistributor, dt, 0, 2);
            }
        }
    }

    private void LoadContactInformation()
    {
        ddlContactPerson.Items.Clear();
        if (ddlCustomer.Items.Count > 0 && ddlDistributor.Items.Count > 0)
        {
            DataTable dtContact = _customerCtrl.GetCustomerContact(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt64(ddlCustomer.SelectedValue), Convert.ToInt32(Constants.IntNullValue));
            if (dtContact.Rows.Count > 0)
            {
                clsWebFormUtil.FillDropDownList(ddlContactPerson, dtContact, 0, 4, true);
            }
        }
    }
    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomers();
        CustomerLedger();
        LoadContactInformation();
        LoadSKUDetail();
        ddlCustomer.Focus();
    }

    private void LoadCustomers()
    {
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer(int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), 0, Constants.IntNullValue);
            if (dtCustomer.Rows.Count > 0)
            {
                lblCustomerAddress.Text = dtCustomer.Rows[0]["ADDRESS"].ToString();
                if (lblCustomerAddress.Text == "")
                {
                    lblCustomerAddress.Text = "N/A";
                }
                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL");

                Session.Add("dtCustomer", dtCustomer);
            }
        }
    }
    private void CustomerLedger()
    {
        if (ddlCustomer.Items.Count > 0)
        {
            decimal Opening = 0;


          
            DataTable dt = _customerCtrl.SelectCustomerCreditBalance(long.Parse(ddlCustomer.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlDistributor.SelectedValue), Constants.Advance_PaymentOrder_id);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Opening = Opening + decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString()));
                    //  Opening = decimal.Parse(_dc.chkNull_0(dt.Rows[0][0].ToString()));

                    lblLedgerBalance.Text = string.Format("{0:0,0.00}", Opening);
                }
                else
                {
                    lblLedgerBalance.Text = Convert.ToString(Opening);
                }
            }
            else
            {
                lblLedgerBalance.Text = Convert.ToString(Opening);
            }
           
        }
    }
    
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomerLedger();

        DataTable dt = (DataTable)Session["dtCustomer"];
        DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

        if (foundRows.Length > 0)
        {
            lblAdress.Text = "Address";

            lblCustomerAddress.Text = foundRows[0]["ADDRESS"].ToString();
            if (lblCustomerAddress.Text == "")
            {
                lblCustomerAddress.Text = "N/A";
            }
        }
        LoadContactInformation();

        LoadSKUDetail();
    }
  

    #region Detail Panel

    DataTable orderSku = new DataTable();
    private static int RowNo;

    private void LoadSKUDetail()
    {
        //if (ddlCustomer.Items.Count > 0)
        //{

        //    SKUPriceDetailController PController = new SKUPriceDetailController();
        //    DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        //    clsWebFormUtil.FillDropDownList(ddlSKus, Dtsku_Price, "SKU_ID", "SkuDetail2", true);
        //    Session.Add("Dtsku_Price", Dtsku_Price);
        //}
        if (ddlCustomer.Items.Count > 0)
        {
            if (ddlCustomer.SelectedItem.Text.Contains("|"))
            {
                string[] Customer = ddlCustomer.SelectedItem.Text.Split('|');
                if (Customer[1].ToString().Trim() == "Default")
                {
                    SKUPriceDetailController PController = new SKUPriceDetailController();
                    DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                    clsWebFormUtil.FillDropDownList(ddlSKus, Dtsku_Price, "SKU_ID", "SkuDetail2", true);
                    Session.Add("Dtsku_Price", Dtsku_Price);
                }
                else
                {
                    SKUPriceDetailController PController = new SKUPriceDetailController();
                    DataTable Dtsku_Price = PController.GetCustomerSKU(int.Parse(ddlDistributor.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue));
                    clsWebFormUtil.FillDropDownList(ddlSKus, Dtsku_Price, "SKU_ID", "SkuDetail2", true);
                    Session.Add("Dtsku_Price", Dtsku_Price);
                }
            }
        }
    }
    private void CreateTable()
    {
        orderSku = new DataTable();
        orderSku.Columns.Add("orderDetailId", typeof(long));
        orderSku.Columns.Add("skuId", typeof(int));
        orderSku.Columns.Add("skuName", typeof(string));
        orderSku.Columns.Add("price", typeof(decimal));
        orderSku.Columns.Add("qty", typeof(int));
        orderSku.Columns.Add("amount", typeof(decimal));

        Session.Add("orderSku", orderSku);

    }

    private bool CheckDublicateSKU()
    {

        orderSku = (DataTable)Session["orderSku"];
        DataRow[] foundRows = orderSku.Select("skuId  = '" + ddlSKus.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }

        return false;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];


            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKus.SelectedValue + "'");
           
            if (decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) > 0)
            {
                if (foundRows.Length > 0)
                {
                    orderSku = (DataTable)Session["orderSku"];
                    if (btnSave.Text == "Add")
                    {
                        if (CheckDublicateSKU())
                        {

                            DataRow dr = orderSku.NewRow();
                            dr["skuId"] = foundRows[0]["SKU_ID"];
                            dr["skuName"] = foundRows[0]["SKU_NAME"]; //AS SKUnMAE
                            dr["price"] = foundRows[0]["TRADE_PRICE"];
                            dr["qty"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));

                            orderSku.Rows.Add(dr);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKus.SelectedItem.Text + " Already Exists ');", true);

                            return;
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {

                        DataRow dr = orderSku.Rows[RowNo];
                        dr["skuId"] = foundRows[0]["SKU_ID"];
                        dr["skuName"] = foundRows[0]["SKU_NAME"];
                        dr["price"] = foundRows[0]["TRADE_PRICE"];
                        dr["qty"] = int.Parse(txtQuantity.Text);
                        dr["amount"] = decimal.Parse(_dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(_dc.chkNull_0(txtQuantity.Text));

                    }
                    Session.Add("orderSku", orderSku);
                    ClearDetail();
                    LoadGird();
                    EnableDisableController(false);
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSKus);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Item Select');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please enter Item Price!');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
        }
    }

    protected void grdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;

        ddlSKus.SelectedValue = grdOrder.Rows[RowNo].Cells[0].Text;
        txtQuantity.Text = grdOrder.Rows[RowNo].Cells[3].Text;

        ddlSKus.Enabled = false;
        txtQuantity.Focus();
        btnSave.Text = "Update";

    }
    protected void grdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        orderSku = (DataTable)Session["orderSku"];
        if (orderSku.Rows.Count > 0)
        {
            orderSku.Rows.RemoveAt(e.RowIndex);
            Session.Add("orderSku", orderSku);
            LoadGird();
            if (orderSku.Rows.Count == 0)
            {
                EnableDisableController(true);
            }
        }
        else if (orderSku.Rows.Count == 0)
        {
            EnableDisableController(true);
        }

    }

    private void LoadGird()
    {
        int TotalValue = 0;
        decimal TotalAmount = 0;
        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

        foreach (DataRow dr in orderSku.Rows)
        {
            TotalValue += int.Parse(dr["qty"].ToString());
            TotalAmount += decimal.Parse(dr["amount"].ToString());

        }
        txtTotalQty.Text = TotalValue.ToString();
        txtTotalAmount.Text = string.Format("{0:0.00}", TotalAmount);
    }
    private void ClearDetail()
    {

        txtQuantity.Text = "";

        ddlSKus.Enabled = true;
        btnSave.Text = "Add";

    }
    #endregion

    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        try
        {
            orderSku = (DataTable)Session["orderSku"];
            if (orderSku.Rows.Count > 0)
            {
                decimal mTotalAmount = 0;
                int mTotalQty = 0;
                foreach (DataRow dr in orderSku.Rows)
                {
                    mTotalAmount += decimal.Parse(dr["amount"].ToString());
                    mTotalQty += int.Parse(dr["qty"].ToString());
                }
              


                if (Session["SaleOrderId"]==null)
                {
                    long sId = oeCtrl.AddOrder(int.Parse(ddlDistributor.SelectedValue), "", 0, long.Parse(ddlCustomer.SelectedValue), int.Parse(ddlContactPerson.SelectedValue)
                    , 214, mTotalAmount, mTotalQty, Constants.Order_Pending_Id, orderSku, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtRemarks.Text);
                    if (sId > 0)
                    {
                        
                       // LoadPendingOrder();
                      //  ClearMasterALL();
                       
                        Response.Redirect("frmSaleOrderView1.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26");
   
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
                    }
                }
                else
                {
                    bool flag = oeCtrl.UpdateOrder(long.Parse(Session["SaleOrderId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                        int.Parse(ddlContactPerson.SelectedValue), int.Parse(ddlDistributor.SelectedValue), 214, "",
                        long.Parse(ddlCustomer.SelectedValue), mTotalAmount, mTotalQty, orderSku, int.Parse(Session["UserId"].ToString()), txtRemarks.Text);

                    if (flag)
                    {
                       
                        Session.Remove("SaleOrderId");
                        Response.Redirect("frmSaleOrderView1.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26");
   
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

       // CreateTable();
        //LoadGird();
        ClearDetail();
        Session.Remove("SaleOrderId");
        Response.Redirect("frmSaleOrderView1.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=26");
   
    }
    private void GetOrder(long sId)
    {
        try
        {
            DocumentPrintController DPrint = new DocumentPrintController();


            DataTable dt = DPrint.SelectReportTitle(int.Parse(ddlDistributor.SelectedValue));


            DataSet ds = null;

            ds = oeCtrl.SelectOrderforView(sId, 0);


            //if (ds.Rows.Count <= 0) return;

            CrpOrderPrinting crpOrder = new CrpOrderPrinting();
            crpOrder.SetDataSource(ds);
            crpOrder.Refresh();

            crpOrder.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString().ToUpper());
            crpOrder.SetParameterValue("CompanyAddress", dt.Rows[0]["ADDRESS1"].ToString());
            crpOrder.SetParameterValue("ContactNo", dt.Rows[0]["CONTACT_NUMBER"].ToString());
            //---------------------------------------------------------------------------------\\

            //---------------------------------------------------------------------------------\\


            //Session.Add("CrpReport", crpOrder);
            // Session.Add("ReportType", 0);

            // const string url = "'Default.aspx'";
            // Response.Write("<script>window.open(" + url + ",'_blank');</script>");
            Session.Add("CrpReport", crpOrder);
            Session.Add("ReportType", 0);
            const string url = "'Default.aspx'";
            const string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);


        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    #region Update Panel
    //private void LoadPendingOrder()
    //{
    //    drpDocumentNo.Items.Clear();
    //    DataTable dtOrder = oeCtrl.SelectPendingOrder2(Convert.ToInt32(ddlDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.Order_Pending_Id, 0, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
    //    drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
    //    clsWebFormUtil.FillDropDownList(drpDocumentNo, dtOrder, 0, 1);
    //    Session.Add("dtOrder", dtOrder);
    //}
    private void ExistenOrderDetail(long OrderId)
    {
        orderSku = oeCtrl.SelectOrderDetail(Convert.ToInt32(ddlDistributor.SelectedValue), OrderId, Constants.DateNullValue, 0);
        Session.Add("orderSku", orderSku);
        LoadGird();

    }
    //protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (long.Parse(drpDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
    //    {
    //        DataTable dt = (DataTable)Session["dtOrder"];
    //        DataRow[] foundRows = dt.Select("SALE_ORDER_ID  = '" + drpDocumentNo.SelectedValue + "'");
    //        if (foundRows.Length > 0)
    //        {
    //            ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
    //            rdbCustomerAddress.SelectedValue=foundRows[0]["MANUAL_ORDER_ID"].ToString();
    //            CustomerLedger();
    //            LoadSKUDetail();
    //            if (rdbCustomerAddress.SelectedValue == "2")
    //            {
    //                CustomerAddress();
    //                rdbSubDealers.SelectedValue = foundRows[0]["DELIVERYMAN_ID"].ToString();
    //            }
    //            else
    //            {
    //                DataTable dtAddress = (DataTable)Session["dtCustomer"];

    //                DataRow[] foundRows2 = dtAddress.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");

    //                if (foundRows2.Length > 0)
    //                {
    //                    lblAdress.Text = "Address";
    //                    if (rdbCustomerAddress.SelectedValue == "0")
    //                    {
    //                        lblCustomerAddress.Text = foundRows2[0]["ShowRoomAddress"].ToString();
    //                        if (lblCustomerAddress.Text == "")
    //                        {
    //                            lblCustomerAddress.Text = "N/A";
    //                        }
    //                    }
    //                    else if (rdbCustomerAddress.SelectedValue == "1")
    //                    {
    //                        lblCustomerAddress.Text = foundRows2[0]["WareHouseAddress"].ToString();
    //                        if (lblCustomerAddress.Text == "")
    //                        {
    //                            lblCustomerAddress.Text = "N/A";
    //                        }
    //                    }
    //                }
    //            }
    //            ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
    //            txtRemarks.Text = foundRows[0]["remarks"].ToString();

    //            EnableDisableController(false);
    //        }
    //    }
    //    else
    //    {
    //        CreateTable();
    //        LoadGird();
    //        ClearMasterALL();
    //    }
    //}
    private void EnableDisableController(bool CValue)
    {
        if (CValue == true)
        {
            ddlCustomer.Enabled = true;
            ddlDistributor.Enabled = true;
        }
        else
        {
            ddlCustomer.Enabled = false;
            ddlDistributor.Enabled = false;
        }
    }
    private void ClearMasterALL()
    {
        EnableDisableController(true);
        Session.Remove("orderSku");
        CreateTable();

        LoadGird();
        txtRemarks.Text = "";

        RowNo = 0;
       
        ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
    }

    #endregion
}