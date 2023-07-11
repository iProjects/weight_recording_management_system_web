﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_service_ui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                SplashScreen.ShowSplashScreen();

                Application.DoEvents();

                //SplashScreen.SetStatus("Checking for [ " + SystemsConfigFile + " ] ...");
                //if (!File.Exists(SystemsConfigFile))
                //    throw new FileNotFoundException("SB Payroll cannot locate configuration file " + SystemsConfigFile);

                SplashScreen.SetStatus("Checking for Default System...");
                //SBSystem defSys = SQLHelper.GetDataDefaultSystem();
                //if (defSys == null)
                //    throw new ArgumentException("No Default System is Set", "system");

                RegistryAccess.SetStringRegistryValue(SplashScreen.app_name, SplashScreen.app_name);

                //SplashScreen.SetStatus("Connecting to the SQL Server [" + defSys.Server + "]");
                //if (!SQLHelper.ServerExists(defSys))
                //    throw new ArgumentException("Unable to connect to Server [" + defSys.Server + "] ", "server");

                SplashScreen.SetStatus("Checking for a valid Database...");
                //if (!SQLHelper.DatabaseExists(defSys))
                //    throw new ArgumentException("Database [ " + defSys.Database + " ] does not exist in Server [ " + defSys.Server + " ] ", "database");

                SplashScreen.SetStatus("Checking for Database Version...");
                //string dbver = SQLHelper.DatabaseVersion(defSys);
                //string sysver = Assembly.GetEntryAssembly().GetName().Version.ToString();
                //if (!dbver.Equals(sysver))
                //    throw new ArgumentException("Database and System Version do not match; the Database may not be usable. Use a Database Migration Tool", "version");
                
                SplashScreen.SetStatus("Checking Defaults Tables are populated...");

                System.Threading.Thread.Sleep(4000);
                
                Application.Run(new test_service_form());

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
            
        }
    }
}
