/*
 * Created by Ranorex
 * User: anpatel
 * Date: 3/2/2021
 * Time: 6:05 PM
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
using Ranorex.AutomationHelpers.Modules;
using ATS.CodeLibrary.Email;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Reporting;
using System.IO;

namespace UpchainMechDeployTest
{
    /// <summary>
    /// Description of EmailPerformanceReport.
    /// </summary>
    [TestModule("8C461A60-C2D5-40D0-9D01-4D064EE8DE15", ModuleType.UserCode, 1)]
    public class EmailPerformanceReport : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public EmailPerformanceReport()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            string reportPath = CreatePdfReport();
            string prpath = @"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainDownloadPerformance\Performance.csv";
            SendMail(reportPath, prpath);
      
        }
        
        private string CreatePdfReport()
        {
                ReportToPDFModule pdfModule = new ReportToPDFModule();
                pdfModule.PdfName = TestReport.ReportEnvironment.ReportName + ".pdf";
                pdfModule.Xml = @"\\ca01a9001\pgmis\Deployment\Ranorex\style.xml";
                pdfModule.Details = "all";
                return pdfModule.CreatePDF();
   
        }
        private static string GetTestSuiteStatus()
        {
            string status = "";

            var rootChildren = ActivityStack.Instance.RootActivity.Children;

            if (rootChildren.Count > 1)
            {
                Console.WriteLine("Multiple TestSuiteActivites, status taken from first entry");
            }

            var testSuiteAct = rootChildren[0] as TestSuiteActivity;

            if (testSuiteAct != null)
            {
              //  status = testSuiteAct.Status.ToString();
                if (testSuiteAct.TotalFailedTestCaseCount>0)
                {
                	status = "Failed";
                }
                else
                {
                	status = "Success";
                }
            }

            return status;
        }
        
        public void SendMail(string reportPath, string perfReportPath)
        {
			
        	string sResult = GetTestSuiteStatus();
        	string subject, addressTo, addressFrom, body;
            addressFrom = "ranorexrt@atsautomation.com";
            if (sResult.Contains("Success"))
           	{
           		sResult = "Passed";
           	}

            try 
            {
            	string env = @"PROD Thick/PROD Cloud";
            	if(File.Exists(reportPath))
            	{
            		if(UserCodeMethods.GetRowDataFromColumnName("Heartbeat", "EnvironmentHeartbeat") == "QA Cambridge")
            		{
            			env = @"QA Thick/PROD Cloud";
            		}
            		
            		subject = "Download Performance Test in Environment: " + env + " " + sResult;
            		//addressTo = "anpatel@atsautomation.com";
            		body = "See attached csv file for more details";
            		addressTo = "anpatel@atsautomation.com; chenderson@atsautomation.com; brhill@atsautomation.com; stauqeer@atsautomation.com; tchang@atsautomation.com; sageorge@atsautomation.com";
            		MailHelper.SendMessage(body, addressTo, subject, "relay2.atsna.atsauto.net", addressFrom, "Upchain Download Performance Test", new [] { reportPath, perfReportPath });
            	}
            	else
            	{
            		Report.Info("Could not find the file: " + reportPath);
            	} 
            	
            }
            catch (Exception ex)
            	{
            		Report.Failure("Mail Error: " + ex.ToString());
            	}
        }
    }
}
