internal class Program
{
    private static void Main(string[] args)
    {
        int playerHealth = 100;
        int enemyHealth = 100;
        int playerEnergy = 100;
        int enemyEnergy = 100;

        int action;
        int enemyAction;

        Random random;

        List<MessageFight> lstFight = new();

        while (true)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"        Жизни: {0}                  Жизни вируса: {1}", playerHealth, enemyHealth);
            Console.WriteLine(@"      Энергия: {0}                Энергия вируса: {1}", playerEnergy, enemyEnergy);
            Console.WriteLine();
            //Атака
            Console.WriteLine("1.   Почистить реестр (20 урона, -20 энергии)");
            Console.WriteLine("2.   Использовать Касперского (30 урона, -40 энергии)");
            //Жизнь
            Console.WriteLine("3.   Установить обновления (+10 жизни, -10 энергии)");
            Console.WriteLine("4.   Перезагрузить компьютер (+30 жизни, -20 энергии)");
            //Энергия
            Console.WriteLine("5.   Выпить кофе (+30 энергии)");
            Console.WriteLine();

            foreach (var item in lstFight.Skip(Math.Max(0, lstFight.Count() - 30)))
            {
                Console.ForegroundColor = item.ConsoleColor;
                Console.WriteLine(item.Text);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            if (playerHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine();
                Console.WriteLine("Вирус выиграл");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Еще сыграем? (Y/N)");
                var answer = Console.ReadLine();
                if (answer.ToLower().Equals("y") || answer.ToLower().Equals("н"))
                {
                    NewGame(out playerHealth, out enemyHealth, out playerEnergy, out enemyEnergy, lstFight);
                    continue;
                }
                else
                {
                    break;
                }

            }
            if (enemyHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Beep();
                Console.WriteLine();
                //Да, выиграть можно.
                Console.WriteLine("Ты выиграл");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Ещё сыграем? (Y/N)");
                var answer = Console.ReadLine();
                if (answer.ToLower().Equals("y") || answer.ToLower().Equals("н"))
                {
                    NewGame(out playerHealth, out enemyHealth, out playerEnergy, out enemyEnergy, lstFight);
                    continue;
                }
                else
                {
                    break;
                };
            }

            var key = Console.ReadLine();
            int.TryParse(key, out action);
            switch (action)
            {
                case 1:
                    {
                        if (playerEnergy >= 20)
                        {
                            enemyHealth -= 20;
                            playerEnergy -= 20;
                            lstFight.Add(new MessageFight { Text = "Ты почистил реестр и нанёс 20 урона", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        else
                        {
                            lstFight.Add(new MessageFight { Text = "Недостаточно энергии, ты пропустил этот ход", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        break;
                    }
                case 2:
                    {
                        if (playerEnergy >= 40)
                        {
                            enemyHealth -= 30;
                            playerEnergy -= 40;
                            lstFight.Add(new MessageFight { Text = "Ты использовал Касперского и нанёс 30 урона", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        else
                        {
                            lstFight.Add(new MessageFight { Text = "Недостаточно энергии, ты пропустил этот ход", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        break;
                    }
                case 3:
                    {
                        if (playerEnergy >= 10)
                        {

                            playerEnergy -= 10;
                            playerHealth += 10;
                            lstFight.Add(new MessageFight { Text = "Ты установил обновления и восстановил 10 жизни", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        else
                        {
                            lstFight.Add(new MessageFight { Text = "Недостаточно энергии, ты пропустил этот ход", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        break;
                    }
                case 4:
                    {
                        if (playerEnergy >= 20)
                        {
                            playerHealth += 30;
                            playerEnergy -= 20;
                            lstFight.Add(new MessageFight { Text = "Ты перезагрузил компьютер и восстановил 30 жизни", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        else
                        {
                            lstFight.Add(new MessageFight { Text = "Недостаточно энергии, ты пропустил этот ход", 
                                ConsoleColor = ConsoleColor.DarkCyan });
                        }
                        break;
                    }
                case 5:
                    {
                        playerEnergy += 30;
                        lstFight.Add(new MessageFight { Text = "Ты выпил кофе и восстановил 30 энергии", 
                            ConsoleColor = ConsoleColor.DarkCyan });
                        break;
                    }
                default:
                    {
                        playerHealth -= 30;
                        playerEnergy -= 30;
                        lstFight.Add(new MessageFight { Text = $"Ты нажал не туда и установил элементы Яндекс. -30 жизнь, - 30 энергия", 
                            ConsoleColor = ConsoleColor.DarkRed });
                        break;
                    }
            }
            enemyAction = GetEnemyAction(enemyHealth, enemyEnergy);
            switch (enemyAction)
            {
                case 1:
                    {
                        if (enemyAction  == action)
                        {
                            //крит
                            enemyEnergy -= 10;
                            playerHealth -= 40;
                            lstFight.Add(new MessageFight { Text = "Во время очистки реестра выключили свет. 40 урона", 
                                ConsoleColor = ConsoleColor.DarkRed });
                        }
                        else
                        {
                            enemyEnergy -= 20;
                            playerHealth -= 20;
                            lstFight.Add(new MessageFight { Text = "Вирус хрюкнул, сказал, что его лицензия закончилась и нанёс 20 урона", 
                                ConsoleColor = ConsoleColor.Red });
                        }
                        break;
                    }
                case 2:
                    {
                        if (enemyAction == action)
                        {
                            //крит
                            enemyEnergy -= 20;
                            playerHealth -= 50;
                            lstFight.Add(new MessageFight
                            {
                                Text = "Вирус был обнаружен Касперским и отправился в карантин вместе с активатором Windows\n" +
                                    "и нанес 50 урона",
                                ConsoleColor = ConsoleColor.DarkRed
                            });
                        }
                        else
                        {
                            enemyEnergy -= 40;
                            playerHealth -= 30;
                            lstFight.Add(new MessageFight { Text = "Вирус установил Mail.ru agent и нанёс 30 урона", 
                                ConsoleColor = ConsoleColor.Red });
                        }
                        break;
                    }
                case 3:
                    {
                        if (enemyAction == action)
                        {
                            //крит
                            enemyHealth += 40;
                            enemyEnergy -= 15;
                            lstFight.Add(new MessageFight { Text = "Вирус установил порнуху стартовой страницей, отдохнул и восстановил 40 жизни.", 
                                ConsoleColor = ConsoleColor.DarkRed });
                        }
                        else
                        {
                            enemyHealth += 20;
                            enemyEnergy -= 30;
                            lstFight.Add(new MessageFight { Text = "Вирус установил Вирус-Дзен с учетом твоих интересов и восстановил 20 жизни", 
                                ConsoleColor = ConsoleColor.Red });
                        }
                        break;
                    }
                case 4:
                    {
                        if (enemyAction == action)
                        {
                            //крит
                            enemyHealth += 20;
                            enemyEnergy -= 5;
                            lstFight.Add(new MessageFight { Text = "Вирус удалил со стартовой страницы поисковика всё, кроме поисковой строки," +
                                "\n написал \"Привет! Давай поболтаем?\" " +
                                "И восстановил 20 жизни", ConsoleColor = ConsoleColor.DarkRed
                            }); 

                            }
                        else
                        {
                            enemyHealth += 10;
                            enemyEnergy -= 10;
                            lstFight.Add(new MessageFight { Text = "Вирус установил Вирус-Браузер с защитной технологией Протект и восстановил 10 жизни", 
                                ConsoleColor = ConsoleColor.Red });
                        }
                        break;
                    }
                case 5:
                    {
                        if (enemyAction == action)
                        {
                            //крит
                            enemyEnergy += 40;
                            lstFight.Add(new MessageFight { Text = "Вирус заблокировал очередной VPN и восстановил 40 энергии, но, как ни старался, урона не нанёс", 
                                ConsoleColor = ConsoleColor.DarkRed });
                        }   
                        else
                        {
                            enemyEnergy += 20;
                            lstFight.Add(new MessageFight { Text = "Вирус взломал учетку Одноклассников и восстановил 20 энергии, но урона не нанёс", 
                                ConsoleColor = ConsoleColor.Red });
                        }
                        break;
                    }
                default:
                    {
                        lstFight.Add(new MessageFight { Text = "Твои грамотные действия заблокировали вирус", ConsoleColor = ConsoleColor.Red });
                        break;
                    }
            }

            playerHealth = playerHealth < 0 ? 0 : playerHealth;
            enemyHealth = enemyHealth < 0 ? 0 : enemyHealth;
            playerEnergy = playerEnergy < 0 ? 0 : playerEnergy;
            enemyEnergy = enemyEnergy < 0 ? 0 : enemyEnergy;
        }
    }

    private static int GetEnemyAction(int enemyHealth, int enemyEnergy)
    {
        //Атака
        if(enemyHealth >= 25 && enemyEnergy >= 20)
        {
            var rnd = new Random(); 
            return rnd.Next(1, 3);
        }
        //Жизнь
        if(enemyHealth <= 25)
        {
            var rnd = new Random();
            return rnd.Next(3, 5);
        }
        //Энергия
        if(enemyEnergy <= 20)
        {
            return 5;
        }
        return 0;
    }

    private static void NewGame(out int playerHealth, out int enemyHealth, out int playerEnergy, out int enemyEnergy, List<MessageFight> lstFight)
    {
        playerHealth = 100;
        enemyHealth = 100;
        playerEnergy = 100;
        enemyEnergy = 100;
        lstFight.Clear();
    }
}

class MessageFight
{
    public string Text { get; set; }
    public ConsoleColor ConsoleColor { get; set; }
}
//комп
// комп 1
//nout