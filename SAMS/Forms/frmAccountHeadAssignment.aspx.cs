using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Form For Account Head Assignment
/// </summary>
public partial class Forms_frmAccountHeadAssignment : System.Web.UI.Page
{
    AccountHeadController MController = new AccountHeadController();

    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.GetAccountType();
            this.GetSubTypeForHead();
            this.GetDetailAccountTypeForHead();
            this.GetAccountHead();
            this.LoadAssisgned();
        }
    }

    /// <summary>
    /// Loads Distributors To Distributor Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Account MainTypes To MainType Combo
    /// </summary>
    private void GetAccountType()
    {
        DataTable dt = MController.SelectAccountHead(Constants.AC_MainTypeId, Constants.LongNullValue, DrpAccountCategory.SelectedIndex);
        clsWebFormUtil.FillListBox(this.ChAccountHead, dt, 0, 4, true);
        clsWebFormUtil.FillDropDownList(DrpMainAccountType, dt, 0, 10, true);
        if (dt.Rows.Count > 0)
        {   
            ChAll.Visible = true;
            Panel2.Visible = true;
            lblAccountHead.Visible = true;
        }
        else
        {
            ChAll.Visible = false;
            Panel2.Visible = false;
            lblAccountHead.Visible = false;
        }
    }

    /// <summary>
    /// Loads Account SubTypes To SubType Combo
    /// </summary>
    private void GetSubTypeForHead()
    {
        if (DrpMainAccountType.Items.Count > 0)
        {
            DataTable dt = MController.SelectAccountHead(Constants.AC_SubTypeId, long.Parse(DrpMainAccountType.SelectedValue.ToString()), DrpAccountCategory.SelectedIndex);
            clsWebFormUtil.FillDropDownList(DrpSubAccountType, dt, 0, 10, true);
        }
    }

    /// <summary>
    /// Loads Account DetailTypes To DetailType Combo
    /// </summary>
    private void GetDetailAccountTypeForHead()
    {
        if (DrpSubAccountType.Items.Count > 0)
        {
            DataTable dt = MController.SelectAccountHead(Constants.AC_DetailTypeId, int.Parse(DrpSubAccountType.SelectedValue.ToString()), DrpAccountCategory.SelectedIndex);
            clsWebFormUtil.FillDropDownList(DrpDetailAccountType, dt, 0, 10, true);
        }
    }

    /// <summary>
    /// Loads Account Heads To ListBox
    /// </summary>
    private void GetAccountHead()
    {
        if (DrpDetailAccountType.Items.Count > 0)
        {
            DataTable dt = MController.SelectAccountHead(Constants.AC_AccountHeadId, int.Parse(DrpDetailAccountType.SelectedValue.ToString()), DrpAccountCategory.SelectedIndex);
            clsWebFormUtil.FillListBox(this.ChAccountHead, dt, 0, 4, true);
            if (dt.Rows.Count > 0)
            {                
                ChAll.Visible = true;
                Panel2.Visible = true;
                lblAccountHead.Visible = true;
            }
            else
            {
                ChAll.Visible = false;
                Panel2.Visible = false;
                lblAccountHead.Visible = false;                
            }
        }
    }

    /// <summary>
    /// Loads Account SubTypes, DetailTypes, AccountHeads And Checks/UnChecks ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpMainAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetSubTypeForHead();
        this.GetDetailAccountTypeForHead();
        this.GetAccountHead();
        this.LoadAssisgned();
    }

    /// <summary>
    /// Loads DetailTypes, AccountHeads And Checks/UnChecks ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpSubAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDetailAccountTypeForHead();
        this.GetAccountHead();
        this.LoadAssisgned();
    }

    /// <summary>
    /// Loads AccountHeads And Checks/UnChecks ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDetailAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetAccountHead();
        this.LoadAssisgned();
    }

    /// <summary>
    /// Loads Account MainTypes,SubTypes, DetailTypes, AccountHeads And Checks/UnChecks ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpAccountCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetAccountType();
        this.GetSubTypeForHead();
        this.GetDetailAccountTypeForHead();
        this.GetAccountHead();
        this.LoadAssisgned();
    }

    /// <summary>
    /// Assigns/UnAssigns Account Head To Distributor
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (ChAccountHead.Items.Count > 0)
        {
            bool flag = true;
            for (int i = 0; i < ChAccountHead.Items.Count; i++)
            {
                flag = MController.Assign_UnAssign_AccountHead(Convert.ToInt32(ChAccountHead.Items[i].Value), Convert.ToInt32(ddlDistributor.SelectedValue), Convert.ToInt32(rblType.SelectedValue), !ChAccountHead.Items[i].Selected);
            }
            if (flag)
            {
                this.LoadAssisgned();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Account Heads assigned successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error in Voucher')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Account Head found')", true);
        }
    }

    /// <summary>
    /// Checks/UnChecks AccountHead ListBox
    /// </summary>
    private void LoadAssisgned()
    {
        #region Assign UnAssign Account Head
        if (ddlDistributor.Items.Count > 0)
        {

            DataTable dtAccount = MController.GetAssign_UnAssign_AccountHead(Convert.ToInt32(ddlDistributor.SelectedValue),Convert.ToInt32(rblType.SelectedValue));

            for (int nOuter = 0; nOuter < dtAccount.Rows.Count; nOuter++)
            {
                for (int nInner = 0; nInner < this.ChAccountHead.Items.Count; nInner++)
                {
                    if (Convert.ToInt32(ChAccountHead.Items[nInner].Value) == Convert.ToInt32(dtAccount.Rows[nOuter]["ACCOUNT_HEAD_ID"]) && Convert.ToInt32(ddlDistributor.SelectedValue) == Convert.ToInt32(dtAccount.Rows[nOuter]["PRINCIPAL_ID"]))
                    {
                        ChAccountHead.Items[nInner].Selected = true;
                        break;
                    }
                }
            }
        }
            #region CheckUnCheck Select All Radio Button
            int CheckedItems = 0;
            for (int count = 0; count < ChAccountHead.Items.Count; count++)
            {
                if (ChAccountHead.Items[count].Selected == true)
                {
                    CheckedItems += 1;
                }
            }
            if (CheckedItems == ChAccountHead.Items.Count)
            {
                ChAll.Checked = true;
            }
            else
            {
                ChAll.Checked = false;
            }
            #endregion
        

        #endregion
    }

    /// <summary>
    /// Loads AccountHeads And Checks/UnChecks ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetAccountHead();
        this.LoadAssisgned();
    }

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetAccountType();
        this.GetSubTypeForHead();
        this.GetDetailAccountTypeForHead();
        this.GetAccountHead();
        this.LoadAssisgned();
    }
}
