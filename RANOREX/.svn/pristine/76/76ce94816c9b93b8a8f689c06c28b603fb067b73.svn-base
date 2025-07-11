/*
 * Created by Ranorex
 * User: anpatel
 * Date: 5/9/2017
 * Time: 9:57 AM
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
using System.Diagnostics;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace PLM_SW_Addin
{
    /// <summary>
    /// Ranorex User Code collection. A collection is used to publish User Code methods to the User Code library.
    /// </summary>
    [UserCodeCollection]
    public class UserCodeCollection
    {
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the User Code library
    	/// within a User Code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Img_SW_Addin(string pictureAutomationId, int timeOut)
    	{
           Form form = @"/form[@title~'SOLIDWORKS|SolidWorks']";
           string xpathPicture = @".//picture[@automationid='" + pictureAutomationId +"']";
           Stopwatch stopwatch = Stopwatch.StartNew();
           try
           {
               Ranorex.Picture element = form.FindSingle<Ranorex.Picture>(xpathPicture, Duration.FromMilliseconds(timeOut));
               element.Click();
               stopwatch.Stop();
               Report.Log(ReportLevel.Info, "Successfully found " + pictureAutomationId +" button, Time Elapsed: " + stopwatch.Elapsed);
           }
           catch(Exception ex)
           {
           		stopwatch.Stop();
           		Report.Log(ReportLevel.Failure, ex.Message + "  Failed to find " + pictureAutomationId + " Time Elapsed: " + stopwatch.Elapsed + " String xPathPicture: " + xpathPicture);
           }
    	}
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the User Code library
    	/// within a User Code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Img_SW_Addin_Image_Recognization(string imageName)
    	{
    		Stopwatch sw = Stopwatch.StartNew();
    		Form swForm = @"/form[@title~'SOLIDWORKS|SolidWorks']";
    		string path = @"..\..\" + imageName;
    		try 
    		{
    			var searchImage = Ranorex.Imaging.Load(path);
    			swForm.Click(new Location(searchImage, new Imaging.FindOptions(0.85)));
    			sw.Stop();
    			Report.Log(ReportLevel.Info, "Match for Image: " + imageName + " Found, Time Elapsed: " + sw.Elapsed);
    		}
    		catch (Exception ex)
    		{
    			sw.Stop();
    			Report.Log(ReportLevel.Failure, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    		}

    		
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the User Code library
    	/// within a User Code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Find_SW_Addin_Treeitem_Right_Click(string treeItemTextContains, int timeOut)
    	{
    		Form swForm = @"/form[@title~'SOLIDWORKS|SolidWorks']";
          	string xpathTreeitem = @".//tree[@automationid='wpf_treeView']//text[@text~'" + treeItemTextContains +"']";
          	Stopwatch stopwatch = Stopwatch.StartNew();
          	try
          	{
          		Ranorex.Text treeitem = swForm.FindSingle<Ranorex.Text>(xpathTreeitem, Duration.FromMilliseconds(timeOut));
          		treeitem.Click(WinForms.MouseButtons.Right);
          		stopwatch.Stop();
          		Report.Log(ReportLevel.Info, "Successfully found treeitem: " + treeItemTextContains + " Time Elapsed: " + stopwatch.Elapsed);
          	}
          	catch(Exception ex)
           	{
           		stopwatch.Stop();
           		Report.Log(ReportLevel.Failure, ex.Message + " Could not find " + treeItemTextContains +" Time Elapsed: " + stopwatch.Elapsed);
           	}
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the User Code library
    	/// within a User Code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_SW_Addin_ContextMenu(string textofContextMenuContains, int timeOut)
    	{
    		ContextMenu swContextMenu = @"/contextmenu[@processname='SLDWORKS']";
            string xpathMenuitem = @".//text[@text='" + textofContextMenuContains + "']";
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
            	Ranorex.Text menuItem = swContextMenu.FindSingle<Ranorex.Text>(xpathMenuitem, Duration.FromMilliseconds(timeOut));
            	menuItem.Click();
            	sw.Stop();
            	Report.Log(ReportLevel.Info, "Successfully found Context menuitem: " + textofContextMenuContains +" Time Elapsed: " + sw.Elapsed);
            }
           catch(Exception ex)
           {
           	sw.Stop();
           	Report.Log(ReportLevel.Failure, ex.Message + " Could not find menuitem: " + textofContextMenuContains +" Elapsed time: " + sw.Elapsed);
           }
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the User Code library
    	/// within a User Code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Find_Status_Bar_SW_Addin()
    	{
    	Form swForm = @"/form[@title~'SOLIDWORKS|SolidWorks']";
          string xpathStatusBarText = @".//statusbar[@accessiblename='statusStrip']//text[@text~'Success']";
          Stopwatch stopwatch = Stopwatch.StartNew();
          try
          {
          	Ranorex.Text statusBar = swForm.FindSingle<Ranorex.Text>(xpathStatusBarText, Duration.FromMilliseconds(120000));
          	statusBar.MoveTo();
          	stopwatch.Stop();
          	Report.Log(ReportLevel.Info, "Successfully found Staus Bar, Time Elapsed: " + stopwatch.Elapsed);
          }
          catch(Exception ex)
           {
           	stopwatch.Stop();
           	Report.Log(ReportLevel.Failure, ex.Message + " Failed to find Status Bar in Elapsed time: " + stopwatch.Elapsed);
           }
    	}
    }
}
