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
            SetupWidgets();
        }

        private async void SetupWidgets()
        {
            var hubConnection = new HubConnection("http://localhost:56531/");
            var hubProxy = hubConnection.CreateHubProxy("widgetHub");
            var hubProxy2 = hubConnection.CreateHubProxy("chatHub");

            hubProxy.On<int>("updateWidgetCount",
                count =>
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action) (() =>
                    {
                        WidgetCountTextBlock.Text = count.ToString();
                    })));

            hubProxy2.On<string>("broadcastMessage",
                message =>
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        ChatHubText.Text += Environment.NewLine + message;
                    })));

            await hubConnection.Start();
        }
    }
}