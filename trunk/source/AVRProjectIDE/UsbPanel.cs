using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;
using LibUsbDotNet;
using LibUsbDotNet.Descriptors;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using LibUsbDotNet.LudnMonoLibUsb;
using LibUsbDotNet.DeviceNotify;

namespace AVRProjectIDE
{
    public partial class UsbPanel : DockContent
    {
        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In USB Panel");
            }
        }

        private IDeviceNotifier devNotifier;

        delegate void AppendNotifyDelegate(DeviceNotifyEventArgs e);

        public UsbPanel()
        {
            InitializeComponent();

            this.Icon = Icon.FromHandle(GraphicsResx.usbpanelicon.GetHicon());

            try
            {
                UsbDevice.UsbErrorEvent += OnUsbError;

                devNotifier = DeviceNotifier.OpenDeviceNotifier();
                devNotifier.OnDeviceNotify += onDevNotify;

                FillDeviceInfo();
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error Initializing USB Panel");
            }
        }

        private void onDevNotify(object sender, DeviceNotifyEventArgs e)
        {
            Invoke(new AppendNotifyDelegate(AppendNotifyText), new object[] { e });
        }

        private void AppendNotifyText(DeviceNotifyEventArgs e)
        {
            try
            {
                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + Enum.GetName(typeof(EventType), e.EventType);

                string s;

                try
                {
                    s = e.Device.Name;
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        eventText += " - " + s;
                }
                catch { }

                TreeNode tvEvent = treeEvents.Nodes.Insert(0, eventText);

                try
                {
                    s = "VID: 0x" + e.Device.IdVendor.ToString("X4") + ", PID: 0x" + e.Device.IdProduct.ToString("X4");
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add(s);
                }
                catch { }

                try
                {
                    s = e.Device.SerialNumber;
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Serial Number: " + s);
                }
                catch { }

                try
                {
                    s = e.Device.ClassGuid.ToString();
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Class GUID: " + s.ToUpperInvariant());
                }
                catch { }

                try
                {
                    s = Enum.GetName(typeof(DeviceType), e.DeviceType);
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Device Type: " + s);
                }
                catch { }

                try
                {
                    s = e.Port.Name;
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Port: " + s);
                }
                catch { }
            }
            catch (Exception ex)
            {
                //ErrorReportWindow.Show(ex, "Error During USB Panel Notification Event");

                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + "USB Event Exception";

                TreeNode tvEvent = treeEvents.Nodes.Insert(0, eventText);
                tvEvent.Nodes.Add(ex.Message);
            }
        }

        private void FillDeviceInfo()
        {
            try
            {
                UsbRegDeviceList mDevList = UsbDevice.AllDevices;

                treeDevices.Nodes.Clear();
                treeDevices.Nodes.Add("Refreshed at " + DateTime.Now.ToString("HH:mm:ss.fff"));

                foreach (UsbRegistry device in mDevList)
                {
                    TreeNode tvDevice = treeDevices.Nodes.Add(string.Format(
                        "VID: 0x{0:X4}, PID: 0x{1:X4}, (rev: {2}) - {3}",
                        device.Vid,
                        device.Pid,
                        (ushort)device.Rev,
                        device[SPDRP.DeviceDesc]
                    ));

                    UsbDevice mUsbDevice;
                    if (!device.Open(out mUsbDevice)) continue;

                    UsbRegistry mUsbRegistry = device;

                    string[] sDeviceAdd = mUsbDevice.Info.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in sDeviceAdd)
                        tvDevice.Nodes.Add(s);

                    foreach (UsbConfigInfo cfgInfo in mUsbDevice.Configs)
                    {
                        TreeNode tvConfig = tvDevice.Nodes.Add("Config " + cfgInfo.Descriptor.ConfigID);
                        string[] sCfgAdd = cfgInfo.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in sCfgAdd)
                            tvConfig.Nodes.Add(s);

                        TreeNode tvInterfaces = tvConfig; //.Nodes.Add("Interfaces");
                        foreach (UsbInterfaceInfo interfaceInfo in cfgInfo.InterfaceInfoList)
                        {
                            TreeNode tvInterface =
                                tvInterfaces.Nodes.Add("Interface [" + interfaceInfo.Descriptor.InterfaceID + "," + interfaceInfo.Descriptor.AlternateID + "]");
                            string[] sInterfaceAdd = interfaceInfo.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in sInterfaceAdd)
                                tvInterface.Nodes.Add(s);

                            TreeNode tvEndpoints = tvInterface; //.Nodes.Add("Endpoints");
                            foreach (UsbEndpointInfo endpointInfo in interfaceInfo.EndpointInfoList)
                            {
                                TreeNode tvEndpoint = tvEndpoints.Nodes.Add("Endpoint 0x" + (endpointInfo.Descriptor.EndpointID).ToString("X2"));
                                string[] sEndpointAdd = endpointInfo.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in sEndpointAdd)
                                    tvEndpoint.Nodes.Add(s);
                            }
                        }
                    }
                    mUsbDevice.Close();

                    TreeNode tvRegistry = tvDevice.Nodes.Add("Registry Properties");
                    mUsbRegistry = device;
                    UsbSymbolicName symName = UsbSymbolicName.Parse(mUsbRegistry.SymbolicName);

                    if (symName.SerialNumber != string.Empty)
                    {
                        tvRegistry.Nodes.Add(string.Format("SerialNumber: {0}", symName.SerialNumber));
                    }
                    if (symName.ClassGuid != Guid.Empty)
                    {
                        tvRegistry.Nodes.Add(string.Format("Class Guid: {0}", symName.ClassGuid));
                    }
                    foreach (KeyValuePair<string, object> current in mUsbRegistry.DeviceProperties)
                    {
                        string key = current.Key;
                        object oValue = current.Value;

                        if (oValue is string[])
                        {
                            if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(key.Trim()) == false)
                            {
                                TreeNode tvKey = tvRegistry.Nodes.Add(key);
                                string[] saValue = oValue as string[];
                                foreach (string s in saValue)
                                {
                                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                                        tvKey.Nodes.Add(s);
                                }
                            }
                        }
                        else if (oValue is string)
                        {
                            string s = oValue as string;
                            if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                                tvRegistry.Nodes.Add(s);
                        }
                    }
                }

                if (treeDevices.Nodes.Count <= 1)
                {
                    TreeNode tvNoDev = treeDevices.Nodes.Add("No Devices Found");
                    tvNoDev.Nodes.Add("A device must be installed which uses the LibUsb-Win32 driver.");
                    tvNoDev.Nodes.Add("Or");
                    tvNoDev.Nodes.Add("The LibUsb-Win32 kernel service must be enabled.");
                }
            }
            catch (Exception ex)
            {
                //ErrorReportWindow.Show(ex, "Error While Getting USB Device Data");
                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + "Exception During Device Refresh";

                TreeNode tvEvent = treeEvents.Nodes.Insert(0, eventText);
                tvEvent.Nodes.Add(ex.Message);
            }
        }

        private void OnUsbError(object sender, UsbError usbError)
        {
            try
            {
                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + "USB Error";

                TreeNode tvEvent = treeEvents.Nodes.Insert(0, eventText);

                string s;

                try
                {
                    s = usbError.Description;
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add(s);
                }
                catch { }
                try
                {
                    s = usbError.ErrorCode.ToString();
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Error Code" + s);
                }
                catch { }
                try
                {
                    s = usbError.Sender.ToString();
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Sender: " + s);
                }
                catch { }
                try
                {
                    s = usbError.Win32ErrorString;
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Win32 Error: " + s);
                }
                catch { }
                try
                {
                    s = usbError.Win32ErrorNumber.ToString();
                    if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                        tvEvent.Nodes.Add("Win32 Error Number: " + s);
                }
                catch { }
            }
            catch (Exception ex)
            {
                //ErrorReportWindow.Show(ex, "Exception During USB Error Event");
                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + "USB Error Exception";

                TreeNode tvEvent = treeEvents.Nodes.Insert(0, eventText);
                tvEvent.Nodes.Add(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillDeviceInfo();
        }

        private void btnEventsClear_Click(object sender, EventArgs e)
        {
            treeEvents.Nodes.Clear();
        }
    }
}
