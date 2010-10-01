!include "FileAssociation.nsh"
!include "MUI2.nsh"

!define MUI_ABORTWARNING

SetCompressor /SOLID /FINAL lzma

Name "AVR Project IDE"

!define MUI_ICON graphics\mainicon.ico

!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP graphics\installer_small_banner.bmp

!define MUI_WELCOMEFINISHPAGE_BITMAP graphics\installer_large_banner.bmp

RequestExecutionLevel highest

OutFile "AVRProjectIDE_Installer.VERSIONSTRINGGOESGERE.exe"

InstallDir $PROGRAMFILES\AVRProjectIDE
InstallDirRegKey HKCU "Software\AVRProjectIDE" ""

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "eula.rtf"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
Var StartMenuFolder
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

Section ;"Main Editor"

	SetOutPath $INSTDIR
	
	File AVRProjectIDE.exe
	File mainicon.ico
	File SciLexer.dll
	File ScintillaNet.dll
	File LibUsbDotNet.dll
	File WeifenLuo.WinFormsUI.Docking.dll
	
	WriteUninstaller $INSTDIR\uninstaller.exe
	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AVRProjectIDE" "DisplayName" "AVRProjectIDE"	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AVRProjectIDE" "UninstallString" "$\"$INSTDIR\uninstaller.exe$\""	
	WriteRegStr HKCU "Software\AVRProjectIDE" "" $INSTDIR

	!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
	
	SetOutPath $STARTMENU\$StartMenuFolder

	CreateShortCut $STARTMENU\$StartMenuFolder\AVRProjectIDE.lnk $INSTDIR\AVRProjectIDE.exe
	CreateShortCut $STARTMENU\$StartMenuFolder\Uninstall.lnk $INSTDIR\uninstaller.exe

	!insertmacro MUI_STARTMENU_WRITE_END
	
SectionEnd

Section "Arduino Core" SecArdCore

	SetOutPath $INSTDIR\arduino\core
	
	File /r arduino\core\*.*

SectionEnd

Section "Arduino Libraries" SecArdLib

	SetOutPath $INSTDIR\arduino\libraries
	
	File /r arduino\libraries\*.*
	
SectionEnd

Section "Desktop Shortcut" SecDesktopShortcut

	SetOutPath $DESKTOP
	
	CreateShortCut $DESKTOP\AVRProjectIDE.lnk $INSTDIR\AVRProjectIDE.exe
	
SectionEnd

Section "Atmel Part Description XML" SecAtmelPartXML

	SetOutPath $INSTDIR\chip_xml
	
	File /r chip_xml\*.*

SectionEnd

Section "File Association" SecFileAssociate

	${registerExtension} "$INSTDIR\AVRProjectIDE.exe" ".avrproj" "AVRProjectIDE File"
	
SectionEnd

LangString DESC_ArdCore ${LANG_ENGLISH} "Arduino Core Files"
LangString DESC_ArdLib ${LANG_ENGLISH} "Arduino Library Files"
LangString DESC_DesktopShortcut ${LANG_ENGLISH} "A shortcut to AVR Project IDE on your desktop"
LangString DESC_FileAssociate ${LANG_ENGLISH} "Open *.avrproj files with AVR Project IDE"
LangString DESC_AtmelPartXML ${LANG_ENGLISH} "Files used by the Hardware Explorer feature that provides information about the AVR chip you are using"

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_DESCRIPTION_TEXT ${SecArdCore} $(DESC_ArdCore)
!insertmacro MUI_DESCRIPTION_TEXT ${SecArdLib} $(DESC_ArdLib)
!insertmacro MUI_DESCRIPTION_TEXT ${SecDesktopShortcut} $(DESC_DesktopShortcut)
!insertmacro MUI_DESCRIPTION_TEXT ${SecAtmelPartXML} $(DESC_AtmelPartXML)
!insertmacro MUI_DESCRIPTION_TEXT ${SecFileAssociate} $(DESC_FileAssociate)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

Section "Uninstall"

	Delete $INSTDIR\chip_xml\*.*
	RMDir /r $INSTDIR\chip_xml
	
	Delete $INSTDIR\arduino\*.*
	RMDir /r $INSTDIR\arduino
	
	Delete $INSTDIR\*.*
	RMDir /r $INSTDIR

	Delete $STARTMENU\$StartMenuFolder\*.*
	RMDir /r $STARTMENU\$StartMenuFolder
	Delete $DESKTOP\AVRProjectIDE.lnk
	
	${unregisterExtension} ".avrproj" "AVRProjectIDE File"
	
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AVRProjectIDE"
	
SectionEnd