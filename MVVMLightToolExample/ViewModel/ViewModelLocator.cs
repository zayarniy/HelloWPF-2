/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVVMLightToolExample"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

//����� ViewModelLocator �������� ����������� ����� ����� ���������������� ����������� � ViewModels, ������� ��������� ViewModels � ���������������� �����������.���� �� ������ ������������ ���� ViewModel � �������� ��������� �������� ��� ����������������� ����������, �� ������ ������������ ��� ViewModel ��� �������� � ������ ViewModelLocator.
namespace MVVMLightToolExample.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            //�� ������������� ��������� IOC ��� ����������. MVVM Light Toolkit ������������� ��������� IOC �� ���������. ��������� IOC ������������ ��� ������������ � ���������� ����������� �������� ��������������� � ����������. 

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            // �� ������������ MainViewModel � ���������� IOC.
            SimpleIoc.Default.Register<MainViewModel>();
            //�� ������������ NotifyUserMethod � ������� Messenger. ����� �������, ����� �� ���������� ��������� ��������� � ������� Messenger � ������� NotificationMessage, ��� ������������� ��������� NotifyUserMethod.
                        Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}