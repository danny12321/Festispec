using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace POC_bingmaps
{
    class Employee : INotifyPropertyChanged
    {
        private Map Map;

        public string Adress { get; set; }

        public string Name { get; }

        public string Pos { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICommand SelectCommand { get; set; }

        public Employee(string adress, string name, Map map)
        {
            Adress = adress;
            Name = name;
            Map = map;
            SelectCommand = new RelayCommand(Select);
            Geocode(adress);
        }

        public void Select()
        {
            Console.WriteLine("SELECT POPPETJE " + Name);
            Map.SelectedEmployee = this;
        }

        public void Geocode(string addressQuery)
        {
            //Create REST Services geocode request using Locations API
            string geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations/" + addressQuery + "?o=xml&key=AttsGkqIHCOIEA11KtQZDphl5bi8lppin64jeg-ZOOhiS4cdHA_EXJwHSbyZi4Xo";


            //Make the request and get the response
            XmlDocument geocodeResponse = GetXmlResponse(geocodeRequest);

            Latitude = float.Parse(geocodeResponse.GetElementsByTagName("Latitude").Cast<XmlNode>().First().InnerText);
            Longitude = float.Parse(geocodeResponse.GetElementsByTagName("Longitude").Cast<XmlNode>().First().InnerText);
            Pos = $"{Latitude},{Longitude}";
            OnPropertyChanged("");
        }

        private XmlDocument GetXmlResponse(string requestUrl)
        {
            System.Diagnostics.Trace.WriteLine("Request URL (XML): " + requestUrl);
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return xmlDoc;
            }
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
