## General ##

You can configure a lot of things through files stored in the application data folder that AVR Project IDE uses. To find this folder, run AVR Project IDE, and it should appear in the "Messages" subwindow. You can also open the configuration window, go to the last tab, and click on the button that says "Open Application Data Folder".

If any files become unusable, AVR Project IDE should be able to handle it, maybe even rewrite the default configurations back. If any files are missing, AVR Project IDE will recreate the default configuration.

Do not touch workspace.xml unless you understand how DockPanel Suite handles it.

For help with project templates, see TemplatesHelp

### Editor Customization ###

scintconfig.xml handles customization options for the editor. Please do not actually remove any styles, or change style names. Adding styles will not work. The colours available to use are [found here](http://msdn.microsoft.com/en-us/library/system.drawing.color_properties.aspx).

Available attributes are:
  * Font
  * Size (integer)
  * ForeColor
  * BackColor
  * Bold (true or false)
  * Italic (true or false)
  * Underline (true or false)

To customize the keyword lists, remember that only list 0 and 1 actually work. Also, you can have multiple XML tags for the same keyword list, and when they are read, they'll just be combined.

### The .ini File ###

Go ahead and take a look at settings.ini, but all of the settings inside are configurable through the editor.

### Custom Help Menu ###

The custom help menu is stored in helplinks.xml. The structure looks like this:

```
<?xml version="1.0" encoding="utf-8" ?>
<HelpLinks>
  <Link Text="AVR-Libc Reference" URL="http://www.nongnu.org/avr-libc/user-manual/">
    <Link Text="Library Reference" URL="http://www.nongnu.org/avr-libc/user-manual/modules.html" />
    <Link Text="Interrupts" URL="http://www.nongnu.org/avr-libc/user-manual/group__avr__interrupts.html" />
    <Link Text="FAQ" URL="http://www.nongnu.org/avr-libc/user-manual/FAQ.html" />
  </Link>
  <Link Text="Baud Rate Chart" URL="http://www.wormfood.net/avrbaudcalc.php" />
  <Link Text="Fuse Calculator" URL="http://www.engbedded.com/cgi-bin/fcx.cgi" />
  <Link Text="Timer Calculator" URL="http://frank.circleofcurrent.com/content.php?page_id=avrtimercalc" />
  <Link Text="Atmel's Site" URL="http://www.atmel.com/products/AVR/" />
</HelpLinks>
```

The "Text" attribute is what the menu item displays, and the "URL" attribute is the URL to the webpage you want the item to visit when clicked, the URL can also be a file path to an local file or exe. When the "Link" elements are nested, the children of the element become the drop-down from that menu item. If the "URL" attribute is missing, then the link will still appear but will not be clickable.

### Custom External Tools ###

See ExternalTools