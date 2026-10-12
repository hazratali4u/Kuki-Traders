using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommonLayer.Classes;
using System.Text;

public partial class Forms_frmPacking : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();

    private void loadPacking()
    {
        lblDONo.Text = "Carton No: " + Session["packingId"];

        DataTable dt = oeCtrl.SelectPackingList(Convert.ToInt64(Session["packingId"].ToString()));
        if (dt.Rows.Count > 0)
        {
            
            ddlDistributor.SelectedValue = dt.Rows[0]["distributorId"].ToString();
            ddlCustomer.SelectedValue = dt.Rows[0]["customerId"].ToString();
            
            //------------------------------------------------------------------------\\

            txtDriverName.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["driverName"].ToString()));
            txtCellNo.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["cellNo"].ToString()));
            txtTransporter.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["transporter"].ToString()));
            txtBuilty.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["builtyNo"].ToString()));
            txtRemarks.Text = Convert.ToString(_dc.chkNull_0(dt.Rows[0]["remarks"].ToString()));

            dtDetail = oeCtrl.SelectPackingDetail(Convert.ToInt64(Session["packingId"].ToString()), Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt32(ddlCustomer.SelectedValue));
            Session.Add("dtDetail", dtDetail);
        }
    }
   

    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!Page.IsPostBack)
        {

            LoadDistributors();
            LoadCustomers();

            if (Session["packingId"] != null)
            {
                loadPacking();

                EnableDisableController(false);
            }
          
            else
            {
                loadDetail();
            }
           
            LoadGird();
        }
    }

    protected void ddlDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomers();
        loadDetail();
        LoadGird();

    }


    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDetail();
        LoadGird();

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
        if (Session["packingId"] == null)
        {
            if (ddlDistributor.Items.Count > 0)
            {
                DataTable dtCustomer = _customerCtrl.DOCustomers(int.Parse(ddlDistributor.SelectedValue), 1);
                if (dtCustomer.Rows.Count > 0)
                {
                    clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "customerId", "customerTown");
                }
            }
        }
        else
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer(int.Parse(ddlDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            if (dtCustomer.Rows.Count > 0)
            {

                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CustomerTown");
            }
        }
    }
    private void loadDetail()
    {
        if (ddlCustomer.Items.Count > 0)
        {
            dtDetail = _customerCtrl.DODetail(int.Parse(ddlDistributor.SelectedValue), int.Parse(ddlCustomer.SelectedValue), 2);
            Session.Add("dtDetail", dtDetail);
        }
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

    DataTable dtDetail = new DataTable();

    private void LoadGird()
    {
        int TotalValue = 0;
        
        int TotalSale = 0;
        dtDetail = (DataTable)Session["dtDetail"];
        grdOrder.DataSource = dtDetail;
        grdOrder.DataBind();

        //if (Session["packingId"] == null)
        //{
        //    foreach (DataRow dr in dtDetail.Rows)
        //    {
        //        TotalValue += int.Parse(dr["saleQty"].ToString());
        //    }
        //    txtTotalQty.Text = TotalValue.ToString();
           
        
        //}
        //else
        //{
        if (dtDetail != null)
        {
            TotalValue = dtDetail.Rows.Cast<DataRow>().Sum(dr => int.Parse(dr["saleQty"].ToString()));
        }
           
            foreach (GridViewRow gvr in grdOrder.Rows)
            {

                TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;
                if (txtCurrentRecQty.Text.Length > 0)
                {
                    TotalSale += int.Parse(txtCurrentRecQty.Text);
                }
                
            }
            txtSoldQty.Text = TotalValue.ToString();
            txtTotalQty.Text = TotalSale.ToString();
           
     //   }
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
    protected void txtCurrentRecQty_TextChanged(object sender, EventArgs e)
    {

        TextBox txtCurrentRecQty = sender as TextBox;
        TextBox txtRecQty = sender as TextBox;

        if (txtCurrentRecQty == null) return;
        GridViewRow item = (GridViewRow)txtCurrentRecQty.NamingContainer;

        int recieveQty = 0;
        if (!string.IsNullOrEmpty(txtCurrentRecQty.Text))
        {                                   //prev rec qty
            if (Session["packingId"] == null)
            {
                recieveQty = int.Parse(item.Cells[5].Text) + int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text));
            }
            else
            {

                recieveQty = int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text));
            }

        }


        DataTable order = (DataTable)Session["dtDetail"];
        if (recieveQty > 0)
        {
            int orderQty = int.Parse(order.Rows[item.RowIndex]["saleQty"].ToString());
           
                if (recieveQty > orderQty)
                {
                    txtRecQty.Text = "0";
                    txtCurrentRecQty.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Packing Qty should not be greater than Sold Qty!');", true);

                }
                else
                {
                    order.Rows[item.RowIndex]["CurrentPackQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtCurrentRecQty.Text)));
                   
                    int TotalQty = 0;
                    foreach (DataRow dr in order.Rows)
                    {
                        if (dr["CurrentPackQty"].ToString()!="")
                        {
                            if (int.Parse(dr["CurrentPackQty"].ToString()) > 0)
                            {
                                TotalQty += int.Parse(dr["CurrentPackQty"].ToString());
                            }
                        }
                    }
                    txtTotalQty.Text = TotalQty.ToString();


                    if (item.RowIndex + 1 < grdOrder.Rows.Count)
                    {
                        grdOrder.Rows[item.RowIndex + 1].Cells[4].Focus();
                    }
                    else
                    {
                        txtTotalQty.Focus();
                    }
                }
           
        }

    }

    #endregion

    protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
        DataTable orderSkus = new DataTable();

        orderSkus.Columns.Add("packingDetailId", typeof(long));
        orderSkus.Columns.Add("skuId", typeof(int));
        orderSkus.Columns.Add("itemName", typeof(string));
        orderSkus.Columns.Add("price", typeof(decimal));
        orderSkus.Columns.Add("packQty", typeof(int));


        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtRecQty = gvr.FindControl("txtRecQty") as TextBox;
            TextBox txtCurrentRecQty = gvr.FindControl("txtCurrentRecQty") as TextBox;

            DataRow dr = orderSkus.NewRow();

            dr["skuId"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[0].Text));
            dr["packQty"] = Convert.ToInt32(_dc.chkNull_0(txtCurrentRecQty.Text));
            dr["price"] = Convert.ToDecimal(_dc.chkNull_0(gvr.Cells[7].Text));
         
            orderSkus.Rows.Add(dr);
        }


        if (orderSkus.Rows.Count > 0)
        {

            long IsValidInsert = -1;

            if (Session["packingId"] == null)
            {
                IsValidInsert = oeCtrl.InsertPacking(int.Parse(ddlDistributor.SelectedValue), long.Parse(ddlCustomer.SelectedValue)
                , txtBuilty.Text, txtTransporter.Text, txtDriverName.Text, txtCellNo.Text, txtRemarks.Text
                , int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), orderSkus);

                if (IsValidInsert > 0)
                {

                     Session.Add("reportId", IsValidInsert);

                    Response.Redirect("frmPackingView.aspx?LevelID=101&LevelType=3&TopID=26");

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                }

            }
            else
                {
                IsValidInsert = oeCtrl.UpdatePacking(long.Parse(Session["packingId"].ToString()), int.Parse(ddlDistributor.SelectedValue)
                    , long.Parse(ddlCustomer.SelectedValue)
                              , txtBuilty.Text, txtTransporter.Text, txtDriverName.Text, txtCellNo.Text, txtRemarks.Text
                              , int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), orderSkus);

                if (IsValidInsert > 0)
                {

                    Session.Add("reportId", IsValidInsert);

                    Response.Redirect("frmPackingView.aspx?LevelID=101&LevelType=3&TopID=26");

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
                }
            }
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Select At least one Product');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("packingId");

        Response.Redirect("frmPackingView.aspx?LevelID=101&LevelType=3&TopID=26");
    }


  
}