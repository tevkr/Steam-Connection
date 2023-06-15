![logo](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/logo.svg)

[![en](https://img.shields.io/badge/Lang-en-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.md)
[![ru](https://img.shields.io/badge/Lang-ru-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.ru-RU.md)
[![donationalerts](https://img.shields.io/badge/DonationAlerts-red.svg?logo=bitcoin)](https://www.donationalerts.com/r/nom_xd)
[![virustotal](https://img.shields.io/badge/VirusTotal-1/63-green.svg?logo=virustotal)](https://www.virustotal.com/gui/file/21435f3898ebc8268fc8825cd7cb8ea154cb8b0f72b958fd3e035bf3dd7ecc1e)
[![download](https://img.shields.io/badge/Download-Latest-green.svg)](https://github.com/tevkr/Steam-Connection/releases/latest)

Steam Connection - приложение для быстрой смены аккаунтов Steam. Приложение написано на C# WPF. Приложение сохраняет данные от аккаунтов на вашем компьютере и никуда их не отсылает, убедиться в этом можно посмотрев исходный код.
## Предупреждение
Для корректной работы парсера CS:GO званий, а также функции автообновления необходимо установить `.NET Desktop Runtime` от компании `Microsoft`.

Ссылки для скачивания: [Arm64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.14-windows-arm64-installer) | [x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.14-windows-x64-installer) | [x86](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.14-windows-x86-installer).
## Содержание
- [Предисловие](#предисловие)
- [Скриншоты](#скриншоты)
    - [Темная тема](#темная-тема)
- [Как пользоваться](#как-пользоваться)
    - [Добавление аккаунта](#добавление-аккаунта)
    - [Вход в аккаунт](#вход-в-аккаунт)
 - [Дополнительные функции](#дополнительные-функции)
    - [Обновить данные об аккаунтах](#обновить-данные-об-аккаунтах)
    - [Отображение VAC статуса](#отображение-vac-статуса)
    - [Отображение Dota 2 рангов](#отображение-dota-2-рангов)
    - [Отображение CS:GO рангов](#отображение-csgo-рангов)
    - [Пин-код на программу](#пин-код-на-программу)
    - [Перемещение положения аккаунта](#перемещение-положения-аккаунта)
 - [Взаимодействие с интернетом](#взаимодействие-с-интернетом)
 - [Часто задаваемые вопросы](#часто-задаваемые-вопросы)
 - [Послесловие](#послесловие)
 - [Обновления](#обновления)
     - [Обновление 1.0.0.1](#обновление-1001)
     - [Обновление 1.0.1.0](#обновление-1010)
     - [Обновление 1.0.1.1](#обновление-1011)
     - [Обновление 1.0.1.2](#обновление-1012)
     - [Обновление 1.0.2.0](#обновление-1020)
     - [Обновление 1.0.3.0](#обновление-1030)
     - [Обновление 1.0.3.1](#обновление-1031)
     - [Обновление 1.0.3.2](#обновление-1032)
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
### Добавление аккаунта
В пункте аккаунты нужно нажать на синюю кнопку добавить аккаунт справа сверху.
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

Допустим номера аккаунтов: 1 2 3 4 5 6 7 8 9 10. Вы перемещаете аккаунт с номером 9 на место аккаунта с номером 2. После данного действия произойдет следующее: 1 9 2 3 4 5 6 7 8 10. То есть все последующие аккаунты перемещаются на 1 место вперед.
## Взаимодействие с интернетом
Взаимодействие с интернетом происходит только для парса данных.

CS:GO звания: [csgostats.gg](https://csgostats.gg/);

DOTA 2 звания: [opendota](https://docs.opendota.com/);

Steam: [steamidfinder](https://steamid.xyz/).
## Часто задаваемые вопросы
1. Что такое директория стим?

Директория стим - это папка, где находится программа steam.exe.

2. Почему нет звания в CS:GO?

Если ваши звания отображаются некорректно, стоит авторизироваться на сайте [csgostats.gg](https://csgostats.gg/) через стим и следовать рекомендациям.

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

## Обновления
### Обновление 1.0.0.1
Смена [csgo-stats.net](https://csgo-stats.net/) на [csgostats.gg](https://csgostats.gg/).
### Обновление 1.0.1.0
Добавлена возможность запоминать пароль при входе в аккаунт.
### Обновление 1.0.1.1
Исправлен баг при совместном использовании функций запоминания пароля и автоматического выключения Steam Connection.
### Обновление 1.0.1.2
Исправлены баги, добавлен метод ForceWindowToForeground в классе Utils. 
### Обновление 1.0.2.0
- Добавлена возможность обновляться через приложение;
- Добавлен флаг -noreactlogin для фикса последнего UI обновления Steam.
### Обновление 1.0.3.0
- Добавлена поддержка React UI;
- Изменен метод парсинга Steam данных;
- Добавлено автозаполнение директории Steam;
- Исправлены ошибки.
### Обновление 1.0.3.1
- Исправлена ошибка с автообновлением.
### Обновление 1.0.3.2
- Исправлены ошибки связанные с новым обновлением Steam;
- Исправлен парсер CS:GO статистики.