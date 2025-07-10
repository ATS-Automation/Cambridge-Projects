VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "DDMData"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Dim objConnection As New ADODB.Connection
Dim rs As New ADODB.Recordset
Dim rs2 As New ADODB.Recordset

Public Function OpenConnection()
    objConnection.ConnectionString = "DRIVER={SQL Server};SERVER=hqddm;UID=sa;PWD=kamino;DATABASE=atsddm"
    
    On Error GoTo errConnection
    objConnection.Open
    Exit Function
      
errConnection:
    MsgBox "Connection Failed: " + Err.Description
    Exit Function
End Function

Public Function CloseConnection()
    objConnection.Close
    Set objConnection = Nothing
End Function

Public Function GetProjectAssemblyInfoByFileName(FileName As String) As Recordset

    Dim sSQL As String
    sSQL = "exec GetProjectByFileName @file_name = '" & FileName & "'"
    
    rs.Open sSQL, objConnection, adOpenForwardOnly
    
    Set GetProjectAssemblyInfoByFileName = rs
    
End Function

Public Function GetList(iJob As Long, iAssembly As Integer, bTopLevel As Boolean) As Recordset
    Dim List As Recordset
'    If iAssembly = 0 Then
'       Set List = GetJobInfo(ijob)
'    Else
    If bTopLevel Then
        Set List = GetAssemblyInfo(iJob, iAssembly)
    Else
        Set List = GetDetailInfo(iJob, iAssembly)
    End If
'    End If
                
   Set GetList = List
End Function


Public Function GetJobInfo(iJob As Long) As Recordset
                
    Dim sSQL As String
    sSQL = "exec GetProjectByJobNum @job=" & iJob
           
    rs.Open sSQL, objConnection, adOpenForwardOnly
        
    Set GetJobInfo = rs
    
End Function

Function GetAssemblyInfo(iJob As Long, iAssembly As Integer) As Recordset

    Dim sSQL As String
    sSQL = "exec GetAssemblyInfoByLegacy @job=" & iJob & ", @assembly=" & iAssembly
    
    rs.Open sSQL, objConnection, adOpenForwardOnly
    
    Set GetAssemblyInfo = rs
    
End Function

Function GetDetailInfo(iJob As Long, iAssembly As Integer) As Recordset
    
    Dim sSQL As String
    Dim detail_id As Variant
    
    sSQL = "exec GetDetailIdByLegacy  @job=" & iJob & ", @assembly=" & iAssembly
    detail_id = objConnection.Execute(sSQL).GetRows
    detail_id = detail_id(0, 0)
    
    sSQL = "exec rptMfrPartsList_ddmDataexport @detail_id=" & detail_id & "; exec rptPurPartsList_ddmDataexport @detail_id=" & detail_id
    rs.Open sSQL, objConnection, adOpenForwardOnly
    
    'sSQL = "exec rptPurPartsList @detail_id=" & detail_id
    'rs.Open sSQL, objConnection, adOpenForwardOnly
    
    Set GetDetailInfo = rs

End Function

Function GetAsmDesc(iJob As Long, iAssembly As Integer) As String

    Dim sSQL As String
    Dim ReturnArray As Variant

    sSQL = "select ppd.bin_description " & _
        "from EngProjectPublicDetails ppd (nolock) " & _
        "where ppd.project_legacy_number = " & iJob & " " & _
        "and ppd.asm_legacy_number = " & iAssembly & " " & _
        "and ppd.detail_legacy_number = 0"
        
    On Error Resume Next
    ReturnArray = objConnection.Execute(sSQL).GetRows()
    GetAsmDesc = ReturnArray(0, 0)
   
End Function

Function GetDetDesc(iJob As Long, iAssembly As Integer, iDetail As Integer) As String

    Dim sSQL As String
    Dim ReturnArray As Variant

    sSQL = "select pbdet.bin_description " & _
    "from projectheaders ph inner join projectdetails pbpro on pbpro.bin_index = ph.project_bin_index " & _
    "inner  join projecttree pt on pt.parent_detail_id = pbpro.detail_id " & _
    "inner join projectdetails pdasm on pdasm.detail_id = pt.child_detail_id " & _
    "inner join projecttree pt2 on pt2.parent_detail_id = pdasm.detail_id " & _
    "inner join projectdetails pddet on pddet.detail_id = pt2.child_detail_id " & _
    "inner join publicbins pbdet on pbdet.bin_index = pddet.bin_index " & _
    "Where ph.legacy_number = " & iJob & " " & _
    "and pdasm.legacy_number = " & iAssembly & " " & _
    "and pddet.legacy_number = " & iDetail
    
    On Error Resume Next
    ReturnArray = objConnection.Execute(sSQL).GetRows()
    GetDetDesc = ReturnArray(0, 0)

End Function

Function GetProjectAssemblyList(iJob As Long) As Recordset
    Dim sSQL As String

    sSQL = "select pdasm.legacy_number, pbasm.bin_name, dbo.f_GetBinDescription1(pbasm.bin_description) as desc1, dbo.f_GetBinDescription2(pbasm.bin_description) as desc2 " & _
        "From projectheaders ph " & _
        "inner join ProjectDetails pdpro on ph.project_bin_index = pdpro.bin_index " & _
        "inner join EngProjectTree pt on pt.parent_detail_id = pdpro.detail_id " & _
        "inner join EngProjectDetails pdasm on pdasm.detail_id = pt.child_detail_id " & _
        "inner join EngPublicBins pbasm on pbasm.bin_index = pdasm.bin_index " & _
        "Where pbasm.document_type = 1 " & _
        "and pdasm.legacy_number >= 0 " & _
        "and ph.legacy_number = " & iJob & " " & _
        "Order By pdasm.legacy_number"
  
    rs.Open sSQL, objConnection, adOpenForwardOnly
    
    Set GetProjectAssemblyList = rs
End Function







