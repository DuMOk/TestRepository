using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Configuration;

namespace Task6
{
    public class DataFileAccessors<T> : IDataAccessors<T>
    {
        private ILogger _myLog = new MyNLog();
        
        public List<T> GetAll()
        {
            try
            {
                XmlDocument myDoc = new XmlDocument();
                myDoc.Load(ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString);

                XPathNavigator navigator = myDoc.CreateNavigator();
                XPathNodeIterator iterator = (XPathNodeIterator)navigator.Evaluate("Persons/Person");
                var strings = new List<T>();

                while (iterator.MoveNext())
                {
                    strings.Add((T)Convert.ChangeType(iterator.Current.Value, typeof(T)));
                }
                
                return strings;
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Log(ex.ToString());
                throw;
            }
        }

        public T GetByName(string name)
        {
            try
            {
                XmlDocument myDoc = new XmlDocument();
                myDoc.Load(ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString);

                XPathNavigator navigator = myDoc.CreateNavigator();
                XPathNodeIterator iterator = 
                    (XPathNodeIterator)navigator.Evaluate(String.Format("Persons/Person[. = '{0}']", name));

                while (iterator.MoveNext())
                {
                    return (T)Convert.ChangeType(iterator.Current.Value, typeof(T));
                }
                
                return default(T);
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Log(ex.ToString());
                throw;
            }
        }
       
        public void DeleteByName(string name)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString);

                XmlNode root = doc.DocumentElement;
                XmlNode node = root.SelectSingleNode(String.Format("Person[. = '{0}']", name));
                XmlNode outer = node.ParentNode;

                outer.RemoveChild(node);
                doc.Save(ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString);
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Log(ex.ToString());
                throw;
            }
        }
      
        public void Insert(T name)
        {
            try
            {
                XmlDocument insDoc = new XmlDocument();
                insDoc.Load(ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString);

                XmlElement root = insDoc.DocumentElement;
                XmlElement param = insDoc.CreateElement("Person");

                param.InnerText = (string)Convert.ChangeType(name, typeof(T));
                root.AppendChild(param);

                insDoc.Save(ConfigurationManager.ConnectionStrings["FilePers"].ConnectionString);
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Log(ex.ToString());
                throw;
            }
        }
    }
}
