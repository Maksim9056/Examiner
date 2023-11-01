# Программа "Экзаменатор" ([www.экзаменатор.москва](https://www.xn--80aaogooijss7i.xn--80adxhks/wordpress/))
# Презназначена для проведения электронных экзаменов (тестирования) пользователей и сотрудников организаций 
## Состаит из 5 проектов:
* **ExamClient** - клиенский кросплатформенный проект написанный на технологии MAUI (frontend), содержит администраторский и клиенский интерфейс работы с программой. Имеет настройку подключения для задания IP и порта, сохраняемую в json файл.
  
* **ExamConsole** - проект для запуска из консоли серверной компоненты, содержит вызов процедуры старта из проекта ExamServer.program.Main()
* **ExamModels** - проект библиотеки (dll), содержащий классы и модель проекта по патерну MVC (model-view-controller)
* **ExamServer** - проект библиотеки (dll), содержащий классы серверной части (backend) для обработки запросов от клиента, классы логирования и отправки сообщенйи на email.
* **ExamServerData** - проект библиотеки (dll), содержит классы работы с СУБД по технологии Entity Framework Core, работающую с СУБД: SQLite, PostGreSQL (основная), MS SQL.
* **ExamWorkerService** - проект для запуска проекта в качетсве службы windows (также демона на linux - в будущем)  <https://metanit.com/sharp/dotnet/3.1.php?ysclid=log4hvjeb8711038467>. 
  Также для запуска программы из консоли.
  


### Автор: Бобрецов Максим Сергеевич
