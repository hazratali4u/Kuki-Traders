using System;
using System.Data;
using SAMSCommonLayer.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Company Related Tasks
    /// <remarks>
    /// <list type="bullet">
    /// <item>Insert Company</item>
    /// <item>Get Company</item>
    /// </list>
    /// </remarks>
    /// </summary>
	public class CompanyConfigrationController
	{

        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
		public CompanyConfigrationController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        /// <summary>
        /// Gets Company Data
        /// </summary>
        /// <remarks>
        /// Returns Company Data as Datatable
        /// </remarks>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <returns>Company Data as Datatable</returns>
       
        public DataTable SelectAllowances(int p_allowanceID,int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectAllowances mdtCompany = new spSelectAllowances();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.AllowanceID = p_allowanceID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectDeductions(int p_DeductionID, int p_CompanyID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDeductions mdtCompany = new spSelectDeductions();
                mdtCompany.Connection = mConnection;
                mdtCompany.CompanyID = p_CompanyID;
                mdtCompany.DeductionID = p_DeductionID;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
     
        public DataTable SelectEmployeeSalery(int p_DistributionID, int p_CustomerID,DateTime p_salaryMonth)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEMPLOYEE_SALARY mdtCompany = new spSelectEMPLOYEE_SALARY();
                mdtCompany.Connection = mConnection;
                mdtCompany.DISTRIBUTOR_ID = p_DistributionID;
                mdtCompany.EMPLOYEE_ID = p_CustomerID;
                mdtCompany.SALARY_MONTH = p_salaryMonth;
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectEmployeeAllowances(int p_SalaryID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEMPLOYEE_SALARY_ALLOWANCE mdtCompany = new spSelectEMPLOYEE_SALARY_ALLOWANCE();
                mdtCompany.Connection = mConnection;
                mdtCompany.SALARY_ID = p_SalaryID;
             
                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectEmployeeDeductions(int p_SalaryID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEMPLOYEE_SALARY_DEDUCTION mdtCompany = new spSelectEMPLOYEE_SALARY_DEDUCTION();
                mdtCompany.Connection = mConnection;
                mdtCompany.SALARY_ID = p_SalaryID;

                DataTable dt = mdtCompany.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        /// <summary>
        /// Inserts Company
        /// </summary>
        /// <remarks>
        /// Returns Inserted Company ID as String
        /// </remarks>
        /// <param name="p_ISCURRENT">IsCurrent</param>
        /// <param name="p_ISDELETED">IsDeleted</param>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <param name="p_EMAIL_ADDRESS">Email</param>
        /// <param name="p_PHONE">Phone</param>
        /// <param name="p_FAX">Fax</param>
        /// <param name="p_WEBSITE">Website</param>
        /// <param name="p_COMPANY_NAME">Name</param>
        /// <param name="p_ADDRESS1">Address1</param>
        /// <param name="p_ADDRESS2">Address2</param>
        /// <returns>Inserted Company ID as String</returns>
        public int InsertAllowances(int p_CompanyID ,string p_AllowanceDescription,decimal p_AllowanceRatio,int p_RatioType,int p_User_ID ,DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertAllowances mAccountHead = new spInsertAllowances();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.AllowanceDescription = p_AllowanceDescription;
                mAccountHead.AllowanceRatio = p_AllowanceRatio;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.RatioType = p_RatioType;
                mAccountHead.User_ID = p_User_ID;
                return mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdateAllowances(int p_AllowanceID ,int p_CompanyID, string p_AllowanceDescription,decimal p_AllowanceRatio,int p_RatioType,bool p_IS_DELETED,bool p_IS_ACTIVE,int p_User_ID )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateAllowances mAccountHead = new spUpdateAllowances();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.AllowanceDescription = p_AllowanceDescription;
                mAccountHead.AllowanceID = p_AllowanceID;
                mAccountHead.AllowanceRatio = p_AllowanceRatio;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.RatioType = p_RatioType;
                mAccountHead.User_ID = p_User_ID;
                return mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public int InsertDeductions(int p_CompanyID, string p_DeductionDescription, decimal p_DeductionRatio, int p_RatioType, int p_User_ID, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDeductions mAccountHead = new spInsertDeductions();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DeductionDescription = p_DeductionDescription;
                mAccountHead.DeductionRatio = p_DeductionRatio;
                mAccountHead.Document_Date = p_Document_Date;
                mAccountHead.RatioType = p_RatioType;
                mAccountHead.User_ID = p_User_ID;
                return mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdateDeductions(int p_DeductionID, int p_CompanyID, string p_DeductionDescription, decimal p_DeductionRatio, int p_RatioType, bool p_IS_DELETED, bool p_IS_ACTIVE, int p_User_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDeductions mAccountHead = new spUpdateDeductions();
                mAccountHead.Connection = mConnection;
                mAccountHead.CompanyID = p_CompanyID;
                mAccountHead.DeductionDescription = p_DeductionDescription;
                mAccountHead.DeductionID =p_DeductionID;
                mAccountHead.DeductionRatio = p_DeductionRatio;
                mAccountHead.IS_ACTIVE = p_IS_ACTIVE;
                mAccountHead.IS_DELETED = p_IS_DELETED;
                mAccountHead.RatioType = p_RatioType;
                mAccountHead.User_ID = p_User_ID;
                return mAccountHead.ExecuteQuery();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
     }
}