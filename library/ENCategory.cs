using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENCategory
    {
        // Atributos privados
        private int _id;
        private string _name;

        // Propiedades públicas con validación
        public int id
        {
            get { return _id; }
            set { _id = value >= 0 ? value : 0; }  // Asegura que el ID no sea negativo
        }

        public string name
        {
            get { return _name; }
            set { _name = !string.IsNullOrWhiteSpace(value) ? value : "Nombre Desconocido"; }  // Asigna un valor por defecto si el nombre es nulo o vacío
        }

        // Constructor por defecto, crea una categoría con nombre vacío
        public ENCategory()
        {
            name = string.Empty;
        }

        // Constructor parametrizado, crea una categoría con los valores proporcionados
        public ENCategory(int Id, string Name)
        {
            id = Id;
            name = Name;
        }

        // Métodos para la base de datos

        // Lee una categoría desde la base de datos
        public bool Read() =>
         new CADCategory().Read(this);

        // Lee todas las categorías desde la base de datos
        public List<ENCategory> ReadAll() => new CADCategory().ReadAll();

    }
}
