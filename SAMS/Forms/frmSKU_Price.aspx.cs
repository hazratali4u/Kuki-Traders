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
public partial class SKU_Price : System.Web.UI.Page
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
            txtDateEffected.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
        }
    }

   
    private void Populate_drpSKUCompany()
    {
        //SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        //DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        //clsWebFormUtil.FillDropDownList(this.ddskuPrincipal, m_dt, 0, 3, true);
    }

    private void Populate_drpSKUDivisions()
    {
        //SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        //DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        //clsWebFormUtil.FillDropDownList(this.ddskuDivision, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        clsWebFormUtil.FillDropDownList(this.ddskuCategory, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Brands To Brand Combo
    /// </summary>
    private void Populate_drpSKUBrand()
    {
        //SkuHierarchyController mHer_Controller = new SkuHierarchyController();

        //DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        //clsWebFormUtil.FillDropDownList(this.ddskuBrand, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSKUS()
    {
        SkuController mSKUController = new SkuController();
        int compid = Constants.IntNullValue;
        int divid = Constants.IntNullValue;
        int catid = Convert.ToInt32(ddskuCategory.SelectedValue);
        int barandid = Constants.IntNullValue;
        int varid = Constants.IntNullValue;
        DataTable m_dt = mSKUController.SelectSkuInfo2(compid, divid, catid, barandid, varid);
        clsWebFormUtil.FillDropDownList(this.ddskuName, m_dt, 0, 1, true);
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

    protected void ddskuCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUS();
        this.LoadGrid();
    }
    
    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    private void LoadGrid()
    {
        Grid_pricedetails.DataSource = null;
        Grid_pricedetails.DataBind();

        SKUPriceDetailController mSKUController = new SKUPriceDetailController();
    
        if (ddskuName.Items.Count > 0)
        {
            DataTable dtsku = mSKUController.SelectSKuCurrentPrice2(Constants.IntNullValue,int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue,
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
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


        //DataTable dt2 = dt.Clone();
        //DataRow[] dr = dt.Select("SUBZONE_ID = 2");

        //if (dr.Length > 0)
        //{
        //    for (int k = 0; k < dr.Length; k++)
        //    {
        //        dt2.ImportRow(dr[k]);
        //    }
        //}
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);


        
       
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
                if(ChbDistributorList.Items[i].Selected == true)
                {
                    mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), int.Parse(ddskuName.SelectedValue.ToString()), 1, 0, 0, 0, decimal.Parse(dc.chkNull_0(txtTradePrice.Text)), 0, Convert.ToDateTime(txtDateEffected.Text),0);
                }
            }
            this.LoadGrid();
            txtTradePrice.Text = "";
        }
    }
}
