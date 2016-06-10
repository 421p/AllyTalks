using System;
using System.Windows;
using System.Windows.Threading;
using WebSocketSharp;

namespace AllyTalksClient {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly WebSocket _ws;

        public MainWindow()
        {
            InitializeComponent();
            _ws = new WebSocket("ws://127.0.0.1:7777");

            _ws.OnMessage += (sender, e) => DispatchIt(() => listBox.Items.Add(e.Data));
        }

        private void DispatchIt(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, action);
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            _ws.Send($"{nameTextBox.Text}: {textBox.Text}");
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ws.Connect();
        }
    }
}