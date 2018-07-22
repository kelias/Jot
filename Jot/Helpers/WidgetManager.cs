/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Jarloo.Jot.Models;

namespace Jarloo.Jot.Helpers
{
    public class WidgetManager
    {
        private const string FILENAME = "notes.jot";

        public ObservableCollection<IWidget> Widgets { get; private set; }

        private static WidgetManager instance;

        private WidgetManager()
        {
            Load();
        }

        public static WidgetManager GetInstance()
        {
            return instance ?? (instance = new WidgetManager());
        }

        public void Save()
        {
            var bag = new GoodieBag();
            bag.Widgets = Widgets.ToList();

            using (Stream stream = File.Open(FILENAME, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, bag);
                stream.Close();
            }
        }

        public void Load()
        {
            if (!File.Exists(FILENAME))
            {
                Widgets = new ObservableCollection<IWidget>();
                return;
            }

            GoodieBag bag;
            using (Stream stream = File.Open(FILENAME, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                bag = (GoodieBag) binaryFormatter.Deserialize(stream);
                stream.Close();
            }

            Widgets = new ObservableCollection<IWidget>(bag.Widgets);
        }

        public void ChangeZOrder(IWidget w)
        {
            var oldIndex = Widgets.IndexOf(w);
            var newIndex = Widgets.Count - 1;
            Widgets.Move(oldIndex, newIndex);
        }
    }
}