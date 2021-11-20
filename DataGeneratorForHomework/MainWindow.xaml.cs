using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace DataGeneratorForHomework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model.DataGenerator dataGenerator = new Model.DataGenerator();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = dataGenerator;
        }

        //private void btnStart_Click(object sender, RoutedEventArgs e)
        //{
        //    //dataGenerator.Generate();
        //    for (int i = 0; i < dataGenerator.Count; i++)
        //    {
        //        string filename = System.IO.Path.GetDirectoryName(dataGenerator.TemplateFilename) +  System.IO.Path.GetFileNameWithoutExtension(dataGenerator.TemplateFilename) + i + System.IO.Path.GetExtension(dataGenerator.TemplateFilename);
        //        StreamWriter sw = new StreamWriter(filename);
        //        sw.WriteLine("{0} {1} {2}", dataGenerator.Random.Next(1, 3), dataGenerator.Random.NextDouble(), dataGenerator.Random.NextDouble());
        //        sw.Close();
        //    }
        //    tbStatus.Text = "Done";
        //}

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //dataGenerator.Generate();
            //int i = 0;
            tbStatus.Text = "Working";
            await Task.Factory.StartNew(() => {
            Parallel.For(0, dataGenerator.Count, (index) =>
             {
                 try
                 {
                         //Console.WriteLine(obj);
                         string filename = System.IO.Path.GetDirectoryName(dataGenerator.TemplateFilename) + System.IO.Path.GetFileNameWithoutExtension(dataGenerator.TemplateFilename) + index + System.IO.Path.GetExtension(dataGenerator.TemplateFilename);
                     StreamWriter sw = new StreamWriter(filename);
                     sw.WriteLine("{0} {1} {2}", dataGenerator.Random.Next(1, 3), dataGenerator.Random.NextDouble(), dataGenerator.Random.NextDouble());
                     Thread.Sleep(100);//Чтобы увидеть изменения текста
                     sw.Close();
                         //tbStatus.Text = filename;//Этот код работать не будет! Так как произведена попытка доступа к элементу созданному в другом потоке
                         this.Dispatcher.Invoke((Action)delegate
                          {
                              tbStatus.Text = filename;
                              Console.WriteLine(filename);
                          });

                 }
                 catch (Exception exc)
                 {
                     Console.WriteLine(exc.Message);
                 }
             }
            );

            this.Dispatcher.Invoke(() => tbStatus.Text = "Done");
                /*
В WPF только поток, создавший объект, DispatcherObject может обращаться к этому объекту. 
Например, фоновый поток, который отключается из основного потока пользовательского интерфейса, не может обновить содержимое объекта Button , 
созданного в потоке пользовательского интерфейса. Чтобы фоновый поток мог получить доступ к свойству Content объекта Button , фоновый поток должен делегировать работу, 
Dispatcher связанную с потоком пользовательского интерфейса. Это достигается с помощью Invoke или BeginInvoke . Invoke является синхронным и BeginInvoke является асинхронным. 
Операция добавляется в очередь событий объекта в Dispatcher указанном DispatcherPriority.
Invoke является синхронной операцией; Поэтому Управление не вернется к вызывающему объекту до тех пор, 
                пока не будет возвращен обратный вызов.                  
                */
            });
        }

    }
}
/*
Если теперь запустить программу, библиотека TPL распределит рабочую нагрузку по множеству потоков, взятых из пула, используя столько процессоров, сколько возможно. Однако в заголовке окна не будут отображаться имена уникальных потоков, а при вводе в текстовой области ничего не будет видно, пока не обработаются все файлы изображений! Причина в том, что первичный поток пользовательского интерфейса все равно остается блокированным, ожидая, пока все прочие потоки завершат свою работу.
*/
