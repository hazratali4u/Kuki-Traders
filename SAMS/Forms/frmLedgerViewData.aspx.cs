using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form To Edit Vouchers
/// </summary>
public partial class Forms_frmLedgerViewData : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.VoucherType();
            this.LoadUser();
            btnView.Attributes.Add("onclick", "return ValidateForm();");
            SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            DrpVoucherType_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2,true);
    }
    
    /// <summary>
    /// Loads Voucher Types To Voucher Type Combo
    /// </summary>
    private void VoucherType()
    {
        LedgerController LController = new LedgerController();
        DataTable dt = LController.SelectVoucherType(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpVoucherType, dt, 0, 1, true);
    }
    
    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        Distributor_UserController mController = new Distributor_UserController();
        DataTable dt = mController.SelectGLUser();
        DrpUser.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));        
        clsWebFormUtil.FillDropDownList(this.DrpUser, dt, 0, 1);
    }

    /// <summary>
    /// Loads UnPosted Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnView_Click(object sender, EventArgs e)
    {
        LedgerController mController = new LedgerController();
        int VoucherType = Constants.IntNullValue;
        int VoucherType2 = Constants.IntNullValue;

        if (DrpVoucherType.SelectedValue == "16")
        {
            VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
        }
        else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
        {
            if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
            {
                VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else
            {
                VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
            }
        }

        int mUserID = Constants.IntNullValue;
        if (this.Session["RoleID"].ToString() == "3")
        {
            mUserID = Convert.ToInt32(this.Session["UserID"]);
        }
        DataTable dt = mController.SelectUnPostVoucherNo(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), mUserID,false, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0, VoucherType2);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();  
    }
    
    /// <summary>
    /// Shows Voucher in Crystal Report For Print
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void GrdLedger_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView(); 

        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        ds = RptAccountCtl.SelectUnpostVoucherForPrint(int.Parse(drpDistributor.SelectedValue.ToString()), GrdLedger.Rows[e.NewEditIndex].Cells[1].Text, Convert.ToInt32(GrdLedger.Rows[e.NewEditIndex].Cells[6].Text), Constants.IntNullValue);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());
        CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
        string VoucherType3 = "";
        if (DrpVoucherType.SelectedValue == "16")
        {

            VoucherType3 = DrpVoucherType.SelectedItem.Text;
        }
        else
        {
            VoucherType3 = DrpVoucherSubType.SelectedItem.Text;
        }
        CrpReport.SetParameterValue("VoucherSubType", VoucherType3);
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0); 
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);

                
    }

    /// <summary>
    /// Cancels Voucher Posting
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LedgerController lController = new LedgerController();
        foreach (GridViewRow dr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)dr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
                lController.PostSelectVoucher(int.Parse(drpDistributor.SelectedValue.ToString()), dr.Cells[1].Text, int.Parse(dr.Cells[6].Text), 1, DateTime.Parse(dr.Cells[7].Text));
            }
        }
        DataTable dt = lController.SelectUnPostVoucherNo(int.Parse(DrpVoucherType.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),Constants.IntNullValue,false, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),1);
        GrdLedger.DataSource = dt;
        GrdLedger.DataBind();
    }

    /// <summary>
    /// Stores Variables To Session And Redirects To Voucher Editing Form
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void GrdLedger_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {   
        this.Session.Add("VoucherNo", GrdLedger.Rows[e.RowIndex].Cells[1].Text);
        this.Session.Add("DistributorId", drpDistributor.SelectedValue.ToString());
        this.Session.Add("VoucherTypeId", GrdLedger.Rows[e.RowIndex].Cells[6].Text);

        string url = "'frmVoucherEditing.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=900,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Voucher in Crystal Report For Print
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnPrintVoucher_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        SAMSBusinessLayer.Reports.crpVoucherView CrpReport = new SAMSBusinessLayer.Reports.crpVoucherView();
        DataSet ds = null;        
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        ArrayList alVoucherNos = new ArrayList();

        foreach (GridViewRow gvr in GrdLedger.Rows)
        {
            CheckBox ChbSelect = (CheckBox)gvr.Cells[0].FindControl("ChbSelect");
            if (ChbSelect.Checked == true)
            {
                alVoucherNos.Add(gvr.Cells[1].Text);
            }
        }

        if (alVoucherNos.Count > 0)
        {

            int VoucherType = Constants.IntNullValue;
            int VoucherType2 = Constants.IntNullValue;

            if (DrpVoucherType.SelectedValue == "16")
            {
                VoucherType = Convert.ToInt32(DrpVoucherType.SelectedValue);
            }
            else if (DrpVoucherType.SelectedValue == "14" || DrpVoucherType.SelectedValue == "15")
            {
                if (DrpVoucherSubType.SelectedValue == Constants.IntNullValue.ToString())
                {
                    VoucherType2 = Convert.ToInt32(DrpVoucherType.SelectedValue);
                }
                else
                {
                    VoucherType = Convert.ToInt32(DrpVoucherSubType.SelectedValue);
                }
            }

            ds = DPrint.PrintVouchers(VoucherType, int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), 0, int.Parse(DrpUser.SelectedValue.ToString()), VoucherType2);

            DataTable dtSelectedVoucher = ds.Tables["RptVoucherView"].Clone();

            for (int i = 0; i < alVoucherNos.Count; i++)
            {
                DataRow[] foundRecord = ds.Tables["RptVoucherView"].Select("VOUCHER_NO = '" + alVoucherNos[i].ToString() + "'");
                if (foundRecord.Length > 0)
                {
                    for (int j = 0; j < foundRecord.Length; j++)
                    {
                        DataRow dr = dtSelectedVoucher.NewRow();
                        dtSelectedVoucher.ImportRow(foundRecord[j]);
                    }
                }
            }

            CrpReport.SetDataSource(dtSelectedVoucher);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Company_Name", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("DISTRIBUTOR_NAME", dt.Rows[0]["DISTRIBUTOR_NAME"].ToString());

            string VoucherType3 = "";
            if (DrpVoucherType.SelectedValue == "16")
            {
                
                VoucherType3 = DrpVoucherType.SelectedItem.Text;
            }
            else
            {
                VoucherType3 = DrpVoucherSubType.SelectedItem.Text;
            }
            CrpReport.SetParameterValue("VoucherType", DrpVoucherType.SelectedItem.Text);
            CrpReport.SetParameterValue("VoucherSubType", VoucherType3);
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Select at least one Voucher');", true);
        }
    }

    protected void DrpVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DrpVoucherSubType.Items.Clear();
        if (DrpVoucherType.SelectedValue == "14")
        {
          //  DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Receipt Voucher", "14"));
            DrpVoucherSubType.Items.Add(new ListItem("Cash Payment Voucher", "24"));
            DrpVoucherSubType.Enabled = true;
        }
        else if (DrpVoucherType.SelectedValue == "16")
        {
            DrpVoucherSubType.Enabled = false;
        }
        else
        {
          //  DrpVoucherSubType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Receipt Voucher", "15"));
            DrpVoucherSubType.Items.Add(new ListItem("Bank Payment Voucher", "17"));
            DrpVoucherSubType.Enabled = true;
        }
    }

    protected void GrdLedger_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            //Select All
            CheckBox ChbSelect = (CheckBox)e.Row.Cells[0].FindControl("ChbSelect");
            CheckBox ChbSelectHead = (CheckBox)this.GrdLedger.HeaderRow.FindControl("ChbSelectHead");
            ChbSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", ChbSelectHead.ClientID);
        }
    }
}
