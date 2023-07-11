using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using weight_recording_dal;

namespace weight_recording_ui.reports
{
    public partial class chartform : Form
    {
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public chartform(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished chartform initialization", TAG));

        }

        private void chartform_Load(object sender, EventArgs e)
        {
            weightchart.Invalidate();

            Dictionary<string, string> chart_types = new Dictionary<string, string>();
            chart_types.Add("Line", "Line");
            chart_types.Add("Column", "Column");
            chart_types.Add("Point", "Point");

            cbocharttype.DisplayMember = "Value";
            cbocharttype.ValueMember = "Key";
            //datasource should be last to be set
            cbocharttype.DataSource = chart_types.ToList();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished chartform load", TAG));

        }
        private void btnloadchart_Click(object sender, EventArgs e)
        {
            btnloadchart.Text = "loading chart data....";

            string chart_type = cbocharttype.SelectedValue.ToString();
            Console.WriteLine(chart_type);

            //load_chart_data_with_random_data();

            weightchart.Invoke(new Action(() =>
            {
                load_chart_data_from_service(chart_type);
            }));

            btnloadchart.Text = "load chart";
        }

        private void load_chart_data_from_service(string chart_type)
        {
            try
            {
                weightchart.Titles.Clear();
                weightchart.Legends.Clear();
                weightchart.ChartAreas.Clear();
                weightchart.Series.Clear();

                weightchart.Invalidate();

                List<weight_ui_dto> lst_weights = dalutilz.getallweightsforui();
                lst_weights.Reverse();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + lst_weights.Count.ToString() + " ] records for printing...", TAG));

                weightchart.Titles.Add("weight chart");
                weightchart.Titles[0].Font = new Font("Arial", 15, FontStyle.Bold);

                var chart_legend = new Legend
                {
                    Name = "weight",
                    Alignment = StringAlignment.Center,
                    Docking = Docking.Top
                };

                weightchart.Legends.Add(chart_legend);

                var chart_area = new ChartArea
                {
                    Name = "weight area",
                    BorderColor = Color.Lavender
                };

                this.weightchart.ChartAreas.Add(chart_area);

                var chart_series = new Series
                {
                    Name = "weight series",
                    Color = Color.Green,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    Legend = "weight",
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 20,
                    ChartType = SeriesChartType.Line
                };

                switch (chart_type)
                {
                    case "Line":
                        chart_series.ChartType = SeriesChartType.Line;
                        break;
                    case "Column":
                        chart_series.ChartType = SeriesChartType.Column;
                        break;
                    case "Point":
                        chart_series.ChartType = SeriesChartType.Point;
                        break;
                }

                this.weightchart.Series.Add(chart_series);

                int count = lst_weights.Count;

                string[] months_fullname = new string[] {"January", "February", "March", "April", "May",
  "June", "July", "August", "September", "October", "November", "December"};

                string[] months_abbrev = new string[] {"Jan", "Feb", "Mar", "Apr", "May",
  "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

                for (int i = 0; i < count; i++)
                {
                    double weight = double.Parse(lst_weights[i].weight_weight);
                    double month = double.Parse(DateTime.Parse(lst_weights[i].weight_date).Month.ToString());

                    chart_series.Points.AddXY(month, weight);

                    chart_series.Points[i].Label = weight.ToString();

                    string year = DateTime.Parse(lst_weights[i].weight_date).Year.ToString();
                    string month_name = months_abbrev[DateTime.Parse(lst_weights[i].weight_date).Month - 1];

                    chart_series.Points[i].AxisLabel = month_name + "-" + year;
                    chart_series.Points[i].ToolTip = month_name + "-" + year;

                    if (weight < 50)
                    {
                        chart_series.Points[i].Color = Color.DarkRed;
                    }
                    else if (weight > 100)
                    {
                        chart_series.Points[i].Color = Color.DarkRed;
                    }
                    else
                    {
                        chart_series.Points[i].Color = Color.DarkGreen;
                    }

                }

                weightchart.Invalidate();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("created chart report sucessfully...", TAG));

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        private double f(int i)
        {
            var f1 = 59894 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
            return f1;
        }
        private void load_chart_data_with_random_data()
        {
            weightchart.Series.Clear();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series1",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            this.weightchart.Series.Add(series1);

            for (int i = 0; i < 100; i++)
            {
                series1.Points.AddXY(i, f(i));
            }
            weightchart.Invalidate();
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbocharttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chart_type = cbocharttype.SelectedValue;
            Console.WriteLine(chart_type);
        }




    }
}
