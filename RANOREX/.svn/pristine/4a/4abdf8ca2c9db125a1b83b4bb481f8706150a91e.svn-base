/*
 * Created by Ranorex
 * User: abridgman
 * Date: 10/7/2016
 * Time: 2:13 PM
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

namespace EnvironmentSwitcherConfiguration
{
    class Program
    {
    	
        [STAThread]
        public static int Main(string[] args)
        {
            // Uncomment the following 2 lines if you want to automate Windows apps
            // by starting the test executable directly
            //if (Util.IsRestartRequiredForWinAppAccess)
            //    return Util.RestartWithUiAccess();

            Keyboard.AbortKey = System.Windows.Forms.Keys.Pause;
            int error = 0;
            
            // Create PopupWatcher  
            var repo = EnvironmentSwitcherConfigurationRepository.Instance;
            PopupWatcher myPopupWatcher = new PopupWatcher();  
            myPopupWatcher.WatchAndClick(repo.SOLIDWORKSDialog.ButtonOKInfo, repo.SOLIDWORKSDialog.ButtonOKInfo);
            myPopupWatcher.WatchAndClick(repo.LoginFailed.ButtonOKInfo, repo.LoginFailed.ButtonOKInfo);
            myPopupWatcher.WatchAndClick(repo.OtherUserActive.ButtonNoInfo, repo.OtherUserActive.ButtonNoInfo);
            myPopupWatcher.WatchAndClick(repo.LoginFailed.ButtonOKInfo, repo.LoginFailed.ButtonOKInfo);
            myPopupWatcher.WatchAndClick(repo.PLMWorxAddIn.ButtonNoInfo, repo.PLMWorxAddIn.ButtonNoInfo);
            
            myPopupWatcher.Start();            
  
            try
            {
                error = TestSuiteRunner.Run(typeof(Program), Environment.CommandLine);
            }
            catch (Exception e)
            {
                Report.Error("Unexpected exception occurred: " + e.ToString());
                error = -1;
            }
            return error;
        }
    }
}
