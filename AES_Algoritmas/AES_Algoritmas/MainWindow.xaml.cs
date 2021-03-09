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
using Microsoft.Win32;
using System.IO;

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
            radioButtonECB.IsChecked = true;
            radioButtonCypherInputText.IsChecked = true;
            radioButtonCypherOutputText.IsChecked = true;
            radioButtonDecypherInputText.IsChecked = true;
            radioButtonDecypherOutputText.IsChecked = true;
        }

        private void buttonEncrypt_Click(object sender, RoutedEventArgs e)
        {
            int mode = 1;
            if(radioButtonCBC.IsChecked==true)
                mode = 2;
            try
            {
                string text;
                AES aes;
                aes = new AES(textBoxKey.Text, textBoxIV.Text, mode);
                if (radioButtonCypherInputText.IsChecked == true)
                {
                    text = textBoxCypherInput.Text;
                }
                else
                {
                    text = File.ReadAllText(textBoxCypherFilePath.Text);
                }

                if(radioButtonCypherOutputText.IsChecked == true)
                {
                    textBoxCypherOutput.Text = aes.encrypt(text);
                }
                else
                {
                    if (!File.Exists($"{textBoxCypherFileName.Text}.txt") || File.Exists($"{textBoxCypherFileName.Text}.txt") && MessageBox.Show("File exists, do you want to overwrite it?", "File exists", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        StreamWriter sw = new StreamWriter($"{textBoxCypherFileName.Text}.txt");
                        sw.WriteLine(aes.encrypt(text));
                        sw.Close();
                        MessageBox.Show("File created successfully");
                    }
                }
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
                string text;
                AES aes = new AES(textBoxKey.Text, textBoxIV.Text, mode);
                if (radioButtonDecypherInputText.IsChecked == true)
                {
                    text = textBoxDecypherInput.Text;
                }
                else
                {
                    text = File.ReadAllText(textBoxDecypherFilePath.Text);
                }

                if (radioButtonDecypherOutputText.IsChecked == true)
                {
                    textBoxDecypherOutput.Text = aes.decrypt(text);
                }
                else
                {
                    if (!File.Exists($"{textBoxDecypherFileName.Text}.txt") || File.Exists($"{textBoxDecypherFileName.Text}.txt") && MessageBox.Show("File exists, do you want to overwrite it?", "File exists", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        StreamWriter sw = new StreamWriter($"{textBoxDecypherFileName.Text}.txt");
                        sw.WriteLine(aes.decrypt(text));
                        sw.Close();
                        MessageBox.Show("File created successfully");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Decryption unsuccessful.");
            }
        }

        //mode selection
        private void radioButtonECB_Checked(object sender, RoutedEventArgs e)
        {
            labelIV.Visibility = Visibility.Collapsed;
            textBoxIV.Visibility = Visibility.Collapsed;
        }

        private void radioButtonCBC_Checked(object sender, RoutedEventArgs e)
        {
            labelIV.Visibility = Visibility.Visible;
            textBoxIV.Visibility = Visibility.Visible;
        }

        //Cypher input
        private void radioButtonCypherInputText_Checked(object sender, RoutedEventArgs e)
        {
            textBoxCypherInput.Visibility = Visibility.Visible;
            gridCypherInput.Visibility = Visibility.Collapsed;
        }

        private void radioButtonCypherInputFile_Checked(object sender, RoutedEventArgs e)
        {
            textBoxCypherInput.Visibility = Visibility.Collapsed;
            gridCypherInput.Visibility = Visibility.Visible;
        }

        //Cypher output
        private void radioButtonCypherOutputText_Checked(object sender, RoutedEventArgs e)
        {
            textBoxCypherOutput.Visibility = Visibility.Visible;
            gridCypherOutput.Visibility = Visibility.Collapsed;
        }

        private void radioButtonCypherOutputFile_Checked(object sender, RoutedEventArgs e)
        {
            textBoxCypherOutput.Visibility = Visibility.Collapsed;
            gridCypherOutput.Visibility = Visibility.Visible;
        }

        //Decypher input
        private void radioButtonDecypherInputText_Checked(object sender, RoutedEventArgs e)
        {
            textBoxDecypherInput.Visibility = Visibility.Visible;
            gridDecypherInput.Visibility = Visibility.Collapsed;
        }

        private void radioButtonDecypherInputFile_Checked(object sender, RoutedEventArgs e)
        {
            textBoxDecypherInput.Visibility = Visibility.Collapsed;
            gridDecypherInput.Visibility = Visibility.Visible;
        }

        //Decypher output
        private void radioButtonDecypherOutputText_Checked(object sender, RoutedEventArgs e)
        {
            textBoxDecypherOutput.Visibility = Visibility.Visible;
            gridDecypherOutput.Visibility = Visibility.Collapsed;
        }

        private void radioButtonDecypherOutputFile_Checked(object sender, RoutedEventArgs e)
        {
            textBoxDecypherOutput.Visibility = Visibility.Collapsed;
            gridDecypherOutput.Visibility = Visibility.Visible;
        }

        private void buttonCypherBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Text Files|*.txt";
            dialog.DefaultExt = ".txt";
            Nullable<bool> dialogOK = dialog.ShowDialog();

            if(dialogOK == true)
            {
                textBoxCypherFilePath.Text = dialog.FileName;
            }
        }

        private void buttonDecypherBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Text Files|*.txt";
            dialog.DefaultExt = ".txt";
            Nullable<bool> dialogOK = dialog.ShowDialog();

            if (dialogOK == true)
            {
                textBoxDecypherFilePath.Text = dialog.FileName;
            }
        }

    }
}
