using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POC_bingmaps
{
    class Map
    {
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public string Name { get; set; }
        public string Adress { get; set; }

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged("");
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }

        public Map()
        {
            Employees = new ObservableCollection<Employee>();
            Employees.Add(new Employee("Den Bosch", "Geert", this) { Latitude = 13f, Longitude = 15f, Pos = "51.688641,5.887090" });
            Employees.Add(new Employee("Den Bosch", "Peter", this) { Latitude = 15f, Longitude = 15f, Pos = "52.688641,5.287090" });

            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove);
        }

        private void Add()
        {
            var employee = new Employee(Adress, Name, this);
            Employees.Add(employee);
            Adress = "";
            Name = "";
            OnPropertyChanged("");
        }

        public void Remove()
        {
            Employees.Remove(SelectedEmployee);
            SelectedEmployee = null;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
