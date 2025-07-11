/*
 * Created by Ranorex
 * User: kdekroon
 * Date: 3/27/2017
 * Time: 9:32 AM
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
    public class GetTiming
    {
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
        public static IDictionary<string, string> Settings { get; private set; }
        
        private static System.DateTime StartTime;
        
        public static TimeSpan ActionTime {get; private set;}
   
        
        [UserCodeMethod]
        public static void getStartTime()
        {
            Report.Log(ReportLevel.Info,"Start");  
            StartTime = System.DateTime.Now;   
        }
        
        [UserCodeMethod]
        public static void getEndTime(string action_name)
        {
        	Settings = ATS.CodeLibrary.Configuration.ConfigurationHelper.GetAllLocalSettingsDecrypted();
            System.DateTime EndTime = System.DateTime.Now;   
              
            ActionTime = EndTime - StartTime;  
            //ActionTime = duration.Minutes.ToString()+":"+duration.Seconds.ToString()+"."+duration.Milliseconds.ToString();  
            Report.Log(ReportLevel.Info,"Action time was " + ActionTime.TotalMilliseconds.ToString() + " seconds.");
        	
            SqlCredentials creds = new SqlCredentials(
            			Settings["SqlServer"], 
            			Settings["SqlDatabase"],
            			Settings["SqlUsername"],
            			Settings["SqlPassword"],
            			Settings["SqlDomain"]);
            // get action_id
           
        	try
        	{
        		var parameters = new Dictionary<string, object>
        		{
        			{ "@Username",  Environment.UserName.Substring(0, Math.Min(Environment.UserName.Length, 256)) },
        			{ "@ActionTime", ActionTime.TotalMilliseconds},
        			{ "@ActionName", action_name.Substring(0, Math.Min(action_name.Length,256)) },
        			{ "@RunID", RecordRunInDatabase.run_id}
        			
        		};
        		
        		SqlHelper.ExecuteNonQuery(creds, "INSERT INTO TestTimeTransactions(" +
        		                          "run_id, " +
        		                          "created_date, " +
        		                          "created_by, " +
        		                          "action_id, " +
        		                          "duration_seconds)" +
        		                          "VALUES(" 
        		                          + "@RunID,"
        		                          + "GETDATE(),"
        		                          + "@Username," 
        		                          + "(SELECT action_id FROM Actions WHERE action_name = @ActionName)"
        		                          + "@ActionTime)", parameters);
        		EndTime = new System.DateTime();
        		ActionTime = new System.TimeSpan();
        		
				Report.Log(ReportLevel.Info,"Action time was successfully written to the database.");
        	}
        	catch(Exception ex)
        	{
        		Report.Log(ReportLevel.Error, "Failed to write values to database\n" + ex.Message);
        	}
        }
    }
}
