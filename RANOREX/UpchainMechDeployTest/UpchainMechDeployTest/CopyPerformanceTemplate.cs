/*
 * Created by Ranorex
 * User: anpatel
 * Date: 3/2/2021
 * Time: 5:29 PM
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
using System.IO;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace UpchainMechDeployTest
{
    /// <summary>
    /// Description of CopyPerformanceTemplate.
    /// </summary>
    [TestModule("E8CA611E-F8F8-469A-BFEC-C25F3E3366DF", ModuleType.UserCode, 1)]
    public class CopyPerformanceTemplate : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CopyPerformanceTemplate()
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
            if (File.Exists(@"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainDownloadPerformance\Performance.csv"))
            {
            	Report.Info("Deleting Previous Performance Report");
            	File.Delete(@"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainDownloadPerformance\Performance.csv");
            }
           
            Report.Info("Copying Performance Report Template...");
            File.Copy(@"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainDownloadPerformance\Template\Template.csv",@"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainDownloadPerformance\Performance.csv");
            
        }
    }
}
