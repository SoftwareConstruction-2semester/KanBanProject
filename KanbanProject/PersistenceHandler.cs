using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace KanbanProject
{
    class PersistenceHandler
    {
        public static void Save(ObservableCollection<PostIt> ToDo, ObservableCollection<PostIt> Doing, ObservableCollection<PostIt> Done)
        {
            String json = JsonConvert.SerializeObject(ToDo);
            File.WriteAllText("ToDo.json", json);

            json = JsonConvert.SerializeObject(Doing);
            File.WriteAllText("Doing.json", json);

            json = JsonConvert.SerializeObject(Done);
            File.WriteAllText("Done.json", json);
        }

        public static void Load(ObservableCollection<PostIt> postits)
        {
           
        }

        public static List<PostIt> LoadTodoList()
        {
            string json = File.ReadAllText("ToDo.json");
            return JsonConvert.DeserializeObject<List<PostIt>>(json);
        }

        public static List<PostIt> LoadDoingList()
        {
            string json = File.ReadAllText("Doing.json");
            return JsonConvert.DeserializeObject<List<PostIt>>(json);
        }

        public static List<PostIt> loadDoneList()
        {
            string json = File.ReadAllText("Done.json");
            return JsonConvert.DeserializeObject<List<PostIt>>(json);
        }
    }
}