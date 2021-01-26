/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVVPPasswordLightTool"
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

// ласс ViewModelLocator €вл€етс€ посредником между вашим пользовательским 
//интерфейсом и ViewModels, который св€зывает ViewModels с пользовательским интерфейсом.
//≈сли вы хотите использовать вашу ViewModel в качестве источника прив€зки дл€ 
//пользовательского интерфейса, вы должны предоставить эту ViewModel как свойство 
//в классе ViewModelLocator.
namespace MVVMPasswordLightTool.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            //мы устанавливаем контейнер IOC дл€ приложени€. MVVM Light Toolkit предоставл€ет контейнер IOC по умолчанию.  онтейнер IOC используетс€ дл€ отслеживани€ и разрешени€ экземпл€ров. 
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            // мы регистрируем MainViewModel в контейнере IOC.
            SimpleIoc.Default.Register<MainViewModel>();
            //ћы регистрируем NotifyUserMethod с классом Messenger. “аким образом, когда мы отправл€ем текстовое сообщение с классом Messenger с помощью NotificationMessage, оно автоматически выполн€ет NotifyUserMethod.
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public ViewModel2 Main2//ѕример создание другой модели
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel2>();
            }
        }



        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}