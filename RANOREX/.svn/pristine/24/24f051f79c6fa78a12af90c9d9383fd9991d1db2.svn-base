/*
 * Created by Ranorex
 * User: anpatel
 * Date: 3/28/2017
 * Time: 2:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace PLMTest
{
    /// <summary>
    /// Ranorex User Code collection. A collection is used to publish User Code methods to the User Code library.
    /// </summary>
    [UserCodeCollection]
    
    public class GetTiming
    {
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
        private static System.DateTime starttime;
        public static string ActionTime {get; private set;}
        
        [UserCodeMethod]
        public static void getStartTime()
        {
            Report.Log(ReportLevel.Info,"Start");  
            starttime = System.DateTime.Now;   
        }
        
        [UserCodeMethod]
        public static void getEndTime()
        {
        	System.DateTime end = System.DateTime.Now;
            TimeSpan duration = end - starttime;  
            ActionTime = duration.TotalSeconds.ToString();
            Report.Log(ReportLevel.Info, ActionTime); 
        }
        
    }
}
