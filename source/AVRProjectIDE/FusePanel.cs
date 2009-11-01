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
        private const int TOP_MARGIN = 25;
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
            get { return SettingsManagement.AppDataPath + "tempfuse.txt"; }
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

            fuseReset = FillCheckListAndGetReset(typeOfFuse, xEle);

            FillDictionaries(xEle);

            FillControls();

            fuseVal = MatchAVRDUDEArgs(fuseBox, typeOfFuse, fuseReset);

            SetCheckListWithInt(fuseVal);

            txtManualHex.Text = fuseVal.ToString("X2");

            disablePresetChangeEvent = false;

            ManualEvent(null, null);
        }

        #endregion

        #region Utilities

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

        private int FillCheckListAndGetReset(FuseType typeOfFuse, XmlElement xEle)
        {
            int fuseReset = 0xFF;

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
                }

                chkListBits.Items[i] = chkText;
            }

            return fuseReset;
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

        private void FillControls()
        {
            int ctrlCnt = 0;
            int possibilities = 0;
            foreach (KeyValuePair<int, Dictionary<string, int>> i in presets)
            {
                if (i.Value.Count == 1)
                {
                    CheckBox cb = new CheckBox();
                    foreach (KeyValuePair<string, int> j in i.Value)
                        cb.Text = j.Key;
                    cb.Location = CreatePoint(ctrlCnt);
                    ctrlCnt++;
                    possibilities += 2;
                    cb.Size = CreateSize(25);
                    cb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                    cb.CheckStateChanged += new EventHandler(PresetEvent);

                    checkboxes.Add(i.Key, cb);
                    grpPresetBox.Controls.Add(cb);
                }
                else if (i.Value.Count > 1)
                {
                    ComboBox cb = new ComboBox();
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                    foreach (KeyValuePair<string, int> j in i.Value)
                    {
                        cb.Items.Add(j.Key);
                        possibilities++;
                    }
                    cb.SelectedIndex = 0;

                    cb.SelectedIndexChanged += new EventHandler(PresetEvent);

                    cb.Location = CreatePoint(ctrlCnt);
                    ctrlCnt++;
                    cb.Size = CreateSize(20);
                    cb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                    dropdowns.Add(i.Key, cb);
                    grpPresetBox.Controls.Add(cb);
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
            bool oldDisable = disablePresetChangeEvent;
            disablePresetChangeEvent = true;

            foreach (KeyValuePair<int, CheckBox> i in checkboxes)
            {
                string str = i.Value.Text;

                if (presets.ContainsKey(i.Key) == false)
                    continue;

                if ((presets[i.Key]).ContainsKey(str) == false)
                    continue;

                int val = (presets[i.Key])[str];

                int compare = fuseVal & i.Key;

                if (compare == val)
                    i.Value.CheckState = CheckState.Checked;
                else
                    i.Value.CheckState = CheckState.Unchecked;
            }

            foreach (KeyValuePair<int, ComboBox> i in dropdowns)
            {
                if (presets.ContainsKey(i.Key) == false)
                    continue;

                foreach (KeyValuePair<string, int> j in presets[i.Key])
                {
                    int val = j.Value;

                    int compare = fuseVal & i.Key;

                    if (compare == val && i.Value.Items.Contains(j.Key))
                    {
                        i.Value.SelectedIndex = i.Value.Items.IndexOf(j.Key);
                    }
                }
            }

            disablePresetChangeEvent = oldDisable;

            OutputToTextbox(fuseVal);
        }

        #endregion

        #region Preset Settings Related

        private bool disablePresetChangeEvent = false;
        private void PresetEvent(object sender, EventArgs e)
        {
            if (disablePresetChangeEvent)
                return;

            foreach (KeyValuePair<int, CheckBox> i in checkboxes)
            {
                string str = i.Value.Text;

                if (presets.ContainsKey(i.Key) == false)
                    continue;

                if ((presets[i.Key]).ContainsKey(str) == false)
                    continue;

                int val = (presets[i.Key])[str];

                if (i.Value.CheckState == CheckState.Unchecked)
                {
                    val = val ^ 0xFF;
                }

                int m = 0xFF ^ i.Key;
                val = val | m;

                fuseVal = fuseVal | i.Key;
                fuseVal = fuseVal & val;
            }

            foreach (KeyValuePair<int, ComboBox> i in dropdowns)
            {
                if (presets.ContainsKey(i.Key) == false)
                    continue;

                string str = (string)i.Value.Items[i.Value.SelectedIndex];

                foreach (KeyValuePair<string, int> j in presets[i.Key])
                {
                    if (j.Key != str)
                        continue;

                    int val = j.Value;

                    int m = 0xFF ^ i.Key;
                    val = val | m;

                    fuseVal = fuseVal | i.Key;
                    fuseVal = fuseVal & val;
                }
            }

            txtManualHex.Text = fuseVal.ToString("X2");
            SetCheckListWithInt(fuseVal);
            OutputToTextbox(fuseVal);
        }

        #endregion

        #region Button Events

        private void btnReset_Click(object sender, EventArgs e)
        {
            fuseVal = fuseReset;

            txtManualHex.Text = fuseVal.ToString("X2");
            SetCheckListWithInt(fuseVal);

            ManualEvent(sender, e);
        }

        private int ReadFuse()
        {
            if (ProjectBuilder.CheckForWinAVR())
            {
                owner.BurnerPanel.FormToProj();

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "avrdude";
                p.StartInfo.Arguments = "";
                //p.StartInfo.FileName = "cmd";
                //p.StartInfo.Arguments = "/k avrdude";

                string overrides = "";

                if (string.IsNullOrEmpty(owner.Project.BurnPort) == false)
                    overrides += "-P " + owner.Project.BurnPort;

                if (owner.Project.BurnBaud != 0)
                    overrides += " -b " + owner.Project.BurnBaud.ToString("0");

                string avrdudeFuseStr = GetAVRDUDEFuseStringFromType(typeOfFuse);

                string action = String.Format("-U {0}:r:\"{1}\":h", avrdudeFuseStr, TempFuseFilePath);

                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WorkingDirectory = SettingsManagement.AppDataPath;
                p.StartInfo.Arguments += String.Format(" -c {0} -p {1} -n {2} {3} {4}", owner.Project.BurnProgrammer, owner.Project.BurnPart, overrides, owner.Project.BurnOptions, action);

                try
                {
                    File.Delete(TempFuseFilePath);
                }
                catch { }

                try
                {
                    p.Start();
                    p.WaitForExit(5000);

                    System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                    while (true)
                    {
                        if (File.Exists(TempFuseFilePath))
                            break;

                        if (sw.ElapsedMilliseconds > 5000)
                            break;
                    }

                    if (File.Exists(TempFuseFilePath) == false)
                    {
                        MessageBox.Show("Failed to Read Fuse, temporary file not found at '" + TempFuseFilePath + "'. Here is the AVRDUDE output:\r\n\r\n" + p.StandardError.ReadToEnd() + "\r\n" + p.StandardOutput.ReadToEnd());
                        return -1;
                    }
                    else
                    {
                        string fuseStr = File.ReadAllText(TempFuseFilePath);
                        try
                        {
                            File.Delete(TempFuseFilePath);
                        }
                        catch { }

                        if (string.IsNullOrEmpty(fuseStr))
                        {
                            MessageBox.Show("Temporary Fuse File is Empty at " + TempFuseFilePath);
                            return -1;
                        }

                        fuseStr = fuseStr.Trim();
                        if (string.IsNullOrEmpty(fuseStr))
                        {
                            MessageBox.Show("Temporary Fuse File is Empty at " + TempFuseFilePath);
                            return -1;
                        }

                        return Convert.ToInt32(fuseStr, 16);
                    }
                }
                catch (Exception ex)
                {
                    ErrorReportWindow erw = new ErrorReportWindow(ex, "Error While Reading Fuse");
                    erw.ShowDialog();
                }
            }

            return -1;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int v = ReadFuse();

            if (v >= 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Fuse Read Back as 0x" + v.ToString("X2") + ", place it in the calculator?", "Fuse Read", MessageBoxButtons.YesNo))
                {
                    fuseVal = v;

                    txtManualHex.Text = fuseVal.ToString("X2");
                    SetCheckListWithInt(fuseVal);

                    ManualEvent(sender, e);
                }
            }
        }

        private void btnBurn_Click(object sender, EventArgs e)
        {
            if (ProjectBuilder.CheckForWinAVR())
            {
                owner.BurnerPanel.FormToProj();

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd";
                p.StartInfo.Arguments = "/k avrdude ";

                string overrides = "";

                if (string.IsNullOrEmpty(owner.Project.BurnPort) == false)
                    overrides += "-P " + owner.Project.BurnPort;

                if (owner.Project.BurnBaud != 0)
                    overrides += " -b " + owner.Project.BurnBaud.ToString("0");

                p.StartInfo.Arguments += String.Format("-c {0} -p {1} {2} {3} {4}", owner.Project.BurnProgrammer, owner.Project.BurnPart, overrides, owner.Project.BurnOptions, GetArgString(fuseVal));

                try
                {
                    p.Start();
                }
                catch (Exception ex)
                {
                    ErrorReportWindow erw = new ErrorReportWindow(ex, "Error While Writing Fuse");
                    erw.ShowDialog();
                }
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            int v = ReadFuse();

            if (v == -1)
                return;

            if (v == fuseVal)
                MessageBox.Show("Fuse Verified Successfully to be 0x" + v.ToString("X2"));
            else
                MessageBox.Show("Fuse Is Mismatched, Read 0x" + v.ToString("X2") + " , Current: 0x" + fuseVal.ToString("X2"));
        }

        #endregion

        private string GetArgString(int fuseVal)
        {
            string avrdudeFuseStr = GetAVRDUDEFuseStringFromType(typeOfFuse);

            return String.Format("-U {0}:w:0x{1:X2}:m", avrdudeFuseStr, fuseVal);
        }

        private void OutputToTextbox(int fuseVal)
        {
            string str = owner.txtSuggestedFusebox.Text;

            string avrdudeFuseStr = GetAVRDUDEFuseStringFromType(typeOfFuse);

            string newStr = GetArgString(fuseVal);

            Regex r = new Regex("-U\\s*" + avrdudeFuseStr + ":[wrv]:0[xX]([0-9a-fA-F]+):m");
            Match match = r.Match(str);
            if (match.Success)
            {
                while (match.Success)
                {
                    str = str.Replace(match.Value, newStr);

                    match = match.NextMatch();
                }
            }
            else
            {
                str += " " + newStr;
            }

            while (str.Contains("  "))
                str.Replace("  ", " ");

            owner.txtSuggestedFusebox.Text = str;
        }
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
