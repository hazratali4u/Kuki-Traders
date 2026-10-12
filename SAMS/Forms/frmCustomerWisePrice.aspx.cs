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
public partial class Forms_frmCustomerWisePrice : System.Web.UI.Page
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

            LoadGroup();
            LoadSKUDetail();
            CreatTable();
            // LoadDistributor();
            txtDateEfected.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
        }
    }

    /// <summary>
    /// Creates Datatable For Order, Invoice And Sale Return
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("DATE_EFFECTED", typeof(string));

        Session.Add("PurchaseSKU", PurchaseSKU);
    }

    /// <summary>
    /// Loads SKU Data To SKU ListBox
    /// </summary>
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 3, Constants.DateNullValue);
        clsWebFormUtil.FillDropDownList(ddlSKuCde, Dtsku_Price, 0, 10, true);
        Session.Add("Dtsku_Price", Dtsku_Price);
    }

    /// <summary>
    /// Loads Order To Order Grid
    /// </summary>
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
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
        ddlSKuCde.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtUnitPrice.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;

        txtDateEfected.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        //txtskuCode.Enabled = false;
        hfSKUID.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes An SKU From Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        Session.Add("PurchaseSKU", PurchaseSKU);
        LoadGird();
    }

    /// <summary>
    /// Checks SKU in Order Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
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
    /// Clears Session Variables And Redircts To Step1
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CreatTable();
        LoadGird();
        ClearAll();
        Session.Remove("PurchaseSKU");
        txtGroupPrice.Text = "";
        txtDateEfected.Text = "";

    }

    /// <summary>
    /// Adds/Updates Row To Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSKuCde.SelectedValue + "'");

        if (btnSave.Text == "Add")
        {
            if (CheckDublicateSKU())
            {
                DataRow dr = PurchaseSKU.NewRow();
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["UNIT_PRICE"] = decimal.Parse(dc.chkNull_0(txtUnitPrice.Text));

                dr["DATE_EFFECTED"] = txtDateEfected.Text;
                PurchaseSKU.Rows.Add(dr);

                Session.Add("PurchaseSKU", PurchaseSKU);

                LoadGird();
                ClearAll();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = PurchaseSKU.Rows[RowId];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];

            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["UNIT_PRICE"] = decimal.Parse(dc.chkNull_0(txtUnitPrice.Text));

            dr["DATE_EFFECTED"] = txtDateEfected.Text;
            Session.Add("PurchaseSKU", PurchaseSKU);

            LoadGird();
            ClearAll();
        }

        ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);


    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {

        DataControl DC = new DataControl();
        PurchaseSKU = (DataTable)Session["PurchaseSKU"];
        OrderEntryController mOrderController = new OrderEntryController();
        bool IsValidInsert = true;

        if (btnSaveOrder.ToolTip.Equals("Save Group"))
        {
            IsValidInsert = mOrderController.Add_CustomerWisePrice(PurchaseSKU, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtGroupPrice.Text, Constants.DateNullValue, ChbIsActive.Checked);
        }
        else if (btnSaveOrder.ToolTip.Equals("Update Group"))
        {
            //edit code Update_CustomerWisePrice
            IsValidInsert = mOrderController.Update_CustomerWisePrice(PurchaseSKU, int.Parse(Session["UserId"].ToString()), DateTime.Parse(Session["CurrentWorkDate"].ToString()), txtGroupPrice.Text, Convert.ToInt32(drpAddEditGroup.SelectedValue), Constants.DateNullValue, ChbIsActive.Checked);

        }
        if (IsValidInsert)
        {
            ClearMasterALL();
            LoadGroup();
            txtGroupPrice.Text = "";
            txtDateEfected.Text = "";

            btnSaveOrder.ToolTip = "Save Group";
        }
    }

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {

        txtUnitPrice.Text = "";
        btnSave.Text = "Add";


        //   txtDateEfected.Text = "";
        //  txtGroupPrice.Text = "";

        //txtskuCode.Enabled = true;

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        Session.Remove("PurchaseSKU");

        CreatTable();
        LoadGird();
    }

    protected void drpAddEditGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (long.Parse(drpAddEditGroup.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            ClearAll();
            string strtemp = drpAddEditGroup.SelectedItem.ToString();
            string[] words = strtemp.Split('-');
            txtGroupPrice.Text = words[1];
            btnSaveOrder.ToolTip = "Update Group";
            int temp = Convert.ToInt32(drpAddEditGroup.SelectedValue);
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectGroupDetail(Convert.ToInt32(drpAddEditGroup.SelectedValue));
            //DataTable dt = (DataTable)Session["dtOrder"];
            // DataRow[] foundRows = dt.Select("SKU_ID   = '" + drpAddEditGroup.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {

                GrdPurchase.DataSource = dt;
                GrdPurchase.DataBind();

                Session.Add("PurchaseSKU", dt);
                txtDateEfected.Text = Convert.ToDateTime(dt.Rows[0]["DATE_EFFECTED"]).ToString("dd-MMM-yyyy");


                ScriptManager.GetCurrent(Page).SetFocus(ddlSKuCde);
                // //RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
            }
        }
        else
        {
            CreatTable();
            LoadGird();
            btnSaveOrder.ToolTip = "Save Group";
            ClearAll();
            ClearMasterALL();
            txtDateEfected.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
        }


    }

    private void LoadGroup()
    {
        drpAddEditGroup.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectGroup();
        drpAddEditGroup.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpAddEditGroup, dt, 0, 1);
    }

}
