![logo](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/logo.svg)

[![en](https://img.shields.io/badge/lang-en-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.md)
[![ru](https://img.shields.io/badge/lang-ru-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.ru-RU.md)
[![donationalerts](https://img.shields.io/badge/donationalerts-red.svg)](https://www.donationalerts.com/r/nom_xd)
[![virustotal](https://img.shields.io/badge/virustotal-1/62-green.svg)](https://www.virustotal.com/gui/file/5f2f067611bd810ac5006d5457c77e0303da070d53f6d7cbfec832f487751b1b/detection)
[![download](https://img.shields.io/badge/download-latest-green.svg)](https://github.com/tevkr/Steam-Connection/releases/tag/V1.0.0.0)

Steam Connection - приложение для быстрой смены аккаунтов Steam. Приложение написано на C# WPF. Приложение сохраняет данные от аккаунтов на вашем компьютере и никуда их не отсылает, убедиться в этом можно посмотрев исходный код.
## Предисловие
Это мой первый опыт в создании приложения на C# WPF, в качестве главного паттерна выбрал MVVM, но из-за неопытности вышло не очень, было бы время и желание, написал бы проект с нуля, но уже c багажом знаний.
## Скриншоты
### Темная тема
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/screen_dark_1.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/screen_dark_2.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/screen_dark_3.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/screen_dark_4.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/screen_dark_5.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/screen_dark_6.png)
## Как пользоваться
### Установка директории стим
Чтобы иметь возможность заходить в аккаунты, нужно указать где именно находится steam.exe. Для этого нужно зайти в настройки, нажать на поле “Директория стим” и найти на компьютере steam.exe.
![dir](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/steam_dir_1.png)
![dir](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/steam_dir_2.png)
### Добавление аккаунта
В пункте аккаунты нужно нажать на синюю кнопку добавить аккаунт слева сверху.
![add](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/add_account.png)

В поле “Ссылка на стим” есть 6 способов указать ссылку на стим:
Примеры:
* https://steamcommunity.com/id/nomerochek
* steamcommunity.com/id/nomerochek
* nomerochek
* http://steamcommunity.com/profiles/76561198337662327
* steamcommunity.com/profiles/76561198337662327
* 76561198337662327
 
Обычное время добавления аккаунта меньше 5 секунд.
### Вход в аккаунт
Нужно нажать на баннер аккаунта левой кнопкой мыши, и нажать “Да” на всплывшем баннере.
![login_in](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/login.png)

Пояснение:
* Если не хотите каждый раз подтверждать вход в аккаунт (всплывающий баннер), следует зайти в настройки и включить функцию “Отключить подтверждение при входе в аккаунт”.
* Если хотите, чтобы программа автоматически закрывалась при входе в аккаунт, следует зайти в настройки и включить функцию “Закрывать программу после входа в аккаунт”.
* При входе в аккаунт не обязательно закрывать предыдущую сессию steam, приложение сделает это за вас.
## Дополнительные функции
### Обновить данные об аккаунтах
Если вы поменяли никнейм, картинку профиля, повысили звание, можно отобразить эти изменения в программе. Достаточно нажать на режим редактирования в пункте “Аккаунты” (Кнопка с карандашом) ![edit_button](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/edit_button.svg).

А далее на кнопку “Обновить данные”. После этих действий данные об аккаунте обновятся ![update button](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/update_accounts.svg).
### Отображение VAC статуса
![no_vac](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/no_vac.svg) – 0 VAC банов.

![vac](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/vac.svg) – 1+ VAC бан.
### Отображение Dota 2 рангов
Отображение Dota 2 рангов
### Отображение CS:GO рангов
Отображение CS:GO рангов
### Пин-код на программу
При каждом запуске программы будет появляться окно с вводом пин-кода, количество попыток неограниченное. Также есть возможность стереть все данные и войти в “чистое” приложение.
### Перемещение положения аккаунта
Достаточно нажать на режим редактирования в пункте “Аккаунты” (Кнопка с карандашом) ![edit_button](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/edit_button.svg). И перетаскивать аккаунты при помощи кнопки ![drag_and_drop](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/drag_and_drop_button.svg).

Перемещение аккаунтов происходит по следуюей логике:

Допустим номера аккаунов: 1 2 3 4 5 6 7 8 9 10. Вы перемещаете аккаунт с номером 9 на место аккаунта с номером 2. После данного действия произойдет следующее: 1 9 2 3 4 5 6 7 8 10. То есть все последующие аккауты перемещаются на 1 место вперед.
## Взаимодействие с интернетом
Взаимодействие с интернетом происходит только для парса данных.

CS:GO звания: [csgo-stats.net](https://csgo-stats.net/).

DOTA 2 звания: [opendota](https://docs.opendota.com/).

Steam: [valvesoftware](https://developer.valvesoftware.com/wiki/Steam_Web_API).
## Как компилировать
Для того, чтобы скомпилировать проект, и при добавлении или изменении аккаунта не вылезала ошибка, нужно добавить .env файл в проект:

![dot_env](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/dot_env.png)

Со следующим содержимим:
```
STEAM_API_KEY=********************************
```
Где * нужно заменить символами своего Steam Api Key. Зарегистрировать ключ веб-API Steam можно по [ссылке](https://steamcommunity.com/dev/apikey).
## Часто задаваемые вопросы
1. Что такое директория стим?

Директория стим - это папка, где находится программа steam.exe.

2. Почему нет звания в CS:GO?

Если ваши звания отображаются некорректно стоит добавить в друзья бота от [csgo-stats.net](https://csgo-stats.net/). Подробнее по [ссылке](https://csgo-stats.net/tools/steam).

3. Почему нет звания в Dota 2?

Включите общедоступную историю матчей.
## Послесловие
Над проектом работали:

[@tevkr](https://github.com/tevkr) - разработка.

[@rhtfl](https://github.com/rhtfl) - дизайн.

Тестировали:

1. [Бойцов Артемий](https://vk.com/id262269724).
2. [Кулаков Даниил](https://vk.com/id462365418).
3. [Пряхин Дмитрий](https://vk.com/id80937368).
4. [Новиков Дмитрий](https://vk.com/id506852309).
5. [Федоров Илья](https://vk.com/id108573137).

По вопросам (баги, что-то не работает) [сюда](https://vk.com/id152392361).
