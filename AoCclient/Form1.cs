using System.Text;
using System.Text.Json;

namespace AoCclient
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:8080";

        public Form1()
        {
            _httpClient = new();
            InitializeComponent();
            LoadDays();
        }

        public async void LoadDays()
        {
            var json = await _httpClient.GetAsync(_url + "/supporteddays");
            if (json.IsSuccessStatusCode)
            {
                List<int> days = JsonSerializer.Deserialize<List<int>>(json.Content.ReadAsStream())!;
                if (days == null || days.Count == 0)
                    return;
                foreach (int i in days)
                {
                    comboBox1.Items.Add("Day " + i);
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        public async void getResponse(string text)
        {
            try
            {
                AocRequest request = new AocRequest(comboBox1.SelectedIndex + 1, radioButton1.Checked ? 1 : 2, new List<string>(text.Split("\n")));
                var json = JsonSerializer.Serialize(request);
                var response = await _httpClient.PostAsync(_url + "/answer", new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                    answerTextBox.Text = await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                //no one cares
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getResponse(testDataBox.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getResponse(realDataBox.Text);
        }
    }
}