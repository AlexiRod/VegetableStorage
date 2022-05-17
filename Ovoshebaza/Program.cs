using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Ovoshebaza
{
    class Program
    {
        #region Main Program

        // Выброс лишних ящиков после переопределения store (Recalculate)
        // Изменение параметров магазина
        // игра
        // красивый вывод)

        public static int timeForAnimation = 500;
        public static Random random = new Random();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Help();
                Console.Write("Команда: ");
                ReadCommand();
            }
        }

        /// <summary>
        /// Сообщение со справкой.
        /// </summary>
        public static void Help()
        {
            Console.WriteLine("Приветствую на моей овощебазе!");
            Console.WriteLine("Здесь можно выращивать овощи в подпольных лабораториях и фасовать по ящикам ака зеленый Эскобар, " +
                "\nформировать целые партии овощей в контейнерах, чтобы потом толкать их веганам, " +
                "\nпомещать их в один общий склад или удалять оттуда за ненадобностью (мы ведь следим за качеством своего товара), " +
                "\nподнимать реальные money без смс и регистрации, грабить корованы и многое многое другое (ничего).");
            Console.WriteLine("Для работы введи одну из команд:");
            Console.WriteLine("-check == Проверить работоспособность программы/наличие функций из условия, уронить и задушить прогу.");
            Console.WriteLine("-play == Проверить дополнительный функционал (я знаю, тебе уже нетерпится)");
            Console.WriteLine("-kto == eto kto?");
            Console.WriteLine("-exit == Завершение работы приложения");
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        /// <summary>
        /// Метод для чтения команд.
        /// </summary>
        public static void ReadCommand()
        {
            string command = Console.ReadLine().Trim();

            switch (command)
            {
                case "-check":
                    Console.Clear();
                    CheckFunctions();
                    break;
                case "-play":
                    Console.Clear();
                    Start();
                    break;
                case "-kto":
                    Console.Clear();
                    ShowPrekol("prekol.txt");
                    break;
                case "-exit":
                    Console.WriteLine("Программа завершена. Спасибо, что посетили нашу овощебазу. Огурцы будут помнить вас вечно.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Write("Команда либо на языке помидоров, либо неверная. Попробуй еще раз: ");
                    ReadCommand();
                    break;
            }
        }

        #endregion




        #region Functions

        /// <summary>
        /// Проверка работы функций из условия.
        /// </summary>
        public static void CheckFunctions()
        {
            Console.WriteLine("Выберите способ ввода информации о складе: ");
            Console.WriteLine("-console == Работа с консолью (добавление и удаление контейнеров, работа с ними)");
            Console.WriteLine("-file == Получение всей информации из трех файлов - с описанием склада," +
                    " действий с ним и контейнеров соответственно. Учтите, что все данные склада будут заменены новыми и перерассчитаны.");
            Console.WriteLine("-close == Возврат в главное меню");
            Console.Write("Команда: ");

            switch (Console.ReadLine().Trim())
            {
                case "-console":
                    Console.Clear();
                    WorkWithConsole();
                    break;
                case "-file":
                    Console.Clear();
                    WorkWithFile();
                    break;
                case "-close":
                    Console.Clear();
                    return;

                default:
                    Console.Write("Команда либо на языке помидоров, либо неверная. Попробуй еще раз: ");
                    Console.ReadKey();
                    Console.Clear();
                    CheckFunctions();
                    break;
            }
            Console.Clear();
            CheckFunctions();
        }


        #region Console

        /// <summary>
        /// Метод для считывания команд.
        /// </summary>
        private static void WorkWithConsole()
        {
            Console.WriteLine("Введите одну из команд для работы со складом: ");
            Console.WriteLine("-fix == Изменение параметров склада - вместимость и цена за хранение одного контейнера " +
                $"(сейчас: {Store.MaxCount} и {Store.Price:c2} соответственно)");
            Console.WriteLine("-add == Добавление контейнера");
            Console.WriteLine("-remove == Удаление контейнера по его Id");
            Console.WriteLine("-info == Информация о складе и его содержимом");
            Console.WriteLine("-close == Возврат в главное меню");
            Console.Write("Команда: ");

            switch (Console.ReadLine().Trim())
            {
                case "-fix":
                    Console.Clear();
                    FixStore();
                    break;
                case "-add":
                    Console.Clear();
                    AddContainer();
                    break;
                case "-remove":
                    Console.Clear();
                    RemoveContainer();
                    break;
                case "-info":
                    Console.Clear();
                    Console.WriteLine("Информация о складе:");
                    Console.WriteLine(Store.GetInfo(1));
                    Console.ReadKey();
                    break;
                case "-close":
                    Console.Clear();
                    return;

                default:
                    Console.Write("Команда либо на языке помидоров, либо неверная. Попробуй еще раз: ");
                    Console.ReadKey();
                    break;
            }
            Console.Clear();
            WorkWithConsole();
        }

        /// <summary>
        /// Изменение параметров склада.
        /// </summary>
        private static void FixStore()
        {
            Console.WriteLine($"Актуальная информация о складе:{Environment.NewLine}{Store.GetInfo(1)}{Environment.NewLine}Доступные команды:");
            Console.WriteLine("-count = <значение_вместимости> - изменить вместимость склада (натуральное число)");
            Console.WriteLine("-price = <значение_цены> - изменить цену за хранение одного контейнера на складе (натуральное число)");
            Console.WriteLine("-close == Возврат назад");
            Console.Write("Команда: ");

            string[] parts = Console.ReadLine().Trim().Split(" = ");
            switch (parts[0])
            {
                case "-count":
                    if (parts.Length > 1 && int.TryParse(parts[1], out int count) && count > 0)
                    {
                        Store.ChangeCount(count);
                        Console.Write("Значение вместимости успешно изменено. Хотите ли перераспределить контейнеры в связи с изменением " +
                            "вместимости \n(удалять раннее добавленные контейнеры, пока общее количество не будет меньше либо равно максимальному)?" +
                            "\nУчтите, что если вы не сделаете этого, вы не сможете добавлять новые контейнеры, так как их количество избыточно. " +
                            "\nВведите \"yes\" чтобы сделать это или любую другую строку, если не хотите этого делать: ");
                        Recalculate();
                    }
                    else Console.Write("Неверное значение вместимости. ");
                    break;
                case "-price":
                    if (parts.Length > 1 && decimal.TryParse(parts[1], out decimal price) && price > 0)
                    {
                        Store.ChangePrice(price);
                        Console.Write("Значение цены успешно изменено. Хотите ли перераспределить контейнеры в связи с изменением " +
                            "цены (удалить нерентабельные контейнеры)? \nВведите \"yes\" чтобы сделать это или любую другую строку, если не хотите этого делать: ");
                        Recalculate();
                    }
                    else Console.Write("Неверное значение цены. ");
                    break;
                case "-close":
                    Console.Clear();
                    return;

                default:
                    Console.Write("Команда либо на языке помидоров, либо неверная. Попробуй еще раз: ");
                    Console.ReadKey();
                    Console.Clear();
                    FixStore();
                    break;
            }
            Console.ReadKey();
            Console.Clear();
            FixStore();
        }

        /// <summary>
        /// Перераспределение контейнеров.
        /// </summary>
        public static void Recalculate()
        {
            if (Console.ReadLine().Trim() == "yes")
            {
                Console.WriteLine("\nПерераспределение успешно выполнено.\n");
                List<Container> deleted = Store.RecalculateContainers();
                if (deleted.Count > 0)
                {
                    Console.WriteLine("Удаленные контейнеры: ");
                    foreach (Container cont in deleted)
                        Console.WriteLine(cont);
                }
                else Console.WriteLine("Все контейнеры подходят под заданные параметры, поэтому ни один не был удален.");
            }
            else Console.WriteLine("Перераспределение не будет выполнено. ");
            //Console.ReadKey();
            //Console.Clear();
        }


        /// <summary>
        /// Добавление контейнера с вводимыми параметрами.
        /// </summary>
        private static void AddContainer()
        {
            Console.Write("Введите Id контейнера: ");
            string id = Console.ReadLine().Trim();
            Console.WriteLine("Вводите информацию о ящиках внутри контейнера, следуя инструкциям:");

            List<Box> boxes = new List<Box>();
            do
            {
                Console.Write("Введите Id ящика: ");
                string boxId = Console.ReadLine().Trim();
                int boxMass;
                decimal boxPrice;
                do
                {
                    Console.Write("Введите натуральное число - массу овощей в ящике: ");
                } while (!int.TryParse(Console.ReadLine(), out boxMass) || boxMass <= 0);
                do
                {
                    Console.Write("Введите цену овощей в ящике за килограмм: ");
                } while (!decimal.TryParse(Console.ReadLine(), out boxPrice) || boxPrice <= 0);

                boxes.Add(new Box(boxId, boxMass, boxPrice));


                AnimationAdd();

                Console.WriteLine("Ящик успешно сформирован для добавления в контейнер.");
                Console.Write("Введите \"-add\", если хотите сформировать очередной ящик с овощами, или любую" +
                    " другую сторку чтобы закончить ввод информации о ящиках: ");
            } while (Console.ReadLine().Trim() == "-add");

            Animate(ConsoleColor.Cyan, "Формируем контейнер", 5);

            Container container = new Container(boxes, id, true);
            string message = "";
            if (Store.TryToAddContainer(container, ref message))
                Console.WriteLine($"{message}Контейнер оказался рентабельным и был добавлен на склад. Информация о нем:{Environment.NewLine}{container}");
            else
                Console.WriteLine($"Контейнер оказался нерентабельным и не был добавлен на склад. Цена контейнера: {container.GetPrice():c2}" +
                    $", в то время как его хранение обходится в {Store.Price:c2}");

            Console.Write("Нажмите любую клавишу: ");
            Console.ReadKey();
        }

        /// <summary>
        /// Удаление контейнера по заданному Id.
        /// </summary>
        private static void RemoveContainer()
        {
            Console.Write("Введите Id контейнера, который хотите удалить: ");
            string id = Console.ReadLine().Trim();

            if (Store.TryToDeleteContainer(id))
                Console.WriteLine("Контейнер успешно удален.");
            else Console.WriteLine("Контейнер с таким Id не найден.");

            Console.Write("Нажмите любую клавишу: ");
            Console.ReadKey();
        }

        #endregion


        #region File

        /// <summary>
        /// Ввод информации из файла.
        /// </summary>
        private static void WorkWithFile()
        {
            Console.Write("Введите путь к файлу с инфорацией о контейнере (в файле два натуральных числа с новой строки - " +
                "вместиость склада и цена за хранение контейнера): ");
            string pathToStore = Console.ReadLine().Trim();

            Console.Write("Введите путь к файлу с описанием ящиков с овощами (в файле n строк, элементы каждой строки разделенны " +
                "строго точкой с запятой - Id ящика; масса овощей в нем; цена за кг): ");
            string pathToBoxes = Console.ReadLine().Trim();

            Console.Write("Введите путь к файлу с описанием контейнеров (в файле n строк, элементы каждой строки разделенны строго" +
                " точкой с запятой - Id контейнера; список Id ящиков внутри контейнера, разделенные запятой): ");
            string pathToContaineres = Console.ReadLine().Trim();

            Console.Write("Введите путь к файлу с действиями (в файле n действий двух видов: 1)\"-add <Id> " +
                "<Спиок Id ящиков строго из файла с описанием ящиков внутри контейнера, разделенные запятой>\" - добавление контейнера " +
                "с указанными параметрами; 2) \"-remove <Id>\" - удаление контейнера с указанным Id): ");
            string pathToCommands = Console.ReadLine().Trim();

            string diagnose = "";
            try
            {
                List<Box> boxes = new List<Box>();
                List<Container> containers = new List<Container>();
                if (ReadFileStore(pathToStore))
                    diagnose += $"Данные о складе успешно считаны и изменены.{Environment.NewLine}";
                else throw new Exception("Данные о складе введены неверно. ");
                if (ReadFileBoxes(pathToBoxes, out boxes))
                    diagnose += $"Данные о ящиках успешно считаны и добавлены.{Environment.NewLine}";
                else throw new Exception("Данные о ящиках введены неверно. ");
                if (ReadFileContainers(pathToContaineres, out containers, boxes))
                    diagnose += $"Данные о контейнерах успешно считаны и добавлены.{Environment.NewLine}";
                else throw new Exception("Данные о контейнерах введены неверно. Возможно, одного из указанных " +
                    "ящиков нет в соответствующем файле с данными о ящиках. ");

                diagnose += $"Данные введены верно. Склад и его составляющие заменены считанными данными.{Environment.NewLine}";
                Store.containers = containers;
                if (ReadFileCommands(pathToCommands, boxes, ref diagnose))
                    diagnose += $"Команды успешно выполнены. Их результат записан в файл diagnose.txt{Environment.NewLine}";
                else throw new Exception("Ошибка при выполнении команд, похоже, они введены неверно. Возможно, одного из указанных " +
                    "ящиков при добавлении контейнера нет в соответствующем файле с данными о ящиках. ");


                Console.WriteLine("Резльтат выполнения и лог команд записан в файл diagnose.txt, который расположен в папке с исполняемой программой (ехе)");
                File.WriteAllText("diagnose.txt", diagnose);
            }
            catch (IOException ex)
            {
                diagnose += $"Ошибка при чтении данных из файла! {ex.Message}. Работа приостановлена.";
                Console.WriteLine("Упс... произошла ошибка при работе с файлом. Работа программы завершена принудительно: " + ex.Message);
            }
            catch (Exception ex)
            {
                diagnose += $"Неизвестная ошибка! {ex.Message}. Работа приостановлена.";
                Console.WriteLine("Упс... произошла ошибка. Работа программы завершена принудительно: " + ex.Message);
            }

            Console.Write("Нажмите любую клавишу: ");
            Console.ReadKey();
        }

        /// <summary>
        /// Считывание информации о складе из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns></returns>
        private static bool ReadFileStore(string path)
        {
            string[] str = File.ReadAllLines(path);

            if (int.TryParse(str[0], out int count) && count > 0)
                Store.ChangeCount(count);
            else return false;

            if (decimal.TryParse(str[1], out decimal price) && price > 0)
                Store.ChangePrice(price);
            else return false;

            Store.containers = new List<Container>();
            return true;
        }

        /// <summary>
        /// Считывание информации о ящиках из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns></returns>
        private static bool ReadFileBoxes(string path, out List<Box> boxes)
        {
            string[] str = File.ReadAllLines(path);
            boxes = new List<Box>();

            for (int i = 0; i < str.Length; i++)
            {
                string[] parts = str[i].Split(';');
                if (!int.TryParse(parts[1], out int mass) || mass <= 0)
                    return false;
                if (!decimal.TryParse(parts[2], out decimal price) || price <= 0)
                    return false;
                boxes.Add(new Box(parts[0], mass, price));
            }

            return true;
        }

        /// <summary>
        /// Считывание информации о контейнере из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns></returns>
        private static bool ReadFileContainers(string path, out List<Container> containers, List<Box> boxes)
        {
            string[] str = File.ReadAllLines(path);
            containers = new List<Container>();

            for (int i = 0; i < str.Length; i++)
            {
                string[] parts = str[i].Split(';');
                string[] ids = parts[1].Split(',');
                List<Box> boxesAdd = new List<Box>();
                for (int j = 0; j < ids.Length; j++)
                {
                    bool isOk = false;
                    Box boxAdd = null;
                    foreach (Box box in boxes)
                        if (box.Id == ids[j])
                        {
                            isOk = true;
                            boxAdd = box;
                            break;
                        }

                    if (!isOk)
                        return false;
                    boxesAdd.Add(boxAdd);
                }

                containers.Add(new Container(boxesAdd, parts[0], false));
            }
            return true;
        }

        /// <summary>
        /// Считывание команд из файла.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns></returns>
        private static bool ReadFileCommands(string path, List<Box> boxes, ref string res)
        {
            string[] str = File.ReadAllLines(path);
            res += $"*** Результат выполнения введенных команд:{Environment.NewLine}{Environment.NewLine}";

            for (int i = 0; i < str.Length; i++)
            {
                string[] parts = str[i].Split();

                if (parts[0] == "-add")
                {
                    string conId = parts[1];
                    string[] ids = parts[2].Split(',');
                    List<Box> boxesAdd = new List<Box>();
                    for (int j = 0; j < ids.Length; j++)
                    {
                        bool isOk = false;
                        Box boxAdd = null;
                        foreach (Box box in boxes)
                            if (box.Id == ids[j])
                            {
                                isOk = true;
                                boxAdd = box;
                                break;
                            }

                        if (!isOk)
                            return false;
                        boxesAdd.Add(boxAdd);
                    }

                    Container container = new Container(boxesAdd, conId, false);
                    string message = "";
                    if (Store.TryToAddContainer(container, ref message))
                        res += $"*{message}Контейнер оказался рентабельным и был добавлен на склад. Информация о нем:{Environment.NewLine}{container}{Environment.NewLine}";
                    else
                        res += $"* Контейнер оказался нерентабельным и не был добавлен на склад. Цена контейнера: {container.GetPrice():c2}" +
                            $", в то время как его хранение обходится в {Store.Price:c2}{Environment.NewLine}";
                }
                else if (parts[0] == "-remove")
                {
                    string conId = parts[1];

                    if (Store.TryToDeleteContainer(conId))
                        res += $"* Контейнер успешно удален.{Environment.NewLine}";
                    else res += $"* Контейнер с таким Id не найден.{Environment.NewLine}";
                }
                else res += $"* Какая-то неизвестная, помидорная команда...{Environment.NewLine}";
            }

            return true;
        }

        #endregion



        #region Prekols

        /// <summary>
        /// ПрЕкол да?
        /// </summary>
        /// <param name="path">Путь к прЕколу..... тернист и опасен...........</param>
        public static void ShowPrekol(string path)
        {
            Console.WriteLine(File.ReadAllText(path));
            Console.Write("Neponel...   ");
            Console.ReadLine();
        }

        /// <summary>
        /// Метод для создания прЕкольной анимации создания ящика.
        /// </summary>
        public static void AnimationAdd()
        {
            Animate(ConsoleColor.DarkGreen, "Сажаем семена", 3);
            Animate(ConsoleColor.Yellow, "Выращиваем овощи", 3);
            Animate(ConsoleColor.Green, "Собираем урожай", 3);
            Animate(ConsoleColor.Blue, "Пакуем ящик", 3);
        }

        /// <summary>
        /// Метод для создания универсальной анимации вывода.
        /// </summary>
        /// <param name="consoleColor">Цвет вывода.</param>
        /// <param name="s">Сообщение.</param>
        /// <param name="count">Количество точек.</param>
        public static void Animate(ConsoleColor consoleColor, string s, int count)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(s);
            for (int i = 0; i < count; i++)
            {
                Console.Write(".");
                Thread.Sleep(timeForAnimation);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        #endregion

        #endregion




        #region Game

        public static int day = 1;
        public static decimal pay = 5;
        public static decimal balance = 2000;

        public static List<Order> orders = new List<Order>();
        public static List<Package> packages = new List<Package>();

        /// <summary>
        /// Метод с информацией об игре.
        /// </summary>
        public static void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Здарова. Наконец то мы можем поговорить на понятном тебе языке - языке боли. Заранее извиняюсь, если ты девочка, тут я буду " +
                "\nобщаться с \"пользователем\", поэтому все глаголы в мужском роде, не обессудь. Ну и, скорее всего, тут куча грамматических ошибок. " +
                "\nВ общем, в качестве доп функционала" +
                " кроме методов для изменения и перераспределения элементов склада (ну и безумно крутого вывода о формировании ящиков и контейнеров)" +
                "\nя решил добавить небольшую игру. Небольшую потому, что я где-то слышал про то, что доп функционал не должен быть сильно сложнее и " +
                "\nбольше основного задания. Поэтому, если ты ожидаешь увидеть тут казино с блекджеком и шлюпками, тебе на три блока вниз.");
            Console.WriteLine("А ну и собственно учти, что для игры необходимо внести налог в размере одного склада с овощами (типо все то, что на " +
                "нем сейчас есть, попадет в антипространство ок да).\nПоставь полное разрешение у консоли и, если готов и уже наконец поиграть, введи в консоль \"-dusha\": ");
            if (Console.ReadLine().Trim() == "-dusha")
            {
                Console.Clear();
                Console.WriteLine("Отлично. Ты успешно продал свою душу за игру и теперь ты подвластен мне, но это аюсолютно неважно сейчас.");
                Console.WriteLine("Итак, как я сказал ранне, твой склад сейчас обчищается моими людьми. Мы провели небольшой кап ремонт и теперь " +
                    "\nон вмещает только 15 контейнеров, стоимость их хранения, коэфициент повреждения, рентабельность и прочее здесь не учитываются. " +
                    "\nСмысл игры довольно прост - тебе нужно как можно дольше " +
                    "выживать в этом суровом, несправедливом и аморальном капиталистическом мире. \nКаждый день у тебя списывается денюшка за обслуживание" +
                    "\nтвоего складского помещения, работу грузчиков, платятся налоги и прочее. Для того, чтобы дяденьки в масках не пришли за тобой, " +
                    "\nдабы прогуляться с тобой в ближайший лесок, тебе надо как-то эти денюшки зарабатывать и платить. По мере твоего продвижения по " +
                    "\nигровой карьерной лестнице ежедневная плата будет лишь расти, поэтому тебе надо будет находить новые методы заработка денег.");
                Console.WriteLine("Так как игра небольшая, есть два варианта сделать это - попытаться продать партию овощей какому-то магазину или " +
                    "ограбить корован. \nДА, Я ЖЕ СКАЗАЛ, ЗДЕСЬ МОЖНО ГРАБИТЬ КОРОВАНЫ.");
                Console.WriteLine("Продажа партии возможна, если магазин озвучил свой заказ - n-ое количество определенных овощей и " +
                    "\nна складе есть контейнеры с необходимыми овощами. Магазин не будет возражать, если кроме требуемых продуктов в контейнерах будут лишние, но " +
                    "\nне примет партию, если хотя бы один пункт из списка заказа не выполнен.");
                Console.WriteLine("Для того, чтобы на складе как-то появились овощи, их нужно вырастить - в этом тебе любезно помогут колхозы, которые " +
                    "\nс радостью впарят тебе пару ящиков помидоров, однако учитывай, что их предложения могут быть как выгодными, так и не очень.");
                Console.WriteLine("Грабеж корованов - лютый рандом. Это способ заработать денег или овощей, прибив пару верблюдов, \nно каким будет полученный " +
                    "хабар, определит Random random = new Random();");
                Console.WriteLine("Также стоит учитывать, что цены за овощи в данной игре немного оторваны от реальных, поэтому считай, что ты открыл свой " +
                    "\nсклад на Камчатке, и именно поэтому помидоры стоят как крыло от самолета. Ну и так как это просто игра для пир грейда (лол зачем я вообще " +
                    "\nее делаю, я же склепаю лютый кал из грязи и палок), возможно она окажется довольно дисбалансной - ящики овощей будут стоить как баррель нефти или " +
                    "\nв день с тебя будут списывать годовой бюджет Ростовской области - считай, это альфа-тестирование.");
                Console.WriteLine("Ну, с правилами вроде все.");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine(File.ReadAllText("Mechanics.txt"));
                Console.ReadLine();
                Store.containers = new List<Container>();
                Store.ChangeCount(15);
                Store.ChangePrice(1);
                Console.ResetColor();
                PlayGame();
            }
        }

        /// <summary>
        /// Ну че народ, погнали.
        /// </summary>
        public static void PlayGame()
        {
            DayInfo();
            CalculateOrdersAndPackages();
            ReadGameCommand();
        }

        /// <summary>
        /// Актуальная ежедневная информация.
        /// </summary>
        public static void DayInfo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"День {day}");
            Console.WriteLine($"Контейнеров на складе: {Store.containers.Count}. " +
               $"Общая вместимость: {Store.MaxCount} штук. Возможные действия: ");
            Console.WriteLine($"Ежедневная плата: {pay:c2}");
            Console.WriteLine($"Баланс: {balance:c2}");
            Console.ResetColor();
            Console.WriteLine("Если нужного количества денег при переходе к следующему дню на балансе не окажется, ты проиграешь. Возможные команды:");
            Console.WriteLine("-info == посмотреть информацию о складе и его содержимом");
            Console.WriteLine("-fix == приоберсти дополнительные места на складе");
            Console.WriteLine("-mix == перераспределить содержимое двух контейнеров");
            Console.WriteLine("-order == приобрести у колхоза закупку с конкретными овощами");
            Console.WriteLine("-sell == продать партию овощей магазину");
            Console.WriteLine("-loot == ограбить корован))))");
            Console.WriteLine("-next == перейти к следующему дню и заплатить ежедневный счет");
            Console.WriteLine("-help == вывод справки с информацией о механиках игры");
            Console.WriteLine("-exit == сдаться и выйти, завершив работу приложения (твоя душа остается у меня)");
            Console.Write("Команда: ");
        }

        /// <summary>
        /// Считывание команды.
        /// </summary>
        public static void ReadGameCommand()
        {
            string command = Console.ReadLine().Trim();
            switch (command)
            {
                case "-info":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Store.GetInfo(2));
                    if (Store.containers.Count == 0)
                        Console.WriteLine("Тут как-то пустовато...");

                    Console.ReadKey();
                    break;
                case "-fix":
                    Console.Clear();
                    PayToFixStore();
                    break;
                case "-mix":
                    Console.Clear();
                    RemixContainers();
                    break;
                case "-order":
                    Console.Clear();
                    OrderVegetables();
                    break;
                case "-sell":
                    Console.Clear();
                    SellVegetables();
                    break;
                case "-loot":
                    Console.Clear();
                    GrabitCorovani();
                    break;
                case "-next":
                    Console.Clear();
                    SimulateDay();
                    break;
                case "-help":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(File.ReadAllText("Mechanics.txt"));
                    Console.ReadLine();
                    break;
                case "-exit":
                    Console.WriteLine("Программа завершена. Спасибо, что посетили нашу овощебазу (и отдали душу). Огурцы будут помнить вас вечно.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(File.ReadAllText("Reflection.txt"));
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Write("Команда либо на языке помидоров, либо неверная. Попробуй еще раз: ");
                    ReadGameCommand();
                    break;
            }
            DayInfo();
            ReadGameCommand();
        }

        /// <summary>
        /// Метод для увеличения размеров склада.
        /// </summary>
        private static void PayToFixStore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Дополнительное место на складе стоит $1000, хотите приобрести одно? Введите \"-pay\", " +
                           "чтобы заплатить или любую другую строку, чтобы отказаться: ");
            if (Console.ReadLine().Trim() == "-pay")
            {
                if (balance < pay)
                {
                    Console.WriteLine("На это у тебя не хватает деньжат.");
                    Console.ReadKey();
                    return;
                }

                balance -= 1000;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Дополнительное место успешно приобретено.");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                Store.ChangeCount(Store.MaxCount + 1);
            }
        }

        /// <summary>
        /// Перераспределение контейнеров.
        /// </summary>
        private static void RemixContainers()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"День {day}");
            Console.WriteLine($"Баланс: {balance:c2}");

            bool isFound = false;
            Container container1 = null, container2 = null;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Информация о контейнерах на складе:");
            Console.WriteLine(Store.GetInfo(2));
            if (Store.containers.Count == 0)
                Console.WriteLine("На складе нет ни одного контейнера...");
            Console.ResetColor();
            do
            {
                Console.Write("Введи Id двух контейнеров, которые хотите перераспределить, через пробел или \"-close\", чтобы выйти: ");
                string input = Console.ReadLine();
                if (input == "-close")
                    return;
                string[] str = input.Split();

                if (str.Length != 2)
                {
                    Console.WriteLine("Неправильный ввод!");
                    continue;

                }

                bool found1 = false;
                foreach (Container container in Store.containers)
                    if (container.Id == str[0])
                    {
                        container1 = container;
                        found1 = true;
                        break;
                    }
                bool found2 = false;
                foreach (Container container in Store.containers)
                    if (container.Id == str[1])
                    {
                        container2 = container;
                        found2 = true;
                        break;
                    }
                isFound = found1 & found2;
                if (!found1)
                    Console.WriteLine($"Контейнер {str[0]} не найден на складе.");
                if (!found2)
                    Console.WriteLine($"Контейнер {str[1]} не найден на складе.");
            } while (!isFound);

            decimal cost = container1.GetPrice() / 5 + container2.GetPrice() / 5;
            Console.Write($"Слияние этих контейнеров будет стоить {cost:c2}. Введите \"-pay\", чтобы заплатить и выполнить слияние или любую" +
                $" другую строку, чтобы отказаться: ");
            if (Console.ReadLine().Trim() == "-pay")
            {
                if (balance < cost)
                {
                    Console.WriteLine("На это у тебя не хватает деньжат.");
                    Console.ReadKey();
                    return;
                }

                balance -= pay;
                Console.WriteLine("Транзакция прошла успешно. Переходим к слиянию.");
                Console.ReadKey();
                Console.Clear();
                MakeMixing(container1, container2);

            }

        }

        /// <summary>
        /// Считывание необходимых данных и перераспределение.
        /// </summary>
        /// <param name="container1">Первый контейнер.</param>
        /// <param name="container2">Второй контейнер.</param>
        private static void MakeMixing(Container container1, Container container2)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Чтобы переместить содержимое одного контейнера в другой, необходимо ввести команду вида \"<Id_откуда> <Id_куда> <Название_овоща> <Масса>\".");
            Console.WriteLine("После выполнения команды указанная масса овощей из одного контейнера переместится в дргой. Если вы хотите закончить перемещение, введите \"-end\".");
            Console.WriteLine("Учтите, что, если один из контейнеров после выполнения операций останется пустым, он удалится со склада.");



            string input = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n* Контейнер 1: {container1.Id}\n Содержимое:\n" + container1.GetInfo(0) + Environment.NewLine);
                Console.WriteLine($"\n* Контейнер 2: {container2.Id}\n Содержимое:\n" + container2.GetInfo(0) + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Введите команду вида \"<Id_откуда> <Id_куда> <Название_овоща> <Масса>\" или \"-end\" чтобы закончить перемещение: ");
                input = Console.ReadLine();
                if (input == "-end")
                    break;
                string[] parts = input.Split();
                if (parts.Length != 4)
                {
                    Console.WriteLine("Команда введена неверно!");
                    continue;
                }

                if (parts[0] == container1.Id && parts[1] == container2.Id) { }
                else if (parts[0] == container2.Id && parts[1] == container1.Id)
                {
                    Container c = container2;
                    container2 = container1;
                    container1 = c;
                }
                else
                {
                    Console.WriteLine("Id контейнеров указаны неверно!");
                    continue;
                }

                Box boxMix = null;
                bool isFoundBox = false;
                foreach (Box box in container1.boxes)
                    if (box.Id == parts[2])
                    {
                        boxMix = box;
                        isFoundBox = true;
                        break;
                    }
                if (!isFoundBox)
                {
                    Console.WriteLine($"Указанного овоща нет в контейнере {container1.Id}!");
                    continue;
                }
                int mass;
                if (!int.TryParse(parts[3], out mass) || mass <= 0 || mass > boxMix.Mass)
                {
                    Console.WriteLine($"Масса для пермещения указана неверно! Она должна быть в диапозоне от 1 до {boxMix.Mass}");
                    continue;
                }

                bool isChanged = false;
                foreach (Box box in container2.boxes)
                    if (box.Id == boxMix.Id)
                    {
                        box.ChangeMass(box.Mass + mass);
                        boxMix.ChangeMass(boxMix.Mass - mass);
                        isChanged = true;
                        break;
                    }

                if (!isChanged)
                {
                    container2.boxes.Add(new Box(boxMix.Id, mass, boxMix.Price));
                    boxMix.ChangeMass(boxMix.Mass - mass);
                }

                Console.WriteLine("Перемещение овощей успешно выполнено.");
                Console.ReadKey();
                Console.Clear();
            } while (true);

            Console.WriteLine("Слияние контейнеров завершено. Актуальная информация о них:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n* Контейнер 1: {container1.Id}\n Содержимое:\n" + container1.GetInfo(0) + Environment.NewLine);
            Console.WriteLine($"\n* Контейнер 2: {container2.Id}\n Содержимое:\n" + container2.GetInfo(0) + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;

            bool isEmpty = true;
            foreach (Box box in container2.boxes)
                if (box.Mass > 0)
                {
                    isEmpty = false;
                    break;
                }
            if (isEmpty)
            {
                Console.WriteLine($"{container2.Id} оказался пустым и был удален со склада за ненадобностью.");
                Store.TryToDeleteContainer(container2.Id);
            }
            isEmpty = true;
            foreach (Box box in container1.boxes)
                if (box.Mass > 0)
                {
                    isEmpty = false;
                    break;
                }
            if (isEmpty)
            {
                Console.WriteLine($"{container1.Id} оказался пустым и был удален со склада за ненадобностью.");
                Store.TryToDeleteContainer(container1.Id);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Закупка у колхоза
        /// </summary>
        private static void OrderVegetables()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"День {day}");
            Console.WriteLine($"Баланс: {balance:c2}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (packages.Count == 0)
            {
                Console.WriteLine("Ого! Похоже, ты разобрал все закупки! И что теперь мне кушать? Ладно, не переживай, закупки обновятся на следующик день.");
                Console.ReadKey();
                return;
            }
            int i = 1;
            foreach (Package package in packages)
                Console.WriteLine(i++ + ") " + package);
            Console.ResetColor();

        readIndex:
            int packInd;
            string input = "";
            do
            {
                Console.WriteLine("Введи \"-close\" чтобы выйти, \"-show\" чтобы посмотреть список заказов из магазина или номер закупки, которую хочешь приобрести.");
                Console.Write("Учти, что если ты приобретешь покупку, в которой больше контейнеров, чем досутпных мест на складе, случайные контейнеры со склада заменятся контейнерами из заказа: ");
                input = Console.ReadLine();
                if (input == "-close")
                    return;
                if (input == "-show")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    int c = 1;
                    foreach (Order order in orders)
                        Console.WriteLine("Заказ " + c++ + ") " + order);
                    if (orders.Count == 0)
                        Console.WriteLine("Все закупки приобретены.");
                    Console.ResetColor();
                }
            } while (!int.TryParse(input, out packInd) || packInd <= 0 || packInd > packages.Count);
            packInd--;
            if (packages[packInd].Price > balance)
            {
                Console.WriteLine("Сожалею, но эта покупочка тебе не по карману. Выбери другую, или иди зарабатывай fri hudrid bux на эту.");
                goto readIndex;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Поздравляшки! Закупка успешно приобретена.");
            foreach (Container container in packages[packInd].containeres)
            {
                string message = "";
                if (!Store.TryToAddContainer(container, ref message))
                    Console.WriteLine($"Увы, контейнер {container.Id} оказался нерентабельным и не был добавлен. В нем было следующее содержимое:\n{container.GetInfo(0)}");
                else Console.WriteLine($"Контейнер {container.Id} успешно добавлен на склад. {message}");
            }
            Console.ResetColor();

            balance -= packages[packInd].Price;
            packages.RemoveAt(packInd);
            Console.ReadKey();
        }

        /// <summary>
        /// Осуществление заказа.
        /// </summary>
        private static void SellVegetables()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"День {day}");
            Console.WriteLine($"Баланс: {balance:c2}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Store.GetInfo(3));
            if (Store.containers.Count == 0)
                Console.WriteLine("На складе нет ни одного контейнера...");

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (orders.Count == 0)
            {
                Console.WriteLine("Ого! Похоже, ты выполнил все заказы! Не волнуйся, на следующик день на тебя снова свалится ворох работы.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Заказы из магазина:");
            int i = 1;
            foreach (Order order in orders)
                Console.WriteLine(i++ + ") " + order);
            Console.ResetColor();

            int ordInd;
            string input = "";
            do
            {
                Console.WriteLine("Введи \"-close\" чтобы выйти, \"-show\" чтобы посмотреть список заказов из магазина или номер заказа, который хочешь выполнить.");
                Console.Write("Для того, чтобы отправить его, необходимо отправить в магазин нужные контейнеры со склада корованом (и надеяться, что его не ограбят): ");
                input = Console.ReadLine();
                if (input == "-close")
                    return;
                if (input == "-show")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    int c = 1;
                    foreach (Package package in packages)
                        Console.WriteLine("Закупка " + c++ + ") " + package);
                    if (packages.Count == 0)
                        Console.WriteLine("Все закупки приобретены.");
                    Console.ResetColor();
                }
            } while (!int.TryParse(input, out ordInd) || ordInd <= 0 || ordInd > orders.Count);
            ordInd--;

            ExecuteOrder(orders[ordInd]);
        }

        /// <summary>
        /// Отправка партии в магазин.
        /// </summary>
        /// <param name="order">Заказ.</param>
        private static void ExecuteOrder(Order order)
        {
            List<Box> boxes = new List<Box>();
            List<Container> corovan = new List<Container>();

            bool isCompelete = true;
            string id = "";
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Информация о контейнерах на складе:");
                Console.WriteLine(Store.GetInfo(2));
                if (Store.containers.Count == 0)
                    Console.WriteLine("На складе нет ни одного контейнера...");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Выбранный заказ:\n" + order.ToString());
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Содержимое корована для отправки в магазин:");
                foreach (Container item in corovan)
                    Console.WriteLine($"Контейнер {item.Id}; Содержимое: {item.GetInfo(0)}");
                Console.WriteLine("\n-------\n");
                Console.ResetColor();
            readId:
                Console.Write("Введи Id контейнера, который хочешь добавить в корован, дабы отправить его в магазин или \"-close\" чтобы выйти: ");
                id = Console.ReadLine().Trim();
                if (id == "-close")
                    return;
                bool isFound = false;
                foreach (Container container in Store.containers)
                    if (container.Id == id && !corovan.Contains(container))
                    {
                        isFound = true;
                        corovan.Add(container);
                        break;
                    }

                if (!isFound)
                {
                    Console.WriteLine("Такого контейнера не найдено на складе.");
                    goto readId;
                }

                Console.WriteLine("Данный контейнер успешно добавлен к коровану.");
                foreach (Container item in corovan)
                    foreach (var b in item.boxes)
                        boxes.Add(b);

                foreach (Box box in boxes)
                {
                    Box curOrderBox = new Box("", int.MaxValue, 0);
                    foreach (Box ordBox in order.orderBoxes)
                        if (ordBox.Id == box.Id)
                        {
                            curOrderBox = ordBox;
                            break;
                        }
                    if (box.Mass < curOrderBox.Mass)
                    {
                        isCompelete = false;
                        break;
                    }
                }

                if (boxes.Count == 0)
                    isCompelete = false;

                if (!isCompelete)
                {
                    Console.WriteLine("Корован пока не готов к отправке. Определенных овощей пока что мало.");
                    Console.ReadKey();
                }
            } while (!isCompelete);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Поздравляшки! Корован укомплектован и отправлен в путь!");
            Console.ReadKey();
            Animate(ConsoleColor.Yellow, "Корован в пути", 5);

            Console.WriteLine("Корован успешно прибыл в магазин и продал контейнеры. Вот твои кровно заработанные: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"+ {order.price:c2}!");
            Console.ResetColor();

            balance += order.price;
            orders.Remove(order);
            Store.TryToDeleteContainer(id);

            Console.ReadKey();
        }

        /// <summary>
        /// Можно грабить корованы.
        /// </summary>
        private static void GrabitCorovani()
        {
            Console.Write("А ты отчаянный малый, раз решил грабить корованы. Мне это нравится. Итак, для начала надо экипироваться. " +
                  $"\nВведи количество деняк, которое ты готов отдать на то, чтобы снарядить наш отряд для грабежа (значение от 1 до {(int)balance})\n" +
                  $"Если хочешь отказаться, введи \"-close\": ");
            string input = Console.ReadLine();
            if (input == "-close")
                return;
            decimal pay = 0;
            while (!decimal.TryParse(input, out pay) || pay <= 0 || pay > balance)
            {
                Console.Write("Неверный ввод! Повтори попытку: ");
            }
            Console.WriteLine($"Отлично! Твои {pay:c2} успешно вложены в дело. Благодаря этому твой шанс на успешный грабеж корована увеличивается.");

            input = "";
            while (input != "1" && input != "2")
            {
                Console.Write("Выбери, как хочешь грабить корованы: активно (1) или пассивно (2): ");
                input = Console.ReadLine();
            }

            if (input == "2")
            {
                Console.WriteLine("Выбрано пассивное ограбление. От тебя требуется только одно - ждать");
                Console.ReadKey();
                Animate(ConsoleColor.Yellow, "Направляемся в пустыню", 3);
                Animate(ConsoleColor.DarkYellow, "Ожидаем корован", 5);
                Animate(ConsoleColor.DarkRed, "Нападем на корован", 3);
                Animate(ConsoleColor.Red, "Производим грабеж", 3);
                Console.WriteLine("Ограбление завершено. Давай глянем, че у нас по хабару.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Выбрано активное ограбление. Необходимо долбить по клавишам, чтобы произвести ограбление:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ГРАБИТЬ ГРАБИТЬ УБИВАТЬ!!!! ");
                int count = random.Next(30, 61);
                while (count > 0)
                {
                    Console.ReadKey();
                    count--;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nВсе, все, все, можешь расслабиться. Ограбление завершено. Давай глянем, че у нас по хабару (нажми Enter)");
                Console.ReadLine();
                Console.ResetColor();
            }
            int level = (int)Math.Ceiling((decimal)day / 5);

            int gran = (int)(pay * 0.8m);
            int chanceToWinMoney = random.Next(gran, Math.Max((int)(1m * ((int)balance + 1)), gran));
            int chanceToWinContainer = random.Next(gran, Math.Max((int)(1m * balance) + 1, gran));

            if (chanceToWinMoney <= pay * 0.9m)
            {
                int min = (int)(pay * (decimal)Math.Max(1 - (double)level / 50, (-1) * (1 - (double)level / 50)));
                int max = (int)(pay * (decimal)(1 + (double)level / 120));
                decimal win = (int)(random.Next(min, max) * 1.08m);
                if (win == 0)
                    win = pay;
                balance += win;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Опа! Золотишко подъехало! Держи свои {win:c2}");
                Console.ResetColor();
            }
            if (chanceToWinContainer <= pay * 0.9m)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Order order = new Order(Math.Max(day / 2, 1), "Vegetables.txt");

                Package package = new Package(order.orderBoxes, random.Next(1, level / 2 + 1));
                Console.WriteLine($"\nОпа! Найден товар! У нас было 2 пакета травы, 75 таблеток мескалина, 5 упаковок кислотыи и:");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Container container in package.containeres)
                    Console.WriteLine($"Контейнер: {container.Id};\nСодержимое: " + container.GetInfo(0));

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Награбленное отправляется на склад:");
                string message = "";
                foreach (Container container in package.containeres)
                    if (Store.TryToAddContainer(container, ref message))
                        Console.WriteLine($"Контейнер {container.Id} добавлен на склад. {message}");

                Console.ResetColor();
            }
            balance -= pay;
            Console.ForegroundColor = ConsoleColor.Red;
            if (chanceToWinContainer > pay && chanceToWinMoney > pay)
                Console.WriteLine("Сожалею, но корован ушел от нас. Хабара не будет.");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }



        /// <summary>
        /// Ежедневные расходы и обновления.
        /// </summary>
        private static void SimulateDay()
        {
            balance -= pay;
            balance = (int)balance;

            Console.Clear();
            Animate(ConsoleColor.Green, "Подсчитываем расходы", 5);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"С вас списано {pay:c2}, ваш баланс: ${balance}. С уважением, ваш ОвощеБанк.");
            Animate(ConsoleColor.Blue, "", 5);
            if (balance <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Сожалею, но налоги, обыски и бастующие работники обанкротили твой склад! Твой баланс на текущий момент: ${balance}." +
                    $" \nНа этом моя мини-игра завершена, но у меня есть еще кое-что.");
                Console.ReadKey();
                Console.ResetColor();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(File.ReadAllText("Reflection.txt"));
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Налоги уплачены, зарплаты розданы, контейнеры пересчитаны... Впереди новый день.");
            Console.ResetColor();
            Console.ReadKey();

            day++;
            CalculateOrdersAndPackages();
            pay = (int)(day * Math.Log10(day) * 20 * random.Next(1, 11)) + 5;

        }

        /// <summary>
        /// Обновление заказов и закупок.
        /// </summary>
        public static void CalculateOrdersAndPackages()
        {
            int level = (int)Math.Ceiling((decimal)day / 5);
            int curCount = (int)Math.Ceiling((double)day / 7) + 2;

            for (int i = orders.Count; i < curCount; i++)
                orders.Add(new Order(day, "Vegetables.txt"));
            for (int i = packages.Count; i < curCount; i++)
                packages.Add(new Package(orders[random.Next(orders.Count)].orderBoxes, random.Next(1, level / 2 + 1)));



        }
        #endregion

    }
}
