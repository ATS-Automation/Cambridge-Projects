/*
 * Created by Ranorex
 * User: kdekroon
 * Date: 5/29/2017
 * Time: 5:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinForms = System.Windows.Forms;
using PLM_Mechanical_Deploy_Test;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;

namespace TrainingSetup
{
    class Program
    {
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

            // Create PopupWatcher  
        }
    }
}
