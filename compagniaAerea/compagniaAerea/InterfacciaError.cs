using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace compagniaAerea
{
    interface InterfacciaError
    {
        
        Boolean checkText();
        void ValueText(TextBox tx);
        void valuePassword(PasswordBox ps);
        void checkPs(PasswordBox ps1, PasswordBox ps2);
        void longTxt(TextBox txtB1, int numMax);
        String codError();
        
    }
}
