To create a external tool entry, add a XML entry into ext\_tools.xml like so:

```
<Tool text="Example Tool Name" cmd="notepad" dir="" args="%PROJNAME%.avrproj" />
```

"text" is what the menu item will display.

"cmd" is the executable path or name, do not leave "cmd" blank, do not put arguments in it either.

"dir" indicates working directory of the executable. If "dir" is blank, then the project directory will become the working directory.

"args" contains the arguments for the executable.

## Available Placeholders ##

These placeholder text will be replaced based on the properties of your project and your currently viewed file.

  * %PROJNAME% is the project file name
  * %PROJDIR% is the full path to the directory that the project file is in (without ending slash character)
  * %PROJOUTFOLDER% is the output directory for the current project, without leading or ending slash character
  * %FILENAMENOEXT% is the currently viewed file name without extension
  * %FILEEXT% is the extension of the currently viewed file
  * %FILEDIR% is the full path to the directory the currently view file resides in, without ending slash character