# VegetableStorage
## Консольный менеджер для работы со складом овощей и игра

### Даты разработки: 12.2020

### Ключевые технологии: .Net Core (Console Application)

### Описание

В программе реализована работа с тремя основными сущностями - складом, контейнером и коробкой с овощами, каждая из которых обладает определенной логикой поведения, свойствами и спецификой. В основной части проекта написана логика взаимодействия со складом, требующаяся по заданному ТЗ, однако в другом режиме работы реализована консольная игра по продаже овощей. Подробнее про нее можно почитать в самой программе, где есть описание правил игры. 

Игра получилась довольно интересной, общение с пользователем идет в очень неформальном режиме, используется большое количество шуток. Небольшой кусочек описания этой игры, чтобы заинтересовать пользователя:

Итак, как я сказал ранне, твой склад сейчас обчищается моими людьми. Мы провели небольшой кап ремонт и теперь
он вмещает только 15 контейнеров, стоимость их хранения, коэфициент повреждения, рентабельность и прочее здесь не учитываются.

Смысл игры довольно прост - тебе нужно как можно дольше выживать в этом суровом, несправедливом и аморальном капиталистическом мире.
Каждый день у тебя списывается денюшка за обслуживание твоего складского помещения, работу грузчиков, платятся налоги и прочее. 
Для того, чтобы дяденьки в масках не пришли за тобой, дабы прогуляться с тобой в ближайший лесок, тебе надо как-то эти денюшки зарабатывать и платить.

По мере твоего продвижения по игровой карьерной лестнице ежедневная плата будет лишь расти, поэтому тебе надо будет находить новые методы заработка денег.
Так как игра небольшая, есть два варианта сделать это - попытаться продать партию овощей какому-то магазину или ограбить корован.
ДА, Я ЖЕ СКАЗАЛ, ЗДЕСЬ МОЖНО ГРАБИТЬ КОРОВАНЫ.

Продажа партии возможна, если магазин озвучил свой заказ - n-ое количество определенных овощей и
на складе есть контейнеры с необходимыми овощами. Магазин не будет возражать, если кроме требуемых продуктов в контейнерах будут лишние, но
не примет партию, если хотя бы один пункт из списка заказа не выполнен.

Для того, чтобы на складе как-то появились овощи, их нужно вырастить - в этом тебе любезно помогут колхозы, которые
с радостью впарят тебе пару ящиков помидоров, однако учитывай, что их предложения могут быть как выгодными, так и не очень.
Грабеж корованов - лютый рандом. Это способ заработать денег или овощей, прибив пару верблюдов,
но каким будет полученный хабар, определит Random random = new Random();

Также стоит учитывать, что цены за овощи в данной игре немного оторваны от реальных, поэтому считай, что ты открыл свой
склад на Камчатке, и именно поэтому помидоры стоят как крыло от самолета. Ну и так как это просто игра для пир грейда (лол зачем я вообще
ее делаю, я же склепаю лютый кал из грязи и палок), возможно она окажется довольно дисбалансной - ящики овощей будут стоить как баррель нефти или
в день с тебя будут списывать годовой бюджет Ростовской области - считай, это альфа-тестирование.