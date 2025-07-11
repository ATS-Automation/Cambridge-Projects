using Ranorex;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using ATS.CodeLibrary.Email;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace ReportToPDF
{
	/// <summary>
	/// Allows conversion of Ranorex report files to PDF
	/// </summary>
	[TestModule("FFA0759D-37D2-4ABB-89A7-411F0FCF2DFE", ModuleType.UserCode, 1)]
	public class ReportToPDF : ITestModule
	{
		//Variables
		string PDFName;
		string xml;
		string details;
		static bool registered = false;
		static System.IO.DirectoryInfo zippedReportFileDirectoryInfo;
		
		string _varUpChain = "";
		[TestVariable("21542803-6fc6-4b53-b1c0-9e533aea9362")]
		public string varUpChain
		{
			get { return _varUpChain; }
			set { _varUpChain = value; }
		}
		
		string _SendEmailtoAll = "Yes";
		[TestVariable("5375769f-84b9-4720-b457-8a465af3b7c4")]
		public string SendEmailtoAll
		{
			get { return _SendEmailtoAll; }
			set { _SendEmailtoAll = value; }
		}
		
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public ReportToPDF()
		{
			//Init variables
			this.PDFName = "";
			
			this.xml =  @"\\ca01a9001\pgmis\Deployment\Ranorex\style.xml";
			
			//Possible values: none | failed | all
			this.details = "all";
		}
		
		/// <summary>
		/// Performs the playback of actions in this module.
		/// </summary>
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
			//Delegate must be registered only once
			if(!registered)
			{
				//PDF will be generated at the very end of the testsuite
				TestSuite.TestSuiteCompleted += delegate {
					
					//Specify report name if not already set
					if(String.IsNullOrEmpty(this.PDFName))
					{
						this.PDFName = CreatePDFName();
					}
					
					//Necessary to end the testreport in order to update the duration
					TestReport.EndTestModule();
					
					//Comment out if ConvertReportToPDF() is called directly
					try
					{
						Report.LogHtml(ReportLevel.Success,"PDFReport", "Successfully created PDF: <a href='" + ConvertReportToPDF(PDFName, xml, details) + "' target='_blank'>Open PDF</a>");
					//	SendMail(PDFName);
						SendeMail(PDFName);
					}
					catch (Exception e)
					{
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("ReportToPDF: " + e.Message);
						Console.ResetColor();
						Console.WriteLine("Press any key to continue...");
						Console.ReadKey();
					}
					
					//Delete *.rxzlog if not enabled within test suite settings
					Cleanup();
					
					//Update error value
					UpdateError();
				};
				
				registered = true;
			}
		}
		
		public static string ConvertReportToPDF (string PDFName, string xml, string details)
		{
			var zippedReportFileDirectory = CreateTempReportFileDirectory();
			var reportFileDirectory = TestReport.ReportEnvironment.ReportFileDirectory;
			var name = TestReport.ReportEnvironment.ReportName;
			
			var input = String.Format(@"{0}\{1}.rxzlog", zippedReportFileDirectory, name);
			var PDFReportFilePath = String.Format(@"{0}\{1}", reportFileDirectory, CheckExtension(PDFName));
			
			TestReport.SaveReport();
			Report.Zip(TestReport.ReportEnvironment, zippedReportFileDirectory, name);

			Ranorex.PDF.Creator.CreatePDF(input, PDFReportFilePath, xml, details);
			return PDFReportFilePath;
		}

		private static string CheckExtension(string PDFName)
		{
			var split = PDFName.Split('.');
			
			for(int i =0; i <split.Length; i++)
			{
				if(split[i].Contains("pdf") && i == split.Length -1 && split.Length > 1)
				{
					return PDFName;
				}
			}
			
			return PDFName + ".pdf";
		}
		
		private static string CreateTempReportFileDirectory()
		{
			//Create new temp directory for zipped report
			try 
			{
				zippedReportFileDirectoryInfo = System.IO.Directory.CreateDirectory(String.Format(@"{0}\temp", TestReport.ReportEnvironment.ReportFileDirectory));
				return zippedReportFileDirectoryInfo.FullName;					
			} 
			catch (Exception ex) 
			{
				throw new Exception("Failed to create temp folder: " + ex.Message);
			}	
		}
		
		private void Cleanup()
		{
			try 
			{
				zippedReportFileDirectoryInfo.Delete(true);	
			}
			catch (Exception ex) 
			{
				throw new Exception("Failed to recursively delete zipped report file directory: " + ex.Message);
			}
		}
		
		private string CreatePDFName()
		{
			//Report Status is not part of the ReportName at this stage of the test
			var name = TestReport.ReportEnvironment.ReportName;

			//Get status from TestSuite
			var testsuite = (TestSuite) TestSuite.Current;
			
			if( testsuite.ReportSettings.ReportFormatString.Contains("%X"))
			{
				name = name += "_" + GetTestSuiteStatus();
			}

			return name;
		}
		
		private void UpdateError()
		{
			var testsuite = (TestSuite) TestSuite.Current;
			
			if(GetTestSuiteStatus().Contains("Failed"))
			{
				Report.Failure("Rethrow Exception within PDF Module (Necessary for correct error value)");
			}
		}
		
		private string GetTestSuiteStatus()
		{
			string status = "";
			
			var rootChildren = ActivityStack.Instance.RootActivity.Children;
			
			if(rootChildren.Count > 1)
			{
				Console.WriteLine("Multiple TestSuiteActivites, status taken from first entry");
			}
			
			var testSuiteAct = rootChildren[0] as TestSuiteActivity;
			
			if(testSuiteAct != null)
			{
				status = testSuiteAct.Status.ToString();
			}
			
			return status;
		}
		public void ReadDataFile(string datafilepath, out bool EQA, out bool EDV, out bool IQA, out bool IDV)
		{
            string line = "";
            string sub1 = "";
       //     string sub2 = "";
            EQA = EDV = IQA = IDV = false;
        //    UpchainVersion = "";
            if (!File.Exists(datafilepath))
            {
                return;
            }
            StreamReader myReader = new StreamReader(datafilepath);


                while ((line = myReader.ReadLine()) != null)
                {

                    if (line.Contains("testcontainername="))
                    {
                        int indexofSub = line.IndexOf("=") + 1;
                        sub1 = line.Substring(indexofSub);

                        if (sub1.Contains("QA")&&(sub1.Contains("Elec")))
                        {
                            EQA = true;
                        }

                        if (sub1.Contains("DEV") && (sub1.Contains("Elec")))
                        {
                            EDV = true;
                        }

                        if (sub1.Contains("INFOR") && (sub1.Contains("DEV")))
                        {
                            IDV = true;
                        }
                        if (sub1.Contains("INFOR") && (sub1.Contains("QA")))
                        {
                            IQA = true;
                        }
                    }
    /*                if (line.Contains("UPCHAIN XLM Version:"))
                    {
                    	int indexofSub2 = line.IndexOf(":") + 2;
                    	sub2 = line.Substring(indexofSub2);
                    }
                    UpchainVersion = sub2;*/
                }
            myReader.Close();
		}
/*		public void SendMail(string reportPath)
        {
			
			bool EQA, EDV, IDV, IQA;
           string datapath = TestReport.ReportEnvironment.ReportDataFilePath;
			reportPath = TestReport.ReportEnvironment.ReportFileDirectory + "\\" + reportPath + ".pdf";
           string sResult = GetTestSuiteStatus();
           if (sResult == "Success")
           {
           	sResult = "Passed";
           }
           ReadDataFile(datapath, out EQA, out EDV, out IQA, out IDV);

            try 
            {
            	
            
            	Outlook.Application app = new Outlook.Application();
            	Outlook.MailItem mailItem = app.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            	if (EQA && IQA)
            	{
            		mailItem.Subject = "Electrical and INFOR Deploy Test in the PLM QA environment Result - " + sResult;
            	}
            	else if (EDV && IDV)
            	{
            		mailItem.Subject = "Electrical and INFOR Deploy Test in the PLM DEV environment Result - " + sResult;
            	}
            	else if (EQA && !IQA)
            	{
            		mailItem.Subject = "Electrical Deploy Test in the PLM QA environment Result - " + sResult;
            	}
            	else if (EDV && !IDV)
            	{
            		mailItem.Subject = "Electrical Deploy Test in the PLM DEV environment Result - " + sResult;
            	}
            	else if (IQA && !EQA)
            	{
            		mailItem.Subject = "INFOR Deploy Test in the PLM QA environment Result - " + sResult;
            	}
            	else if (IDV && !EDV)
            	{
            		mailItem.Subject = "INFOR Deploy Test in the PLM DEV environment Result - " + sResult;
            	}
            	else
            	{
            		mailItem.Subject = "PLM Electrical Deploy Test Result - " + sResult;
            	}
            	if (sResult == "Passed")
            	{
            		mailItem.To = "anpatel@atsautomation.com; abridgman@atsautomation.com; mcampbell@atsautomation.com; chenderson@atsautomation.com; kdekroon@atsautomation.com; ecaulfield@atsautomation.com; brhill@atsautomation.com; dpremuzic@upchainxlm.com";
            	}
            	else
            	{
            		mailItem.To = "anpatel@atsautomation.com;";
            	}
            	//mailItem.To = "anpatel@atsautomation.com;";
            	if (System.IO.File.Exists(reportPath))
           		{
            
           		 	mailItem.Attachments.Add(reportPath);
            		mailItem.Importance = Outlook.OlImportance.olImportanceHigh;
            		mailItem.Display(mailItem);
            		mailItem.HTMLBody = mailItem.Subject + ".  See Attached PDF for more info." + mailItem.HTMLBody;
            
            		((Outlook._MailItem)mailItem).Send();
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
        */
		public void SendeMail(string reportPath)
		{
			bool EQA, EDV, IDV, IQA;
			string UpchainVersion = varUpChain;
			Report.Log(ReportLevel.Info,"Ranorex Test performed on Upchain Version: " + UpchainVersion);
			string environment = "";
           string datapath = TestReport.ReportEnvironment.ReportDataFilePath;
			reportPath = TestReport.ReportEnvironment.ReportFileDirectory + "\\" + reportPath + ".pdf";
           string sResult = GetTestSuiteStatus();
           string subject, addressTo, addressFrom, body;
           addressFrom = "ranorexrt@atsautomation.com";
           if (sResult == "Success")
           {
           	sResult = "Passed";
           }
           ReadDataFile(datapath, out EQA, out EDV, out IQA, out IDV);
           try 
           {
           	if (EQA && IQA)
            	{
            		subject = "Electrical/INFOR Deploy test in QA for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	else if (EDV && IDV)
            	{
            		subject = "Electrical/INFOR Deploy test in DEV for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	else if (EQA && !IQA)
            	{
            		subject = "Electrical Deploy test in QA for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	else if (EDV && !IDV)
            	{
            		subject = "Electrical Deploy test in DEV for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	else if (IQA && !EQA)
            	{
            		subject = "INFOR Deploy test in QA for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	else if (IDV && !EDV)
            	{
            		subject = "INFOR Deploy test in DEV for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	else
            	{
            		subject = "Electrical Deploy Test for Upchain Version:" + UpchainVersion + " " + sResult;
            	}
            	if (sResult == "Passed")
            	{
            		if (SendEmailtoAll == "Yes")
            		{
            			addressTo = "anpatel@atsautomation.com; chenderson@atsautomation.com; ecaulfield@atsautomation.com; brhill@atsautomation.com; stauqeer@atsautomation.com; tchang@atsautomation.com";
            		}
            		else
            		{
            			addressTo = "anpatel@atsautomation.com";
            		}
            	}
            	else
            	{ 
            		addressTo = "anpatel@atsautomation.com";
            	}
            	
            	if (System.IO.File.Exists(reportPath))
           		{
            
            		
            		body = subject + ".  See Attached PDF for more info.";
            		MailHelper.SendMessage(body, addressTo, subject, "relay2.atsna.atsauto.net", addressFrom, "PLM Deploy Test", new [] { reportPath });
            		if (subject.Contains("DEV"))
            		    {
            		    	environment = "DEV";
            		    }
            		if (subject.Contains("QA"))
            		    {
            		    	environment = "QA";
            		    }
            		        PostPDFtoSharePoint(UpchainVersion, environment, reportPath);
            
            		
            	}
            
            	else
            	{
            		Report.Info("Could not find file: " + reportPath);
            	} 
           	
           } 
           catch (System.Net.Mail.SmtpFailedRecipientException eX1)
           {           	
           		Report.Error("SMTP Error, failed to send email to: " + eX1.FailedRecipient);
           }           
           catch (Exception ex)
           {
           		Report.Error("Mail Error: " + ex.ToString());	
           
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
            	string SPsite = @"https://portal/corit/corpapp/General%20Documentation/Forms/AllItems.aspx?RootFolder=%2Fcorit%2Fcorpapp%2FGeneral%20Documentation%2FPLM%2FDeploy%2FTesting%2F2017&FolderCTID=0x012000FAC402B95968F148A04148BC661E3DBB&View=%7BF9039D9C%2D5993%2D4748%2DB0BA%2DA505940BBC41%7D";
            	string date = month + "-" + day + "-" + year;
            	if (!Directory.Exists(SPdirectory))
            	{
            		int i = 0;
            		do
            		{
            			
            			Report.Log(ReportLevel.Error, "Cannot log into SharePoint, running SharepointLogOn Recording Module...");
            			PLMTest.SharepointLogOn.Start();
            			
            		/*	int ms = 10000;
            			Process.Start(SPsite);
            			Ranorex.Delay.Seconds(20);
            			Thread.Sleep(ms);
            			Process[] processNames1 = Process.GetProcessesByName("iexplore");
            			Report.Log(ReportLevel.Error,"SPdirectory not found, retrying...");
            			foreach (Process item in processNames1)
            			{
            				item.Kill();
            			}
            			Process.Start(SPdirectory);
            			Ranorex.Delay.Seconds(10); */
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
            		File.Copy(pdfpath, SPdirectory + "\\" + year + "\\" + dirName + "\\ElecDeployTest.pdf", true);
            		Report.Success("Sharepoint", "File saved to SharePoint successfully");
            	}
            	
			} 
			catch (Exception ex) 
			{
				Report.Error("Error occured: " + ex.ToString());	
			}
		}
		
	}
}
