namespace MGO2_Setlist_Randomizer
{
    public partial class MGO2Rando : Form
    {
        private CheckBox[] mapCheckBoxes;
        private Random random = new Random();
        private TextBox[] textBoxes;
        private string[] shortModeForms = new string[] { "DM", "TDM", "CAP", "BASE", "SNE", "RES", "TSNE", "SDM", "BOMB", "SCAP", "RACE" };


        public MGO2Rando()
        {
            InitializeComponent();

            //Array for grouping up the map checkboxes
            mapCheckBoxes = new CheckBox[] { AA, BB, CC, DD, FF, GG, HH, II, JJ, LL, MM, OO, PP, QQ, RR, SS, TT, UU, VV, WW };

            //Array of textboxes 1-15
            textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15 };
        }

        // Function to generate the special setting based on the selected mode
        private string GenerateSpecialSetting(CheckBox selectedMode)
        {
            string specialSetting = "";

            if (drebinPointsBox.Checked)
            {
                specialSetting = "DP: On";
            }
            else if (noDrebinPointsBox.Checked)
            {
                specialSetting = "DP: Off";
            }
            else if (randomDrebinPointsBox.Checked)
            {
                specialSetting = "DP: " + random.Next(1000, 3000);
            }

            if (headshotsOnlyBox.Checked)
            {
                specialSetting += " Headshots Only";
            }

            return specialSetting;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Check that only one of DP On, DP Off, and DP Randomize is checked
            int dpOptionsChecked = (drebinPointsBox.Checked ? 1 : 0) + (noDrebinPointsBox.Checked ? 1 : 0) + (randomDrebinPointsBox.Checked ? 1 : 0);
            if (dpOptionsChecked > 1)
            {
                MessageBox.Show("Please select only one of the following options: DP On, DP Off, DP Randomize.");
                return;
            }
            // Array for grouping up the mode checkboxes
            CheckBox[] modeCheckBoxes = new CheckBox[] { DM, TDM, CAP, BASE, SNE, RES, TSNE, SDM, BOMB, SCAP, RACE };

            // Create a list of checked maps
            List<CheckBox> checkedMaps = new List<CheckBox>();
            foreach (CheckBox mapCheckbox in mapCheckBoxes)
            {
                if (mapCheckbox.Checked)
                {
                    checkedMaps.Add(mapCheckbox);
                }
            }

            // Create a list of checked modes
            List<CheckBox> checkedModes = new List<CheckBox>();
            foreach (CheckBox modeCheckbox in modeCheckBoxes)
            {
                if (modeCheckbox.Checked)
                {
                    checkedModes.Add(modeCheckbox);
                }
            }

            // If no maps or modes are checked, exit
            if (checkedMaps.Count == 0 || checkedModes.Count == 0)
            {
                MessageBox.Show("Please select at least one map and one mode.");
                return;
            }

            // If no special settings are checked, exit
            if (!drebinPointsBox.Checked && !noDrebinPointsBox.Checked && !randomDrebinPointsBox.Checked && !headshotsOnlyBox.Checked)
            {
                MessageBox.Show("Please select at least one special setting (DP on, DP off, Randomize DP or Headshots Only).");
                return;
            }

            // Clear all text boxes
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }

            // Warmup SCAP mode
            int start = 0;

            if (warmupSCAPBox.Checked)
            {
                textBoxes[0].Text = "Blood Bath SCAP DP: On";
                start = 1;
            }

            // Repeat until all text boxes are filled
            for (int i = start; i < textBoxes.Length; i++)
            {
                // If all maps are exhausted, refill the list
                if (checkedMaps.Count == 0)
                {
                    foreach (CheckBox mapCheckbox in mapCheckBoxes)
                    {
                        if (mapCheckbox.Checked)
                        {
                            checkedMaps.Add(mapCheckbox);
                        }
                    }
                }

                // If all modes are exhausted, refill the list
                if (checkedModes.Count == 0)
                {
                    foreach (CheckBox modeCheckbox in modeCheckBoxes)
                    {
                        if (modeCheckbox.Checked)
                        {
                            checkedModes.Add(modeCheckbox);
                        }
                    }
                }

                // Select a random map
                int randomMapIndex = random.Next(checkedMaps.Count);
                CheckBox selectedMap = checkedMaps[randomMapIndex];

                // Extract the map name from the checkbox text
                int startIndex = selectedMap.Text.IndexOf('(') + 1;
                int endIndex = selectedMap.Text.IndexOf(')');
                string mapName = selectedMap.Text.Substring(startIndex, endIndex - startIndex);

                // Select a random mode
                int randomModeIndex = random.Next(checkedModes.Count);
                CheckBox selectedMode = checkedModes[randomModeIndex];

                // Generate the special setting based on the selected mode
                string specialSetting = GenerateSpecialSetting(selectedMode);

                // For the mode, print out the short form
                int modeIndex = Array.IndexOf(modeCheckBoxes, selectedMode);
                string shortModeForm = shortModeForms[modeIndex];

                // Fill the text box
                textBoxes[i].Text = $"{mapName} {shortModeForm} {specialSetting}";

                // Remove the used map and mode from their respective lists
                checkedMaps.RemoveAt(randomMapIndex);
                checkedModes.RemoveAt(randomModeIndex);
            }
        }


        // Button click event
        private void button2_Click(object sender, EventArgs e)
        {
            // Check that only one of DP On, DP Off, and DP Randomize is checked
            int dpOptionsChecked = (drebinPointsBox.Checked ? 1 : 0) + (noDrebinPointsBox.Checked ? 1 : 0) + (randomDrebinPointsBox.Checked ? 1 : 0);
            if (dpOptionsChecked > 1)
            {
                MessageBox.Show("Please select only one of the following options: DP On, DP Off, DP Randomize.");
                return;
            }

            // Array for grouping up the mode checkboxes
            CheckBox[] modeCheckBoxes = new CheckBox[] { DM, TDM, CAP, BASE, SNE, RES, TSNE, SDM, BOMB, SCAP, RACE };

            // Create a list of checked maps
            List<CheckBox> checkedMaps = new List<CheckBox>();
            foreach (CheckBox mapCheckbox in mapCheckBoxes)
            {
                if (mapCheckbox.Checked)
                {
                    checkedMaps.Add(mapCheckbox);
                }
            }

            // Create a list of checked modes
            List<CheckBox> checkedModes = new List<CheckBox>();
            foreach (CheckBox modeCheckbox in modeCheckBoxes)
            {
                if (modeCheckbox.Checked)
                {
                    checkedModes.Add(modeCheckbox);
                }
            }

            // If no maps or modes are checked, exit
            if (checkedMaps.Count == 0 || checkedModes.Count == 0)
            {
                MessageBox.Show("Please select at least one map and one mode.");
                return;
            }

            // If no special settings are checked, exit
            if (!drebinPointsBox.Checked && !noDrebinPointsBox.Checked && !randomDrebinPointsBox.Checked && !headshotsOnlyBox.Checked)
            {
                MessageBox.Show("Please select at least one special setting (DP on, DP off, Randomize DP or Headshots Only).");
                return;
            }

            // Clear all text boxes
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }

            // Warmup SCAP mode
            int start = 0;
            if (warmupSCAPBox.Checked)
            {
                textBoxes[0].Text = "Blood Bath SCAP DP: On";
                start = 1;
            }

            // Random generators for selecting maps and modes
            Random mapRandom = new Random();
            Random modeRandom = new Random();

            // Repeat until all text boxes are filled
            for (int i = start; i < textBoxes.Length; i++)
            {
                // If all maps are exhausted, refill the list
                if (checkedMaps.Count == 0)
                {
                    foreach (CheckBox mapCheckbox in mapCheckBoxes)
                    {
                        if (mapCheckbox.Checked)
                        {
                            checkedMaps.Add(mapCheckbox);
                        }
                    }
                }

                // If all modes are exhausted, refill the list
                if (checkedModes.Count == 0)
                {
                    foreach (CheckBox modeCheckbox in modeCheckBoxes)
                    {
                        if (modeCheckbox.Checked)
                        {
                            checkedModes.Add(modeCheckbox);
                        }
                    }
                }

                // Select a random map
                int randomMapIndex = mapRandom.Next(checkedMaps.Count);
                CheckBox selectedMap = checkedMaps[randomMapIndex];

                // Extract the map name from the checkbox text
                int startIndex = selectedMap.Text.IndexOf('(') + 1;
                int endIndex = selectedMap.Text.IndexOf(')');
                string mapName = selectedMap.Text.Substring(startIndex, endIndex - startIndex);

                // Select a random mode
                int randomModeIndex = modeRandom.Next(checkedModes.Count);
                CheckBox selectedMode = checkedModes[randomModeIndex];

                // Generate the special setting based on the selected mode
                string specialSetting = GenerateSpecialSetting(selectedMode);

                // For the mode, print out the short form
                int modeIndex = Array.IndexOf(modeCheckBoxes, selectedMode);
                string modeShortForm = shortModeForms[modeIndex];

                // Put it all together and add to the appropriate textbox
                textBoxes[i].Text = $"{mapName} {modeShortForm} {specialSetting}";

                // Remove the used map and mode from their respective lists
                checkedMaps.RemoveAt(randomMapIndex);
                checkedModes.RemoveAt(randomModeIndex);
            }
        }
    }
}
