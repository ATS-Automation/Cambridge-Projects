pushd \\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build
JDE_Heartbeat.exe /rc:Heartbeat_PROD_AP /zr /zrf:Heartbeat_PROD_AP.rxzlog
ROBOCOPY "\\ca01a9001\pgmis\Deployment_DEV\RanorexToPDF_Executable" "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build" /E
ROBOCOPY "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\SendMail" "\\ca01a9001\pgmis\Deployment_DEV\Ranorex\JDE_Heartbeat\Build" /E
ReportToPDF.exe Heartbeat_PROD_AP.rxzlog Heartbeat_PROD_AP.pdf
SendeMail "See attached PDF report of Test Result" anpatel@atsautomation.com "JDE Heartbeat Test Result - PROD ENV" anpatel@atsuatomation.com "JDE Test Result" Heartbeat_PROD_AP.pdf
popd