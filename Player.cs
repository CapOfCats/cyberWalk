using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovik2
{
    public class Player
    {
        public Player()
        { }
        public Player(int _strength, int _sneakness, int _luck, int _food, int _money, int _energy, int _reputation, PictureBox _avatar, PictureBox _chip, Form _form, PictureBox _endscreen, Button roll)
        {
            strength = _strength;
            sneakness = _sneakness;
            luck = _luck;
            food = _food;
            money = _money;
            energy = _energy;
            reputation = _reputation;
            avatar = _avatar;
            chip = _chip;
            form = _form;
            endscreen = _endscreen;
            endscreen.Visible = false;
            rollbutton = roll;
        }

        public Form form;
        public int bossCount = 0;
        public int strength { get; set; }
        public int sneakness { get; set; }
        public int luck { get; set; }
        public int food { get; set; }
        public int money { get; set; }
        public int energy { get; set; }
        public int reputation { get; set; }
        public int currentPos = 0;
        public PictureBox avatar;
        public PictureBox chip;
        public TextBox strengthTB;
        public TextBox sneaknessTB;
        public TextBox luckTB;
        public TextBox energyTB;
        public TextBox foodTB;
        public TextBox moneyTB;
        public TextBox reputationTB;
        public PictureBox endscreen;
        public Button rollbutton;
        public void RepCheck()
        {
            if (reputation >= 10)
            {
                avatar.Image = Image.FromFile("Game\\good.jpg");
                
            }
            else if (reputation <= -10)
            {
                avatar.Image = Image.FromFile("Game\\evil.jpg");
                
            }
            else
            {
                avatar.Image = Image.FromFile("Game\\normal.jpeg");
                
            }
        }

        public bool DeathCheck()
        {
            food --;
            energy --;
            foodTB.Text = $"{food}" + "/100";
            energyTB.Text = $"{energy}" + "/100";
            if (food <= 0 || energy <= 0 || money<=0)
                return true;             
            else
                return false;
        }

        public TextBox TBCreation(Point p, Size s, HorizontalAlignment a, bool ROnly,Form f, Color backcolor, Font font)
        {
            TextBox tb = new TextBox();
            tb.Location = p;
            tb.Size = s;
            tb.TextAlign = a;
            tb.ReadOnly = ROnly;
            FormControls(tb, new PictureBox());
            tb.BringToFront();
            tb.Multiline = true;
            tb.Font = font;
            tb.BackColor = backcolor;
            return tb;            
        }
        public void IfDead()
        {
            End(false);
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.Start();
            timer.Tick += new System.EventHandler((object sender, EventArgs e) =>
            {
                form.Close();
            });
        }
        public void IfWon()
        {
            End(true);
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.Start();
            timer.Tick += new System.EventHandler((object sender, EventArgs e) =>
            {
                form.Close();
            });
        }
        public void End(bool what)
        {
            rollbutton.Dispose();
            endscreen.Visible = true;
            if (what)
                endscreen.Image = Image.FromFile("Game\\victorypic.jpg");
            else return;
        }
        public void FormControls(TextBox tb, PictureBox pb)
        {
            form.Controls.Add(tb);
        }
         public void Initialize(Form form)
        {
            strengthTB = TBCreation(new Point(210, 35), new Size(60, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            sneaknessTB = TBCreation(new Point(210, 80), new Size(60, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            luckTB = TBCreation(new Point(210, 125), new Size(60, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            energyTB = TBCreation(new Point(10, 280), new Size(90, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            foodTB = TBCreation(new Point(110, 280), new Size(90, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            moneyTB = TBCreation(new Point(210, 280), new Size(90, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            reputationTB = TBCreation(new Point(115, 360), new Size(150, 30), HorizontalAlignment.Center, true,form, Color.LightGreen, new Font("Trebuchet MS", 14F, FontStyle.Italic));
            strengthTB.Text = strength.ToString();
            sneaknessTB.Text = sneakness.ToString();
            luckTB.Text = luck.ToString();
            energyTB.Text = energy.ToString() + "/100";
            foodTB.Text = food.ToString() + "/100";
            moneyTB.Text = money.ToString() + "/100";
            reputationTB.Text = reputation.ToString();
        }
        public string Walk(int where)
        {
            currentPos += where;
            chip.Location = new Point(chip.Location.X + (80 * where), 370);
            if (currentPos > 12)
            {
                currentPos -= 13;
                chip.Location = new Point(355 + (79 * currentPos), 370);
            }
            return $"Выпало {where}!";
        }
         public Player UpdateStats(int[] statsToCount)
        {
            for(int i=0;i<7;i++)
                if(statsToCount[i]!=0)
                    switch (i)
                    {
                        case 0:
                            {
                                strength += statsToCount[i];
                                if (strength <= 0)
                                    strength = 1;
                                strengthTB.Text = strength.ToString();
                            }
                                break;
                        case 1:
                            {
                                sneakness += statsToCount[i];
                                if (sneakness <= 0)
                                    sneakness = 1;
                                sneaknessTB.Text = sneakness.ToString();
                            }
                                break;
                        case 2:
                            {
                                luck += statsToCount[i];
                                if (luck <= 0)
                                    luck = 1;
                                luckTB.Text = luck.ToString();
                            }
                            break;
                        case 3:
                            {
                                food += statsToCount[i];
                                foodTB.Text = food.ToString() + "/100";
                            }
                            break;
                        case 4:
                            {
                                energy += statsToCount[i];
                                energyTB.Text = energy.ToString() + "/100";
                            }
                            break;
                        case 5:
                            {
                                money += statsToCount[i];
                                moneyTB.Text = money.ToString() + "/100";
                            }
                            break;
                        case 6:
                            {
                                reputation += statsToCount[i];
                                reputationTB.Text = reputation.ToString();
                            }
                            break;
                    }
            return this;
        }
    }
}
