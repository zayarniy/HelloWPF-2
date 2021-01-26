//NOTE: This file is not meant to be compiled!

public class Button : ButtonBase
{
  // The routed event
  public static readonly RoutedEvent ClickEvent;

  static Button()
  {
    // Register the event
    Button.ClickEvent = EventManager.RegisterRoutedEvent("Click",
    RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));
    //�
  }

  // A .NET event wrapper (optional)
  public event RoutedEventHandler Click
  {
    add { AddHandler(Button.ClickEvent, value); }
    remove { RemoveHandler(Button.ClickEvent, value); }
  }

  protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
  {
        //�
        // Raise the event
        RaiseEvent(new RoutedEventArgs(Button.ClickEvent, this));
        //�
    }
    //�
}