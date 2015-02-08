using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DarkAccuracy
{


    public partial class frmMain : Form
    {
        private String[] classesArray = { "bounty_hunter", "crusader", "grave_robber", "hellion", "highwayman", "jester", "leper", "occultist", "plague_doctor", "vestal" };

        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var confirmResult = MessageBox.Show("Did you backup everything??!?!?",
                                     "Time to win!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;
            confirmResult = MessageBox.Show("Did you really backup everything??!?!?",
                "Time to win!!",
                MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;
            confirmResult = MessageBox.Show("The whole heroes folder??!?!?",
                "Time to win!!",
                MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;

            append("Starting");

            foreach (string s in classesArray)
            {
                String file = Application.StartupPath + "\\heroes\\" + s + "\\" + s + ".info.darkest";
                if (!File.Exists(file))
                {
                    MessageBox.Show("File: " + file + " not found!");
                    return;
                }
                append(s + " info file located");
            }

            foreach (string file in classesArray.Select(s => Application.StartupPath + "\\heroes\\" + s + "\\" + s + ".info.darkest"))
            {
                append("Processing " + file);
                File.WriteAllText(file, Regex.Replace(File.ReadAllText(file), ".atk (.*?)\\w%", ".atk 100%"));
                append("Processed " + file);
            }


            String path = Application.StartupPath + "\\monsters";

            var monsterConfirm = MessageBox.Show("Do you want monsters to get 100% accuracy aswell?", "Mobs",
                MessageBoxButtons.YesNo);

            if (monsterConfirm == DialogResult.Yes)
            {
                var monsterConfirm2 = MessageBox.Show("Did you backup the monsters folder!??!?!?", "Mobs",
                    MessageBoxButtons.YesNo);

                if (monsterConfirm2 == DialogResult.Yes)
                {

                    foreach (string s in Directory.GetFiles(path, "*", SearchOption.AllDirectories).Where(s => s.Contains("info.darkest")))
                    {
                        append("Processing " + s);
                        File.WriteAllText(s, Regex.Replace(File.ReadAllText(s), ".atk (.*?)\\w%", ".atk 100%"));
                        append("Processed " + s);
                    }
                }
            }


            MessageBox.Show("All Done!");
        }

        private void append(String text)
        {
            logBox.AppendText(text + "\n");
            logBox.ScrollToCaret();
        }
    }
}
