using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

public partial class Forms_frmPurchaseReceivedEntry: System.Web.UI.Page
{
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    readonly DataControl dc = new DataControl();

     DataTable OrderedSKU;
    static long mPURCHASE_MASTER_ID = Constants.LongNullValue;
    static int mPURCHASE_PRINCIPAL_ID = Constants.IntNullValue;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            LoadDistributor();
          
            GetDocumentNo();
            Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

            txtGrossAmount.Attributes.Add("readonly", "readonly");
            txtNetAmount.Attributes.Add("readonly", "readonly");
            txtTotalQuantity.Attributes.Add("readonly", "readonly");
            LoadDocumentDetail();
           
        }
    }

    #region Load

    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo2(Constants.LongNullValue, Constants.IntNullValue, 2, Constants.DateNullValue);

        drpDocumentNo.Items.Add(new ListItem("--------Select--------", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 1, 0, false);
    }
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2,true);
    }
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        if (drpDocumentNo.Items.Count > 0)

        {
            if (drpDocumentNo.SelectedValue != Constants.LongNullValue.ToString())
            {
                var arr = drpDocumentNo.SelectedItem.Text.Split('-');
                var arr2 = drpDocumentNo.SelectedItem.Value.Split('-');

                DataTable dt = mPurchase.SelectPurchaseOrderDocumentNo2(Convert.ToInt64(arr[0]), Constants.IntNullValue, 2, Constants.DateNullValue);

                if (dt.Rows.Count > 0)
                {
                    bool flag = true;

                    drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var arr3 = Convert.ToString(dr["PURCHASE_MASTER_ID"]).Split('-');
                        if (Convert.ToString(arr3[1]) != "")
                            {
                            if (Convert.ToString(arr3[1]) == Convert.ToString(arr2[1]))
                            {
                                if (Convert.ToDateTime(dr["DOCUMENT_DATE"].ToString()).ToString("dd-MMM-yyyy") == Convert.ToDateTime(Session["CurrentWorkDate"].ToString()).ToString("dd-MMM-yyyy"))
                                {


                                    mPURCHASE_MASTER_ID = Convert.ToInt64(arr3[1]);
                                    mPURCHASE_PRINCIPAL_ID = Convert.ToInt32(dc.chkNull_0(dr["SOLD_FROM"].ToString()));

                                    txtInvoiceNo.Text = dr["INVOICE_NO"].ToString().Replace("&nbsp;", "");
                                    txtGrossAmount.Text = string.Format("{0,0:0.00}" ,dr["TOTAL_AMOUNT"]);
                                    txtDiscount.Text = string.Format("{0,0:0.00}", dr["DISCOUNT"]);
                                    txtNetAmount.Text = string.Format("{0,0:0.00}", dr["TOTAL_NET_AMOUNT"]);
                                    txtRemarks.Text = dr["REMARKS"].ToString().Replace("&nbsp;", ""); //BUILTY AS REMARKS

                                    OrderedSKU = mPurchase.SelectPurchaseOrderDetail2(long.Parse(arr3[1]), Constants.IntNullValue, Convert.ToDateTime(Session["CurrentWorkDate"]), long.Parse(arr3[0]));


                                }
                                else
                                {
                                    mPURCHASE_MASTER_ID = Constants.LongNullValue;
                                    mPURCHASE_PRINCIPAL_ID = Convert.ToInt32(dc.chkNull_0(dt.Rows[0]["SOLD_FROM"].ToString()));

                                    OrderedSKU = mPurchase.SelectPurchaseOrderDetail2(long.Parse(arr3[1]), Constants.IntNullValue, Convert.ToDateTime(Session["CurrentWorkDate"]),long.Parse(arr3[0]));

                                }
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            mPURCHASE_MASTER_ID = Constants.LongNullValue;
                            mPURCHASE_PRINCIPAL_ID = Convert.ToInt32(dc.chkNull_0(dt.Rows[0]["SOLD_FROM"].ToString()));

                            OrderedSKU = mPurchase.SelectPurchaseOrderDetail(long.Parse(arr[0]), Constants.IntNullValue);
                            flag = false;
                            break;
                        }
                    }
                    Session.Add("OrderedSKU", OrderedSKU);
                    LoadGird();
                }
            }
        }
    }
    private void LoadGird()
    {
        int TotalValue = 0;
        decimal value = 0;

        OrderedSKU = (DataTable)Session["OrderedSKU"];
        GrdPurchase.DataSource = null;
        GrdPurchase.DataBind();

        GrdPurchase.DataSource = OrderedSKU;
        GrdPurchase.DataBind();

        foreach (DataRow dr in OrderedSKU.Rows)
        {
            TotalValue += int.Parse(dr["RcdQty"].ToString());
            value += decimal.Parse(dr["RcdQty"].ToString())* decimal.Parse(dr["PRICE"].ToString());
        }
        txtTotalQuantity.Text = TotalValue.ToString();
        txtGrossAmount.Text= string.Format("{0,0:0.00}", value);
        txtNetAmount.Text = string.Format("{0,0:0.00}", value -Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)));
       

        if (value == 0)
        {
            txtDiscount.Text = "";
        }
    }

    #endregion

    #region Click OPerations
    
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        DataTable PurchasedPSKU = new DataTable();
        PurchasedPSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchasedPSKU.Columns.Add("SKU_ID", typeof(int));
        PurchasedPSKU.Columns.Add("PRICE", typeof(decimal));
        PurchasedPSKU.Columns.Add("Quantity", typeof(int));
        PurchasedPSKU.Columns.Add("FREE_SKU", typeof(int));
        PurchasedPSKU.Columns.Add("AMOUNT", typeof(decimal));

        foreach (GridViewRow gvr in GrdPurchase.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
          

            DataRow dr = PurchasedPSKU.NewRow();
            dr["SKU_ID"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[0].Text));
            dr["FREE_SKU"] = Convert.ToInt32(dc.chkNull_0(txtRecQty.Text));
            dr["PRICE"] = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[6].Text));
            dr["Quantity"] = Convert.ToInt32(dc.chkNull_0(gvr.Cells[3].Text));
            dr["AMOUNT"] = Convert.ToDecimal(dc.chkNull_0(gvr.Cells[6].Text)) * Convert.ToInt32(dc.chkNull_0(txtRecQty.Text));
            PurchasedPSKU.Rows.Add(dr);
        }


        if (PurchasedPSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()), PurchasedPSKU))
                {
                    LoadDistributor();
                    GetDocumentNo();
                    //drpDocumentNo_SelectedIndexChanged(null, null);
                    //LoadDocumentDetail();
                    GrdPurchase.DataSource = null;
                    GrdPurchase.DataBind();
                    ClearAll();
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('GRN saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGird();
    }

    #endregion

 
    private bool CalculatePurchase(DateTime MWorkDate, DataTable PurchasedPSKU)
    {
       
        byte Status = 2;
        
        PurchaseController mController = new PurchaseController();

        var arr = drpDocumentNo.SelectedItem.Text.Split('-');
        if (mPURCHASE_MASTER_ID > 0)
        {
            bool mResult = mController.UpdateGRN(mPURCHASE_MASTER_ID, int.Parse(drpDistributor.SelectedValue.ToString()), "", 2, MWorkDate
                , int.Parse(drpDistributor.SelectedValue.ToString()), 0, Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtGrossAmount.Text)), false, PurchasedPSKU, 0, txtRemarks.Text.Trim()
                , int.Parse(Session["UserId"].ToString()),mPURCHASE_PRINCIPAL_ID, drpDocumentNo.SelectedItem.Text.ToString()
                , txtInvoiceNo.Text.Trim(), Constants.DateNullValue, "", "", Status);
            return mResult;
        }
        else
        {
            bool mResult = mController.InsertGRN(int.Parse(drpDistributor.SelectedValue.ToString()), "", 2
            , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0,Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), Convert.ToDecimal(dc.chkNull_0(txtGrossAmount.Text)), false, PurchasedPSKU, 0, txtRemarks.Text.Trim()
            , int.Parse(Session["UserId"].ToString()),mPURCHASE_PRINCIPAL_ID, long.Parse(arr[0]), drpDocumentNo.SelectedItem.Text.ToString()
            , txtInvoiceNo.Text.Trim(), Constants.DateNullValue, "", "", Status);
            return mResult;
        }
    }

    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            OrderedSKU = new DataTable();
            Session.Add("OrderedSKU", OrderedSKU);
            LoadGird();
        }
        else
        {
            LoadDocumentDetail();
          
        }
    }


    protected void GrdPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex == 0)
        {
            e.Row.FindControl("txtRecQty").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRecQty = (TextBox)e.Row.FindControl("txtRecQty");
        }
    }
    protected void txtRecQty_TextChanged(object sender, EventArgs e)
    {
        DataControl _dc = new DataControl();
        TextBox txtRecQty = sender as TextBox;


        if (txtRecQty == null) return;
        GridViewRow item = (GridViewRow)txtRecQty.NamingContainer;

        int recieveQty = 0;
        if (!string.IsNullOrEmpty(txtRecQty.Text))
        {                                   //prev rec qty
            recieveQty = int.Parse(txtRecQty.Text);
        }
        DataTable order = (DataTable)Session["OrderedSKU"];
        if (recieveQty > 0)
        {
            int orderQty = int.Parse(order.Rows[item.RowIndex]["Quantity"].ToString());


            if ((recieveQty + int.Parse(order.Rows[item.RowIndex]["PrevRcdQty"].ToString())) > orderQty)
            {
                txtRecQty.Text = "0";
                txtRecQty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Received Qty should not be greater than Order Qty!');", true);
            }
            else
            {
                order.Rows[item.RowIndex]["CurrentRcdQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtRecQty.Text)));

                decimal GrossAmount = 0;

                foreach (DataRow dr in order.Rows)
                {
                   GrossAmount += int.Parse(dr["CurrentRcdQty"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                }
                txtGrossAmount.Text = Math.Round(GrossAmount, 2).ToString();
                txtNetAmount.Text = Math.Round(GrossAmount - Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)), 2).ToString();

                if (item.RowIndex + 1 < GrdPurchase.Rows.Count)
                {
                    GrdPurchase.Rows[item.RowIndex + 1].Cells[5].Focus();
                }
                else
                {
                    txtInvoiceNo.Focus();
                }
            }
        }
        else
        {
            txtRecQty.Focus();
        }
    }
    private void ClearAll()
    {
        txtInvoiceNo.Text = "";
        txtGrossAmount.Text = "";
        txtDiscount.Text = "";
        txtTotalQuantity.Text = "";
        txtNetAmount.Text = "";
        txtRemarks.Text = "";
    }

}