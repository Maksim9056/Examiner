using ExamClient.Resources.Resx;
using ExamModels;

namespace Client.Users.Doc.DocTestQuestionsTheAnswersMark;

public partial class DocTestQuestionsTheAnswersMark : ContentPage
{
    private List<DocTestMenu.DocTestMenu.RefTestQuestion> refTestQuestions;

    public CommandCL command = new CommandCL();
    private TestQuestionEditorViewModel viewModel;
    private TestQuestionManager viewModelManager;
    private ExamModels.Test CurrrentTest;
    public ExamModels.Questions Questions { get; set; }
    public ExamModels.Test Test { get; set; }
    private ExamModels.Exams Exams;
    private ExamModels.User CurrrentUser;
   // List <string> Галочка = new List<string>();
    List<RefTestQuestion> refTestQuestions1 = new List<RefTestQuestion>();
    ExamModels.TestQuestion[] testQuestions { get; set; }
    static Dictionary<int,string>keyValuePairs = new Dictionary<int,string>();
    public ExamModels.Questions [] Questionsss   { get; set; }

    public List<ExamModels.Questions> questions1 { get; set; } 
    Галочка[] Ставить;
    List<RefTestQuestion> testQuestionListS = new List<RefTestQuestion>();

    public DocTestQuestionsTheAnswersMark(ExamModels.Test refTestQuestions, ExamModels.Questions questions, ExamModels.Exams exams, ExamModels.User user, List<ExamModels.Questions> questions1s)
    {

        InitializeComponent(); 
        
        
        if (questions1 == null)
        {
            questions1 = new List<Questions>();
            questions1 = questions1s;
        }
        //int Locat ;
        viewModel = new TestQuestionEditorViewModel();
        viewModelManager = new TestQuestionManager();
        //CurrrentTest = refTestQuestions;
        Test = refTestQuestions;
        Title = Test.Name_Test;
        Questions = questions;
        //  bool fALSE = false;

        //      questions1
        //if (questions1.SequenceEqual(questions1s))
        //{

        //} // Проверка совпадения с использованием SequenceEqual()

        //bool areEqualы = questions1.Equals(questions2);

        //// Проверка совпадения с использованием Equals()
        //if (!questions1.Any(q => q == questions1s))
        //{
        //    questions1.Add(questions);
        //}
      

        if (!questions1.Any(q => q.QuestionName == questions.QuestionName))
        {
            questions1.Add(questions);
        }

        //if (questions1 == null)
        //{        
        // questions1.Add(questions);
        //}
        //else
        //{
        //    for(int i = 0; i < questions1.Count(); i++) 
        //    {
        //        if (questions1[i] == questions)
        //        {
        //            fALSE = true;
        //        }
        //        else
        //        {
             
        //        }
            
        //    }

        //    if(fALSE == false)
        //    {
        //        questions1.Add (questions);
        //    }


        //}
      //  ExamModels.Questions[] Questionss = new Questions[questions1.Count()];

      //bool add = false;
      //  for (int i = 0;i < questions1.Count(); i++)
      //  {
      //      if (questions1[i] == Questionss[i])
      //      {
      //          add= true;
      //      }
      //      else
      //      {

      //      }
      //  }

      //  if(add == false)
      //  {
      //      for (int i = 0; i < questions1.Count(); i++)
      //      {
      //          Questionss[i] = questions1[i];
      //      }

      //  }
      //  if(Questionsss == null)
      //  {
      //   Questionsss = Questionss;

      //  }
      //  else
      //  {
      //      for (int i = 0; i < Questionsss.Length; i++)
      //      {
      //          if (Questionsss[i] == Questionss[i])
      //          {
      //              add = true;
      //          }
      //          else
      //          {
                  
      //          }
      //      }
        
        //Галочка.Add("v");
        TestList.ItemsSource = GetTestQuestions(Test, questions); 
        Ставить = new Галочка[refTestQuestions1.Count()];
 
      //  TestName.Text = Test.Name_Test;
        CurrrentUser = user;
        Exams = exams;
      //  Names. = keyValuePairs;

#pragma warning disable CS0618 // Тип или член устарел
        MessagingCenter.Subscribe<DocTestQuestionsTheAnswersMark>(this, "UpdateForm", (sender) =>
        {
            // Perform the necessary updates to the form here
            // For example, update the fields, refresh data, etc.
            TestList.ItemsSource = GetTestQuestions(Test,questions);
        });
#pragma warning restore CS0618 // Тип или член устарел
    }


    //public void Update()
    //{
    //    Test = refTestQuestions;
    //    Questions = questions;
    //}

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;
        //Сдесь проверку  на выбраный вопрос ввиде списка массивов и выяснить

        var selectedTestQuestion = (RefTestQuestion)e.SelectedItem;
 
        if (!questions1.Any(q => q.QuestionName == selectedTestQuestion.TestQuestion.IdQuestions.QuestionName))
        {
          
            await DisplayAlert(AppResources.Выбранныйвопрос, selectedTestQuestion.TestQuestion.IdQuestions.QuestionName, AppResources.Ок);
            await Navigation.PushAsync(new Doc.DocAnswerQuestins.DocAnswerQuestins(selectedTestQuestion.TestQuestion.IdQuestions, Test, Exams,CurrrentUser, questions1));

        }
        else 
        {
            await DisplayAlert(AppResources.Выужеответилинавопрос, selectedTestQuestion.TestQuestion.IdQuestions.QuestionName, AppResources.Ок);
        }

        // var selectedTestQuestion = (RefQuestionAnswer)e.SelectedItem;
        //await DisplayAlert("Выбранный ответ", selectedTestQuestion.QuestionAnswer.Answer.AnswerOptions, "OK");
        ((ListView)sender).SelectedItem = null;

    }

    private List<RefTestQuestion> GetTestQuestions(ExamModels.Test test, ExamModels.Questions questions)
    {
        List<RefTestQuestion> testQuestionList = new List<RefTestQuestion>();

        CommandCL.ExamsListGet = null;
        viewModelManager.GetTestQuestionList(test);

        //for (int i = 0; i < refTestQuestions1.Count(); i++)
        //{
          

        //}

        if (CommandCL.TestQuestionListGet == null)
        {
            // Handle the case when the test list is null
        }
        else
        {                  //  questions1.Add(questions);

            for (int i = 0; i < CommandCL.TestQuestionListGet.ListTestQuestion.Count; i++)
            {

                if (!questions1.Any(q => q.QuestionName == CommandCL.TestQuestionListGet.ListTestQuestion[i].IdQuestions.QuestionName))
                {
                    var refTestQuestion = new RefTestQuestion { TestQuestion = CommandCL.TestQuestionListGet.ListTestQuestion[i], EditCommand = "" };
                    testQuestionList.Add(refTestQuestion);
                    testQuestionListS.Add(refTestQuestion);
                }
                else
                {
                   
                    var refTestQuestion = new RefTestQuestion { TestQuestion = CommandCL.TestQuestionListGet.ListTestQuestion[i], EditCommand = " ✔" };
                    testQuestionList.Add(refTestQuestion);
                    testQuestionListS.Add(refTestQuestion);
                }

                //if (CommandCL.TestQuestionListGet.ListTestQuestion[i].IdQuestions.QuestionName == questions.QuestionName)
                //{
                 
                
                //}
                //else
                //{
             
         
                //}
      
            
            }
        }


        return testQuestionList;
    }

    public class RefTestQuestion
    {
        public ExamModels.TestQuestion TestQuestion { get; set; }
        public string EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }


    public class RefQuestionAnswer
    {
        public ExamModels.QuestionAnswer QuestionAnswer { get; set; }
        public string EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }

    private  void TestStart_Clicked(object sender, EventArgs e)
    {

        if(testQuestionListS.Count() == questions1.Count())
        {

            DisplayAlert(AppResources.Сохранентест, AppResources.Завершентест, AppResources.Ок);

            Roles roles = new Roles { Id = CurrrentUser.Id_roles_users};
            Regis_users regis_Users = new Regis_users()
            { Id = CurrrentUser.Id, Employee_Mail = CurrrentUser.Employee_Mail, Name_Employee = CurrrentUser.Name_Employee, Password = CurrrentUser.Password, Rechte = roles,
                Filles = CurrrentUser.Email.Id
            };
            Navigation.PushAsync(new Client.Users.Users(regis_Users));
        }
        else

        {

            DisplayAlert(AppResources.Тестнезавершен,AppResources.Ответилиненавсевопросы, AppResources.Ок);
        }
        
    }
}