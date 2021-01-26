﻿using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

//Пример использования CancellationToken

namespace DataParallelismWithForEach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // New Window level variable.
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        //Отправляет токену CancellationToken сигнал отмены.

        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelToken.Cancel();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            // Start a new "task" to process the files. 
            Task.Factory.StartNew(() => ProcessFiles());
        }

        private void ProcessFiles()
        {
            // Use ParallelOptions instance to store the CancellationToken
            ParallelOptions parOpts = new ParallelOptions
            {
                CancellationToken = cancelToken.Token,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@"Pictures", "*.jpg",
                SearchOption.AllDirectories);
            string newDir = @"ModifiedPictures";
            Directory.CreateDirectory(newDir);

            try
            {
                //  Process the image data in a parallel manner! 
                Parallel.ForEach(files, parOpts, currentFile =>
                    {
                        parOpts.CancellationToken.ThrowIfCancellationRequested();

                        string filename = Path.GetFileName(currentFile);
                        using (Bitmap bitmap = new Bitmap(currentFile))
                        {
                            bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            bitmap.Save(Path.Combine(newDir, filename));

                            //this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";

                            // We need to ensure that the secondary threads access controls
                            // created on primary thread in a safe manner.
                            this.Dispatcher.Invoke((Action) delegate
                            { 
                                this.Title =
                                    $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                            });
                        }
                    }
                );
                this.Dispatcher.Invoke((Action) delegate { this.Title = "Done!"; });
            }
            catch (OperationCanceledException ex)
            {
                this.Dispatcher.Invoke((Action) delegate { this.Title = ex.Message; });
                cancelToken= new CancellationTokenSource();//если мы хотим потом запустить, нужно пересоздать токен
            }
        }
    }
}