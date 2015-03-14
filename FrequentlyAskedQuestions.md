### I found a bug or I have a feature request ###

Please check the issue list, and create an issue if it does not already exist.

### What are those checkboxes for beside my files? ###

Unchecked files are ignored during the build process.

### Why can't I checkmark my .h or .hpp files? ###

You can't compile those, or at least, shouldn't.

### How do I tell the project what language I am using ###

You don't have to, if a file is cpp, then it will be compiled with avr-g++, if a file is c, then avr-gcc is used, if any pde files are found, then the build process will use the same method that Arduino's editor builds the project.

Note that you can't "supplement" a C or C++ project with a pde file as the Arduino core already defines a main function.

### Please explain `-Wall` and `-gdwarf-2` and `-std=gnu99` ###

Those are the same default compiler options that AVRStudio used. It means all warnings are displayed, use dwarf-2 for debugging, and the C standard is GNU99.

### I don't want F\_CPU defined in the compilation options ###

Set clock speed to 0, and there won't be "DF\_CPU=" in the compilation options, allowing you to manually define the clock speed in the code. However some of the calculators built into the IDE still needs a clock speed specified to work right.

### What is the “Run Only Options” button for in the AVRDUDE command builder? ###

It'll run AVRDUDE but won't write the hex file to it, this is used to check that your programmer is working

### How do I send special characters through the serial port? ###

Use escape sequences, such as \r \n \t \v \b \a

If you want to send a single byte, use \xFF where FF is the hexadecimal representation of the byte you want to send.

### How do I debug or run a simulator? ###

Export as an aps file and open it with AVRStudio.

### I can't run the makefile I just exported ###

The generated makefile is intended to be used in your output directory, but the make command is executed on the project directory.

### Some USB devices do not appear in the device list ###

There are some devices that cannot be listed, this is a limitation of libusb. If Windows already loaded the device driver for a device (mice, keyboards, etc), then libusb may not be able to find it.