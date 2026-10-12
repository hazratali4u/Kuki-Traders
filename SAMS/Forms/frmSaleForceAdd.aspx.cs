using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;
using System.IO;

/// <summary>
/// From to Add, Edit, Delete Geo Heirarical Data
/// </summary>
public partial class Forms_frmSaleForceAdd : System.Web.UI.Page
{    
    VenderEntryController VendorCtl = new VenderEntryController();
    Distributor_UserController Duser = new Distributor_UserController();
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
            this.txtJoining.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            this.LoadDISTRIBUTOR();
            this.LoadDepartment();
            this.LoadDesignation();
            if (Request.QueryString["mode"] != null)
            {
                this.LoadEmployeeInformation();                
            }
        }
    }
    
    private void LoadEmployeeInformation()
    {
        DataTable dtEmployee = Duser.SelectUser(Convert.ToInt32(Session["EmployeeID"]), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        if (dtEmployee.Rows.Count > 0)
        {
            Session.Add("EmployeeID", dtEmployee.Rows[0]["USER_ID"]);
            txtEmployeeName.Text = dtEmployee.Rows[0]["USER_NAME"].ToString();
            txtFatherName.Text = dtEmployee.Rows[0]["FATHER_NAME"].ToString();
            txtEmployeeAddress.Text = dtEmployee.Rows[0]["ADDRESS1"].ToString();
            txtEmployeeAddress2.Text = dtEmployee.Rows[0]["ADDRESS2"].ToString();
            txtEmployeeCNIC.Text = dtEmployee.Rows[0]["CNIC_NO"].ToString();
            if (dtEmployee.Rows[0]["CNIC_EXPIRY"].ToString() != "")
            {
                txtEmployeeCNICExpiry.Text = Convert.ToDateTime(dtEmployee.Rows[0]["CNIC_EXPIRY"]).ToString("dd-MMM-yyyy");
            }
            
            txtEmployeeEmail.Text = dtEmployee.Rows[0]["EMAIL"].ToString();
            txtEmployeeContactNo.Text = dtEmployee.Rows[0]["MOBILE"].ToString();
            txtEmployeeMobileNo.Text = dtEmployee.Rows[0]["PHONE"].ToString();
            if (dtEmployee.Rows[0]["DOB"].ToString() != "")
            {
                txtDOB.Text = Convert.ToDateTime(dtEmployee.Rows[0]["DOB"]).ToString("dd-MMM-yyyy");
            }
            if (dtEmployee.Rows[0]["DOJoining"].ToString() != "")
            {
                txtJoining.Text = Convert.ToDateTime(dtEmployee.Rows[0]["DOJoining"]).ToString("dd-MMM-yyyy");
            }
            ddlMartial.SelectedValue = dtEmployee.Rows[0]["MARTIAL_STATUS_ID"].ToString();

            ddlDesignation.SelectedValue = dtEmployee.Rows[0]["DESIGNATION_ID"].ToString();
            ddlDepartment.SelectedValue = dtEmployee.Rows[0]["DEPARTMENT_ID"].ToString();
            ddlLocation.SelectedValue = dtEmployee.Rows[0]["DISTRIBUTOR_ID"].ToString();
            //Session["Pic"] = dtEmployee.Rows[0]["PIC"].ToString();
            btnSaveEmployee.Text = "Update";
        }
    }

    protected void btnSaveEmployee_Click(object sender, EventArgs e)
    {
        DateTime dtExpiry = Constants.DateNullValue;
        DateTime dtDOB = Constants.DateNullValue;
        DateTime dtDOJoining = Constants.DateNullValue;
        if (txtEmployeeCNICExpiry.Text.Length > 0)
        {
            dtExpiry = Convert.ToDateTime(txtEmployeeCNICExpiry.Text);
        }
        if (txtDOB.Text.Length > 0)
        {
            dtDOB = Convert.ToDateTime(txtDOB.Text);
        }
        if (txtJoining.Text.Length > 0)
        {
            dtDOJoining = Convert.ToDateTime(txtJoining.Text);
        }
        string fileName = null;
        if (fuPic.HasFile)
        {
            string path = Server.MapPath("~/Pics");
            string fExtension = "";
            FileInfo oFileInfo = new FileInfo(fuPic.PostedFile.FileName);
            fExtension = Path.GetExtension(fuPic.FileName);
            fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "pic" + fExtension;
            string fullFileName = path + "\\" + fileName;
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fuPic.PostedFile.SaveAs(fullFileName);
        }
        if (btnSaveEmployee.Text == "Save")
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

                if (Duser.InsertDistributor_User2(int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(ddlLocation.SelectedValue), Constants.IntNullValue, strcode, txtEmployeeName.Text, txtFatherName.Text, txtEmployeeCNIC.Text, dtExpiry, txtEmployeeAddress.Text, txtEmployeeAddress2.Text, txtEmployeeMobileNo.Text, txtEmployeeContactNo.Text, txtEmployeeEmail.Text, dtDOB, Convert.ToInt32(ddlMartial.SelectedValue)
                    , Convert.ToInt32(ddlDesignation.SelectedValue), Constants.IntNullValue, Convert.ToInt32(ddlDepartment.SelectedValue), dtDOJoining) > 0)
                {
                    mAutoCode.GetAutoCustomerCode("EM", 1, long.Parse(UserId.ToString()));
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Employee Added Successfully.');", true);
                }
        }
        else
        {
            if (Duser.UpdateDistributor_User2(Convert.ToInt32(Session["EmployeeID"]), int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(ddlLocation.SelectedValue), Constants.IntNullValue,txtEmployeeName.Text, txtFatherName.Text, txtEmployeeCNIC.Text, dtExpiry, txtEmployeeAddress.Text, txtEmployeeAddress2.Text, txtEmployeeMobileNo.Text, txtEmployeeContactNo.Text, txtEmployeeEmail.Text, dtDOB, Convert.ToInt32(ddlMartial.SelectedValue)
                    , Convert.ToInt32(ddlDesignation.SelectedValue), Convert.ToInt32(ddlDesignation.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue),dtDOJoining,true))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Employee Updated Successfully.');", true);
            }
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDISTRIBUTOR()
    {
        DistributorController mController = new DistributorController();
        DataTable dtDistributor = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        ddlLocation.DataSource = dtDistributor;
        ddlLocation.DataTextField = "DISTRIBUTOR_NAME";
        ddlLocation.DataValueField = "DISTRIBUTOR_ID";
        ddlLocation.DataBind();
    }

    /// <summary>
    /// Loads Designations To Designation Combo
    /// </summary>
    private void LoadDesignation()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.SaleForce, null, Constants.IntNullValue, true);
        ddlDesignation.DataSource = m_dt;
        ddlDesignation.DataTextField = "SLASH_DESC";
        ddlDesignation.DataValueField = "REF_ID";
        ddlDesignation.DataBind();
    }

    private void LoadDepartment()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Employee_Depoartment_Id, null, Constants.IntNullValue, true);
        ddlDepartment.DataSource = m_dt;
        ddlDepartment.DataTextField = "SLASH_DESC";
        ddlDepartment.DataValueField = "REF_ID";
        ddlDepartment.DataBind();
    }

    protected void btnCancelEmployee_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmSaleForce.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
    }
}