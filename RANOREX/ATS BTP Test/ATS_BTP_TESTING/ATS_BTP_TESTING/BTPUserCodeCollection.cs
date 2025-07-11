/*
 * Created by Ranorex
 * User: anpatel
 * Date: 9/25/2019
 * Time: 2:00 PM
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
using System.Diagnostics;
using SolidWorks.Interop.swdocumentmgr;

namespace ATS_BTP_TESTING
{
    /// <summary>
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public class BTPUserCodeCollection
    {
    	// You can use the to add a new method with the attribute [UserCodeMethod].
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static bool FileCompare(string path1, string path2)
    	{
    		
        	byte[] file1 = File.ReadAllBytes(path1);
            byte[] file2 = File.ReadAllBytes(path2);
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    	
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void FindandReplaceForTextFiles(string filePath1, string strToFind, string strToReplace)
    	{
    		string text = File.ReadAllText(filePath1);
			string temp = text.Replace(strToFind, strToReplace);
			File.WriteAllText(filePath1, temp);
    		
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void AddModelToAssembly(string assemblyFilepath, string modelFilepath)
    	{
    		var swApp = new SolidWorks.Interop.sldworks.SldWorks();
            int warnings = 0;
            int errors = 0;
            var swModel = swApp.OpenDoc6(assemblyFilepath, (int)SolidWorks.Interop.swconst.swDocumentTypes_e.swDocASSEMBLY, (int)SolidWorks.Interop.swconst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref warnings, ref errors);
            var swAssy = swModel as SolidWorks.Interop.sldworks.AssemblyDoc;
            var newModel = swApp.OpenDoc6(modelFilepath, 1, 32, "", errors, warnings);
            swApp.ActivateDoc3(swModel.GetPathName(), true, 0, ref errors);
            swAssy.AddComponent5(modelFilepath, 0, "", false, "", 0, 0, 0);
            swModel.Save3((int)SolidWorks.Interop.swconst.swSaveAsOptions_e.swSaveAsOptions_Silent, ref errors, ref warnings);
            swApp.CloseDoc(modelFilepath);
            swApp.CloseDoc(swModel.GetPathName());
            swApp.ExitApp();
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static List<string> GetComponentReferences(string FilePath)
    	{
    		var dmClassFact = new SwDMClassFactory();
            var License = "ATSAutomation:swdocmgr_general-11785-02051-00064-50177-08535-34307-00007-44472-11853-13403-27494-36412-27671-31947-32772-02512-41037-02045-09497-31897-48182-23332-20740-01357-53717-46525-53637-48549-00441-25144-23148-51912-23238-24676-28770-0,swdocmgr_previews-11785-02051-00064-50177-08535-34307-00007-27168-42221-04371-40958-62832-23117-17425-14343-23433-33257-46580-34876-16136-27935-24018-20740-01357-53717-46525-53637-48549-00441-25144-23148-51912-23238-24676-28770-0,swdocmgr_geometry-11785-02051-00064-50177-08535-34307-00007-09912-36022-35058-49754-19875-11761-57615-47111-29192-53206-21678-40988-26460-13330-23265-20740-01357-53717-46525-53637-48549-00441-25144-23148-51912-23238-24676-28770-7";
            var ReferenceGatherer = dmClassFact.GetApplication(License);
            SwDmDocumentType DocType = GetFileType(FilePath);
            SwDmDocumentOpenError dummy;
            var From = (ISwDMDocument14)ReferenceGatherer.GetDocument(FilePath, DocType, true, out dummy);
            SwDMSearchOption search = ReferenceGatherer.GetSearchOptionObject();
            object BrokenRefs; object Flags; object Timestamps; 
            var missingRefs = new List<string>();
            var myrefs = From.GetAllExternalReferences4(search, out BrokenRefs, out Flags, out Timestamps);
            if (!(myrefs is string[])) { return missingRefs; }

            var ReferenceCatalog = myrefs as string[];
            foreach (string Component in ReferenceCatalog)
            {

                if (!File.Exists(Component))
                {
                    missingRefs.Add(Component);
                }
            }


            return missingRefs;
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static SwDmDocumentType GetFileType(string FilePath)
    	{
    		switch (Path.GetExtension(FilePath.ToUpper()))
            {
                case ".SLDPRT":
                    return SwDmDocumentType.swDmDocumentPart;
                case ".SLDASM":
                    return SwDmDocumentType.swDmDocumentAssembly;
                case ".SLDDRW":
                    return SwDmDocumentType.swDmDocumentDrawing;
                default:
                    return SwDmDocumentType.swDmDocumentUnknown;
            }
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void KillSW()
    	{
    		foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("SLDWORKS"))
                KillProcess(process);

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("sldexitapp"))
                KillProcess(process);

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("SLDWORKS"))
                KillProcess(process);

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("sldexitapp"))
                KillProcess(process);

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("sldworks_fs"))
                KillProcess(process);
    	}
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void KillProcess(Process process)
    	{
    		try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
            	Report.Info("Error Occurred killing process: " + process.ProcessName.ToString() + " Error Message: " + ex.Message);
            }
    	}
       
    }
}
