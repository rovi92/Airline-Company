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
        void ValueText(TextBox tx);
        Boolean checkText();
        void valuePassword(PasswordBox ps);
        void checkPs(PasswordBox ps1, PasswordBox ps2);
        String codError();
        
    }
}
