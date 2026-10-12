using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From To Assign/UnAssign Rotes And Principals To Sale Force
/// </summary>
public partial class Forms_frmSaleForceAreaAssignment : System.Web.UI.Page
{
    SaleForceController UController = new SaleForceController();
    GeoHierarchyController GControler = new GeoHierarchyController();

    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDISTRIBUTOR();
            this.LoadGrid();
            this.LoadArea();
            this.LoadAssisgned();
        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dtDistributor, 0, 2, true);

    }
        
    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    protected void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectDistributorUser(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()),Constants.IntNullValue, 669);
            clsWebFormUtil.FillDropDownList(this.DrpSaleForce, dt, 0, 6, true);    
        }
    }
    
    /// <summary>
    /// Loads Routes To Route ListBox
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(ChbAreaList, dt, "GEO_ID", "GEO_NAME");
        }
    }
        
    /// <summary>
    /// Checks/UnChecks Route ListBox And Principal ListBox According To Assinged/UnAssinged
    /// </summary>
    private void LoadAssisgned()
    {
        for (int nInner = 0; nInner < this.ChbAreaList.Items.Count; nInner++)
        {
            ChbAreaList.Items[nInner].Selected = false;
        }
              
        if (DrpSaleForce.Items.Count > 0)
        {
            #region Select Assign Area

            DataTable dtArea = UController.SelectSalesForceArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()), Constants.LongNullValue);

            for (int nOuter = 0; nOuter < dtArea.Rows.Count; nOuter++)
            {
                int AreaId = int.Parse(dtArea.Rows[nOuter]["Area_Id"].ToString());

                for (int nInner = 0; nInner < this.ChbAreaList.Items.Count; nInner++)
                {
                    if (int.Parse(ChbAreaList.Items[nInner].Value) == AreaId)
                    {
                        ChbAreaList.Items[nInner].Selected = true;
                        break;
                    }
                }
            }
            #endregion                        
        }
    }
    
    /// <summary>
    /// Loads Sale Forces To Sale Force Combo, Routes To Route Combo And Checks/UnChecks Route ListBox And Principal ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
        this.LoadAssisgned();
        this.UncheckSelectAll();
    }

    /// <summary>
    /// Loads Sale Forces To Sale Force Combo And Checks/UnChecks Route ListBox And Principal ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
        this.LoadAssisgned();
        this.UncheckSelectAll();
    }

    /// <summary>
    /// Checks/UnChecks Route ListBox And Principal ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpSaleForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAssisgned();
        this.UncheckSelectAll();
    }
    
    /// <summary>
    /// Inserts Routes And Principal Assignment
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        UController.UpdateSalesForceArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()));
        
        for (int i = 0; i < ChbAreaList.Items.Count; i++)
        {
            if (ChbAreaList.Items[i].Selected == true)
            {
                UController.InsertSalesForceArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()),int.Parse(ChbAreaList.Items[i].Value.ToString()));
            }
        }
        
        UController.UpdateSalesForcePrincipal(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()));

     }
    
    /// <summary>
    /// Uncheks "Select All" CheckBoxes
    /// </summary>
    private void UncheckSelectAll()
    {
        ChbSelectAll.Checked = false;
    }
}
