using Microsoft.AspNetCore.SignalR.Client;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        HubConnection connection; //global bir HubConnection tanýmlýyoruz
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; // listBox1 de çakýþmasýný engellemek için kody ekliyoruz
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7065/myHub") // bu url ile baðlanýyoruz
                .WithAutomaticReconnect() // baðlantý kesildiðinde otomatik olarak 0,2,10,30 saniyede 4 periyodik þekilde tekrar baðlanmayý deniyoruz
                .Build();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // SendMessageAsync metoduna textBox1 deki yazýyý gönderiyoruz

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
            //receiveMessage dinleme yaparak mesajý alýyoruz
            connection.On<string>("receiveMessage", (message) =>
            {
                var newMessage = message;
                listBox1.Items.Add(newMessage);
            });
            try
            {
                //connection baðlantýsýný baþlatýyoruz
                await connection.StartAsync();
                listBox1.Items.Add("Baðlandý");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
    }
}