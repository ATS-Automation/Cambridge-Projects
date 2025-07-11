/*
 * Created by Ranorex
 * User: anpatel
 * Date: 4/17/2018
 * Time: 3:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace SolidWorksTaskPaneTools
{
    /// <summary>
    /// Ranorex User Code collection. A collection is used to publish User Code methods to the User Code library.
    /// </summary>
    [UserCodeCollection]
    public class SolidworksUserCodeCollection
    {
    	[UserCodeMethod]
    	public static void Click_Img_SW_Addin_Image_Recognization(string imageName, Boolean continueOnFail)
    	{
    		Stopwatch sw = Stopwatch.StartNew();
    		Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
    		string path = @"\\ca01a9001\pgmis\Deployment\Ranorex\" + imageName;
    		try 
    		{
    			var searchImage = Ranorex.Imaging.Load(path);
    			swForm.Click(new Location(searchImage, new Imaging.FindOptions(0.97)));
    			sw.Stop();
    			Report.Log(ReportLevel.Info, "Match for Image: " + imageName + " Found, Mouse Left Click " + imageName + " Time Elapsed: " + sw.Elapsed);
    		}
    		catch (Exception ex)
    		{
    			sw.Stop();
    			if(continueOnFail)
    				Report.Log(ReportLevel.Info, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    			else
    				Report.Log(ReportLevel.Warn, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    		}
    	}
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
    }
}
