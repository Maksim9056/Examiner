//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace ExamAPI.Controllers.Catalog
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Catalog : ControllerBase
//    {
//        //GlobalClass catalog = new GlobalClass();

//        public void AddCatalog()
//        {
//            //catalog.AddCatalog();
//        }
//        // GET: api/<Catalog>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<Catalog>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<Catalog>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<Catalog>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<Catalog>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }


//        //public void TestSQL()
//        //{
//        //    using (ApplicationContext db = new ApplicationContext())
//        //    {

//        //    }


//        //    int Count_roles = 0;
//        //    using (ApplicationContext db = new ApplicationContext())
//        //    {
//        //        // получаем объекты из бд и выводим на консоль
//        //        var users = db.Roles.Count();
//        //        Count_roles = users;


//        //    }


//        //    //  string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'MyAir-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

//        //    if (Count_roles > 0)
//        //    {

//        //        //var backupFilePath = Environment.CurrentDirectory.ToString(); // Замените на путь, где вы хотите сохранить резервную копию
//        //        //                                                              // Создание подключения к базе данных PostgreSQL
//        //        //using (var connection = new NpgsqlConnection(GlobalClass.connectionStringPostGreSQL))
//        //        //{
//        //        //    var NAME = Environment.MachineName;
//        //        //    connection.Open();
//        //        //    string backupPath = $"{backupFilePath} \\Backup.bak";
//        //        //    string backupQuery = $"BACKUP DATABASE Testdb TO DISK = '{backupPath}'";

//        //        //    NpgsqlCommand command = new NpgsqlCommand(backupQuery, connection);


//        //        //    command.ExecuteNonQuery();
//        //        // }
//        //        //     var connectionString = "Host=localhost;Port=5432;Database=Testdb;Username=postgres;Password=1"; // Замените на свою строку подключения         

//        //        //      string connectionSмtring = "YourConnectionString";







//        //        // db.Database.SqlQueryRaw(sqlCommand,);
//        //        // Создание SQL-команды для выполнения резервного копирования

//        //        //using (var command = new NpgsqlCommand($"pg_dump -U <username> -Fc -f {backupFilePath} <database_name>", connection))
//        //        //{
//        //        //    // Замените "<username>" на имя пользователя для подключения к PostgreSQL
//        //        //    // Замените "<database_name>" на имя базы данных, которую вы хотите скопировать

//        //        //    // Выполнение команды резервного копирования
//        //        //    command.ExecuteNonQuery();
//        //        //}


//        //    }
//        //    else
//        //    {
//        //        // Console.WriteLine("Users list:");
//        //        //foreach (int  u in users)
//        //        //{
//        //        //    //     Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
//        //        //}
//        //        int D = 0;
//        //        using (ApplicationContext context = new ApplicationContext())
//        //        {
//        //            D = context.Roles.Count();
//        //        }

//        //        if (D == 0)
//        //        {
//        //            using (ApplicationContext db = new ApplicationContext())
//        //            {
//        //                // создаем два объекта User

//        //                Roles user1 = new Roles { Name_roles = "Пользователь" };
//        //                // добавляем их в бд
//        //                db.Roles.AddRange(user1);
//        //                db.SaveChanges();
//        //            }


//        //            // добавление данных
//        //            using (ApplicationContext db = new ApplicationContext())
//        //            {
//        //                // создаем два объекта User

//        //                Roles user1 = new Roles { Name_roles = "Admin" };
//        //                // добавляем их в бд
//        //                db.Roles.AddRange(user1);
//        //                db.SaveChanges();
//        //            }



//        //            string Email = "info@экзаменатор.москва";
//        //            DateTime dateTime = DateTime.Now;
//        //            var data = $"{dateTime:F}";
//        //            Roles roles = new Roles { Id = 1 };

//        //            // добавление данных
//        //            using (ApplicationContext db = new ApplicationContext())
//        //            {
//        //                // создаем два объекта User
//        //                Filles filles = new Filles() { Id = 0 };
//        //                User user1 = new User { Name_Employee = "Admin", Password = "Admin", DataMess = data, Id_roles_users = 2, Employee_Mail = Email, Email = filles };

//        //                // добавляем их в бд
//        //                db.Users.AddRange(user1);
//        //                db.SaveChanges();
//        //            }
//        //        }
//        //    }
//        //}
//    } 
//}