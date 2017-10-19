using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lords.DataModel
{
    public class AppreciationDictionary
    {
        private static readonly object _mutex = new object();

        private static Dictionary<int, Appreciation> _default;
        public static Dictionary<int, Appreciation> Default
        {
            get
            {
                if (_default == null)
                {
                    lock (_mutex)
                    {
                        _default = EnsureDefaultDictionariesLoaded();
                    }
                }
                return _default;
            }
        }

        public static Appreciation GetAppreciation(int id)
        {
            if (Default.ContainsKey(id))
            {
                return Default[id];
            }
            else
            {
                return Appreciation.NullAppreciation();
            }
        }

        private static Dictionary<int, Appreciation> EnsureDefaultDictionariesLoaded()
        {
            Dictionary<int, Appreciation> dictionary = new Dictionary<int, Appreciation>();

            var assembly = typeof(AppreciationDictionary).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("Lords.DataModel.Dictionaries.AppreciationDictionary.xml"))
            {
                var reader = new AppreciationDictionaryReader(dictionary, stream);
                reader.Process();
            }

            return dictionary;
        }


    }
}
