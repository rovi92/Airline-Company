using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    class Error : InterfacciaError
    {
       ArrayList errorTxtBox = new ArrayList();//lista degli errori delle textbox
        ArrayList tipeError = new ArrayList();//lista dei tipi ti errori
        string result;
        #region se ci sono errori
        public Boolean checkText()//metodo per riempire una arrayList per sapere se ci sono errori
        {
                for(int i = 0;i < errorTxtBox.Count; i++){
                if (errorTxtBox[i].Equals(false)){
                    errorTxtBox.Clear();
                    return false;
                }  
            }
            return true;
        }
        #endregion

        #region campi vuoti
        public void ValueText(TextBox tx)// da errore se ci sono campi vuoti
        {
           
            if (tx.Text.Equals(""))
            {
                errorTxtBox.Add(false);
                tipeError.Add(1);
            }
            else
            {
                errorTxtBox.Add(true);
            }

        }
        #endregion campi vuoti

        #region valutazione psw
        public void valuePassword(PasswordBox ps)// da errore se la password è vuota
        {
            if (ps.Password.Equals(""))
            {
                errorTxtBox.Add(false);
                tipeError.Add(1);//richiamo dell'errore nello switch
            }
            else
            {
                errorTxtBox.Add(true);
            }
        }
        #endregion

        #region funzione per conferma password
        public void checkPs(PasswordBox ps1, PasswordBox ps2)//da errore se le password sono differenti
        {
            if (!ps1.Password.Equals(ps2.Password))
            {
                tipeError.Add(2);//richiamo dell'errore 
            }
            }
        #endregion

        #region funzione per lunghezza txtbox
        public void longTxt(TextBox txtB1, int numMax)
        {
            if (txtB1.Text.Length > numMax)
            {
                errorTxtBox.Add(false);
                tipeError.Add(3);
            }
        }
        #endregion

        #region funzione di messaggio di errore
        public string codError()
        {
            ArrayList tmp = new ArrayList();
            Boolean repetCaseEmpty = false;
            Boolean repetCasePs = false;
            Boolean repetCaseLong = false;
            

            for (int i = 0; i < tipeError.Count; i++)
            {
              
            int caseSwitch = Convert.ToInt32(tipeError[i]);
                switch (caseSwitch)
                {
                    case 1://messaggio di errore se è vuoto
                        if (repetCaseEmpty.Equals(false))
                        {
                            repetCaseEmpty = true;
                            result = " potrebbe esserci un campo non compilato "  ;
                            tmp.Add(result);
                        }
                        break;
                    case 2://messaggio di errore se la password è differente
                        if (repetCasePs.Equals(false)){
                            repetCasePs = true;
                            result = " password non valida ";
                            tmp.Add(result);
                            
                        }
                         break;
                    case 3://messaggio di errore se è troppo lungo un parametro
                        if (repetCaseLong.Equals(false))
                        {
                            repetCaseLong = true;
                            result = " lunghezza non valida in un campo ";
                            tmp.Add(result);
                        }
                        break;
                }
                
            }
            result = null;// da qui c'è il metodo ri stampa del messaggio 
            int iTmp = 0;
            for(int i = 0;i < tmp.Count; i++)
            {
                iTmp ++;
                if (i != tmp.Count-1)
                {
                  
                    result +=""+iTmp + tmp[i] + " , ";
                }
                else {
                    
                    result += ""+ iTmp +tmp[i];
                }
                
            }
            return result;

        }
        #endregion

    }
}
