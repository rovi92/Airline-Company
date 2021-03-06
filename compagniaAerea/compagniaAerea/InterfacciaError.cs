﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    interface InterfacciaError
    {
        //funzione
        Boolean checkText();
        void ValueText(TextBox tx);
        void valuePassword(PasswordBox ps);
        void CfCheck(TextBox txtB1, int num);
        void CAPCheck(TextBox txtB1);
        void checkEmail(TextBox txtB1);
        void checkString(String s);
        void checkNumber(TextBox txtB1);
        void checkPs(PasswordBox ps1, PasswordBox ps2);
        void longTxt(TextBox txtB1, int numMax);
        void shortTxt(TextBox txtB1, int numMin);
        void TraverseVisualTree(System.Windows.Media.Visual myMainWindow);
        String codError();
        
    }
}
