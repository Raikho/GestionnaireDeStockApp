using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GestionnaireDeStockApp
{
    public static class ControlInputService
    {
        public static bool CorrectPickedChara { get; set; }

        public static bool CheckAllCharacteristics(TextBox refStringTextBox, TextBox nameStringTextBox, TextBox priceDoubleTextBox, TextBox quantIntTextBox, TextBlock textBlockError)
        {
            var refChecked = CheckStringTypeInput(refStringTextBox, textBlockError);
            var nameChecked = CheckStringTypeInput(nameStringTextBox, textBlockError);
            var priceTypeChecked = CheckDoubleTypeInput(priceDoubleTextBox, textBlockError);
            var quantTypeChecked = CheckIntTypeInput(quantIntTextBox, textBlockError);
            if (refChecked == false
                || nameChecked == false
                || priceTypeChecked == false
                || quantTypeChecked == false)
            {
                return CorrectPickedChara = false;
            }
            else
                return CorrectPickedChara = true;
        }

        /// <summary>
        /// Ajoute une "référence" à un article en création.
        /// </summary>
        /// <returns></returns>
        public static bool CheckStringTypeInput(TextBox textBox, TextBlock textBlockError)
        {
            try
            {
                string newInput = textBox.Text;
                if (!Regex.IsMatch(newInput, @"^[a-zA-Z0-9, ]+$"))
                {
                    textBlockError.Text += "Référence: veuillez effectuer une saisie alphanumérique.\n";
                    return CorrectPickedChara = false;
                }
                else
                    return CorrectPickedChara = true;
            }
            catch (Exception except)
            {
                textBlockError.Text += $"L'erreur suivante est survenue: {except.Message}";
                return CorrectPickedChara = false;
            }
        }


        /// <summary>
        /// Ajoute une "prix" à un article en création.
        /// </summary>
        /// <returns></returns>
        public static bool CheckDoubleTypeInput(TextBox textBox, TextBlock textBlockError)
        {
            try
            {
                string newInput = textBox.Text;
                bool correctNum = double.TryParse(newInput, out double price);
                if (!correctNum)
                {
                    textBlockError.Text += "Prix: veuillez saisir un prix chiffré.\n";
                    return CorrectPickedChara = false;
                }
                else
                    return CorrectPickedChara = true;
            }
            catch (Exception except)
            {
                textBlockError.Text += $"L'erreur suivante est survenue: {except.Message}";
                return CorrectPickedChara = false;
            }
        }

        /// <summary>
        /// Ajoute une "quantité" à un article en création.
        /// </summary>
        /// <returns></returns>
        public static bool CheckIntTypeInput(TextBox textBox, TextBlock textBlockError)
        {
            try
            {
                string newInput = textBox.Text;
                bool correctNum = int.TryParse(newInput, out int quantity);
                if (!correctNum)
                {
                    textBlockError.Text += "Quantité: veuillez saisir une quantité chiffrée.\n";
                    return CorrectPickedChara = false;
                }
                else
                    return CorrectPickedChara = true;
            }
            catch (Exception except)
            {
                textBlockError.Text += $"L'erreur suivante est survenue: {except}";
                return CorrectPickedChara = false;
            }
        }

        public static bool CheckDoubleTypeInput(TextBox textBox)
        {
            try
            {
                string newInput = textBox.Text;
                bool correctNum = double.TryParse(newInput, out double variable);
                if (correctNum == true || textBox.Text == "")
                {
                    return CorrectPickedChara = true;
                }
                else
                    return CorrectPickedChara = false;
            }
            catch (Exception)
            {
                return CorrectPickedChara = false;
            }
        }
    }
}
