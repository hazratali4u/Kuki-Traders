using System;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using SAMSCommonLayer.Classes;
using CrystalDecisions.Shared;

public partial class Forms_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument CrpReport = (ReportDocument)Session["CrpReport2"];
        MemoryStream oStream = new MemoryStream();
        try
        {
            if (int.Parse(Session["ReportType2"].ToString()) == 0)
            {
               
                oStream = (MemoryStream)CrpReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
            }
            else
            {
                string path = SAMSCommonLayer.Classes.Configuration.GetAppInstallationPath() + "\\Exported.xls";
                CrpReport.SetDatabaseLogon("sa", "123");
                CrpReport.ExportToDisk(ExportFormatType.Excel, path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);

                if (file.Exists)
                {

                    Response.Clear();

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

                    Response.AddHeader("Content-Length", file.Length.ToString());

                    Response.ContentType = "application/octet-stream";

                    Response.WriteFile(file.FullName);

                    Response.End();

                }

                else
                {

                    Response.Write("This file does not exist.");

                }
            }
            
        }
        catch (Exception exp)
        {
            ExceptionPublisher.PublishException(exp);

        }
        finally
        {
            CrpReport.Close();
            CrpReport.Dispose();
            oStream.Close();
            oStream.Dispose();
            Session.Remove("CrpReport2");
            GC.Collect();
        }
    }
}