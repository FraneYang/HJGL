using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace Web.Controls
{
    public partial class ChartControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// 创建Chart图形
        /// </summary>
        /// <param name="dataSourceChart">Chart类</param>
        public void CreateChart(Model.DataSourceChart dataSourceChart)
        {
            Chart chart1 = new Chart();
            chart1.ID = "chart1";
            chart1.BackColor = Color.WhiteSmoke;
            chart1.ImageLocation = "~/Images/ChartPic_#SEQ(300,3)";
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.Palette = ChartColorPalette.BrightPastel;
            chart1.BackSecondaryColor = Color.White;
            chart1.BackGradientStyle = GradientStyle.TopBottom;
            chart1.BorderWidth = 2;
            chart1.BorderColor = Color.FromArgb(26, 59, 105);
            chart1.ImageType = ChartImageType.Png;

            chart1.Width = dataSourceChart.Width;
            chart1.Height = dataSourceChart.Height;

            Title title = new Title();
            title.Text = dataSourceChart.Title;
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);
            chart1.Titles.Add(title);

            Legend legend = new Legend();
            legend.Name = dataSourceChart.Title;
            legend.TextWrapThreshold = 1;
            legend.Docking = Docking.Top;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font(new FontFamily("Trebuchet MS"), 9);
            legend.LegendStyle = LegendStyle.Row;
            legend.IsEquallySpacedItems = true;
            legend.IsTextAutoFit = false;
            chart1.Legends.Add(legend);

            ChartArea chartArea = new ChartArea();
            chartArea.Name = dataSourceChart.Title;
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            chartArea.Area3DStyle.Enable3D = dataSourceChart.IsNotEnable3D;
            chart1.ChartAreas.Add(chartArea);

            if (dataSourceChart.ChartType == SeriesChartType.Pie)
            {
                foreach (Model.DataSourceTeam dataSourceTeam in dataSourceChart.DataSourceTeams)
                {
                    chart1.Series.Add(dataSourceTeam.DataPointName);
                    chart1.Series[dataSourceTeam.DataPointName].ChartType = dataSourceChart.ChartType;
                    chart1.Series[dataSourceTeam.DataPointName].Name = dataSourceTeam.DataPointName;
                    chart1.Series[dataSourceTeam.DataPointName].IsValueShownAsLabel = true;
                    chart1.Series[dataSourceTeam.DataPointName].BorderWidth = 2;
                    chart1.Series[dataSourceTeam.DataPointName].Label = "#PERCENT{P1}";
                    chart1.Series[dataSourceTeam.DataPointName]["DrawingStyle"] = "Cylinder";
                    int m = 0;
                    foreach (Model.DataSourcePoint dataSourcePoint in dataSourceTeam.DataSourcePoints)
                    {
                        chart1.Series[dataSourceTeam.DataPointName].Points.AddXY(dataSourcePoint.PointText, dataSourcePoint.PointValue);
                        chart1.Series[dataSourceTeam.DataPointName].Points[m].LegendText = dataSourcePoint.PointText + "#PERCENT{P1}";
                        m++;
                    }
                }
            }
            else
            {
                foreach (Model.DataSourceTeam dataSourceTeam in dataSourceChart.DataSourceTeams)
                {
                    chart1.Series.Add(dataSourceTeam.DataPointName);
                    chart1.Series[dataSourceTeam.DataPointName].ChartType = dataSourceChart.ChartType;
                    chart1.Series[dataSourceTeam.DataPointName].Name = dataSourceTeam.DataPointName;
                    chart1.Series[dataSourceTeam.DataPointName].IsValueShownAsLabel = true;
                    chart1.Series[dataSourceTeam.DataPointName].BorderWidth = 2;
                    chart1.Series[dataSourceTeam.DataPointName]["DrawingStyle"] = "Cylinder";
                    foreach (Model.DataSourcePoint dataSourcePoint in dataSourceTeam.DataSourcePoints)
                    {
                        chart1.Series[dataSourceTeam.DataPointName].Points.AddXY(dataSourcePoint.PointText, dataSourcePoint.PointValue);
                    }
                }
            }
            Controls.Add(chart1);
        }
    }
}