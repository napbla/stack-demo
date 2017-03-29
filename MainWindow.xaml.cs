using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;


namespace StackDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stack a = new Stack();
        Button[] b = new Button[10];
        
        const int width = 75;
        const int height = 25;
        const int vttrai = 225;
        float vtdau = -250;
        
        Thickness topLeft = new Thickness(vttrai,-250,0,0); //vi tri dau
        const int bottomHeight = 225; //vi tri cuoi
        const int distance = 10; //khoang cach
      
        static Timer timer;
        static Timer timer2;

        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(100);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timerElapsed);


            timer2 = new Timer(100);
            timer2.AutoReset = true;
            timer2.Elapsed += new ElapsedEventHandler(timerElapsed2);
        }

        private delegate void SimpleDelegate();

       
        
        float accelerator = 10;

        private void dropDown()
        {

            if (vtdau < (bottomHeight - (a.TopPosition() - 1) * height * 2 - distance))
            {
                vtdau += (accelerator);
                if (vtdau > (bottomHeight - (a.TopPosition() - 1) * height * 2 - distance))
                    vtdau = bottomHeight - (a.TopPosition() - 1) * height * 2 - distance;
                accelerator = accelerator * 2 ;
                b[a.TopPosition() - 1].Margin = new Thickness(vttrai, vtdau, 0, 0);
            }
            else
            {
                timer.Stop();
                vtdau = -250;
                accelerator = 10;
            }
        }

        private void getUp()
        {

            if (vtdau >-250 )
            {
                vtdau -= accelerator;
                if (vtdau < -250)
                    vtdau = -250;
                b[a.TopPosition()].Margin = new Thickness(vttrai, vtdau, 0, 0);
            }
            else
            {
                timer2.Stop();
                myGrid.Children.Remove(b[a.TopPosition()]); 
                vtdau = -250;
                accelerator = 10;
              
            }
        }

        void timerElapsed(object sender, ElapsedEventArgs e)
        {

            SimpleDelegate del1 = dropDown;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, del1);

        }

        void timerElapsed2(object sender, ElapsedEventArgs e)
        {

            SimpleDelegate del1 = getUp;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, del1);

        }

        private void pushBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((vtdau == -250)&&(accelerator==10))
            {
                if (ioTb.Text != "")
                {
                    statusLine.Text = a.Push(ioTb.Text);
                    if (statusLine.Text != "Stack is full")
                    {
                        int t = a.TopPosition() - 1;

                        //tao button
                        if (b[t] == null)
                        {
                            b[t] = new Button();
                            b[t].Height = height;
                            b[t].Width = width;
                            b[t].IsEnabled = true;
                            b[t].Margin = topLeft;
                        }
                        b[t].Content = a.ViewTop();
                        myGrid.Children.Add(b[t]);

                        //bat timer
                        timer.Start();
                    }
                    ioTb.Text = "";
                }
                else
                    statusLine.Text = "Please fill in the text box!";
            }
            else statusLine.Text = "Please wait ...";
        }

        private void popBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((vtdau == -250)&&(accelerator==10))
            {
                ioTb.Text = a.Pop();
                if (ioTb.Text == "Stack is empty")
                {
                    statusLine.Text = ioTb.Text;
                    ioTb.Text = "";
                    return;
                }
                else statusLine.Text = "Successfully pop " + ioTb.Text;

                vtdau = (bottomHeight - a.TopPosition() * height * 2 - distance);
                accelerator = 30;
                timer2.Start();         
            }
            else statusLine.Text = "Please wait ...";
        }

        private void topBtn_Click(object sender, RoutedEventArgs e)
        {
            ioTb.Text = a.ViewTop();
            if (ioTb.Text == "Stack is empty")
            {
                statusLine.Text = ioTb.Text;
                ioTb.Text = "";
            }
            else
                statusLine.Text = "Top is " + a.ViewTop();
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created on Apr 2012\nby\n\n\nLâm Tuấn Anh                  0913148\nNguyễn Thái Thiệu           0913102\nNguyễn Lê Hoàng Anh    0913075 \n\n\n09VLH1\nEmail:tuananhlam91@gmail.com");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

      
    }
}
