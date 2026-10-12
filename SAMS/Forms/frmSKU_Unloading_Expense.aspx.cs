using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit SKU Price
/// </summary>
public partial class frmSKU_Unloading_Expense : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Populate_drpSKUCompany();
            Populate_drpSKUDivisions();
            Populate_drpSKUCategory();
            Populate_drpSKUBrand();
            LoadSKUS();
            LoadGrid();
            LoadDistributor();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void Populate_drpSKUCompany()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        clsWebFormUtil.FillDropDownList(this.ddskuPrincipal, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Divisions To Division Combo, Categories To Category Combo, Brands To Brand Combo, SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUDivisions();
        Populate_drpSKUCategory();
        Populate_drpSKUBrand();
        LoadSKUS();
        LoadGrid();
    }

    /// <summary>
    /// Loads Divisions To Division Combo
    /// </summary>
    private void Populate_drpSKUDivisions()
    {
        if (ddskuPrincipal.Items.Count > 0)
        {
            SkuHierarchyController mHer_Controller = new SkuHierarchyController();
            if (ddskuPrincipal.Items.Count   > 0)
            {
                DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, int.Parse(ddskuPrincipal.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
                clsWebFormUtil.FillDropDownList(this.ddskuDivision, m_dt, 0, 3, true);
            }
        }
    }

    /// <summary>
    /// Loads Categories To Category Combo, Brands To Brand Combo, SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUCategory();
        Populate_drpSKUBrand();
        LoadSKUS();
        LoadGrid();
    }
    
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        if (ddskuDivision.Items.Count > 0)
        {
            SkuHierarchyController mHer_Controller = new SkuHierarchyController();
            if (ddskuDivision.Items.Count   > 0)
            {
                DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(ddskuDivision.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
                clsWebFormUtil.FillDropDownList(this.ddskuCategory, m_dt, 0, 3, true);
            }
        }
    }

    /// <summary>
    /// Loads Brands To Brand Combo, SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUBrand();
        LoadSKUS();
        LoadGrid();
    }
    
    /// <summary>
    /// Loads Brands To Brand Combo
    /// </summary>
    private void Populate_drpSKUBrand()
    {
        if (ddskuCategory.Items.Count > 0)
        {
            SkuHierarchyController mHer_Controller = new SkuHierarchyController();
            string strSKUCategoryID = this.ddskuCategory.SelectedItem.Value;

            if (int.Parse(strSKUCategoryID) > 0)
            {
                DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, int.Parse(ddskuCategory.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
                clsWebFormUtil.FillDropDownList(this.ddskuBrand, m_dt, 0, 3, true);
            }
        }
    }

    /// <summary>
    /// Loads SKUS To SKU Combo And Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUS();
        LoadGrid();
    }
    
    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSKUS()
    {
        if (ddskuBrand.Items.Count > 0)
        {
            SkuController mSKUController = new SkuController();
            int compid = int.Parse(ddskuPrincipal.SelectedItem.Value);
            int divid = int.Parse(ddskuDivision.SelectedItem.Value);
            int catid = int.Parse(ddskuCategory.SelectedItem.Value);
            int barandid = int.Parse(ddskuBrand.SelectedItem.Value);
            int varid = Constants.IntNullValue;

            DataTable m_dt = mSKUController.SelectSkuInfo(compid, divid, catid, barandid, varid);
            clsWebFormUtil.FillDropDownList(this.ddskuName, m_dt, 0, 18, true);
        }     
    }

    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuName_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
    
    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    private void LoadGrid()
    {
        SKUPriceDetailController mSKUController = new SKUPriceDetailController();
        if (ddskuName.Items.Count > 0)
        {
            DataTable dtsku = mSKUController.SelectSKu_Unloading_expense(Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue,
                Constants.IntNullValue, int.Parse (rdbbtncheck.SelectedValue.ToString())  , Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(ddskuName.SelectedValue.ToString()));
              Grid_pricedetails.DataSource = dtsku.DefaultView;
               Grid_pricedetails.DataBind();
        }
        
    }
    
    /// <summary>
    /// Loads Locations To Location ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);


        //DistributorController DController = new DistributorController();
        //DataTable dt3 = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //DataTable dt2 = dt3.Clone();
        //DataRow[] dr = dt3.Select("SUBZONE_ID = 2");

        //if (dr.Length > 0)
        //{
        //    for (int k = 0; k < dr.Length; k++)
        //    {
        //        dt2.ImportRow(dr[k]);
        //    }
        //}

        //clsWebFormUtil.FillListBox(this.ChbDistributorList, dt2, 0, 2, true);
    }

    /// <summary>
    /// Save Or Updates an SKU Price
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();
            for(int i  = 0;i<ChbDistributorList.Items.Count;i++)   
            {
                if (ChbDistributorList.Items[i].Selected == true)
                {
                    mPriceController.InsertSKU_Expense_Rate(int.Parse(ChbDistributorList.Items[i].Value), int.Parse(ddskuName.SelectedValue.ToString()), 1, decimal.Parse(dc.chkNull_0(txtUnloading_expense.Text)), int.Parse(rdbbtncheck.SelectedItem.Value.ToString()));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Location');", true);
                }
            }
            this.LoadGrid();
            txtRetailPrice.Text = "";
            txtTradePrice.Text = "";
            txtUnloading_expense.Text = "";
            txtDistributorPrice.Text = ""; 
        }
    }
   
    protected void rdbbtncheck_SelectedIndexChanged(object sender, EventArgs e){
    
       this.LoadGrid();
       if (rdbbtncheck.SelectedIndex == 0)
       {
           lblpercentage.Visible = true;
       }
       else
       {
           lblpercentage.Visible = false;
       }

    }
}
