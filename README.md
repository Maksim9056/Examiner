# Программа "Экзаменатор" ([www.экзаменатор.москва](https://www.xn--80aaogooijss7i.xn--80adxhks/wordpress/))
# Презназначена для проведения электронных экзаменов (тестирования) пользователей и сотрудников организаций 
## Состаит из 5 проектов:
* **ExamClient** - клиенский кросплатформенный проект написанный на технологии MAUI (frontend), содержит администраторский и клиенский интерфейс работы с программой. Имеет настройку подключения для задания IP и порта, сохраняемую в json файл. <https://metanit.com/sharp/maui/?ysclid=log69x8jam340753752>
  
* **ExamConsole** - проект для запуска из консоли серверной компоненты, содержит вызов процедуры старта из проекта ExamServer.program.Main()
* **ExamModels** - проект библиотеки (dll), содержащий классы и модель проекта по патерну MVC (model-view-controller)
* **ExamServer** - проект библиотеки (dll), содержащий классы серверной части (backend) для обработки запросов от клиента, классы логирования и отправки сообщений на email.
* **ExamServerData** - проект библиотеки (dll), содержит классы работы с СУБД по технологии Entity Framework Core, работающую с СУБД: SQLite, PostGreSQL (основная), MS SQL. <https://metanit.com/sharp/efcore/>
* **ExamWorkerService** - проект для запуска проекта в качеcтве службы windows (также демона на linux - в будущем)  <https://metanit.com/sharp/dotnet/3.1.php?ysclid=log4hvjeb8711038467>. 
  Также для запуска программы из консоли.
=
## Проект клиента (frontend) содержит два интерфейса:
* **Администратора** - предназначенный для работы с пользователями, создания бекапов, а также создания контента (содержания) экзаменов, тестов, вопросов и ответов.
  
* **Пользователя** - предназначено для работы пользователей с назначенными экзаменами, просмотра статистики и графического отображения.

## Сервер и клиента сохраняют свои настройки в файлы json:
* **Server.json**
  ```
  {"Ip_adress":"192.168.1.170","Port":9595,"TypeSQL":1}
  ```
>Где TypeSQL: 1. PostGreSQL,  2. SQLite, 3. MS SQL
* **Client.json**
   ```
  {"Ip_adress":"192.168.1.170","Port":9595}
  ```
   Файлы формируются со значениями по умолчанию при первом старте программы.

## Клиент имеет 3 (три) стартовые страницы:
1. Интерфейс ввода логина и пароля
<br>
<img src='https://github.com/Maksim9056/Examiner/assets/108364585/94c4e241-85c3-473d-9345-a9a3f9eb7e9d' width="450" />
</br>

2. Интерфейс задания настроек подключения (IP, Port)
<br>
<img src='https://github.com/Maksim9056/Examiner/assets/108364585/238543a2-353e-4e86-8406-6ee909c305e6' width="450" />
</br>

3. Интерфейс регистрации нового пользователя
<br>
<img src='https://github.com/Maksim9056/Examiner/assets/108364585/9718dbb2-6a14-4712-9520-95d8b04ddbb5' width="450" />
</br>

## Сервер запускается в 2 режимах:
1. В режиме консоли запуском файла ExamWorkerService.exe 
2. В режиме службы надо зарегистрировать командой   
 ```
 sc.exe create "Examiner" binpath="C:\Examiner\ExamWorkerService.exe"
 ```  
   и запустить приложение Службы найти зарегистрированую службу и поставить автоматичекий запуск и запустить.


### Автор: Бобрецов Максим Сергеевич
