![logo](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/logo.svg)

[![en](https://img.shields.io/badge/lang-en-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.md)
[![ru](https://img.shields.io/badge/lang-ru-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.ru-RU.md)
[![virustotal](https://img.shields.io/badge/virustotal-1/57-green.svg)](https://www.virustotal.com/gui/file/43be1d07ced3240dedc1394990915f629e21c1324b5ba7696ba2b805c6c59b8e/detection)
[![support](https://img.shields.io/badge/donationalerts-donate-red.svg)](https://www.donationalerts.com/r/nom_xd)
[![download](https://img.shields.io/badge/download-blue.svg)](https://www.donationalerts.com/r/nom_xd)

Steam Connection - приложение для быстрой смены аккаунтов Steam. Приложение написано на C# WPF. Приложение сохраняет данные от аккаунтов на вашем компьютере и никуда их не отсылает, убедиться в этом можно посмотрев исходный код.
## Предисловие
Это мой первый опыт в создании приложения на C# WPF, в качестве главного паттерна выбрал MVVM, но из-за неопытности вышло не очень, было бы время и желание, написал бы проект с нуля, но уже c багажом знаний. 
Также стоит сказать, что проект состоит на 98+ % из кода на Python, потому что для парсинга CS:GO званий я выбрал самый популярный сайт со статистикой CS:GO - [csgostats.gg](https://csgostats.gg/). Но из-за CloudFlare я не мог нормально получить html код страницы при помощи C#, однако у меня это получилось при помощи Python. И чтобы пользователям не пришлось скачивать и устанавливать Python ради одного десяти строчного скрипта, я добавил интерпретатор Python в проект.
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

Не стоит пытаться прекратить работу приложения во время добавления аккаунта. Приложение не залагало, оно парсит данные, такие как звания в играх, никнеймы, вак статусы, аватарки. 
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

Допустим номера аккаунов: 1 2 3 4 5 6 7 8 9 10. Вы перемещаете аккаунт с номером 9 на место аккаунта с номером 2. После данного действия произойдет следующее: 1 9 2 3 4 5 6 7 8 9 10. То есть все последующие аккауты перемещаются на 1 место вперед.
## Взаимодействие с интернетом
Взаимодействие с интернетом происходит только для парса данных.

CS:GO звания: [csgostats.gg](https://csgostats.gg/).

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
## Послесловие
Над проектом работали:

[@tevkr](https://github.com/tevkr) - разработка.

[@rhtfl](https://github.com/rhtfl) - дизайн.

Тестировали:
//TODO
