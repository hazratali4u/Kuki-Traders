using System;
using System.Data;
using SAMSDataAccessLayer.Classes;
using SAMSCommonLayer.Classes;


namespace SAMSDatabaseLayer.Classes
{
	public class spInsertPacking_Master
	{
		#region Private Members
		private string sp_Name = "spInsertPacking_Master" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_distributorId;
		private int m_userId;
		private DateTime m_documentDate;
		private DateTime m_timeStamp;
		private DateTime m_lastUpdate;
		private bool m_isDeleted;
		private bool m_posting;
		private long m_customerId;
		private long m_packingMasterId;
		private string m_builtyNo;
		private string m_transporter;
		private string m_driverName;
		private string m_cellNo;
		private string m_remarks;
		#endregion
		#region Public Properties
		public int distributorId
		{
			set
			{
				m_distributorId = value ;
			}
			get
			{
				return m_distributorId;
			}
		}
		public int userId
		{
			set
			{
				m_userId = value ;
			}
			get
			{
				return m_userId;
			}
		}
		public DateTime documentDate
		{
			set
			{
				m_documentDate = value ;
			}
			get
			{
				return m_documentDate;
			}
		}
		public DateTime timeStamp
		{
			set
			{
				m_timeStamp = value ;
			}
			get
			{
				return m_timeStamp;
			}
		}
		public DateTime lastUpdate
		{
			set
			{
				m_lastUpdate = value ;
			}
			get
			{
				return m_lastUpdate;
			}
		}
		public bool isDeleted
		{
			set
			{
				m_isDeleted = value ;
			}
			get
			{
				return m_isDeleted;
			}
		}
		public bool posting
		{
			set
			{
				m_posting = value ;
			}
			get
			{
				return m_posting;
			}
		}
		public long customerId
		{
			set
			{
				m_customerId = value ;
			}
			get
			{
				return m_customerId;
			}
		}
		public long packingMasterId
		{
			get
			{
				return m_packingMasterId;
			}
		}
		public  string builtyNo
		{
			set
			{
				m_builtyNo = value ;
			}
			get
			{
				return m_builtyNo;
			}
		}
		public  string transporter
		{
			set
			{
				m_transporter = value ;
			}
			get
			{
				return m_transporter;
			}
		}
		public  string driverName
		{
			set
			{
				m_driverName = value ;
			}
			get
			{
				return m_driverName;
			}
		}
		public  string cellNo
		{
			set
			{
				m_cellNo = value ;
			}
			get
			{
				return m_cellNo;
			}
		}
		public  string remarks
		{
			set
			{
				m_remarks = value ;
			}
			get
			{
				return m_remarks;
			}
		}


		public IDbConnection  Connection
		{
			set
			{
				m_connection = value;
			}
			get
			{
				return m_connection;
			}
		}
		public IDbTransaction  Transaction
		{
			set
			{
				m_transaction = value;
			}
			get
			{
				return m_transaction;
			}
		}
		#endregion
		#region Constructor
		public spInsertPacking_Master()
		{
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				m_packingMasterId = (long)((IDataParameter)(cmd.Parameters["@packingMasterId"])).Value;
				return true;
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
			}
		}
		public IDataReader ExecuteReader()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public DataTable ExecuteTable()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
				da.SelectCommand = command;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds.Tables[0];
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public string ExecuteScalar()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_distributorId= Convert.ToInt32(dr["distributorId"]);
				m_userId= Convert.ToInt32(dr["userId"]);
				m_documentDate= Convert.ToDateTime(dr["documentDate"]);
				m_timeStamp= Convert.ToDateTime(dr["timeStamp"]);
				m_lastUpdate= Convert.ToDateTime(dr["lastUpdate"]);
				m_isDeleted=Convert.ToBoolean(dr["isDeleted"]);
				m_posting=Convert.ToBoolean(dr["posting"]);
				m_customerId=Convert.ToInt64(dr["customerId"]);
				m_packingMasterId=Convert.ToInt64(dr["packingMasterId"]);
				m_builtyNo= Convert.ToString(dr["builtyNo"]);
				m_transporter= Convert.ToString(dr["transporter"]);
				m_driverName= Convert.ToString(dr["driverName"]);
				m_cellNo= Convert.ToString(dr["cellNo"]);
				m_remarks= Convert.ToString(dr["remarks"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@distributorId" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_distributorId==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_distributorId;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@userId" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_userId==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_userId;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@documentDate" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_documentDate==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_documentDate;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@timeStamp" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_timeStamp==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_timeStamp;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@lastUpdate" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_lastUpdate==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_lastUpdate;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@isDeleted" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_isDeleted;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@posting" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
				parameter.Value = m_posting;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@customerId" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_customerId==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_customerId;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@packingMasterId" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			parameter.Direction = ParameterDirection.Output;
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@builtyNo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_builtyNo== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_builtyNo;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@transporter" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_transporter== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_transporter;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@driverName" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_driverName== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_driverName;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@cellNo" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_cellNo== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_cellNo;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@remarks" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_remarks== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_remarks;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
