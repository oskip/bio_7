using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BIO_Z7.Exceptions;
using BIO_Z7.Properties;

namespace BIO_Z7
{
    public partial class MainForm : Form
    {
        public static readonly ArrayList ValidChars = new ArrayList { 'A', 'C', 'T', 'G', '-' };

        public MainForm()
        {
            InitializeComponent();
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            inputBox.ForeColor = ValidateInput(inputBox.Text) ? Color.DarkGreen : Color.Crimson;
        }

        private bool ValidateInput(string text)
        {
            var k = text.IndexOf(SubwordFactory.Separator);
            var isValid = text.All(l => ValidChars.Contains(l)) &
                          text.Split(SubwordFactory.Separator).All(f => f.Length == k);
            return isValid;
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RunDnaSequencing(sender, e);
            else if (e.KeyCode == Keys.Escape)
                EscapeAction(sender, e);
        }

        private void RunDnaSequencing(object sender, KeyEventArgs e)
        {
            try
            {
                var collection = SubwordFactory.CreateFromInput(inputBox.Text);
                var graph = SubwordsGraphAdapter.GetGraph(collection);
                var result = graph.GetDnaSequence();
                MessageBox.Show(@"Sekwencja: " + result, @"Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NoEulerianTrailException ex)
            {
                var result = MessageBox.Show(Resources.MainForm_EnterAction_NoEulerianTrailMessage, @"Informacja",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result.ToString() == "Yes")
                    RunDnaSequencingWithErrorCompensation(1);
            }
            catch (Exception ex)
            {
                HandleErrors(ex);
            }
            finally
            {
                e.Handled = true;
            }
        }

        private void RunDnaSequencingWithErrorCompensation(int level)
        {
            try
            {
                var collection = SubwordFactory.CreateFromInput(inputBox.Text);
                var graph = SubwordsGraphAdapter.GetGraph(collection);
                var result = graph.GetDnaSequenceWithCompensation(level);
                if (result.CompensatingSubwords != null && result.DeletedSubwords != null)
                    MessageBox.Show(string.Format(@"Kompensacja przez usunięcie: {0} oraz dodanie {1} dała sekwencję: {2}", 
                        string.Join(",",result.DeletedSubwords), result.CompensatingSubwords, result.DnaSequence),
                        @"Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (result.CompensatingSubwords != null)
                    MessageBox.Show(string.Format(@"Kompensacja przez dodanie: {0} dała sekwencję: {1}", result.CompensatingSubwords, result.DnaSequence), 
                                    @"Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (result.DeletedSubwords != null)
                    MessageBox.Show(string.Format(@"Kompensacja przez usunięcie: {0} dała sekwencję: {1}", result.DeletedSubwords, result.DnaSequence),
                                    @"Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NoEulerianTrailException e)
            {
                if (level < 3)
                {
                    var result = MessageBox.Show(
                        String.Format("Nie odnaleziono kompensującego podsłowa dla {0} błędów"+
                        "\nPrzeprowadzić testy dla {1} błędów?", level, level+1),
                        @"Wynik", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result.ToString() == "Yes")
                        RunDnaSequencingWithErrorCompensation(level+1);
                }
                else
                {
                    MessageBox.Show(Resources.MainForm_RunDnaSequencingWithErrorCompensation_Compensation_Fail,
                        @"Niepowodzenie", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception e)
            {
                HandleErrors(e);
            }
        }

        private void HandleErrors(Exception ex)
        {
            var message = GetMessageForException(ex);
            MessageBox.Show(message, @"Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EscapeAction(object sender, KeyEventArgs e)
        {
            Application.Exit();
        }

        private string GetMessageForException(Exception ex)
        {
            //C# nie ma switchowania po typie..
            var type = ex.GetType();
            if (type == typeof (BadSymbolException)) return "Napotkano nieprawidłowy znak w sekwencji.";
            if (type == typeof (InconsistentSubwordLengthException))
                return "Wprowadzone podsłowa nie są równej długości";
            return "Napotkano błąd: " + ex.Message;
        }
    }
}
