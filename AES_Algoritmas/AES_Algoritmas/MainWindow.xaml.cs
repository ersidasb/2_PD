using System;
using System.Collections.Generic;
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

namespace AES_Algoritmas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxIV.MaxLength = 16;
            textBoxKey.MaxLength = 16;
        }

        private void buttonEncrypt_Click(object sender, RoutedEventArgs e)
        {
            int mode = 1;
            if(radioButtonCBC.IsChecked==true)
                mode = 2;
            try
            {
                AES aes = new AES(textBoxKey.Text, textBoxIV.Text, mode);
                textBoxCypherOutput.Text = aes.encrypt(textBoxCypherInput.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonDecrypt_Click(object sender, RoutedEventArgs e)
        {
            int mode = 1;
            if (radioButtonCBC.IsChecked == true)
                mode = 2;
            try
            {
                AES aes = new AES(textBoxKey.Text, textBoxIV.Text, mode);
                textBoxDecypherOutput.Text = aes.decrypt(textBoxDecypherInput.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Decryption unsuccessful.");
            }
        }
    }
}
