using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace каленжарь_еды__
{

    public class Note
    {
        public string name;
        public string opis;
        public DateTime data;
        public Note(string name, string opis, DateTime data)
        {
            this.name = name;
            this.opis = opis;
            this.data = data;
        }
    }
    public class Ejednevnik
    {
        public List<Note> notes;
        public List<int> today = new List<int>();
        public DateTime data;
        public Ejednevnik()
        {
            this.data = DateTime.Today.Date;
            List<Note> notes = DataWork.Deserialize<List<Note>>("spisok.json");
            this.notes = notes;
            LoadNotes();
        }
        public void AddNote(Note note)
        {
            this.notes.Add(note);
            LoadNotes();
            DataWork.Serialize(this.notes, "spisok.json");
        }
        public void LoadNotes()
        {
            this.today.Clear();
            for (int i = 0; i < this.notes.Count; i++)
            {
                Note note = this.notes[i];
                if (note.data == this.data)
                {
                    this.today.Add(i);
                }
            }
            DataWork.Serialize(this.notes, "spisok.json");
        }
    }

    public class DataWork
    {
        public static T Deserialize<T>(string path)
        {
            try
            {
                T znachenie = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return znachenie;
            }
            catch
            {
                File.WriteAllText(path, "[]");
                return Deserialize<T>(path);
            }
        }
        public static void Serialize(object obj, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(obj));
        }
    }
}
