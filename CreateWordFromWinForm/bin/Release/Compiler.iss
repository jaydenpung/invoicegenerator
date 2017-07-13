; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!
#define MyAppName "JesServices - Invoice"
#define MyAppExeName "JesServicesInvoice.exe"

[Setup]
AppName=JesServicesInvoice
AppVersion=1.0
DefaultDirName={pf}\JesServicesInvoice
DefaultGroupName=JesServicesInvoice
UninstallDisplayIcon={app}\JesServicesInvoice.exe
Compression=lzma2
SolidCompression=yes
OutputDir=.
OutputBaseFilename=JesServicesSetup
PrivilegesRequired=admin

[Files]
Source: "CreateWordFromWinForm.exe"; DestDir: "{app}"; DestName: {#MyAppExeName}
Source: "*"; Excludes: "app.publish,CreateWordFormWinForm.exe,*.iss"; DestDir: "{app}"

[Icons]
Name: "{group}\JesServicesInvoice"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\JesServices.ico"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; IconFilename: "{app}\JesServices.ico"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Registry]
Root: "HKCU"; Subkey: "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: String; ValueName: "{app}\{#MyAppExeName}"; ValueData: "RUNASADMIN"; Flags: uninsdeletekeyifempty uninsdeletevalue

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch application"; Flags: postinstall runascurrentuser 