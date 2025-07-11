/*
 * Created by Ranorex
 * User: anpatel
 * Date: 04/26/24
 * Time: 2:25 PM
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
using RestSharp;
using Newtonsoft.Json.Linq;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using System.Diagnostics;
using ATS.CodeLibrary.Email;

namespace OauthDanceSharepoint
{
    /// <summary>
    /// Creates a Ranorex user code collection. A collection is used to publish user code methods to the user code library.
    /// </summary>
    [UserCodeCollection]
    public class UserCodeCollectionSPProject
    {
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void Ranorex_Oauth_Started()
    	{
    			var	addressTo = "anpatel@atsautomation.com; mmahida@atsautomation.com; brhill@atsautomation.com; smotilal@atsautomation.com; rtadepalli@atsautomation.com";
            //	var	addressTo = "anpatel@atsautomation.com; smotilal@atsautomation.com";
            	var subject = "Ranorex Oauth Dance Automation Started";
            	var addressFrom = "ranorexrt@atsautomation.com";
    			MailHelper.SendMessage("Successfully Started Ranorex Automation, please wait for completion email", addressTo, subject, "relay2.atsna.atsauto.net", addressFrom, "Ranorex Automation");
    		
    	}
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void RestartRemoteComputer(string computerName, string userName, string passWord)
    	{
    		Report.Info("Attempting to Restart Computer: " + computerName);
    		string pathToScript = @"\\ca01a9001\pgmis\Mulesoft_PowerShell\RestartMachine.ps1";
    		string scriptArguments = $"-ExecutionPolicy Bypass -File \"{pathToScript}\" -computerName \"{computerName}\" -userName \"{userName}\" -passWord \"{passWord}\"";
//    		try
//            {
    			using (Process p = new Process())
    			{
    				ProcessStartInfo startInfo = new ProcessStartInfo();
            		startInfo.FileName = "powershell.exe";
            		startInfo.Arguments = scriptArguments;
       		    	startInfo.UseShellExecute = false; // Set to false to redirect output
      		    	startInfo.RedirectStandardOutput = true; // Redirect standard output if needed
      		    	startInfo.RedirectStandardError = true;
      		   		p.StartInfo = startInfo;
      		   		p.Start();
      		   		string stdOutput = p.StandardOutput.ReadToEnd();
      		   		string outputError = p.StandardError.ReadToEnd();
      		   		
      		   		
      		   		if (!stdOutput.IsEmpty())
      		   		{
      		   			Report.Info("Output of powershell script: " + stdOutput);
      		   		}
      		   		else
      		   		{
      		   			Report.Info("Error Stream: " + outputError);
      		   		}
	           		//Process.Start(startInfo);
	           		p.WaitForExit();
    			}
    			Report.Info("Waiting 30 seconds...");
    			Delay.Seconds(30.0);
    			var	addressTo = "anpatel@atsautomation.com; mmahida@atsautomation.com; brhill@atsautomation.com";
            //	var	addressTo = "anpatel@atsautomation.com";
            	var subject = "Restart command has been sent to: " + computerName;
            	var addressFrom = "ranorexrt@atsautomation.com";
    			MailHelper.SendMessage("Please attempt to login and run Active MQ services", addressTo, subject, "relay2.atsna.atsauto.net", addressFrom, "Ranorex Automation");
    		
             
//            }
//            catch (Exception ex)
//            {
//                Report.Info("Error Occurred: " + ex.Message.ToString());
//            }
    	}
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static string GetOktaToken()
    	{
    		var client = new RestClient("https://atscorp.okta.com");
            var request = new RestRequest(@"/oauth2/default/v1/token", Method.Post);

            // Add your client id, client secret, and grant type
            request.AddParameter("client_id", "0oa80c580t0xeGEHD697");
            request.AddParameter("client_secret", "lctQ3NbxcmWwgHWs7N12kZQfF3X21AdiuPmnGHZaVzSc0s-xQmpuiTBnl2aWMdDG");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "mulescope");

            // Execute the request
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Parse the response content as dynamic object
                var content = JObject.Parse(response.Content);

                // Extract the access token
                string accessToken = content["access_token"].ToString();

                // Store the access token
                Report.Info("Access Token: " + accessToken);
               	return accessToken;
            }
            else
            {
                Report.Info("Exception: " + response.ErrorException + "Error Message: " + response.ErrorMessage);
                return "Error!";
            }
    	}
    	
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void UpdateRuntime(string envID)
    	{
    		var token = GetOktaToken();
    		if (token == "Error!")
    		{
    			Report.Info("Error on getting token, unable to run UpdateRuntime API Call!");
    		}
    		else
    		{
	    	//	var clientURL = @"http://ats-cloudhub-api-dev.ca-c1.cloudhub.io";
	    	//	if (envID.ToLower() == "prod")
	    	//	{
	    		var	clientURL = @"https://cloudhub-org-api-jymxwx.5spzrh.can-c1.cloudhub.io";
	    	//	}
	    		var client = new RestClient(clientURL);
	    		var request = new RestRequest(@"/api/bulk/applications/updateruntime", Method.Get);
	    		request.AddHeader("Authorization", "Bearer " + token);
	    	//	if (envID.ToLower() == "prod")
	    	//	{
	    			request.AddQueryParameter("env", envID.ToLower());
	    	//	}
	    	/*	else
	    		{
	    			request.AddQueryParameter("envID", envID.ToLower());
	    		}*/
	    		Report.Info("Calling API URL: " + clientURL + @"/api/bulk/applications/updateruntime");
	    		var response = client.Execute(request);
	    		
	    		if (response.IsSuccessful)
	            {
	                // Parse the response content as dynamic object
	                var content = JObject.Parse(response.Content);
	
	                if(content["result"].ToString() == "ok")
	                {
	                	Report.Info("Successfully sent the command to update runtime!");
	                }
	
	            }
	            else
	            {
	                Report.Info("Exception: " + response.ErrorException + "Error Message: " + response.ErrorMessage);
	            }
    		}
    		
    	}
    	
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void GetMuleAppStatus(string envID, string appName, int timeoutMinutes)
    	{
    		var token = GetOktaToken();
    		if (token == "Error!")
    		{
    			Report.Info("Error on getting token, unable to run GetMuleAppStatus API Call!");
    		}
    		else
    		{
	    	//	var clientURL = @"http://ats-cloudhub-api-dev.ca-c1.cloudhub.io";
	    	//	if (envID.ToLower() == "prod")
	    	//	{
	    			var clientURL = @"https://cloudhub-org-api-jymxwx.5spzrh.can-c1.cloudhub.io";
	    	//	}
	    		Report.Info("Calling API URL: " + clientURL + @"/api/applications/" + appName);
	    		var client = new RestClient(clientURL);
	    		var request = new RestRequest(@"/api/applications/" + appName, Method.Get);
	    		request.AddHeader("Authorization", "Bearer " + token);
//	    		if (envID.ToLower() == "prod")
//	    		{
//	    			request.AddQueryParameter("env", envID.ToLower());
//	    		}
//	    		else
//	    		{
//	    			request.AddQueryParameter("envID", envID.ToLower());
//	    		}
	    		request.AddQueryParameter("env", envID.ToLower());
	    		
	    		var response = client.Execute(request);
	    		
	    		if (response.IsSuccessful)
	            {
	                // Parse the response content as dynamic object
	                JObject content = JObject.Parse(response.Content);
					int i = 0;
					int TO = 1 * timeoutMinutes;
	                do
	                {	
	                
	                	if(content.ContainsKey("deploymentUpdateStatus"))
	                	{
	                		Report.Info("Appname: " + appName + " is still restarting, trying again in 1 minute...");
	                		Delay.Seconds(60.0);
	                		i++;
	                	}
	                	response = client.Execute(request);
	                	content = JObject.Parse(response.Content);
	                	   
	                }while(content.ContainsKey("deploymentUpdateStatus") && i<TO);
	                
	                if (i>= TO)
	                {
	                	Report.Error("App has exceeded the timeout loop of " + TO + " minutes, the app may still be restarting, please check mule logs for status and perform oauth dance if necessary");
	                }
	                
	                Report.Info("Appname: " + appName + " is done restarting!");
	
	            }
	            else
	            {
	                Report.Info("Exception: " + response.ErrorException + "Error Message: " + response.ErrorMessage);
	            }
    		}
    	}
    	
    	
    	/// <summary>
    	/// This is a placeholder text. Please describe the purpose of the
    	/// user code method here. The method is published to the user code library
    	/// within a user code collection.
    	/// </summary>
    	[UserCodeMethod]
    	public static void RestartMuleSoftAPI(string envID, string status, string csvAppNames)
    	{
    		var token = GetOktaToken();
    		if (token == "Error!")
    		{
    			Report.Info("Error on getting token, unable to run RestartMuleSoftAPI API Call!");
    		}
    		else
    		{
	    	//	var clientURL = @"http://ats-cloudhub-api-dev.ca-c1.cloudhub.io";
	    	//	if (envID.ToLower() == "prod")
	    	//	{
	    			var clientURL = @"https://cloudhub-org-api-jymxwx.5spzrh.can-c1.cloudhub.io";
	    	//	}
	    		Report.Info("Calling API URL: " + clientURL + @"/api/bulk/applications/updatestatus");
	    		var client = new RestClient(clientURL);
	    		var request = new RestRequest(@"/api/bulk/applications/updatestatus", Method.Post);
	    		request.AddHeader("Authorization", "Bearer " + token);
	    		//request.AddQueryParameter("env", envID.ToLower());
	    	//	if (envID.ToLower() == "prod")
	    	//	{
	    			request.AddQueryParameter("env", envID.ToLower());
	    	//	}
	    	/*	else
	    		{
	    			request.AddQueryParameter("envID", envID.ToLower());
	    		}*/
	    		request.AddQueryParameter("status", status.ToLower());
	    		request.AddParameter("text/plain", @csvAppNames, ParameterType.RequestBody);
	    		
	    		var response = client.Execute(request);
	    		
	    		if (response.IsSuccessful)
	            {
	                // Parse the response content as dynamic object
	                JObject content = JObject.Parse(response.Content);
	                if (content.ContainsKey("Status"))
	                {
	                	Report.Info(content.ToString());
	                	Report.Info("Waiting 60 seconds...");
	                	Delay.Seconds(60.0);
	                }
	
	            }
	            else
	            {
	              	Report.Info("Exception: " + response.ErrorException + "Error Message: " + response.ErrorMessage);
	            }
    		}
    	}
    
    }
}
