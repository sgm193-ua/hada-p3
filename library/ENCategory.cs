using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENCategory
    {

        private int _id;
        private string _name;

        public int Id
        {
            get { return _id; }
            set { _id = value >= 0 ? value : 0; }  
        }

        public string Name
        {
            get { return _name; }
            set { _name = !string.IsNullOrWhiteSpace(value) ? value : "Nombre Desconocido"; }  
        }


        public ENCategory()
        {
            Name = string.Empty;
        }

        public ENCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public bool Read() =>
         new CADCategory().Read(this);


        public List<ENCategory> ReadAll() => new CADCategory().ReadAll();

    }
}
