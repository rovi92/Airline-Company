using System;
using System.Collections;
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
                
                TraverseVisualTree(visualChild);
            }
        }
        #endregion

    }
}
