using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BusinessLayer
{
    public class DatabaseBusiness
    {
      
        private string _connectionString = @"Data Source=CASPER_NIRVANA\SQLEXPRESS;Initial Catalog=HospitalAutomation;Integrated Security=true";
        private SqlConnection _connection;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        public DatabaseBusiness()
        {
            _connection = new SqlConnection(ConnectionString);
        }


        private SqlConnection OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
            return _connection;
        }

        private void CloseConnection()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public void ExecuteCommand(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            using (SqlCommand _command = new SqlCommand(cmdText, OpenConnection()))
            {
                _command.CommandType = cmdType;
                _command.Parameters.AddRange(parameters);
                _command.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public DataTable ExecuteAdapter(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlDataAdapter _adapter = new SqlDataAdapter(cmdText, ConnectionString);
            _adapter.SelectCommand.Parameters.AddRange(parameters);
            _adapter.SelectCommand.CommandType = cmdType;
            DataTable _dt = new DataTable();
            _adapter.Fill(_dt);
            return _dt;
        }

        public DataTable ExecuteAdapter(string cmdText)
        {
            SqlDataAdapter _adapter = new SqlDataAdapter(cmdText, ConnectionString);
            DataTable _dt = new DataTable();
            _adapter.Fill(_dt);
            return _dt;
        }

        public DataTable ExecuteAdapter(string cmdText, CommandType cmdType, SqlParameter[] parameters, out LoginState state)
        {
            SqlDataAdapter _adapter = new SqlDataAdapter(cmdText, ConnectionString);

            SqlParameter _returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
            _returnValue.Direction = ParameterDirection.ReturnValue;
            _adapter.SelectCommand.Parameters.AddRange(parameters);
            _adapter.SelectCommand.Parameters.Add(_returnValue);
            _adapter.SelectCommand.CommandType = cmdType;
            DataTable _dt = new DataTable();
            _adapter.Fill(_dt);
            if (Convert.ToInt32(_returnValue.Value) == -1)
                state = LoginState.UserExistsPasswordWrong;
            else if (Convert.ToInt32(_returnValue.Value) == 0)
                state = LoginState.UserNotExists;
            else
                state = LoginState.UserExists;
            return _dt;
        }

    }

    public enum LoginState
    {
        UserExistsPasswordWrong = -1,
        UserNotExists = 0,
        UserExists = 1
    }
}
