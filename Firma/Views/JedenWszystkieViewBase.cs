using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Firma.Views
{
    public class JedenWszystkieViewBase : UserControl
    {
        static JedenWszystkieViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(JedenWszystkieViewBase), new FrameworkPropertyMetadata(typeof(JedenWszystkieViewBase)));
        }
    }
}
