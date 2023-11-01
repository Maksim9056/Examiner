# Программа "Экзаменатор" ([www.экзаменатор.москва](https://www.xn--80aaogooijss7i.xn--80adxhks/wordpress/))
# Презназначена для проведения электронных экзаменов (тестирования) пользователей и сотрудников организаций 
## Состаит из 5 проектов:
* **ExamClient** - клиенский кросплатформенный проект написанный на технологии MAUI, содержит администраторский и клиенский интерфейс работы с программой. Имеет настройку подключения для задания IP и порта, сохраняемую в json файл.
  
* **ExamConsole** - проект для запуска из консоли серверной компоненты, содержит вызов процедуры старта из проекта ExamServer.program.Main()
* **ExamModels** - проект библиотеки (dll), содержащий классы и модель проекта по патерну MVC (model-view-controller)
* **ExamServer** - 
* **ExamServerData** - проект библиотеки (dll), содержит классы работы с СУБД по технологии Entity Framework Core, работающую с СУБД: SQLite, PostGreSQL, MS SQL.
* **ExamWorkerService** - 


### Автор: Бобрецов Максим Сергеевич
