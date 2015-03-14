The template file is templates.xml in the application data folder. See CustomizationHelp on how to find this file.

When a template is applied, only the settings that it needs to touch are modified, other project settings are NOT returned to default, they are LEFT ALONE.

Templates cannot include files

### Adding a Custom Project Template ###

The XML for a template looks like:

```
  <template name="Arduino ATMega8 8MHz">
    <Device>atmega8</Device>
    <ClockFreq>8000000</ClockFreq>
    <BurnPart>atmega8</BurnPart>
    <BurnProgrammer>stk500</BurnProgrammer>
    <BurnOptions>-F</BurnOptions>
    <BurnBaud>19200</BurnBaud>
  </template>
```

The each template is stored in its own "template" tag, with a "name" attribute.

The easiest way to write your own template is to save a project with the desired settings, and then open the .avrproj file with a text editor. Copy and paste in the tags that you want the template to set.

These tags are supported
```
<ClockFreq>
<Device>
<LinkerOpt>
<OtherOpt>
<OtherOptionsForC>
<OtherOptionsForCPP>
<OtherOptionsForS>
<Optimization>
<UseInitStack>
<InitStackAddr>
<PackStructs>
<ShortEnums>
<UnsignedBitfields>
<UnsignedChars>
<FunctionSections>
<DataSections>
<BurnPart>
<BurnProgrammer>
<BurnOptions>
<BurnBaud>
<BurnAutoReset>
<BurnFuseBox>
<IncludeDirList>
<LibraryDirList>
<LinkObjList>
<LinkLibList>
<MemorySegList>
<ArduinoCoreOverride>
```

A template can inherit from other templates by doing something like this:

```
<Inherit>Arduino ATMega8 8MHz</Inherit>
```

Starting from Alpha 41, file templates are implemented

to make a project template create a blank file

```
<CreateFile name="filename.ext" />
```

or if you want to include a file from the file\_templates folder (inside your app data directory)

```
<CreateFile name="filename.ext">
  <Template>copyright.txt</Template>
</CreateFile>
```

Please note that this will only work when the file does not already exist.

### Editing File Templates ###

File templates are stores inside the app data path, under the file\_templates folder

The following text (`*`.txt) files: defaultcode, defaultheader, initialmain, and initialpde, are used by the editor, and will be unpacked if missing. copyright will be unpacked if missing but is not required.

If those files are not missing, you are free to edit them, if they are missing, the editor will recreate them.

You may add your own files here if you want the project template to be able to find them

#### Macros ####

To insert formated date and time, do something like this:

```
%DATETIME:MM/dd/yyyy hh:mm:ss%
```

Reference http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx for the formatting string

Also available (self explanatory):

```
%FILENAME%
%FILENAMENOEXT%
%FILEEXT%
%PROJNAME%
```

To insert another file from within the file\_templates directory

```
%INCTEMPLATEFILE:filename.txt%
```


To insert another file from anywhere on your computer

```
%INCFILE:c:\something\filename.txt%
```

Note that included files may contain the above macros