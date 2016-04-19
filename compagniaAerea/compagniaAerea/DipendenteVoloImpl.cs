using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    class DipendenteVoloImpl : DipendenteVolo
    {
        DatabaseManager myDatabase;
        List<Piano_di_volo> piano_di_volo = new List<Piano_di_volo>();

        public DipendenteVoloImpl()
        {
            myDatabase = DatabaseManager.Instance;
        }

        public void UpdatePianodivolo()
        {
            piano_di_volo = (from p in myDatabase.getDb().Piano_di_volo
                             select p).ToList();
        }

        //Aggiungi piano di volo
        public void Aggiungi_pianodivolo(DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo, Boolean cancellato)
        {
            String orario_p = DateTime.Parse(orario_partenza, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss");
            String orario_a = DateTime.Parse(orario_arrivo, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss");
            Piano_di_volo p = new Piano_di_volo()
            {
                data_partenza = Convert.ToDateTime(data_partenza).ToUniversalTime(),
                data_arrivo = Convert.ToDateTime(data_arrivo).ToUniversalTime(),
                orario_partenza = TimeSpan.Parse(orario_p),
                orario_arrivo = TimeSpan.Parse(orario_a),
                cancellato = false
            };
        }

        //Aggiungi tratta
        public void Aggiungi_tratta(String aeroporto_partenza, String aeroporto_arrivo, int gate_partenza, int gate_arrivo, DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo)
        {
            String orario_p = DateTime.Parse(orario_partenza, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss");
            String orario_a = DateTime.Parse(orario_arrivo, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss");
            Tratta t = new Tratta()
            {
                //inserimento dati nel database

                aeroporto_partenza = aeroporto_partenza,
                aeroporto_arrivo = aeroporto_arrivo,
                gate_partenza = gate_partenza,
                gate_arrivo = gate_arrivo,
                data_partenza = Convert.ToDateTime(data_partenza).ToUniversalTime(),
                data_arrivo = Convert.ToDateTime(data_arrivo).ToUniversalTime(),
                orario_partenza = TimeSpan.Parse(orario_p),
                orario_arrivo = TimeSpan.Parse(orario_a),
                posti_economy = 0,
                posti_buisness = 0,
                posti_first = 0,
            };
            myDatabase.getDb().Tratta.InsertOnSubmit(t);
            myDatabase.getDb().SubmitChanges();

          
        }

        //Controlla se il piano di volo è già inserito nel db
        public Boolean checkPianodivoloExist(DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo)
        {
            foreach (Piano_di_volo p in piano_di_volo) {
                if(p.data_partenza == data_partenza && p.data_arrivo == data_arrivo && p.orario_partenza.Equals(orario_partenza) && p.orario_arrivo.Equals(orario_arrivo))
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
