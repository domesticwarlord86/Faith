using System.Windows;

namespace Faith.Windows
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
