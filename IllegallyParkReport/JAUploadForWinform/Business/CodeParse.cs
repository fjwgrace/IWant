using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WiseMan.Business.Common;

namespace JAUploadForWinform.Business
{
    class CodeParse
    {
        private static Dictionary<string, string> collections = new Dictionary<string, string>();

        public static Dictionary<string,string> DptCollections
        {
            get { return collections; }
        }
        public static void LoadXml()
        {
            try
            {
                collections.Clear();
                string path = Application.StartupPath + "\\Model\\CollectList.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList nodeList = doc.SelectSingleNode("collections").ChildNodes;
                foreach (XmlNode item in nodeList)
                {
                    collections.Add(item.Attributes["value"].Value, item.InnerText);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error("加载采集机关信息出错", ex);
            }

        }
        public static string NameToCode(string name)
        {
            string code = null; 
            foreach(KeyValuePair<string,string> item in collections)
            {
                if(item.Value==name)
                {
                    code = item.Key;
                    break;
                }
            }
            return code;
        }
        public static string CodeToName(string code)
        {
            string name = null;
            foreach (KeyValuePair<string, string> item in collections)
            {
                if (item.Key == code)
                {
                    name = item.Value;
                    break;
                }
            }
            return name;
        }

    }
}
