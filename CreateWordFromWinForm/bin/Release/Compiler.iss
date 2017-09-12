; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!
#define MyAppName "JesServices - Invoice"
#define MyAppExeName "JesServicesInvoice.exe"

[Setup]
AppName=JesServicesInvoice
AppVersion=1.4
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
Source: "CreateWordFromWinForm.exe"; DestDir: "{app}"; DestName: {#MyAppExeName}; Flags: ignoreversion
Source: "Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion                     
Source: "Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "Spire.Doc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Spire.Doc.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "Spire.License.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Spire.License.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "Spire.Pdf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "icon.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "Template.docx"; DestDir: "{app}"; Flags: onlyifdoesntexist

[Icons]
Name: "{group}\JesServicesInvoice"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\icon.ico"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; IconFilename: "{app}\icon.ico"

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
Type: filesandordirs; Name: "{app}\app.publish"