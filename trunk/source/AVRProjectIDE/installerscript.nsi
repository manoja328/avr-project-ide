!include "FileAssociation.nsh"
!include "MUI2.nsh"

!define MUI_ABORTWARNING

SetCompressor /SOLID /FINAL lzma

Name "AVR Project IDE"

RequestExecutionLevel highest

OutFile "AVRProjectIDE_Installer.exe"

InstallDir $PROGRAMFILES\AVRProjectIDE
InstallDirRegKey HKCU "Software\AVRProjectIDE" ""

;Page directory
;Page components
;Page instfiles
;UninstPage uninstConfirm
;UninstPage instfiles

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

Section ;"Main Editor"

	SetOutPath $INSTDIR
	
	FILE AVRProjectIDE.exe
	File mainicon.ico
	File SciLexer.dll
	File ScintillaNet.dll
	File WeifenLuo.WinFormsUI.Docking.dll
	
	WriteUninstaller $INSTDIR\uninstaller.exe
	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AVRProjectIDE" "DisplayName" "AVRProjectIDE"	
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AVRProjectIDE" "UninstallString" "$\"$INSTDIR\uninstaller.exe$\""	
	WriteRegStr HKCU "Software\AVRProjectIDE" "" $INSTDIR
	
SectionEnd

Section "Arduino 0017 Core" SecArdCore

	SetOutPath $INSTDIR\arduino\core
	
	File /r arduino\core\*.*

SectionEnd

Section "Arduino 0017 Libraries" SecArdLib

	SetOutPath $INSTDIR\arduino\libraries
	
	File /r arduino\libraries\*.*
	
SectionEnd

Section "Desktop Shortcut" SecDesktopShortcut

	SetOutPath $DESKTOP
	
	CreateShortCut $DESKTOP\AVRProjectIDE.lnk $INSTDIR\AVRProjectIDE.exe
	
SectionEnd

Section "Start Menu Entry" SecStartMenu

	SetOutPath $STARTMENU\AVRProjectIDE
	
	CreateShortCut $STARTMENU\AVRProjectIDE\AVRProjectIDE.lnk $INSTDIR\AVRProjectIDE.exe
	CreateShortCut $STARTMENU\AVRProjectIDE\Uninstall.lnk $INSTDIR\uninstaller.exe
	
SectionEnd

Section "Atmel Part Description XML" SecAtmelPartXML

	SetOutPath $INSTDIR\chip_xml
	
	File /r chip_xml\*.*

SectionEnd

Section "File Association" SecFileAssociate

	${registerExtension} "$INSTDIR\AVRProjectIDE.exe" ".avrproj" "AVRProjectIDE File"
	
SectionEnd

LangString DESC_ArdCore ${LANG_ENGLISH} "Arduino 0017 Core Files"
LangString DESC_ArdLib ${LANG_ENGLISH} "Arduino 0017 Library Files"
LangString DESC_DesktopShortcut ${LANG_ENGLISH} "A shortcut to AVR Project IDE on your desktop"
LangString DESC_StartMenu ${LANG_ENGLISH} "A shortcut to AVR Project IDE on in your start menu"
LangString DESC_FileAssociate ${LANG_ENGLISH} "Open *.avrproj files with AVR Project IDE"
LangString DESC_AtmelPartXML ${LANG_ENGLISH} "Files used by the Hardware Explorer feature that provides information about the AVR chip you are using"

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_DESCRIPTION_TEXT ${SecArdCore} $(DESC_ArdCore)
!insertmacro MUI_DESCRIPTION_TEXT ${SecArdLib} $(DESC_ArdLib)
!insertmacro MUI_DESCRIPTION_TEXT ${SecDesktopShortcut} $(DESC_DesktopShortcut)
!insertmacro MUI_DESCRIPTION_TEXT ${SecStartMenu} $(DESC_StartMenu)
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

	Delete $STARTMENU\AVRProjectIDE\AVRProjectIDE.lnk
	RMDir /r $STARTMENU\AVRProjectIDE
	Delete $DESKTOP\AVRProjectIDE.lnk
	
	${unregisterExtension} ".avrproj" "AVRProjectIDE File"
	
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\AVRProjectIDE"
	
SectionEnd