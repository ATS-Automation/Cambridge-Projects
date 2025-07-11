/*
 * Created by Ranorex
 * User: kdekroon
 * Date: 4/5/2017
 * Time: 4:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ATS.CodeLibrary.DataUtilities.SQL;

namespace PLM_Mechanical_Deploy_Test
{
    /// <summary>
    /// Ranorex User Code collection. A collection is used to publish User Code methods to the User Code library.
    /// </summary>
    [UserCodeCollection]
    public class RecordRunInDatabase
    {
    	public static IDictionary<string, string> Settings { get; private set; }
    	
    	public static int run_id { get ; private set; }
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
        [UserCodeMethod]
        public static void TestRunMasterInsert(string EnvironmentID, string SoftwareVersion)
        {
            Report.Log(ReportLevel.Info,"Writing record to TestRunMaster table");  
       
            try
        	{
        		var parameters = new Dictionary<string, object>
        		{
        			{ "@Username",  Environment.UserName.Substring(0, Math.Min(Environment.UserName.Length, 256)) },
        			{ "@ConfigName", TestSuite.Current.SelectedRunConfig.Name.Substring(0, Math.Min(TestSuite.Current.SelectedRunConfig.Name.Length, 256))},
        			{ "@PLMWorxVersion", SoftwareVersion.Substring(0, Math.Min(SoftwareVersion.Length, 256))},
        			{ "@EnvironmentID", MySqlHelper.EnvironmentID},        			
        		};
        		
        		var sqlStatement = "INSERT INTO TestRunMaster (" +
        		                                     "created_date, " +
        		                                     "created_by, " +
        		                                     "run_config, " +
        		                                     "plmworx_version, " +
        		                                     "environment_id) "
        		                          + "VALUES (" 
        		                          + "GETDATE(),"
        		                          + "@Username," 
        		                          + "@ConfigName,"
        		                          + "@PLMWorxVersion,"
        		                          + "@EnvironmentID)"
        		                          + "SELECT SCOPE_IDENTITY()";
        		
        		run_id = MySqlHelper.ExecuteScalarDeployTestDatabase(sqlStatement, parameters);
        		
        		Report.Log(ReportLevel.Info,"Record was successfully written to the database.");
        	}
        	catch(Exception ex)
        	{
        		Report.Log(ReportLevel.Error, "Failed to write values to RunTestMaster table\n" + ex.Message);
        	}
        }
    }
}
