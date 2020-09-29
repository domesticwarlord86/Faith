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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RB.Faith.Windows
{
    /// <summary>
    /// Interaction logic for BotbaseWindow.xaml
    /// </summary>
    public partial class BotbaseWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotbaseWindow"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel-style DataContext for this window.</param>
        public BotbaseWindow(BotbaseWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
