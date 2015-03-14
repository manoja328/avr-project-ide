## Update 99 ##

  * fixed a bug related to capitalization of chip names
  * did a check for new supported chips
  * there was a capitalized "P" in the new USnooBie entry inside the project template xml file, that was changed to a lower case "p"

## Update 98 ##

  * Advertising for [USnooBie](http://frank.circleofcurrent.com/usnoobie/), added to template
  * no mention of WinAVR since WinAVR is out of date, see SystemRequirements instead
  * fixed several issues, [issue 66](https://code.google.com/p/avr-project-ide/issues/detail?id=66)
  * slightly improved makefile generation
  * Arduino 0020 core and libraries
  * fixed libobjc.a being in the template
  * Chip descriptor XML files from AVR Studio 4.18 SP3

## Update 96 ##

  * [issue 63](https://code.google.com/p/avr-project-ide/issues/detail?id=63) and [issue 64](https://code.google.com/p/avr-project-ide/issues/detail?id=64)
  * better about box
  * double checked "show in taskbar"
  * better error report window

## Update 95 ##

  * Find dialog in Disassembly Viewer behaviour changed, see [issue 61](https://code.google.com/p/avr-project-ide/issues/detail?id=61)
  * Better USB info panel
  * [Issue 62](https://code.google.com/p/avr-project-ide/issues/detail?id=62)
  * added help for disassembly viewer

## Update 94 ##

  * USB Device Information and Notification Panel!!!!!
  * Added avrdude sections to generated makefiles

## Update 93 ##

  * [Issue 58](https://code.google.com/p/avr-project-ide/issues/detail?id=58) fixed

## Update 91 ##

  * [Issue 56](https://code.google.com/p/avr-project-ide/issues/detail?id=56) fixed

## Update 90 ##

  * Fix for [issue 54](https://code.google.com/p/avr-project-ide/issues/detail?id=54)

## Update 89 ##

  * Attempted fix for [issue 54](https://code.google.com/p/avr-project-ide/issues/detail?id=54)
  * Fix for [issue 55](https://code.google.com/p/avr-project-ide/issues/detail?id=55)

## Update 88 ##

  * Disassembly Viewer added, see [issue 53](https://code.google.com/p/avr-project-ide/issues/detail?id=53)
  * Google changed something that added an extra tag into the news feed, I changed the code in Welcome Window to fix that

## Update 87 ##

  * Only one find/replace dialog can be open at once
  * Might have fixed a few rare crashes and bugs
  * No more "run make" button, replaced with a button that opens the command line, since people might want to use "make clean" and other such options with make

## Update 86 ##

  * Small improvement to file adding wizard, auto-detects file extensions

## Update 85 ##

  * During cloning, a new folder called external\_files will be created to store files outside the project directory.
  * I stopped using the word alpha because it's probably never going to stop being in this constantly updated state

## Alpha 84 ##

  * Added project cloning

## Alpha 83 ##

  * Fixed [issue 49](https://code.google.com/p/avr-project-ide/issues/detail?id=49)

## Alpha 82 ##

  * Fixed [issue 48](https://code.google.com/p/avr-project-ide/issues/detail?id=48)

## Alpha 81 ##

  * Fixed a error in the templates regarding the rDuino LEDHead, the bootloader needs to use stk500v1, not stk500

## Alpha 80 ##

  * Noticed optimization option was not saved right
  * Icons for editor panels
  * APS export saving dialog now use a default folder and filename

## Alpha 79 ##

  * [Issue 47](https://code.google.com/p/avr-project-ide/issues/detail?id=47) prompted me to add one more try-catch inside Hardware Explorer
  * More details provided in Error Report Window

## Alpha 78 ##

  * Added hotkeys for dialog buttons
  * Fixed [issue 46](https://code.google.com/p/avr-project-ide/issues/detail?id=46) regarding generated makefiles

## Alpha 77 ##

  * Minor change to absolute file path, now saved in the XML as a comment instead of an element, also uses Path.GetFullPath method to avoid having double dots in the path

## Alpha 76 ##

  * Fixed compile problem with Arduino 0018 files

## Alpha 74 & 75 ##

  * Changed to Arduino 0018 files

## Alpha 73 ##

  * Noticed (hopefully fixed) a bug regarding serial port preference
  * Fixed stack-overflow in the error report window

## Alpha 72 ##

  * Fixed hardware explorer not showing vector name info
  * Fixed AVRDUDE tool not being able to do anything useful

## Alpha 71 ##

  * Cancelling renaming a file no longer causes exception (can't believe nobody told me about this one)
  * Fixed some icons
  * Added notification for updated files
  * Added new macro for file templates (USERNAME)
  * Icons for open files are different from closed files
  * Cosmetic fix in timer calculator

## Alpha 70 ##

  * update 69 broke arduino compiling, fixed in update alpha 70

## Alpha 69 ##

  * Added ability to override Arduino Core path for a particular project, allowing for certain projects to use alternative core files (maybe meant for Sanguino, Teensy, etc)
  * Fixed APS import so that the save directory defaults to the directory the APS resides in.
  * Fixed possible crash due to invalid data in clipboard.
  * Fixed crash when user refuses to find a missing file.
  * Default distributed configuration templates now include the rDuino LEDHead.
  * Fixed .a files not found properly
  * Fixed burner part and programmer not being remembered properly

## Alpha 68 ##

  * Some small structure changes involving Burner Panel
  * Added command line preview of AVRDUDE arguments
  * Fixed cosmetic bug with the new port override textbox
  * Port override will automatically turn lowercase com port names to uppercase

## Alpha 67 ##

  * Programmer port override now uses textbox instead of dropdown, allowing for more flexible port parameters such as usb:xx:xx and other weird things.
  * Programmer port will automatically be set to "avrdoper" (avrdude will like this) if the field is blank and avrdoper is chosen as the programmer device.
  * When programmer port is set to "COMx" and "//./" is not already prepended, "//./" will become prepended to allow ports greater than COM4 to be used
  * Atmel part description files updated to the versions that are supplied with AVR Studio 4.18

## Alpha 66 ##

  * fuse calculator now has undefined presets that do not exist within the XMLs

## Alpha 65 ##

  * installer improvements

## Alpha 64 ##

  * fixed an arduino compile bug
  * option to hide warnings

## Alpha 63 ##

  * Calculators for Timer, USART, SPI, TWI, and ADC
  * Every single WndProc has been overridden with one that launches the error report when an exception occurs, hopefully this makes the IDE more robust
  * Instead of using true.ToString(), a new function is used to accept "true", "yes", "y", and non-zero integers as booleans, this is to prevent any language issues

## Alpha 62 ##

  * Added number base conversion feature, select text or copy text to display its value in base 16, 10, and 2
  * Added file type specific compiler options

## Alpha 61 ##

  * Fixed Hardware Explorer not refreshing after changing chips

## Alpha 60 ##

  * Bugfix for file wizard when not using templates
  * Better assembly language support

## Alpha 59 ##

  * Added file creation wizard
  * File tree panel now sorts files alphabetically
  * Added ability to delete files

## Alpha 58 ##

  * Added Atmel XML as a data source for Hardware Explorer's interrupt information tab

## Alpha 57 ##

  * Fixed some stuff regarding recent file list
  * Added option to reopen last open file on startup
  * Added current line highlighting
  * Preserve cursor position between sessions

## Alpha 55 ##

  * Bookmarks!

## Alpha 53 ##

  * Option added to change message output behaviour

## Alpha 51 ##

  * Dialogs now have option to discard changes
  * Fixed bug regarding fuse reading

## Alpha 48 ##

  * Goto Dialog accessible through the edit menu, or CTRL-Z, not yet through right click, will add upon request or boredom
  * Splash screen
  * New icon

## Alpha 47 ##

  * Tiny potential annoyance in AVRDUDE Tool fixed
  * Changed behaviour for the build output, much more intelligent now.

## Alpha 46 ##

  * AVRDUDE Tool: write any file, dump file, verify against file, interactive mode, fuse tool
  * AVRDUDE options moved to a panel so that it can appear in many winforms
  * May have found and fixed a bug regarding port overrides
  * Build output now outputted "upside-down"

## Alpha 45 ##

  * Fuse Calculator / Tool
  * More settings available, such as whitespace and line numbering display modes

## Alpha 44 ##

  * Added option to disable update checking
  * Redid some tab ordering
  * Fixed wrong file template being used when creating from wizard
  * Minor changes to Hardware Explorer's chip info display

## Alpha 43 ##

  * Fixed bug regarding infinitly including files in template

## Alpha 41 ##

  * File templates implemented, with macros, see wiki for details
  * No more updater needed as now you just download a installer file

## Alpha 40 ##

  * Bootloader information displayed in Hardware Explorer
  * Fixed bug regarding renaming files
  * Added ability to compile single files
  * Fixed issue regarding packages not being displayed in Hardware Explorer
  * Added button to launch AVRDUDE in interactive mode
  * Updater searches the main project webpage instead of the BuildID wiki page
  * Installer requests highest permission level instead of user

## Alpha 39 ##

  * Updated Editor Settings window, everything is configurable now
  * Zoom level is preserved

## Alpha 37 ##

  * Fixed critical Find Next bug
  * Improved installer
  * Fixed backup not working

## Alpha 36 ##

  * No changes really, new NSIS installer used

## Alpha 35 ##

  * Wizard can merge initial file with existing file
  * Autocomplete can be disabled
  * New editor settings window
  * New projects use default settings from last project
  * Welcome window now launches after IDE, like how AVR Studio does it
  * Tells you that you need WinAVR if you don't have it
  * Fix for autocomplete undo behaviour
  * Fixed wrong file extension from wizard
  * Fixed opening .avrproj files from explorer
  * Welcome window can be disabled
  * Config window autolaunches for unconfigured projects

## Alpha 33 ##

  * Installer
  * Atmel part descriptor files used to implement Hardware Explorer
  * Working autocomplete
  * FuseBox for burning fuses

## Alpha 22 ##

  * news feed live!

## Alpha 21 ##

  * more fixes for build process, fixed bug that overwrote .hex file making it useless
  * added autoreset and updated templates (delete your old template to get the new one)
  * updater updates the IDE, and the IDE updates the updater, updater also unpacks DLLs for you
  * window size now stored upon closing editor

## Alpha 19 ##

  * More fixes for Arduino build process
  * Automatic updater included with alpha 18
  * fixed [issue 8](https://code.google.com/p/avr-project-ide/issues/detail?id=8), progress on 6
  * Added global try-catch with error window.

## Alpha 10 ##

  * Fixed Arduino build process

## Alpha 8 ##

  * Experimental DLL unloading, does not work
  * Error Reporting Window
  * Improved autogenerated project name in wizard
  * Initial favorite folder changed to My Documents/Projects

## Alpha 7 ##

  * Added F7 and F8 hotkeys for build and burn
  * Wizard remembers last template and initial file type
  * fixed crash regarding arduino\_temp\_main.cxx not written due to temp folder not created