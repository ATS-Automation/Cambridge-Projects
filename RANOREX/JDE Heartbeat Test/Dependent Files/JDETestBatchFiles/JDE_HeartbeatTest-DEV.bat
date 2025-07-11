pushd \\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build
JDE_Heartbeat.exe /rc:Heartbeat_Dev /zr /zrf:Heartbeat_Dev.rxzlog
ROBOCOPY "\\ca01a9001\pgmis\Deployment_DEV\RanorexToPDF_Executable" "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build" /E
ROBOCOPY "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\SendMail" "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build" /E
ReportToPDF.exe Heartbeat_Dev.rxzlog Heartbeat_Dev.pdf
SendeMail "See attached PDF report of Test Result" anpatel@atsautomation.com "JDE Heartbeat Result - DEV Env" anpatel@atsuatomation.com "JDE Test Result" Heartbeat_Dev.pdf
popd
pause