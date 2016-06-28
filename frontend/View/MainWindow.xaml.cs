using System.Collections.ObjectModel;
using System.Windows;
using AllyTalksClient.Model.Message;
using GalaSoft.MvvmLight.Messaging;

namespace AllyTalksClient.View {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<ObservableCollection<Message>>(this, ReceiveMessage);
        }

        private void ReceiveMessage(ObservableCollection<Message> obj)
        {
            lstMessages.ItemsSource = obj;
        }
    }
}