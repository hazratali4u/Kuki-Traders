using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommonLayer.Classes;

public partial class Forms_frmSaleReturn : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("pragma", "no-cache");

        if (!Page.IsPostBack)
        {
            CreateTable();
            LoadDistributors();
            LoadCustomers();


            if (Convert.ToString(Session["SaleReturnId"]) != "")
            {
                DataTable dtOrder = oeCtrl.SelectSaleReturn(Convert.ToInt64(Session["SaleReturnId"]),Convert.ToInt32(ddlDistributor.SelectedValue),1, int.Parse(Session["UserID"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
              
                ddlCustomer.SelectedValue = dtOrder.Rows[0]["CUSTOMER_ID"].ToString();
                ddlDistributor.SelectedValue = dtOrder.Rows[0]["DISTRIBUTOR_ID"].ToString();
                hfZoneId.Value = dtOrder.Rows[0]["zoneId"].ToString();
                hfGst.Value = dtOrder.Rows[0]["gstPge"].ToString();
                

                ExistenOrderDetail(Convert.ToInt64(Session["SaleReturnId"]));

                EnableDisableController(false);
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
    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomers();
      
        
        LoadSKUDetail();
        ddlCustomer.Focus();
    }

    private void LoadCustomers()
    {
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer(int.Parse(ddlDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            if (dtCustomer.Rows.Count > 0)
            {

                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CustomerTown");

                hfZoneId.Value = dtCustomer.Rows[0]["zoneId"].ToString();
                hfGst.Value=dtCustomer.Rows[0]["TAX_RATE"].ToString();
               

                Session.Add("dtCustomer", dtCustomer);
            }
        }
    }
  
   
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session["dtCustomer"] != null)
        {
            DataTable dt = (DataTable)Session["dtCustomer"];
            DataRow[] foundRows = dt.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");
            if (foundRows.Length > 0)
            {
                hfZoneId.Value = foundRows[0]["zoneId"].ToString();
                hfGst.Value = foundRows[0]["TAX_RATE"].ToString();
               
            }
        }

        LoadSKUDetail();

    }
   

    #region Detail Panel

    DataTable orderSku = new DataTable();
    private static int RowNo;

    private void LoadSKUDetail()
    {
        ddlSKus.Items.Clear();

        if (ddlDistributor.Items.Count > 0)
        {
           
                    SKUPriceDetailController PController = new SKUPriceDetailController();
                    DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlDistributor.SelectedValue), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
                    clsWebFormUtil.FillDropDownList(ddlSKus, Dtsku_Price, "SKU_ID", "SkuDetail2", true);
                    Session.Add("Dtsku_Price", Dtsku_Price);
           
        }
    }
    private void CreateTable()
    {
        orderSku = new DataTable();
        orderSku.Columns.Add("orderDetailId", typeof(long));
        orderSku.Columns.Add("skuId", typeof(int));
        orderSku.Columns.Add("skuName", typeof(string));
        orderSku.Columns.Add("qty", typeof(int));
        orderSku.Columns.Add("originalQty", typeof(int));
        
        orderSku.Columns.Add("price", typeof(decimal));
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
                            dr["qty"] = int.Parse(_dc.chkNull_0(txtQuantity.Text));
                            dr["price"] = Convert.ToDecimal(foundRows[0]["TRADE_PRICE"]);
                           
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
                        dr["qty"] = int.Parse(txtQuantity.Text);
                        dr["price"] = Convert.ToDecimal(foundRows[0]["TRADE_PRICE"]);
                    }
                    Session.Add("orderSku", orderSku);
                    ClearDetail();
                    LoadGird();

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
            EnableDisableController(false);
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
        txtQuantity.Text = grdOrder.Rows[RowNo].Cells[2].Text;

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
        }
    }

    private void LoadGird()
    {
        int TotalValue = 0;
        
        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

        foreach (DataRow dr in orderSku.Rows)
        {
            TotalValue += int.Parse(dr["qty"].ToString());
            

        }
        txtTotalQty.Text = TotalValue.ToString();
        
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
                int mTotalQty = 0;
                decimal mTotalAmount = 0;
                foreach (DataRow dr in orderSku.Rows)
                {
                  
                    mTotalQty += int.Parse(dr["qty"].ToString());
                    mTotalAmount += decimal.Parse(dr["price"].ToString()) * int.Parse(dr["qty"].ToString());
                }


                if (Convert.ToString(Session["SaleReturnId"]) == "")
                {
                    bool flag = oeCtrl.Add_SaleReturn(int.Parse(ddlDistributor.SelectedValue),long.Parse(ddlCustomer.SelectedValue)
                    , mTotalQty, orderSku, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 1
                    , Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)), mTotalAmount, Convert.ToInt32(hfZoneId.Value));
                    if (flag)
                    {
                        
                        //ClearMasterALL();
                        Response.Redirect("frmSaleReturnView.aspx?LevelID=" + Request["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID="+ Request.QueryString["TopID"].ToString());
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' some error occurred');", true);
                    }
                }
                else
                {
                    bool flag = oeCtrl.Update_SaleReturn(Convert.ToInt64(Session["SaleReturnId"]), int.Parse(ddlDistributor.SelectedValue), long.Parse(ddlCustomer.SelectedValue)
                     , mTotalQty, orderSku, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 1
                     , Convert.ToDecimal(_dc.chkNull_0(hfGst.Value)), mTotalAmount, Convert.ToInt32(hfZoneId.Value));
                    
                    if (flag)
                    {
                   
                    //ClearMasterALL();
                        Response.Redirect("frmSaleReturnView.aspx?LevelID=" + Request["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString());
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

        CreateTable();
        LoadGird();
        ClearDetail();
        Response.Redirect("frmSaleReturnView.aspx?LevelID=103&LevelType=3&TopID=26");
    }
 

    #region Update Panel
   
    private void ExistenOrderDetail(long OrderId)
    {
        orderSku = oeCtrl.SelectSaleReturnDetail(Convert.ToInt32(ddlDistributor.SelectedValue), OrderId, Constants.DateNullValue);
        Session.Add("orderSku", orderSku);
        LoadGird();

    }
  
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
       

        RowNo = 0;
        
        ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
    }

    #endregion
}