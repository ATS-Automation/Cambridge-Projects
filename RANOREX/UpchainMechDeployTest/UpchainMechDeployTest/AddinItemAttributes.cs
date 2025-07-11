/*
 * Created by Ranorex
 * User: anpatel
 * Date: 1/11/2019
 * Time: 11:04 AM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using Ranorex;

namespace UpchainMechDeployTest
{
	/// <summary>
	/// Description of AddinItemAttributes.
	/// </summary>
	public class AddinItemAttributes
	{
		public string shapeValue;
		public string materialValue;
		public string finishValue;
		public string heatTreatValue;
		
		public AddinItemAttributes()
		{
		}
		public void printAttributeValues()
		{
			Report.Info(string.Format("Item has Shape: {0}, Material: {1}, Finish: {2}, and Heat Treat: {3}",shapeValue,materialValue,finishValue,heatTreatValue));
		}
	}
}
