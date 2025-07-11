/*
 * Created by Ranorex
 * User: kdekroon
 * Date: 4/11/2017
 * Time: 4:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace PLM_Mechanical_Deploy_Test
{

    public class GlobalProjectData
    {
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
        private static DataTable _instance;
        private static DataTable Instance
        {
        	get
            {
                if (_instance == null)
                {
					string sqlStatement = "SELECT property_name, property_value FROM dbo.tblConfigurationProperties WHERE application_id = 'PLMWorxRanorex' AND property_name LIKE '" + TestSuite.Current.Parameters["RunEnvironment"] + "%'";
                    _instance = MySqlHelper.ExecuteQueryConfigurationsDatabase(sqlStatement);
                }
                return _instance;
            }	
        }
        
        public static string GetConfigurationValue(string propertyName)
        {
			return Instance.AsEnumerable().First(row => row["property_name"].ToString() == propertyName)["property_value"].ToString();
        }
    }
}
