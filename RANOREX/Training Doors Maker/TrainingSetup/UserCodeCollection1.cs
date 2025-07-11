/*
 * Created by Ranorex
 * User: kdekroon
 * Date: 5/29/2017
 * Time: 5:41 PM
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

namespace TrainingSetup
{
    /// <summary>
    /// Ranorex User Code collection. A collection is used to publish User Code methods to the User Code library.
    /// </summary>
    [UserCodeCollection]
    public class UserCodeCollection
    {
		/// <summary>
    	/// This usercode method uses an xpath to click a picture object in SolidWorks - intended for the PLMWorx addin.
    	/// Must pass in the object's automation ID.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Img_SW_Addin(string pictureAutomationId, int timeOut)
    	{
           Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
           string xpathPicture = @".//picture[@automationid='" + pictureAutomationId +"']";
           Stopwatch stopwatch = Stopwatch.StartNew();
           try
           {
               Ranorex.Picture element = swForm.FindSingle<Ranorex.Picture>(xpathPicture, Duration.FromMilliseconds(timeOut));
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
    	/// This usercode method uses an xpath to click a text object in SolidWorks - intended for the PLMWorx addin.
    	/// Must pass in the object's automation ID.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Txt_SW_Addin(string textAutomationId, int timeOut)
    	{
           Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
           string xpathText = @".//text[@automationid~'" + textAutomationId +"']";
           Stopwatch stopwatch = Stopwatch.StartNew();
           try
           {
               Ranorex.Text element = swForm.FindSingle<Ranorex.Text>(xpathText, Duration.FromMilliseconds(timeOut));
               element.Click();
               stopwatch.Stop();
               Report.Log(ReportLevel.Info, "Successfully found " + textAutomationId +" button, Time Elapsed: " + stopwatch.Elapsed);
           }
           catch(Exception ex)
           {
           		stopwatch.Stop();
           		Report.Log(ReportLevel.Failure, ex.Message + "  Failed to find " + textAutomationId + " Time Elapsed: " + stopwatch.Elapsed + " String xPathText: " + xpathText);
           }
    	}
    	
    	/// <summary>
    	/// This usercode method uses an xpath to click a text object in SolidWorks - intended for the PLMWorx addin.
    	/// Must pass in the object's Caption (for objects that don't have an AutomationID).
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Txt_Sw_Addin_Caption(string textCaption, int timeOut)
    	{
           Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
           string xpathText = @".//text[@caption~'" + textCaption +"']";
           Stopwatch stopwatch = Stopwatch.StartNew();
           try
           {
               Ranorex.Text element = swForm.FindSingle<Ranorex.Text>(xpathText, Duration.FromMilliseconds(timeOut));
               element.Click();
               stopwatch.Stop();
               Report.Log(ReportLevel.Info, "Successfully found " + textCaption +" button, Time Elapsed: " + stopwatch.Elapsed);
           }
           catch(Exception ex)
           {
           		stopwatch.Stop();
           		Report.Log(ReportLevel.Failure, ex.Message + "  Failed to find " + textCaption + " Time Elapsed: " + stopwatch.Elapsed + " String xPathText: " + xpathText);
           }		
    	}
    	
    	/// <summary>
    	/// This usercode method uses an xpath to click an expand button object in SolidWorks in the PLMWorx addin.
    	/// Must pass in the caption of the text beside the expand button.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Expand_SW_Addin(string textCaption, int timeOut)
    	{
    	   Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
           //string xpathText = @".//text[@caption='" + textCaption +"']/../../checkbox[@automationid='Expander']";
           string xpathText = @".//text[@caption='" + textCaption +"']/../../../parent::treeitem";
           Stopwatch stopwatch = Stopwatch.StartNew();
           try
           {
           	Ranorex.TreeItem element = swForm.FindSingle<Ranorex.TreeItem>(xpathText, Duration.FromMilliseconds(timeOut));
           	   
               //Ranorex.CheckBox element = swForm.FindSingle<Ranorex.CheckBox>(xpathText, Duration.FromMilliseconds(timeOut));
               //element.Click();
               element.Expand();
               stopwatch.Stop();
               Report.Log(ReportLevel.Info, "Successfully explanded " + textCaption +" button, Time Elapsed: " + stopwatch.Elapsed);
               
           }
           catch(Exception ex)
           {
           		stopwatch.Stop();
           		Report.Log(ReportLevel.Failure, ex.Message + "  Failed to expand " + textCaption + " button, Time Elapsed: " + stopwatch.Elapsed + " String xPathText: " + xpathText);
           }
    	}
		    	
    	
    	/// <summary>
    	/// This usercode method uses an xpath to click a button object in SolidWorks - intended for the PLMWorx addin.
    	/// Must pass in the object's automation ID.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Click_Btn_SW_Addin(string btnAutomationId, int timeOut)
    	{
    	   Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
           string xpathBtn = @".//button[@automationid~'" + btnAutomationId +"']";
           Stopwatch stopwatch = Stopwatch.StartNew();
           try
           {
               Ranorex.Button element = swForm.FindSingle<Ranorex.Button>(xpathBtn, Duration.FromMilliseconds(timeOut));
               element.Click();
               stopwatch.Stop();
               Report.Log(ReportLevel.Info, "Successfully found " + btnAutomationId +" button, Time Elapsed: " + stopwatch.Elapsed);
           }
           catch(Exception ex)
           {
           		stopwatch.Stop();
           		Report.Log(ReportLevel.Failure, ex.Message + "  Failed to find " + btnAutomationId + " Time Elapsed: " + stopwatch.Elapsed + " String xPathBtn: " + xpathBtn);
           }
    	}
    	
    	/// <summary>
    	/// This usercode method uses a bitmap image to find an object through image recognition in SolidWorks
    	/// </summary>
    	/// 
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
    			Report.Log(ReportLevel.Info, "Match for Image: " + imageName + " Found, Time Elapsed: " + sw.Elapsed);
    		}
    		catch (Exception ex)
    		{
    			sw.Stop();
    			if(continueOnFail)
    				Report.Log(ReportLevel.Info, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    			else
    				Report.Log(ReportLevel.Failure, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    		}
    	}
    	
    	/// <summary>
    	/// This usercode method finds a tree item in the PLMWorx add-in and right clicks on it
    	/// </summary>
    	[UserCodeMethod]
    	public static void Find_SW_Addin_Treeitem_Right_Click(string treeItemTextContains, int timeOut)
    	{
    		Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
          	string xpathTreeitem = @".//text[@text~'" + treeItemTextContains +"']";
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
    	/// This usercode method clicks on a right click menu option in the PLMWorx SolidWorks add-in
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
    	/// This usercode method finds the status bar on the PLMWorx SolidWorks add-in and waits for the text to contain "Success"
    	/// </summary>
    	[UserCodeMethod]
    	public static void Find_Status_Bar_SW_Addin()
    	{
    		Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
        	string xpathStatusBarText = @".//statusbar[@accessiblename='statusStrip']//text[@text~'Success']";
        	Stopwatch stopwatch = Stopwatch.StartNew();
        	try
            {
          		Ranorex.Text statusBar = swForm.FindSingle<Ranorex.Text>(xpathStatusBarText, Duration.FromMilliseconds(90000));
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
