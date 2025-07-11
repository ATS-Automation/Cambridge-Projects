/*
 * Created by Ranorex
 * User: kdekroon
 * Date: 4/11/2017
 * Time: 12:48 PM
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
using System.Data;

using ATS.CodeLibrary.DataUtilities.SQL;

namespace PLM_Mechanical_Deploy_Test
{
    public class MySqlHelper
    {
    	private static IDictionary<string, string> _Settings = null;
    	public static IDictionary<string, string> Settings 
    	{ 
    		get
    		{
    			if (_Settings == null)
    				_Settings = ATS.CodeLibrary.Configuration.ConfigurationHelper.GetAllLocalSettingsDecrypted();
    			
    			return _Settings;
    		}
    	}
    	
    	private static int environmentID = 0;
        public static int EnvironmentID 
        { 
        	get
        	{
        		if (environmentID == 0)
        		{        			
        			string sqlStatement = "SELECT environment_id FROM Environments WHERE environment_name = '" + TestSuite.Current.Parameters["RunEnvironment"] + "'";       		
        			var results =  ExecuteQueryDeployTestDatabase(sqlStatement);  
        			environmentID = int.Parse(results.Rows[0][0].ToString());
        		}
        		
        		return environmentID;
        	}
        }
    	
    	private static SqlCredentials deployTestCreds = null;    	
    	public static SqlCredentials DeployTestCreds {
    		get
    		{
    			if (deployTestCreds == null)
    				deployTestCreds = new SqlCredentials(
            			Settings["SqlServer"], 
            			Settings["SqlDatabase"],
            			Settings["SqlUsername"],
            			Settings["SqlPassword"],
            			Settings["SqlDomain"]);
    			
    			return deployTestCreds;
    		}
    	}
    	
    	private static SqlCredentials configurationCreds = null;    	
    	public static SqlCredentials ConfigurationCreds {
    		get
    		{
    			if (configurationCreds == null)
    				configurationCreds = new SqlCredentials(
            			Settings["atsConfigurationsServer"], 
            			Settings["atsConfigurationsDatabase"],
            			Settings["atsConfigurationsUsername"],
            			Settings["atsConfigurationsPassword"]);
    			
    			return configurationCreds;
    		}
    	}

        public static DataTable ExecuteQueryDeployTestDatabase(string sqlStatement, Dictionary<string, object> parameters = null)
        {        	      	
        	// Run query to translate RunEnvironment global variable into an EnvironmentID 
        	var ResultsList = SqlHelper.ExecuteQuery(DeployTestCreds, sqlStatement, parameters);
        	var Results = ValidateResults(ResultsList);
    		return Results;    	
        }
        

        public static int ExecuteScalarDeployTestDatabase(string sqlStatement, Dictionary<string, object> parameters = null)
        {
        	var ResultsList = SqlHelper.ExecuteQuery(DeployTestCreds,sqlStatement, parameters);
        	var Results = ValidateResults(ResultsList);
        	return int.Parse(Results.Rows[0][0].ToString());
        }
        
        public static DataTable ExecuteQueryConfigurationsDatabase(string sqlStatement)
        {        	   	
        	var ResultsList = SqlHelper.ExecuteQuery(ConfigurationCreds,sqlStatement);  
        	var Results = ValidateResults(ResultsList);
    		return Results;    
        }
        
        private static DataTable ValidateResults(DataSet ResultsList)
        {
        	if (ResultsList == null)
        	{
        		throw new ArgumentException();
        	}
        		
        	var Results = ResultsList.Tables[0];        		
        	if (Results.Rows.Count == 0)
        	{
        		throw new ArgumentException();
        	}
        	
        	return Results;
        }        
    }
}
