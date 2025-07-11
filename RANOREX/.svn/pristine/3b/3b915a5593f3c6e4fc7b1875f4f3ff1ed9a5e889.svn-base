/*
 * Created by Ranorex
 * User: anpatel
 * Date: 2/25/2020
 * Time: 1:28 PM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using ATS.CodeLibrary.DataUtilities.Cryptography;
using System.IO;
using System.Diagnostics;
using ATS.CodeLibrary.DataUtilities.SQL;

namespace GlobalServicesPlatformTesting
{
    /// <summary>
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public class GSPUserCodeCollection
    {
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
        
        public static SqlCredentials credsJDEDev = new SqlCredentials("CA15A2450", "JDE_DEVELOPMENT", "integration", CryptographyHelper.DecryptString("XrqqBbXPhNAVo7l1T2YU1Q=="));
        private static Random random = new Random();
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void SQLCheckJDESalesOrderPONumber(string PONumber, int timeoutMinutes)
        {
        	int totalTimeout = timeoutMinutes * 2;
        	int i = 0;
        	var SQL = "SELECT COUNT(*) FROM TESTDTA.F4211 WHERE SDVR01 = @PONumber";
        	var dic = new Dictionary<string, object>{{"@PONumber", PONumber}};
        	var results = SqlHelper.ExecuteQuery(credsJDEDev,SQL,dic).Tables[0];
        	int count = Convert.ToInt32(results.Rows[0][0].ToString());
        	Report.Info("Searching for PO Number: " + PONumber + " in JDE...Found " + count.ToString() + " instance(s)");
        	if(count<1)
        	{
        		do
        		{      		
        			Report.Info("Did not find PO Number in JDE Dev, retrying in 30 seconds");
        			Delay.Seconds(30);
					results = SqlHelper.ExecuteQuery(credsJDEDev,SQL,dic).Tables[0];
        			count = Convert.ToInt32(results.Rows[0][0].ToString());
        			Report.Info("Searching for PO Number: " + PONumber + " in JDE...Found " + count.ToString() + " instance(s)");        		
        			i++;
        		}while((count<1) && (i<totalTimeout));
        	}
        	
        	if(i>=totalTimeout)
        	{
        		Report.Error("Could not find PO Number in JDE Dev after " + timeoutMinutes + " minutes, the check timed out.");
        		Ranorex.Validate.Fail();
        	}
        	
        	Delay.Seconds(10);
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string RandomString(int length)
        {
        	const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(System.Linq.Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string FindValidPrice(string DomRxPath)
        {
        	int count = 0;
			decimal t1 = 0;
        	WebDocument webDoc = DomRxPath;
        	IList<Ranorex.SpanTag> spanList = new System.Collections.Generic.List<Ranorex.SpanTag>();
        	for (int i = 1; i < 8; i++) 
        	{
        		var tempSpanList = webDoc.Find<SpanTag>(".//div[#'product-grid']/div[@class='row items4']["+ i.ToString()+"]//div[@class='cc-item-price']/span[2]");
        		((List<SpanTag>)spanList).AddRange(tempSpanList);
        	
        	}

        	
        		foreach (var spanItem in spanList)
        		{
        		     			
        				if(spanItem.InnerText.Contains("$") && (count<2))
        				{
        					t1 = t1 + Convert.ToDecimal(spanItem.InnerText.Substring(1));
        					Report.Log(ReportLevel.Info, "Price: " + spanItem.InnerText);
        					var Span = spanItem.FindSingle<Ranorex.SpanTag>(spanItem.GetPath() + "/../..//div[4]//span[@innertext='Add to Cart']");
        					Span.UseEnsureVisible = true;
        					Span.Click();
        					count++;
        				}
        				if (count>=2)
        				{
        					break;
        				}
        			
        		}
        		Report.Log(ReportLevel.Info, "Total Price: " + "$" + t1.ToString());
        		return "$" + t1.ToString();
        }
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string DecryptPassword(string encryptedPW)
        {
        	return CryptographyHelper.DecryptString(encryptedPW);
        }
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string ReadAllTextFromFile(string filePath)
        {
        	return File.ReadAllText(filePath);
        }
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void WriteAllTextToFile(string filePath, string fileContent)
        {
        	File.WriteAllText(filePath, fileContent);
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void KillProcess(string processname)
        {
        	Process[] processes = Process.GetProcesses();
         	if (processes.Length == 0)
            {
                Report.Warn(string.Format("Process '{0}' not found.", processname));
            }
	        foreach (var proc in processes) 
	        {
	        	if (proc.ProcessName.Contains(processname))
	        	{
	        		try
	                {
	                    proc.Kill();
	                    Report.Info("Process killed: " + proc.ProcessName);
	                }
	                catch (Exception ex)
	                {
	                    Report.Warn(ex.Message);
	                }
	        	}
	        	
	        }
        }
    }
}
