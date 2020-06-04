using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ColorCount
{
    public static class SaveLoadColorList
    {
        public static List<ColorTagged> Load(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            List<ColorTagged> colorTaggeds = new List<ColorTagged>();
            XmlDocument xml = new XmlDocument();

            xml.Load(filePath);
            XmlNode node = xml.LastChild;

            XmlNodeList nodes = node.ChildNodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNodeList n = nodes[i].ChildNodes;
                int tag = Int32.Parse(n[0].InnerText);
                string name = n[1].InnerText;
                string value = n[2].InnerText;
                ColorTagged c = new ColorTagged(tag);
                c.Name = name;
                c.Hex = value;

                colorTaggeds.Add(c);
            }



            return colorTaggeds;
        }
        public static bool Save(ColorList colorList,string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version = \"1.0\" encoding = \"UTF-8\" ?>");
            sb.AppendLine("<ColorList>");
            for (int i = 0; i < colorList.Count; i++)
            {
                sb.AppendLine("<Color>");
                sb.Append("<Tag>");
                sb.Append(colorList[i].Tag);
                sb.AppendLine("</Tag>");
                sb.Append("<Name>");
                sb.Append(colorList[i].Name);
                sb.AppendLine("</Name>");
                sb.Append("<Value>");
                sb.Append(colorList[i].Hex);
                sb.AppendLine("</Value>");
                sb.AppendLine("</Color>");
            }
            sb.AppendLine("</ColorList>");

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(sb.ToString());
            }


            return true;
        }
    }
}
