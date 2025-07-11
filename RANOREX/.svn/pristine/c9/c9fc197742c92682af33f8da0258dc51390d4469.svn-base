/*
 * Created by Ranorex
 * User: anpatel
 * Date: 9/18/2019
 * Time: 5:14 PM
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
using Ranorex.Core.Reporting;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace JDE_Heartbeat
{
    /// <summary>
    /// Description of DurationofPrevious.
    /// </summary>
    /// 
    
    [TestModule("DCA8E87E-C8B1-41B5-A8F8-29FDD9F7A639", ModuleType.UserCode, 1)]
    public class DurationofPrevious : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// 
        
        string _durationMainTest = "0";
        [TestVariable("79e7a72d-75cb-4dfb-a50b-a7932a405a6d")]
        public string durationMainTest
        {
        	get { return _durationMainTest; }
        	set { _durationMainTest = value; }
        }
        
        public DurationofPrevious()
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
            var result = ActivityStack.Current.Parent.Parent.ElapsedTime.TimeSpan.TotalSeconds - ActivityStack.Current.Parent.ElapsedTime.TimeSpan.TotalSeconds;
            Report.Log(ReportLevel.Info, "Elapsed Time: " + result.ToString("F2") + " seconds");
            durationMainTest = result.ToString("F2");
            
        }
    }
}
