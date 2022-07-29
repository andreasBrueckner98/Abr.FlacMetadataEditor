using System.Windows;

namespace Abr.FlacMetadataEditor.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors
        
        /// <summary>
        /// Initializes new Instance of <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion
    }
}