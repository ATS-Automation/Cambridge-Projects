pushd \\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build
JDE_Heartbeat.exe /rc:Heartbeat_All /zr /zrf:Heartbeat.rxzlog
ROBOCOPY "\\ca01a9001\pgmis\Deployment_DEV\RanorexToPDF_Executable" "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build" /E
ROBOCOPY "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\SendMail" "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build" /E
ReportToPDF.exe Heartbeat.rxzlog Heartbeat.pdf
SendeMail "See attached PDF report of Test Result" anpatel@atsautomation.com "JDE Heartbeat Result" ranorexrt@atsautomation.com "JDE Test Result" Heartbeat.pdf
popd
pause