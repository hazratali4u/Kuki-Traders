using System;
using System.Data;
using SAMSCommonLayer.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Customer Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Customer
    /// </item>
    /// <term>
    /// Update Customer
    /// </term>
    /// <item>
    /// Get Customer
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class CustomerDataController
	{
        #region Constructor

        /// <summary>
        /// Constructor For CustomerDataController
        /// </summary>
        public CustomerDataController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

		#region Private Variables

		IDbTransaction mTransaction;
		IDbConnection mConnection;
		
        #endregion

        #region Public Methods

        #region Select
        public DataTable GetVendorOpening(int p_Distributor_Id, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetVendorLedger mDistGeoArea = new uspGetVendorLedger();
                mDistGeoArea.Connection = mConnection;
                mDistGeoArea.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistGeoArea.DOCUMENT_DATE = p_Document_Date;
                DataTable dt = mDistGeoArea.ExecuteTable();
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

        public DataTable GetVendorPayment(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, DateTime p_VENDOR_LEDGER_DATE, int p_PAYMENT_MODE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetVendorPayment mLedgerDate = new uspGetVendorPayment();
                mLedgerDate.Connection = mConnection;
                mLedgerDate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mLedgerDate.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mLedgerDate.VENDOR_LEDGER_DATE = p_VENDOR_LEDGER_DATE;
                mLedgerDate.PAYMENT_MODE = p_PAYMENT_MODE;
                DataTable dt = mLedgerDate.ExecuteTable();
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
        /// Gets Customer Credit Limit
        /// </summary>
        /// <remarks>
        /// Returns Customer Credit Limit as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_CustomerId">Customer</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>Customer Credit Limit as Datatable</returns>
        public DataTable SelectCustomerCreditLimit(int p_Distributor_Id,long p_CustomerId,int p_Principal_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCUSTOMER_PRINCIPAL_CREDITLIMIT mCustomer = new spSelectCUSTOMER_PRINCIPAL_CREDITLIMIT();
                mCustomer.Connection = mConnection;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.CUSTOMER_ID = p_CustomerId;
                mCustomer.PRINCIPAL_ID = p_Principal_Id;
                DataTable dt = mCustomer.ExecuteTable();  
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
        /// Gets All Customers Opening Credit
        /// </summary>
        /// <remarks>
        /// Returns All Customers Opening Credit as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Document_Date"></param>
        /// <returns>All Customers Opening Credit as Datatable</returns>
        public DataTable SelectOpeningCredit(int p_Distributor_Id, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectOpCredit mDistGeoArea = new UspSelectOpCredit();
                mDistGeoArea.Connection = mConnection;
                mDistGeoArea.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistGeoArea.DOCUMENT_DATE = p_Document_Date;
                DataTable dt = mDistGeoArea.ExecuteTable();
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

        public DataTable SelectOpeningCredit(int p_Distributor_Id, long pCustomerId, DateTime p_Document_Date, int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectOpCredit mDistGeoArea = new UspSelectOpCredit();
                mDistGeoArea.Connection = mConnection;
                mDistGeoArea.DISTRIBUTOR_ID = p_Distributor_Id;
               
                mDistGeoArea.CUSTOMER_ID = pCustomerId;
                mDistGeoArea.typeId = pType;
                DataTable dt = mDistGeoArea.ExecuteTable();
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
        /// Gets Customers RouteWise
        /// </summary>
        /// <remarks>
        /// Returns Customers RouteWise as Datatable
        /// </remarks>
        /// <param name="strRoutes">Routes</param>
        /// <param name="strCustomerTypes">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <returns>Customers RouteWise as Datatable</returns>
        public DataTable SelectCustomerByRoute(string strRoutes, string strCustomerTypes, int p_Distributor_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectRouteCustomer mCustomer = new uspSelectRouteCustomer();
                mCustomer.Connection = mConnection;

                mCustomer.ROUTES = strRoutes;
                mCustomer.CUSTOMER_TYPES = strCustomerTypes;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                DataTable dt = mCustomer.ExecuteTable();
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
        /// Gets Customer With Invoices
        /// </summary>
        /// <remarks>
        /// Retruns Customers With Invoices as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <param name="p_document_date">Date</param>
        /// <returns>Customers With Invoices as Datatable</returns>
        public DataTable SelectPrincipalCustomer(int p_Distributor_Id, int p_PrincipalId, DateTime p_document_date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectInvoiceBuiltyInfo mCustData = new UspSelectInvoiceBuiltyInfo();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.PRINCIPAL_ID = p_PrincipalId;
                mCustData.Document_date = p_document_date;
                mCustData.CUSTOMER_ID = Constants.LongNullValue;
                mCustData.PRINCIPAL_ID = p_PrincipalId;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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
        /// Gets Customers
        /// </summary>
        /// <remarks>
        /// Returns Customers as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Area">Market</param>
        /// <param name="p_Route">Route</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>Customers as Datatable</returns>
        public DataTable SelectPrincipalCustomer(int p_Distributor_Id, int p_Area, int p_Route, int p_PrincipalId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPRINCIPALCUSTOMER mCustData = new UspSelectPRINCIPALCUSTOMER();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.AREA_ID = p_Area;
                mCustData.ROUTE_ID = p_Route;
                mCustData.PRINCIPAL_ID = p_PrincipalId;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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


        public DataTable DOCustomers(int pDistributorId, int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDeleiveryOrder mCustData = new spSelectDeleiveryOrder();
                mCustData.Connection = mConnection;

                mCustData.dsitributorId = pDistributorId;
               
                mCustData.typeId = pType;
                
                DataTable dt = mCustData.ExecuteTable();
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
        public DataTable DODetail(int pDistributorId, int pCustomerId, int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDeleiveryOrder mCustData = new spSelectDeleiveryOrder();
                mCustData.Connection = mConnection;

                mCustData.dsitributorId = pDistributorId;
                mCustData.customerId = pCustomerId;
                mCustData.typeId = pType;

                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customers
        /// </summary>
        /// <remarks>
        /// Returns Customers as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Area">Market</param>
        /// <param name="p_Route">Route</param>
        /// <returns>Customers as Datatable</returns>
        public DataTable SelectAllCustomer(int p_Distributor_Id, int p_Area, int p_Town,int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER
                {
                    Connection = mConnection,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    //AREA_ID = p_Area,
                    townId = p_Town,
                    type=pType
                };

                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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
        public DataTable SelectAllCustomer(int p_Distributor_Id, int p_Area, int p_Town)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER
                {
                    Connection = mConnection,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    //AREA_ID = p_Area,
                    townId = p_Town,
                  
                };

                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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
        public DataTable GetCustomerByID(int p_Distributor_Id, long p_CUSTOMER_ID,int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER
                {
                    Connection = mConnection,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID,
                    type=pType
                };

                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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

        public DataTable GetCustomerBrand(long p_CUSTOMER_BRAND_ID, int p_Distributor_Id, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_BRAND
                {
                    Connection = mConnection,
                    CUSTOMER_BRAND_ID = p_CUSTOMER_BRAND_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable GetCustomerOwner(long p_CUSTOMER_OWNER_ID, int p_Distributor_Id, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_OWNER
                {
                    Connection = mConnection,
                    CUSTOMER_OWNER_ID = p_CUSTOMER_OWNER_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable GetCustomerBank(long p_CUSTOMER_BANK_ID, int p_Distributor_Id, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_BANK
                {
                    Connection = mConnection,
                    CUSTOMER_BANK_ID = p_CUSTOMER_BANK_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable GetCustomerCheque(int p_CUSTOMER_CHEQUE_ID, int p_Distributor_Id, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_CHEQUE
                {
                    Connection = mConnection,
                    CUSTOMER_CHEQUE_ID = p_CUSTOMER_CHEQUE_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable GetCustomerDealer(int p_CUSTOMER_DEALER_ID, int p_Distributor_Id, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_DEALER
                {
                    Connection = mConnection,
                    CUSTOMER_DEALER_ID = p_CUSTOMER_DEALER_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable GetCustomerLedger(long p_CUSTOMER_LEDGER_ID, int p_Distributor_Id, long p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_LEDGER
                {
                    Connection = mConnection,
                    CUSTOMER_LEDGER_ID = p_CUSTOMER_LEDGER_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable GetCustomerOpening(long p_CUSTOMER_OPENING_ID, int p_Distributor_Id, long p_CUSTOMER_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_OPENING
                {
                    Connection = mConnection,
                    CUSTOMER_OPENING_ID = p_CUSTOMER_OPENING_ID,
                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID,
                    TYPE_ID = p_TYPE_ID
                };

                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer Opening Balance
        /// </summary>
        /// <remarks>
        /// Returns Customer Opening Balance as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_CustomerId">Customer</param>
        /// <param name="p_fromDate">DateFrom</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>Customer Opening Balance as Datatable</returns>
        public DataTable SelectPrincipalCustomerOpBalance(int p_Distributor_Id, int p_CustomerId, DateTime p_fromDate, int p_PrincipalId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspCustomerOpBalance mCustData = new UspCustomerOpBalance();
                mCustData.Connection = mConnection;

                mCustData.Distributor_id = p_Distributor_Id;
                mCustData.principal_id = p_PrincipalId;
                mCustData.From_Date = p_fromDate;
                mCustData.customer_id = p_CustomerId;
                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer For Different Search Criterias
        /// </summary>
        /// <remarks>
        /// Returns Customer as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_FIELDNAME">FieldToSearchFor</param>
        /// <param name="p_PARAMETERNAME">ValueToSearch</param>
        /// <returns>Customer as Datatable</returns>
        public DataTable UspSelectCustomer(int p_Distributor_Id, string p_FIELDNAME, string p_PARAMETERNAME , int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectCUSTOMER mCustData = new UspSelectCUSTOMER();
                mCustData.Connection = mConnection;
                mCustData.FEILDNAME = p_FIELDNAME;
                mCustData.PARAMETER = p_PARAMETERNAME;
                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.type = pType;

                DataTable dt = mCustData.ExecuteTable();
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

        public DataTable UspSelectCustomer2(int p_Distributor_Id, string p_FIELDNAME, string p_PARAMETERNAME,int p_drpBusinesType,int p_drpVolumClass,int p_drpChanelType,int p_depToen,int p_drpMarket,int p_drpRout)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectCUSTOMER2 mCustData = new UspSelectCUSTOMER2();
                mCustData.Connection = mConnection;
                mCustData.FEILDNAME = p_FIELDNAME;
                mCustData.PARAMETER = p_PARAMETERNAME;
                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.BUSINESS_TYPE_ID = p_drpBusinesType;
                mCustData.CHANNEL_TYPE_ID = p_drpChanelType;
                mCustData.ROUTE_ID = p_drpRout;
                mCustData.TOWN_ID = p_depToen;
                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer For Different Search Criterias
        /// </summary>
        /// <remarks>
        /// Returns Customer as Datatable
        /// </remarks>
        /// <param name="p_User_Id">User</param>
        /// <param name="p_DistributorId">Location</param>
        /// <param name="p_FIELDNAME">FieldToSearchFor</param>
        /// <param name="p_PARAMETERNAME">ValueToSearch</param>
        /// <returns>Customer as Datatable</returns>
        public DataTable UspSelectCustomer(int p_User_Id, int p_DistributorId, string p_FIELDNAME, string p_PARAMETERNAME)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspRptSelectCUSTOMER mCustData = new UspRptSelectCUSTOMER();
                mCustData.Connection = mConnection;
                mCustData.FEILDNAME = p_FIELDNAME;
                mCustData.PARAMETER = p_PARAMETERNAME;
                mCustData.USER_ID = p_User_Id;
                mCustData.DISTRIBUTOR_ID = p_DistributorId;
                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer Credit/Advance Balance
        /// </summary>
        /// <remarks>
        /// Returns Customer Credit/Advance Balance
        /// </remarks>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_Distributor_id">Location</param>
        /// <param name="TranType">Type</param>
        /// <returns>Customer Credit/Advance Balance</returns>
        public DataTable SelectCustomerCreditBalance(long p_Customer_Id, int p_Principal_Id, int p_Distributor_id, int TranType)
        {
            IDbConnection mConnection = null;
            try
            {
                Configuration.GetAccountHead();

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectCustomerBalance mCustData = new UspSelectCustomerBalance();
                mCustData.Connection = mConnection;
                mCustData.CUSTOMER_ID = p_Customer_Id;
                mCustData.PRINCIPAL_ID = p_Principal_Id;
                mCustData.DISTRIBUTOR_ID = p_Distributor_id;
                mCustData.TranType = TranType;
                mCustData.ACCOUNT_HEAD_ID = long.Parse(Configuration.AccountReceivable);

                DataTable dt = mCustData.ExecuteTable();
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


        public DataTable TurnRatioOrDueAmount(long pCustomerId, int pDistributorId, DateTime pDate, int puserId, int pTypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectTotalRealization mCustData = new spSelectTotalRealization();
                mCustData.Connection = mConnection;
                mCustData.CUSTOMER_ID = pCustomerId;
                mCustData.USER_ID = puserId;
                mCustData.DISTRIBUTOR_ID = pDistributorId;
                mCustData.DATE = pDate;
                mCustData.typeId = pTypeId;
                DataTable dt = mCustData.ExecuteTable();
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

        #endregion

        #region Insert, Update

        /// <summary>
        /// Shifts Customers Between Locations, Routes, Markets
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_fromDistributor_Id">LocationFrom</param>
        /// <param name="p_toDistributor">LocationTo</param>
        /// <param name="p_fromArea_Id">MarketFrom</param>
        /// <param name="p_to_area_id">MarketTo</param>
        /// <param name="p_FROM_ROUTE_ID">RouteFrom</param>
        /// <param name="p_TO_ROUTE_ID">RouteTo</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool CustomerShifting(int p_fromDistributor_Id, int p_toDistributor, int p_fromArea_Id, int p_to_area_id, int p_FROM_ROUTE_ID, int p_TO_ROUTE_ID, int p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspCustomerShifting mCustomerShift = new UspCustomerShifting();
                mCustomerShift.Connection = mConnection;
                mCustomerShift.FROM_DISTRIBUTOR_ID = p_fromDistributor_Id;
                mCustomerShift.FROM_AREA_ID = p_fromArea_Id;
                mCustomerShift.TO_DISTRIBUTOR_ID = p_toDistributor;
                mCustomerShift.TO_AREA_ID = p_to_area_id;
                mCustomerShift.FROM_ROUTE_ID = p_FROM_ROUTE_ID;
                mCustomerShift.TO_ROUTE_ID = p_TO_ROUTE_ID;
                mCustomerShift.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomerShift.ExecuteQuery();
                return true;
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
        /// Inserts Customer Credit Limit
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Principal">Principal</param>
        /// <param name="p_CreditLimit">CreditLimit</param>
        /// <param name="p_CreditDays">CreditDays</param>
        /// <param name="p_ChannelTypeId">Channel</param>
        /// <param name="p_Category">Category</param>
        /// <param name="p_class">Class</param>
        /// <param name="p_Type">Type</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertCustomerCreditLimit(int p_Distributor_Id, long p_Customer_Id, int p_Principal, decimal p_CreditLimit,int p_CreditDays,int p_ChannelTypeId,int p_Category,string p_class,string p_Type,bool p_IsActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_PRINCIPAL_CREDITLIMIT mCustData = new spInsertCUSTOMER_PRINCIPAL_CREDITLIMIT();

                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.CUSTOMER_ID = p_Customer_Id;
                mCustData.PRINCIPAL_ID = p_Principal;
                mCustData.CREDITLIMIT_VALUE = p_CreditLimit;
                mCustData.TIME_STAMP = DateTime.Today;
                mCustData.CREDIT_DAYS = p_CreditDays;
                mCustData.BUSINESS_TYPE_ID = p_Category;
                mCustData.CHANNEL_TYPE_ID = p_ChannelTypeId;
                mCustData.CLASSFICATION = p_class;
                mCustData.CUSTOMER_TYPE = p_Type;
                mCustData.IS_ACTIVE = p_IsActive; 
                mCustData.ExecuteQuery();
                return true;
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
        /// Updates Customer Credit Limit
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Principal">Principal</param>
        /// <param name="p_CreditLimit">CreditLimit</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdateCustomerCreditLimit(int p_Distributor_Id, long p_Customer_Id, int p_Principal, decimal p_CreditLimit)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_PRINCIPAL_CREDITLIMIT mCustData = new spUpdateCUSTOMER_PRINCIPAL_CREDITLIMIT();

                mCustData.Connection = mConnection;
                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.CUSTOMER_ID = p_Customer_Id;
                mCustData.PRINCIPAL_ID = p_Principal;
                mCustData.CREDITLIMIT_VALUE = p_CreditLimit;
                 
                mCustData.ExecuteQuery();
                return true;
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
         
       

        public string InsertCustomer(long p_Customer_Id, bool p_Is_Gst_Registered, bool p_Is_Active, int pCountryId, int pTownId, int p_Distributor_Id
           , string p_Gst_Number, string p_Contact_Person, string p_Contact_Number, string p_Email_Address, string p_Customer_Code
           , string p_Customer_Name, string p_Address, DateTime p_RegDate, decimal pTAX_RATE, string p_FAX, string p_NTN, int p_creditdays
           , decimal p_credetvalue, int pChannelType, int pVolumeId, decimal pDiscount, decimal pITax, decimal pWHTax, int pPriceGroup)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER mCustomer = new spInsertCUSTOMER();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_Customer_Id;
                mCustomer.ADDRESS = p_Address;
                mCustomer.CountryId = pCountryId;
                mCustomer.TownId = pTownId;
                mCustomer.CONTACT_NUMBER = p_Contact_Number;
                mCustomer.CONTACT_PERSON = p_Contact_Person;
                mCustomer.CUSTOMER_CODE = p_Customer_Code;
                mCustomer.CUSTOMER_NAME = p_Customer_Name;
                mCustomer.EMAIL_ADDRESS = p_Email_Address;
                mCustomer.TAX_RATE = pTAX_RATE;
                mCustomer.GST_NUMBER = p_Gst_Number;
                mCustomer.FAX_NO = p_FAX;
                mCustomer.NTN = p_NTN;
                mCustomer.IS_GST_REGISTERED = p_Is_Gst_Registered;
                mCustomer.volumeId = pVolumeId;
                mCustomer.channelType = pChannelType;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.IS_ACTIVE = p_Is_Active;
                mCustomer.TIME_STAMP = System.DateTime.Now;
                mCustomer.LASTUPDATE_DATE = System.DateTime.Now;
                mCustomer.REGDATE = p_RegDate;
                mCustomer.CREDIT_DAYS = p_creditdays;
                mCustomer.CREDITLIMIT_VALUE = p_credetvalue;
                mCustomer.discount = pDiscount;
                mCustomer.INCOME_TAX = pITax;
                mCustomer.WH_TAX = pWHTax;
                mCustomer.priceGroup = pPriceGroup;
                mCustomer.ExecuteQuery();

                return "Record Updated Customer Code " + p_Customer_Code;



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
		 

        public bool InsertCustomerBrand(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_BRAND_NAME, string p_DEALERSHIP_NAME)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_BRAND mCustomer = new spInsertCUSTOMER_BRAND();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.BRAND_NAME = p_BRAND_NAME;
                mCustomer.DEALERSHIP_NAME = p_DEALERSHIP_NAME;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerBrand(long p_CUSTOMER_BRAND_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_BRAND_NAME, string p_DEALERSHIP_NAME)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_BRAND mCustomer = new spUpdateCUSTOMER_BRAND();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_BRAND_ID = p_CUSTOMER_BRAND_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.BRAND_NAME = p_BRAND_NAME;
                mCustomer.DEALERSHIP_NAME = p_DEALERSHIP_NAME;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool DeleteCustomerBrand(long p_CUSTOMER_BRAND_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteCUSTOMER_BRAND mCustomer = new spDeleteCUSTOMER_BRAND();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_BRAND_ID = p_CUSTOMER_BRAND_ID;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool InsertCustomerOwner(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_OWNER_NAME, string p_FATHER_NAME, string p_RESIDENTIAL_ADDRESS, string p_CNIC_NO, DateTime p_CNIC_EXPIRY, string p_ADDRESS
            , string p_EMAIL_ADDRESS, string p_CONTACT_NO, string p_MOBILE_NO, string p_PTCL, string p_PIC)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_OWNER mCustomer = new spInsertCUSTOMER_OWNER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.OWNER_NAME = p_OWNER_NAME;
                mCustomer.FATHER_NAME = p_FATHER_NAME;
                mCustomer.RESIDENTIAL_ADDRESS = p_RESIDENTIAL_ADDRESS;
                mCustomer.CNIC_NO = p_CNIC_NO;
                mCustomer.CNIC_EXPIRY = p_CNIC_EXPIRY;
                mCustomer.ADDRESS = p_ADDRESS;
                mCustomer.EMAIL_ADDRESS = p_EMAIL_ADDRESS;
                mCustomer.CONTACT_NO = p_CONTACT_NO;
                mCustomer.MOBILE_NO = p_MOBILE_NO;
                mCustomer.PTCL = p_PTCL;
                mCustomer.PIC = p_PIC;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerOwner(long p_CUSTOMER_OWNER_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_OWNER_NAME, string p_FATHER_NAME, string p_RESIDENTIAL_ADDRESS, string p_CNIC_NO, DateTime p_CNIC_EXPIRY, string p_ADDRESS
            , string p_EMAIL_ADDRESS, string p_CONTACT_NO, string p_MOBILE_NO, string p_PTCL, string p_PIC)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_OWNER mCustomer = new spUpdateCUSTOMER_OWNER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_OWNER_ID = p_CUSTOMER_OWNER_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.OWNER_NAME = p_OWNER_NAME;
                mCustomer.FATHER_NAME = p_FATHER_NAME;
                mCustomer.RESIDENTIAL_ADDRESS = p_RESIDENTIAL_ADDRESS;
                mCustomer.CNIC_NO = p_CNIC_NO;
                mCustomer.CNIC_EXPIRY = p_CNIC_EXPIRY;
                mCustomer.ADDRESS = p_ADDRESS;
                mCustomer.EMAIL_ADDRESS = p_EMAIL_ADDRESS;
                mCustomer.CONTACT_NO = p_CONTACT_NO;
                mCustomer.MOBILE_NO = p_MOBILE_NO;
                mCustomer.PTCL = p_PTCL;
                mCustomer.PIC = p_PIC;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool InsertCustomerBank(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_BANK_NAME, string p_BRANCH_CODE, string p_ACCOUNT_TITLE, string p_ACCOUNT_NO
            , string pStamp, string pSignature, string pInitial)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_BANK mCustomer = new spInsertCUSTOMER_BANK();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.BANK_NAME = p_BANK_NAME;
                mCustomer.BRANCH_CODE = p_BRANCH_CODE;
                mCustomer.ACCOUNT_TITLE = p_ACCOUNT_TITLE;
                mCustomer.ACCOUNT_NO = p_ACCOUNT_NO;
                mCustomer.STAMP = pStamp;
                mCustomer.SIGNATURE = pSignature;
                mCustomer.INITIAL = pInitial;

                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerBank(int p_CUSTOMER_BANK_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_BANK_NAME, string p_BRANCH_CODE, string p_ACCOUNT_TITLE
            , string p_ACCOUNT_NO, string pStamp, string pSignature, string pInitial)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_BANK mCustomer = new spUpdateCUSTOMER_BANK();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_BANK_ID = p_CUSTOMER_BANK_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.BANK_NAME = p_BANK_NAME;
                mCustomer.BRANCH_CODE = p_BRANCH_CODE;
                mCustomer.ACCOUNT_TITLE = p_ACCOUNT_TITLE;
                mCustomer.ACCOUNT_NO = p_ACCOUNT_NO;
                mCustomer.STAMP = pStamp;
                mCustomer.SIGNATURE = pSignature;
                mCustomer.INITIAL = pInitial;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool DeleteCustomerBank(long p_CUSTOMER_BANK_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteCUSTOMER_BANK mCustomer = new spDeleteCUSTOMER_BANK();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_BANK_ID = p_CUSTOMER_BANK_ID;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool InsertCustomerCheque(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_CHEQUE_NO, string p_BANK_NAME, decimal p_AMOUNT)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_CHEQUE mCustomer = new spInsertCUSTOMER_CHEQUE();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.CHEQUE_NO = p_CHEQUE_NO;
                mCustomer.BANK_NAME = p_BANK_NAME;
                mCustomer.AMOUNT = p_AMOUNT;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerCheque(int p_CUSTOMER_CHEQUE_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_CHEQUE_NO, string p_BANK_NAME, decimal p_AMOUNT)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_CHEQUE mCustomer = new spUpdateCUSTOMER_CHEQUE();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_CHEQUE_ID = p_CUSTOMER_CHEQUE_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.CHEQUE_NO = p_CHEQUE_NO;
                mCustomer.BANK_NAME = p_BANK_NAME;
                mCustomer.AMOUNT = p_AMOUNT;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool DeleteCustomerCheque(int p_CUSTOMER_CHEQUE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteCUSTOMER_CHEQUE mCustomer = new spDeleteCUSTOMER_CHEQUE();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_CHEQUE_ID = p_CUSTOMER_CHEQUE_ID;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool InsertCustomerDealer(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_NAME, string p_ADDRESS, string p_CONTACT_PERSON, string p_CONTACT_NO)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_DEALER mCustomer = new spInsertCUSTOMER_DEALER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.NAME = p_NAME;
                mCustomer.ADDRESS = p_ADDRESS;
                mCustomer.CONTACT_PERSON = p_CONTACT_PERSON;
                mCustomer.CONTACT_NO = p_CONTACT_NO;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerDealer(int p_CUSTOMER_DEALER_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, string p_NAME, string p_ADDRESS, string p_CONTACT_PERSON, string p_CONTACT_NO)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_DEALER mCustomer = new spUpdateCUSTOMER_DEALER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_DEALER_ID = p_CUSTOMER_DEALER_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.NAME = p_NAME;
                mCustomer.ADDRESS = p_ADDRESS;
                mCustomer.CONTACT_PERSON = p_CONTACT_PERSON;
                mCustomer.CONTACT_NO = p_CONTACT_NO;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool DeleteCustomerDealer(int p_CUSTOMER_DEALER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteCUSTOMER_DEALER mCustomer = new spDeleteCUSTOMER_DEALER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_DEALER_ID = p_CUSTOMER_DEALER_ID;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool DeleteCustomerLedger(int p_CUSTOMER_LEDGER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteCUSTOMER_LEDEGR mCustomer = new spDeleteCUSTOMER_LEDEGR();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_LEDGER_ID = p_CUSTOMER_LEDGER_ID;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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
        public bool InsertCustomerLedger(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, DateTime p_LEDGER_DATE, decimal p_BALANCE, string p_REMARKS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_LEDGER mCustomer = new spInsertCUSTOMER_LEDGER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.LEDGER_DATE = p_LEDGER_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerLedger(long p_CUSTOMER_LEDGER_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, DateTime p_LEDGER_DATE, decimal p_BALANCE, string p_REMARKS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_LEDGER mCustomer = new spUpdateCUSTOMER_LEDGER();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_LEDGER_ID = p_CUSTOMER_LEDGER_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.LEDGER_DATE = p_LEDGER_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool InsertCustomerOpening(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_OPENING_DATE, decimal p_BALANCE, string p_REMARKS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_OPENING mCustomer = new spInsertCUSTOMER_OPENING();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.TYPE_ID = p_TYPE_ID;
                mCustomer.OPENING_DATE = p_OPENING_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;
                return mCustomer.ExecuteQuery();


            //   LedgerController LController = new LedgerController();
           //    Configuration.GetAccountHead();
            //   string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);                                                                                                                                                                                                                     //  <asp:ListItem Value="25">Opening Credit</asp:ListItem>
            //   LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_DISTRIBUTOR_ID, p_BALANCE, 0, p_OPENING_DATE, "Credit Sale Default", DateTime.Now, 0, int.Parse(p_CUSTOMER_ID.ToString()), 0, 0, Constants.Document_Opening, 0, mTransaction, mConnection, 25, p_DELIVERYMAN_ID);
            //   LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_DISTRIBUTOR_ID, 0, p_BALANCE, p_OPENING_DATE, "Sale Default", DateTime.Now, 0, int.Parse(p_CUSTOMER_ID.ToString()), 0, 0, Constants.Document_Opening, 0, mTransaction, mConnection, 25, p_DELIVERYMAN_ID.ToString());
              
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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

        public bool UpdateCustomerOpening(long p_CUSTOMER_OPENING_ID, long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_OPENING_DATE, decimal p_BALANCE, string p_REMARKS)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_OPENING mCustomer = new spUpdateCUSTOMER_OPENING();
                mCustomer.Connection = mConnection;
                mCustomer.CUSTOMER_OPENING_ID = p_CUSTOMER_OPENING_ID;
                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.TYPE_ID = p_TYPE_ID;
                mCustomer.OPENING_DATE = p_OPENING_DATE;
                mCustomer.BALANCE = p_BALANCE;
                mCustomer.REMARKS = p_REMARKS;
                return mCustomer.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
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
        /// Inserts Or Updates Customer Tagging
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_saleInvoiceId">Invoice</param>
        /// <param name="p_CreditType">Type</param>
        /// <param name="p_CreditAmount">Amount</param>
        /// <param name="p_Remarks">Remarks</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdateCustomerTaging(long p_saleInvoiceId,int p_CreditType,decimal p_CreditAmount,string p_Remarks)
        {

            IDbConnection mConnection = null;
            try
            {
                spInsertCUSTOMER_CREDITTAGING mCustData = new spInsertCUSTOMER_CREDITTAGING();
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mCustData.Connection = mConnection;
                mCustData.SALE_INVOICE_ID = p_saleInvoiceId;
                mCustData.CREDIT_TYPE = p_CreditType;
                mCustData.CREDIT_AMOUNT = p_CreditAmount;
                mCustData.REMARKS = p_Remarks;  
                return mCustData.ExecuteQuery();
                
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

        public string UpdateCustomer(long p_Customer_Id, bool p_Is_Gst_Registered, bool p_Is_Active, int pCountryId, int pTownId, int p_Distributor_Id
           , string p_Gst_Number, string p_Contact_Person, string p_Contact_Number, string p_Email_Address
           , decimal pTAX_RATE, string p_Customer_Code, string p_Customer_Name, string p_Address, DateTime p_RegDate, string pFax, string p_NTN
           , int p_creditdays, decimal p_credetvalue, int pChannelType, int pVolumeId, decimal pDiscount, decimal pITax, decimal pWHTax, int pPriceGroup)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER mCustomer = new spUpdateCUSTOMER();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_Customer_Id;
                mCustomer.ADDRESS = p_Address;
                mCustomer.CountryId = pCountryId;
                mCustomer.TownId = pTownId;
                mCustomer.CONTACT_NUMBER = p_Contact_Number;
                mCustomer.CONTACT_PERSON = p_Contact_Person;
                mCustomer.CUSTOMER_CODE = p_Customer_Code;
                mCustomer.CUSTOMER_NAME = p_Customer_Name;
                mCustomer.EMAIL_ADDRESS = p_Email_Address;
                mCustomer.FAX_NO = pFax;
                mCustomer.GST_NUMBER = p_Gst_Number;
                mCustomer.NTN = p_NTN;
                mCustomer.REGDATE = p_RegDate;
                mCustomer.IS_GST_REGISTERED = p_Is_Gst_Registered;
                mCustomer.TAX_RATE = pTAX_RATE;
                mCustomer.volumeId = pVolumeId;
                mCustomer.channelType = pChannelType;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.IS_ACTIVE = p_Is_Active;
                mCustomer.TIME_STAMP = System.DateTime.Now;
                mCustomer.LASTUPDATE_DATE = System.DateTime.Now;
                mCustomer.discount = pDiscount;
                mCustomer.CREDIT_DAYS = p_creditdays;
                mCustomer.CREDITLIMIT_VALUE = p_credetvalue;
                mCustomer.INCOME_TAX = pITax;
                mCustomer.WH_TAX = pWHTax;
                mCustomer.priceGroup = pPriceGroup;

                mCustomer.ExecuteQuery();

                return "Record Updated Customer Code " + p_Customer_Code;

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


        public bool ActiveInActiveCustomer(long p_CUSTOMER_ID, int p_DISTRIBUTOR_ID, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspActiveInActiveCustomer mCustomer = new uspActiveInActiveCustomer();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomer.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mCustomer.IS_ACTIVE = p_IS_ACTIVE;
                return mCustomer.ExecuteQuery();

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
        /// Inserts Customer From Excel File
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_TownId">Town</param>
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportCustomer(int p_TownId, int p_DistributorId, string pFileName)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;

            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            mTransaction = ProviderFactory.GetTransaction(mConnection);

            Sourcefile = new FileStream(pFileName, FileMode.Open);
            ReadSourceFile = new StreamReader(Sourcefile);
            string FileContents = "";
            try
            {
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    spInsertCUSTOMEROld mDistRoute = new spInsertCUSTOMEROld();
                    mDistRoute.Connection = mConnection;
                    mDistRoute.Transaction = mTransaction;
                    mDistRoute.CUSTOMER_ID = long.Parse(ParametersArr[0].ToString());
                    mDistRoute.DISTRIBUTOR_ID = p_DistributorId;
                    mDistRoute.CUSTOMER_CODE = ParametersArr[1].ToString();
                    mDistRoute.CUSTOMER_NAME = ParametersArr[2].ToString();
                    mDistRoute.ADDRESS = ParametersArr[3].ToString();
                    mDistRoute.CONTACT_PERSON = ParametersArr[4].ToString();
                    mDistRoute.CONTACT_NUMBER = ParametersArr[5].ToString();
                    mDistRoute.IS_GST_REGISTERED = false;
                    mDistRoute.GST_NUMBER = null;
                    mDistRoute.IS_ACTIVE = true;
                    mDistRoute.REGDATE = System.DateTime.Now;
                    mDistRoute.TIME_STAMP = System.DateTime.Now;
                    mDistRoute.LASTUPDATE_DATE = System.DateTime.Now;
                    mDistRoute.TOWN_ID = p_TownId;
                    mDistRoute.AREA_ID = int.Parse(ParametersArr[6].ToString());
                    mDistRoute.ROUTE_ID = int.Parse(ParametersArr[7].ToString());
                    mDistRoute.CHANNEL_TYPE_ID = int.Parse(ParametersArr[8].ToString());
                    mDistRoute.BUSINESS_TYPE_ID = int.Parse(ParametersArr[9].ToString());
                    mDistRoute.PROMOTION_CLASS = int.Parse(ParametersArr[10].ToString());
                    mDistRoute.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;


            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                ExceptionPublisher.PublishException(excp);
                return false;

            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();

            }

        }

        #endregion

        #region Customer Contact
        public DataTable GetCustomerContact(int p_Distributor_Id, long p_CUSTOMER_ID, int p_DESIGNATION_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                var mCustData = new spSelectCUSTOMER_CONTACT
                {
                    Connection = mConnection,

                    DISTRIBUTOR_ID = p_Distributor_Id,
                    CUSTOMER_ID = p_CUSTOMER_ID,
                    DESIGNATION_ID = p_DESIGNATION_ID

                };

                DataTable dt = mCustData.ExecuteTable();
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

        public string InsertCustomerContact(long p_Customer_Id, bool p_Is_Active, int p_Distributor_Id, string p_Contact_Person, string p_Contact_Number
            , string p_Email_Address, int pDesignationId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_CONTACT mCustomer = new spInsertCUSTOMER_CONTACT();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_Customer_Id;
                mCustomer.CONTACT_NO = p_Contact_Number;
                mCustomer.CONTACT_PERSON = p_Contact_Person;
                mCustomer.EMAIL = p_Email_Address;
                mCustomer.DESIGNATION_ID = pDesignationId;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.IS_ACTIVE = p_Is_Active;
                mCustomer.TIME_STAMP = System.DateTime.Now;
                mCustomer.LASTUPDATE_DATE = System.DateTime.Now;

                mCustomer.ExecuteQuery();

                return "Record Inserted";



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


        public string UpdateCustomerContact(int p_Contact_Id, bool p_Is_Active, int p_Distributor_Id, string p_Contact_Person, string p_Contact_Number
           , string p_Email_Address, int pDesignationId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_CONTACT mCustomer = new spUpdateCUSTOMER_CONTACT();
                mCustomer.Connection = mConnection;

                mCustomer.CONTACT_ID = p_Contact_Id;
                mCustomer.CONTACT_NO = p_Contact_Number;
                mCustomer.CONTACT_PERSON = p_Contact_Person;
                mCustomer.EMAIL = p_Email_Address;
                mCustomer.DESIGNATION_ID = pDesignationId;
                mCustomer.IS_ACTIVE = p_Is_Active;
                mCustomer.LASTUPDATE_DATE = System.DateTime.Now;

                mCustomer.ExecuteQuery();

                return "Record Updated";



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
        #endregion
        #endregion
    }
}
