using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovik2
{
    class Solution
    {
        int[] requirements;
        int[] rewards;
        public List<TextBox> requirementsTBs;
        public List<PictureBox> requirementStatsPics;
        public Button solutionButton;
        Player player;
        public static Dictionary<Situation.situationType, List<string>> resultCache = new Dictionary<Situation.situationType, List<string>>();
        public static Dictionary<Situation.situationType, List<string>> requirementsCache = new Dictionary<Situation.situationType, List<string>>();
        public static Dictionary<Situation.situationType, List<string>> rewardsCache = new Dictionary<Situation.situationType, List<string>>();
        public static Dictionary<Situation.situationType, List<string>> solutionTextCache = new Dictionary<Situation.situationType, List<string>>();
        public static Dictionary<Situation.bossType, List<string>> BFResultCache = new Dictionary<Situation.bossType, List<string>>();
        public static Dictionary<Situation.bossType, List<string>> BFRequirementsCache = new Dictionary<Situation.bossType, List<string>>();
        public static Dictionary<Situation.bossType, List<string>> BFRewardsCache = new Dictionary<Situation.bossType, List<string>>();
        public static Dictionary<Situation.bossType, List<string>> BFSolutionTextCache = new Dictionary<Situation.bossType, List<string>>();
        public string resultText { get; set; }
        int[] statsBuffer;
        TextBox resultField;
        public enum sType
        {
            badSolution,
            goodSolution,
            neutralSolution,
            FS,
            Unluck,
            Exchange,
            BOSSFight
        }
        public sType thisType;
        public Solution(Button b, sType type, int[]_requirements,int[] _rewards,string _resultText, Player p, TextBox _resultField, List<TextBox> reqTBs, List<PictureBox> reqPics,string buttonText)
        {
            thisType = type;
            solutionButton = b;
            solutionButton.Text = buttonText;
            solutionButton.Enabled = false;
            resultText = _resultText;
            requirements = _requirements;
            rewards = _rewards;
            player = p;
            resultField = _resultField;
            requirementsTBs = reqTBs;
            requirementStatsPics = reqPics;
            for (int i = 0; i < reqTBs.Count; i++)
                reqTBs[i].Text = requirements[i].ToString();
        }

        public Solution(Button b, sType type, int[] _requirements, int[] _rewards, int[]curretStats, TextBox _resultField, List<TextBox>reqTBs, List<PictureBox>reqPics)
        {
            thisType = type;
            solutionButton = b;
            solutionButton.Enabled = false;            
            requirements = _requirements;
            statsBuffer = curretStats;
            resultField = _resultField;
            rewards = _rewards;
            requirementsTBs = reqTBs;
            requirementStatsPics = reqPics;
            for (int i = 0; i < reqTBs.Count; i++)
                reqTBs[i].Text = requirements[i].ToString();
        }

        public int[] AcceptSolution(Situation.situationType sitType)
        {
            if (this.resultText != null)
            {
                resultField.Text = resultText;
                int _charIndex;
                void startTimer()
                {
                    _charIndex = 0;
                    resultField.Text = string.Empty;
                    Thread t = new Thread(new ThreadStart(TypewriteText));
                    t.Start();
                }

                void TypewriteText()
                {
                    while (_charIndex < resultText.Length)
                    {
                        Thread.Sleep(20);
                        resultField.Invoke(new Action(() =>
                        {
                            resultField.Text += resultText[_charIndex];
                        }));
                        _charIndex++;
                    }
                }
                startTimer();
            }               
            if ((sitType == Situation.situationType.Good && thisType == sType.goodSolution) ||
               (sitType == Situation.situationType.Bad && thisType == sType.badSolution) ||
               (sitType == Situation.situationType.Slipy && thisType == sType.neutralSolution))
                return StatsChange(true);
            else return StatsChange(false);
           //else 
        }
        public void AccessCheck()
        {
            if (statsBuffer!=null)
            {
                if (statsBuffer[0] < requirements[0] ||
                     statsBuffer[1] < requirements[1] ||
                     statsBuffer[2] < requirements[2] ||
                     statsBuffer[3] < requirements[3] ||
                     statsBuffer[4] < requirements[4] ||
                     statsBuffer[5] < requirements[5] || !ReputationCheck())
                     solutionButton.Enabled = false;
                else
                    return;
            }

            else if (player.strength < requirements[0] ||
               player.sneakness < requirements[1] ||
               player.luck < requirements[2] ||
               player.food < requirements[3] ||
               player.energy < requirements[4] ||
               player.money < requirements[5] || !ReputationCheck())
               solutionButton.Enabled = false;
            else
                return;
        }
        public bool ReputationCheck()
        {
            if (statsBuffer != null)
            {
                if (requirements[6] == 1 && statsBuffer[6] < 10)
                    return false;
                else if (requirements[6] == -1 && statsBuffer[6] > -10)
                    return false;
                else if (requirements[6] == 2 && statsBuffer[6] <= -10 && statsBuffer[6] >= 10)
                    return false;
                else return true;
            }
            else
            {
                if (requirements[6] == 1 && player.reputation < 10)
                    return false;
                else if (requirements[6] == -1 && player.reputation > -10)
                    return false;
                else if (requirements[6] == 2 && player.reputation <= -10 && player.reputation >= 10)
                    return false;
                else return true;
            }
        }
        public int[] StatsChange(bool repMatched)
        {
            if (repMatched)
            {
                for (int i = 0; i < 6; i++)
                     if (rewards[i] > 0)
                        rewards[i]++;
                if (rewards[6] >= 10)
                    rewards[6]++;
                else if (rewards[6] <= -10)
                    rewards[6]--;               
            }
            for(int i=3;i<6;i++)
            {
                rewards[i] -= requirements[i];
            }
            return rewards;
        }
        public void EnableButton()
        {
            
            //if(flag)
            solutionButton.Enabled = true;
        }
        public static void ReadCaches()
        {
            List<string> stringBuffer = new List<string>();
            StreamReader sr = new StreamReader("Game\\caches\\BSResults.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();           
            resultCache.Add(Situation.situationType.Bad, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\GSResult.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            resultCache.Add(Situation.situationType.Good, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\SSResult.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            resultCache.Add(Situation.situationType.Slipy, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\ExchangeResult.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            resultCache.Add(Situation.situationType.Exchange, stringBuffer);
            sr.Close();
            //
            sr = new StreamReader("Game\\caches\\BSRequirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            requirementsCache.Add(Situation.situationType.Bad, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\GSRequirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            requirementsCache.Add(Situation.situationType.Good, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\SSRequirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            requirementsCache.Add(Situation.situationType.Slipy, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\ExchangeRequirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            requirementsCache.Add(Situation.situationType.Exchange, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSSRequirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            requirementsCache.Add(Situation.situationType.BOSS, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BSRewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            rewardsCache.Add(Situation.situationType.Bad, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\ExchangeRewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            rewardsCache.Add(Situation.situationType.Exchange, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\UnluckyRewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            rewardsCache.Add(Situation.situationType.Unlucky, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\FreeStatsRewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            rewardsCache.Add(Situation.situationType.FreeStats, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\GSRewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            rewardsCache.Add(Situation.situationType.Good, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\SSRewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            rewardsCache.Add(Situation.situationType.Slipy, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BSVariants.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            solutionTextCache.Add(Situation.situationType.Bad, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\GSVariants.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            solutionTextCache.Add(Situation.situationType.Good, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\SSVariants.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            solutionTextCache.Add(Situation.situationType.Slipy, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS1Results.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFResultCache.Add(Situation.bossType.boss1, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS2Results.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFResultCache.Add(Situation.bossType.boss2, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS3Results.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFResultCache.Add(Situation.bossType.boss3, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS1Requirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFRequirementsCache.Add(Situation.bossType.boss1, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS2Requirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFRequirementsCache.Add(Situation.bossType.boss2, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS3Requirements.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFRequirementsCache.Add(Situation.bossType.boss3, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS1Rewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFRewardsCache.Add(Situation.bossType.boss1, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS2Rewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFRewardsCache.Add(Situation.bossType.boss2, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS3Rewards.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFRewardsCache.Add(Situation.bossType.boss3, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS1Variants.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFSolutionTextCache.Add(Situation.bossType.boss1, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS2Variants.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFSolutionTextCache.Add(Situation.bossType.boss2, stringBuffer);
            sr.Close();

            sr = new StreamReader("Game\\caches\\BOSS3Variants.txt");
            stringBuffer = sr.ReadToEnd().Split('\n').ToList();
            BFSolutionTextCache.Add(Situation.bossType.boss3, stringBuffer);
            sr.Close();
        }
    }
}
