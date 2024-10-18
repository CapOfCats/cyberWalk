using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovik2
{
     class Situation
    {
        public static Dictionary<situationType, List<string>> textCache = new Dictionary<situationType, List<string>>();
        public static Dictionary<situationType, List<System.Drawing.Image>> backgroundCache = new Dictionary<situationType, List<System.Drawing.Image>>();
        public static Dictionary<bossType, List<string>> bossPhrases = new Dictionary<bossType, List<string>>();
        public static Dictionary<bossType, List<string>> bossSituationsTexts = new Dictionary<bossType, List<string>>();
        public situationType thisType { get; }
        public TextBox textField;
        PictureBox field;
        TextBox infoField;
        public List<Button> BFListButs = new List<Button>();
        Situation motherSituation;
        public string text { get; set; }
        public List<Solution> solutions = new List<Solution>();
        public int[] statsToReturn;
        public bool firstFlag =false;
        bossType bt;
        PictureBox dialoguePerson;
        TextBox dialogue;
        Button listDialogue;
        List<Situation> BFSituations = new List<Situation>();
        public Button continueButton;
        public List<List<PictureBox>> reqPicsBF = new List<List<PictureBox>>();
        public List<List<TextBox>> reqTBsBF = new List<List<TextBox>>();
        public List<Button> BFButtons = new List<Button>();
        public List<Button> ListButtons = new List<Button>();

        public static situationType[] sitLocation = new situationType[]
       {
            situationType.Good,
            situationType.Slipy,
            situationType.Bad,
            situationType.Slipy,
            situationType.FreeStats,
            situationType.Exchange,
            situationType.Good,
            situationType.Unlucky,
            situationType.Bad,
            situationType.Slipy,
            situationType.Good,
            situationType.Bad,
            situationType.BOSS
       };

        public Situation(
            int pos,
            TextBox _textField,
            PictureBox _field,
            Button b1,
            Button b2,
            Button b3,
            Player player,
            List<TextBox> reqTBs1,
            List<PictureBox> reqPics1,
            List<TextBox> reqTBs2,
            List<PictureBox> reqPics2,
            List<TextBox> reqTBs3,
            List<PictureBox> reqPics3,
            TextBox _infofield,
            PictureBox _person,
            TextBox _dialogue
            )
        {
            dialoguePerson = _person;
            dialogue = _dialogue;

            //listDialogue = _ToSpeak;
            dialoguePerson.Visible = false;
            dialogue.Visible = false;
            //listDialogue.Visible = false;
            thisType = sitLocation[pos];
            textField = _textField;
            field = _field;
            infoField = _infofield;
            var rand = new Random();
            switch (thisType)
            {
                case situationType.Good:
                    {
                        int number = rand.Next(1, 16);
                        if (player.reputation>10)
                            text = textCache[situationType.GoodEx][number - 1];
                        else
                            text = textCache[thisType][number-1];
                        field.Image = backgroundCache[thisType][number-1];
                        solutions.Add(new Solution(
                            b1,
                            Solution.sType.goodSolution,
                            Solution.requirementsCache[thisType][number * 3 - 3].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 3].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 3],
                            player,
                            textField,
                            reqTBs1,
                            reqPics1,                            
                            Solution.solutionTextCache[thisType][number * 3 - 3]
                        ));
                        solutions.Add(new Solution(
                            b2,
                            Solution.sType.neutralSolution,
                            Solution.requirementsCache[thisType][number * 3 - 2].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 2].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 2],
                            player,
                            textField,
                            reqTBs2,
                            reqPics2,
                            Solution.solutionTextCache[thisType][number * 3 - 2]
                        ));
                        solutions.Add(new Solution(
                            b3,
                            Solution.sType.badSolution,
                            Solution.requirementsCache[thisType][number * 3 - 1].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 1].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 1],
                            player,
                            textField,
                            reqTBs3,
                            reqPics3,
                            Solution.solutionTextCache[thisType][number * 3 - 1]
                        ));
                        MeetSituation();
                        ChooseSolution(0, player);
                    }
                    break;
                case situationType.Slipy:
                    {
                        int number = rand.Next(1, 16);
                        if(player.reputation >= -10 && player.reputation <= 10)
                            text = textCache[situationType.SlipyEx][number - 1];
                        else
                            text = textCache[thisType][number-1];
                        field.Image = backgroundCache[thisType][number - 1];
                        solutions.Add(new Solution(
                            b1,
                            Solution.sType.goodSolution,
                            Solution.requirementsCache[thisType][number * 3 - 3].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 3].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 3],
                            player,
                            textField,
                            reqTBs1,
                            reqPics1,
                            Solution.solutionTextCache[thisType][number * 3 - 3]
                        ));
                        solutions.Add(new Solution(
                            b2,
                            Solution.sType.neutralSolution,
                            Solution.requirementsCache[thisType][number * 3 - 2].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 2].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 2],
                            player,
                            textField,
                            reqTBs2,
                            reqPics2,
                            Solution.solutionTextCache[thisType][number * 3 - 2]
                        ));
                        solutions.Add(new Solution(
                            b3,
                            Solution.sType.badSolution,
                            Solution.requirementsCache[thisType][number * 3 - 1].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 1].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 1],
                            player,
                            textField,
                            reqTBs3,
                            reqPics3,
                            Solution.solutionTextCache[thisType][number * 3 - 1]
                        ));
                        MeetSituation();
                        ChooseSolution(0, player);
                    }break;
                case situationType.Bad:
                    {
                        int number = rand.Next(1, 16);
                        if(player.reputation < -10)
                            text = textCache[situationType.BadEx][number - 1];
                        else
                            text = textCache[thisType][number - 1];
                        field.Image = backgroundCache[thisType][number - 1];
                        solutions.Add(new Solution(
                            b1,
                            Solution.sType.goodSolution,
                            Solution.requirementsCache[thisType][number * 3 - 3].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 3].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 3],
                            player,
                            textField,
                            reqTBs1,
                            reqPics1,
                            Solution.solutionTextCache[thisType][number * 3 - 3]
                        ));
                        solutions.Add(new Solution(
                            b2,
                            Solution.sType.neutralSolution,
                            Solution.requirementsCache[thisType][number * 3 - 2].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 2].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 2],
                            player,
                            textField,
                            reqTBs2,
                            reqPics2,
                            Solution.solutionTextCache[thisType][number * 3 - 2]
                        ));
                        solutions.Add(new Solution(
                            b3,
                            Solution.sType.badSolution,
                            Solution.requirementsCache[thisType][number * 3 - 1].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number * 3 - 1].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number * 3 - 1],
                            player,
                            textField,
                            reqTBs3,
                            reqPics3,
                            Solution.solutionTextCache[thisType][number * 3 - 1]
                        ));
                        MeetSituation();
                        ChooseSolution(0, player);
                    }
                    break;
                case situationType.Exchange:
                {
                        int number = rand.Next(1, 8);
                        text = textCache[thisType][number - 1];
                        field.Image = Image.FromFile("Game\\exch.jpg");
                        solutions.Add(new Solution(
                            b1,
                            Solution.sType.Exchange,
                            Solution.requirementsCache[thisType][number-1].Split(',').Select(int.Parse).ToArray(),
                            Solution.rewardsCache[thisType][number-1].Split(',').Select(int.Parse).ToArray(),
                            Solution.resultCache[thisType][number-1],
                            player,
                            textField,
                            reqTBs1,
                            reqPics1,
                            "Согласиться"
                        ));
                        solutions.Add(new Solution(
                            b2,
                            Solution.sType.Exchange,
                            new int[] { 0, 0, 0, 0, 0, 0, 0 },
                            new int[] { 0, 0, 0, 0, 0, 0, 0 },
                            "Вы отказались",
                            player,
                            textField,
                            reqTBs2,
                            reqPics2,
                            "Игнорировать"
                        ));
                        foreach (PictureBox pb in reqPics3)
                            pb.Dispose();
                        foreach (TextBox tb in reqTBs3)
                            tb.Dispose();
                        b3.Dispose();
                        MeetSituation();
                        ChooseSolution(0,player);
                    }
                    break;
                case situationType.FreeStats:
                    {
                        int number = rand.Next(1, 8);
                        text = textCache[thisType][number - 1];
                        field.Image = Image.FromFile("Game\\freeS.jpg");
                        statsToReturn = Solution.rewardsCache[thisType][number - 1].Split(',').Select(int.Parse).ToArray();
                        solutions.Add(new Solution(
                            b1,
                            Solution.sType.FS,
                            new int[] { 0, 0, 0, 0, 0, 0, 0 },
                            Solution.rewardsCache[thisType][number - 1].Split(',').Select(int.Parse).ToArray(),
                            null,
                            player,
                            textField,
                            reqTBs1,
                            reqPics1,
                            null
                        ));
                        foreach (PictureBox pb in reqPics3)
                            pb.Dispose();
                        foreach (TextBox tb in reqTBs3)
                            tb.Dispose();
                        foreach (PictureBox pb in reqPics2)
                            pb.Dispose();
                        foreach (TextBox tb in reqTBs2)
                            tb.Dispose();
                        foreach (PictureBox pb in solutions[0].requirementStatsPics)
                            pb.Dispose();
                        foreach (TextBox tb in solutions[0].requirementsTBs)
                            tb.Dispose();
                        MeetSituation();
                        statsToReturn = solutions[0].AcceptSolution(thisType);
                        b3.Dispose();
                        b2.Dispose();
                        b1.Dispose();
                    }
                    break;
                case situationType.Unlucky:
                    {
                        int number = rand.Next(1, 8);
                        text = textCache[situationType.Unlucky][number - 1];
                        field.Image = Image.FromFile("Game\\unluck.jpg");
                        statsToReturn = Solution.rewardsCache[thisType][number - 1].Split(',').Select(int.Parse).ToArray();
                        solutions.Add(new Solution(
                           b1,
                           Solution.sType.Unluck,
                           new int[] { 0, 0, 0, 0, 0, 0, 0 },
                           Solution.rewardsCache[thisType][number - 1].Split(',').Select(int.Parse).ToArray(),
                           null,
                           player,
                           textField,
                           reqTBs1,
                           reqPics1,
                           null
                       ));
                        foreach (PictureBox pb in reqPics3)
                            pb.Dispose();
                        foreach (TextBox tb in reqTBs3)
                            tb.Dispose();
                        foreach (PictureBox pb in reqPics2)
                            pb.Dispose();
                        foreach (TextBox tb in reqTBs2)
                            tb.Dispose();
                        foreach (PictureBox pb in solutions[0].requirementStatsPics)
                            pb.Dispose();
                        foreach (TextBox tb in solutions[0].requirementsTBs)
                            tb.Dispose();
                        MeetSituation();
                        statsToReturn = solutions[0].AcceptSolution(thisType);
                        b3.Dispose();
                        b2.Dispose();
                        b1.Dispose();
                    }
                    break;
                case situationType.BOSS:
                    {
                        text = textCache[thisType][player.bossCount]; 
                        field.Image = Image.FromFile($"Game\\boss{player.bossCount + 1}.jpg");                                                                           
                        solutions.Add(new Solution(
                            b1,
                            Solution.sType.neutralSolution,
                            Solution.requirementsCache[thisType][player.bossCount].Split(',').Select(int.Parse).ToArray(),
                            new int[] { 0, 0, 0, 0, 0, 0, 0 },
                            "...",
                            player,
                            textField,
                            reqTBs1,
                            reqPics1,
                            "Покончим с этим."
                        ));
                        solutions.Add(new Solution(
                            b2,
                            Solution.sType.neutralSolution,
                            new int[] { 0, 0, 0, 0, 0, 0, 0 },
                            new int[] { 0, 0, 0, 0, 0, 0, 0 },
                            "Ты решил, что пока не готов к этой битве",
                            player,
                            textField,
                            reqTBs2,
                            reqPics2,
                            "Избежать"
                        ));                        
                        foreach (PictureBox pb in reqPics3)
                            pb.Dispose();
                        foreach (TextBox tb in reqTBs3)
                            tb.Dispose();
                        b3.Dispose();
                    }
                    break;
            }
        }

        public Situation(int pos, TextBox _textField, TextBox _infobox)
        {
            thisType = sitLocation[pos];
            textField = _textField;
            infoField = _infobox;
            firstFlag = true;
        }
        public Situation(            
            TextBox _textField,
            Button b1,
            Button b2,
            Button b3,
            List<TextBox> reqTBs1,
            List<PictureBox> reqPics1,
            List<TextBox> reqTBs2,
            List<PictureBox> reqPics2,
            List<TextBox> reqTBs3,
            List<PictureBox> reqPics3,
            TextBox _infofield,
            PictureBox _person,
            TextBox _dialogue,
            int startIndex,
            bossType _BT,
            Player player,
            situationType st,
            Situation _motherSituation,
            Button _continueButton            
            )
        {
            continueButton = _continueButton;
            motherSituation = _motherSituation;
            bt = _BT;
            thisType = st;
            dialoguePerson = _person;
            dialogue = _dialogue;
            dialoguePerson.Visible = false;
            dialogue.Visible = false;
            textField = _textField;
            infoField = _infofield;
            text = bossSituationsTexts[_BT][startIndex/3];
            b1.Visible = true;
            b2.Visible = true;
            b3.Visible = true;
            foreach (PictureBox pb in reqPics1)
                pb.Visible = true;
            foreach (PictureBox pb in reqPics2)
                pb.Visible = true;
            foreach (PictureBox pb in reqPics3)
                pb.Visible = true;
            foreach (TextBox tb in reqTBs1)
                tb.Visible = true;
            foreach (TextBox tb in reqTBs2)
                tb.Visible = true;
            foreach (TextBox tb in reqTBs3)
                tb.Visible = true;
            solutions.Add(new Solution(
                b1,
                Solution.sType.BOSSFight,
                Solution.BFRequirementsCache[_BT][startIndex].Split(',').Select(int.Parse).ToArray(),
                Solution.BFRewardsCache[_BT][startIndex].Split(',').Select(int.Parse).ToArray(),
                Solution.BFResultCache[_BT][startIndex],
                player,
                textField,
                reqTBs1,
                reqPics1,
                Solution.BFSolutionTextCache[_BT][startIndex]
            ));
            solutions.Add(new Solution(
                b2,
                Solution.sType.BOSSFight,
                Solution.BFRequirementsCache[_BT][startIndex + 1].Split(',').Select(int.Parse).ToArray(),
                Solution.BFRewardsCache[_BT][startIndex + 1].Split(',').Select(int.Parse).ToArray(),
                Solution.BFResultCache[_BT][startIndex + 1],
                player,
                textField,
                reqTBs2,
                reqPics2,
                Solution.BFSolutionTextCache[_BT][startIndex + 1]
            ));
            solutions.Add(new Solution(
                b3,
                Solution.sType.BOSSFight,
                Solution.BFRequirementsCache[_BT][startIndex + 2].Split(',').Select(int.Parse).ToArray(),
                Solution.BFRewardsCache[_BT][startIndex + 2].Split(',').Select(int.Parse).ToArray(),
                Solution.BFResultCache[_BT][startIndex + 2],
                player,
                textField,
                reqTBs3,
                reqPics3,
                Solution.BFSolutionTextCache[_BT][startIndex + 2]
            ));
            MeetSituation();
            ChooseSolution(startIndex, player);
        }
        public enum situationType
        {
            Good,
            Bad,
            Slipy,
            FreeStats,
            Exchange,
            Unlucky,
            GoodEx,
            BadEx,
            SlipyEx,
            BOSS
        }

        public enum bossType
        {
            boss1,
            boss2,
            boss3
        }

        public void MeetSituation()
        {
            
            bool flag = false;
            //List<Solution> solutionsToEnable = solutions;
            int _charIndex;
            void startTimer()
            {
                _charIndex = 0;
                textField.Text = string.Empty;
               Thread t = new Thread(new ThreadStart(TypewriteText));
                t.Start();
            }

            void TypewriteText()
            {
                while (_charIndex < text.Length)
                {
                    Thread.Sleep(20);
                    textField.Invoke(new Action(() =>
                    {
                        textField.Text += text[_charIndex];
                    }));
                    _charIndex++;
                }                  
            }
            for (int i = 0; i < 3; i++)
                if (solutions.Count >= i + 1)
                    solutions[i].EnableButton();
            startTimer();
            
            //foreach (Solution s in solutionsToEnable)
            //    s.solutionButton.Enabled = true;
            //var currentTexts = textCache[type];
        }
        

        public void ChooseSolution(int bossIndex, Player player)
        {  
            for (int i=0; i<solutions.Count;i++)
                solutions[i].AccessCheck();
            if (solutions.Count > 0)
                solutions[0].solutionButton.Click += new System.EventHandler((object sender, EventArgs e) =>
            {
                statsToReturn = solutions[0].AcceptSolution(thisType);
                foreach (Solution s in solutions)
                    s.solutionButton.Dispose();
                if (solutions[0].thisType == Solution.sType.BOSSFight)
                {
                    continueButton.Visible = true;
                    continueButton.Click += new System.EventHandler((a, b) =>
                    {
                        InfoWarn();
                        foreach (Solution s in solutions)
                        {
                            foreach (PictureBox pb in s.requirementStatsPics)
                                pb.Dispose();
                            foreach (TextBox tb in s.requirementsTBs)
                                tb.Dispose();
                        }
                        continueButton.Dispose();
                        player = player.UpdateStats(statsToReturn);
                        if (player.DeathCheck())
                            player.IfDead();
                        player.RepCheck();
                        motherSituation.BeginFight(bossIndex + 3, bt, player);
                    });
                }                
            });
            if (solutions.Count > 1)
                solutions[1].solutionButton.Click += new System.EventHandler((object sender, EventArgs e) =>
            {
                statsToReturn = solutions[1].AcceptSolution(thisType);
                foreach (Solution s in solutions)
                    s.solutionButton.Dispose();
                if (solutions[1].thisType == Solution.sType.BOSSFight)
                {
                    continueButton.Visible = true;
                    continueButton.Click += new System.EventHandler((a, b) =>
                    {
                        InfoWarn();
                        foreach (Solution s in solutions)
                        {
                            foreach (PictureBox pb in s.requirementStatsPics)
                                pb.Dispose();
                            foreach (TextBox tb in s.requirementsTBs)
                                tb.Dispose();
                        }
                        continueButton.Dispose();
                        player = player.UpdateStats(statsToReturn);
                        if (player.DeathCheck())
                            player.IfDead();
                        player.RepCheck();
                        motherSituation.BeginFight(bossIndex + 3, bt, player);
                    });
                }
            });

            if(solutions.Count > 2)
                solutions[2].solutionButton.Click += new System.EventHandler((object sender, EventArgs e) =>
                {
                    statsToReturn = solutions[2].AcceptSolution(thisType);                    
                    foreach (Solution s in solutions)
                        s.solutionButton.Dispose();
                    if (solutions[2].thisType == Solution.sType.BOSSFight)
                    {
                        continueButton.Visible = true;
                        continueButton.Click += new System.EventHandler((a, b) =>
                        {
                            InfoWarn();
                            foreach (Solution s in solutions)
                            {
                                foreach (PictureBox pb in s.requirementStatsPics)
                                    pb.Dispose();
                                foreach (TextBox tb in s.requirementsTBs)
                                    tb.Dispose();
                            }
                            continueButton.Dispose();
                            player = player.UpdateStats(statsToReturn);
                            if (player.DeathCheck())
                                player.IfDead();
                            player.RepCheck();
                            motherSituation.BeginFight(bossIndex + 3, bt, player);
                        });
                    }
                });
        }

        public void InfoWarn()
        {
            for( int i=0;i<7;i++)
                if(statsToReturn[i]!=0)
                    switch(i)
                    {
                        case 0: infoField.Text += Environment.NewLine + $"Параметр Силы изменился на {statsToReturn[i]}"; break;
                        case 1: infoField.Text += Environment.NewLine + $"Параметр Пронырливости изменился на {statsToReturn[i]}"; break;
                        case 2: infoField.Text += Environment.NewLine + $"Параметр Удачи изменился на {statsToReturn[i]}"; break;
                        case 3: infoField.Text += Environment.NewLine + $"Параметр Сытости изменился на {statsToReturn[i]}"; break;
                        case 4: infoField.Text += Environment.NewLine + $"Параметр Энергии изменился на {statsToReturn[i]}"; break;
                        case 5: infoField.Text += Environment.NewLine + $"Параметр Денег изменился на {statsToReturn[i]}"; break;
                        case 6: infoField.Text += Environment.NewLine + $"Репутация изменилась на {statsToReturn[i]}"; break;
                    }
        }
        public void FightOffer(Player player)
        {
            for (int i = 0; i < solutions.Count; i++)
                solutions[i].AccessCheck();
            if (solutions.Count > 0)
                solutions[0].solutionButton.Click += new System.EventHandler((object sender, EventArgs e) =>
                {
                    statsToReturn = solutions[0].AcceptSolution(thisType);
                    BossDialogue(player);
                    foreach (Solution s in solutions)
                        s.solutionButton.Dispose();
                    foreach (Solution s in solutions)
                    {
                        foreach (PictureBox pb in s.requirementStatsPics)
                            pb.Dispose();
                        foreach (TextBox tb in s.requirementsTBs)
                            tb.Dispose();
                    }
                });
            if (solutions.Count > 1)
                solutions[1].solutionButton.Click += new System.EventHandler((object sender, EventArgs e) =>
                {
                    continueButton.Visible = true;
                    statsToReturn = solutions[1].AcceptSolution(thisType);
                    foreach (Solution s in solutions)
                        s.solutionButton.Dispose();
                    foreach (Solution s in solutions)
                    {
                        foreach (PictureBox pb in s.requirementStatsPics)
                            pb.Dispose();
                        foreach (TextBox tb in s.requirementsTBs)
                            tb.Dispose();
                    }
                });
        }
       
        void BossDialogue(Player player)
        {
            dialogue.Visible = true;
            dialoguePerson.Visible = true;
            listDialogue = ListButtons[0];
            listDialogue.Visible = true;
            bossType BT;
            switch (player.bossCount)
            {
                case 0:
                    {
                        BT = bossType.boss1;
                        dialoguePerson.Image = Image.FromFile($"Game\\boss{player.bossCount + 1}.jpg");
                        listDialogue.Text = "...";
                        dialogue.Text = "...";                        
                        BossDialogueSwitch(0, dialogue, dialoguePerson,ListButtons, BT, player);    
                    }
                    break;
                case 1:
                    {
                        BT = bossType.boss2;
                        dialoguePerson.Image = Image.FromFile($"Game\\boss{player.bossCount + 1}.jpg");
                        listDialogue.Text = "...";
                        dialogue.Text = "...";
                        BossDialogueSwitch(0, dialogue, dialoguePerson, ListButtons, BT, player);
                    }
                    break;
                case 2:
                    {
                        BT = bossType.boss3;
                        dialoguePerson.Image = Image.FromFile($"Game\\boss{player.bossCount + 1}.jpg");
                        listDialogue.Text = "...";
                        dialogue.Text = "...";
                        BossDialogueSwitch(0, dialogue, dialoguePerson, ListButtons, BT, player);
                    }
                    break;
            }
        }
        void BossDialogueSwitch(int replicsCount, TextBox dialogue, PictureBox dialoguePerson, List<Button> listDialogue, bossType BT, Player player)
        {
            listDialogue[replicsCount].Visible = true;
            listDialogue[replicsCount].Click += new System.EventHandler((object sender, EventArgs e) =>
            {
                switch (replicsCount)
                {
                    case 0:
                        {
                            dialogue.Text = bossPhrases[BT][replicsCount];
                            listDialogue[replicsCount].Dispose();
                            BossDialogueSwitch(++replicsCount, dialogue, dialoguePerson, listDialogue,BT, player);
                            
                        }
                        break;
                    case 1:
                        {
                            dialogue.Text = bossPhrases[BT][replicsCount];
                            listDialogue[replicsCount].Dispose();
                            BossDialogueSwitch(++replicsCount, dialogue, dialoguePerson, listDialogue,BT,player);
                            
                        }
                        break;
                    case 2:
                        {
                            dialogue.Text = bossPhrases[BT][replicsCount];
                            listDialogue[replicsCount].Dispose();
                            BossDialogueSwitch(++replicsCount, dialogue, dialoguePerson, listDialogue, BT, player);
                            
                        }
                        break;
                    default:
                        {
                            dialogue.Dispose();
                            dialoguePerson.Dispose();
                            listDialogue[replicsCount].Dispose();
                            BeginFight(0, BT, player);
                        }
                        break;
                }
            });
        }
        void BeginFight(int startIndex, bossType BT, Player player)
        {
            if(startIndex>9)
            {
                continueButton.Visible = true;
                player.bossCount++;
            }
            else
                BFSituations.Add(new Situation(
                textField,
                BFButtons[startIndex],
                BFButtons[startIndex+1],
                BFButtons[startIndex+2],
                reqTBsBF[startIndex],
                reqPicsBF[startIndex],
                reqTBsBF[startIndex+1],
                reqPicsBF[startIndex+1],
                reqTBsBF[startIndex+2],
                reqPicsBF[startIndex+2],
                infoField,
                dialoguePerson,
                dialogue,
                startIndex,
                BT, 
                player,
                situationType.BOSS,
                this,
                BFListButs[startIndex/3]
                ));
        }
      
        public static void ReadCaches()
        {
            List<string> situationsTexts = new List<string>();           
            StreamReader sr = new StreamReader("Game\\caches\\GoodSituations.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.Good,  situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BadSituations.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.Bad, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\SlipySituations.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.Slipy, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\Unlucky.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.Unlucky, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\FreeStats.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.FreeStats, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\Exchange.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.Exchange, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSSPreviews.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.BOSS, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\GoodSituationsAlternative.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.GoodEx, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BadSituationsAlternative.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.BadEx, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\SlipySituationsAlternative.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            textCache.Add(situationType.SlipyEx, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS1Situations.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            bossSituationsTexts.Add(bossType.boss1, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS2Situations.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            bossSituationsTexts.Add(bossType.boss2, situationsTexts);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS3Situations.txt");
            situationsTexts = sr.ReadToEnd().Split('\n').ToList();
            bossSituationsTexts.Add(bossType.boss3, situationsTexts);
            sr.Close();

            List<Image> GoodList = new List<Image>();
            List<Image> BadList = new List<Image>();
            List<Image> SlipyList = new List<Image>();
            for (int i=1;i<17;i++)
            {
                GoodList.Add(Image.FromFile($"Game\\GoodPics\\{i}.jpg"));
                BadList.Add(Image.FromFile($"Game\\BadPics\\{i}.jpg"));
                SlipyList.Add(Image.FromFile($"Game\\SlipyPics\\{i}.jpg"));
            }

            backgroundCache.Add(situationType.Good, GoodList);
            backgroundCache.Add(situationType.Slipy, SlipyList);
            backgroundCache.Add(situationType.Bad, BadList);

            bossPhrases.Add(bossType.boss1, new string[]
            {
                "Я нашел тебя. Не сказал бы, что я рад встрече..",
                "За тобой должок перед нашими боссами. И сегодня ты его оплатишь!",
                "Приготовься! Я никогда не беру пленных!"
            }
            .ToList());

            bossPhrases.Add(bossType.boss2, new string[]
            {
                "Холера, наконец - то!",
                "За твою голову назначена огромная награда...",
                "Начнем же..."
            }
            .ToList());

            bossPhrases.Add(bossType.boss3, new string[]
           {
                "ПРОТИВНИК В ПОЛЕ ЗРЕНИЯ",
                "ИДЕНТИФИКАЦИЯ ЛИЧНОСТИ... ПОДОЗРЕВАЕМЫЙ СОВПАДАЕТ С РАЗЫСКИВАЕМЫМ ФОТОРОБОТОМ",
                "НАЧИНАЮ НЕМЕДЛЕННУЮ АННИГИЛЯЦИЮ ПО ПРОТОКОЛУ N3-S0SY-3A-3DD1"
           }
           .ToList());
        }
    }
}
