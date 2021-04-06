using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EncryptorProj;
using Microsoft.Win32;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsFieldsFilled(PlainBitsTextBox.Text))
            {
                GeneratedKeyTextBox.Text = "";
                CipherBitsTextBox.Text = "";
                Encryptor encryptor = new Encryptor();
                BitArray key = KeyTextBox.Text.StringToBitArray();
                BitArray bitArrToEncrypt = PlainBitsTextBox.Text.StringToBitArray();
                BitArray result = encryptor.Encrypt(key, bitArrToEncrypt);
                GeneratedKeyTextBox.Text = encryptor.GeneratedKey.ToBitString();
                CipherBitsTextBox.Text = result.ToBitString();
            } 
        }

        private void DecryptBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsFieldsFilled(CipherBitsTextBox.Text))
            {
                GeneratedKeyTextBox.Text = "";
                PlainBitsTextBox.Text = "";
                Encryptor encryptor = new Encryptor();
                BitArray key = KeyTextBox.Text.StringToBitArray();
                BitArray bitArrToDecrypt = CipherBitsTextBox.Text.StringToBitArray();
                BitArray result = encryptor.Encrypt(key, bitArrToDecrypt);
                GeneratedKeyTextBox.Text = encryptor.GeneratedKey.ToBitString();
                PlainBitsTextBox.Text = result.ToBitString();
            }
        }

        private void OpenToEncryptBtn_CLick(object sender, RoutedEventArgs e)
        {
            CipherBitsTextBox.Text = "";
            GeneratedKeyTextBox.Text = "";

            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "D:\\"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] fileText = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                System.Array.Reverse(fileText);
                PlainBitsTextBox.Text = new BitArray(fileText).ToBitString();
            }
        }

        private void OpenToDecryptBtn_Click(object sender, RoutedEventArgs e)
        {
            PlainBitsTextBox.Clear();
            GeneratedKeyTextBox.Text = "";

            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "D:\\"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] fileText = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                System.Array.Reverse(fileText);
                CipherBitsTextBox.Text = new BitArray(fileText).ToBitString();
            }
        }

        private void SaveCipherBtn_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = "D:\\"
            };

            if(saveFileDialog.ShowDialog() == true)
            {
                string name = saveFileDialog.FileName;
                byte[] res = CipherBitsTextBox.Text.StringToBitArray().ToByteArray();
                System.Array.Reverse(res);
                System.IO.File.WriteAllBytes(name, res);
            }
        }

        private void SavePlainBtn_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = "D:\\"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                byte[] res = PlainBitsTextBox.Text.StringToBitArray().ToByteArray();
                System.Array.Reverse(res);
                string name = saveFileDialog.FileName;
                System.IO.File.WriteAllBytes(name, res);
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsFieldsFilled(string message)
        {
            if (KeyTextBox.Text.Length != 40)
            {
                MessageBox.Show("The key length must be 40 bits.", "Incorrect key", MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }

            if (message.Length == 0)
            {
                MessageBox.Show("Please, enter your message.", "Empty field", MessageBoxButton.OKCancel,
                                MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void KeyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                (
                    textBox.Text
                           .Where
                            (ch =>
                                ch == '1' || ch == '0'
                            )
                           .ToArray()
                );
                textBox.SelectionStart = e.Changes.First().Offset + 1;
                textBox.SelectionLength = 0;
            }
        }
    }
}
