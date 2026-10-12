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


public partial class Forms_frmSaleReturnQA : System.Web.UI.Page
{
    readonly DataControl _dc = new DataControl();
    readonly CustomerDataController _customerCtrl = new CustomerDataController();
    readonly OrderEntryController oeCtrl = new OrderEntryController();

    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!Page.IsPostBack)
        {
            CreateTable();
            LoadDistributors();
            LoadCustomers();
            

            if (Convert.ToString(Session["SaleReturnQAId"]) != "")
            {
                DataTable dtOrder = oeCtrl.SelectSaleReturn(Convert.ToInt64(Session["SaleReturnQAId"]), Convert.ToInt32(ddlDistributor.SelectedValue), 2, Constants.IntNullValue, Constants.DateNullValue);

                ddlCustomer.SelectedValue = dtOrder.Rows[0]["CUSTOMER_ID"].ToString();
                ddlDistributor.SelectedValue = dtOrder.Rows[0]["DISTRIBUTOR_ID"].ToString();
                txtRemarks.Text= dtOrder.Rows[0]["remarks"].ToString();
                ExistenOrderDetail(Convert.ToInt64(Session["SaleReturnQAId"]));

                EnableDisableController(false);
            }
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
    private void LoadCustomers()
    {
        if (ddlDistributor.Items.Count > 0)
        {
            DataTable dtCustomer = _customerCtrl.SelectPrincipalCustomer(int.Parse(ddlDistributor.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            if (dtCustomer.Rows.Count > 0)
            {

                clsWebFormUtil.FillDropDownList(ddlCustomer, dtCustomer, "CUSTOMER_ID", "CustomerTown");
                Session.Add("dtCustomer", dtCustomer);
            }
        }
    }

    #region Detail Panel

    DataTable orderSku = new DataTable();
    
    private void CreateTable()
    {
        orderSku = new DataTable();
        orderSku.Columns.Add("orderDetailId", typeof(long));
        orderSku.Columns.Add("skuId", typeof(int));
        orderSku.Columns.Add("skuName", typeof(string));
        orderSku.Columns.Add("qty", typeof(int));
        orderSku.Columns.Add("NGQty", typeof(int));
        orderSku.Columns.Add("remarks", typeof(string));

        Session.Add("orderSku", orderSku);

    }

    private void LoadGird()
    {
        int TotalValue = 0;

        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

        foreach (DataRow dr in orderSku.Rows)
        {
            TotalValue += int.Parse(dr["CurrentNGQty"].ToString());
        }
        txtTotalQty.Text = TotalValue.ToString();

    }
   
    #endregion

    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        try
        {
          
        
        DataTable orderSkus = new DataTable();
        

        orderSkus.Columns.Add("orderDetailId", typeof(long));
        orderSkus.Columns.Add("skuId", typeof(int));
        orderSkus.Columns.Add("skuName", typeof(string));
        orderSkus.Columns.Add("qty", typeof(int));
        orderSkus.Columns.Add("NGQty", typeof(int));
        orderSkus.Columns.Add("remarks", typeof(string));


        foreach (GridViewRow gvr in grdOrder.Rows)
        {
            TextBox txtNGQty = gvr.FindControl("txtNGQty") as TextBox;
            TextBox txtRemarks = gvr.FindControl("txtRemarks") as TextBox;

            DataRow dr = orderSkus.NewRow();

            dr["skuId"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[0].Text));
            dr["remarks"] = txtRemarks.Text;
            dr["qty"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[2].Text));
            dr["NGQty"] = Convert.ToInt32(_dc.chkNull_0(txtNGQty.Text));

            orderSkus.Rows.Add(dr);

        }
            if (orderSkus.Rows.Count > 0)
            {
                int mTotalQty = 0;
                foreach (DataRow dr in orderSkus.Rows)
                {
                    if (dr["NGQty"].ToString() != "")
                    {
                        if (int.Parse(dr["NGQty"].ToString()) > 0)
                        {
                            mTotalQty += int.Parse(dr["NGQty"].ToString());
                        }
                    }
                }


                if (Convert.ToString(Session["SaleReturnQAId"]) != "")
                {
                    bool flag = oeCtrl.Update_SaleReturnQA(Convert.ToInt64(Session["SaleReturnQAId"]), int.Parse(ddlDistributor.SelectedValue), long.Parse(ddlCustomer.SelectedValue)
                     , mTotalQty, orderSkus, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 2, txtRemarks.Text);

                    if (flag)
                    {

                        //ClearMasterALL();
                        Response.Redirect("frmSaleReturnViewQA.aspx?LevelID=104&LevelType=3&TopID=26");
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('some error occurred');", true);
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
        Response.Redirect("frmSaleReturnViewQA.aspx?LevelID=104&LevelType=3&TopID=26");


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

        ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
    }

    #endregion

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // orderSku = (DataTable)Session["orderSku"];
        if (e.Row.RowIndex == 0)
        {
            //Set the focus to control on the edited row
            e.Row.FindControl("txtNGQty").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtNGQty = (TextBox)e.Row.FindControl("txtNGQty");
            TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

        }

    }
    protected void txtNGQty_TextChanged(object sender, EventArgs e)
    {

        TextBox txtNGQty = sender as TextBox;
      

        if (txtNGQty == null) return;
        GridViewRow item = (GridViewRow)txtNGQty.NamingContainer;

        int recieveQty = 0;
        if (!string.IsNullOrEmpty(txtNGQty.Text))
        {
            recieveQty = int.Parse(_dc.chkNull_0(txtNGQty.Text));

        }

        DataTable order = (DataTable)Session["orderSku"];
        if (recieveQty > 0)
        {
            int orderQty = int.Parse(order.Rows[item.RowIndex]["qty"].ToString());

            if (recieveQty > orderQty)
            {
                
                txtNGQty.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('N.G Qty should not be greater than Return Qty!');", true);

            }
            else
            {
                order.Rows[item.RowIndex]["CurrentNGQty"] = Convert.ToString(int.Parse(_dc.chkNull_0(txtNGQty.Text)));
               
                int TotalQty = 0;
                foreach (DataRow dr in order.Rows)
                {
                    if (dr["CurrentNGQty"].ToString() != "")
                    {
                        if (int.Parse(dr["CurrentNGQty"].ToString()) > 0)
                        {
                            TotalQty += int.Parse(dr["CurrentNGQty"].ToString());
                        }
                    }
                }
                txtTotalQty.Text = TotalQty.ToString();


                if (item.RowIndex + 1 < grdOrder.Rows.Count)
                {
                    Session.Add("orderSku", order);

                    grdOrder.Rows[item.RowIndex].Cells[4].Focus();
                }
                else
                {
                    Session.Add("orderSku", order);

                    grdOrder.Rows[item.RowIndex].Cells[4].Focus();
                }
            }
        }
        else
        {
            txtNGQty.Focus();
        }
    }
}