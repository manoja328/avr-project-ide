using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class FusePanel : UserControl
    {
        #region Control Sizing

        private const int LEFT_MARGIN = 10;
        private const int RIGHT_MARGIN = 10;
        private const int TOP_MARGIN = 10;
        private const int ROW_SPACE = 25;

        private Size CreateSize(int height)
        {
            return new Size(grpPresetBox.Width - LEFT_MARGIN - RIGHT_MARGIN, height);
        }

        private Point CreatePoint(int cnt)
        {
            return new Point(LEFT_MARGIN, TOP_MARGIN + ROW_SPACE * cnt);
        }

        #endregion

        #region Fields

        private FuseCalculator owner;

        private int fuseVal = 0xFF;
        private int fuseReset = 0xFF;

        private FuseType typeOfFuse = FuseType.Unspecified;

        private Dictionary<int, Dictionary<string, int>> presets = new Dictionary<int, Dictionary<string, int>>();
        private Dictionary<string, int> maskLookup = new Dictionary<string, int>();

        private Dictionary<int, ComboBox> dropdowns = new Dictionary<int, ComboBox>();
        private Dictionary<int, CheckBox> checkboxes = new Dictionary<int, CheckBox>();

        #endregion

        #region Properties

        public string FuseName
        {
            get
            {
                return Enum.GetName(typeof(FuseType), typeOfFuse);
            }
        }

        public FuseType TypeOfFuse
        {
            get { return typeOfFuse; }
            set { typeOfFuse = value; }
        }

        private string TempFuseFilePath
        {
            get { return SettingsManagement.AppDataPath + Path.DirectorySeparatorChar + "tempfuse.txt"; }
        }

        #endregion

        #region Constructors

        private FusePanel()
        {
            InitializeComponent();
        }

        public FusePanel(XmlElement xEle, string fuseBox, FuseCalculator owner)
            : this()
        {
            this.owner = owner;

            string fuseName = xEle.Name.ToLowerInvariant().Trim();

            typeOfFuse = GetTypeOfFuseFromName(fuseName);

            txtInfo.Text = "Checked = Burnt = 0\r\n";
            txtInfo.Text += GetInfoXMLText(xEle);

            fuseReset = 0xFF;

            FillCheckListAndGetReset(typeOfFuse, xEle, chkListBits, out fuseReset);

            FillDictionaries(xEle);

            fuseVal = MatchAVRDUDEArgs(fuseBox, typeOfFuse, fuseReset);

            SetCheckListWithInt(fuseVal);

            txtManualHex.Text = fuseVal.ToString("X2");
        }

        #endregion

        #region Static Utilities

        private static int MatchAVRDUDEArgs(string fuseBox, FuseType typeOfFuse, int fuseReset)
        {
            string avrdudeFuseStr = GetAVRDUDEFuseStringFromType(typeOfFuse);

            Regex r = new Regex("-U\\s*" + avrdudeFuseStr + ":[wrv]:0[xX]([0-9a-fA-F]+):m");
            Match match = r.Match(fuseBox);
            if (match.Success)
            {
                try
                {
                    return Convert.ToInt32("0x" + match.Groups[1].Value, 16);
                }
                catch
                {
                    return fuseReset;
                }
            }
            else
                return fuseReset;
        }

        private static string GetAVRDUDEFuseStringFromType(FuseType typeOfFuse)
        {
            if (typeOfFuse == FuseType.Extended)
                return "efuse";
            else if (typeOfFuse == FuseType.Low)
                return "lfuse";
            else if (typeOfFuse == FuseType.High)
                return "hfuse";
            else if (typeOfFuse == FuseType.LockBits)
                return "lock";
            else
                return "fuse";
        }

        private static void FillCheckListAndGetReset(FuseType typeOfFuse, XmlElement xEle, CheckedListBox chkListBits, out int fuseReset)
        {
            fuseReset = 0xFF;

            string fuseOrLock = typeOfFuse == FuseType.LockBits ? "LOCKBIT" : "FUSE";

            for (int i = 0; i < 8; i++)
            {
                string chkText = "Bit " + i.ToString("0");

                foreach (XmlElement j in xEle.GetElementsByTagName(fuseOrLock + i.ToString("0")))
                {
                    chkText += ": ";

                    foreach (XmlElement k in j.GetElementsByTagName("NAME"))
                        chkText += k.InnerText.Trim() + ": ";

                    foreach (XmlElement k in j.GetElementsByTagName("TEXT"))
                        chkText += k.InnerText.Trim();

                    foreach (XmlElement k in j.GetElementsByTagName("DEFAULT"))
                    {
                        string str = k.InnerText.Trim();
                        int num = 1;
                        if (int.TryParse(str, out num))
                        {
                            if (num == 0)
                            {
                                fuseReset -= Convert.ToInt32(Math.Pow(2, i));
                                break;
                            }
                        }
                    }

                    chkListBits.Items[i] = chkText;
                }
            }
        }

        private static string GetInfoXMLText(XmlElement xEle)
        {
            foreach (XmlNode xText in xEle.ChildNodes)
            {
                if (xText.NodeType == XmlNodeType.Element)
                    if (xText.Name == "TEXT")
                        return xText.InnerText.Trim() + "\r\n";
            }

            return "\r\n";
        }

        private static FuseType GetTypeOfFuseFromName(string fuseName)
        {
            if (fuseName.StartsWith("low"))
                return FuseType.Low;
            else if (fuseName.StartsWith("high"))
                return FuseType.High;
            else if (fuseName.StartsWith("ext"))
                return FuseType.Extended;
            else if (fuseName.StartsWith("lock"))
                return FuseType.LockBits;
            else
                return FuseType.Unspecified;
        }

        #endregion

        #region Preset Loading

        private void FillDictionaries(XmlElement xEle)
        {
            for (int i = 0; i < 256; i++)
            {
                foreach (XmlElement j in xEle.GetElementsByTagName("TEXT" + i.ToString("0")))
                {
                    foreach (XmlElement k in j.GetElementsByTagName("MASK"))
                    {
                        int mask = -1;
                        if (Program.TryParseText(k.InnerText, out mask) == false)
                            continue;

                        foreach (XmlElement l in j.GetElementsByTagName("VALUE"))
                        {
                            int val = -1;
                            if (Program.TryParseText(l.InnerText, out val) == false)
                                continue;

                            foreach (XmlElement m in j.GetElementsByTagName("TEXT"))
                            {
                                string str = m.InnerText.Trim();
                                if (string.IsNullOrEmpty(str))
                                    continue;

                                if (maskLookup.ContainsKey(str) == false)
                                    maskLookup.Add(str, mask);
                                else
                                    continue;

                                if (presets.ContainsKey(mask) == false)
                                    presets.Add(mask, new Dictionary<string, int>());

                                if (presets[mask].ContainsKey(str) == false)
                                    presets[mask].Add(str, val);
                            }

                            break;
                        }

                        break;
                    }
                }
            }
        }

        #endregion

        #region Manual Settings Related

        private int GetIntFromCheckList()
        {
            int res = 0;
            for (int i = 0; i < 8; i++)
            {
                if (chkListBits.GetItemChecked(i) == false)
                    res += Convert.ToInt32(Math.Pow(2, i));
            }
            return res;
        }

        private int GetIntFromCheckList(int index, CheckState newChecked)
        {
            int res = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i == index)
                {
                    if (newChecked == CheckState.Unchecked)
                        res += Convert.ToInt32(Math.Pow(2, i));
                }
                else if (chkListBits.GetItemChecked(i) == false)
                    res += Convert.ToInt32(Math.Pow(2, i));
            }
            return res;
        }

        bool disableCheckListEvent = false;
        private void SetCheckListWithInt(int x)
        {
            disableCheckListEvent = true;
            for (int i = 0; i < 8; i++)
            {
                chkListBits.SetItemChecked(i, x % 2 == 0);
                x /= 2;
            }
            disableCheckListEvent = false;
        }

        private void txtManualHex_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32("0x" + txtManualHex.Text, 16);
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void txtManualHex_Validated(object sender, EventArgs e)
        {
            int oldVal = fuseVal;
            try
            {
                fuseVal = Convert.ToInt32("0x" + txtManualHex.Text, 16);
            }
            catch
            {
                fuseVal = oldVal;
            }

            txtManualHex.Text = fuseVal.ToString("X2");
            SetCheckListWithInt(fuseVal);

            ManualEvent(sender, e);
        }

        private void chkListBits_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (disableCheckListEvent)
                return;

            fuseVal = GetIntFromCheckList(e.Index, e.NewValue);

            txtManualHex.Text = fuseVal.ToString("X2");

            ManualEvent(sender, e);
        }

        private void ManualEvent(object sender, EventArgs e)
        {

        }

        #endregion

        #region Preset Settings Related

        private void PresetEvent(object sender, EventArgs e)
        {

        }

        #endregion
    }

    public enum FuseType
    {
        Low,
        High,
        Extended,
        Unspecified,
        LockBits
    }
}
