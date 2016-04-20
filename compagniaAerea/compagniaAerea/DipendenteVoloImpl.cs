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
        int numero_volo = 0;

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
            /*TimeSpan orario_p = TimeSpan.Parse(DateTime.Parse(orario_partenza, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss"));
            TimeSpan orario_a = TimeSpan.Parse(DateTime.Parse(orario_arrivo, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss"));*/
            Piano_di_volo p = new Piano_di_volo()
            {
                data_partenza = data_partenza,
                data_arrivo = data_arrivo,
                orario_partenza = TimeSpan.Parse(orario_partenza.ToString()),
                orario_arrivo = TimeSpan.Parse(orario_arrivo.ToString()),
                cancellato = cancellato
            };
            myDatabase.getDb().Piano_di_volo.InsertOnSubmit(p);
            myDatabase.getDb().SubmitChanges();
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
            /*  var query = from p in piano_di_volo
                          where p.data_partenza == data_partenza.ToUniversalTime() &&
                          p.data_arrivo == data_arrivo.ToUniversalTime() &&
                          p.orario_partenza.ToString("HH:mm:ss") == orario_partenza &&
                          p.orario_arrivo.ToString("HH:mm:ss") == orario_arrivo
                          select p.numero_volo;

              return query.Count() > 0 ? { numero_volo = Convert.ToInt32(query); true } : false;*/
            return false;
        }
    }
}
