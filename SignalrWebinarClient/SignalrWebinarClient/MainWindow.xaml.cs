using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;

namespace SignalrWebinarClient
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupWidget();
        }

        private async void SetupWidget()
        {
            var hubConnection = new HubConnection("http://localhost:56531/");
            var hubProxy = hubConnection.CreateHubProxy("widgetHub");

            hubProxy.On<int>("updateWidgetCount",
                count =>
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action) (() =>
                    {
                        WidgetCountTextBlock.Text = count.ToString();
                    })));

            await hubConnection.Start();
        }
    }
}