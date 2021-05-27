using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;

namespace EntityFrameworkExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EntityFrameworkExample.MusicDatabaseContainer _dbContainer;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _dbContainer = new MusicDatabaseContainer();
            _dbContainer.Tracks.Load();
            Grid.ItemsSource = _dbContainer.Tracks.Local;
            ReportViewer.Load += ReportViewer_Load;
        }


        private void ReportViewer_Load(object sender, EventArgs e)
        {
            ReportDataSource reportDataSource = new ReportDataSource();
            MusicDatabaseDataSet dataSet = new MusicDatabaseDataSet();
            //_Database_mdfDataSet dataset = new _Database_mdfDataSet();
            dataSet.BeginInit();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = dataSet.Tracks;
            ReportViewer.LocalReport.DataSources.Add(reportDataSource);
            ReportViewer.LocalReport.ReportPath = "../../Report.rdlc";
            dataSet.EndInit();

            MusicDatabaseDataSetTableAdapters.TracksTableAdapter tracksTableAdapter = new MusicDatabaseDataSetTableAdapters.TracksTableAdapter { ClearBeforeFill = true };
            tracksTableAdapter.Fill(dataSet.Tracks);
            ReportViewer.RefreshReport();

        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            _dbContainer.Tracks.Add(new Track
            {
                ArtistName = ArtistNameTxt.Text,
                TrackName = TrackNameTxt.Text

            }) ;
            _dbContainer.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    int id = (row.Item as Track).TrackId;
                    //SampleContext context = new SampleContext();

                    Track track = _dbContainer.Tracks
                        .Where(o => o.TrackId == id)
                        .FirstOrDefault();

                    _dbContainer.Tracks.Remove(track);
                    _dbContainer.SaveChanges();
                    tbStatus.Text = id+" was removed";
                    //row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }

        public void SavePDF(ReportViewer viewer, string savePath)

        {
            byte[] Bytes = viewer.LocalReport.Render(format: "PDF", deviceInfo: "");

            using (System.IO.FileStream stream = new System.IO.FileStream(savePath, System.IO.FileMode.Create))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }

        private void btnSavePDFPerort_Click(object sender, RoutedEventArgs e)
        {
            SavePDF(ReportViewer, "report.pdf");
        }
    }
}


