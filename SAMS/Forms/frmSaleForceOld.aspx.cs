using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes; 

/// <summary>
/// From to Add, Edit Employee
/// </summary>
public partial class Forms_frmSaleForce : System.Web.UI.Page
{
    Distributor_UserController UController = new Distributor_UserController();
    static int UserId;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDISTRIBUTOR();
            this.LoadDesignation();
            LoadDepartment();
            this.LoadGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtJoinDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
        
        Response.Expires = 0;
        Response.Cache.SetNoStore();
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        ddDistributorId.DataSource = dtDistributor;
        ddDistributorId.DataTextField = "DISTRIBUTOR_NAME";
        ddDistributorId.DataValueField = "DISTRIBUTOR_ID";
        ddDistributorId.DataBind();
    }

    /// <summary>
    /// Loads Designations To Designation Combo
    /// </summary>
    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.SaleForce, null, Constants.IntNullValue, true);
        ddDesignation.DataSource = m_dt;
        ddDesignation.DataTextField = "SLASH_DESC";
        ddDesignation.DataValueField = "REF_ID";
        ddDesignation.DataBind();
    }
    private void LoadDepartment()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants .Employee_Depoartment_Id , null, Constants.IntNullValue, true);
        drpDepartment.DataSource = m_dt;
        drpDepartment.DataTextField = "SLASH_DESC";
        drpDepartment.DataValueField = "REF_ID";
        drpDepartment.DataBind();
    }
    
    /// <summary>
    /// Loads Active Employees To Employee Grid
    /// </summary>
    protected void LoadGrid()
    {
        if (ddDistributorId.Items.Count > 0)
        {
            DataTable dt = UController.SelectDistributorUser(Constants.IntNullValue, int.Parse(ddDistributorId.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.Grid_users.DataSource = dt;
            this.Grid_users.DataBind();
        }
    }

    /// <summary>
    /// Loads Employees To Employee Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddDistributorId_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }

    /// <summary>
    /// Sets Employee Data For Edit. This Function Runs When An Existing Employee Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow gvr = this.Grid_users.Rows[e.NewEditIndex];
            UserId  = int.Parse(gvr.Cells[0].Text);
            
           
                this.txtUserName.Text = gvr.Cells[2].Text.Replace("&nbsp;", "");
                this.txtNICNo.Text = gvr.Cells[3].Text.Replace("&nbsp;", "");
                this.txtPhoneNo.Text = gvr.Cells[4].Text.Replace("&nbsp;", "");
                this.txtMobileNo.Text = gvr.Cells[5].Text.Replace("&nbsp;", "");
                this.txtEmail.Text = gvr.Cells[6].Text.Replace("&nbsp;", "");
                this.txtAddress1.Text = gvr.Cells[7].Text.Replace("&nbsp;", "");
                this.txtAddress2.Text = gvr.Cells[8].Text.Replace("&nbsp;", "");
            this.chkIsActive.Checked = bool.Parse(gvr.Cells[10].Text);
            ddDesignation.SelectedValue = gvr.Cells[11].Text;

            txtJoinDate.Text = gvr.Cells[13].Text.Replace("&nbsp;","");
            if (gvr.Cells[15].Text != 0.ToString())
            {
              drpDepartment.SelectedValue = gvr.Cells[15].Text;
            } 
            btnSave.Text = "Update";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    /// <summary>
    /// Sets PageIndex Of Employee Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void Grid_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_users.PageIndex = e.NewPageIndex;
        this.LoadGrid(); 
    }

    /// <summary>
    /// Saves Or Updates An Employee.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();

                string strcode = "";
                UserId = int.Parse(mAutoCode.GetAutoCustomerCode("EM", 0, Constants.LongNullValue).ToString());

                if (UserId.ToString().Length == 1)
                {
                    strcode = "EM" + "000" + UserId.ToString();
                }
                else if (UserId.ToString().Length == 2)
                {
                    strcode = "EM" + "00" + UserId.ToString();
                }
                else if (UserId.ToString().Length == 3)
                {
                    strcode = "EM" + "0" + UserId.ToString();
                }
                else
                {
                    strcode = "EM" + UserId.ToString();
                }

                string SaleForceId = UController.InsertDistributor_User(int.Parse(this.Session["CompanyId"].ToString()), txtNICNo.Text, true, Convert.ToDateTime(txtJoinDate.Text), System.DateTime.Now, int.Parse(ddDesignation.SelectedValue.ToString()),
                    int.Parse(ddDistributorId.SelectedValue.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtLoginId.Text, txtpassword.Text
                    , txtMobileNo.Text, strcode, txtUserName.Text, txtPhoneNo.Text,int.Parse (drpDepartment .SelectedValue ));

                mAutoCode.GetAutoCustomerCode("EM", 1, long.Parse(UserId.ToString()));
            }
            else if (btnSave.Text == "Update")
            {
                UController.UpdateDistributor_User(UserId, int.Parse(this.Session["CompanyId"].ToString()), txtNICNo.Text, chkIsActive.Checked, Convert.ToDateTime(txtJoinDate.Text), System.DateTime.Now, int.Parse(ddDesignation.SelectedValue.ToString()),
                    int.Parse(ddDistributorId.SelectedValue.ToString()), Constants.IntNullValue, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtLoginId.Text, txtpassword.Text
                    , txtMobileNo.Text, null, txtUserName.Text, txtPhoneNo.Text,int .Parse (drpDepartment .SelectedValue ));
            }
            LoadGrid();
            ClearControls();
            btnSave.Text = "Save";
           
        }

        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Cancels Save Or Update Transaction
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGrid();
        ClearControls();
        btnSave.Text = "Save";
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    protected void ClearControls()
    {
        try
        {
            txtUserName.Text = null;
            txtPhoneNo.Text = null;
            txtMobileNo.Text = null;
            txtAddress1.Text = null;
            txtAddress2.Text = null;
            txtEmail.Text = null;
            txtLoginId.Text = null;
            txtpassword.Text = null;
            txtNICNo.Text = null;
            lblErrorMsg.Text = null;
            txtJoinDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
