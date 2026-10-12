using System;
using System.Data;
using SAMSCommonLayer.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Employee Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Employee
    /// </item>
    /// <term>
    /// Update Employee
    /// </term>
    /// <item>
    /// Get Employee
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class Distributor_UserController
    {
        #region Constructor

        /// <summary>
        /// Constructor for Distributor_UserController
        /// </summary>
        public Distributor_UserController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        /// <summary>
        /// Inserts Employee
        /// </summary>
        /// <remarks>
        /// Returns Inserted Employee ID
        /// </remarks>
        /// <param name="p_CompanyId">Company</param>
        /// <param name="p_NIC">NIC</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_Time_Stamp">CreatedOn</param>
        /// <param name="p_Lastupdate_Date">LastUpdateDate</param>
        /// <param name="p_User_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Role_Id">Role</param>
        /// <param name="p_Email_Address">Email</param>
        /// <param name="p_Address1">Address1</param>
        /// <param name="p_Address2">Address2</param>
        /// <param name="p_Login_Id">Login</param>
        /// <param name="p_Password">Password</param>
        /// <param name="p_Mobile">Mobile</param>
        /// <param name="p_User_Code">Code</param>
        /// <param name="p_User_Name">Name</param>
        /// <param name="p_Phone">Phone</param>
        /// <returns>Inserted Employee ID</returns>
   
        
        public string InsertDistributor_User(int p_CompanyId, string p_NIC, bool p_Is_Active, DateTime p_Join_Date, DateTime p_Lastupdate_Date, int p_User_Type_Id, int p_Distributor_Id,
          int p_Role_Id, string p_Email_Address, string p_Address1, string p_Address2, string p_Login_Id, string p_Password, string p_Mobile, string p_User_Code, string p_User_Name, string p_Phone, int p_Dept_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTOR_USER mDistributorUser = new spInsertDISTRIBUTOR_USER();
                mDistributorUser.Connection = mConnection;
                mDistributorUser.COMPANY_ID = p_CompanyId;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.NIC_NO = p_NIC;
                mDistributorUser.ADDRESS1 = p_Address1;
                mDistributorUser.ADDRESS2 = p_Address2;
                mDistributorUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributorUser.EMAIL = p_Email_Address;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.LOGIN_ID = p_Login_Id;
                mDistributorUser.MOBILE = p_Mobile;
                mDistributorUser.PASSWORD = p_Password;
                mDistributorUser.PHONE = p_Phone;
                mDistributorUser.ROLE_ID = p_Role_Id;
                mDistributorUser.TIME_STAMP = System.DateTime.Now;
                mDistributorUser.DATE_JOIN = p_Join_Date;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.USER_TYPE_ID = p_User_Type_Id;
                mDistributorUser.IS_ACTIVE = p_Is_Active;
                mDistributorUser.DEPARTMENT_ID = p_Dept_ID;

                mDistributorUser.ExecuteQuery();

                return mDistributorUser.USER_ID.ToString();

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

        public int InsertDistributor_User2(int p_COMPANY_ID, int p_DISTRIBUTOR_ID, int p_ROLE_ID, string p_USER_CODE, string p_USER_NAME, string p_FATHER_NAME, string p_CNIC_NO, DateTime p_CNIC_EXPIRY, string p_ADDRESS1, string p_ADDRESS2, string p_MOBILE, string p_PHONE
            , string p_EMAIL, DateTime p_DOB, int p_MARTIAL_STATUS_ID, int p_USER_TYPE_ID, int p_DESIGNATION_ID, int p_DEPARTMENT_ID, DateTime p_DOJoining)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTOR_USER2 mDistributorUser = new spInsertDISTRIBUTOR_USER2();
                mDistributorUser.Connection = mConnection;
                mDistributorUser.COMPANY_ID = p_COMPANY_ID;
                mDistributorUser.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDistributorUser.ROLE_ID = p_ROLE_ID;
                mDistributorUser.USER_CODE = p_USER_CODE;
                mDistributorUser.USER_NAME = p_USER_NAME;
                mDistributorUser.FATHER_NAME = p_FATHER_NAME;
                mDistributorUser.CNIC_NO = p_CNIC_NO;
                mDistributorUser.CNIC_EXPIRY = p_CNIC_EXPIRY;
                mDistributorUser.ADDRESS1 = p_ADDRESS1;
                mDistributorUser.ADDRESS2 = p_ADDRESS2;
                mDistributorUser.MOBILE = p_MOBILE;
                mDistributorUser.PHONE = p_PHONE;
                mDistributorUser.EMAIL = p_EMAIL;
                mDistributorUser.DOB = p_DOB;
                mDistributorUser.MARTIAL_STATUS_ID = p_MARTIAL_STATUS_ID;
                mDistributorUser.USER_TYPE_ID = p_USER_TYPE_ID;
                mDistributorUser.DESIGNATION_ID = p_DESIGNATION_ID;
                mDistributorUser.DEPARTMENT_ID = p_DEPARTMENT_ID;
                mDistributorUser.DOJoining = p_DOJoining;
                mDistributorUser.ExecuteQuery();

                return mDistributorUser.USER_ID;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return Constants.IntNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateDistributor_User2(int p_USER_ID, int p_COMPANY_ID, int p_DISTRIBUTOR_ID, int p_ROLE_ID, string p_USER_NAME, string p_FATHER_NAME, string p_CNIC_NO, DateTime p_CNIC_EXPIRY, string p_ADDRESS1, string p_ADDRESS2, string p_MOBILE, string p_PHONE
            , string p_EMAIL, DateTime p_DOB, int p_MARTIAL_STATUS_ID, int p_USER_TYPE_ID, int p_DESIGNATION_ID, int p_DEPARTMENT_ID, DateTime p_DOJoining, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDISTRIBUTOR_USER2 mDistributorUser = new spUpdateDISTRIBUTOR_USER2();
                mDistributorUser.Connection = mConnection;
                mDistributorUser.USER_ID = p_USER_ID;
                mDistributorUser.COMPANY_ID = p_COMPANY_ID;
                mDistributorUser.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDistributorUser.ROLE_ID = p_ROLE_ID;
                mDistributorUser.USER_NAME = p_USER_NAME;
                mDistributorUser.FATHER_NAME = p_FATHER_NAME;
                mDistributorUser.CNIC_NO = p_CNIC_NO;
                mDistributorUser.CNIC_EXPIRY = p_CNIC_EXPIRY;
                mDistributorUser.ADDRESS1 = p_ADDRESS1;
                mDistributorUser.ADDRESS2 = p_ADDRESS2;
                mDistributorUser.MOBILE = p_MOBILE;
                mDistributorUser.PHONE = p_PHONE;
                mDistributorUser.EMAIL = p_EMAIL;
                mDistributorUser.DOB = p_DOB;
                mDistributorUser.MARTIAL_STATUS_ID = p_MARTIAL_STATUS_ID;
                mDistributorUser.USER_TYPE_ID = p_USER_TYPE_ID;
                mDistributorUser.DESIGNATION_ID = p_DESIGNATION_ID;
                mDistributorUser.DEPARTMENT_ID = p_DEPARTMENT_ID;
                mDistributorUser.DOJoining = p_DOJoining;
                mDistributorUser.IS_ACTIVE = p_IS_ACTIVE;
                return mDistributorUser.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
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
        /// Updates Employee
        /// </summary>
        /// <remarks>
        /// Returns Updated Employee ID
        /// </remarks>
        /// <param name="UserId">Employee</param>
        /// <param name="p_CompanyId">Company</param>
        /// <param name="p_NIC">NIC</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_Time_Stamp">CreatedOn</param>
        /// <param name="p_Lastupdate_Date">LastUpdateDate</param>
        /// <param name="p_User_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Role_Id">Role</param>
        /// <param name="p_Email_Address">Email</param>
        /// <param name="p_Address1">Address1</param>
        /// <param name="p_Address2">Address2</param>
        /// <param name="p_Login_Id">Login</param>
        /// <param name="p_Password">Password</param>
        /// <param name="p_Mobile">Mobile</param>
        /// <param name="p_User_Code">Code</param>
        /// <param name="p_User_Name">Name</param>
        /// <param name="p_Phone">Phone</param>
        /// <returns>Updated Employee ID</returns>
        public string UpdateDistributor_User(int UserId, int p_CompanyId, string p_NIC, bool p_Is_Active, DateTime p_Join_Date, DateTime p_Lastupdate_Date, int p_User_Type_Id, int p_Distributor_Id,
              int p_Role_Id, string p_Email_Address, string p_Address1, string p_Address2, string p_Login_Id, string p_Password, string p_Mobile, string p_User_Code, string p_User_Name, string p_Phone, int p_Dept_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateDISTRIBUTOR_USER mDistributorUser = new spUpdateDISTRIBUTOR_USER();
                mDistributorUser.USER_ID = UserId;
                mDistributorUser.Connection = mConnection;
                mDistributorUser.COMPANY_ID = p_CompanyId;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.NIC_NO = p_NIC;
                mDistributorUser.ADDRESS1 = p_Address1;
                mDistributorUser.ADDRESS2 = p_Address2;
                mDistributorUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributorUser.EMAIL = p_Email_Address;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.LOGIN_ID = p_Login_Id;
                mDistributorUser.MOBILE = p_Mobile;
                mDistributorUser.PASSWORD = p_Password;
                mDistributorUser.PHONE = p_Phone;
                mDistributorUser.ROLE_ID = p_Role_Id;
                mDistributorUser.DATE_JOIN = p_Join_Date;
                mDistributorUser.TIME_STAMP = System.DateTime.Now;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.USER_TYPE_ID = p_User_Type_Id;
                mDistributorUser.IS_ACTIVE = p_Is_Active;
                mDistributorUser.DEPARTMENT_ID = p_Dept_ID;
                mDistributorUser.ExecuteQuery();

                return mDistributorUser.USER_ID.ToString();

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
        /// Gets Employee Data
        /// </summary>
        /// <remarks>
        /// Returns Employee Data as Datatable
        /// </remarks>
        /// <param name="p_Type">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Employee Data as Datatable</returns>
        public DataTable SelectDistributorUser(int p_Type, int p_Distributor_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectDISTRIBUTOR_USERInfo mDistUser = new uspSelectDISTRIBUTOR_USERInfo();
                mDistUser.Connection = mConnection;

                mDistUser.TYPE = p_Type;
                mDistUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistUser.COMPANY_ID = Companyid;

                DataTable dt = mDistUser.ExecuteTable();
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

        public DataTable SelectDistributorUser(int p_Type, int p_Distributor_Id, int Companyid, int p_DESIGNATION_ID, int p_DEPARTMENT_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectDISTRIBUTOR_USERInfo mDistUser = new uspSelectDISTRIBUTOR_USERInfo();
                mDistUser.Connection = mConnection;

                mDistUser.TYPE = p_Type;
                mDistUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistUser.COMPANY_ID = Companyid;
                mDistUser.DESIGNATION_ID = p_DESIGNATION_ID;
                mDistUser.DEPARTMENT_ID = p_DEPARTMENT_ID;

                DataTable dt = mDistUser.ExecuteTable();
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

        public DataTable SelectUser(int p_USER_ID, int p_Distributor_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDISTRIBUTOR_USER2 mDistUser = new spSelectDISTRIBUTOR_USER2();
                mDistUser.Connection = mConnection;

                mDistUser.USER_ID = p_USER_ID;
                mDistUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistUser.COMPANY_ID = Companyid;

                DataTable dt = mDistUser.ExecuteTable();
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
        /// Get All Employees
        /// </summary>
        /// <returns>All Employees Data as Datatable</returns>
        public DataTable SelectGLUser()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectGLUserDetail mDistUser = new UspSelectGLUserDetail();
                mDistUser.Connection = mConnection;
                DataTable dt = mDistUser.ExecuteTable();
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

    }
}
