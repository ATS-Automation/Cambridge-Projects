/*
 * Created by Ranorex
 * User: anpatel
 * Date: 6/20/2018
 * Time: 4:26 PM
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
using System.Linq;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Repository;
using Ranorex.Core.Data;
using ATS.CodeLibrary.DataUtilities.Cryptography;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Xml.Linq;
using ATS.CodeLibrary.Email;

namespace UpchainMechDeployTest
{
    /// <summary>
    /// Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public class UserCodeMethods
    {
        // You can use the "Insert New User Code Method" functionality from the context menu,
        // to add a new method with the attribute [UserCodeMethod].
              
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        /// 
        
        public static Stopwatch timer = new Stopwatch();
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void StartTimer(string message, string itemNum, string majRev, string minorRev, string version)
        {
        	timer.Start();
        	string item = itemNum + "-" + majRev + "-" + minorRev + "." + version;
        	Report.Info(message + item);
        }
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string StopTimer(string message, string itemNum, string majRev, string minorRev, string version, string category)
        {
        	timer.Stop();
        	string item = itemNum + "-" + majRev + "-" + minorRev + "." + version;
        	var minutes = timer.Elapsed.Minutes;
        	var hours = timer.Elapsed.Hours;
        	var seconds = timer.Elapsed.Seconds;
        	string totalTime;
        	if (hours>0)
        	{
        		totalTime = hours.ToString() + "h" + minutes.ToString() + "m" + seconds.ToString() + "s";
        		Report.Info(message + item + " is " + totalTime);
        	}
        	
        	else if (hours == 0 && minutes == 0)
        	{
        		totalTime = seconds.ToString() + "s";
        		Report.Info(message + item + " is " + totalTime);
        	}
        	else
        	{
        		totalTime = minutes.ToString() + "m" + seconds.ToString() + "s";
        		Report.Info(message + item + " is " + totalTime);
        	}
        	WriteFile(@"\\ca01a9001\pgmis\Deployment_DEV\Ranorex\UpChainDownloadPerformance\Performance.csv",item +","+ totalTime +","+ category + "," + System.DateTime.Now.ToString("MMM dd - h:mm tt") + "\n");
        	timer.Reset();
        	return totalTime;
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void WriteFile(string location, string text)
        {
        	
        	File.AppendAllText(location, text);
        }
        [UserCodeMethod]
        public static string GetRowDataFromColumnName(string dataSourceName, string columnName)
        {
           DataCache dataSheet = DataSources.Get(dataSourceName);
           int columnIndex = (dataSheet.Columns as IEnumerable<Ranorex.Core.Data.Column>).First(x => x.Name == columnName).Index;
           string result = dataSheet.Rows[0].Values[columnIndex].ToString();
           Report.Log(ReportLevel.Info, "Getting value of column " + columnName +" from datasource " + dataSourceName +" : " + result);
           return result;
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string DecryptPassword(string password)
        {
        	return CryptographyHelper.DecryptString(password);
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void RoboCopyHelper(string sourceFolder, string destinationFolder)
        {
        	try
            {
                var p = new Process()
                {                                        
                    StartInfo = new ProcessStartInfo
                    {          
                        FileName = "ROBOCOPY",        
                        Arguments = $"\"{sourceFolder}\" \"{destinationFolder}\" /XO /E /Z",
                    },
                };

                p.Start();
                
                
            }
            catch (Exception ex)
            {
            	Report.Error("Error Occurred: " + ex.ToString());
            }
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void DeleteRegKey(string path, string name)
        {
        	
           try 
           {
           		RegistryKey myKey = Registry.CurrentUser.OpenSubKey(path, true);
            	if (myKey == null)
                	return;

            	if(myKey.OpenSubKey(name) !=null)
            	{
            		myKey.DeleteSubKeyTree(name);
            		myKey.Close();
            	}
        	}
       		catch (Exception ex) 
       		{
       			Report.Error("Error Occurred: " + ex.ToString());
           	         	
           	} 
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void Find_WpfTreeItem_Click(string treeItemTextContains, string mouseAction, int timeOut)
        {
        	var repo = UpchainMechDeployTestRepository.Instance;
        	var wpfTree = repo.SOLIDWORKS.ElementHost1.StatusBarContainer.GripContainer.GridBody.GridBomMain.WpfTree;
        	string xpathTreeItemText = @".//text[@text~'" + treeItemTextContains +"']";
        	Stopwatch stopwatch = Stopwatch.StartNew();
	        	try 
	        	{
	        		Ranorex.Text treeitem = wpfTree.Self.FindSingle<Ranorex.Text>(xpathTreeItemText, Duration.FromMilliseconds(timeOut));
	        		switch (mouseAction.ToUpper())
	        		{
	        			case "LEFT":
	        				treeitem.Click();
	        				stopwatch.Stop();
	        				Report.Info("Left clicking treeitem with text: " + treeItemTextContains + "Time Elapsed: " + stopwatch.Elapsed);
	        				break;
	        			
	        			case "RIGHT":
	        				treeitem.Click(WinForms.MouseButtons.Right);
	        				stopwatch.Stop();
	        				Report.Info("Right Clicking treeitem with text: " + treeItemTextContains + "Time Elapsed: " + stopwatch.Elapsed);
	        				break;
	        				
						case "DOUBLE":
	        				treeitem.DoubleClick();
	        				stopwatch.Stop();
	        				Report.Info("Double Clicking treeitem with text: " + treeItemTextContains + "Time Elapsed: " + stopwatch.Elapsed);
	        				break;	
	        				
	        			case "MOVETO":
	        				treeitem.MoveTo();
	        				stopwatch.Stop();
	        				Report.Info("Moving to treeitem with text: " + treeItemTextContains + "Time Elapsed: " + stopwatch.Elapsed);
	        				break;	
	        				
	        			default:
	        				throw new Exception("You must enter a mouse action: Left, Right, Double, or MoveTo");
	        			
	        		}
	        		
	        	} 
	        	catch (Exception Ex) 
	        	{
	        		stopwatch.Stop();
	        		Report.Info("Error occurred: " + Ex.Message.ToString());
	        		
	        	}

        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void MapNetworkDriveCode(string driveLetter, string pathToMap)
        {
        	try 
        	{
        	
        		Report.Log(ReportLevel.Info, "UNC path successfully set to " + pathToMap);
        	    var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "mapDrive.bat");
        	  try 
        	  {
        	  	
        	   
        	    if (System.IO.File.Exists(path))
	                	System.IO.File.Delete(path);
	
	                using (var sw = new System.IO.StreamWriter(path))
	                {
	                	sw.WriteLine("Echo y ");
	                    sw.WriteLine("net use " + driveLetter +" /d");
	                    sw.WriteLine("net use " + driveLetter +" " + pathToMap + " /persistent:yes");
	                }
	
	                Delay.Seconds(2);
	                System.Diagnostics.Process.Start(path).WaitForExit();
	                Report.Log(ReportLevel.Info, "UNC path successfully mapped to " + driveLetter + " drive");
        	   }
        	    finally 
        	    {
        	    	if (System.IO.File.Exists(path))
	             	{
	            		Delay.Seconds(2);
        	    		System.IO.File.Delete(path);
	            		Report.Log(ReportLevel.Info, "Batch file deleted");
	             	}
        	  	
        	  	}  
        	}
        	catch (ArgumentException ex)
        	{
        	    Report.Log(ReportLevel.Failure, "Error setting UNC path, check path to map value: " + pathToMap + "\n" + ex.ToString());	
        	}
        	
        	catch (Exception x)
        	{
        		Report.Log(ReportLevel.Failure, "Error occured:\n" + x.ToString());
        	}
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        /// 
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void CheckCloudPluginVersionandInstall(string webClientVersion)
        {
        	string addInDirectory = UserCodeMethods.GetRowDataFromColumnName("Heartbeat", "UpChainCloudDirectory");
        //	string addInDirectory = @"C:\Program Files\Upchain\Upchain CAD Connector";
        	string addInPath = GetFilePathUsingRegex(addInDirectory, @"CADPlugin.dll");
 			string addinUninstallPath = GetFilePathUsingRegex(addInDirectory, @".*unins\d\d\d.exe");
 			string clientVersion = "";
 			var repo = UpchainMechDeployTestRepository.Instance;
 			var downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
 			var DownloadedPluginPath = downloadFolder + @"\UpchainCADConnector.exe";
 			Report.Info("Path of Download Folder Plugin: " + DownloadedPluginPath);
 			
 			try 
        	{
        		if (File.Exists(addInPath))
        		{
        			clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        			if(webClientVersion == clientVersion)
 					{
 						Report.Info("Plugin versions match! WebClientVersion = " + webClientVersion + " Installed Plugin Version = " + clientVersion);
 					}
        			else
        			{
        				Report.Info("Plugin versions DO NOT match! WebClientVersion = " + webClientVersion + " Installed Plugin Version = " + clientVersion);
        				if(File.Exists(DownloadedPluginPath))
        				{
        					Report.Info("Deleting UpchainCADConnector.exe from " + DownloadedPluginPath);
        					File.Delete(DownloadedPluginPath);
        				}
        				
        				
        				repo.Upchain.Plugins.Click();
        				repo.Upchain.SelfInfo.WaitForAttributeEqual(5000, "State", "complete");
        				Delay.Duration(10000, false);
        				Report.Info("Downloading new plugin...");
        				repo.Upchain.DownloadCADConnectorButton.Click();
        				int i=0;
        				do
        				{
        					Report.Info("Did not find UpchainCADConnector.exe in Download Folder, waiting 30 seconds... iteration[MAX 8 before timeout]: " + (i+1).ToString());
        					Delay.Seconds(30);
        					i++;
        				
        				}while (!File.Exists(DownloadedPluginPath)&&(i<8));
        				
        				Report.Info("Download complete, uninstalling previous plugin...");
	        			Host.Local.RunApplication(addinUninstallPath, "/verysilent",addInDirectory,false);
	        			i=0;
	        			do
	        			{
	        				Report.Info("Waiting 10 seconds for uninstall to complete");
	        				Delay.Seconds(10.0);
	        				i++;
	        			} while(File.Exists(addinUninstallPath)&&i<4);
	        			Report.Info("Plugin Uninstall completed, installing new Plugin...");	        			
	        			Host.Local.RunApplication(DownloadedPluginPath, "/verysilent", downloadFolder, false);
	        			i=0;
	        				do
	        				{
	        					Report.Info("Waiting 10 seconds for install to complete");
	        					Delay.Seconds(10.0);
	        					i++;
	        				} while(!File.Exists(addInPath)&&i<6);
	        			Report.Info("Plugin successfully installed!");
	        			Delay.Seconds(5.0);
	        			clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        				if(webClientVersion == clientVersion)
 						{
 						Report.Info("Plugin versions match");
 						}

        			}
        		}
        		else
        		{
        			Report.Info("Upchain Client Plugin not installed! Downloading New Plugin...");
        			if(File.Exists(DownloadedPluginPath))
        			{
        				Report.Info("Deleting UpchainCADConnector.exe from " + DownloadedPluginPath);
        				File.Delete(DownloadedPluginPath);
        			}
        				
        				
        			repo.Upchain.Plugins.Click();
        			repo.Upchain.SelfInfo.WaitForAttributeEqual(5000, "State", "complete");
        			Delay.Duration(10000, false);
        			Report.Info("Downloading new plugin...");
        			repo.Upchain.DownloadCADConnectorButton.Click();
        			int i=0;
        			do
        			{
        				Report.Info("Did not find UpchainCADConnector.exe in Download Folder, waiting 30 seconds... iteration[MAX 8 before timeout]: " + (i+1).ToString());
        				Delay.Seconds(30);
        				i++;
        				
        			}while (!File.Exists(DownloadedPluginPath)&&(i<8));
        			Report.Info("Download complete, Installing new plugin...");
        			Host.Local.RunApplication(DownloadedPluginPath, "/verysilent", downloadFolder, false);
        			i=0;
	        			do
	        			{
	        				Report.Info("Waiting 10 seconds for install to complete");
	        				Delay.Seconds(10.0);
	        				i++;
	        				addInPath = GetFilePathUsingRegex(addInDirectory, @"CADPlugin.dll");
	        			} while((addInPath=="0")&&i<6);
	        		Report.Info("Plugin successfully installed!");
	        		Delay.Seconds(5.0);
	        		clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        			if(webClientVersion == clientVersion)
 					{
 						Report.Info("Plugin versions match! WebClientVersion = " + webClientVersion + " Installed Plugin Version = " + clientVersion);
 					}
        		}	
        			
        		
 			}
 			catch (Exception ex) 
        	{
        		
        		Ranorex.Validate.Fail("Error occurred: " + ex.Message.ToString());
        		
        	
        	}
 			
 			
        }
        
        [UserCodeMethod]
        public static void CheckandInstallCorrectAddInVersion(string Env)
        {
        	string addInDirectory = UserCodeMethods.GetRowDataFromColumnName("DriveMappings", "UpChainAddInDirectory");
 			string addInPath = GetFilePathUsingRegex(addInDirectory, @".*AddIn.dll");
 			string addinUninstallPath = GetFilePathUsingRegex(addInDirectory, @".*unins\d\d\d.exe");
 			string upchainConfigFilePath = GetFilePathUsingRegex(addInDirectory, @".*AppEnvironment.config");
        	string clientVersion = "";
        	try 
        	{
        		if (File.Exists(addInPath))
        		{
        			clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        		}
        		string serverVersionPath = UserCodeMethods.GetRowDataFromColumnName("DriveMappings", Env);
        		var serverPaths = serverVersionPath.Split('\\');
        		var serverPathFolder = serverPaths.Take(serverPaths.Length - 1).Last();
        		DirectoryInfo serverDirectory = new DirectoryInfo(serverVersionPath);
        		FileInfo[] serverExe = serverDirectory.GetFiles("SW_AddIn*.exe");
        		string serverVersion = FileVersionInfo.GetVersionInfo(serverExe[0].FullName).FileVersion.Trim();
        		if (serverVersion == clientVersion)
        		{
        			Report.Info("Addin versions match, checking if server and client match...");
        			
            		XDocument doc = XDocument.Load(upchainConfigFilePath);
            		var elements = doc.Descendants("environmentConfig");
           		 	var element = elements.Descendants().First(x => x.Attribute("key").Value.ToString().Equals("WebServiceUrl"));
            		var clientConnectionToServer = element.Attribute("value").Value.ToString().TrimStart("http://".ToCharArray()).Substring(0, 9);
            		if (!(serverVersionPath.Contains(clientConnectionToServer)))
            		{
            			Report.Info("file versions match however the client and server do not match...");
            			Report.Info($"Server environment set to: {serverPathFolder} and Client set to: {clientConnectionToServer}");
            			if(File.Exists(addinUninstallPath))
	        			{
	        				int i=0;
	        				Report.Info("Uninstalling AddIn...");
	        				Host.Local.RunApplication(addinUninstallPath, "/verysilent",addInDirectory,false);
	        				do
	        				{
	        					Report.Info("Waiting 10 seconds for uninstall to complete");
	        					Delay.Seconds(10.0);
	        					i++;
	        				} while(File.Exists(addinUninstallPath)&&i<4);
	        				
	        			}
            			if (File.Exists(addInPath))
            			{
            				var fileInfo = new FileInfo(addInPath);            				
            				var dirPath = fileInfo.DirectoryName;
            				Report.Warn($"Uninstall did not complete properly, trying to delete files in folder {dirPath}");
            				MailHelper.SendMessage($"{dirPath} not deleted, waiting for 1 min", "anpatel@atsautomation.com", "Heartbeat Test Investigate", "relay2.atsna.atsauto.net", "ranorexrt@atsautomation.com", "PLM Deploy Test", new string [0]);
            				Delay.Seconds(60);
            				DeleteFolder(dirPath);

            			}
	        			if (!File.Exists(addInPath))
	        			{
	        				int i=0;
	        				Report.Info("Did not find installed add in, installing...");
	        				Host.Local.RunApplication(serverExe[0].FullName, "/verysilent", serverVersionPath, false);
	        				do
	        				{
	        					Report.Info("Waiting 10 seconds for install to complete");
	        					Delay.Seconds(10.0);
	        					i++;
	        					addInPath = GetFilePathUsingRegex(addInDirectory, @".*AddIn.dll");
	        				} while(!File.Exists(addInPath)&&i<4);
	        			}
            			
            		}
            		clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        			Report.Info("Server Version: " + serverVersion + " Client Version: " + clientVersion);
        		}
        		
        		if (!File.Exists(addInPath))
        		{
        			int i=0;
        			Report.Info("Did not find installed add in, installing...");
        			Host.Local.RunApplication(serverExe[0].FullName, "/verysilent", serverVersionPath, false);
        			do
        			{
        				Report.Info("Waiting 10 seconds for install to complete");
        				Delay.Seconds(10.0);
        				i++;
        				addInPath = GetFilePathUsingRegex(addInDirectory, @".*AddIn.dll");
        			} while(!File.Exists(addInPath)&&i<4);
        				
        			clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        			Report.Info("Server Version: " + serverVersion + " Client Version: " + clientVersion);
        		}
        		if (serverVersion != clientVersion)
        		{
        			
        			Report.Info("Addin versions do not match...");
        			if(File.Exists(addinUninstallPath))
        			{
        				int i=0;
        				Report.Info("Uninstalling old addin...");
        				Host.Local.RunApplication(addinUninstallPath, "/verysilent",addInDirectory,false);
        				do
        				{
        					Report.Info("Waiting 10 seconds for uninstall to complete");
        					Delay.Seconds(10.0);
        					i++;
        				} while(File.Exists(addinUninstallPath)&&i<4);
        				
        			}
        			if (!File.Exists(addInPath))
        			{
        				int i=0;
        				Report.Info("Did not find installed add in, installing...");
        				Host.Local.RunApplication(serverExe[0].FullName, "/verysilent", serverVersionPath, false);
        				do
        				{
        					Report.Info("Waiting 10 seconds for install to complete");
        					Delay.Seconds(10.0);
        					i++;
        					addInPath = GetFilePathUsingRegex(addInDirectory, @".*AddIn.dll");
        				} while(!File.Exists(addInPath)&&i<4);
        			}
        			
        			clientVersion = FileVersionInfo.GetVersionInfo(addInPath).FileVersion.Trim();
        			Report.Info("Server Version: " + serverVersion + " Client Version: " + clientVersion);
        			
        		}
        		
        	} 
        	catch (Exception ex) 
        	{
        		
        		Ranorex.Validate.Fail("Error occurred: " + ex.Message.ToString());
        	
        	}        	
        }
        
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void Addin_Img_Recog_Click(string imageName, Boolean continueOnFail)
        {
        	Stopwatch sw = Stopwatch.StartNew();
    		Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
    		string path = @"\\ca01a9001\pgmis\Deployment\Ranorex\" + imageName;
    		try 
    		{
    			Ranorex.Controls.ProgressForm.Hide();
    			var searchImage = Ranorex.Imaging.Load(path);
    			swForm.Click(new Location(searchImage, new Imaging.FindOptions(0.95)));
    			sw.Stop();
    			Report.Log(ReportLevel.Info, "Match for Image: " + imageName + " Found, Time Elapsed: " + sw.Elapsed);
    			Ranorex.Controls.ProgressForm.Show();
    		}
    		catch (Exception ex)
    		{
    			sw.Stop();
    			Ranorex.Controls.ProgressForm.Show();
    			if(continueOnFail)
    				Report.Log(ReportLevel.Info, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    			else
    				Report.Log(ReportLevel.Warn, ex.Message + "Match for Image: " + imageName + " Not Found, Time Elapsed: " + sw.Elapsed);
    		}
        	
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static void RightClick_Addin_Img_Recog(string imageName, Boolean continueOnFail)
        {
        	Stopwatch sw = Stopwatch.StartNew();
    		Form swForm = @"/form[@title~'^SolidWorks' or @title~'^SOLIDWORKS']";
    		string path = @"\\ca01a9001\pgmis\Deployment\Ranorex\" + imageName;
    		try 
    		{
    			var searchImage = Ranorex.Imaging.Load(path);
    			swForm.Click(WinForms.MouseButtons.Right, new Location(searchImage, new Imaging.FindOptions(0.95)));
    			sw.Stop();
    			Report.Log(ReportLevel.Info, "Match for Image: " + imageName + " Found, Time Elapsed: " + sw.Elapsed);
    			Delay.Seconds(1.0);
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
        
        /// <summary>
        ///Deletes a folder and subfolders with the given path.  Converts all Read-Only files within Folder to Normal so they get deleted as well.
        /// </summary>
        [UserCodeMethod]
        public static void DeleteFolder(string folderPath)
        {
        	try
        	{
	        	if(Directory.Exists(folderPath))
	        	{
	        		foreach (var file in  new DirectoryInfo(folderPath).GetFiles())
	        		{
	        		
	        				File.SetAttributes(file.FullName, FileAttributes.Normal);
	        		
	        		}
	        		var dirInfo = new DirectoryInfo(folderPath);
	        		dirInfo.Attributes = FileAttributes.Normal;
	        		Directory.Delete(folderPath, true);
	        		Report.Info($"Folder: {folderPath} successfully deleted");
	        	}
	        	else
	        	{
	        		Report.Info($"Folder: {folderPath} doesn't exist, can't delete");
	        	}
        	}
        	catch (Exception ex)
        	{
        		Ranorex.Validate.Fail(ex.StackTrace.ToString());
        	}
        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static string GetFilePathUsingRegex(string directory, string regExString)
        {
        	
        	try
        	{
	        	Regex regex = new Regex(regExString);
	            DirectoryInfo dirInfo = new DirectoryInfo(directory);
	            if (dirInfo.Exists)
	            {
	                FileInfo[] files = dirInfo.GetFiles();
	                foreach (var file in files)
	                {
	                    Match match = regex.Match(file.FullName);
	                    if (match.Success)
	                    {
	                    	Report.Info("File Path found successfully: " + file.FullName);
	                    	return file.FullName;
	                    }
	
	                }
	                throw new Exception("REGEX MATCH NOT FOUND");
	                    
	            }
	            else
	            {
	               throw new Exception("Directory Does not Exist");
	            }
        	}
        	catch (Exception ex)
        	{
        		Report.Warn("Error occured: " + ex.Message.ToString());
        		return "0";
        	}

        }
        
        /// <summary>
        /// This is a placeholder text. Please describe the purpose of the
        /// user code method here. The method is published to the user code library
        /// within a user code collection.
        /// </summary>
        [UserCodeMethod]
        public static List <Tuple<string, int, int>> GetFileNameandVersionFromTable(Ranorex.Table dataTable, string fileNameColumn, string colNameforcolIndex)
        {
            var cols = dataTable.Columns;
            var rows = dataTable.Rows;
            var tuplist = new List<Tuple<string, int, int>>();
            int colIndex = -1;
            int strtoNum = 0;
            bool colindexflagset = false;
            var dict = new Dictionary<string, int>();
            foreach (var col in cols) 
            {
            	if (col.Text == fileNameColumn)
            	{
            		foreach (var cell in col.Cells) 
            		{
            			if (cell.Text.Contains("AP"))
            			    {
            			    	dict.Add(cell.Text, cell.RowIndex);
            			    }
            		}
            	}
            	if (!colindexflagset)
            	{
	            	if (col.Text == colNameforcolIndex)
	            	{
	            		colIndex = col.Index;
	            		colindexflagset = true;
	            	}
            	}
            }
            
            foreach (var row in rows) 
            {
            	foreach (var kvp in dict) 
            	{
            		if (row.Index == kvp.Value)
            		{
						foreach (var cell in row.Cells) 
						{
							if (cell.ColumnIndex == colIndex)
							{						
								strtoNum = Int32.Parse(cell.Text);
								tuplist.Add(Tuple.Create(kvp.Key, kvp.Value, strtoNum));
							}
						}            			
            		}	
            		
            	}
            	
            	
            }

            return tuplist;
        }
    }
}
