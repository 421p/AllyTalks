using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace AllyTalksClient.View {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
       
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            //lstFriends.SelectedIndex = 0;
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "CleanMessagesArea")
            {
                System.Console.WriteLine("!!");
                lstMessages.Items.Clear();
            }
        }

        
    }
}