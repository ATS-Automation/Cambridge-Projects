/*
 * Created by Ranorex
 * User: anpatel
 * Date: 6/19/2018
 * Time: 1:09 PM
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
using ATS.CodeLibrary.Email;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;
using Ranorex.AutomationHelpers.Modules;
using System.IO;

namespace UpchainMechDeployTest
{
    /// <summary>
    /// Description of ATSEmail.
    /// </summary>
    [TestModule("2709F2A7-7243-4D3C-AAE5-05097F0A4ED6", ModuleType.UserCode, 1)]
    public class ATSEmail : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// 
        
        string _upChainVersion = "";
        [TestVariable("c9efba5b-2d7a-4c6a-b611-58bde2b5d45b")]
        public string upChainVersion
        {
        	get { return _upChainVersion; }
        	set { _upChainVersion = value; }
        }
        
        
        public ATSEmail()
        {
        	
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        /// 
        
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            string reportPath = CreatePdfReport();
            if (!(upChainVersion =="" || upChainVersion == null))
        	{
        		if(TestSuite.Current.SelectedRunConfig.ToString()=="Heartbeat")
        		{
        			if(UserCodeMethods.GetRowDataFromColumnName("Heartbeat", "EnvironmentHeartbeat") == "QA Cambridge")
        			{
        				PostPDFtoSharePoint(upChainVersion, "HeartbeatQA", reportPath);
        			}
        			else
        			{
        				PostPDFtoSharePoint(upChainVersion, "HeartbeatPROD", reportPath);
        			}
        		}
            	else
            	{
        			PostPDFtoSharePoint(upChainVersion, TestSuite.Current.Parameters["RunEnvironment"], reportPath);
            	}
        	}
        	SendMail(reportPath);
        	
        }
        
        private string CreatePdfReport()
        {
                ReportToPDFModule pdfModule = new ReportToPDFModule();
                pdfModule.PdfName = TestReport.ReportEnvironment.ReportName + ".pdf";
                pdfModule.Xml = @"\\ca01a9001\pgmis\Deployment\Ranorex\style.xml";
                pdfModule.Details = "all";
                return pdfModule.CreatePDF();
   
        }
        
        public void SendMail(string reportPath)
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
      	
            	if(TestSuite.Current.SelectedRunConfig.ToString()=="Heartbeat")
            	{
            		subject =  TestSuite.Current.SelectedRunConfig.ToString() + " Test in Environment " + UserCodeMethods.GetRowDataFromColumnName("Heartbeat","EnvironmentHeartbeat") + " " + sResult;
            	}
            	else
            	{
            		subject = "Mechanical Deploy Test in the " + TestSuite.Current.Parameters["RunEnvironment"] + " environment Test Config: " + TestSuite.Current.SelectedRunConfig.Name.ToString() + " " + sResult;
            	}
            	if (sResult == "Passed")
            	{
            		addressTo = "anpatel@atsautomation.com; chenderson@atsautomation.com; brhill@atsautomation.com; stauqeer@atsautomation.com; tchang@atsautomation.com";
            	//	addressTo = "anpatel@atsautomation.com;";
            	}
            	else
            	{
            		addressTo = "anpatel@atsautomation.com; chenderson@atsautomation.com; brhill@atsautomation.com; stauqeer@atsautomation.com; tchang@atsautomation.com";
            	//	addressTo = "anpatel@atsautomation.com;";
            	}
            	if (System.IO.File.Exists(reportPath))
           		{
            
           		 	body = subject + ".  See Attached PDF for more info.";
            		 if (!(upChainVersion =="" || upChainVersion == null))
            		 {
            		 	body = body + "\n\n Open sharepoint folder for PDF:\n\n https://portal/corit/corpapp/General%20Documentation/PLM/Deploy/Testing/" + System.DateTime.Now.Year.ToString();
            		 }
           		 	MailHelper.SendMessage(body, addressTo, subject, "relay2.atsna.atsauto.net", addressFrom, "PLM Deploy Test", new [] { reportPath });
            	}
            
            	else
            	{
            		Report.Info("Could not find file: " + reportPath);
            	} 
            }
            catch (Exception ex)
            	{
            		Report.Failure("Mail Error: " + ex.ToString());
            	}
        }
        public void PostPDFtoSharePoint (string version, string env, string pdfpath)
		{
			try 
			{
				string datesubstring = version.Substring(version.IndexOf("at") + 3);
            	string day = datesubstring.Substring(0, 2);
            	string month = datesubstring.Substring(3, 2);
            	string year = datesubstring.Substring(6, 4);
				string SPdirectory = @"\\portal@SSL\DavWWWRoot\corit\corpapp\General Documentation\PLM\Deploy\Testing";
          //  	string SPsite = @"https://portal/corit/corpapp/General%20Documentation/Forms/AllItems.aspx?RootFolder=%2Fcorit%2Fcorpapp%2FGeneral%20Documentation%2FPLM%2FDeploy%2FTesting%2F2017&FolderCTID=0x012000FAC402B95968F148A04148BC661E3DBB&View=%7BF9039D9C%2D5993%2D4748%2DB0BA%2DA505940BBC41%7D";
            	string date = month + "-" + day + "-" + year;
            	if (!Directory.Exists(SPdirectory))
            	{
            		int i = 0;
            		do
            		{
            			
            			Report.Log(ReportLevel.Warn, "Cannot log into SharePoint, running SharepointLogOn Recording Module...");
            			UpchainMechDeployTest.SharepointLogOn.Start();
            			
            			i++;
            		}while (!Directory.Exists(SPdirectory) && i<=3);
            	}
            	if (!Directory.Exists(SPdirectory + "\\" + year))
            	{
            		Directory.CreateDirectory(SPdirectory + "\\" + year);
            	}
            	if (Directory.Exists(SPdirectory + "\\" + year))
            	{
            		string dirName = "r" + version.Remove(version.IndexOf("at")) + date + "-" + env + "-" + "RANOREX";
            		if (!Directory.Exists(SPdirectory + "\\" + year + "\\" + dirName))
            		{
            			Directory.CreateDirectory(SPdirectory + "\\" + year + "\\" + dirName);
            		}
            		
            		if(TestSuite.Current.SelectedRunConfig.ToString()=="Heartbeat")
            		{
            			File.Copy(pdfpath, SPdirectory + "\\" + year + "\\" + dirName + "\\MechanicalDeployTest-" + UserCodeMethods.GetRowDataFromColumnName("Heartbeat","EnvironmentHeartbeat") + ".pdf", true);
            		}
            		else
            		{
            			File.Copy(pdfpath, SPdirectory + "\\" + year + "\\" + dirName + "\\MechanicalDeployTest.pdf", true);
            		}
            		Report.Success("Sharepoint", "File saved to SharePoint successfully");
            		string SharepointFilePath=@"https://portal/corit/corpapp/General%20Documentation/PLM/Deploy/Testing/" + year;
            		Report.LogHtml(
                    ReportLevel.Success,
                    "SharepointPDFFolder",
                    string.Format(
                        "Sharepoint PDF location: <a href='{0}' target='_blank'>Open Sharepoint Folder</a>",
                        SharepointFilePath + "/" + dirName));
            	}
            	
			} 
			catch (Exception ex) 
			{
				Report.Error("Error occured: " + ex.ToString());	
			}
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
    }
}
