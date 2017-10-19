using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Lords.DataModel
{
    internal class AppreciationDictionaryReader
    {
        private Dictionary<int, Appreciation> _dict;
        private Stream _stream;

        public AppreciationDictionaryReader(Dictionary<int, Appreciation> dict, Stream stream)
        {
            _dict = dict;
            _stream = stream;
        }

        public void Process()
        {
            ReadDictionaryXML();
        }

        private void ReadDictionaryXML()
        {
            var xdoc = XDocument.Load(_stream);

            IEnumerable<XElement> xdicts;

            if (xdoc.Root.Name == "dictionaries")
            {
                xdicts = xdoc.Root.Elements("dictionary");
            }
            else
            {
                XElement xdict = xdoc.Element("dictionary");
                if (xdict == null) throw new Exception("Expected <dictionary> root node in Appreciation dictionary.");

                List<XElement> dicts = new List<XElement>();
                dicts.Add(xdict);
                xdicts = dicts;
            }

            foreach (var xdict in xdicts)
            {
                foreach (XElement xentry in xdict.Elements("appreciation"))
                {
                    string idString = xentry.Attribute("Id").Value;
                    int id = 0;
                    int.TryParse(idString, out id);

                    string resouceType = xentry.Attribute("resource").Value;

                    int property = 0;
                    int.TryParse(xentry.Attribute("property").Value, out property);

                    int capacity = 0;
                    int.TryParse(xentry.Attribute("capacity").Value, out capacity);

                    int acceleration = 0;
                    int.TryParse(xentry.Attribute("acceleration").Value, out acceleration);

                    _dict.Add(id, new Appreciation(id, resouceType, property, capacity, acceleration));
                }
            }
        }
    }
}
