﻿using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Clients
{
    public class ClientsVM
    {
        private Client _client;

        public ClientsVM(Client client)
        {
            this._client = client;
        }

        public int ClientId
        {
            get { return _client.id; }
            set { _client.id = value; }
        }

        public string ClientName
        {
            get { return _client.name; }
            set { _client.name = value; }
        }

        public string PostalCode
        {
            get { return _client.postalcode; }
            set { _client.postalcode = value; }
        }

        public string Street
        {
            get { return _client.street; }
            set { _client.street = value; }
        }

        public string Housenumber
        {
            get { return _client.housenumber; }
            set { _client.housenumber = value; }
        }

        public string Country
        {
            get { return _client.country; }
            set { _client.country = value; }
        }

        public string Phone
        {
            get { return _client.phone; }
            set { _client.phone = value; }
        }

        internal Client ToModel()
        {
            return _client;
        }
    }
}
