/*
 * Created by Ranorex
 * User: anpatel
 * Date: 5/25/2016
 * Time: 2:19 PM
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

namespace PLMTest
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

            Keyboard.AbortKey = System.Windows.Forms.Keys.Delete;
            int error = 0;

            try
            {
            	// start a new Pop up watcher
        		var repo = PLMTestRepository.Instance;
	            PopupWatcher myPopupWatcher = new PopupWatcher();  
	            
	            myPopupWatcher.WatchAndClick(repo.OtherClientsActive.ButtonYesInfo, repo.OtherClientsActive.ButtonYesInfo);
	            myPopupWatcher.Start(); 
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
