![logo](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/logo.svg)

[![en](https://img.shields.io/badge/lang-en-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.md)
[![ru](https://img.shields.io/badge/lang-ru-blue.svg)](https://github.com/tevkr/Steam-Connection/blob/main/README.ru-RU.md)
[![donationalerts](https://img.shields.io/badge/donationalerts-red.svg)](https://www.donationalerts.com/r/nom_xd)
[![virustotal](https://img.shields.io/badge/virustotal-1/66-green.svg)](https://www.virustotal.com/gui/file/d382657a09ffa49c91da9d3e1c29ac3437b81c3428444fcaa2b2b1a0a21c80e4/detection)
[![download](https://img.shields.io/badge/download-latest-green.svg)](https://github.com/tevkr/Steam-Connection/releases/latest)

Steam Connection is an application for quickly changing Steam accounts. The application is written in C# WPF. The application saves data from accounts on your computer and does not send them anywhere, you can verify this by looking at the source code.
## Contents
- [Foreword](#foreword)
- [Screenshots](#screenshots)
    - [Dark theme](#dark-theme)
- [How to use](#how-to-use)
    - [Account adding](#account-adding)
    - [Log in to account](#log-in-to-account)
 - [Additional features](#additional-features)
    - [Update account information](#update-account-information)
    - [Displaying the VAC status](#displaying-the-vac-status)
    - [Displaying Dota 2 ranks](#displaying-dota-2-ranks)
    - [Displaying CS:GO ranks](#displaying-csgo-ranks)
    - [PIN code for the program](#pin-code-for-the-program)
    - [Moving the account position](#moving-the-account-position)
 - [Interaction with the Internet](#interaction-with-the-internet)
 - [Frequently Asked Questions](#frequently-asked-questions)
 - [Afterword](#afterword)
 - [Updates](#updates)
     - [1.0.0.1 update](#1001-update)
     - [1.0.1.0 update](#1010-update)
     - [1.0.1.1 update](#1011-update)
     - [1.0.1.2 update](#1012-update)
     - [1.0.2.0 update](#1020-update)
     - [1.0.3.0 update](#1030-update)
## Foreword
This is my first experience in creating an application in C# WPF, I chose MVVM as the main pattern, but due to inexperience it didn't work out very well. If I have had the time and desire, I would have written a project from scratch, but already with a lot of knowledge.
## Screenshots
### Dark theme
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_screen_dark_1.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_screen_dark_2.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_screen_dark_3.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_screen_dark_4.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_screen_dark_5.png)
![screen](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_screen_dark_6.png)
## How to use
### Account adding
In the accounts section, click on the blue add account button on the top right.
![add](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_add_account.png)

In the "Steam link" field, there are 6 ways to specify a link to steam:

Examples:
* https://steamcommunity.com/id/nomerochek
* steamcommunity.com/id/nomerochek
* nomerochek
* http://steamcommunity.com/profiles/76561198337662327
* steamcommunity.com/profiles/76561198337662327
* 76561198337662327
 
The usual time for adding an account is less than 5 seconds.
### Log in to account
You need to click on the account banner with the left mouse button, and click "Yes" on the pop-up banner.
![login_in](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_login.png)

Annotation:
* If you do not want to confirm the login to your account every time (a pop-up banner), go to the settings and enable the "Disable confirmation when logging in to your account" function.
* If you want the program to automatically close when you log in to your account, go to the settings and enable the "Close the program after logging in to your account" function.
* When you log in to your account, it is not necessary to close the previous steam.exe session, the application will do it for you.
## Additional features
### Update account information
If you have changed your nickname, profile picture, or promoted your rank, you can display these changes in the program. Just click on the editing mode in the "Accounts" section (the button with a pencil) ![edit_button](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/edit_button.svg).

And then click on the "Update the data" button. After these actions, the accounts data will be updated ![update button](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/en_update_accounts.svg).
### Displaying the VAC status
![no_vac](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/no_vac.svg) – 0 VAC bans.

![vac](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/vac.svg) – 1+ VAC bans.
### Displaying Dota 2 ranks
Displaying Dota 2 ranks
### Displaying CS:GO ranks
Displaying CS:GO ranks
### PIN code for the program
Each time you start the program, a window will appear with the PIN code input, the number of attempts is unlimited. It is also possible to erase all data and enter an "empty" application.
### Moving the account position
Just click on the editing mode in the "Accounts" section (the button with a pencil) ![edit_button](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/edit_button.svg). And drag and drop accounts using the button ![drag_and_drop](https://github.com/tevkr/Steam-Connection/blob/main/README%20images/drag_and_drop_button.svg).

The accounts are moved according to the following logic:

Let's say the account ids: 1 2 3 4 5 6 7 8 9 10. You are moving the account with the id 9 to the place of the account with the id 2. After this action, the following will happen: 1 9 2 3 4 5 6 7 8 10. That is, all subsequent accounts are moved 1 place ahead.
## Interaction with the Internet
Interaction with the Internet occurs only for data parsing.

CS:GO ranks: [csgostats.gg](https://csgostats.gg/);

DOTA 2 ranks: [opendota](https://docs.opendota.com/);

Steam: [steamidfinder](https://steamid.xyz/).
## Frequently Asked Questions
1. What is the steam directory?

The steam directory is the folder where the steam.exe is located.

2. Why there is no CS:GO rank?

If your ranks are displayed incorrectly, you should log in to the site [csgostats.gg](https://csgostats.gg/) via steam and follow the recommendations.

3. Why there is no Dota 2 rank?

Expose public match data.
## Afterword
The project was worked on by:

[@tevkr](https://github.com/tevkr) - development.

[@rhtfl](https://github.com/rhtfl) - design.

Tested by:

1. [Boicov Artemy](https://vk.com/id262269724).
2. [Kulakov Daniil](https://vk.com/id462365418).
3. [Pryahin Dmitry](https://vk.com/id80937368).
4. [Novikov Dmitry](https://vk.com/id506852309).
5. [Fedorov Ilya](https://vk.com/id108573137).

## Updates
### 1.0.0.1 update
Changing [csgo-stats.net](https://csgo-stats.net/) to [csgostats.gg](https://csgostats.gg/).
### 1.0.1.0 update
Remember the password when logging into the account feature added.
### 1.0.1.1 update
Fixed a bug when using remember password and automatic shutdown of Steam Connection functions together.
### 1.0.1.2 update
Bugs fixed, the ForceWindowToForeground method added in the Utils class.
### 1.0.2.0 update
- Added the ability to update via the app;
- Added the -noreactlogin flag to fix the last Steam UI update.
### 1.0.3.0 update
- Added React UI support;
- Changed the method of parsing Steam data;
- Added auto-completion of the Steam directory;
- Fixed bugs.