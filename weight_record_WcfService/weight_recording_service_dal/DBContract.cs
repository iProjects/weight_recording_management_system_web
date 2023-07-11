using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weight_recording_service_dal
{
    public static class DBContract
    {
        public static String DATABASE_NAME = "july";
        public static String SQLITE_DATABASE_NAME = "july.sqlite3";

        public static String error = "error";
        public static String info = "info";
        public static String warn = "warn";
        public static String APP = "c-sharp";

        public static String mssql = "mssql";
        public static String mysql = "mysql";
        public static String sqlite = "sqlite";
        public static String postgresql = "postgresql";


        public static String WEIGHTS_SELECT_ALL_QUERY = "SELECT * FROM " +
                                    DBContract.weightsentitytable.TABLE_NAME;

        public static String WEIGHTS_SELECT_ALL_FILTER_QUERY = "SELECT * FROM " +
                            DBContract.weightsentitytable.TABLE_NAME +
                            " where " +
                            DBContract.weightsentitytable.WEIGHT_STATUS +
                            " = " +
                            "'active'";

        //weights table
        public static class weightsentitytable
        {
            public static String TABLE_NAME = "tblweights";
            //Columns of the table
            public static String WEIGHT_ID = "weight_id";
            public static String WEIGHT_WEIGHT = "weight_weight";
            public static String WEIGHT_DATE = "weight_date";
            public static String WEIGHT_STATUS = "weight_status";
            public static String CREATED_DATE = "created_date";
            public static String WEIGHT_APP = "weight_app";

        }




    }
}
