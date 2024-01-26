using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sergioteacher.Csharp05.EditorTextoA
{
    public partial class MainWindow : Window
    {
        private static String tituloA = "EditorTextoA";
        private static String fpath;
        bool modificado;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = tituloA;
            fpath = "";
            modificado = false;
        }

        private void Acercade_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Un editor, con un control básico de edición \n " +
                "mostrando:\n" +
                "   -Un menú con `Command´ \n" +
                "   -y facilidades de fichero con WPF" +
                "\n" +
                "\n" +
                "             Copyright (C) Sergioteacher", "Edidor básico");
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Ventana1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (modificado)
            {
                MessageBoxResult resultado = MessageBox.Show("Se ha modificado, ¿Guardar?", "Duda...", MessageBoxButton.YesNo);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        SaveFile();
                        break;
                }
            }
        }

        private void CommandBinding_Executed_New(object sender, ExecutedRoutedEventArgs e)
        {
            if (modificado)
            {
                MessageBoxResult resultado = MessageBox.Show("Se ha modificado, ¿Guardar?", "Duda...", MessageBoxButton.YesNoCancel);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        SaveFile();
                        break;
                    case MessageBoxResult.No:
                        ClearText();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else
            {
                ClearText();
            }
        }

        private void CommandBinding_Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            if (modificado)
            {
                MessageBoxResult resultado = MessageBox.Show("Se ha modificado, ¿Guardar?", "Duda...", MessageBoxButton.YesNo);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        SaveFile();
                        break;
                }
            }

            OpenFileDialog();
        }

        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (Mguardar.IsEnabled == true)
            {
                SaveFile();
            }
        }

        private void CommandBinding_Executed_GuardarEn(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileAs();
        }

        private void CommandBinding_Executed_Print(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Imprimiendo ...");
        }

        private void CommandBinding_Executed_Help(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help");
        }

        private void Tedit_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateStatus();
        }

        private void Tedit_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            int fila = Tedit.GetLineIndexFromCharacterIndex(Tedit.CaretIndex);
            int columna = Tedit.CaretIndex - Tedit.GetCharacterIndexFromLineIndex(fila);
            Testado.Text = " Fila: " + (fila + 1).ToString() + ", Columna: " + (columna + 1).ToString();

            modificado = true;
            Ventana1.Title = tituloA + " *" + fpath;
            if (fpath != "") { Mguardar.IsEnabled = true; }
        }

        private void ClearText()
        {
            Tedit.Text = "";
            fpath = "";
            modificado = false;
            Ventana1.Title = tituloA + " " + fpath;
        }

        private void OpenFileDialog()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFile()
        {
            if (fpath == "")
            {
                SaveFileAs();
            }
            else
            {
                File.WriteAllText(fpath, Tedit.Text);
                modificado = false;
                Ventana1.Title = tituloA + " " + fpath;
            }
        }

        private void SaveFileAs()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}