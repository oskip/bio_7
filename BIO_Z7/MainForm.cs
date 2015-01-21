using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            {
                var collection = SubwordFactory.CreateFromInput(inputBox.Text);
                var graph = SubwordsGraphAdapter.GetGraph(collection);
                var result = graph.GetDnaSequence();
                MessageBox.Show(result);
                e.Handled = true;
            }
        }
    }
}
