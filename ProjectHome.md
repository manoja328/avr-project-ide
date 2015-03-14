# AVR Project IDE #

### Important Notice ###

AVR Studio 5 Beta is out and it's super awesome, go try it out. The file formats used by AVR Studio 5 are different from AVR Studio 4.18 (and older), and the AVR Studio related features inside AVR Project IDE are only relevant to AVR Studio 4.18 and older.

### End of Important Notice ###

This development environment is designed to provide an easy to use and lightweight editor that allows users to quickly program AVR microcontrollers. This editor combines easy project editing, configuration, building, and flash memory burning all into one environment.

The environment utilizes the tools provided by [AVR Toolchain](http://code.google.com/p/avr-project-ide/wiki/SystemRequirements) to provide support for C and C++ code. Arduino code can also be built from within the environment as long as the Arduino core files are available.

The file organization, project creation, and project configuration, are modeled after AVR Studio with improvements. The editor component utilizes Scintilla. The subwindow management system uses DockPanel Suite to provide an environment similar to Visual Studio and AVR Studio.

The design is focused around familiarity, and compatibility with existing popular software. The design also aims to provide as much screen space as possible and minimize the amount of actions required to perform a task.

This software is written in C# from Visual Studio 2008, and will only work on Windows with [.NET Framework 3.5](http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6) . Actually building and burning a project requires an installation of [AVR Toolchain](http://code.google.com/p/avr-project-ide/wiki/SystemRequirements). Building an Arduino project requires the Arduino core files and any libraries you use, version 0022 of these files are included with the featured download.

Please check out the [Wiki](http://code.google.com/p/avr-project-ide/w/list) !!

Please also report all issues and suggestions under the issue tab on the top of this page.

This project is open source, just go to the source tab, it's all under trunk/source. It's not packaged since I don't want to worry about uploading several MB of data every time I do an update.

Frank's website is at http://frank.circleofcurrent.com/

## Distinguishing Features ##

  * Scintilla editor, with configurable syntax highlighting and indentation preferences
  * Docking subwindows and tabs, with workspace preference saved upon exit and reloaded at startup
  * Backs up open files with modifications in configurable time intervals
  * Supports the building of C, C++, and Arduino projects using AVR Toolchain
  * Quickly configure AVRDUDE and burn your project right from the editor
  * Customizable project templates, apply as much or as little settings as you want
  * Customize your help menu with bookmarks which you can edit through an XML file, even launch your own applications
  * Autocomplete that is about half as smart as Visual Studio
  * Hardware Explorer that displays information about hardware IO registers, interrupt vector spellings, pin information, and more
  * Provides a easy to use interface for AVRDUDE
  * Provides a powerful fuse calculator, and other useful calculation tools
  * USB device information and event notifications are displayed in a panel
  * There's a whole lot more other little details, so fool around, click on things, break things.
  * Code templates with macros available for quick generation of header files, copyright text, etc.

## Compatibility Features ##

  * Import and export .aps files used by AVR Studio 4.18 and older
  * Generates makefiles, or run your own makefile
  * Build process automatically detects Arduino code and builds that code in a way identical to how Arduino actually builds its code

## AVR Studio Related Improvements ##

  * Uses AVRDUDE so you can use non-official programmers from within the editor
  * Configuration manager is very familiar

## Arduino Related Improvements ##

  * Serial port terminal that supports escape sequences and displays non-printable characters
  * More project flexibility
  * Apply an Arduino template to quickly choose your Arduino flavor, the chip will be chosen, clock speed set accordingly, and AVRDUDE will be set to use the bootloader.

# Updater #

The data below is utilized by the updater, which checks the version below against the current version being used.

`BUILDID:"AVRProjectIDE_108"`