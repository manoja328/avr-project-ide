SET USRINPUT=
SET /p USRINPUT=Enter in the Alpha Number:

mkdir AVRProjectIDE_Alpha%USRINPUT%
mkdir AVRProjectIDE_Alpha%USRINPUT%\arduino
copy /Y /B AVRProjectIDE.exe AVRProjectIDE_Alpha%USRINPUT%\AVRProjectIDE.exe
copy /Y /B ScintillaNet.dll AVRProjectIDE_Alpha%USRINPUT%\ScintillaNet.dll
copy /Y /B SciLexer.dll AVRProjectIDE_Alpha%USRINPUT%\SciLexer.dll
copy /Y /B WeifenLuo.WinFormsUI.Docking.dll AVRProjectIDE_Alpha%USRINPUT%\WeifenLuo.WinFormsUI.Docking.dll
xcopy arduino\*.* AVRProjectIDE_Alpha%USRINPUT%\arduino\ /E /Y
pause