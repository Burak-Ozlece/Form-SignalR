using Microsoft.AspNetCore.SignalR.Client;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        HubConnection connection; //global bir HubConnection tan�ml�yoruz
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; // listBox1 de �ak��mas�n� engellemek i�in kody ekliyoruz
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7065/myHub") // bu url ile ba�lan�yoruz
                .WithAutomaticReconnect() // ba�lant� kesildi�inde otomatik olarak 0,2,10,30 saniyede 4 periyodik �ekilde tekrar ba�lanmay� deniyoruz
                .Build();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // SendMessageAsync metoduna textBox1 deki yaz�y� g�nderiyoruz

                await connection.InvokeAsync("SendMessageAsync", textBox1.Text);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
                throw;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //receiveMessage dinleme yaparak mesaj� al�yoruz
            connection.On<string>("receiveMessage", (message) =>
            {
                var newMessage = message;
                listBox1.Items.Add(newMessage);
            });
            try
            {
                //connection ba�lant�s�n� ba�lat�yoruz
                await connection.StartAsync();
                listBox1.Items.Add("Ba�land�");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
    }
}