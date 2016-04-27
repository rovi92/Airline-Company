using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

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
                for(int i = 0;i < errorTxtBox.Count; i++)
            {
                if (errorTxtBox[i].Equals(false))
                {
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

        #region controllo codice fiscale
        public void CfCheck(TextBox tx, int num)
        {
            if(tx.Text.Length != num)
            {
                errorTxtBox.Add(false);
                tipeError.Add(4);
            }
        }
        #endregion

        #region controllo CAP
        public void CAPCheck(TextBox tx)
        {
            /* try
             {
                 Int32.Parse(numero);
                 errorTxtBox.Add(true);
             }
             catch (FormatException)
             {
                 errorTxtBox.Add(false);
                 tipeError.Add(5);
             }

             if (tx.Text.Length != num)
             {
                 errorTxtBox.Add(false);
                 tipeError.Add(5);
             }*/
            /*int i = 0;
            Boolean result = int.TryParse(numero, out i); */
            
            if(Convert.ToInt32(tx.Text) == int.Parse(tx.Text) && tx.Text.Length == 5)
            {
                errorTxtBox.Add(true);
            } else
            {
                errorTxtBox.Add(false);
                tipeError.Add(5);
            }
        }
        #endregion

        #region checkNumber
        public void checkNumber(TextBox tx)
        {
            double value;
            if(double.TryParse(tx.Text, out value))
            {
                errorTxtBox.Add(true);
            }
            else
            {
                errorTxtBox.Add(false);
                tipeError.Add(7);

            }
        }
        #endregion

        #region checkEmail
        public void checkEmail(TextBox tx)
        {
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)­$");
            if (r.IsMatch(tx.Text))
            {
                errorTxtBox.Add(true);
            }
            else
            {
                errorTxtBox.Add(false);
                tipeError.Add(6);
            }
           
        }
        #endregion

        #region checkTimePicker
        public void checkString(String s)//Controlla se è stato scritto un orario attraverso il time picker
        {
            if(s == null)
            {
                errorTxtBox.Add(false);
                tipeError.Add(1);
            } else
            {
                errorTxtBox.Add(true);
               
            }
        }
        #endregion

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
                errorTxtBox.Add(false);
                tipeError.Add(2);//richiamo dell'errore 
            }
         }
        #endregion

        #region funzione per lunghezza massima txtbox
        public void longTxt(TextBox txtB1, int numMax)
        {
            if (txtB1.Text.Length > numMax)
            {
                errorTxtBox.Add(false);
                tipeError.Add(3);
            }
        }
        #endregion
      
        #region funzione per lunghezza minima
          public void shortTxt(TextBox txtB1, int numMin)
        {
            if(txtB1.Text.Length < numMin)
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
            Boolean repetCaseCf = false;
            Boolean repetCaseCAP = false;
            Boolean repetCaseEmail = false;
            Boolean repetCaseNum = false;
            

            for (int i = 0; i < tipeError.Count; i++)
            {
              
            int caseSwitch = Convert.ToInt32(tipeError[i]);
                switch (caseSwitch)
                {
                    case 1://messaggio di errore se è vuoto
                        if (repetCaseEmpty.Equals(false))
                        {
                            repetCaseEmpty = true;
                            result = " Compilare tutti i campi "  ;
                            tmp.Add(result);
                        }
                        break;
                    case 2://messaggio di errore se la password è differente
                        if (repetCasePs.Equals(false)){
                            repetCasePs = true;
                            result = " Password non corrispondente ";
                            tmp.Add(result);
                            
                        }
                         break;
                    case 3://messaggio di errore se è troppo lungo un parametro
                        if (repetCaseLong.Equals(false))
                        {
                            repetCaseLong = true;
                            result = " Lunghezza non valida in un campo ";
                            tmp.Add(result);
                        }
                        break;
                    case 4://messaggio di errore se il codice fiscale non è formato da 16 caratteri
                        if (repetCaseCf.Equals(false))
                        {
                            repetCaseCf = true;
                            result = " Il codice fiscale deve contenere 16 caratteri";
                            tmp.Add(result);
                        }
                        break;
                    case 5://messaggio di errore se il CAP non è formato da 5 numeri
                        if (repetCaseCAP.Equals(false))
                        {
                            repetCaseCAP = true;
                            result = " Il CAP deve contenere 5 numeri";
                            tmp.Add(result);
                        }
                        break;
                    case 6://messaggio di errore se la mail non è valida
                        if (repetCaseEmail.Equals(false))
                        {
                            repetCaseEmail = true;
                            result = " Email non valida";
                            tmp.Add(result);
                        }
                        break;
                    case 7://messaggio di errore se è stato inserito un carattere invece di un numero
                        if (repetCaseNum.Equals(false))
                        {
                            repetCaseNum = true;
                            result = " C'è almeno un campo che deve contenere solo numeri";
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

        #region pulire i campi dopo un risultato positivo
        //metodo di pulizia grid
        public void TraverseVisualTree(Visual myMainWindow)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for (int i = 0; i < childrenCount; i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is TextBox )
                {
                    TextBox tb = (TextBox)visualChild;
                    tb.Clear();
                 }else if(visualChild is PasswordBox)
                {
                    PasswordBox tb = (PasswordBox)visualChild;
                    tb.Clear();
                }
                else if (visualChild is DataGrid)
                {
                    DataGrid dg = (DataGrid)visualChild;
                    dg.ItemsSource = null;
                    dg.Items.Clear();
                    dg.Items.Refresh();
                }

                TraverseVisualTree(visualChild);
            }
        }
        #endregion

    }
}
