using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.IO;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUDataAdd : System.Web.UI.Page
{

    readonly SkuController mController = new SkuController();
    readonly DataControl dc = new DataControl();
    readonly SkuHierarchyController mHer_Controller = new SkuHierarchyController();
    
    private  DataTable m_dt,m_SKUDt;
   

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
               
                Populate_drpSKUCategory();
                if (Session["skuId"] != null)
                {
                    m_SKUDt = mController.SelectSkuInfo(int.Parse(Session["skuId"].ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
                    if (m_SKUDt != null)
                    {
                        if (m_SKUDt.Rows.Count > 0)
                        {
                            txtskuname.Text = m_SKUDt.Rows[0]["SKU_NAME"].ToString();
                            txtPackSize.Text = m_SKUDt.Rows[0]["PACKSIZE"].ToString().Replace("&nbsp","");
                            ddlCategory.SelectedValue = m_SKUDt.Rows[0]["TECHNOLOGY_ID"].ToString();
                            ddlSource.SelectedValue = m_SKUDt.Rows[0]["SOURCE_ID"].ToString();
                            txtMaximumStock.Text = m_SKUDt.Rows[0]["MAX_STOCK"].ToString();
                            txtMinimumStock.Text = m_SKUDt.Rows[0]["MIN_STOCK"].ToString();
                            txtunitincase.Text = m_SKUDt.Rows[0]["UNITS_IN_CASE"].ToString();
                         //   txtSkuweight.Text = m_SKUDt.Rows[0]["SKU_WEIGHT"].ToString();
                           
                         //   ddlUOM.SelectedValue = m_SKUDt.Rows[0]["UOM"].ToString();

                            ddlGST.SelectedValue = m_SKUDt.Rows[0]["GST_ON"].ToString().Trim();
                           
                            btnSave.Text = "Update";
                            chkIsActive.Checked = Convert.ToBoolean(m_SKUDt.Rows[0]["ISACTIVE"]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

   
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        ddlCategory.Items.Clear();   
        
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()),2);
                clsWebFormUtil.FillDropDownList(ddlCategory, m_dt, 0, 3, true);

    }
        
    

   /// <summary>
   /// Save Or Updates an SKU
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {       
       if (txtskuname.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter Item Name";
           return;
       }

       
       if(btnSave.Text == "Save") 
       {
           mController.InsertSKUS3(int.Parse(Session["CompanyId"].ToString()), txtskuname.Text, txtPackSize.Text, Convert.ToInt32(ddlCategory.SelectedValue)
               , Convert.ToInt32(ddlSource.SelectedValue), Convert.ToInt32(dc.chkNull_0(txtMaximumStock.Text))
               , Convert.ToInt32(dc.chkNull_0(txtMinimumStock.Text)), Convert.ToInt16(dc.chkNull_0(txtunitincase.Text))
               , Constants.ShortNullValue, Constants.DecimalNullValue, Convert.ToInt32(Session["UserID"]), Convert.ToChar(ddlGST.SelectedValue));
          
           CLearAll();  
       }
       else if (btnSave.Text == "Update")
       {
           mController.UpdateSKUS3(int.Parse(Session["skuId"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtskuname.Text, txtPackSize.Text
               , Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlSource.SelectedValue), Convert.ToInt32(dc.chkNull_0(txtMaximumStock.Text))
               , Convert.ToInt32(dc.chkNull_0(txtMinimumStock.Text)), Convert.ToInt16(dc.chkNull_0(txtunitincase.Text))
               , Constants.DecimalNullValue, Constants.ShortNullValue, chkIsActive.Checked, Convert.ToInt32(Session["UserID"]), Convert.ToChar(ddlGST.SelectedValue));
          
           CLearAll();

           Session.Remove("skuId");
       }

       Response.Redirect("frmSKUData.aspx?LevelID=" + Request.QueryString["LevelID"] + "&LevelType=" + Request.QueryString["LevelType"] + "&TopID=2");
    }
  

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void CLearAll()
    {
        txtMaximumStock.Text = "";
       
        txtunitincase.Text = "";
        txtskuname.Text = "";
        txtPackSize.Text = "";
        txtMinimumStock.Text = "";
        
        btnSave.Text = "Save";
        //txtSkuweight.Text = "";        
    }
}