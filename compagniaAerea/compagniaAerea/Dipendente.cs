using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    interface Dipendente
    {
        
        void getDipendente(int idDIpendente);
        //List<DipendenteImpl.InfoDipendente> getStaff(); TE L'HO COMMENTATA IO PERCHE' MI DAVA ERRORE
       
        /*get dei dati dipendente*/
        String getNome();
        String getCognome();
        String getIndirizzo();
        String getDataNascita();
        String getDataAssunzione();
        String getEmail();
        String getTelefono();
        String getSesso();
        String getPilota();
        String getHostess();
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
