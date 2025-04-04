using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENProduct
    {
        private string _code;
        private string _name;
        private int _amount;
        private float _price;
        private int _category;
        private DateTime _creationDate;

        public string Code { get; set; }
        public string Name { get; set; }
        public int Amount  // Cambié 'amount' a 'Amount' para seguir la convención PascalCase
        {
            get { return _amount; }
            set { _amount = (value >= 0) ? value : 0; } // Evita valores negativos
        }
        public float Price  // Cambié 'price' a 'Price' para seguir la convención PascalCase
        {
            get { return _price; }
            set { _price = (value >= 0) ? value : 0; } // Evita valores negativos
        }
        public int Category { get; set; }
        public DateTime CreationDate { get; set; }

        public ENProduct() { }

        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate)
        {
            this._code = code;
            this._name = name;
            this._amount = amount >= 0 ? amount : 0;
            this._price = price >= 0 ? price : 0;
            this._category = category;
            this._creationDate = creationDate;
        }

        public bool Create() => new CADProduct().Create(this);
        public bool Update() => new CADProduct().Update(this);
        public bool Delete() => new CADProduct().Delete(this);
        public bool Read() => new CADProduct().Read(this);
        public bool ReadFirst() => new CADProduct().ReadFirst(this);
        public bool ReadNext() => new CADProduct().ReadNext(this);
        public bool ReadPrev() => new CADProduct().ReadPrev(this);
        public bool validCategory() => new CADProduct().CategoryExists(this.Category);

        public bool existe() {
            return this.Read();
        }
    }
}