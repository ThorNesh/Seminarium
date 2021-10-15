using Api.App.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.App.Infrastructure.IServices
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        IEnumerable<Client> FilterByName(string searchedName);
        IEnumerable<Client> FilterByLastname(string searchedLastname);
        IEnumerable<Client> FilterByContactNumber(string searchedNumber);
        IEnumerable<Client> FilterByContactMail(string searchedMail);
        Client Get(int id);
        int Post(Client var);
        int UpdateName(int id, string name);
        int UpdateLastname(int id, string lastname);
        int UpdateContactNumber(int id, string contactNumber);
        int UpdateContactMail(int id, string contactMail);
        int Delete(int id);

    }
}
