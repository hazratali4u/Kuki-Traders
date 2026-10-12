using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommonLayer.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form To Add, Edit, SKU Group
/// </summary>
public partial class frmSKUGroups : System.Web.UI.Page
{
    SKUGroupController gController = new SKUGroupController();
    DataView dv ;
    private static int SKUGroupID = 0;

    /// <summary>
    /// Page_Load Function Populates All Combos, ListBox And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCategory(); 
            GetSKUName();
            LoadGrid();
        }
    }
    
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void LoadCategory()
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        DataTable dt = sController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddCatagory, dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Brands To Brand Combo And SKUS To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSKUName();
    }
    
    /// <summary>
    /// Loads SKUS To UnAssigned ListBox
    /// </summary>
    protected void GetSKUName()
    {
        SkuController mContoller = new SkuController();
        DataTable dt = mContoller.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddCatagory.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
        lstUnAssignSKU.DataSource = dt;
        lstUnAssignSKU.DataTextField = "SKU_NAME";
        lstUnAssignSKU.DataValueField = "SKU_ID";
        lstUnAssignSKU.DataBind();
    }

    /// <summary>
    /// Loads SKU Group Grid
    /// </summary>
    protected void LoadGrid()
    {
        DataTable dt = gController.GET_SKUGroupDetail(int.Parse(this.Session["CompanyId"].ToString()));
        dv = new DataView(dt);

        DataTable dt1 = gController.GET_Group_ID(int.Parse(this.Session["CompanyId"].ToString()));
        this.SKUGroup_Grid.DataSource = dt1;
        this.SKUGroup_Grid.DataBind();
        for (int i = 0; i < SKUGroup_Grid.Rows.Count; i++)
        {
            dv.RowFilter = "SKU_GROUP_ID = " + dt1.Rows[i]["SKU_GROUP_ID"].ToString();

            ListBox list = (ListBox)SKUGroup_Grid.Rows[i].Cells[2].FindControl("listbox1");
            list.DataSource = dv;
            list.DataTextField = "SKUDETAIL";
            list.DataValueField = "SKU_ID";
            list.DataBind();
        }
    }

    /// <summary>
    /// Sets SKU Group Data For Edit. This Function Runs When An Existing SKU Group Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void SKUGroup_Grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lstAssignSKU.Items.Clear();
        GridViewRow gvr = (GridViewRow)this.SKUGroup_Grid.Rows[e.NewEditIndex];
        SKUGroupID = int.Parse(gvr.Cells[0].Text);
        this.txtGroupName.Text = gvr.Cells[1].Text;
        ListBox list = (ListBox)gvr.Cells[2].FindControl("listbox1");
        for (int i = 0; i < list.Items.Count; i++)
        {

            lstAssignSKU.DataSource = list.DataSource;
            lstAssignSKU.Items.Add(list.Items[i].Text);
            lstAssignSKU.Items[i].Value = list.Items[i].Value;
            lstAssignSKU.DataBind();
        }

        btnSave.Text = "Update";
    }

    /// <summary>
    /// Sets PageIndex Of SKU Group Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void SKUGroup_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SKUGroup_Grid.PageIndex = e.NewPageIndex;
        LoadGrid();
    }

    /// <summary>
    /// Save Or Updates a SKU Group
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtGroupName.Text.Length == 0)
        {
            lblErrormsg.Text = "Must Enter Group Name";
            return; 
        }
        if (btnSave.Text == "Save")
        {
            DataTable dt_uniqueGroupName = GetUnique_GroupName();
            if (dt_uniqueGroupName.Rows.Count > 0)
            {
                lblErrormsg.Text = "Group Name already exists.";
                return;
            }
            lblErrormsg.Text = null;
            string ID = gController.Insert_SKUGroup(txtGroupName.Text, System.DateTime.Today, chIsActive.Checked, int.Parse(this.Session["UserId"].ToString()), 1, int.Parse(this.Session["CompanyId"].ToString()));
            
            for (int i = 0; i < lstAssignSKU.Items.Count; i++)
            {
                int SKU_ID = Convert.ToInt32(lstAssignSKU.Items[i].Value.ToString());
                gController.Insert_SKU_GroupDetail(int.Parse(ID), SKU_ID);
            }
        }
        if (btnSave.Text == "Update")
        {
            lblErrormsg.Text = null;
            Update();
        }
        LoadGrid();
        lstAssignSKU.Items.Clear();
        lstUnAssignSKU.Items.Clear();
        txtGroupName.Text = ""; 
        btnSave.Text = "Save"; 
    }
        
    /// <summary>
    /// Updates SKU Group
    /// </summary>
    protected void Update()
    {
              
        if (lstAssignSKU.Items.Count == 0)
        {
            gController.UpdateSKUinGroup(SKUGroupID, 0,txtGroupName.Text,chIsActive.Checked,1);
        }
        else
        {
            gController.UpdateSKUinGroup(SKUGroupID, 0, txtGroupName.Text, chIsActive.Checked,1);
            for (int i = 0; i < lstAssignSKU.Items.Count; i++)
            {
                gController.UpdateSKUinGroup(SKUGroupID, Convert.ToInt32(lstAssignSKU.Items[i].Value), txtGroupName.Text, chIsActive.Checked,1);
            }
        }
    }
    
    /// <summary>
    /// Gets Existing Group Names
    /// </summary>
    /// <remarks>
    /// Returns Existing Group Names as Datatable
    /// </remarks>
    /// <returns>Existing Group Names as Datatable</returns>
    protected DataTable GetUnique_GroupName()
    {
        DataTable dt = gController.GET_UniqueGroupName(Constants.IntNullValue, txtGroupName.Text, Constants.DateNullValue, true, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        return dt;       
    }
    
    /// <summary>
    /// Adds All SKUS From UnAssigned ListBox To SKU Group ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        int ItemCount = lstUnAssignSKU.Items.Count;

        for (int i = 0; i < ItemCount; i++)
        {
            this.lstAssignSKU.Items.Add(new clsListItems(lstUnAssignSKU.Items[i].Text, lstUnAssignSKU.Items[i].Value.ToString()));

        }
        lstUnAssignSKU.Items.Clear();   
    }

    /// <summary>
    /// Removes All SKUS From SKU Group ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnRemoveAll_Click(object sender, EventArgs e)
    {
        int ItemCount = lstAssignSKU.Items.Count;

        for (int i = 0; i < ItemCount; i++)
        {
            this.lstUnAssignSKU.Items.Add(new clsListItems(lstAssignSKU.Items[i].Text, lstAssignSKU.Items[i].Value.ToString()));
        }
        lstAssignSKU.Items.Clear(); 
    }

    /// <summary>
    /// Adds SKU From UnAssigned ListBox To SKU Group ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstUnAssignSKU.Items.Count > 0)
        {
            this.lstAssignSKU.Items.Add(new clsListItems(lstUnAssignSKU.SelectedItem.Text, lstUnAssignSKU.SelectedValue.ToString()));
            lstUnAssignSKU.Items.RemoveAt(lstUnAssignSKU.SelectedIndex);
        }

    }

    /// <summary>
    /// Removes SKU From SKU Group ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstAssignSKU.Items.Count > 0)
        {
            this.lstUnAssignSKU.Items.Add(new clsListItems(lstAssignSKU.SelectedItem.Text, lstAssignSKU.SelectedValue.ToString()));
            lstAssignSKU.Items.RemoveAt(lstAssignSKU.SelectedIndex);
        }
    }
}