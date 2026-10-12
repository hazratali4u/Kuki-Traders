using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUHierarchyData : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
    static int PrincipalId;
    static int DivisionId;
    static int CategoryId;
    static int BrandId;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  this.LoadPrincipal();
            //this.LoadDivision();
            this.LoadCategroy();
           // this.LoadBrand();
            TabContainer1.ActiveTabIndex = 0;
        }
    }
   //Commented Code model,type, natrure
    /*
    #region Model Tab

    /// <summary>
    /// Loads Principals To Principal Grid On Principal Tab And To All Principal Combos On Form
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable dt = mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()),true);
        GrdPrincipal.DataSource = dt;
        GrdPrincipal.Columns[0].Visible = true;
        GrdPrincipal.DataBind();
        GrdPrincipal.Columns[0].Visible = false;
    }

    /// <summary>
    /// Sets Principal Data For Edit. This Function Runs When An Existing Principal Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PrincipalId = int.Parse(GrdPrincipal.Rows[e.NewEditIndex].Cells[0].Text);
        txtPrincipalCode.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[1].Text;
        txtPrincipalName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[2].Text;
        txtPrincipalName.Enabled = true;
        btnSavePrincipal.Text = "Update";
    }

    /// <summary>
    /// Deletes A Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PrincipalId = int.Parse(GrdPrincipal.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateHierarchy(Constants.SKUPrincipal, PrincipalId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
        this.LoadPrincipal();
    }

    /// <summary>
    /// Sets PageIndex Of Principal Grid On Principal Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdPrincipal.PageIndex = e.NewPageIndex;
        this.LoadPrincipal();
    }

    /// <summary>
    /// Save Or Updates a Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSavePrincipal_Click(object sender, EventArgs e)
    {
        if (btnSavePrincipal.Text == "New")
        {
            txtPrincipalCode.Text = this.GetAutoCode("MO", 0);
            txtPrincipalName.Enabled = true;
            txtPrincipalName.Focus();
            btnSavePrincipal.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtPrincipalName);
        }
        else if (btnSavePrincipal.Text == "Save")
        {
            mController.InsertPrincipal(Constants.SKUPrincipal, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()),true ,"", "", "");
            this.SetAutoCode("MO", long.Parse(txtPrincipalCode.Text.Substring(2)));
            btnSavePrincipal.Text = "New";
            txtPrincipalCode.Text = "";
            txtPrincipalName.Text = "";
            txtPrincipalName.Enabled = false;
            this.LoadPrincipal();

        }
        else if (btnSavePrincipal.Text == "Update")
        {
            mController.UpdatePrincipal(Constants.SKUPrincipal, PrincipalId, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), true, "", "", "");
            btnSavePrincipal.Text = "New";
            txtPrincipalCode.Text = "";
            txtPrincipalName.Text = "";
            txtPrincipalName.Enabled = false;
            this.LoadPrincipal();
        }

    }

    #endregion

    #region Type Tab

    /// <summary>
    /// Loads Divisions To Division Grid On Division Tab
    /// </summary>
    private void LoadDivision()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        GrdDivision.DataSource = dt;
        GrdDivision.Columns[0].Visible = true;
        GrdDivision.DataBind();
        GrdDivision.Columns[0].Visible = false;
    }
    
    /// <summary>
    /// Sets Division Data For Edit. This Function Runs When An Existing Division Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdDivision_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DivisionId = int.Parse(GrdDivision.Rows[e.NewEditIndex].Cells[0].Text);
        txtDivisionCode.Text = GrdDivision.Rows[e.NewEditIndex].Cells[1].Text;
        txtDivisionName.Text = GrdDivision.Rows[e.NewEditIndex].Cells[2].Text;
        txtDivisionName.Enabled = true;
        btnSaveDivison.Text = "Update";
    }

    /// <summary>
    /// Deletes A Division
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdDivision_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DivisionId = int.Parse(GrdDivision.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateHierarchy(Constants.SKUDivision, DivisionId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
        this.LoadDivision();
    }

    /// <summary>
    /// Sets PageIndex Of Division Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdDivision.PageIndex = e.NewPageIndex;
        this.LoadDivision();
    }

    /// <summary>
    /// Save Or Updates a Division
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDivison_Click(object sender, EventArgs e)
    {
        if (btnSaveDivison.Text == "New")
        {
            txtDivisionCode.Text = this.GetAutoCode("TY", 0);
            txtDivisionName.Enabled = true;
            txtDivisionName.Focus();
            btnSaveDivison.Text = "Save";
        }
        else if (btnSaveDivison.Text == "Save")
        {
            mController.InsertHierarchy(Constants.SKUDivision, Constants.IntNullValue, txtDivisionCode.Text, txtDivisionName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("TY", long.Parse(txtDivisionCode.Text.Substring(2)));
            btnSaveDivison.Text = "New";
            txtDivisionCode.Text = "";
            txtDivisionName.Text = "";
            txtDivisionName.Enabled = false;
            this.LoadDivision();

        }
        else if (btnSaveDivison.Text == "Update")
        {
            mController.UpdateHierarchy(Constants.SKUDivision, DivisionId, Constants.IntNullValue, txtDivisionCode.Text, txtDivisionName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveDivison.Text = "New";
            txtDivisionName.Text = "";
            txtDivisionName.Text = "";
            txtDivisionName.Enabled = false;
            this.LoadDivision();
        }

    }

    #endregion
   
      
      #region Nature Tab
        
    /// <summary>
    /// Loads Brands To Brand Grid On Brand Tab
    /// </summary>
    private void LoadBrand()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        GrdBrand.DataSource = dt;
        GrdBrand.Columns[0].Visible = true;
        GrdBrand.DataBind();
        GrdBrand.Columns[0].Visible = false;
    }
    /// <summary>
    /// Sets Brand Data For Edit. This Function Runs When An Existing Brand Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdBrand_RowEditing(object sender, GridViewEditEventArgs e)
    {
        BrandId = int.Parse(GrdBrand.Rows[e.NewEditIndex].Cells[0].Text);
        txtBrandCode.Text = GrdBrand.Rows[e.NewEditIndex].Cells[1].Text;
        txtBrandName.Text = GrdBrand.Rows[e.NewEditIndex].Cells[2].Text;
        txtBrandName.Enabled = true;
        btnSaveBrand.Text = "Update";
    }

    /// <summary>
    /// Deletes A Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SkuController msku = new SkuController();
        BrandId = int.Parse(GrdBrand.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateHierarchy(Constants.SKUBrand, BrandId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
        this.LoadBrand();
    }

    /// <summary>
    /// Sets PageIndex Of Brand Grid On Brand Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdBrand.PageIndex = e.NewPageIndex;
        this.LoadBrand();
    }

    /// <summary>
    /// Save Or Updates a Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveBrand_Click(object sender, EventArgs e)
    {
        if (btnSaveBrand.Text == "New")
        {
            txtBrandCode.Text = this.GetAutoCode("NA", 0);
            txtBrandName.Enabled = true;
            txtBrandName.Focus();
            btnSaveBrand.Text = "Save";
        }
        else if (btnSaveBrand.Text == "Save")
        {
            mController.InsertHierarchy(Constants.SKUBrand, Constants.IntNullValue, txtBrandCode.Text, txtBrandName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("NA", long.Parse(txtBrandCode.Text.Substring(2)));
            btnSaveBrand.Text = "New";
            txtBrandCode.Text = "";
            txtBrandName.Text = "";
            txtBrandName.Enabled = false;
            this.LoadBrand();
        }
        else if (btnSaveBrand.Text == "Update")
        {
            mController.UpdateHierarchy(Constants.SKUBrand, BrandId, Constants.IntNullValue, txtBrandCode.Text, txtBrandName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveBrand.Text = "New";
            txtBrandCode.Text = "";
            txtBrandName.Text = "";
            txtBrandName.Enabled = false;
            this.LoadBrand();
        }
    }

    #endregion
  
    */
    #region Category Tab

    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    private void LoadCategroy()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()), 2);
        GrdCategory.DataSource = dt;
        GrdCategory.DataBind();
    }
    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CategoryId = int.Parse(GrdCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
        txtCategoryName.Enabled = true;
        btnSaveCategory.Text = "Update";
    }

    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CategoryId = int.Parse(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
        this.LoadCategroy();
    }

    /// <summary>
    /// Sets PageIndex Of Category Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdCategory.PageIndex = e.NewPageIndex;
        this.LoadCategroy();
    }

    /// <summary>
    /// Save Or Updates a Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        if (btnSaveCategory.Text == "New")
        {
            txtCategoryCode.Text = this.GetAutoCode("CA", 0);
            txtCategoryName.Enabled = true;
            txtCategoryName.Focus();
            btnSaveCategory.Text = "Save";
        }
        else if (btnSaveCategory.Text == "Save")
        {
            mController.InsertHierarchy(Constants.SKUCategory, Constants.IntNullValue, txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();

        }
        else if (btnSaveCategory.Text == "Update")
        {
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Constants.IntNullValue, txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
        }
    }

    #endregion

   

    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CodeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string PreeFix,int CodeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, Constants.LongNullValue);
    }
    
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CValue">Value</param>
    private void SetAutoCode(string PreeFix, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(PreeFix, 1, CValue);
    }
}