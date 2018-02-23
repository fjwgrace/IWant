using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    class ColorManage
    {
        private static Dictionary<string, ColorConverter> ColorDic = new Dictionary<string, ColorConverter>();

        public static string GetColor(string key)
        {
            if(ColorDic.ContainsKey(key))
            {
                return ColorDic[key].Converter;
            }
            return "未知颜色" + key;
        }
        public static string GetCenterColor(string code)
        {
            foreach( var item in ColorDic)
            {
                if(item.Value.Converter==code)
                {
                    return item.Value.Name;
                }
            }
            return "未知颜色";
        }
        public static void LoadColorConverter()
        {
            try
            {
                string path = Application.StartupPath + "\\Model\\ColorConverter.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList nodeList = doc.SelectSingleNode("colors").ChildNodes;
                foreach (XmlNode item in nodeList)
                {
                    ColorConverter cvt = new ColorConverter();
                    cvt.Key = item.Attributes["name"].Value;
                    cvt.Name = item.Attributes["value"].Value;
                    cvt.Converter = item.InnerText;
                    ColorDic.Add(cvt.Key, cvt);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("加载车牌颜色映射表失败", ex);
            }
        }
    }
    class ColorConverter
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Converter { get; set; }
    }
}
