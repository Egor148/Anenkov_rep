using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4_testirov
{
    public partial class Form1 : Form
    {
        double c1, c2;
        Valute val = new Valute();
        public Form1()
        {
            InitializeComponent();
            //объект класса Valute
            monthCalendar1.DateChanged += monthCalendar1_DateSelected;
            for (int i = 0; i < val.name.Length; i++)
            {
                comboBox1.Items.Add(val.name[i]);
                comboBox2.Items.Add(val.name[i]);
                comboBox3.Items.Add(val.name[i]);
            }
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            textBox1.Text = "1";
            textBox4.Text = "1";

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox2.SelectedItem.ToString();
            if (selectedState != "RUB")
            {
                string val = get_kurs(selectedState);
                c2 = Convert.ToDouble(val);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            if (selectedState != "RUB")
            {
                string val = get_kurs(selectedState);
                c1 = Convert.ToDouble(val);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string get_kurs(string name)
        {

            string url = "http://www.cbr.ru/scripts/XML_daily.asp";
            //string url = "https://tursportopt.ru/category/rybolovnye-tovary-optom/";
            //XmlDocument xml_doc = new XmlDocument();
            // xml_doc.Load(url);
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Valute"];
            foreach (DataRow row in currency.Rows)
            {
                if (row["CharCode"].ToString() == name)//Ищу нужный код валюты
                {
                    return row["Value"].ToString(); //Возвращаю значение курсы валюты
                }
            }
            return "";
        }
        public void get_history_kurs(string date1, string date2, string id)
        {
            string url = "https://cbr.ru/scripts/XML_dynamic.asp?date_req1=" + date1 + "&date_req2=" + date2 + "&VAL_NM_RQ=" + id;
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Record"];

            foreach (DataRow row in currency.Rows)
            {
                val.his.Add(row["Date"].ToString());
                val.his.Add(row["Value"].ToString());
            }
        }

        public double read_kurs(double c1, double c2, double n)
        {
            double res;
            res = ((c1 / c2) * n);
            return res;
        }
        private void kurs() //отношение курсов
        {
            string selectedState1 = comboBox1.SelectedItem.ToString();
            string selectedState2 = comboBox2.SelectedItem.ToString();
            if (selectedState1 != "RUB" && selectedState2 != "RUB")
            {
                string selected = get_box1();
                string val = get_kurs(selected);
                c1 = Convert.ToDouble(val); // наше значение 1 колонки

                selected = get_box2();
                val = get_kurs(selected);
                c2 = Convert.ToDouble(val); // значение 2 колонки

                string nn = textBox1.Text;
                double n = Convert.ToDouble(nn);
                double res = read_kurs(c1, c2, n);
                textBox4.Text = Convert.ToString(res);
            }
            else
            {
                string selected1 = get_box1();
                string selected2 = get_box2();

                if (selected1 == "RUB") // если первая колонка рубль
                {
                    string val2 = get_kurs(selected2);
                    c2 = Convert.ToDouble(val2); // значение 2 колонки
                    textBox4.Text = "1";
                    string nn = textBox1.Text;
                    double n = Convert.ToDouble(nn);
                    textBox1.Text = Convert.ToString(c2);
                }
                else if (selected2 == "RUB")
                {
                    string val1 = get_kurs(selected1);
                    c1 = Convert.ToDouble(val1); // значение 2 колонки
                    textBox1.Text = "1";
                    string nn = textBox4.Text;
                    double n = Convert.ToDouble(nn);
                    textBox4.Text = Convert.ToString(c1);
                }
            }
        }
        public double get_text1()
        {
            string s = textBox1.Text;
            return Convert.ToDouble(s);
        }
        public double get_text2()
        {
            string s = textBox1.Text;
            return Convert.ToDouble(s);
        }

        public string get_box1()//выбор comboBox1
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            return selectedState;
        }
        public string get_box2()//выбор comboBox2
        {
            string selectedState = comboBox2.SelectedItem.ToString();
            return selectedState;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kurs();//вызов функции для расчета отношения валют
        }
        private void name_valute1(object sender, EventArgs e)
        {//"EUR", "RUB", "BYN", "INR", "KZT", "CAD", "CNY", "UZS"
            switch (get_box1())
            {
                case "USD":
                    label4.Text = "Доллар США";
                    break;
                case "EUR":
                    label4.Text = "Евро";
                    break;
                case "RUB":
                    label4.Text = "Рубль";
                    break;
                case "BYN":
                    label4.Text = "Белорусский рубль";
                    break;
                case "INR":
                    label4.Text = "Рупий";
                    break;
                case "KZT":
                    label4.Text = "Тенге";
                    break;
                case "CAD":
                    label4.Text = "Доллар Канада";
                    break;
                case "CNY":
                    label4.Text = "Юань";
                    break;
                case "UZS":
                    label4.Text = "Узб. Сумы";
                    break;
            }
        }
        private void name_valute2(object sender, EventArgs e)
        {
            switch (get_box2())
            {
                case "USD":
                    label5.Text = "Доллар США";
                    break;
                case "EUR":
                    label5.Text = ("Евро");
                    break;
                case "RUB":
                    label5.Text = "Рубль";
                    break;
                case "BYN":
                    label5.Text = "Белорусский рубль";
                    break;
                case "INR":
                    label5.Text = "Рупий";
                    break;
                case "KZT":
                    label5.Text = "Тенге";
                    break;
                case "CAD":
                    label5.Text = "Доллар Канада";
                    break;
                case "CNY":
                    label5.Text = "Юань";
                    break;
                case "UZS":
                    label5.Text = "Узб. Сумы";
                    break;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.MaxSelectionCount = 1;
            // Это возвратит только дату без времени
            textBox3.Text += get_kalend();
        }
        private string get_kalend()
        {
            string s;
            s = String.Format("{0}", this.monthCalendar1.SelectionRange.Start.ToShortDateString() + " ");
            return s;
        }
        public void Replace()
        {
            for (int i = 0; i < val.date.Count; i++)
            {
                string s = val.date[i];

                s = s.Replace(".", "/");
                val.date[i] = s;
            }
            for (int i = 0; i < val.his.Count; i++)
            {
                string s = val.his[i];

                s = s.Replace(".", "/");
                val.his[i] = s;
            }
        }
        public void his_Replace()
        {
            for (int i = 0; i < val.his.Count; i++)
            {
                string s = val.his[i];

                s = s.Replace(".", "/");
                val.his[i] = s;
            }
        }
        public string get_box3() // валюта для истории курса валюты
        {
            string selectedState = comboBox3.SelectedItem.ToString();
            return selectedState;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            his_Replace();//передаем список
            double d1, d2, price1 = 0, price2 = 0;
            string s = val.date[0].Substring(0, val.date[0].IndexOf('/'));//обрезаем только число из даты
            d1 = Convert.ToDouble(s);
            string s2 = val.date[1].Substring(0, val.date[1].IndexOf('/'));//обрезаем только число из даты
            d2 = Convert.ToDouble(s2);
            string pr1, pr2;
            //поиск цены в his[i]
            for (int i = 0; i < val.his.Count; i += 2)
            {
                if (s.Equals(val.his[i].Substring(0, (val.his[i]).IndexOf('/'))))
                {
                    pr1 = val.his[i + 1];
                    price1 = Convert.ToDouble(pr1);
                }
                else if (s2.Equals(val.his[i].Substring(0, (val.his[i]).IndexOf('/'))))
                {
                    pr2 = val.his[i + 1];
                    price2 = Convert.ToDouble(pr2);
                }
            }

            double[,] d = { { d1, price1 }, { d2, price2 } }; //воткнуть даты придумать как считать разницу в функции
            //взять цену из функции get_kurs_history добавить еще один аргумент bool.
            // если выполняем экстраполяцию то type(bool) = true
            // и там в обработке ищем дату и возвращаем цену, подставляем цену и делаем экстраполяцию
            // или найти цену в his[i]
            string x = textBox3.Text;
            s = x.Substring(0, x.IndexOf('.'));//обрезаем только число из даты
            double xx = Convert.ToDouble(s); //нужная дата
            textBox2.Clear();
            textBox2.AppendText("Экстраполяция на " + xx + " число: " + extrapolate(d, xx));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            Replace();//функция убирающая точки из даты и вставляющая /
            switch (get_box3())
            {
                case "USD":
                    val.id = "R01235";
                    break;
                case "EUR":
                    val.id = "R01239";
                    break;
                case "RUB":
                    break;
                case "BYN":
                    val.id = "R01090B";
                    break;
                case "INR":
                    val.id = "R01270";
                    break;
                case "KZT":
                    val.id = "R01335";
                    break;
                case "CAD":
                    val.id = "R01350";
                    break;
                case "CNY":
                    val.id = "R01375";
                    break;
                case "UZS":
                    val.id = "R01717";
                    break;
            }
            //switch case с названием валюты и кодом
            get_history_kurs(val.date[0], val.date[1], val.id);
            for (int i = 0; i < val.his.Count; i++)
            {
                if (val.his[i] == "") break;
                textBox2.AppendText(val.his[i] + Environment.NewLine);
            }
        }



        public double extrapolate(double[,] d, double x)
        {
            double y = d[0, 1] + (x - d[0, 0]) / (d[1, 0] - d[0, 0]) * (d[1, 1] - d[0, 1]);
            return y;
        }

    }
}
