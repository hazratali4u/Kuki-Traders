using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using SAMSBusinessLayer.Classes;
using SAMSCommonLayer.Classes;

/// <summary>
/// Summary description for wsAccountHead
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class wsAccountHead : System.Web.Services.WebService {

    public wsAccountHead () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetAccountHead(string prefixText, int count)
    {
        List<string> AccountHeads = new List<string>();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable DtAccountHead = mAccountController.SelectAccountHeadByName(prefixText);

        for (int i = 0; i < DtAccountHead.Rows.Count; i++)
        {
            AccountHeads.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(DtAccountHead.Rows[i]["ACCOUNT_HEAD"].ToString(), DtAccountHead.Rows[i]["ACCOUNT_HEAD_ID"].ToString()));
        }

        return AccountHeads.ToArray();
    }    
}
