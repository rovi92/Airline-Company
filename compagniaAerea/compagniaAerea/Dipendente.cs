using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    interface Dipendente
    {

        void getDipendente(int idDIpendente);
        List<Personale> getStaff();
       
        /*get dei dati dipendente*/
        String getNome();
        String getCognome();
        String getCodice();
        String getIndirizzo();
        String getDataNascita();
        String getDataAssunzione();
        String getEmail();
        String getTelefono();
        String getSesso();
        String getImpiego();
       // List<String> getValueGrid(DataGrid dg);
        /*set dei dati dipende (ogni campo ho un sou metodo per gestire cambiamenti singoli nelle informazioni)*/
        void createDipendente(string nome, string cognome, string indirizzo, DateTime data_di_nascita, DateTime data_assunzione, string email, string telefono, string sesso, bool pilota, bool hostess);
        void setIndirizzo(string indirizzo, int idPersonale);
        void setEmail(string email, int idPersonale);
        void setTelefono(string telefono, int idPersonale);

    }
}
