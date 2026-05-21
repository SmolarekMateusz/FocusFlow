using FocusFlow.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FocusFlow.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddTaskView.xaml
    /// </summary>
    public partial class AddTaskView : MetroWindow
    {
        public AddTaskView()
        {

            InitializeComponent();
            this.DataContext = new AddTaskViewModel(this);
        }
    }
}
