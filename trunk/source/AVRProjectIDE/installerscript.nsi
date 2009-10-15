!include "FileAssociation.nsh"

SetCompressor /SOLID /FINAL lzma

Name "AVR Project IDE"

OutFile "AVRProjectIDE_Installer.exe"

InstallDir $PROGRAMFILES\AVRProjectIDE

Page directory
Page components
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles


Section ;"Main Editor"

	SetOutPath $INSTDIR
	
	FILE AVRProjectIDE.exe
	File mainicon.ico
	File AVRProjectIDEUpdater.exe
	File SciLexer.dll
	File ScintillaNet.dll
	File WeifenLuo.WinFormsUI.Docking.dll
	
	WriteUninstaller $INSTDIR\uninstaller.exe
	
SectionEnd

Section "Arduino 0017 Core"

	SetOutPath $INSTDIR\arduino\core
	
	File /r arduino\core\*.*

SectionEnd

Section "Arduino 0017 Libraries"

	SetOutPath $INSTDIR\arduino\libraries
	
	File /r arduino\libraries\*.*
	
SectionEnd

Section "Desktop Shortcut"

	SetOutPath $DESKTOP
	
	CreateShortCut $DESKTOP\AVRProjectIDE.lnk $INSTDIR\AVRProjectIDE.exe
	
SectionEnd

Section "Start Menu Entry"

	SetOutPath $STARTMENU\AVRProjectIDE
	
	CreateShortCut $STARTMENU\AVRProjectIDE\AVRProjectIDE.lnk $INSTDIR\AVRProjectIDE.exe
	
SectionEnd

Section "Atmel Part Description XML"

	SetOutPath $INSTDIR\chip_xml
	
	File /r chip_xml\*.*

SectionEnd

Section "File Association"

	${registerExtension} "$INSTDIR\AVRProjectIDE.exe" ".avrproj" "AVRProjectIDE File"
	
SectionEnd

Section "Uninstall"

	Delete $INSTDIR\chip_xml\*.*
	RMDir $INSTDIR\chip_xml
	
	Delete $INSTDIR\arduino\*.*
	RMDir $INSTDIR\arduino
	
	Delete $INSTDIR\*.*
	RMDir $INSTDIR

	Delete $STARTMENU\AVRProjectIDE\AVRProjectIDE.lnk
	RMDir $STARTMENU\AVRProjectIDE
	Delete $DESKTOP\AVRProjectIDE.lnk
	
	${unregisterExtension} ".avrproj" "AVRProjectIDE File"
	
SectionEnd