So you want to use your Arduino hardware with AVRProjectIDE eh?

Open the editor settings window, make sure that the paths to the Arduino core and library folders are correct and exist.

## Getting Started ##

Create a new project with one of the Arduino templates, and the initial file as a pde file.

## Use Existing Code ##

You can also create a blank project (not using the wizard), to apply the Arduino template, open the configuration window's last tab.

The easiest thing to do is to just drag and drop all your .pde files into the file tree.

## Configuring AVRDUDE to use the Bootloader ##

The template should have done that for you, but you still need to pick the right serial port.

## Using Libraries ##

Oh come on, you mean you can't type out "`#include <LibraryName.h>`" ?