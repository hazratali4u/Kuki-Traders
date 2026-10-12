using System;
using System.Data;
using SAMSCommonLayer.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Purchase, TranferOut, Purchase Return, TranferIn And Damage Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// <term>
    /// Update Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </term>
    /// <item>
    /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PurchaseController
    {
        #region Constructor

        /// <summary>
        /// Constructor for PurchaseController
        /// </summary>
        public PurchaseController()
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

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPrivousePurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = PConnection;
                mPurchaseDetail.Transaction = PTransaction;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.POSTING = p_Posting;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="P_DocumentDate">Date</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, DateTime P_DocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = P_DocumentDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting, int p_SOLD_TO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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


        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, DateTime pDocumentDate, byte pStatus)
        
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                //mPurchaseMaster.DOCUMENT_DATE = pDocumentDate;
                mPurchaseMaster.status = pStatus;
                mPurchaseMaster.POSTING = 0;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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

        public DataTable SelectGRN(int pType, int pVendorId)// fOR vENDOR iNVOICE fORM
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
               
                 mPurchaseMaster.TYPE_ID = pType;
                 mPurchaseMaster.SOLD_FROM = pVendorId;
                mPurchaseMaster.status = 2;
                mPurchaseMaster.POSTING = 0;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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

        public DataTable SelectSKUClosingStock(int p_DISTRIBUTOR_ID, int p_SKU_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.TYPE_ID = 12;
                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mStockUpdate.SKU_ID = p_SKU_ID;
                
                DataTable dt = mStockUpdate.ExecuteTable();
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

        public DataSet DailySupplierPurchased(int pDistributorId, int pSupplierId, DateTime p_From_Date, DateTime p_ToDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SAMSBusinessLayer.Reports.dsSalesPurchaseRegister ds = new SAMSBusinessLayer.Reports.dsSalesPurchaseRegister();

                spDailyPurchased mLedger = new spDailyPurchased();

                mLedger.Connection = mConnection;

                mLedger.distributorId = pDistributorId;
                mLedger.supplierId = pSupplierId;
                mLedger.fromDate = p_From_Date;
                mLedger.toDate = p_ToDate;

                DataTable dt = mLedger.ExecuteTable();
                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spDailyPurchased"].ImportRow(dr);
                }



                return ds;
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

        public bool Add_Voucher(int p_Distributor_id, int p_PRINCIPAL_ID, string p_VOUCHER_NO, int p_VOUCHER_TYPE_ID, DateTime p_VOUCHER_DATE, int p_PAYMENT_MODE, string p_PayeesName, string p_Remarks, DateTime ChequeDate, string ChequeNo, DataTable dtVoucherDetail, int p_UserId, string p_SlipNo, DateTime pDueDate)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertGL_MASTER mISom = new spInsertGL_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;
                decimal MDebitAmount = 0;
                decimal MCreditAmount = 0;

                //------------Insert into Sale Order Master----------

                if (dtVoucherDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.VOUCHER_TYPE_ID = p_VOUCHER_TYPE_ID;
                    mISom.VOUCHER_NO = p_VOUCHER_NO;
                    mISom.VOUCHER_DATE = p_VOUCHER_DATE;
                    mISom.PAYMENT_MODE = p_PAYMENT_MODE;
                    mISom.CHEQUE_DATE = ChequeDate;
                    mISom.CHEQUE_NO = ChequeNo;
                    mISom.PAYEES_NAME = p_PayeesName;
                    mISom.REMARKS = p_Remarks;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.USER_ID = p_UserId;
                    mISom.Slip_No = p_SlipNo;
                    mISom.IS_POSTED = false;
                    mISom.IS_DELETED = false;
                    mISom.DUE_DATE = pDueDate;
                    mISom.ExecuteQuery();

                    //----------------Insert into sale order detail-------------
                    spInsertGL_DETAIL mVoucherDetail = new spInsertGL_DETAIL();
                    mVoucherDetail.Connection = mConnection;
                    mVoucherDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtVoucherDetail.Rows)
                    {
                        mVoucherDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mVoucherDetail.PRINCIPAL_ID = int.Parse(dr["PRINCIPAL_ID"].ToString());
                        mVoucherDetail.VOUCHER_TYPE_ID = p_VOUCHER_TYPE_ID;
                        mVoucherDetail.VOUCHER_NO = p_VOUCHER_NO;
                        mVoucherDetail.ACCOUNT_HEAD_ID = long.Parse(dr["ACCOUNT_HEAD_ID"].ToString());
                        mVoucherDetail.DEBIT = decimal.Parse(dr["DEBIT"].ToString());
                        mVoucherDetail.CREDIT = decimal.Parse(dr["CREDIT"].ToString());
                        mVoucherDetail.GL_REMARKS = dr["REMARKS"].ToString();
                        mVoucherDetail.IS_DELETED = false;
                        mVoucherDetail.ExecuteQuery();

                        MDebitAmount += decimal.Parse(dr["DEBIT"].ToString());
                        MCreditAmount += decimal.Parse(dr["CREDIT"].ToString());
                    }
                }

                mTransaction.Commit();
                return true;
            }

            catch (Exception exp)
            {
                mTransaction.Rollback();
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


        //public long AccountType(int pTechnology, int pDocumentType)
        //{
        //    long Account = 0;
        //    //---------------------------------------------------------------Short
        //    if (pTechnology == 3 && pDocumentType == 8)
        //    {
        //        Account = long.Parse(Configuration.ShortStock70ccParts);
        //    }
        //    else if (pTechnology == 9 && pDocumentType == 8)
        //    {
        //        Account = long.Parse(Configuration.ShortStock100ccParts);
        //    }
        //    else if (pTechnology == 10 && pDocumentType == 8)
        //    {
        //        Account = long.Parse(Configuration.ShortStock125ccParts);
        //    }
        //    else if (pTechnology == 19 && pDocumentType == 8)
        //    {
        //        Account = long.Parse(Configuration.ShortStock150ccParts);
        //    }
        //    //-----------------------------------------------------------------Excess
        //    else if (pTechnology == 3 && pDocumentType == 9)
        //    {
        //        Account = long.Parse(Configuration.ExcessStock70ccParts);
        //    }
        //    else if (pTechnology == 9 && pDocumentType == 9)
        //    {
        //        Account = long.Parse(Configuration.ExcessStock100ccParts);
        //    }
        //    else if (pTechnology == 10 && pDocumentType == 9)
        //    {
        //        Account = long.Parse(Configuration.ExcessStock125ccParts);
        //    }
        //    else if (pTechnology == 19 && pDocumentType == 9)
        //    {
        //        Account = long.Parse(Configuration.ExcessStock150ccParts);
        //    }
        //    return Account;
        //}

        public void GetDocuemntDetail(int p_TYPE_ID, out string DocumentType, out string Remarks)
        {
            DocumentType = "";
            Remarks = "";
            if (p_TYPE_ID == Constants.Document_Purchase)//3
            {
                DocumentType = "Purchase";
                Remarks = "Purchase Voucher";
            }
            if (p_TYPE_ID == Constants.Document_Purchase_Return)//3
            {
                DocumentType = "Purchase Return";
                Remarks = "Purchase-Return Voucher";
            }
            if (p_TYPE_ID == Constants.Document_Transfer_In)//3
            {
                DocumentType = "Transfer In";
                Remarks = "Transfer-In Voucher";
            }
            else if (p_TYPE_ID == Constants.Document_Transfer_Out)//4
            {
                DocumentType = "Transfer Out";
                Remarks = "Transfer_Out Voucher";
            }
            else if (p_TYPE_ID == Constants.Document_Purchase_Return)//5
            {
                DocumentType = "Purchase Return";
                Remarks = "Purchase Return Voucher";
            }
            else if (p_TYPE_ID == Constants.Document_Damaged)//6
            {
                DocumentType = "Damage";
                Remarks = "Damage Voucher";
            }
            else if (p_TYPE_ID == Constants.Document_Short)//8
            {
                DocumentType = "Short";
                Remarks = "Short Voucher";
            }
           
            else if (p_TYPE_ID == Constants.Document_Acess)//9
            {
                DocumentType = "Excess";
                Remarks = "Excess Voucher";
            }
           
        }
        /// <summary>
        /// Inserts Opening, TranferOut, Purchase Return, TranferIn , Damage , Short and Excess Document
        /// </summary>


        //on frmPurchase.aspx
        public long InsertPurchase(int p_DISTRIBUTOR_ID, string p_INVOICE_NO, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO
            , int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, decimal p_DISCOUNT, decimal p_TOTAL_NET_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail
            , int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertPURCHASE_MASTER mPrchMaster = new spInsertPURCHASE_MASTER();
                mPrchMaster.Connection = mConnection;
                mPrchMaster.Transaction = mTransaction;
                mPrchMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPrchMaster.TYPE_ID = p_TYPE_ID;
               
                mPrchMaster.SOLD_FROM = p_SOLD_FROM;
                mPrchMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPrchMaster.SOLD_TO = p_SOLD_TO;
                mPrchMaster.debitAmount = p_TOTAL_NET_AMOUNT;
                mPrchMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPrchMaster.DISCOUNT = p_DISCOUNT;
                mPrchMaster.netAmount = p_TOTAL_NET_AMOUNT;
                mPrchMaster.USER_ID = p_UserId;
                mPrchMaster.TIME_STAMP = DateTime.Now;
                mPrchMaster.LAST_UPDATE = DateTime.Now;
                mPrchMaster.POSTING = p_Posting;
                mPrchMaster.BUILTY_NO = p_BuiltyNo;
                mPrchMaster.PRINCIPAL_ID = p_PrincipalId;
                mPrchMaster.INVOICE_NO = p_INVOICE_NO;

                mPrchMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;


                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPrchMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    
                    mPurchaseDetail.TYPE_ID = mPrchMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;// Convert.ToDateTime(dr["TIME_STAMP"]);  
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPrchMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPrchMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    //uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    //mSKUStock.Connection = mConnection;
                    //mSKUStock.Transaction = mTransaction;
                    //mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    //mSKUStock.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    //mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    //mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    //mSKUStock.EXPIRY_DATE = mPurchaseDetail.TIME_STAMP;
                    //mSKUStock.TYPE_ID = p_TYPE_ID;
                    //mSKUStock.REF_NO = "";
                    //mSKUStock.ExecuteQuery();

                }

               

                mTransaction.Commit();
                return mPrchMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();

                ExceptionPublisher.PublishException(exp);
                
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        //on frmPurchase.aspx
        public bool UpdatePurchase(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_INVOICE_NO, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM
        , decimal p_TOTAL_AMOUNT,  decimal p_DISCOUNT, decimal p_TOTAL_NET_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPrchMaster = new spUpdatePURCHASE_MASTER();
                mPrchMaster.Connection = mConnection;
                mPrchMaster.Transaction = mTransaction;
                mPrchMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPrchMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPrchMaster.TYPE_ID = p_TYPE_ID;
                mPrchMaster.SOLD_FROM = p_SOLD_FROM;
                mPrchMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPrchMaster.SOLD_TO = p_SOLD_TO;
                mPrchMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPrchMaster.netAmount = p_TOTAL_NET_AMOUNT;
                mPrchMaster.DISCOUNT = p_DISCOUNT;
                mPrchMaster.debitAmount = p_TOTAL_NET_AMOUNT;
                mPrchMaster.USER_ID = p_UserId;
                mPrchMaster.LAST_UPDATE = DateTime.Now;
                mPrchMaster.POSTING = p_Posting;
                mPrchMaster.BUILTY_NO = p_BuiltyNo;
                mPrchMaster.INVOICE_NO = p_INVOICE_NO;

                mPrchMaster.ExecuteQuery();

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;

                    mPurchaseStock.document_date = p_DOCUMENT_DATE;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.qty = int.Parse(dr["Quantity"].ToString());
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

              
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPrchMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPrchMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;

                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPrchMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPrchMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                }


                //#region Vendor Ledger

                //LedgerController LController = new LedgerController();
                //Configuration.GetAccountHead();

                //UspSelectMaxDocumentNo mMaxLNo = new UspSelectMaxDocumentNo();
                //mMaxLNo.Connection = mConnection;
                //mMaxLNo.Transaction = mTransaction;
                //mMaxLNo.Document_TypeId = Constants.Journal_Voucher;
                //mMaxLNo.Distributor_id = mPrchMaster.DISTRIBUTOR_ID;
                //mMaxLNo.TypeId = 1;
                //DataTable MaxLedgerId = mMaxLNo.ExecuteTable();
                //string VoucherNo = MaxLedgerId.Rows[0][0].ToString();


                //LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), 97, p_DISTRIBUTOR_ID, 0, p_TOTAL_NET_AMOUNT, mPrchMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_PrincipalId, mPrchMaster.INVOICE_NO, mPrchMaster.PURCHASE_MASTER_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");
                //LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), p_DISTRIBUTOR_ID, p_TOTAL_NET_AMOUNT, 0, mPrchMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_PrincipalId, mPrchMaster.INVOICE_NO, mPrchMaster.PURCHASE_MASTER_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");

                //#endregion

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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

     

        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID,string p_ORDER_NUMBER,int p_TYPE_ID,DateTime p_DOCUMENT_DATE,int p_SOLD_TO,int p_SOLD_FROM
            ,decimal p_TOTAL_AMOUNT,bool p_IS_DELETE,DataTable dtPurchaseDetail,int p_Posting,string p_BuiltyNo,int p_UserId,int p_PrincipalId, bool pPrincipalType) 
		{
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertPURCHASE_MASTER mPrchMaster = new spInsertPURCHASE_MASTER();
                mPrchMaster.Connection = mConnection;
                mPrchMaster.Transaction = mTransaction;
                mPrchMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPrchMaster.TYPE_ID = p_TYPE_ID;
                mPrchMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPrchMaster.SOLD_FROM = p_SOLD_FROM;
                mPrchMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPrchMaster.SOLD_TO = p_SOLD_TO;
                mPrchMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPrchMaster.USER_ID = p_UserId;
                mPrchMaster.TIME_STAMP = DateTime.Now;
                mPrchMaster.LAST_UPDATE = DateTime.Now;
                mPrchMaster.POSTING = p_Posting;
                mPrchMaster.BUILTY_NO = p_BuiltyNo;
                mPrchMaster.PRINCIPAL_ID = p_PrincipalId;

                mPrchMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;


                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPrchMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());


                    mPurchaseDetail.TYPE_ID = mPrchMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;// Convert.ToDateTime(dr["TIME_STAMP"]);  
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPrchMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPrchMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                //ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}

       
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM
            , decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail,int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, long p_PO_ID
            , string p_REF_NO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPrchMaster = new spInsertPURCHASE_MASTER();
                mPrchMaster.Connection = mConnection;
                mPrchMaster.Transaction = mTransaction;
                mPrchMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPrchMaster.TYPE_ID = p_TYPE_ID;
                mPrchMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPrchMaster.SOLD_FROM = p_SOLD_FROM;
                mPrchMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPrchMaster.SOLD_TO = p_SOLD_TO;
                mPrchMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPrchMaster.USER_ID = p_UserId;
                mPrchMaster.TIME_STAMP = DateTime.Now;
                mPrchMaster.LAST_UPDATE = DateTime.Now;
                mPrchMaster.POSTING = p_Posting;
                mPrchMaster.BUILTY_NO = p_BuiltyNo;
                mPrchMaster.PRINCIPAL_ID = p_PrincipalId;
                
                mPrchMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPrchMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPrchMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPrchMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPrchMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();                    
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    mSKUStock.EXPIRY_DATE = mPurchaseDetail.TIME_STAMP;
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                    mSKUStock.REF_NO = p_REF_NO;
                    mSKUStock.ExecuteQuery();

                }
               
               
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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
        /// Updates Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM
            , decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, bool pPrincipalType)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPrchMaster = new spUpdatePURCHASE_MASTER(); 
                mPrchMaster.Connection = mConnection;
                mPrchMaster.Transaction = mTransaction;
                mPrchMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;  
                mPrchMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPrchMaster.TYPE_ID = p_TYPE_ID;
                mPrchMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPrchMaster.SOLD_FROM = p_SOLD_FROM;
                mPrchMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPrchMaster.SOLD_TO = p_SOLD_TO;
                mPrchMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPrchMaster.USER_ID = p_UserId;   
                mPrchMaster.LAST_UPDATE = DateTime.Now;
                mPrchMaster.POSTING = p_Posting;
                mPrchMaster.BUILTY_NO = p_BuiltyNo;  
                mPrchMaster.ExecuteQuery();
               
                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID,mConnection,mTransaction);
                
                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID; 
                   // mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }
                                
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    
                   //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPrchMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPrchMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                   
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId; 
                    mStockUpdate.TYPE_ID = mPrchMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPrchMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPrchMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                }

               


                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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

        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal,string p_REF_NO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                  //  mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    if (dr["TIME_STAMP"] != null)
                    {
                        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    }
                    else
                    {
                        mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    }
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();


                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.FREE_SKU;
                    mSKUStock.EXPIRY_DATE = mPurchaseDetail.TIME_STAMP;
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                     mSKUStock.REF_NO = p_REF_NO;
                    mSKUStock.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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


        // GRN

        public bool InsertGRN(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO
            , int p_SOLD_FROM, decimal p_DISCOUNT, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo
            , int p_UserId, int p_PrincipalId, long p_PO_ID, string p_REF_NO, string pInvoiceNo, DateTime pDcDate, string pDriverName
            , string pVehicleNo, byte pStatus)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.INVOICE_NO = pInvoiceNo;
                mPurchaseMaster.DISCOUNT = p_DISCOUNT;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.netAmount = p_TOTAL_AMOUNT - p_DISCOUNT;
                mPurchaseMaster.gstAmount = 0;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.dcNo = null;
                mPurchaseMaster.dcDate = pDcDate;
                mPurchaseMaster.driverName = pDriverName;
                mPurchaseMaster.VehicleNo = pVehicleNo;
                mPurchaseMaster.PO_ID = p_PO_ID;
                mPurchaseMaster.status = pStatus;
                mPurchaseMaster.debitAmount = p_TOTAL_AMOUNT - p_DISCOUNT;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString()); //used as receive qty
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = mPurchaseMaster.DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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

        public bool UpdateGRN(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE
            , int p_SOLD_TO, int p_SOLD_FROM, decimal p_DISCOUNT, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting
            , string p_BuiltyNo, int p_UserId, int p_Principal, string p_REF_NO, string p_INVOICE_NO, DateTime pDcDate, string pDriverName
            , string pVehicleNo, byte pStatus)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);


                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.document_date = p_DOCUMENT_DATE;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.qty = int.Parse(dr["FREE_SKU"].ToString());// - int.Parse(dr["ngQty"].ToString());
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }


                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.INVOICE_NO = p_INVOICE_NO;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.DISCOUNT = p_DISCOUNT;
                mPurchaseMaster.netAmount = p_TOTAL_AMOUNT-p_DISCOUNT;
                mPurchaseMaster.gstAmount = 0;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
               
                mPurchaseMaster.dcDate = pDcDate;
                mPurchaseMaster.driverName = pDriverName;
                mPurchaseMaster.VehicleNo = pVehicleNo;
                mPurchaseMaster.status = pStatus;
                mPurchaseMaster.debitAmount = p_TOTAL_AMOUNT - p_DISCOUNT;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = "N/A";
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                  
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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


        public bool PostPendingDocument(long p_PURCHASE_MASTER_ID,int p_Type_Id,int p_Distributor_Id,int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.TYPE_ID = p_Type_Id;  
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.ExecuteQuery();
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

        #region Purchase Order

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public long InsertPurchaseOrder(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, string p_REF_NO, int p_TYPE_ID, int p_CURRENCY_ID, decimal p_EXCHANGE_RATE, DateTime p_DOCUMENT_DATE, decimal p_TOTAL_AMOUNT, int p_USER_ID,
            DataTable dtPurchaseDetail, string pCreditTerm, decimal pNetAMount, decimal pGst, decimal pGstValue)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_ORDER mPurchaseMaster = new spInsertPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.CURRENCY_ID = p_CURRENCY_ID;
                mPurchaseMaster.EXCHANGE_RATE = p_EXCHANGE_RATE;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.creditTerm = pCreditTerm;
                mPurchaseMaster.gst = pGst;
                mPurchaseMaster.gstValue = pGstValue;
                mPurchaseMaster.netAmount = pNetAMount;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TIME_STAMP = DateTime.Now;
                    mPurchaseDetail.ExecuteQuery();                    
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_ORDER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseOrderDetail(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, string p_REF_NO, int p_CURRENCY_ID, decimal p_TOTAL_AMOUNT
            , decimal p_EXCHANGE_RATE, int p_USER_ID, DataTable dtPurchaseDetail, string pCreditTerm, decimal pNetAMount, decimal pGst, decimal pGstValue)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spDeletePURCHASE_ORDER_DETAIL mDelete = new spDeletePURCHASE_ORDER_DETAIL();
                mDelete.Connection = mConnection;
                mDelete.Transaction = mTransaction;

                mDelete.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDelete.ExecuteQuery();
                
                
                spUpdatePURCHASE_ORDER mPurchaseMaster = new spUpdatePURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.CURRENCY_ID = p_CURRENCY_ID;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.EXCHANGE_RATE = p_EXCHANGE_RATE;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.creditTerm = pCreditTerm;
                mPurchaseMaster.gst = pGst;
                mPurchaseMaster.gstValue= pGstValue;
                mPurchaseMaster.netAmount = pNetAMount;
                mPurchaseMaster.ExecuteQuery();
                                              
                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TIME_STAMP = DateTime.Now;
                    mPurchaseDetail.ExecuteQuery();    
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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
        /// Gets Purchase Order Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase Order Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseOrderDocumentNo(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID,DateTime p_DOCUMENT_DATE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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

        public DataSet SelectPurchaseOrderNo(long pPurchaseOrderId, int pTypeId, DateTime pDocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                
                   mPurchaseMaster.Connection = mConnection;
                   mPurchaseMaster.PURCHASE_ORDER_ID = pPurchaseOrderId;
                    
                    mPurchaseMaster.TYPE_ID = pTypeId;
                    mPurchaseMaster.DOCUMENT_DATE = pDocumentDate;

                return mPurchaseMaster.ExecuteDataSet();

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
      
        public DataTable SelectPurchaseOrderDocumentNo2(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID,DateTime p_DOCUMENT_DATE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
               // mPurchaseMaster.status = pStatus;
                DataTable dt = mPurchaseMaster.ExecuteTable();
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
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseOrderDetail(long p_PURCHASE_ORDER_ID,int p_DISTRIBUTOR_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;                
                DataTable dt = mPurchaseDetail.ExecuteTable();
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
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseOrderDetail2(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, long p_PURCHASE_ORDER_ID2)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL2 mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL2();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseDetail.PURCHASE_ORDER_ID2 = p_PURCHASE_ORDER_ID2;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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


        #region GRN Q.A

        public void InsertVendorLedger(long p_ACCOUNT_HEAD_ID, int p_Distributor_Id, decimal p_Debit, decimal p_Credit, DateTime p_Ledger_Date, string p_Remarks, int p_PRINCIPAL_ID, long p_DOCUMENT_NO, int p_DOCUMENT_TYPE_ID, int p_UserId, IDbTransaction mTransaction, IDbConnection mConnection, int p_Paymode)
        {

            try
            {
                uspInsertVendorLedger mspInsertLedger = new uspInsertVendorLedger();
                mspInsertLedger.Connection = mConnection;
                mspInsertLedger.Transaction = mTransaction;
                mspInsertLedger.ACCOUNT_HEAD_ID = p_ACCOUNT_HEAD_ID;
                mspInsertLedger.DISTRIBUTOR_ID = p_Distributor_Id;
                mspInsertLedger.DEBIT = p_Debit;
                mspInsertLedger.CREDIT = p_Credit;
                mspInsertLedger.VENDOR_LEDGER_DATE = p_Ledger_Date;
                mspInsertLedger.REMARKS = p_Remarks;
                mspInsertLedger.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mspInsertLedger.DOCUMENT_TYPE_ID = p_DOCUMENT_TYPE_ID;
                mspInsertLedger.DOCUMENT_NO = p_DOCUMENT_NO;
                mspInsertLedger.USER_ID = p_UserId;
                mspInsertLedger.PAYMENT_MODE = p_Paymode;
                mspInsertLedger.ExecuteQuery();
            }

            catch (Exception excp)
            {
                ExceptionPublisher.PublishException(excp);
                //return null;

            }

        }

        public bool UpdateGRN_QA(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE
           , int p_SOLD_TO, bool pPrincipalType, decimal p_TOTAL_AMOUNT, decimal pNetAmount, decimal pGstAmount, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting
           , string p_BuiltyNo, int p_UserId, int p_Principal, string p_REF_NO, string pDcNo, DateTime pDcDate, string pDriverName, string pVehicleNo)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);


                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.document_date = p_DOCUMENT_DATE;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.qty = int.Parse(dr["FREE_SKU"].ToString()) - int.Parse(dr["ngQty"].ToString());
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.SOLD_FROM = 0;
                //mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.netAmount = pNetAmount;
                mPurchaseMaster.gstAmount = pGstAmount;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                
                mPurchaseMaster.dcDate = pDcDate;
                mPurchaseMaster.driverName = pDriverName;
                mPurchaseMaster.VehicleNo = pVehicleNo;
                mPurchaseMaster.ORDER_NUMBER =Convert.ToString(p_PURCHASE_MASTER_ID);
                mPurchaseMaster.status = 2;
                mPurchaseMaster.debitAmount = pNetAmount;
           
                mPurchaseMaster.ExecuteQuery();

               
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                decimal sevenCC = 0, hundredCC = 0, oneTwoFivecc = 0, OneFiftycc = 0, ngStock = 0;
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    int qty = 0;
                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE =  decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.ngQty = int.Parse(dr["ngQty"].ToString());
                    mPurchaseDetail.remarks = dr["remarks"].ToString();
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();
                    
                    qty = mPurchaseDetail.FREE_SKU - mPurchaseDetail.ngQty;
                    
                    #region  Accounts hiting
                    if (mPurchaseDetail.ngQty > 0)
                    {
                        ngStock += mPurchaseDetail.ngQty * mPurchaseDetail.PRICE;
                    }

                    // 3 70CC, 9 100CC ,10 125CC, 19 150CC, 20 50CC	
                    if (int.Parse(dr["techId"].ToString()) == 3)
                    {
                        sevenCC += qty * mPurchaseDetail.PRICE;
                    }
                    else if (int.Parse(dr["techId"].ToString()) == 9)
                    {
                        hundredCC += qty * mPurchaseDetail.PRICE;
                    }
                    else if (int.Parse(dr["techId"].ToString()) == 10)
                    {
                        oneTwoFivecc += qty * mPurchaseDetail.PRICE;
                    }
                    else if (int.Parse(dr["techId"].ToString()) == 19)
                    {
                        OneFiftycc += qty * mPurchaseDetail.PRICE;
                    }

                    #endregion
                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = qty;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                }
                #region Account Posting
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();

                //   string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);
                UspSelectMaxDocumentNo mMaxDNo = new UspSelectMaxDocumentNo();
                mMaxDNo.Connection = mConnection;
                mMaxDNo.Transaction = mTransaction;
                mMaxDNo.Document_TypeId = Constants.Journal_Voucher;
                mMaxDNo.Distributor_id = mPurchaseMaster.DISTRIBUTOR_ID;
                mMaxDNo.TypeId = 1;
                DataTable MaxId = mMaxDNo.ExecuteTable();
                string VoucherNo = MaxId.Rows[0][0].ToString();

                Configuration.GetAccountHead();
                
                LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), 97, p_DISTRIBUTOR_ID, 0, pNetAmount, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_Principal,  mPurchaseMaster.PURCHASE_MASTER_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");
                LController.PostingPrinvipalInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.PayableAccount), p_DISTRIBUTOR_ID, pNetAmount, 0, mPurchaseMaster.DOCUMENT_DATE, "Purchase Default", DateTime.Now, p_Principal,  mPurchaseMaster.PURCHASE_MASTER_ID, "0", Constants.Document_PrincipalInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, "0");


                #region GL Master, Detail

                spDeleteGL_MASTER2 mDelete = new spDeleteGL_MASTER2();

                mDelete.Connection = mConnection;
                mDelete.Transaction = mTransaction;
                mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDelete.VOUCHER_TYPE_ID = Constants.Journal_Voucher;
                mDelete.PAYEES_NAME = Convert.ToString(p_PURCHASE_MASTER_ID);
                mDelete.TYPE_ID = p_TYPE_ID;
                
                mDelete.ExecuteQuery();


                UspSelectMaxVoucherNo mMaxDNo2 = new UspSelectMaxVoucherNo();
                mMaxDNo2.Connection = mConnection;
                mMaxDNo2.Transaction = mTransaction;

                mMaxDNo2.Document_TypeId = Constants.Journal_Voucher;
                mMaxDNo2.Distributor_id = p_DISTRIBUTOR_ID;
                mMaxDNo2.Month = DateTime.Now;
                DataTable MaxId2 = mMaxDNo2.ExecuteTable();
                string MaxVoucherId = MaxId2.Rows[0][0].ToString();
                string VoucherPrefix = "JV";

                if (MaxVoucherId.Length == 1)
                {
                    MaxVoucherId = VoucherPrefix + "-000" + MaxVoucherId;
                }
                else if (MaxVoucherId.Length == 2)
                {
                    MaxVoucherId = VoucherPrefix + "-00" + MaxVoucherId;
                }
                else if (MaxVoucherId.Length == 3)
                {
                    MaxVoucherId = VoucherPrefix + "-0" + MaxVoucherId;
                }
                else
                {
                    MaxVoucherId = VoucherPrefix + "-" + MaxVoucherId;
                }
                string VoucherNo2 = MaxVoucherId;

                string Remarks = "";
                string DocumentType = "";

                GetDocuemntDetail(p_TYPE_ID, out DocumentType, out Remarks);

                if (LController.PostingGLMaster(p_DISTRIBUTOR_ID, p_Principal, VoucherNo2, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, p_TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mTransaction, mConnection))
                {
                    if (sevenCC > 0)
                    {
                         LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.Stock70ccParts), sevenCC,0, "Stock70ccParts " + Remarks, mTransaction, mConnection);  
                    }
                    if (hundredCC > 0)
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.Stock100ccParts), hundredCC, 0, "Stock100ccParts " + Remarks, mTransaction, mConnection);
                        
                    }
                    if (oneTwoFivecc > 0)
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.Stock125ccParts), oneTwoFivecc, 0, "Stock125ccParts " + Remarks, mTransaction, mConnection);
                        
                    }
                    if (OneFiftycc > 0)
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.Stock150ccParts), OneFiftycc, 0, "Stock150ccParts " + Remarks, mTransaction, mConnection); 
                    }
                    if (!pPrincipalType)
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.LocalPartsVendor2W), 0, p_TOTAL_AMOUNT +  pGstAmount, "LocalPartsVendor2W " + Remarks, mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.SaleTaxReceivableLocal), pGstAmount, 0, "SaleTaxReceivableLocal "+ Remarks, mTransaction, mConnection);
                    }
                    else
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.ImportedPartsVendor2W), 0, p_TOTAL_AMOUNT + pGstAmount, "ImportedPartsVendor2W " + Remarks, mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.SaleTaxReceivableImported), pGstAmount, 0, "Sale Tax Receivable" + Remarks, mTransaction, mConnection);
                    }
                    if (ngStock > 0)
                    {
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.NGStock), ngStock, 0, "NGStock " + Remarks, mTransaction, mConnection);
                        LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo2, long.Parse(Configuration.RejectStock), 0, ngStock, "Reject Stock " + Remarks, mTransaction, mConnection);
                    
                    }
                }

                #endregion

                #endregion


                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
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


        #endregion



        #endregion

        #endregion
    }
}
