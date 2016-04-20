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
        List<String> getValueGrid(DataGrid dg);
        /*set dei dati dipende (ogni campo ho un sou metodo per gestire cambiamenti singoli nelle informazioni)*/
        // void setNome(string nome);
        // void setCognome(string cognome);
        void setIndirizzo(string indirizzo);
        //void setDataNascita(DateTime data);
        //void setDataAssunzione(DateTime data);
        void setEmail(String email);
        void setTelefono(string telefono);
       // void setSesso(string sesso);
       // void setPilota(bool pilota);
       // void sethostess(bool hostess);
    }
}
