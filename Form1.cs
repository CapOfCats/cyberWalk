using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Kursovik2
{
    
    public partial class Form1 : Form
    {
        TextBox infobox;
        TextBox position;
        TextBox lastStep;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("MM\\cyb.png");
            pictureBox2.Image = Image.FromFile("MM\\but.jpg");
            pictureBox2.MouseEnter += new System.EventHandler((object sender, EventArgs e) =>
            {
                pictureBox2.Size = new Size(600, 60);
            });
            pictureBox2.MouseLeave += new System.EventHandler((object sender, EventArgs e) =>
            {
                pictureBox2.Size = new Size(620, 80);
            });

        }

        public TextBox TBCreation(Point p, Size s, HorizontalAlignment a, bool ROnly, Font f, Color c, Color backcolor)
        {
            TextBox tb = new TextBox();
            tb.Location = p;
            tb.Size = s;
            tb.TextAlign = a;
            tb.ReadOnly = ROnly;
            Controls.Add(tb);
            tb.BringToFront();
            tb.Multiline = true;           
            tb.Font = f;
            tb.WordWrap = true;
            tb.ForeColor = c;
            tb.BackColor = backcolor;
            return tb;
            
        }
        public Button ButtonCreation(Point p, Size s, ContentAlignment  a, bool enabled,string sign,Font f, Color c)
        {
            Button b = new Button();
            b.Location = p;
            b.Size = s;
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.Enabled = enabled;
            Controls.Add(b);
            b.BringToFront();
            b.Font = f;
            b.Text = sign;
            b.ForeColor = c;
            return b;
        }

        public PictureBox PicCreation(Point p,Size s,PictureBoxSizeMode sm, Image i,bool visible)
        {
            PictureBox pb = new PictureBox();
            pb.Location = p;
            pb.Size = s;
            pb.SizeMode = sm;
            pb.Image = i;
            pb.Visible = visible;
            Controls.Add(pb);
            pb.BringToFront();
            return pb;
        }
        List<TextBox>CreateRequirementsTB(int height)
        {
            List<TextBox> bufferList = new List<TextBox>();
            bufferList.Add(TBCreation(new Point(460, height+20), new Size(20, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            bufferList.Add(TBCreation(new Point(500, height + 20), new Size(20, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            bufferList.Add(TBCreation(new Point(540, height + 20), new Size(20, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            bufferList.Add(TBCreation(new Point(460, height + 60), new Size(20, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            bufferList.Add(TBCreation(new Point(500, height + 60), new Size(20, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            bufferList.Add(TBCreation(new Point(540, height + 60), new Size(20, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            bufferList.Add(TBCreation(new Point(460, height + 85), new Size(100, 20), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 8F, FontStyle.Regular), Color.Black, Color.White));
            return bufferList;
        }
        List<PictureBox> CreateRequirementsPics(int height)
        {
            List<PictureBox> bufferList = new List<PictureBox>();
            bufferList.Add(PicCreation(new Point(460, height), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\strength.png"), true));
            bufferList.Add(PicCreation(new Point(500, height), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\sneakness.jpeg"), true));
            bufferList.Add(PicCreation(new Point(540, height), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\luck.jpeg"), true));
            bufferList.Add(PicCreation(new Point(460, height+40), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\food.jpg"), true));
            bufferList.Add(PicCreation(new Point(500, height+40), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\energy.jpg"), true));
            bufferList.Add(PicCreation(new Point(540, height+40), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\money.jpg"), true));
            bufferList.Add(PicCreation(new Point(440, height+85), new Size(20, 20), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\perkPics\\reputation.jpg"), true));
            return bufferList;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PreLoad();     
        }
        void PreLoad()
        {
            Button rollbutton = ButtonCreation(new Point(50, 725), new Size(200, 75), ContentAlignment.MiddleCenter, false, "ROLL", new Font("Trebuchet MS", 20F, FontStyle.Bold), Color.Black);
            rollbutton.BackgroundImage = Image.FromFile("Game\\butpic.jpg");
            pictureBox2.Visible = false;
            PictureBox HUD = PicCreation(new Point(5, 5), new Size(300, 400), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\HUD.jpg"), true);
            PictureBox walkfield = PicCreation(new Point(315, 7), new Size(1300, 410), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\wground.jpg"), true);
            Player player = new Player(1, 1, 1, 50, 35, 60, 0,
                PicCreation(
                    new Point(15, 10),
                    new Size(135, 175),
                    PictureBoxSizeMode.StretchImage,
                    Image.FromFile("Game\\normal.jpeg"),
                    true),
                PicCreation(
                    new Point(355, 370),
                    new Size(35, 35),
                    PictureBoxSizeMode.StretchImage,
                    Image.FromFile("Game\\chip.jpg"),
                    true),
                this,
                PicCreation(new Point(0, 0), new Size(1900, 1040), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\lostpic.jpg"), true),
                rollbutton
                );
            player.Initialize(this);
            PictureBox dicefield = PicCreation(new Point(5, 420), new Size(300, 500), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\grey.jpg"), true);
            PictureBox dicecube = PicCreation(new Point(70, 485), new Size(160, 160), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\dice6.png"), true);
            
            PictureBox situationField = PicCreation(new Point(390, 420), new Size(1400, 600), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\SolutionBG.jpg"), true);
            PictureBox dialoguePortrait = PicCreation(new Point(1590 - 5, 425), new Size(200, 250), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\Fixer.jpg"), true);
            TextBox dialogue = TBCreation(new Point(1585, 665), new Size(200, 350), HorizontalAlignment.Center, true, new Font("Trebuchet MS", 16F, FontStyle.Bold), Color.Black, Color.White);
            PictureBox infofield = PicCreation(new Point(1620, 7), new Size(280, 410), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\grey2.jpg"), true);
            infobox = TBCreation(new Point(1630, 65), new Size(260, 340), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 9F, FontStyle.Regular), Color.White, Color.DarkSlateGray);
            infobox.ScrollBars = ScrollBars.Vertical;
            TextBox situationTB = TBCreation(new Point(560, 440), new Size(1020, 230), HorizontalAlignment.Center, true, new Font("Trebuchet MS", 14F, FontStyle.Italic), Color.Blue, Color.Gainsboro);
            situationTB.Visible = false;
            position = TBCreation(new Point(330, 10), new Size(260, 30), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 9.5F, FontStyle.Regular), Color.Black, Color.White);
            lastStep = TBCreation(new Point(600, 10), new Size(260, 30), HorizontalAlignment.Left, true, new Font("Trebuchet MS", 9.5F, FontStyle.Regular), Color.Black, Color.White);
            position.Text = "Текущая позиция: 0";
            lastStep.Text = "Последний ход: 0";
            Situation.ReadCaches();
            Solution.ReadCaches();
            BeginReplics(
                0,
                dialogue,
                "...",
                dialoguePortrait,
                situationTB,
                situationField,
                new int[] { 1, 1, 1, 50, 35, 60, 0 },
                infobox,
                player.strengthTB,
                player.sneaknessTB,
                player.luckTB,
                player.foodTB,
                player.energyTB,
                player.moneyTB,
                player.reputationTB,
                player.avatar,
                player.chip,
                rollbutton,
                dicecube
                );             
        }
        
        void BeginReplics(int clickCount,
            TextBox dialogue,
            string textToFill,
            PictureBox dAvatar,
            TextBox _situationTB,
            PictureBox situationField,
            int[]stats,
            TextBox infobox,
            TextBox strength,
            TextBox sneakness,
            TextBox luck,
            TextBox food,
            TextBox energy,
            TextBox money,
            TextBox reputation,
            PictureBox avatar,
            PictureBox chip,
            Button rollbutton,
            PictureBox dicecube
            )
        {           
            Button next = ButtonCreation(new Point(1350, 900), new Size(230, 110), ContentAlignment.MiddleCenter, true, textToFill, new Font("Trebuchet MS", 12F, FontStyle.Bold), Color.Black);
            next.FlatStyle = FlatStyle.System;
            next.Click += new System.EventHandler((object sender, EventArgs e) =>
            { 
                switch (clickCount)
                {
                    case 0:
                        {
                            dialogue.Text = "Да ладно?! Остался в живых после смертельной раны? Я думала, я одна такая.";                           
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Что случилось?",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();                            
                        }
                        break;
                    case 1:
                        {
                            dialogue.Text = "8 часов назад. Акция в Красной Зоне.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Я ничего не помню...",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 2:
                        {
                            dialogue.Text = "Тебя оперировал лучший врач в городе, малыш. Повезло, что только памятью отделался. Крепкий орешек.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "С кем имею честь?",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 3:
                        {
                            dialogue.Text = "Судя по тому, как спокойно ты отправился в Красную Зону, ты в городе совсем недавно, верно?";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Да. Гостеприимно тут у вас.",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 4:
                        {
                            dialogue.Text = "Я - Аи. Легенда среди фиксеров в Нео-Сити.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "...",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 5:
                        {
                            dialogue.Text = "..."+'\n'+"..."+'\n'+"Всегда пожалуйста";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Фиксеры ничего не делают просто так",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 6:
                        {
                            dialogue.Text = "А может, ты мне просто понравился";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Ага. А я - мэр Нео-Сити",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 7:
                        {
                            dialogue.Text = "Раз ты и правда ничего не помнишь: Ты спас моего лучшего наёмника. Если бы не ты, там бы он и остался. Я у тебя в долгу.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Припоминаю... Спасибо,Аи",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 8:
                        {
                            dialogue.Text = "...так что к тебе нет никаких взысканий. Но есть предложение.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Я весь внимание",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 9:
                        {
                            dialogue.Text = "Доктор сказал, что если очнешься, вживленные тебе наноботы устранят последствия операции в течение суток.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "К чему ты клонишь?",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 10:
                        {
                            dialogue.Text = "Довольно валяться. Я пробила тебя по базам: ты вёз груз, за потерю которого тебя достанет из-под земли крупная корпа";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Вообще-то шпионить - не красиво",
                               dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 11:
                        {
                            dialogue.Text = "Я - фиксер. Это моя работа. Помолчи минуту...";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "...",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 12:
                        {
                            dialogue.Text = "Можешь покинуть город - и тебя тут же накроют. А можешь работать на меня - и я тебя прикрою.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Как мило.",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 13:
                        {
                            dialogue.Text = "Не задирай нос. Не каждый день тебя берёт в штат лучший фиксер в городе";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Фиксеры. Ничего. Не делают. Просто так.",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 14:
                        {
                            dialogue.Text = "У нас общий враг. И это твои ребята. По каким причинам - не твоё дело.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Дай угадаю. Под тенью города я сам должен их убрать?",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 15:
                        {
                            dialogue.Text = "Неплохо";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "А то",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 16:
                        {
                            dialogue.Text = "Но ты еще не готов. Я помогу тебе освоиться, но нянькой тебе я не нанималась.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "...",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break; 
                    case 17:
                        {
                            dialogue.Text = "...Исследуй город, принимай правильные решения, выполняй заказы, набирай авторитет";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "...",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 18:
                        {
                            dialogue.Text = "Жизнь в Нео-сити - не отпуск на Доминикане. Он не прощает ошибок. Следи за здоровьем и постарайся не оказаться в больнице снова";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Хорошо,мама",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 19:
                        {
                            dialogue.Text = "*Слева ты можешь начать ход и посмотреть, куда тебя бросит судьба на этот раз. Сверху ты можешь увидеть,где ты находишься*";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Далее*",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 20:
                        {
                            dialogue.Text = "*Как только ты бросишь кубик ты перейдешь ровно на то же число, что выпало. Это отразится сверху*";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Далее*",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 21:
                        {
                            dialogue.Text = "*В зависимости от района и твоего авторитета ты будешь попадать в разные ситуации. Ты увидишь их ниже. Здесь тебе нужно будет принять верное решение*";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Далее*",
                               dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 22:
                        {
                            dialogue.Text = "*От этого решения будет зависеть, насколько быстро ты выберешься из города*";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Далее*",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 23:
                        {
                            dialogue.Text = "*Корпорация,охотящаяся на вас, сумела пустить в город трёх головорезов. Твоя цель - избавиться от них раньше*";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Далее*",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 24:
                        {
                            dialogue.Text = "*Но будь осторожнее - они чрезвычайно опасны. К битве с ними - максимальная подготовленность*";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Далее*",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 25:
                        {
                            dialogue.Text = "Я замолвлю фиксерам словечко за тебя, а дальше ты сам по себе.";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "Спасибо, Аи",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    case 26:
                        {
                            dialogue.Text = "Держи ствол на предохранителе. Мы еще встретимся";
                            BeginReplics(
                                ++clickCount,
                                dialogue,
                                "*Кивнуть*",
                                dAvatar,
                                _situationTB,
                                situationField,
                                stats,
                                infobox,
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                            );
                            next.Dispose();
                        }
                        break;
                    default:
                        {
                            dialogue.Dispose();
                            next.Dispose();
                            dAvatar.Visible = false;
                            _situationTB.Visible = true;
                            situationField.Image = Image.FromFile("Game\\doctor.jpg");
                            AfterReplics(
                                0,
                                stats,
                                _situationTB,
                                infobox,
                                situationField,                                
                                strength,
                                sneakness,
                                luck,
                                food,
                                energy,
                                money,
                                reputation,
                                avatar,
                                chip,
                                rollbutton,
                                dicecube
                                );
                        }
                        break;
                }
                
            });
        }
        public void AfterReplics(int position,int[]currentStats, TextBox sTB, TextBox infobox, PictureBox situationField, TextBox strength, TextBox sneakness, TextBox luck, TextBox food, TextBox energy, TextBox money, TextBox reputation, PictureBox avatar, PictureBox chip, Button rollbutton, PictureBox dicecube)
        {
            Situation firstOne = new Situation(position, sTB, infobox);
            firstOne.text = "Аи удалилась, а ты уже начал чувствовать себя лучше. Доктор дал последние рекомендации и сказал, что ты можешь идти. Ты догадался, что твоё лечение уже оплачено, но может стоит поблагодарить доктора лично?";
            firstOne.solutions.Add(
                new Solution(
                    ButtonCreation(
                        new Point(600, 680),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "Да, отблагодарить доктора и предложить ему оплату",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black),
                        Solution.sType.goodSolution,
                        new int[] { 0, 0, 0, 0, 0, 10, 0 },
                        new int[] { 0, 0, 0, 0, 0, 10, 1 },
                        currentStats,
                        sTB,
                        CreateRequirementsTB(680),
                        CreateRequirementsPics(680)
                        )
                    { resultText = "Доктор рассмеялся, услышав твоё предложение. Похлопав по плечу, он похвалил тебя за такой подход, но от денег вежливо отказался." }
                );
            firstOne.solutions.Add(
                new Solution(
                    ButtonCreation(
                        new Point(600, 790),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "Сказать спасибо и удалиться",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black),
                        Solution.sType.neutralSolution,
                        new int[] { 0, 0, 0, 0, 0, 0, 0 },
                        new int[] { 0, 0, 0, 0, 0, 0, 0 },
                        currentStats,
                        sTB,
                        CreateRequirementsTB(790),
                        CreateRequirementsPics(790)
                        )
                    { resultText = "Доктор кивнул в твою сторону и продолжил заниматься своими делами." }
                );
            firstOne.solutions.Add(
                new Solution(
                    ButtonCreation(
                        new Point(600, 900),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "Встать и молча уйти",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black),
                        Solution.sType.badSolution,
                        new int[] { 0, 0, 0, 0, 0, 0, 0 },
                        new int[] { 0, 0, 0, 0, 0, 0, -1 },
                        currentStats,
                        sTB,
                        CreateRequirementsTB(900),
                        CreateRequirementsPics(900)
                        )
                    { resultText = "По лёгкому удивлению доктора можно было понять, что он ждал хотя-бы благодарности. Да, ты ему ничего не должен, но всё-таки, он жизнь тебе спас" }
                );
            firstOne.MeetSituation();
            firstOne.ChooseSolution(0, new Player());
            firstOne.solutions[1].solutionButton.Disposed += new System.EventHandler((object sender, EventArgs e) =>
            {
                Button Continue = ButtonCreation(
                    new Point(600, 680),
                    new Size(940, 100),
                    ContentAlignment.MiddleCenter,
                    true,
                    "Proceed",
                    new Font("Trebuchet MS", 15F, FontStyle.Bold),
                    Color.Black);
                Continue.Click += new System.EventHandler((a,b) =>
                {
                    strength.Dispose();
                    sneakness.Dispose();
                    luck.Dispose();
                    food.Dispose();
                    energy.Dispose();
                    money.Dispose();
                    reputation.Dispose();
                    avatar.Dispose();
                    chip.Dispose();
                    firstOne.InfoWarn();
                    foreach (Solution s in firstOne.solutions)
                    {
                        foreach (PictureBox pb in s.requirementStatsPics)
                            pb.Dispose();
                        foreach (TextBox tb in s.requirementsTBs)
                            tb.Dispose();
                    }
                    TurnEnd(infobox, firstOne.statsToReturn, situationField, Continue, firstOne.textField, firstOne, new Player(), dicecube, rollbutton);
                });
            });
           
        }
          void TurnEnd(TextBox infobox, int[] statsToSetResult, PictureBox situationField, Button Continue,TextBox situationTB, Situation situation, Player player, PictureBox dicecube, Button rollbutton)
        {
            situationField.Image = Image.FromFile("Game\\SolutionBG.jpg");
            Continue.Dispose();
            situationTB.Visible = false;
            if (situation.firstFlag)
            {
                 player = new Player(1, 1, 1, 50, 35, 60, 0,
                    PicCreation(
                        new Point(15, 10),
                        new Size(135, 175),
                        PictureBoxSizeMode.StretchImage,
                        Image.FromFile("Game\\normal.jpeg"),
                        true),
                    PicCreation(
                        new Point(355, 370),
                        new Size(35, 35),
                        PictureBoxSizeMode.StretchImage,
                        Image.FromFile("Game\\chip.jpg"),
                        true),
                    this,
                    PicCreation(new Point(0, 0), new Size(1900, 1040), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\lostpic.jpg"), true),
                    rollbutton
                    );
                player.Initialize(this);
            }
            player = player.UpdateStats(statsToSetResult);
            player.RepCheck();
            if (player.DeathCheck())
                player.IfDead();
            infobox.Text += Environment.NewLine + "Ход окончен, сделайте следующий" + "\n" + "<--";
            dicecube = PicCreation(new Point(70, 485), new Size(160, 160), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\dice6.png"), true);
            rollbutton = ButtonCreation(new Point(50, 725), new Size(200, 75), ContentAlignment.MiddleCenter, true, "ROLL", new Font("Trebuchet MS", 20F, FontStyle.Bold), Color.Black);
            rollbutton.Click += new System.EventHandler((a, b) =>
            {
                rollbutton.Enabled = false;
                Turn(dicecube, player, situationTB,situationField,rollbutton);
            });
        }
        void Turn(PictureBox dicecube, Player player,TextBox situationTB, PictureBox situationField, Button rollbutton)
        {
            situationTB.Visible = true;
            int step = CubeAnimation(dicecube);
            infobox.Text += Environment.NewLine + player.Walk(step);
            position.Text = $"Текущая позиция : {player.currentPos}";
            lastStep.Text = $"Последний ход : {step}";
            Situation situation = new Situation(
                player.currentPos,
                situationTB,
                situationField,
                ButtonCreation(
                        new Point(600, 680),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black                        
                        ),
                 ButtonCreation(
                        new Point(600, 790),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black),
                  ButtonCreation(
                        new Point(600, 900),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black),
                  player,
                  CreateRequirementsTB(680),
                  CreateRequirementsPics(680),
                  CreateRequirementsTB(790),
                  CreateRequirementsPics(790),
                  CreateRequirementsTB(900),
                  CreateRequirementsPics(900),
                  infobox,
                  PicCreation(new Point(1590 - 5, 425), new Size(200, 250), PictureBoxSizeMode.StretchImage, Image.FromFile("Game\\Fixer.jpg"), true),
                  TBCreation(new Point(1585, 665), new Size(200, 350), HorizontalAlignment.Center, true, new Font("Trebuchet MS", 16F, FontStyle.Bold), Color.Black, Color.White)
                );
            if (situation.thisType != Situation.situationType.BOSS)
            {
                situation.solutions[0].solutionButton.Disposed += new System.EventHandler((object sender, EventArgs e) =>
                {
                    Button Continue = ButtonCreation(
                        new Point(600, 680),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "Proceed",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black);
                    Continue.Click += new System.EventHandler((a, b) =>
                    {
                        situation.InfoWarn();
                        foreach (Solution s in situation.solutions)
                        {
                            foreach (PictureBox pb in s.requirementStatsPics)
                                pb.Dispose();
                            foreach (TextBox tb in s.requirementsTBs)
                                tb.Dispose();
                        }
                        TurnEnd(infobox, situation.statsToReturn, situationField, Continue, situation.textField, situation, player, dicecube, rollbutton);
                    });
                });
                if (situation.thisType == Situation.situationType.FreeStats || situation.thisType == Situation.situationType.Unlucky)
                    situation.solutions[0].solutionButton.Dispose();
            }
            else
            {
                for (int i=0; i<4;i++)
                {
                    situation.reqPicsBF.Add(CreateRequirementsPics(680));
                    situation.reqPicsBF.Add(CreateRequirementsPics(790));
                    situation.reqPicsBF.Add(CreateRequirementsPics(900));
                    situation.reqTBsBF.Add(CreateRequirementsTB(680));
                    situation.reqTBsBF.Add(CreateRequirementsTB(790));
                    situation.reqTBsBF.Add(CreateRequirementsTB(900));
                    situation.BFButtons.Add(ButtonCreation(
                        new Point(600, 680),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black
                        ));
                    situation.BFButtons.Add(
                        ButtonCreation(
                        new Point(600, 790),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black));
                    situation.BFButtons.Add(
                        ButtonCreation(
                        new Point(600, 900),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black));
                    situation.BFListButs.Add(
                        ButtonCreation(
                        new Point(600, 680),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "Proceed",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black
                        ));
                    situation.ListButtons.Add(
                        ButtonCreation(
                        new Point(1350, 900),
                        new Size(230, 110),
                        ContentAlignment.MiddleCenter,
                        true,
                        "",
                        new Font("Trebuchet MS", 12F, FontStyle.Bold),
                        Color.Black
                        ));
                }
                foreach (Button sb in situation.ListButtons)
                    sb.Visible = false;
                foreach (List<PictureBox> lPb in situation.reqPicsBF)
                    foreach (PictureBox pb in lPb)
                        pb.Visible = false;
                foreach (List<TextBox> lTb in situation.reqTBsBF)
                    foreach (TextBox tb in lTb)
                        tb.Visible = false;
                foreach (Button sb in situation.BFButtons)
                    sb.Visible = false;
                foreach (Button sb in situation.BFListButs)
                    sb.Visible = false;
                situation.MeetSituation();
                situation.continueButton = ButtonCreation(
                        new Point(600, 680),
                        new Size(940, 100),
                        ContentAlignment.MiddleCenter,
                        true,
                        "Proceed",
                        new Font("Trebuchet MS", 15F, FontStyle.Bold),
                        Color.Black);
                situation.continueButton.Visible = false;
                situation.FightOffer(player);
                situation.continueButton.Click += new System.EventHandler((a, b) =>
                {
                    if (player.bossCount >= 3)
                        player.IfWon();
                    situation.InfoWarn();
                    foreach (Solution s in situation.solutions)
                    {
                        foreach (PictureBox pb in s.requirementStatsPics)
                            pb.Dispose();
                        foreach (TextBox tb in s.requirementsTBs)
                            tb.Dispose();
                        foreach (List<PictureBox> lPb in situation.reqPicsBF)
                            foreach (PictureBox pb in lPb)
                                pb.Dispose();
                        foreach (List<TextBox> lTb in situation.reqTBsBF)
                            foreach (TextBox tb in lTb)
                                tb.Dispose();
                        foreach (Button sb in situation.BFButtons)
                            sb.Dispose();
                    }
                    TurnEnd(infobox, situation.statsToReturn, situationField, situation.continueButton, situation.textField, situation, player, dicecube, rollbutton);
                });
            }
        }
        int CubeAnimation(PictureBox whatToAnimate)
        {
            int picture = 0;
            var rand = new Random();
            void startTimer()
            {                               
                Thread t = new Thread(new ThreadStart(ChangePic));
                t.Start();
            }

            void ChangePic()
            {
                for (int i=0; i<10;i++)
                {
                    Thread.Sleep(100);
                    whatToAnimate.Invoke(new Action(() =>
                    {
                        picture = rand.Next(1, 7);
                        switch(picture)
                        {
                            case 1: whatToAnimate.Image = Image.FromFile("Game\\dice1.png"); break;
                            case 2: whatToAnimate.Image = Image.FromFile("Game\\dice2.png"); break;
                            case 3: whatToAnimate.Image = Image.FromFile("Game\\dice3.png"); break;
                            case 4: whatToAnimate.Image = Image.FromFile("Game\\dice4.png"); break;
                            case 5: whatToAnimate.Image = Image.FromFile("Game\\dice5.png"); break;
                            case 6: whatToAnimate.Image = Image.FromFile("Game\\dice6.png"); break;
                        }
                    }));
                }                
            }

            startTimer();
            picture = rand.Next(1, 7);

            switch (picture)
            {
                case 1: whatToAnimate.Image = Image.FromFile("Game\\dice1.png"); break;
                case 2: whatToAnimate.Image = Image.FromFile("Game\\dice2.png"); break;
                case 3: whatToAnimate.Image = Image.FromFile("Game\\dice3.png"); break;
                case 4: whatToAnimate.Image = Image.FromFile("Game\\dice4.png"); break;
                case 5: whatToAnimate.Image = Image.FromFile("Game\\dice5.png"); break;
                case 6: whatToAnimate.Image = Image.FromFile("Game\\dice6.png"); break;
            }

            return picture;
        }
        
    }
}
//Передавать плеера в реплики
//Энейблд роллбаттон тру вместо пересоздания
//Оптимизируй конструктор ситуации
