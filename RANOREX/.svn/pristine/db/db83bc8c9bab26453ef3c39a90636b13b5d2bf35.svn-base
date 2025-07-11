/*
 * Created by Ranorex
 * User: mcampbell
 * Date: 6/29/2016
 * Time: 11:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;

namespace PLM_Mechanical_Deploy_Test
{
    class Program
    {
    	//public static IDictionary<string, string> Settings { get; private set; }        	
        [STAThread]
        public static int Main(string[] args)
        {
        	//Settings = ATS.CodeLibrary.Configuration.ConfigurationHelper.GetAllLocalSettingsDecrypted();  	
        	
        	// Set abort key to Pause key
	        Keyboard.AbortKey = System.Windows.Forms.Keys.Pause;
        	
        	int error = 0;
	               
        	try
        	{
        		
        		// start a new Pop up watcher
        		var repo = PLM_Mechanical_Deploy_TestRepository.Instance;
	            PopupWatcher myPopupWatcher = new PopupWatcher();  
	            myPopupWatcher.WatchAndClick(repo.SOLIDWORKSPopUp.ButtonOKInfo, repo.SOLIDWORKSPopUp.ButtonOKInfo);
	            myPopupWatcher.WatchAndClick(repo.OtherClientsActive.ButtonYesInfo, repo.OtherClientsActive.ButtonYesInfo);
	            myPopupWatcher.WatchAndClick(repo.OtherUserActive.ButtonYesInfo, repo.OtherUserActive.ButtonYesInfo);
	            myPopupWatcher.WatchAndClick(repo.Warning.ButtonOKInfo, repo.Warning.ButtonOKInfo);
	            myPopupWatcher.WatchAndClick(repo.PLMWorxAddinVersion.ButtonNoInfo, repo.PLMWorxAddinVersion.ButtonNoInfo);
	            myPopupWatcher.WatchAndClick(repo.WindowsSecurity.CloseInfo, repo.WindowsSecurity.CloseInfo);
	            myPopupWatcher.WatchAndClick(repo.SaveModifiedDocuments.cbDoNotSaveReadOnlyInfo, repo.SaveModifiedDocuments.DontSave1Info);
	            myPopupWatcher.WatchAndClick(repo.SaveModifiedDocuments.cbDontShowWhenSavingInfo, repo.SaveModifiedDocuments.ButtonSaveAllInfo);
	            myPopupWatcher.WatchAndClick(repo.LoginFailed.SelfInfo, repo.LoginFailed.ButtonOKInfo);
	            myPopupWatcher.WatchAndClick(repo.OpenForeignDWGFile.SelfInfo, repo.OpenForeignDWGFile.ContinueOpeningDWGFileInfo);
	            myPopupWatcher.WatchAndClick(repo.CheckInProblem.SelfInfo, repo.CheckInProblem.ButtonYesInfo);
	            myPopupWatcher.WatchAndClick(repo.SWPOPUPFORMSAVEALL.SelfInfo, repo.SWPOPUPFORMSAVEALL.DontSaveInfo);
	            //myPopupWatcher.WatchAndClick(repo.UpchainXLMAddInUpdater.CancelInfo, repo.UpchainXLMAddInUpdater.CancelInfo);
	            myPopupWatcher.Start();            
	            
				error = TestSuiteRunner.Run(typeof(Program), Environment.CommandLine);
        	}

        	catch(Exception e)
        	{
       		
        		Report.Error("Unexpected exception occurred: " + e.ToString());
                error = -1;
        	}
        	return error;
            // Uncomment the following 2 lines if you want to automate Windows apps
            // by starting the test executable directly
            //if (Util.IsRestartRequiredForWinAppAccess)
            //    return Util.RestartWithUiAccess();



        }
    }
}
