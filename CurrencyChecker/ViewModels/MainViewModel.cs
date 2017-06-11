using CurrencyChecker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Person> collection;
        public ObservableCollection<Person> Collection
        {
            get { return collection; }
            set { collection = value; }
        }

        public MainViewModel()
        {
            Collection = new ObservableCollection<Person>();
            Collection.Add(new Person { Name = "Pierwsza", Age = 19 });
            Collection.Add(new Person { Name = "Druga", Age = 25 });
            Collection.Add(new Person { Name = "Trzecia", Age = 30 });
        }

        public void AddElement()
        {
            Collection.Add(new Person()
            {
                Name = "Name" + DateTime.Now.Ticks,
                Age = (int)DateTime.Now.Ticks
            });
        }

    }
}
