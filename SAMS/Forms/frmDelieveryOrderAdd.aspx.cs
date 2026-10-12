using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommonLayer.Classes;
using System.Collections;

public partial class Forms_frmDelieveryOrderAdd : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();


    private static int _mCustomerTypeId;
    private static int _mCustomerVolClassId;

    DataTable _dtFreeSku;

    private void loadInvoice()
    {
        var id = Session["invoiceId"].ToString().Split('-');
        lblDONo.Text = "DO No: " + id[1];
        lblOrderNo.Text = "Order No: " + id[0];
        DataTable dt = oeCtrl.SelectPendingOrder2(Convert.ToInt64(id[1]), 2);
        if (dt.Rows.Count > 0)
        {
            hfOrderQty.Value = dt.Rows[0]["orderQty"].ToString();
            ddlDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();
            txtTotalAmount.Text = string.Format("{0:0.00}",dt.Rows[0]["TOTAL_NET_AMOUNT"]); 
            lblDate.Text = "SO Date: " + Convert.ToDateTime(dt.Rows[0]["SODate"].ToString()).ToString("dd-MM-yyyy"); 
            _mCustomerTypeId = int.Parse(dt.Rows[0]["BUSINESS_TYPE_ID"].ToString());
            _mCustomerVolClassId = int.Parse(dt.Rows[0]["VOLUME_CLASS_ID"].ToString());
            hfIsGstReg.Value = dt.Rows[0]["isGstRegister"].ToString();
            rdbInvoiceType.SelectedValue = dt.Rows[0]["INVOICE_TYPE"].ToString();
            /// <summary>
            /// Customer Deal 
            /// </summary>
            
            DataTable dtCustomer = _customerCtrl.GetCustomerByID(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt64(ddlCustomer.SelectedValue),1);
            if (dtCustomer == null)
            {
              
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Activate Customer first!');", true);
                return;
            }


            hfGst.Value = dt.Rows[0]["GstPage"].ToString();
          // hfAddGst.Value = dt.Rows[0]["AddGstPage"].ToString();
            CustomerLedger();
            lblCustomer.Text = ddlCustomer.SelectedItem.Text;
            //------------------------------------------------------------------------\\

            txtDriverName.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["driverName"].ToString()));
            txtCellNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["cellNo"].ToString()));
            txtTruckNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["truckNo"].ToString()));
            txtBuilty.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["builtyNo"].ToString()));
            rdbDiscountType.SelectedValue = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["freight"].ToString()));
          // if (dt.Rows[0]["discount"] != null)
           // {
              //  hfExtraDiscount.Value = string.Format("{0:0.00}", dt.Rows[0]["discount"]);
           // }
            txtRemarks.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["remarks"].ToString()));

            orderSku = oeCtrl.SelectOrderDetail(Convert.ToInt32(ddlDistributor.SelectedValue), long.Parse(id[1]), long.Parse(id[0]), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 2);
        }   
    }
    private void loadOrder() 
    {
        lblOrderNo.Text = "Order No: " + Session["orderId"].ToString();

        DataTable dt = oeCtrl.SelectPendingOrder2(Convert.ToInt64(Session["orderId"]), 1);
        if (dt.Rows.Count > 0)
        {
            hfOrderQty.Value = dt.Rows[0]["orderQty"].ToString();
            ddlDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            ddlCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();
            lblDate.Text = "SO Date: " + dt.Rows[0]["DOCUMENT_DATE"].ToString();
            txtTotalAmount.Text = string.Format("{0:0.00}",dt.Rows[0]["TOTAL_NET_AMOUNT"]); 
            _mCustomerTypeId = int.Parse(dt.Rows[0]["BUSINESS_TYPE_ID"].ToString());
            _mCustomerVolClassId = int.Parse(dt.Rows[0]["VOLUME_CLASS_ID"].ToString());

            hfIsGstReg.Value = dt.Rows[0]["isGstRegister"].ToString();

            DataTable dtCustomer = _customerCtrl.GetCustomerByID(Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt64(ddlCustomer.SelectedValue),1);
            if (dtCustomer != null)
            {
               // hfZoneId.Value = dtCustomer.Rows[0]["zoneId"].ToString();
                hfGst.Value = dtCustomer.Rows[0]["TAX_RATE"].ToString();
              //  hfAddGst.Value = dtCustomer.Rows[0]["TAX_RATE2"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please Activate Customer first!');", true);
                return;
            }

            CustomerLedger();

            //------------------------------------------------------------------\\
            txtRemarks.Text = dt.Rows[0]["remarks"].ToString();
            lblCustomer.Text = ddlCustomer.SelectedItem.Text;
            orderSku = oeCtrl.SelectOrderDetail(Convert.ToInt32(ddlDistributor.SelectedValue), long.Parse(Session["orderId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 0);


        }
    }
    protected void Calculate()
    {
        try
        {

            decimal ObjGrossSales = 0;
            decimal ObjStandardDiscount = 0;
            decimal ObjDiscountPrice = 0;

            decimal ObjExtraDiscount = 0;
            decimal ProductStdDist = 0;
            decimal TotalAmount = 0;
            decimal TotalAmount2 = 0;

           

            DataTable dt = new DataTable();

            dt = (DataTable)Session["orderSku"];

            foreach (DataRow dr in dt.Rows)
            {
                dr["standardDiscount"] = 0;
                dr["standardDiscountPer"] = 0;
                dr["extraDiscount"] = 0;
                
                dr["amount"] = decimal.Parse(_dc.chkNull_0(dr["price"].ToString())) * int.Parse(dr["CurrentRcdQty"].ToString());

                dr["discountedPrice"] = 0;

                TotalAmount2 += decimal.Parse(dr["amount"].ToString());
            }
            decimal gstAmount = CalculateGst(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)));
            //decimal addGstAmount = CalculateGst(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(hfAddGst.Value)));

          //  decimal Removediscount = CalculateDiscount(TotalAmount2, Convert.ToDecimal(_dc.chkNull_0(txtFreight.Text)));
           // decimal creditAmount = 0;

          //  if (Removediscount < TotalAmount2)
           // {
             //   creditAmount = TotalAmount2 - Removediscount + gstAmount ;
                txtTotalAmount.Text = string.Format("{0:0.00}", TotalAmount2 + gstAmount);
          //  }
            //else
           // {
         //       ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Discount should be less than Total Amount');", true);
          //  }
           

            PromotionCollections_Controller Arpc = (PromotionCollections_Controller)Session["Arpc"];
           

            if (dt.Rows.Count > 0)
            {
                if (Arpc != null)
                {
                    SKUGroupController GroupCtl = new SKUGroupController();

                    ArrayList ARlistOffer = oeCtrl.GetPromotionOffers(Arpc, _mCustomerVolClassId, _mCustomerTypeId, dt, false);

                    for (int i = 0; i < ARlistOffer.Count; i++)
                    {
                        PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];

                        if (pofferCol.SKU_ID > 0)
                        {
                            #region slab on SKU
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (int.Parse(dr["skuId"].ToString()) == pofferCol.SKU_ID)
                                {

                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + pofferCol.Offer_Value;

                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100) * (decimal.Parse(dr["amount"].ToString()));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["standardDiscount"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["standardDiscountPer"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        }
                                    }
                                   
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region slab on SKU Group

                            foreach (DataRow dr in dt.Rows)
                            {
                                if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["skuId"].ToString())))
                                {


                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["extraDiscount"] = decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / 100) * (decimal.Parse(dr["amount"].ToString()) - decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["standardDiscount"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["standardDiscountPer"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / 100);
                                        }
                                    }
                                }
                               


                            }
                            #endregion
                        }

                    }
                }
                #region Recalculate Products

                foreach (DataRow dr in dt.Rows)
                {
                    ProductStdDist = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["amount"].ToString())) - decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString())));
                    if (ProductStdDist > 0)
                    {
                        dr["standardDiscount"] = decimal.Parse(_dc.chkNull_0(dr["standardDiscountPer"].ToString())) * (decimal.Parse(_dc.chkNull_0(dr["amount"].ToString())));
                        dr["discountedPrice"] = (decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString()))) / int.Parse(_dc.chkNull_0(dr["CurrentRcdQty"].ToString()));
                        dr["discountedPrice"] = (decimal.Parse(_dc.chkNull_0(dr["price"].ToString())) - decimal.Parse(dr["discountedPrice"].ToString()));
                    }
                    ObjGrossSales += decimal.Parse(_dc.chkNull_0(dr["amount"].ToString()));
                    ObjStandardDiscount += decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString()));
                    ObjExtraDiscount += decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString()));
                  

                    #region GST Calculation
                    
                    decimal TempAmount = decimal.Parse(_dc.chkNull_0(dr["amount"].ToString()));
                    decimal TempDiscount = decimal.Parse(_dc.chkNull_0(dr["standardDiscount"].ToString())) + decimal.Parse(_dc.chkNull_0(dr["extraDiscount"].ToString()));
                    
                    dr["netAmount"] = (TempAmount  - TempDiscount);
                   
                    #endregion
                    TotalAmount += (TempAmount - TempDiscount);
                }
              
                //dt = (DataTable)Session["orderSku"];
                 gstAmount = CalculateGst(TotalAmount, Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)));
                 hfGstAmount.Value = Convert.ToString(gstAmount);
                 hfStandardDiscount.Value = Convert.ToString(ObjStandardDiscount);
                 hfExtraDiscount.Value = Convert.ToString(ObjExtraDiscount);

                 txtTotalAmount.Text = string.Format("{0:0.00}",  TotalAmount + gstAmount);
               
                Session.Add("orderSku",dt);
             
                #endregion
            }
          
        }

        catch (Exception eee)
        {
            eee.Message.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            


            LoadDistributors();
            LoadCustomers();
           
            if (Session["invoiceId"] != null)
            {
                loadInvoice();
                Session.Add("orderSku", orderSku);
            }
            if (Session["orderId"] != null)
            {
                loadOrder();
                Session.Add("orderSku", orderSku);
               
            }
            LoadPromotion();
            EnableDisableController(false);
            
            LoadGird();
        }
    }

    #region Load

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
    private void LoadCustomers()
    {
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer(int.Parse(ddlDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            
                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CustomerTown");
               // Session.Add("dtCustomer", dtCustomer);
            
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
   
    private bool CreditLimit()
    {
        //if (!chkLimit.Checked)
        //{
        //    decimal ledgerBal = decimal.Parse(_dc.chkNull_0(lblLedgerBalance.Text));
        //    //decimal SaleLimit = decimal.Parse(_dc.chkNull_0(lblSaleLimit.Text));
        //    if (decimal.Parse(_dc.chkNull_0(txtTotalAmount.Text)) > ledgerBal  )//+ SaleLimit)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        return true;
    }
 
    private void LoadPromotion()
    {

        OrderEntryController orderController = new OrderEntryController();
        PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DISTRIBUTOR_ID"].ToString()), Constants.IntNullValue, Convert.ToDateTime(this.Session["CurrentWorkDate"]));
        Session.Add("Arpc", Arpc);

    }

    #endregion

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
   

    #region Detail Panel

    DataTable orderSku = new DataTable();
   
    private void LoadGird()
    {
        int TotalValue = 0;
        //decimal TotalAmount = 0;
        int TotalSale = 0;
        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

        if (Session["invoiceId"] == null)
        {
            foreach (DataRow dr in orderSku.Rows)
            {
                TotalValue += int.Parse(dr["qty"].ToString());
                TotalSale += int.Parse(dr["RcdQty"].ToString());
            }
         
            if (TotalSale == TotalValue)
            {
              
                btnSaveOrder.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Could not Dispatch against that Order.');", true);
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                //  "alert('Could not Dispatch against that Order.'); window.location='" +
                //    Request.ApplicationPath + "/Forms/frmSaleOrderView.aspx?LevelType=3&TopID=26&LevelID=97';", true);
                return;
            }
        }
        else
        {
        int i = 0;
        foreach (GridViewRow gvr in grdOrder.Rows)
        {
          
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            if (txtCurrentRecQty.Text.Length > 0)
            {
                TotalSale += int.Parse(txtCurrentRecQty.Text);

            }
            i += 1;
            }
        txtTotalQty.Text = TotalSale.ToString();
         //   txtTotalAmount.Text = string.Format("{0:0.00}", TotalAmount);
        }
    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // orderSku = (DataTable)Session["orderSku"];
        if (e.Row.RowIndex == 0)
        {
            //Set the focus to control on the edited row
            e.Row.FindControl("txtCurrentRecQty").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCurrentRecQty = (TextBox)e.Row.FindControl("txtCurrentRecQty");
            
        }

    }
    private bool CheckStock(int Qty, int SkuId, string SkuName)
    {
        if (Qty > 0)
        {
            PurchaseController mController = new PurchaseController();
            DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(ddlDistributor.SelectedValue), SkuId);
            if (dtstock.Rows.Count > 0)
            {
                if (ViewState["PrevRcdQty"] != null)
                {
                    if (int.Parse(dtstock.Rows[0][0].ToString()) + int.Parse(ViewState["PrevRcdQty"].ToString()) < Qty)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return false;
                    }
                }
                else
                {
                    if (int.Parse(dtstock.Rows[0][0].ToString()) < Qty)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                        return false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + SkuName + " No Stock Found');", true);
                return false;
            }
        }
        return true;
    }
    protected void txtCurrentRecQty_TextChanged(object sender, EventArgs e)
    {
       
        TextBox txtCurrentRecQty = sender as TextBox;
        TextBox txtRecQty = sender as TextBox;

        if (txtCurrentRecQty == null) return;
        GridViewRow item = (GridViewRow)txtCurrentRecQty.NamingContainer;
       
        int recieveQty = 0;
        if (!string.IsNullOrEmpty(txtCurrentRecQty.Text))
        {                                   //prev rec qty
            recieveQty = int.Parse(item.Cells[7].Text) + int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text));
            
        }
       
        DataTable order = (DataTable)Session["orderSku"];
       // if (recieveQty >  0)
      //  {
            int orderQty = int.Parse(order.Rows[item.RowIndex]["qty"].ToString());
            if (Session["invoiceId"] != null)
            {
                ViewState["PrevRcdQty"] = Convert.ToInt32(_dc.chkNull_0(order.Rows[item.RowIndex]["CurrentRcdQty"].ToString()));
            }
            if (CheckStock(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)), int.Parse(order.Rows[item.RowIndex]["skuId"].ToString()), Convert.ToString(order.Rows[item.RowIndex]["skuName"])))
            {
                ViewState.Remove("PrevRcdQty");

                if (recieveQty > orderQty)
                {
                    txtCurrentRecQty.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Dispatch Qty should not be greater than Order Qty!');", true);

                }
                else
                {
                    order.Rows[item.RowIndex]["CurrentRcdQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)));
                    order.Rows[item.RowIndex]["amount"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)) * decimal.Parse(order.Rows[item.RowIndex]["price"].ToString()));
                    
               
                  
                    if (item.RowIndex + 1 < grdOrder.Rows.Count)
                    {
                        Session.Add("orderSku", order);
                        Calculate();
                       
                        grdOrder.Rows[item.RowIndex + 1].Cells[4].Focus();
                    }
                    else
                    {
                        Session.Add("orderSku", order);
                        Calculate();
                        txtTotalQty.Focus();
                    }
                }
            }
            else
            {
                txtCurrentRecQty.Focus();
            }
      //  }

    }

   
    #endregion

    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        Calculate();
        bool flag = true;
        int orderQty = 0;
        int recieveQty = 0;
        int i = 0;
        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
            if (txtCurrentRecQty.Text.Length > 0)
            {
                orderSku = (DataTable)Session["orderSku"];
                orderQty = int.Parse(orderSku.Rows[i]["qty"].ToString());
                recieveQty = Convert.ToInt32(txtRecQty.Text) + Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text));
               
                
                if (Session["invoiceId"] != null)
                {
                    ViewState["PrevRcdQty"] = Convert.ToInt32(_dc.chkNull_0(orderSku.Rows[i]["CurrentRcdQty"].ToString()));
                }
                if (CheckStock(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)), int.Parse(orderSku.Rows[i]["skuId"].ToString()), Convert.ToString(orderSku.Rows[i]["skuName"])))
                {
                    ViewState.Remove("PrevRcdQty");

                    if (recieveQty > orderQty)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Dispatch Qty should not be greater than Order Qty.');", true);
                        return;
                    }
                    i += 1;
                }
                else
                {
                    flag = false;
                    break;
                    
                }
            }
        }
        if (flag)//if All Item's stock is OKK
        {
          

            orderSku = (DataTable)Session["orderSku"];
            _dtFreeSku = (DataTable)Session["_dtFreeSku"];
     

            if (orderSku.Rows.Count > 0)
            {
                if (CreditLimit())
                {
                    long IsValidInsert = -1;

                    decimal mTotalAmount = orderSku.Rows.Cast<DataRow>().Sum(dr => decimal.Parse(dr["netAmount"].ToString()));
                    int mTotalQty = orderSku.Rows.Cast<DataRow>().Sum(dr => int.Parse(dr["CurrentRcdQty"].ToString()));

                    mTotalAmount += Convert.ToDecimal(_dc.chkNull_0(hfStandardDiscount.Value)) + Convert.ToDecimal(_dc.chkNull_0(hfExtraDiscount.Value));
                     
                    if (Session["orderId"] != null)
                    {
                        IsValidInsert = oeCtrl.Add_Invoice(Convert.ToInt32(rdbInvoiceType.SelectedValue), int.Parse(ddlDistributor.SelectedValue), int.Parse(hfOrderQty.Value),
                            long.Parse(ddlCustomer.SelectedValue), long.Parse(Session["orderId"].ToString()), mTotalAmount,
                          Convert.ToDecimal(_dc.chkNull_0(hfStandardDiscount.Value)), Convert.ToDecimal(_dc.chkNull_0(hfExtraDiscount.Value)), Convert.ToDecimal(_dc.chkNull_0(hfGstAmount.Value))
                          , Convert.ToDecimal(_dc.chkNull_0(txtTotalAmount.Text)), orderSku, _dtFreeSku
                        , int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                        , mTotalQty, txtDriverName.Text, txtTruckNo.Text,Convert.ToString(rdbDiscountType.SelectedItem.Value), txtCellNo.Text, txtBuilty.Text
                        , Constants.DateNullValue, txtRemarks.Text, decimal.Parse(_dc.chkNull_0(hfGst.Value))
                        , Convert.ToInt32(hfIsGstReg.Value));
                        if (IsValidInsert > 0)
                        {
                          
                           
                            Response.Redirect("frmSaleOrderView.aspx?LevelID=97&LevelType=3&TopID=26");

                        }
                        else
                        {
                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                        }
                    }

                    else
                    {
                        var id = Session["invoiceId"].ToString().Split('-');

                        if (oeCtrl.Update_Invoice(Convert.ToInt32(rdbInvoiceType.SelectedValue),Convert.ToInt64(id[1]), DateTime.Parse(Session["CurrentWorkDate"].ToString())
                        , int.Parse(ddlDistributor.SelectedValue), "0", long.Parse(ddlCustomer.SelectedValue), 0,
                        mTotalAmount, Convert.ToDecimal(_dc.chkNull_0(hfStandardDiscount.Value)), Convert.ToDecimal(_dc.chkNull_0(hfExtraDiscount.Value)),Convert.ToDecimal(_dc.chkNull_0(hfGstAmount.Value))
                        ,Convert.ToDecimal(_dc.chkNull_0(txtTotalAmount.Text)), 0, 0, orderSku, int.Parse(this.Session["UserId"].ToString())
                        , 0, 0, 0, mTotalQty, txtDriverName.Text, txtTruckNo.Text, rdbDiscountType.SelectedItem.Value, txtCellNo.Text, txtBuilty.Text
                        , Constants.DateNullValue, txtRemarks.Text, decimal.Parse(_dc.chkNull_0(hfGst.Value))
                        ,  Convert.ToInt32(hfIsGstReg.Value)))
                        {
                            DataTable dt = oeCtrl.SelectDispatchQty(Convert.ToInt64(id[0]), 3);
                            if (dt.Rows.Count > 0)
                            {
                                int dispatchQty = Convert.ToInt32(dt.Rows[0]["dispatchQty"].ToString());
                                if (int.Parse(hfOrderQty.Value) == dispatchQty)
                                {
                                    if (!oeCtrl.UpdateOrder(Convert.ToInt64(id[0]), int.Parse(ddlDistributor.SelectedValue), 1))
                                    {
                                        
                                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                                    }
                                }
                                else
                                {
                                    if (!oeCtrl.UpdateOrder(Convert.ToInt64(id[0]), int.Parse(ddlDistributor.SelectedValue), 0))
                                    {
                                        
                                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                                    }
                                }
                            }
                            //Session.Add("reportId", id[1]);
                            
                            Response.Redirect("frmSaleOrderView.aspx?LevelID=97&LevelType=3&TopID=26");
                        }
                        else
                        {
                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                        }
                    }

                }
                else
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Check Customer Business Limit');", true);
                }

            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Select At least one Product');", true);
            }
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("orderId");
        Session.Remove("invoiceId");

        Response.Redirect("frmSaleOrderView.aspx?LevelID=97&LevelType=3&TopID=26");
    }


    private decimal CalculateGst(decimal mTotalAmount, decimal gst)
    {
        decimal gstAmount = mTotalAmount * (gst / 100);
        return gstAmount;
    }
    private decimal CalculateAdditionGst(decimal mTotalAmount, decimal gst)
    {

        decimal gstAmount = mTotalAmount * (gst / 100);
        return gstAmount;
        
    }
    private decimal CalculateDiscount(decimal mTotalAmount, decimal discount)
    {
        if (discount > 0)
        {
            if (rdbDiscountType.SelectedValue == "0")
            {

                 discount = mTotalAmount * (discount / 100);
                
            }
        }
        return discount;
    }

}