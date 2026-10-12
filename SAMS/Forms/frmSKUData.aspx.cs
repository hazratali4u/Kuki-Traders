using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{

    readonly SkuController mController = new SkuController();
     
    
    private  DataTable m_SKUDt;
    private static int m_sku_id;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("skuId");
           // ltrlNew.Text = "<a href='frmSKUDataAdd.aspx?LevelID=" + Request["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2'>Add Product</a>";
            ltrlNew.Text = "<a style='text-decoration:none'  href='frmSKUDataAdd.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=2'><img src='../images/addNew.png' height='20' width='20' alt='' />&nbsp;&nbsp; Add Product</a>";
           
            LoadData();
            LoadGrid();
        }
    }
        
    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    private void LoadData()
    {
        SkuController mSKUController = new SkuController();
        m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.Session.Add("m_SKUDt", m_SKUDt);
    }

    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)this.Session["m_SKUDt"];
        if (m_SKUDt != null)
        {
            if (!string.IsNullOrEmpty(txtSeach.Text))
            {
                switch (ddSearchType.SelectedIndex)
                {

                    case 1:
                        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 2:
                        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 3:
                        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 4:
                        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 5:
                        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 6:
                        m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    default:
                        m_SKUDt.DefaultView.RowFilter = "SKU_NAME" + " like '%" + "" + "%'";
                        break;
                }
            }
            grdSKUData.DataSource = m_SKUDt.DefaultView;
            grdSKUData.DataBind();
        }
    }

   /// <summary>
   /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        Session.Add("skuId" , int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[1].Text));

        Response.Redirect("frmSKUDataAdd.aspx?LevelID="+ Request.QueryString["LevelID"] + "&LevelType=" + Request.QueryString["LevelType"]+ "&TopID=2");
    }

   /// <summary>
   /// Deletes A SKU
   /// </summary>
   /// <param name="sender">object</param>
   /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {
       int m_sku_id = int.Parse(grdSKUData.Rows[e.RowIndex].Cells[1].Text);
       string result = mController.UpdateSKUS3(m_sku_id, int.Parse(this.Session["CompanyId"].ToString()), null, null,  Constants.IntNullValue
               ,  Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.ShortNullValue, Constants.DecimalNullValue
               , Constants.ShortNullValue, false, Convert.ToInt32(Session["UserID"]), Constants.CharNullValue);
       this.LoadData();
       this.LoadGrid();
   }

  
    /// <summary>
    /// Filters SKU From SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
}