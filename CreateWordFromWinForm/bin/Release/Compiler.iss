; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!
#define MyAppName "JesServices - Invoice"
#define MyAppExeName "JesServicesInvoice.exe"

[Setup]
AppName=JesServicesInvoice
AppVersion=1.1
DefaultDirName={pf}\JesServicesInvoice
DefaultGroupName=JesServicesInvoice
UninstallDisplayIcon={app}\JesServicesInvoice.exe
Compression=lzma2
SolidCompression=yes
OutputDir=.
OutputBaseFilename=JesServicesSetup
PrivilegesRequired=admin
DisableDirPage=no

[Files]
Source: "CreateWordFromWinForm.exe"; DestDir: "{app}"; DestName: {#MyAppExeName}
Source: "*"; Excludes: "app.publish, *.iss, JesServicesSetup.exe, CreateWordFromWinForm.exe, CreateWordFromWinForm.Application, CreateWordFromWinForm.exe.config,CreateWordFromWinForm.exe.manifest,CreateWordFromWinForm.pdb, CreateWordFromWinForm.vshost.application,CreateWordFromWinForm.vshost.exe, CreateWordFromWinForm.vshost.exe.config, CreateWordFromWinForm.vshost.config,CreateWordFromWinForm.vshost.exe.manifest"; DestDir: "{app}"

[Icons]
Name: "{group}\JesServicesInvoice"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\JesServices.ico"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; IconFilename: "{app}\JesServices.ico"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Registry]
Root: "HKCU"; Subkey: "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"; ValueType: String; ValueName: "{app}\{#MyAppExeName}"; ValueData: "RUNASADMIN"; Flags: uninsdeletekeyifempty uninsdeletevalue

[Dirs]
Name: "{app}\InvoiceFolder"
Name: "{app}\DocFolder"

[Run]
Filename: {app}\{#MyAppExeName}; Description: Launch Now; Flags: postinstall skipifsilent nowait runascurrentuser

[InstallDelete]
Type: files; Name: "{app}\CreateWordFromWinForm.exe.config"
Type: files; Name: "{app}\CreateWordFromWinForm.exe.manifest"
Type: files; Name: "{app}\CreateWordFromWinForm.pdb"
Type: files; Name: "{app}\CreateWordFromWinForm.vshost.exe.config"
Type: files; Name: "{app}\CreateWordFromWinForm.vshost.config"
Type: files; Name: "{app}\CreateWordFromWinForm.vshost.application"
Type: files; Name: "{app}\CreateWordFromWinForm.vshost.exe.manifest"
Type: files; Name: "{app}\CreateWordFromWinForm.exe.config"
Type: files; Name: "{app}\CreateWordFromWinForm.vshost.exe"
Type: files; Name: "{app}\JesServicesSetup.exe"
Type: files; Name: "{app}\CreateWordFromWinForm.exe"
Type: files; Name: "{app}\CreateWordFromWinForm.application"
Type: files; Name: "{app}\JesServices.ico"