using System.Windows;

namespace Faith.Windows
{
    /// <summary>
    /// Interaction logic for BotbaseWindow.xaml
    /// </summary>
    public partial class BotBaseWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotBaseWindow"/> class.
        /// </summary>
        /// <param name="viewModel">ViewModel-style DataContext for this window.</param>
        public BotBaseWindow(BotBaseWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
