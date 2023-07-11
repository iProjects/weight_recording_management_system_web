﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace weight_recording_service_dal
{
    public sealed class postgresqlapisingleton
    {
        // Because the _instance member is made private, the only way to get the single
        // instance is via the static Instance property below. This can also be similarly
        // achieved with a GetInstance() method instead of the property.
        private static postgresqlapisingleton singleInstance;

        public static postgresqlapisingleton getInstance(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new postgresqlapisingleton(notificationmessageEventname);
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        private string CONNECTION_STRING = @"Server=127.0.0.1;Database=july;User Id=sa;Password=123456789;Port=5432;";
        private const string db_name = "july";//DBContract.DATABASE_NAME;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private string TAG;

        private postgresqlapisingleton(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
            try
            {
                TAG = this.GetType().Name;
                setconnectionstring();
                createdatabaseonfirstload();
                createtablesonfirstload();
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
            }
        }

        private postgresqlapisingleton()
        {

        }
        private void setconnectionstring()
        {
            try
            {
                postgresqlconnectionstringdto _connectionstringdto = new postgresqlconnectionstringdto();

                _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["postgresql_datasource"];
                _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["postgresql_database"];
                _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["postgresql_userid"];
                _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["postgresql_password"];
                _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["postgresql_port"];

                CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
            }
        }

        string buildconnectionstringfromobject(postgresqlconnectionstringdto _connectionstringdto)
        {
            string CONNECTION_STRING = @"Server=" + _connectionstringdto.datasource + ";" +
                "Database=" + _connectionstringdto.database + ";" +
                "Port=" + _connectionstringdto.port + ";" +
                "User Id=" + _connectionstringdto.userid + ";" +
                "Password=" + _connectionstringdto.password;
            return CONNECTION_STRING;
        }
        void createdatabaseonfirstload()
        {
            postgresqlconnectionstringdto _connectionstringdto = new postgresqlconnectionstringdto();

            _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["postgresql_datasource"];
            _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["postgresql_database"];
            _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["postgresql_userid"];
            _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["postgresql_password"];
            _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["postgresql_port"];
            _connectionstringdto.new_database_name = System.Configuration.ConfigurationManager.AppSettings["postgresql_database"];

            createdatabasegivenname(_connectionstringdto);
        }
        void createtablesonfirstload()
        {
            postgresqlconnectionstringdto _connectionstringdto = new postgresqlconnectionstringdto();

            _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["postgresql_datasource"];
            _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["postgresql_database"];
            _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["postgresql_userid"];
            _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["postgresql_password"];
            _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["postgresql_port"];
            _connectionstringdto.new_database_name = System.Configuration.ConfigurationManager.AppSettings["postgresql_database"];

            createtables(_connectionstringdto);
        }
        public responsedto createdatabasegivenname(postgresqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                _connectionstringdto.database = "postgres";

                string CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

                string query = "CREATE DATABASE " + _connectionstringdto.new_database_name + ";";

                using (var con = new NpgsqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                        _responsedto.isresponseresultsuccessful = true;
                        _responsedto.responsesuccessmessage = "successfully created database [ " + _connectionstringdto.new_database_name + " ] in " + DBContract.postgresql + ".";
                        _responsedto.responseresultobject = _connectionstringdto;
                        return _responsedto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public bool createdatabase()
        {
            try
            {
                string _default_database_name = utilzsingleton.getInstance(_notificationmessageEventname).getappsettinggivenkey("postgresql_database", "july");
                string query = "CREATE DATABASE " + _default_database_name + ";";
                using (var con = new NpgsqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }
        }
        public responsedto createtables(postgresqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            responsedto _innerresponsedto = new responsedto();
            try
            {

                _connectionstringdto.database = _connectionstringdto.new_database_name;
                string CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

                bool does_table_exist_in_db = checkiftableexists(CONNECTION_STRING, DBContract.weightsentitytable.TABLE_NAME, _connectionstringdto.new_database_name);

                //Create the table 
                string SQL_CREATE_CROPS_TABLE = " CREATE TABLE IF NOT EXISTS " + DBContract.weightsentitytable.TABLE_NAME + " (" +
                      DBContract.weightsentitytable.WEIGHT_ID + " SERIAL PRIMARY KEY NOT NULL, " +
                      DBContract.weightsentitytable.WEIGHT_WEIGHT + " TEXT, " +
                      DBContract.weightsentitytable.WEIGHT_DATE + " TEXT, " +
                      DBContract.weightsentitytable.WEIGHT_STATUS + " TEXT, " +
                      DBContract.weightsentitytable.CREATED_DATE + " TEXT, " +
                      DBContract.weightsentitytable.WEIGHT_APP + " TEXT " +
                       " ); ";

                _innerresponsedto = createtable(SQL_CREATE_CROPS_TABLE, CONNECTION_STRING);
                if (_innerresponsedto.isresponseresultsuccessful)
                    _responsedto.responsesuccessmessage += _innerresponsedto.responsesuccessmessage;
                else
                    _responsedto.responseerrormessage += _innerresponsedto.responseerrormessage;

                string successmsg = "successfully created tables in database [ " + _connectionstringdto.database + " ] - server [ " + DBContract.postgresql + " ].";
                int msg_length = successmsg.Length;
                msg_length = msg_length + 1;
                int stars_printed = 0;
                string str_stars = "";
                string str_newline = Environment.NewLine;

                while (stars_printed != msg_length)
                {
                    str_stars += "*";
                    ++stars_printed;
                }

                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_stars;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += successmsg;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_stars;

                _responsedto.isresponseresultsuccessful = true;
                return _responsedto;

            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += Environment.NewLine + ex.Message;
                return _responsedto;
            }
        }

        bool checkiftableexists(string CONNECTION_STRING, string table_name, string database_name)
        {
            try
            {
                string query = "SELECT EXISTS (SELECT FROM information_schema.tables  WHERE  table_schema = '" + database_name + "' AND table_name = '" + table_name + "');";

                using (var con = new NpgsqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        //execute the query  
                        int _rows_affected = cmd.ExecuteNonQuery();
                        Console.WriteLine("_rows_affected [ " + _rows_affected + " ]");

                        var da = new NpgsqlDataAdapter(cmd);
                        var dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();

                        int _rows_count = dt.Rows.Count;
                        if (_rows_count > 0) return true;
                        else return false;
                    }
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }
        }

        public responsedto createtable(string query, string CONNECTION_STRING)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                //setup the connection to the database
                using (var con = new NpgsqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        //execute the query  
                        int _rows_affected = cmd.ExecuteNonQuery();
                        Console.WriteLine("_rows_affected [ " + _rows_affected + " ]");

                        _responsedto.isresponseresultsuccessful = true;
                        string[] _conn_arr = CONNECTION_STRING.Split(new char[] { ';' });
                        _conn_arr.SetValue("", _conn_arr.Length - 1);
                        _conn_arr.SetValue("", _conn_arr.Length - 2);
                        string _sanitized_conn_arr = _conn_arr[0] + ";" + _conn_arr[1];
                        _responsedto.responsesuccessmessage += "successfully executed query [ " + query + " ] against connection [ " + _sanitized_conn_arr + " ]." + Environment.NewLine;
                        return _responsedto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                Exception _exception = ex.GetBaseException();
                _responsedto.responseerrormessage += Environment.NewLine + "error executing query [ " + query + " ].";
                _responsedto.responseerrormessage += Environment.NewLine + Environment.NewLine + _exception.Message + Environment.NewLine;
                return _responsedto;
            }
        }

        public responsedto checkpostgresqlconnectionstate()
        {
            responsedto _responsedto = new responsedto();
            try
            {
                postgresqlconnectionstringdto _connectionstringdto = getpostgresqlconnectionstringdto();

                _responsedto = checkconnectionasadmin(_connectionstringdto);

                return _responsedto;
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public postgresqlconnectionstringdto getpostgresqlconnectionstringdto()
        {
            try
            {
                postgresqlconnectionstringdto _connectionstringdto = new postgresqlconnectionstringdto();

                _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["postgresql_datasource"];
                _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["postgresql_database"];
                _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["postgresql_userid"];
                _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["postgresql_password"];
                _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["postgresql_port"];

                return _connectionstringdto;

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return null;
            }
        }

        public responsedto checkconnectionasadmin(postgresqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string CONNECTION_STRING = @"Server=" + _connectionstringdto.datasource + ";" +
                "Database=" + _connectionstringdto.database + ";" +
                "User Id=" + _connectionstringdto.userid + ";" +
                "Password=" + _connectionstringdto.password + ";" +
                "Port=" + _connectionstringdto.port;

                string query = DBContract.WEIGHTS_SELECT_ALL_QUERY;

                int count = getrecordscountgiventable(DBContract.weightsentitytable.TABLE_NAME, CONNECTION_STRING);

                if (count != -1)
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "connection to postgresql successfull. records count [ " + count + " ]";
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responseerrormessage = "connection to postgresql failed.";
                    return _responsedto;
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public int getrecordscountgiventable(string tablename, string CONNECTION_STRING)
        {
            string query = "SELECT * FROM " + tablename;
            DataTable dt = getallrecordsglobal(query, CONNECTION_STRING);
            if (dt != null)
                return dt.Rows.Count;
            else
                return -1;
        }
        public bool isdbconnectionalive(string CONNECTION_STRING)
        {
            var con = new NpgsqlConnection(CONNECTION_STRING);
            con.Open();
            return true;
        }

        public bool isdbconnectionalive()
        {
            try
            {
                //setup the connection to the database
                var con = new NpgsqlConnection(CONNECTION_STRING);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }

        }

        public DataTable getallrecordsglobal(string query, string CONNECTION_STRING)
        {
            if (!isdbconnectionalive(CONNECTION_STRING)) return null;

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            using (var con = new NpgsqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    var da = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }

        public DataTable getallrecordsglobal(string query)
        {
            if (!isdbconnectionalive()) return null;

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            using (var con = new NpgsqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    var da = new NpgsqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }
        public DataTable getallrecordsglobal()
        {
            DataTable dt = getallrecordsglobal(DBContract.WEIGHTS_SELECT_ALL_QUERY);
            return dt;
        }

        public List<weight_record_dto> getallweights()
        {
            List<weight_record_dto> weights = new List<weight_record_dto>();
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(DBContract.WEIGHTS_SELECT_ALL_QUERY, conn))
                {
                    conn.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        weight_record_dto dto = new weight_record_dto();
                        dto.weight_id = reader.GetString(0);
                        dto.weight_weight = reader.GetString(1);
                        dto.weight_date = reader.GetString(2);
                        dto.weight_status = reader.GetString(3);
                        dto.created_date = reader.GetString(4);
                        dto.weight_app = reader.GetString(5);

                        weights.Add(dto);
                    }
                }
            }
            return weights;
        }

        public int insertgeneric(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;
            //setup the connection to the database
            using (var con = new NpgsqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }

        public int insertgeneric(string query, Dictionary<string, object> args, string CONNECTION_STRING)
        {
            int numberOfRowsAffected;
            if (CONNECTION_STRING == null)
            {
                numberOfRowsAffected = insertgeneric(query, args);
                return numberOfRowsAffected;
            }
            else if (String.IsNullOrEmpty(CONNECTION_STRING))
            {
                numberOfRowsAffected = insertgeneric(query, args);
                return numberOfRowsAffected;
            }
            else
            {

                //setup the connection to the database
                using (var con = new NpgsqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        //set the arguments given in the query
                        foreach (var pair in args)
                        {
                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                        }
                        //execute the query and get the number of row affected
                        numberOfRowsAffected = cmd.ExecuteNonQuery();
                    }
                    return numberOfRowsAffected;
                }
            }
        }

        public responsedto createweightindatabase(weight_record_dto _weight_record_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "INSERT INTO " +
                DBContract.weightsentitytable.TABLE_NAME +
                " ( " +
                DBContract.weightsentitytable.WEIGHT_WEIGHT + ", " +
                DBContract.weightsentitytable.WEIGHT_DATE + ", " +
                DBContract.weightsentitytable.WEIGHT_STATUS + ", " +
                DBContract.weightsentitytable.CREATED_DATE + ", " +
                DBContract.weightsentitytable.WEIGHT_APP +
                " ) VALUES(@weight_weight, @weight_date, @weight_status, @created_date, @weight_app)";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@weight_weight", _weight_record_dto.weight_weight},
				    {"@weight_date", _weight_record_dto.weight_date},
                    {"@weight_status", "active"},
				    {"@created_date", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")},
                    {"@weight_app", _weight_record_dto.weight_app}
			    };

                int numberOfRowsAffected = insertgeneric(query, args, CONNECTION_STRING);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record creation failed in " + DBContract.postgresql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record created successfully in " + DBContract.postgresql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }










    }
}
