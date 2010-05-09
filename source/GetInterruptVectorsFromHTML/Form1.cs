using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GetInterruptVectorsFromHTML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<IntVect>> chipRes = new Dictionary<string, List<IntVect>>();
            List<IntVect> vectList = new List<IntVect>();

            string input = textBox1.Text;

            XmlDocument xDoc = new XmlDocument();

            try
            {
                xDoc.LoadXml(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            XmlElement docEle = xDoc.DocumentElement;

            foreach (XmlElement vectEle in docEle.GetElementsByTagName("tr"))
            {
                string newVectName = "";
                string oldVectName = "";
                string desc = "";
                string allParts = "";

                try
                {
                    newVectName = vectEle.GetElementsByTagName("td")[0].InnerText;
                    oldVectName = vectEle.GetElementsByTagName("td")[1].InnerText;
                    desc = vectEle.GetElementsByTagName("td")[2].InnerText;
                    allParts = vectEle.GetElementsByTagName("td")[3].InnerText;
                }
                catch { continue; }

                IntVect v = new IntVect(newVectName, oldVectName, desc);

                string[] parts = allParts.Split(new char[] { ',', ' ', '\r', '\n', '\t', });
                foreach (string part_ in parts)
                {
                    string part = part_.ToLowerInvariant().Trim();

                    if (string.IsNullOrEmpty(part))
                        continue;

                    if (chipRes.ContainsKey(part) == false)
                        chipRes.Add(part, new List<IntVect>());

                    if (chipRes[part].Contains(v) == false)
                        chipRes[part].Add(v);

                    if (vectList.Contains(v) == false)
                        vectList.Add(v);
                }
            }

            string filePath = Directory.GetCurrentDirectory().Trim(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "interruptvectors.xml";
            
            XmlTextWriter writer = new XmlTextWriter(filePath, new ASCIIEncoding());

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 1;
            writer.IndentChar = '\t';

            writer.WriteStartDocument();

            writer.WriteStartElement("InterruptVectors");

            writer.WriteStartElement("ListOfVectors");
            {
                foreach (IntVect i in vectList)
                {
                    writer.WriteStartElement("Vector");
                    writer.WriteAttributeString("Text", i.Description);
                    writer.WriteElementString("NewName", i.NewName);
                    writer.WriteElementString("OldName", i.OldName);
                    writer.WriteElementString("Desc", i.Description);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();

            writer.WriteStartElement("ListOfChips");
            {
                foreach (KeyValuePair<string, List<IntVect>> i in chipRes)
                {
                    writer.WriteStartElement("Chip");
                    writer.WriteAttributeString("Name", i.Key);
                    foreach (IntVect j in i.Value)
                    {
                        writer.WriteStartElement("HasVector");
                        writer.WriteAttributeString("NewName", j.NewName);
                        writer.WriteAttributeString("OldName", j.OldName);
                        writer.WriteString(j.Description);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Close();

            textBox1.Text = File.ReadAllText(filePath);
            textBox1.ReadOnly = true;
            btnGO.Enabled = false;
            btnGO.Visible = false;
        }
    }
}
