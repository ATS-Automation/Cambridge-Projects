/*
 * Created by Ranorex
 * User: anpatel
 * Date: 9/12/2016
 * Time: 10:23 AM
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
using System.IO;
using Microsoft.Office.Interop.Excel;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ATS_BTP_TESTING
{
	/// <summary>
	/// Description of SearchDataTable.
	/// </summary>
	public class SearchDataTable
	{
		public SearchDataTable()
		{
		}
		
		public static List<string> SearchDT(Ranorex.Table rTable, string searchedColName, string searchedCellText, string returnedColName)
		{
			List<string> returnedValues = new List<string>();
			var dtColumn = rTable.FindDescendants<Ranorex.Column>();
			bool searchColNameExist, searchCellTextExist, returnedColNameExist;
			returnedColNameExist = false;
			searchColNameExist = false;
			searchCellTextExist = false;
			foreach (var rCol in dtColumn) 
			{
				if (rCol.Text == searchedColName)
        		{
					searchColNameExist = true;
        			foreach (Ranorex.Cell rSearchedCell in rCol.Cells) 
        			{
        				if (rSearchedCell.Text == searchedCellText)
        				{
        					searchCellTextExist = true;
        					int rowIndex = rSearchedCell.RowIndex;
        					var dtCol2 = rTable.FindDescendants<Ranorex.Column>();
        					foreach (var rCol2 in dtCol2) 
        					{
        						if (rCol2.Text == returnedColName)
        						{
        							returnedColNameExist = true;
        							foreach (Ranorex.Cell rReturnedCell in rCol2.Cells) 
        							{
        								if (rReturnedCell.RowIndex == rowIndex)
        								{
        									returnedValues.Add(rReturnedCell.Text);
        								}
        							}
        						}
        					}
        				}
        			}
        		}
			}
			Ranorex.Validate.IsTrue(searchColNameExist, "Finding Searched Column Name \"" + searchedColName + "\" @ValidateRESULT@.");
			Ranorex.Validate.IsTrue(searchCellTextExist, "Finding Searched Cell Value \"" + searchedCellText + "\" @ValidateRESULT@.");
			Ranorex.Validate.IsTrue(returnedColNameExist, "Finding Returned Column Name \"" + returnedColName + "\" @ValidateRESULT@.");
			return returnedValues;
		}
	}
}
