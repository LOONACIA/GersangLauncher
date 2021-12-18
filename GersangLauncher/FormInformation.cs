using System.Windows.Forms;

namespace GersangLauncher
{
	public partial class FormInformation : Form
	{
		public FormInformation()
		{
			InitializeComponent();
			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			LabelVersion.Text = $"거상 런처 v{version.Major}.{version.Minor}.{version.Build}";
			TextBoxLicense.BackColor = this.BackColor;
			TextBoxLicense.Text =
"MIT License" + "\r\n\r\n" +
"Copyright (c) 2021 LOONACIA" + "\r\n\r\n" +
"Permission is hereby granted, free of charge, to any person obtaining a copy " +
"of this software and associated documentation files(the \"Software\"), to deal " +
"in the Software without restriction, including without limitation the rights " +
"to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell " +
 "copies of the Software, and to permit persons to whom the Software is " +
 "furnished to do so, subject to the following conditions:" + "\r\n\r\n" +
 "The above copyright notice and this permission notice shall be included in all " +
"copies or substantial portions of the Software." + "\r\n\r\n" +
"THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR " +
"IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, " +
"FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE " +
"AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER " +
"LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, " +
"OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE " +
"SOFTWARE.";
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink(linkLabel1.Text);
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenLink(linkLabel2.Text);
		}

		private void OpenLink(string link)
		{
			System.Diagnostics.Process process = new();
			process.StartInfo.FileName = link;
			process.StartInfo.UseShellExecute = true;
			process.Start();
		}
	}
}
