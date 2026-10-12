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


public partial class Forms_frmSaleReturnAccount : System.Web.UI.Page
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


            if (Convert.ToString(Session["SaleReturnAccId"]) != "")
            {
                DataTable dtOrder = oeCtrl.SelectSaleReturn(Convert.ToInt64(Session["SaleReturnAccId"]), Convert.ToInt32(ddlDistributor.SelectedValue), 3, Constants.IntNullValue, Constants.DateNullValue);

                ddlCustomer.SelectedValue = dtOrder.Rows[0]["CUSTOMER_ID"].ToString();
                ddlDistributor.SelectedValue = dtOrder.Rows[0]["DISTRIBUTOR_ID"].ToString();
                txtRemarks.Text = dtOrder.Rows[0]["remarks"].ToString();
                hfIsGstReg.Value = dtOrder.Rows[0]["isGstRegister"].ToString();
                hfZoneId.Value = dtOrder.Rows[0]["zoneId"].ToString();
                hfGst.Value = dtOrder.Rows[0]["gstPge"].ToString();
                hfAddGst.Value = dtOrder.Rows[0]["addGstPge"].ToString();

                ExistenOrderDetail(Convert.ToInt64(Session["SaleReturnAccId"]));

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
        

        orderSku = (DataTable)Session["orderSku"];
        grdOrder.DataSource = orderSku;
        grdOrder.DataBind();

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
            orderSkus.Columns.Add("returnQty", typeof(int));
            orderSkus.Columns.Add("remarks", typeof(string));
            orderSkus.Columns.Add("unitPrice", typeof(decimal));
            orderSkus.Columns.Add("techId", typeof(int));
            foreach (GridViewRow gvr in grdOrder.Rows)
            {
                TextBox txtPrice = gvr.FindControl("txtPrice") as TextBox;

                DataRow dr = orderSkus.NewRow();

                dr["skuId"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[0].Text));
                dr["remarks"] = Convert.ToString(gvr.Cells[4].Text);
                dr["qty"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[2].Text));
                dr["NGQty"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[3].Text));
                dr["returnQty"] = Convert.ToInt32(_dc.chkNull_0(gvr.Cells[5].Text));
                dr["unitPrice"] = Convert.ToInt32(_dc.chkNull_0(txtPrice.Text));
                dr["techId"] = Convert.ToInt32(gvr.Cells[7].Text);
                orderSkus.Rows.Add(dr);

                if (int.Parse(dr["returnQty"].ToString()) > 0 && Convert.ToInt32(_dc.chkNull_0(txtPrice.Text))==0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Plz enter price of this Product " + gvr.Cells[1].Text + "');", true);
                    return;
                }
            }
            if (orderSkus.Rows.Count > 0)
            {
                decimal mTotalAmount = 0;
                foreach (DataRow dr in orderSkus.Rows)
                {
                    mTotalAmount += decimal.Parse(dr["unitPrice"].ToString()) * int.Parse(dr["returnQty"].ToString());
                }


                if (Convert.ToString(Session["SaleReturnAccId"]) != "")
                {
                    bool flag = oeCtrl.Update_SaleReturnAccount(Convert.ToInt64(Session["SaleReturnAccId"]), int.Parse(ddlDistributor.SelectedValue), long.Parse(ddlCustomer.SelectedValue)
                     , mTotalAmount, orderSkus, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), 3
                     , txtRemarks.Text, Convert.ToInt32(hfIsGstReg.Value), Convert.ToInt32(hfZoneId.Value), Convert.ToDecimal(_dc.chkNull_0(hfGst.Value))
                     , Convert.ToDecimal(_dc.chkNull_0(hfAddGst.Value)));

                    if (flag)
                    {

                        //ClearMasterALL();
                        Response.Redirect("frmSaleReturnViewAccount.aspx?LevelID=105&LevelType=3&TopID=26");
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
        Response.Redirect("frmSaleReturnViewAccount.aspx?LevelID=105&LevelType=3&TopID=26");


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
            e.Row.FindControl("txtPrice").Focus();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtPrice = (TextBox)e.Row.FindControl("txtPrice");

        }

    }
}