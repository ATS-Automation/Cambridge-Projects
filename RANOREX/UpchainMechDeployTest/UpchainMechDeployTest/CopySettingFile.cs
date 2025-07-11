/*
 * Created by Ranorex
 * User: anpatel
 * Date: 4/30/2019
 * Time: 2:33 PM
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
    /// Description of CopySettingFile.
    /// </summary>
    [TestModule("6A69BC5F-B82E-431B-B6B4-BFB1FEAF064E", ModuleType.UserCode, 1)]
    public class CopySettingFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CopySettingFile()
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
            string CopyFromPath = @"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainMechDeployTest\Settings\Settings.xml";
            string appData = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        	string CopyToPath = appData + @"\Upchain\Upchain For Solidworks\Settings.xml";
            File.Copy(CopyFromPath, CopyToPath, true);
            Report.Info("Upchain Settings File copied successfully to: " + CopyToPath);
        }
    }
}
