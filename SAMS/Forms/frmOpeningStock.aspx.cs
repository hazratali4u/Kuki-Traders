using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From To Adjust Stock
/// </summary>
public partial class Forms_frmOpeningStock : System.Web.UI.Page
{
    readonly SKUPriceDetailController PController = new SKUPriceDetailController();
    readonly DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreatTable();
            GetDocumentNo();
            LoadDistributor();
            LoadSKUDetail();
            txtExpDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Creates Datatable For Document
    /// </summary>
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

        Session.Add("PurchaseSKU", PurchaseSKU);
    }
    #region Sel/Index Change

    protected void DrpDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDocumentNo();
        if (DrpDocumentType.SelectedValue == "7")
        {
            txtPurchasePrice.Enabled = true;
        }
        else
        {

            txtPurchasePrice.Enabled = false;
            txtPurchasePrice.Text = "0.0";
        }
    }
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            CreatTable();
            LoadGird();
            drpDistributor.Enabled = true;
            DrpDocumentType.Enabled = true;
            ClearAll();
            txtDocumentNo.Text = "";
            DisAbaleOption(false);
        }
        else
        {
            LoadDocumentDetail();
            LoadSKUDetail();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }    
    
    #endregion 
  
    

    /// <summary>
    /// Gets Document Nos
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
      //  DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, int.Parse(Session["UserId"].ToString()), 0);
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(int.Parse(DrpDocumentType.SelectedValue.ToString()), Constants.IntNullValue, Constants.LongNullValue, Constants.IntNullValue, 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpDocumentNo, dt, 0, 0);
    }

    
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

   
   
    /// <summary>
    /// Loads SKU Detail To ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 2, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, "SKU_ID","SkuPriceDetail2", true);
        Session.Add("Dtsku_Price", Dtsku_Price);
    }
       
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
    }


    
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;

        txtPurchasePrice.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtExpDate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
 
        ddlSKuCde.Enabled = false;
        txtQuantity.Focus();

        btnSave.Text = "Update";

    }

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
        
    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
            txtDocumentNo.Text = dt.Rows[0][2].ToString();
            PurchaseSKU = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }

    /// <summary>
    /// Checks Duplicate SKU in Grid
    /// </summary>
    /// <returns>bool</returns>
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
      
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");
        if (foundRows.Length > 0)
        {
            PurchaseSKU = (DataTable)Session["PurchaseSKU"];

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


            if (btnSave.Text == "Add")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                  
                    dr["SKU_Name"] = ddlSKuCde.SelectedItem.Text;
                    
                    dr["FREE_SKU"] = 0;
                    if (DrpDocumentType.SelectedValue == "7")
                    {
                        dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtPurchasePrice.Text));
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        dr["AMOUNT"] = decimal.Parse(dr["PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    }
                    else
                    {
                        dr["PRICE"] = foundRows[0]["TRADE_PRICE"];
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        dr["AMOUNT"] = decimal.Parse(foundRows[0]["TRADE_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    }
                    dr["TIME_STAMP"] = dtExpDate;
                    dr["techId"] = foundRows[0]["TECHNOLOGY_ID"];
                    PurchaseSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('  " + ddlSKuCde.SelectedItem.Text + " Already Exists ');", true);
                   // txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSave.Text == "Update")
            {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    
                    dr["SKU_Name"] = ddlSKuCde.SelectedItem.Text;
                    
                    if (DrpDocumentType.SelectedValue == "7")
                    {
                        dr["PRICE"] = decimal.Parse(dc.chkNull_0(txtPurchasePrice.Text));
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        dr["AMOUNT"] = decimal.Parse(dr["PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    }
                    else
                    {
                        dr["PRICE"] = foundRows[0]["TRADE_PRICE"];
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));

                        dr["AMOUNT"] = decimal.Parse(foundRows[0]["TRADE_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    }
                    dr["FREE_SKU"] = 0;
                  //  dr["AMOUNT"] = decimal.Parse(foundRows[0]["TRADE_PRICE"].ToString()) * decimal.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["TIME_STAMP"] = dtExpDate;
                    dr["techId"] = foundRows[0]["TECHNOLOGY_ID"];
            }
            Session.Add("PurchaseSKU", PurchaseSKU);
            ClearAll();
            LoadGird();
            DisAbaleOption(true);
           
            ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong SKU please check in list');", true); 
        }
    }    
     
    /// <summary>
    /// Saves Document
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
        if (dt.Rows.Count > 0)
        {
            DateTime MWorkDate = DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString());

            PurchaseController mController = new PurchaseController();
            DataTable dtPurchaseDetail = (DataTable)Session["PurchaseSKU"];
            if (dtPurchaseDetail.Rows.Count > 0)
            {
                decimal mTotalAmount = 0;
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mTotalAmount += decimal.Parse(dr["AMOUNT"].ToString());

                }

                if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
                {
                    bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                          , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0, mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), 0, false);
                }
                else
                {
                    bool mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, int.Parse(DrpDocumentType.SelectedValue.ToString())
                       , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), 0
                       , mTotalAmount, false, dtPurchaseDetail, 0, null, int.Parse(Session["UserId"].ToString()), 0, false);
                }

                lblErrorMsg.Text = "Record Upated";
                PurchaseSKU = (DataTable)Session["PurchaseSKU"];
                PurchaseSKU.Rows.Clear();
                Session.Add("PurchaseSKU", PurchaseSKU);
                LoadGird();
                GetDocumentNo();
                drpDistributor.Enabled = true;                
                DrpDocumentType.Enabled = true;
                ClearAll();
                txtDocumentNo.Text = "";
                DisAbaleOption(false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert(' At least one SKU must enter');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Wrong Location or Unassigne');", true);
        }
    }
   
    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        drpDistributor.Enabled = true;
        DrpDocumentType.Enabled = true;
        ClearAll();
        txtDocumentNo.Text = "";
        DisAbaleOption(false);
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="IsDisable">bool</param>
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

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
       // txtskuCode.Text = "";
      //  txtskuName.Text = "";
        txtQuantity.Text = "";
        
        txtAmount.Text = "";
        txtPurchasePrice.Text = "0";
        ddlSKuCde.Enabled = true;
        btnSave.Text = "Add";
        lblErrorMsg.Text = "";

    }
}