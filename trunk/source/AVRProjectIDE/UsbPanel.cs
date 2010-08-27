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

                TreeNode tvEvent = treeErrors.Nodes.Insert(0, eventText);
                treeErrors.Nodes.Add(ex.Message);
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

                    tvDevice.Nodes.Add(string.Format("{0}: 0x{1:X4}", "CurrentCultureLangID", mUsbDevice.Info.CurrentCultureLangID));
                    tvDevice.Nodes.Add(string.Format("{0}: {1}", "ManufacturerString", mUsbDevice.Info.ManufacturerString));
                    tvDevice.Nodes.Add(string.Format("{0}: {1}", "ProductString", mUsbDevice.Info.ProductString));
                    tvDevice.Nodes.Add(string.Format("{0}: {1}", "SerialString", mUsbDevice.Info.SerialString));
                    tvDevice.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "DriverMode", mUsbDevice.DriverMode, (int)mUsbDevice.DriverMode));

                    if (mUsbDevice.Info.Descriptor != null)
                    {
                        TreeNode tvDescriptor = tvDevice.Nodes.Add(string.Format("Descriptor [Length: {0}]", mUsbDevice.Info.Descriptor.Length));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "DescriptorType", mUsbDevice.Info.Descriptor.DescriptorType, (int)mUsbDevice.Info.Descriptor.DescriptorType));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "Class", mUsbDevice.Info.Descriptor.Class, (int)mUsbDevice.Info.Descriptor.Class));
                        tvDescriptor.Nodes.Add(string.Format("{0}: 0x{1:X2}", "SubClass", mUsbDevice.Info.Descriptor.SubClass));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1}", "ConfigurationCount", mUsbDevice.Info.Descriptor.ConfigurationCount));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1}", "MaxPacketSize0", mUsbDevice.Info.Descriptor.MaxPacketSize0));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1}", "ManufacturerStringIndex", mUsbDevice.Info.Descriptor.ManufacturerStringIndex));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1}", "ProductStringIndex", mUsbDevice.Info.Descriptor.ProductStringIndex));
                        tvDescriptor.Nodes.Add(string.Format("{0}: {1}", "SerialStringIndex", mUsbDevice.Info.Descriptor.SerialStringIndex));
                        tvDescriptor.Nodes.Add(string.Format("{0}: 0x{1:X4}, {2}: 0x{3:X4}", "BcdUsb", mUsbDevice.Info.Descriptor.BcdUsb, "BcdDevice", mUsbDevice.Info.Descriptor.BcdDevice));
                    }

                    if (mUsbDevice.ActiveEndpoints != null)
                    {
                        TreeNode tvActiveEndpoints = tvDevice.Nodes.Add(string.Format("{0} [Count: {1}]", "ActiveEndpoints", mUsbDevice.ActiveEndpoints.Count));
                        foreach (var i in mUsbDevice.ActiveEndpoints)
                        {
                            FillEndpointInfo(tvActiveEndpoints, i);
                        }
                    }

                    //string[] sDeviceAdd = mUsbDevice.Info.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    //foreach (string s in sDeviceAdd)
                    //    tvDevice.Nodes.Add(s);

                    foreach (UsbConfigInfo cfgInfo in mUsbDevice.Configs)
                    {
                        TreeNode tvConfig = tvDevice.Nodes.Add("Config 0x" + cfgInfo.Descriptor.ConfigID.ToString("X2"));

                        tvConfig.Nodes.Add(string.Format("{0}: {1}", "ConfigString", cfgInfo.ConfigString));

                        //string[] sCfgAdd = cfgInfo.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        //foreach (string s in sCfgAdd)
                        //    tvConfig.Nodes.Add(s);

                        TreeNode tvInterfaces = tvConfig; //.Nodes.Add("Interfaces");
                        foreach (UsbInterfaceInfo interfaceInfo in cfgInfo.InterfaceInfoList)
                        {
                            TreeNode tvInterface = tvInterfaces.Nodes.Add("Interface [0x" + interfaceInfo.Descriptor.InterfaceID.ToString("X2") + ", 0x" + interfaceInfo.Descriptor.AlternateID.ToString("X2") + "]");

                            tvInterface.Nodes.Add(string.Format("{0}: {1}", "InterfaceString", interfaceInfo.InterfaceString));
                            if (interfaceInfo.EndpointInfoList != null)
                            {
                                TreeNode tvEndpoints = tvInterface.Nodes.Add(string.Format("Endpoints [Count: {0}]", interfaceInfo.EndpointInfoList.Count));
                                foreach (var i in interfaceInfo.EndpointInfoList)
                                {
                                    FillEndpointInfo(tvEndpoints, i);
                                }
                            }

                            if (interfaceInfo.Descriptor != null)
                            {
                                TreeNode tvDesc = tvInterface.Nodes.Add(string.Format("Descriptor [Length: {0}]", interfaceInfo.Descriptor.Length));
                                tvDesc.Nodes.Add(string.Format("{0}: 0x{1:X2}, {2}: 0x{3:X2}", "InterfaceID", interfaceInfo.Descriptor.InterfaceID, "AlternateID", interfaceInfo.Descriptor.AlternateID));
                                tvDesc.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "Class", interfaceInfo.Descriptor.Class, (int)interfaceInfo.Descriptor.Class));
                                tvDesc.Nodes.Add(string.Format("{0}: 0x{1:X2}", "Subclass", interfaceInfo.Descriptor.SubClass));
                                tvDesc.Nodes.Add(string.Format("{0}: 0x{1:X2}", "Protocol", interfaceInfo.Descriptor.Protocol));
                                tvDesc.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "Class", interfaceInfo.Descriptor.DescriptorType, (int)interfaceInfo.Descriptor.DescriptorType));
                                tvDesc.Nodes.Add(string.Format("{0}: {1}", "EndpointCount", interfaceInfo.Descriptor.EndpointCount));
                                tvDesc.Nodes.Add(string.Format("{0}: 0x{1:X2}", "StringIndex", interfaceInfo.Descriptor.StringIndex));
                            }

                            //string[] sInterfaceAdd = interfaceInfo.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            //foreach (string s in sInterfaceAdd)
                            //    tvInterface.Nodes.Add(s);

                            //TreeNode tvEndpoints = tvInterface; //.Nodes.Add("Endpoints");
                            //foreach (UsbEndpointInfo endpointInfo in interfaceInfo.EndpointInfoList)
                            //{
                            //    TreeNode tvEndpoint = tvEndpoints.Nodes.Add("Endpoint 0x" + (endpointInfo.Descriptor.EndpointID).ToString("X2"));
                            //    string[] sEndpointAdd = endpointInfo.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            //    foreach (string s in sEndpointAdd)
                            //        tvEndpoint.Nodes.Add(s);
                            //}
                        }
                    }
                    mUsbDevice.Close();

                    TreeNode tvRegistry = tvDevice.Nodes.Add("Registry Properties");
                    mUsbRegistry = device;
                    UsbSymbolicName symName = UsbSymbolicName.Parse(mUsbRegistry.SymbolicName);

                    if (string.IsNullOrEmpty(symName.SerialNumber) == false)
                        tvRegistry.Nodes.Add(string.Format("{0}: {1}", "SerialNumber", symName.SerialNumber));
                    else
                        tvRegistry.Nodes.Add(string.Format("{0}: {1}", "SerialNumber", "None"));

                    if (symName.ClassGuid == null)
                        tvRegistry.Nodes.Add(string.Format("{0}: (NULL)", "ClassGuid"));
                    else if (symName.ClassGuid != Guid.Empty)
                        tvRegistry.Nodes.Add(string.Format("{0}: {1}", "ClassGuid", symName.ClassGuid.ToString().ToUpperInvariant()));
                    else
                        tvRegistry.Nodes.Add(string.Format("{0}: {1} (None)", "ClassGuid", symName.ClassGuid));

                    foreach (KeyValuePair<string, object> current in mUsbRegistry.DeviceProperties)
                    {
                        string key = current.Key;
                        object oValue = current.Value;

                        if (oValue is string[])
                        {
                            if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(key.Trim()) == false)
                            {
                                string[] saValue = oValue as string[];
                                TreeNode tvKey = tvRegistry.Nodes.Add(key + " [" + saValue.Length.ToString() + " Items]");
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
                            if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(key.Trim()) == false)
                            {
                                key = key.Trim();

                                if (string.IsNullOrEmpty(s) == false && string.IsNullOrEmpty(s.Trim()) == false)
                                    tvRegistry.Nodes.Add(key + ": " + s);
                            }
                        }
                    }
                }

                if (treeDevices.Nodes.Count <= 1)
                {
                    treeDevices.Nodes.Add("No Devices Found");
                    treeDevices.Nodes.Add("A device must be installed which uses the LibUsb-Win32 driver.");
                    treeDevices.Nodes.Add("Or");
                    treeDevices.Nodes.Add("The LibUsb-Win32 kernel service must be enabled.");
                    treeDevices.Nodes.Add("See the AVR Project IDE wiki for details");
                    treeDevices.Nodes.Add(Properties.Resources.UsbInfoPanelWikiURL);
                }
            }
            catch (Exception ex)
            {
                //ErrorReportWindow.Show(ex, "Error While Getting USB Device Data");
                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + "Exception During Device Refresh";

                TreeNode tvEvent = treeErrors.Nodes.Insert(0, eventText);
                treeErrors.Nodes.Add(ex.Message);
            }
        }

        private static void FillEndpointInfo(TreeNode tvEndpoints, UsbEndpointInfo i)
        {
            if (i.Descriptor != null)
            {
                TreeNode tvEpDesc = tvEndpoints.Nodes.Add(string.Format("Endpoint (ID: 0x{0:X4}) Descriptor [Length: {1}]", i.Descriptor.EndpointID, i.Descriptor.Length));
                tvEpDesc.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "DescriptorType", i.Descriptor.DescriptorType, (int)i.Descriptor.DescriptorType));
                tvEpDesc.Nodes.Add(string.Format("{0}: {1}", "Attributes", i.Descriptor.Attributes));
                tvEpDesc.Nodes.Add(string.Format("{0}: {1}", "MaxPacketSize", i.Descriptor.MaxPacketSize));
                tvEpDesc.Nodes.Add(string.Format("{0}: {1}", "Interval", i.Descriptor.Interval));
                tvEpDesc.Nodes.Add(string.Format("{0}: {1}", "Refresh", i.Descriptor.Refresh));
                tvEpDesc.Nodes.Add(string.Format("{0}: 0x{1:X2}", "SynchAddress", i.Descriptor.SynchAddress));
            }

            if (i.CustomDescriptors != null)
            {
                TreeNode tvEpDesc = tvEndpoints.Nodes.Add(string.Format("EP Custom Descriptor [Length: {0}]", i.CustomDescriptors.Count));
                FillCustomDescriptor(i.CustomDescriptors, tvEpDesc);
            }
        }

        private static void FillEndpointInfo(TreeNode tvEndpoint, UsbEndpointBase i)
        {
            tvEndpoint.Nodes.Add(string.Format("{0}: {1}", "Endpoint Number", i.EpNum));
            tvEndpoint.Nodes.Add(string.Format("{0}: {1} (0x{2:X2})", "Type", i.Type, (int)i.Type));

            FillEndpointInfo(tvEndpoint, i.EndpointInfo);
        }

        private static void FillCustomDescriptor(System.Collections.ObjectModel.ReadOnlyCollection<byte[]> readOnlyCollection, TreeNode tvNode)
        {
            string text = "";
            for (int j = 0; j < readOnlyCollection.Count; j++)
            {
                text += string.Format("{0:X2} ", readOnlyCollection[j]);
                if (j % 16 == 0 && j != 0)
                {
                    tvNode.Nodes.Add(text);
                    text = "";
                }
            }
            if (string.IsNullOrEmpty(text) == false)
                tvNode.Nodes.Add(text);
        }

        private void OnUsbError(object sender, UsbError usbError)
        {
            try
            {
                string eventText = DateTime.Now.ToString("HH:mm:ss.fff - ") + "USB Error";

                TreeNode tvEvent = treeErrors.Nodes.Insert(0, eventText);

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

                TreeNode tvEvent = treeErrors.Nodes.Insert(0, eventText);
                treeErrors.Nodes.Add(ex.Message);
            }
        }

        private void treeDevices_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
        }

        private void mbtnRefreshDevices_Click(object sender, EventArgs e)
        {
            FillDeviceInfo();
        }

        private void mbtnClearEventsErrors_Click(object sender, EventArgs e)
        {
            treeEvents.Nodes.Clear();
            treeErrors.Nodes.Clear();
        }
    }
}
