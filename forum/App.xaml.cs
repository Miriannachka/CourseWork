using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace forum
{
    public partial class App : Application
    {
        public Users CurrentUser { get; set; } // Свойство для хранения текущего пользователя

        // ...
    }
}
