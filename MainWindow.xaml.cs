using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Path = System.IO.Path;

namespace THREADWPFPRACTICE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filename = null;
        string pathname = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                lFileName.Content = openFileDialog.FileName;

            filename = Path.GetFileNameWithoutExtension(lFileName.Content.ToString());
            pathname = Path.GetDirectoryName(lFileName.Content.ToString()) +"\\";

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(lFileName.Content.ToString()))
                return;
            string newpath = pathname + filename + "_copy.txt";
           
            Thread t = new Thread(ChangeString);
            t.Start();

        }

        private void ChangeString(object filename)
        {
          
            string crypted = null;

            string text = File.ReadAllText(lFileName.Content.ToString());
            if ((bool)rbEncrypt.IsChecked)
                crypted = Caesar(text);
            else
                crypted = Caesar(text, -1);

            File.WriteAllText(path, crypted);
        }

        static string Caesar(string value, int shift =1)
        {
            char[] buffer = value.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                // Letter.
                char letter = buffer[i];
                // Add shift to all.
                letter = (char)(letter + shift);
                // Subtract 26 on overflow.
                // Add 26 on underflow.
                if (letter > 'z')
                {
                    letter = (char)(letter - 26);
                }
                else if (letter < 'a')
                {
                    letter = (char)(letter + 26);
                }
                // Store.
                buffer[i] = letter;
            }
            return new string(buffer);
        }
    }
}
