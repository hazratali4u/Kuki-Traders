using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using System.Data;
using SAMSCommonLayer.Classes;

public partial class Forms_frmAccountHeadMapping : System.Web.UI.Page
{
    AccountHeadController MController = new AccountHeadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            LoadAccountHead();
            LoadGrid();
        }
    }

    #region LOad

    
    private void LoadGrid()
    {
        DataTable dt = MController.SelectAssignAccountHead(Constants.IntNullValue, 1, Constants.IntNullValue);
        GrdAccount.DataSource = dt;
        GrdAccount.DataBind();
        Session.Add("dt", dt);
    }
    private void LoadAccountHead()
    {

        Configuration.GetAccountHead();

        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, Constants.LongNullValue);
        clsWebFormUtil.FillDropDownList(DrpCreditHead, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(DrpCashHead, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(drpFreight, dt, 0, 11, true);
        clsWebFormUtil.FillDropDownList(drpTax, dt, 0, 11, true);

    }

    #endregion

    #region Grid Operations

    protected void GrdAccount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hf_ID.Value = GrdAccount.Rows[e.NewEditIndex].Cells[0].Text;
        
        DrpCreditHead.SelectedValue = GrdAccount.Rows[e.NewEditIndex].Cells[1].Text;
        DrpCashHead.SelectedValue = GrdAccount.Rows[e.NewEditIndex].Cells[2].Text;
        drpFreight.SelectedValue = GrdAccount.Rows[e.NewEditIndex].Cells[3].Text;
        drpTax.SelectedValue = GrdAccount.Rows[e.NewEditIndex].Cells[4].Text;

        btnSave.Text = "Update";
    }
    protected void GrdAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        MController.SelectAssignAccountHead(int.Parse(GrdAccount.Rows[e.RowIndex].Cells[0].Text), 2, Constants.IntNullValue);
        LoadGrid();
        ClearAll();
        //ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Transaction exists unable to delete');", true);

    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (hf_ID.Value == "")
        //{
        //    hf_ID.Value = Convert.ToString(Constants.IntNullValue);
        //}
        ////bool flag = MController.SaveAccountAssignment(int.Parse(hf_ID.Value),
        ////int.Parse(DrpCashHead.SelectedValue), int.Parse(DrpCreditHead.SelectedValue),
        ////int.Parse(drpFreight.SelectedValue), int.Parse(drpTax.SelectedValue),
        ////DateTime.Parse(Session["CurrentWorkDate"].ToString()), int.Parse(Session["UserID"].ToString()));

        //if (flag)
        //{
        //    ClearAll();

        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Account assignment saved successfully.');", true);
        //    LoadGrid();
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Some error occurred.');", true);
        //}
    }

    private void ClearAll()
    {
        
        DrpCreditHead.SelectedIndex = 0;
        DrpCashHead.SelectedIndex = 0;
        drpFreight.SelectedIndex = 0;
        drpTax.SelectedIndex = 0;

        btnSave.Text = "Save";
    }   
}