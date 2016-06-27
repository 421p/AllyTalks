using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace AllyTalksClient.View {
    /// <summary>
    ///     Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window {
        public StartPage()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "ShowMainWindow") {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }
}