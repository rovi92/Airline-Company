using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    class DipendenteVoloImpl : DipendenteVolo
    {
        DatabaseManager myDatabase;
        List<Piano_di_volo> piano_di_volo = new List<Piano_di_volo>();
        int lastNumero_volo = 0;


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
        public void Aggiungi_pianodivolo(String città_partenza, String città_arrivo, DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo, Boolean cancellato, double tariffa)
        {
            /*TimeSpan orario_p = TimeSpan.Parse(DateTime.Parse(orario_partenza, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss"));
            TimeSpan orario_a = TimeSpan.Parse(DateTime.Parse(orario_arrivo, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss"));*/
            Piano_di_volo p = new Piano_di_volo()
            {
                aeroporto_partenza = myDatabase.getDb().Aeroporto.First(a => a.città.ToString() == città_partenza).nome,
                aeroporto_arrivo = myDatabase.getDb().Aeroporto.First(a => a.città.ToString() == città_arrivo).nome,
                data_partenza = data_partenza,
                data_arrivo = data_arrivo,
                orario_partenza = TimeSpan.ParseExact(orario_partenza, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                orario_arrivo = TimeSpan.ParseExact(orario_arrivo, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                cancellato = cancellato,
                idPromozione = null
            };
            myDatabase.getDb().Piano_di_volo.InsertOnSubmit(p);
            myDatabase.getDb().SubmitChanges();
            this.lastNumero_volo = p.numero_volo;
            CreateFlightFare(p.numero_volo, tariffa);
            UpdatePianodivolo();
        }

        //Aggiungi tratta
        public void Aggiungi_tratta(String città_partenza, String città_arrivo, int gate_partenza, int gate_arrivo, DateTime data_partenza, DateTime data_arrivo, String orario_partenza, String orario_arrivo, String[] aereo, int numero_volo)
        {
            String orario_p = DateTime.Parse(orario_partenza, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss");
            String orario_a = DateTime.Parse(orario_arrivo, System.Globalization.CultureInfo.CurrentCulture).ToString("HH:mm:ss");
            Tratta t = new Tratta()
            {
                aeroporto_partenza = myDatabase.getDb().Aeroporto.First(a => a.città.ToString() == città_partenza).nome,
                aeroporto_arrivo = myDatabase.getDb().Aeroporto.First(a => a.città.ToString() == città_arrivo).nome,
                gate_partenza = gate_partenza,
                gate_arrivo = gate_arrivo,
                data_partenza = Convert.ToDateTime(data_partenza).ToLocalTime(),
                data_arrivo = Convert.ToDateTime(data_arrivo).ToLocalTime(),
                orario_partenza = TimeSpan.ParseExact(orario_partenza, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                orario_arrivo = TimeSpan.ParseExact(orario_arrivo, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                posti_economy = 0,
                posti_buisness = 0,
                posti_first = 0,
                numero_volo = numero_volo,
                nome = aereo[0],
                modello = aereo[1]
            };
            myDatabase.getDb().Tratta.InsertOnSubmit(t);
            myDatabase.getDb().SubmitChanges();


        }

        //Controlla se il piano di volo è già inserito nel db
        public Boolean checkPianodivoloExist(String città_partenza, String città_arrivo, String data_partenza, String data_arrivo, String orario_partenza, String orario_arrivo)
        {

            return (from p in piano_di_volo
                    where p.Aeroporto.città.ToString() == città_partenza &&
                    p.Aeroporto1.città.ToString() == città_arrivo &&
                    p.data_partenza.ToString("yyyy-MM-dd") == data_partenza &&
                    p.data_arrivo.ToString("yyyy-MM-dd") == data_arrivo &&
                    p.orario_arrivo.ToString("hh':'mm':'ss") == orario_arrivo &&
                    p.orario_partenza.ToString("hh':'mm':'ss") == orario_partenza
                    select p).Count() > 0 ? true : false;

        }

        //Restituisce il numero del volo
        public int getNumeroVolo(String città_partenza, String città_arrivo, String data_partenza, String data_arrivo, String orario_partenza, String orario_arrivo)
        {

            return (from p in piano_di_volo
                    where p.Aeroporto.città.ToString() == città_partenza &&
                    p.Aeroporto1.città.ToString() == città_arrivo &&
                    p.data_partenza.ToString("yyyy-MM-dd") == data_partenza &&
                    p.data_arrivo.ToString("yyyy-MM-dd") == data_arrivo &&
                    p.orario_partenza.ToString("hh':'mm':'ss") == orario_partenza &&
                    p.orario_arrivo.ToString("hh':'mm':'ss") == orario_arrivo
                    select p.numero_volo).First();
        }

        //Aggiungi tariffa
        public void CreateFlightFare(int numero_volo, double tariffa)
        {
            for (int i = 1; i <= 3; i++)
            {
                Tariffario t = new Tariffario()
                {
                    numero_volo = numero_volo,
                    tariffa_solo_andata = tariffa,
                    idClasse = i
                };

                myDatabase.getDb().Tariffario.InsertOnSubmit(t);
            }
            myDatabase.getDb().SubmitChanges();
        }


        public dynamic getPianiDiVolo()
        {
            var result = (from p in myDatabase.getDb().Piano_di_volo
                          select p);
            return result;
        }

        public Boolean checkAeroporto (String aeroporto)
        {
            return (from a in myDatabase.getDb().Aeroporto
                    where a.città.ToString() == aeroporto
                    select a).Count() > 0 ? true : false;
        }


        public class InfoAereo
        {
            public string descrizione { get; set; }

        }

        public List<String> getAerei()
        {
            return (from a in myDatabase.getDb().Aereo
                    select (a.nome + " " + a.modello)).ToList();

        }

        public int getLastNumero_volo()
        {
            return this.lastNumero_volo;
        }

        public Boolean CombineEmplyerToFlight(int idPersonale, DateTime dataPartenza, TimeSpan oraPartenza, int gatePartenza)
        {
            if ((from v in myDatabase.getDb().Volo_attuale
                 where v.idPersonale == idPersonale && v.data_partenza == dataPartenza && v.orario_partenza == oraPartenza && v.gate_partenza == gatePartenza
                 select v).Count() > 0)
            {
                return false;
            }
            else
            {


                Volo_attuale va = new Volo_attuale()
                {
                    orario_partenza = oraPartenza,
                    data_partenza = dataPartenza,
                    gate_partenza = gatePartenza,
                    idPersonale = idPersonale
                };
                myDatabase.getDb().Volo_attuale.InsertOnSubmit(va);
                myDatabase.getDb().SubmitChanges();
                return true;
            }
        }
        public List<Personale> getEmployersInFly(DateTime dataPartenza, TimeSpan oraPartenza, int gatePartenza)
        {
            return (from v in myDatabase.getDb().Volo_attuale
                    where v.data_partenza == dataPartenza && v.orario_partenza == oraPartenza && v.gate_partenza == gatePartenza
                    select new Personale
                    {
                        nome = v.Personale.nome,
                        cognome = v.Personale.cognome,
                        idPersonale = v.Personale.idPersonale,
                        indirizzo = v.Personale.indirizzo,
                        data_di_nascita = v.Personale.data_di_nascita,
                        data_assunzione = v.Personale.data_assunzione,
                        email = v.Personale.email,
                        telefono = v.Personale.telefono,
                        sesso = v.Personale.sesso,
                        pilota = v.Personale.pilota,
                        hostess = v.Personale.hostess
                    }).ToList();
        }
    }
}

